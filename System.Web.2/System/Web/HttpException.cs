using System;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Web
{
	// Token: 0x02000098 RID: 152
	[Serializable]
	public class HttpException : ExternalException
	{
		// Token: 0x060009CD RID: 2509 RVA: 0x00016A58 File Offset: 0x00014C58
		internal static int HResultFromLastError(int lastError)
		{
			int result;
			if (lastError < 0)
			{
				result = lastError;
			}
			else
			{
				result = ((lastError & 65535) | 458752 | int.MinValue);
			}
			return result;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00016A82 File Offset: 0x00014C82
		public static HttpException CreateFromLastError(string message)
		{
			return new HttpException(message, HttpException.HResultFromLastError(Marshal.GetLastWin32Error()));
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00016A94 File Offset: 0x00014C94
		public HttpException()
		{
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00016A9C File Offset: 0x00014C9C
		public HttpException(string message) : base(message)
		{
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00016AA5 File Offset: 0x00014CA5
		internal HttpException(string message, Exception innerException, int code) : base(message, innerException)
		{
			this._webEventCode = code;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00016AB6 File Offset: 0x00014CB6
		public HttpException(string message, int hr) : base(message)
		{
			base.HResult = hr;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00016AC6 File Offset: 0x00014CC6
		public HttpException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00016AD0 File Offset: 0x00014CD0
		public HttpException(int httpCode, string message, Exception innerException) : base(message, innerException)
		{
			this._httpCode = httpCode;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00016AE1 File Offset: 0x00014CE1
		public HttpException(int httpCode, string message) : base(message)
		{
			this._httpCode = httpCode;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00016AF1 File Offset: 0x00014CF1
		public HttpException(int httpCode, string message, int hr) : base(message)
		{
			base.HResult = hr;
			this._httpCode = httpCode;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00016B08 File Offset: 0x00014D08
		protected HttpException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._httpCode = info.GetInt32("_httpCode");
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00016B23 File Offset: 0x00014D23
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("_httpCode", this._httpCode);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00016B3E File Offset: 0x00014D3E
		public int GetHttpCode()
		{
			return HttpException.GetHttpCodeForException(this);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00016B46 File Offset: 0x00014D46
		internal void SetFormatter(ErrorFormatter errorFormatter)
		{
			this._errorFormatter = errorFormatter;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00016B50 File Offset: 0x00014D50
		internal static int GetHttpCodeForException(Exception e)
		{
			if (e is HttpException)
			{
				HttpException ex = (HttpException)e;
				if (ex._httpCode > 0)
				{
					return ex._httpCode;
				}
			}
			else
			{
				if (e is UnauthorizedAccessException)
				{
					return 401;
				}
				if (e is PathTooLongException)
				{
					return 414;
				}
			}
			if (e.InnerException != null)
			{
				return HttpException.GetHttpCodeForException(e.InnerException);
			}
			return 500;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00016BB4 File Offset: 0x00014DB4
		internal static ErrorFormatter GetErrorFormatter(Exception e)
		{
			Exception innerException = e.InnerException;
			ErrorFormatter errorFormatter = null;
			if (innerException != null)
			{
				errorFormatter = HttpException.GetErrorFormatter(innerException);
				if (errorFormatter != null)
				{
					return errorFormatter;
				}
				if (innerException is ConfigurationException)
				{
					ConfigurationException ex = innerException as ConfigurationException;
					if (ex != null && ex.Filename != null)
					{
						errorFormatter = new ConfigErrorFormatter((ConfigurationException)innerException);
					}
				}
				else if (innerException is SecurityException)
				{
					errorFormatter = new SecurityErrorFormatter(innerException);
				}
			}
			if (errorFormatter != null)
			{
				return errorFormatter;
			}
			HttpException ex2 = e as HttpException;
			if (ex2 != null)
			{
				return ex2._errorFormatter;
			}
			return null;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00016C28 File Offset: 0x00014E28
		public string GetHtmlErrorMessage()
		{
			ErrorFormatter errorFormatter = HttpException.GetErrorFormatter(this);
			if (errorFormatter == null)
			{
				return null;
			}
			return errorFormatter.GetHtmlErrorMessage();
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00016C47 File Offset: 0x00014E47
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00016C4F File Offset: 0x00014E4F
		public int WebEventCode
		{
			get
			{
				return this._webEventCode;
			}
			internal set
			{
				this._webEventCode = value;
			}
		}

		// Token: 0x040003A3 RID: 931
		private const int FACILITY_WIN32 = 7;

		// Token: 0x040003A4 RID: 932
		private int _httpCode;

		// Token: 0x040003A5 RID: 933
		private ErrorFormatter _errorFormatter;

		// Token: 0x040003A6 RID: 934
		private int _webEventCode;
	}
}
