// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB.Mapping;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.Default.Oracle
{
	[Table("DecimalOverflow")]
	public class DecimalOverflow
	{
		[Column("Decimal1")] public decimal? Decimal1 { get; set; } // NUMBER (38,20)
		[Column("Decimal2")] public decimal? Decimal2 { get; set; } // NUMBER (31,2)
		[Column("Decimal3")] public decimal? Decimal3 { get; set; } // NUMBER (38,36)
		[Column("Decimal4")] public decimal? Decimal4 { get; set; } // NUMBER (29,0)
		[Column("Decimal5")] public decimal? Decimal5 { get; set; } // NUMBER (38,38)
	}
}
