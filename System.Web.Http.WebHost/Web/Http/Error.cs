using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Http.Properties;

namespace System.Web.Http
{
	// Token: 0x02000003 RID: 3
	internal static class Error
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002470 File Offset: 0x00000670
		internal static string Format(string format, params object[] args)
		{
			return string.Format(CultureInfo.CurrentCulture, format, args);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000247E File Offset: 0x0000067E
		internal static ArgumentException Argument(string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000248C File Offset: 0x0000068C
		internal static ArgumentException Argument(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs), parameterName);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000249C File Offset: 0x0000069C
		internal static ArgumentException ArgumentUriNotHttpOrHttpsScheme(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidHttpUriScheme, new object[]
			{
				actualValue,
				"http",
				"https"
			}), parameterName);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024D8 File Offset: 0x000006D8
		internal static ArgumentException ArgumentUriNotAbsolute(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidAbsoluteUri, new object[]
			{
				actualValue
			}), parameterName);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002504 File Offset: 0x00000704
		internal static ArgumentException ArgumentUriHasQueryOrFragment(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentUriHasQueryOrFragment, new object[]
			{
				actualValue
			}), parameterName);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000252D File Offset: 0x0000072D
		internal static ArgumentNullException PropertyNull()
		{
			return new ArgumentNullException("value");
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002539 File Offset: 0x00000739
		internal static ArgumentNullException ArgumentNull(string parameterName)
		{
			return new ArgumentNullException(parameterName);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002541 File Offset: 0x00000741
		internal static ArgumentNullException ArgumentNull(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentNullException(parameterName, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002550 File Offset: 0x00000750
		internal static ArgumentException ArgumentNullOrEmpty(string parameterName)
		{
			return Error.Argument(parameterName, CommonWebApiResources.ArgumentNullOrEmpty, new object[]
			{
				parameterName
			});
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002574 File Offset: 0x00000774
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName, object actualValue, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002584 File Offset: 0x00000784
		internal static ArgumentOutOfRangeException ArgumentMustBeGreaterThanOrEqualTo(string parameterName, object actualValue, object minValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeGreaterThanOrEqualTo, new object[]
			{
				minValue
			}));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000025B0 File Offset: 0x000007B0
		internal static ArgumentOutOfRangeException ArgumentMustBeLessThanOrEqualTo(string parameterName, object actualValue, object maxValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeLessThanOrEqualTo, new object[]
			{
				maxValue
			}));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025DA File Offset: 0x000007DA
		internal static KeyNotFoundException KeyNotFound()
		{
			return new KeyNotFoundException();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000025E1 File Offset: 0x000007E1
		internal static KeyNotFoundException KeyNotFound(string messageFormat, params object[] messageArgs)
		{
			return new KeyNotFoundException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025EF File Offset: 0x000007EF
		internal static ObjectDisposedException ObjectDisposed(string messageFormat, params object[] messageArgs)
		{
			return new ObjectDisposedException(null, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000025FE File Offset: 0x000007FE
		internal static OperationCanceledException OperationCanceled()
		{
			return new OperationCanceledException();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002605 File Offset: 0x00000805
		internal static OperationCanceledException OperationCanceled(string messageFormat, params object[] messageArgs)
		{
			return new OperationCanceledException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002613 File Offset: 0x00000813
		internal static ArgumentException InvalidEnumArgument(string parameterName, int invalidValue, Type enumClass)
		{
			return new InvalidEnumArgumentException(parameterName, invalidValue, enumClass);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000261D File Offset: 0x0000081D
		internal static InvalidOperationException InvalidOperation(string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000262B File Offset: 0x0000082B
		internal static InvalidOperationException InvalidOperation(Exception innerException, string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs), innerException);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000263A File Offset: 0x0000083A
		internal static NotSupportedException NotSupported(string messageFormat, params object[] messageArgs)
		{
			return new NotSupportedException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x04000001 RID: 1
		private const string HttpScheme = "http";

		// Token: 0x04000002 RID: 2
		private const string HttpsScheme = "https";
	}
}
