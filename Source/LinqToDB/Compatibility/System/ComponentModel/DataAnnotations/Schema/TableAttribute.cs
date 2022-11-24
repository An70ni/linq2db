#if NET40
using System;
namespace System.ComponentModel.DataAnnotations.Schema
{

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class TableAttribute : Attribute
	{
		private readonly string _name;

		private string? _schema;

		public string Name
		{
			get
			{
				return _name;
			}
		}

		public string? Schema
		{
			get
			{
				return _schema;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException("IsNullOrWhiteSpace", nameof(value));
				}

				_schema = value;
			}
		}

		public TableAttribute(string name)
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
