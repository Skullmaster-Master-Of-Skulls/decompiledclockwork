using System;
using System.Data.Entity.SqlServer.Resources;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000008 RID: 8
	internal class Check
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003690 File Offset: 0x00001890
		public static T NotNull<T>(T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			return value;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000036A2 File Offset: 0x000018A2
		public static T? NotNull<T>(T? value, string parameterName) where T : struct
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			return value;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000036B5 File Offset: 0x000018B5
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
