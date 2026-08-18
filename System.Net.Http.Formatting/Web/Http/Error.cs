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
		// Token: 0x0600000E RID: 14 RVA: 0x00002407 File Offset: 0x00000607
		internal static string Format(string format, params object[] args)
		{
			return string.Format(CultureInfo.CurrentCulture, format, args);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002415 File Offset: 0x00000615
		internal static ArgumentException Argument(string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002423 File Offset: 0x00000623
		internal static ArgumentException Argument(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs), parameterName);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002434 File Offset: 0x00000634
		internal static ArgumentException ArgumentUriNotHttpOrHttpsScheme(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidHttpUriScheme, new object[]
			{
				actualValue,
				"http",
				"https"
			}), parameterName);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002470 File Offset: 0x00000670
		internal static ArgumentException ArgumentUriNotAbsolute(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidAbsoluteUri, new object[]
			{
				actualValue
			}), parameterName);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000249C File Offset: 0x0000069C
		internal static ArgumentException ArgumentUriHasQueryOrFragment(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentUriHasQueryOrFragment, new object[]
			{
				actualValue
			}), parameterName);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024C5 File Offset: 0x000006C5
		internal static ArgumentNullException PropertyNull()
		{
			return new ArgumentNullException("value");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024D1 File Offset: 0x000006D1
		internal static ArgumentNullException ArgumentNull(string parameterName)
		{
			return new ArgumentNullException(parameterName);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024D9 File Offset: 0x000006D9
		internal static ArgumentNullException ArgumentNull(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentNullException(parameterName, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024E8 File Offset: 0x000006E8
		internal static ArgumentException ArgumentNullOrEmpty(string parameterName)
		{
			return Error.Argument(parameterName, CommonWebApiResources.ArgumentNullOrEmpty, new object[]
			{
				parameterName
			});
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000250C File Offset: 0x0000070C
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName, object actualValue, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000251C File Offset: 0x0000071C
		internal static ArgumentOutOfRangeException ArgumentMustBeGreaterThanOrEqualTo(string parameterName, object actualValue, object minValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeGreaterThanOrEqualTo, new object[]
			{
				minValue
			}));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002548 File Offset: 0x00000748
		internal static ArgumentOutOfRangeException ArgumentMustBeLessThanOrEqualTo(string parameterName, object actualValue, object maxValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeLessThanOrEqualTo, new object[]
			{
				maxValue
			}));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002572 File Offset: 0x00000772
		internal static KeyNotFoundException KeyNotFound()
		{
			return new KeyNotFoundException();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002579 File Offset: 0x00000779
		internal static KeyNotFoundException KeyNotFound(string messageFormat, params object[] messageArgs)
		{
			return new KeyNotFoundException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002587 File Offset: 0x00000787
		internal static ObjectDisposedException ObjectDisposed(string messageFormat, params object[] messageArgs)
		{
			return new ObjectDisposedException(null, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002596 File Offset: 0x00000796
		internal static OperationCanceledException OperationCanceled()
		{
			return new OperationCanceledException();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000259D File Offset: 0x0000079D
		internal static OperationCanceledException OperationCanceled(string messageFormat, params object[] messageArgs)
		{
			return new OperationCanceledException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025AB File Offset: 0x000007AB
		internal static ArgumentException InvalidEnumArgument(string parameterName, int invalidValue, Type enumClass)
		{
			return new InvalidEnumArgumentException(parameterName, invalidValue, enumClass);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025B5 File Offset: 0x000007B5
		internal static InvalidOperationException InvalidOperation(string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025C3 File Offset: 0x000007C3
		internal static InvalidOperationException InvalidOperation(Exception innerException, string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs), innerException);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025D2 File Offset: 0x000007D2
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
