#if NET40
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
	internal static class TypeExtensions
	{
		public static Type[] GenericTypeArguments(this Type type)
		{
			if (type.IsGenericType && !type.IsGenericTypeDefinition)
			{
				return type.GetGenericArguments();
			}

			return TypeEx.EmptyTypes;
		}
	}
}
#endif
