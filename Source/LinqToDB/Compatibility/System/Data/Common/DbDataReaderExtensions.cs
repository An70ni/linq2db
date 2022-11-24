#if NET40
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace System.Data.Common
{
	internal static class DbDataReaderExtensions
	{
		public static T GetFieldValue<T>(this DbDataReader dataReader,int ordinal)
		{
			return (T)dataReader.GetValue(ordinal);
		}
	}
}
#endif
