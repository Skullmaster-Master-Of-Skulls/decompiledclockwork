using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Http.Properties;

namespace System.Web.Http
{
	// Token: 0x02000004 RID: 4
	internal static class Error
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000027A8 File Offset: 0x000009A8
		internal static string Format(string format, params object[] args)
		{
			return string.Format(CultureInfo.CurrentCulture, format, args);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000027B6 File Offset: 0x000009B6
		internal static ArgumentException Argument(string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000027C4 File Offset: 0x000009C4
		internal static ArgumentException Argument(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs), parameterName);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027D4 File Offset: 0x000009D4
		internal static ArgumentException ArgumentUriNotHttpOrHttpsScheme(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidHttpUriScheme, new object[]
			{
				actualValue,
				"http",
				"https"
			}), parameterName);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002810 File Offset: 0x00000A10
		internal static ArgumentException ArgumentUriNotAbsolute(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidAbsoluteUri, new object[]
			{
				actualValue
			}), parameterName);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000283C File Offset: 0x00000A3C
		internal static ArgumentException ArgumentUriHasQueryOrFragment(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentUriHasQueryOrFragment, new object[]
			{
				actualValue
			}), parameterName);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002865 File Offset: 0x00000A65
		internal static ArgumentNullException PropertyNull()
		{
			return new ArgumentNullException("value");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002871 File Offset: 0x00000A71
		internal static ArgumentNullException ArgumentNull(string parameterName)
		{
			return new ArgumentNullException(parameterName);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002879 File Offset: 0x00000A79
		internal static ArgumentNullException ArgumentNull(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentNullException(parameterName, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002888 File Offset: 0x00000A88
		internal static ArgumentException ArgumentNullOrEmpty(string parameterName)
		{
			return Error.Argument(parameterName, CommonWebApiResources.ArgumentNullOrEmpty, new object[]
			{
				parameterName
			});
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000028AC File Offset: 0x00000AAC
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName, object actualValue, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000028BC File Offset: 0x00000ABC
		internal static ArgumentOutOfRangeException ArgumentMustBeGreaterThanOrEqualTo(string parameterName, object actualValue, object minValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeGreaterThanOrEqualTo, new object[]
			{
				minValue
			}));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028E8 File Offset: 0x00000AE8
		internal static ArgumentOutOfRangeException ArgumentMustBeLessThanOrEqualTo(string parameterName, object actualValue, object maxValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeLessThanOrEqualTo, new object[]
			{
				maxValue
			}));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002912 File Offset: 0x00000B12
		internal static KeyNotFoundException KeyNotFound()
		{
			return new KeyNotFoundException();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002919 File Offset: 0x00000B19
		internal static KeyNotFoundException KeyNotFound(string messageFormat, params object[] messageArgs)
		{
			return new KeyNotFoundException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002927 File Offset: 0x00000B27
		internal static ObjectDisposedException ObjectDisposed(string messageFormat, params object[] messageArgs)
		{
			return new ObjectDisposedException(null, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002936 File Offset: 0x00000B36
		internal static OperationCanceledException OperationCanceled()
		{
			return new OperationCanceledException();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000293D File Offset: 0x00000B3D
		internal static OperationCanceledException OperationCanceled(string messageFormat, params object[] messageArgs)
		{
			return new OperationCanceledException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000294B File Offset: 0x00000B4B
		internal static ArgumentException InvalidEnumArgument(string parameterName, int invalidValue, Type enumClass)
		{
			return new InvalidEnumArgumentException(parameterName, invalidValue, enumClass);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002955 File Offset: 0x00000B55
		internal static InvalidOperationException InvalidOperation(string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002963 File Offset: 0x00000B63
		internal static InvalidOperationException InvalidOperation(Exception innerException, string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs), innerException);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002972 File Offset: 0x00000B72
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
