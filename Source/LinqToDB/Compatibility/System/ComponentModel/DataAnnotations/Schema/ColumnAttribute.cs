#if NET40
using System.ComponentModel.DataAnnotations.Resources;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations.Schema
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class ColumnAttribute : Attribute
	{
		private readonly string? _name;

		private string? _typeName;

		private int _order = -1;

		public string? Name
		{
			get
			{
				return _name;
			}
		}

		public int Order
		{
			get
			{
				return _order;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value));
				}

				_order = value;
			}
		}

		public string? TypeName
		{
			get
			{
				return _typeName;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException("IsNullOrWhiteSpace",nameof(value));
				}

				_typeName = value;
			}
		}

		public ColumnAttribute()
		{
		}

		public ColumnAttribute(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("IsNullOrWhiteSpace",nameof(name));
			}

			_name = name;
		}
	}
}
#endif
