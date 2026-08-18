using System;
using System.Configuration.Internal;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200005B RID: 91
	internal static class ExceptionUtil
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0001366D File Offset: 0x0001186D
		internal static ArgumentException ParameterInvalid(string parameter)
		{
			return new ArgumentException(SR.GetString("Parameter_Invalid", new object[]
			{
				parameter
			}), parameter);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00013689 File Offset: 0x00011889
		internal static ArgumentException ParameterNullOrEmpty(string parameter)
		{
			return new ArgumentException(SR.GetString("Parameter_NullOrEmpty", new object[]
			{
				parameter
			}), parameter);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000136A5 File Offset: 0x000118A5
		internal static ArgumentException PropertyInvalid(string property)
		{
			return new ArgumentException(SR.GetString("Property_Invalid", new object[]
			{
				property
			}), property);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000136C1 File Offset: 0x000118C1
		internal static ArgumentException PropertyNullOrEmpty(string property)
		{
			return new ArgumentException(SR.GetString("Property_NullOrEmpty", new object[]
			{
				property
			}), property);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000136DD File Offset: 0x000118DD
		internal static InvalidOperationException UnexpectedError(string methodName)
		{
			return new InvalidOperationException(SR.GetString("Unexpected_Error", new object[]
			{
				methodName
			}));
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600038E RID: 910 RVA: 0x000136F8 File Offset: 0x000118F8
		internal static string NoExceptionInformation
		{
			get
			{
				return SR.GetString("No_exception_information_available");
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00013704 File Offset: 0x00011904
		internal static ConfigurationErrorsException WrapAsConfigException(string outerMessage, Exception e, IConfigErrorInfo errorInfo)
		{
			if (errorInfo != null)
			{
				return ExceptionUtil.WrapAsConfigException(outerMessage, e, errorInfo.Filename, errorInfo.LineNumber);
			}
			return ExceptionUtil.WrapAsConfigException(outerMessage, e, null, 0);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00013728 File Offset: 0x00011928
		internal static ConfigurationErrorsException WrapAsConfigException(string outerMessage, Exception e, string filename, int line)
		{
			ConfigurationErrorsException ex = e as ConfigurationErrorsException;
			if (ex != null)
			{
				if (filename != null && ex.Filename == null)
				{
					ex.SetFileAndLine(filename, line);
				}
				return ex;
			}
			ConfigurationException ex2 = e as ConfigurationException;
			if (ex2 != null)
			{
				return new ConfigurationErrorsException(ex2);
			}
			XmlException ex3 = e as XmlException;
			if (ex3 != null)
			{
				if (ex3.LineNumber != 0)
				{
					line = ex3.LineNumber;
				}
				return new ConfigurationErrorsException(ex3.Message, ex3, filename, line);
			}
			if (e != null)
			{
				return new ConfigurationErrorsException(SR.GetString("Wrapped_exception_message", new object[]
				{
					outerMessage,
					e.Message
				}), e, filename, line);
			}
			return new ConfigurationErrorsException(SR.GetString("Wrapped_exception_message", new object[]
			{
				outerMessage,
				ExceptionUtil.NoExceptionInformation
			}), filename, line);
		}
	}
}
