using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Http.Properties;

namespace System.Web.Http
{
	// Token: 0x02000005 RID: 5
	internal static class Error
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000027C6 File Offset: 0x000009C6
		internal static string Format(string format, params object[] args)
		{
			return string.Format(CultureInfo.CurrentCulture, format, args);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027D4 File Offset: 0x000009D4
		internal static ArgumentException Argument(string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027E2 File Offset: 0x000009E2
		internal static ArgumentException Argument(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentException(Error.Format(messageFormat, messageArgs), parameterName);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027F4 File Offset: 0x000009F4
		internal static ArgumentException ArgumentUriNotHttpOrHttpsScheme(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidHttpUriScheme, new object[]
			{
				actualValue,
				"http",
				"https"
			}), parameterName);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002830 File Offset: 0x00000A30
		internal static ArgumentException ArgumentUriNotAbsolute(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentInvalidAbsoluteUri, new object[]
			{
				actualValue
			}), parameterName);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000285C File Offset: 0x00000A5C
		internal static ArgumentException ArgumentUriHasQueryOrFragment(string parameterName, Uri actualValue)
		{
			return new ArgumentException(Error.Format(CommonWebApiResources.ArgumentUriHasQueryOrFragment, new object[]
			{
				actualValue
			}), parameterName);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002885 File Offset: 0x00000A85
		internal static ArgumentNullException PropertyNull()
		{
			return new ArgumentNullException("value");
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002891 File Offset: 0x00000A91
		internal static ArgumentNullException ArgumentNull(string parameterName)
		{
			return new ArgumentNullException(parameterName);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002899 File Offset: 0x00000A99
		internal static ArgumentNullException ArgumentNull(string parameterName, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentNullException(parameterName, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000028A8 File Offset: 0x00000AA8
		internal static ArgumentException ArgumentNullOrEmpty(string parameterName)
		{
			return Error.Argument(parameterName, CommonWebApiResources.ArgumentNullOrEmpty, new object[]
			{
				parameterName
			});
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028CC File Offset: 0x00000ACC
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName, object actualValue, string messageFormat, params object[] messageArgs)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028DC File Offset: 0x00000ADC
		internal static ArgumentOutOfRangeException ArgumentMustBeGreaterThanOrEqualTo(string parameterName, object actualValue, object minValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeGreaterThanOrEqualTo, new object[]
			{
				minValue
			}));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002908 File Offset: 0x00000B08
		internal static ArgumentOutOfRangeException ArgumentMustBeLessThanOrEqualTo(string parameterName, object actualValue, object maxValue)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, Error.Format(CommonWebApiResources.ArgumentMustBeLessThanOrEqualTo, new object[]
			{
				maxValue
			}));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002932 File Offset: 0x00000B32
		internal static KeyNotFoundException KeyNotFound()
		{
			return new KeyNotFoundException();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002939 File Offset: 0x00000B39
		internal static KeyNotFoundException KeyNotFound(string messageFormat, params object[] messageArgs)
		{
			return new KeyNotFoundException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002947 File Offset: 0x00000B47
		internal static ObjectDisposedException ObjectDisposed(string messageFormat, params object[] messageArgs)
		{
			return new ObjectDisposedException(null, Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002956 File Offset: 0x00000B56
		internal static OperationCanceledException OperationCanceled()
		{
			return new OperationCanceledException();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000295D File Offset: 0x00000B5D
		internal static OperationCanceledException OperationCanceled(string messageFormat, params object[] messageArgs)
		{
			return new OperationCanceledException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000296B File Offset: 0x00000B6B
		internal static ArgumentException InvalidEnumArgument(string parameterName, int invalidValue, Type enumClass)
		{
			return new InvalidEnumArgumentException(parameterName, invalidValue, enumClass);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002975 File Offset: 0x00000B75
		internal static InvalidOperationException InvalidOperation(string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002983 File Offset: 0x00000B83
		internal static InvalidOperationException InvalidOperation(Exception innerException, string messageFormat, params object[] messageArgs)
		{
			return new InvalidOperationException(Error.Format(messageFormat, messageArgs), innerException);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002992 File Offset: 0x00000B92
		internal static NotSupportedException NotSupported(string messageFormat, params object[] messageArgs)
		{
			return new NotSupportedException(Error.Format(messageFormat, messageArgs));
		}

		// Token: 0x04000002 RID: 2
		private const string HttpScheme = "http";

		// Token: 0x04000003 RID: 3
		private const string HttpsScheme = "https";
	}
}
