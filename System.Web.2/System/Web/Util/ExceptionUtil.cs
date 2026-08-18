using System;

namespace System.Web.Util
{
	// Token: 0x020001F9 RID: 505
	internal static class ExceptionUtil
	{
		// Token: 0x060018F6 RID: 6390 RVA: 0x0004CE4A File Offset: 0x0004B04A
		internal static ArgumentException ParameterInvalid(string parameter)
		{
			return new ArgumentException(SR.GetString("Parameter_Invalid", new object[]
			{
				parameter
			}), parameter);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0004CE66 File Offset: 0x0004B066
		internal static ArgumentException ParameterNullOrEmpty(string parameter)
		{
			return new ArgumentException(SR.GetString("Parameter_NullOrEmpty", new object[]
			{
				parameter
			}), parameter);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0004CE82 File Offset: 0x0004B082
		internal static ArgumentException PropertyInvalid(string property)
		{
			return new ArgumentException(SR.GetString("Property_Invalid", new object[]
			{
				property
			}), property);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0004CE9E File Offset: 0x0004B09E
		internal static ArgumentException PropertyNullOrEmpty(string property)
		{
			return new ArgumentException(SR.GetString("Property_NullOrEmpty", new object[]
			{
				property
			}), property);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0004CEBA File Offset: 0x0004B0BA
		internal static InvalidOperationException UnexpectedError(string methodName)
		{
			return new InvalidOperationException(SR.GetString("Unexpected_Error", new object[]
			{
				methodName
			}));
		}
	}
}
