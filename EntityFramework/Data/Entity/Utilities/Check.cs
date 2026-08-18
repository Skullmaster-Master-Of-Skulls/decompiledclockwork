using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000004 RID: 4
	internal class Check
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002F56 File Offset: 0x00001156
		public static T NotNull<T>(T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			return value;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002F68 File Offset: 0x00001168
		public static T? NotNull<T>(T? value, string parameterName) where T : struct
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			return value;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F7B File Offset: 0x0000117B
		public static string NotEmpty(string value, string parameterName)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(Strings.ArgumentIsNullOrWhitespace(parameterName));
			}
			return value;
		}
	}
}
