// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB.Mapping;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.Default.PostgreSQL
{
	[Table("InheritanceChild")]
	public class InheritanceChild
	{
		[Column("InheritanceChildId" , IsPrimaryKey = true)] public int     InheritanceChildId  { get; set; } // integer
		[Column("InheritanceParentId"                     )] public int     InheritanceParentId { get; set; } // integer
		[Column("TypeDiscriminator"                       )] public int?    TypeDiscriminator   { get; set; } // integer
		[Column("Name"                                    )] public string? Name                { get; set; } // character varying(50)
	}
}
