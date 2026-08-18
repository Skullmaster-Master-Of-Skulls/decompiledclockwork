using System;

namespace System.Web.Util
{
	// Token: 0x02000761 RID: 1889
	internal static class ExceptionUtil
	{
		// Token: 0x06005BE2 RID: 23522 RVA: 0x001708D4 File Offset: 0x0016F8D4
		internal static ArgumentException ParameterInvalid(string parameter)
		{
			return new ArgumentException(SR.GetString("Parameter_Invalid", new object[]
			{
				parameter
			}), parameter);
		}

		// Token: 0x06005BE3 RID: 23523 RVA: 0x00170900 File Offset: 0x0016F900
		internal static ArgumentException ParameterNullOrEmpty(string parameter)
		{
			return new ArgumentException(SR.GetString("Parameter_NullOrEmpty", new object[]
			{
				parameter
			}), parameter);
		}

		// Token: 0x06005BE4 RID: 23524 RVA: 0x0017092C File Offset: 0x0016F92C
		internal static ArgumentException PropertyInvalid(string property)
		{
			return new ArgumentException(SR.GetString("Property_Invalid", new object[]
			{
				property
			}), property);
		}

		// Token: 0x06005BE5 RID: 23525 RVA: 0x00170958 File Offset: 0x0016F958
		internal static ArgumentException PropertyNullOrEmpty(string property)
		{
			return new ArgumentException(SR.GetString("Property_NullOrEmpty", new object[]
			{
				property
			}), property);
		}

		// Token: 0x06005BE6 RID: 23526 RVA: 0x00170984 File Offset: 0x0016F984
		internal static InvalidOperationException UnexpectedError(string methodName)
		{
			return new InvalidOperationException(SR.GetString("Unexpected_Error", new object[]
			{
				methodName
			}));
		}
	}
}
