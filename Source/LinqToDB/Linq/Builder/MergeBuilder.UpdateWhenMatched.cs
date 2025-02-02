﻿using System.Linq;
using System.Linq.Expressions;

namespace LinqToDB.Linq.Builder
{
	using LinqToDB.Expressions;
	using SqlQuery;

	using static LinqToDB.Reflection.Methods.LinqToDB.Merge;

	internal partial class MergeBuilder
	{
		internal class UpdateWhenMatched : MethodCallBuilder
		{
			protected override bool CanBuildMethodCall(ExpressionBuilder builder, MethodCallExpression methodCall, BuildInfo buildInfo)
			{
				return methodCall.IsSameGenericMethod(UpdateWhenMatchedAndMethodInfo);
			}

			protected override IBuildContext BuildMethodCall(ExpressionBuilder builder, MethodCallExpression methodCall, BuildInfo buildInfo)
			{
				// UpdateWhenMatchedAnd<TTarget, TSource>(merge, searchCondition, setter)
				var mergeContext = (MergeContext)builder.BuildSequence(new BuildInfo(buildInfo, methodCall.Arguments[0]));

				var statement = mergeContext.Merge;
				var operation = new SqlMergeOperationClause(MergeOperationType.Update);

				var predicate = methodCall.Arguments[1];
				var setter    = methodCall.Arguments[2];

				if (!setter.IsNullValue())
				{
					var setterExpression = (LambdaExpression)setter.Unwrap();
					UpdateBuilder.BuildSetterWithContext(
						builder,
						buildInfo,
						setterExpression,
						mergeContext.TargetContext,
						operation.Items,
						mergeContext.TargetContext, mergeContext.SourceContext);
				}
				else
				{
					// build setters like QueryRunner.Update
					var sqlTable   = (SqlTable)statement.Target.Source;
					var param      = Expression.Parameter(sqlTable.ObjectType, "s");
					var keys       = sqlTable.GetKeys(false).Cast<SqlField>().ToList();
					foreach (var field in sqlTable.Fields.Where(f => f.IsUpdatable).Except(keys))
					{
						var expression = ExpressionExtensions.GetMemberGetter(field.ColumnDescriptor.MemberInfo, param);
						var tgtExpr    = mergeContext.TargetContext.ConvertToSql(builder.ConvertExpression(expression), 1, ConvertFlags.Field)[0].Sql;
						var srcExpr    = mergeContext.SourceContext.ConvertToSql(builder.ConvertExpression(expression), 1, ConvertFlags.Field)[0].Sql;

						operation.Items.Add(new SqlSetExpression(tgtExpr, srcExpr));
					}

					// skip empty Update operation with implicit setter
					// per https://github.com/linq2db/linq2db/issues/2843
					if (operation.Items.Count == 0)
						return mergeContext;
				}

				statement.Operations.Add(operation);

				if (!predicate.IsNullValue())
				{
					var condition     = (LambdaExpression)predicate.Unwrap();

					operation.Where = BuildSearchCondition(builder, statement, mergeContext.TargetContext, mergeContext.SourceContext, condition);
				}

				return mergeContext;
			}
		}
	}
}
