using System;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x0200010B RID: 267
	public class OnlyOnceErrorHandler : IErrorHandler
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x00017C56 File Offset: 0x00015E56
		public OnlyOnceErrorHandler()
		{
			this.m_prefix = "";
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00017C70 File Offset: 0x00015E70
		public OnlyOnceErrorHandler(string prefix)
		{
			this.m_prefix = prefix;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00017C86 File Offset: 0x00015E86
		public void Reset()
		{
			this.m_enabledDateUtc = DateTime.MinValue;
			this.m_errorCode = ErrorCode.GenericFailure;
			this.m_exception = null;
			this.m_message = null;
			this.m_firstTime = true;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00017CAF File Offset: 0x00015EAF
		public void Error(string message, Exception e, ErrorCode errorCode)
		{
			if (this.m_firstTime)
			{
				this.FirstError(message, e, errorCode);
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00017CC4 File Offset: 0x00015EC4
		public virtual void FirstError(string message, Exception e, ErrorCode errorCode)
		{
			this.m_enabledDateUtc = DateTime.UtcNow;
			this.m_errorCode = errorCode;
			this.m_exception = e;
			this.m_message = message;
			this.m_firstTime = false;
			if (LogLog.InternalDebugging && !LogLog.QuietMode)
			{
				LogLog.Error(OnlyOnceErrorHandler.declaringType, string.Concat(new string[]
				{
					"[",
					this.m_prefix,
					"] ErrorCode: ",
					errorCode.ToString(),
					". ",
					message
				}), e);
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00017D51 File Offset: 0x00015F51
		public void Error(string message, Exception e)
		{
			this.Error(message, e, ErrorCode.GenericFailure);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00017D5C File Offset: 0x00015F5C
		public void Error(string message)
		{
			this.Error(message, null, ErrorCode.GenericFailure);
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00017D67 File Offset: 0x00015F67
		public bool IsEnabled
		{
			get
			{
				return this.m_firstTime;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00017D6F File Offset: 0x00015F6F
		public DateTime EnabledDate
		{
			get
			{
				if (this.m_enabledDateUtc == DateTime.MinValue)
				{
					return DateTime.MinValue;
				}
				return this.m_enabledDateUtc.ToLocalTime();
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00017D94 File Offset: 0x00015F94
		public DateTime EnabledDateUtc
		{
			get
			{
				return this.m_enabledDateUtc;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00017D9C File Offset: 0x00015F9C
		public string ErrorMessage
		{
			get
			{
				return this.m_message;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00017DA4 File Offset: 0x00015FA4
		public Exception Exception
		{
			get
			{
				return this.m_exception;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00017DAC File Offset: 0x00015FAC
		public ErrorCode ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
		}

		// Token: 0x040002D6 RID: 726
		private DateTime m_enabledDateUtc;

		// Token: 0x040002D7 RID: 727
		private bool m_firstTime = true;

		// Token: 0x040002D8 RID: 728
		private string m_message;

		// Token: 0x040002D9 RID: 729
		private Exception m_exception;

		// Token: 0x040002DA RID: 730
		private ErrorCode m_errorCode;

		// Token: 0x040002DB RID: 731
		private readonly string m_prefix;

		// Token: 0x040002DC RID: 732
		private static readonly Type declaringType = typeof(OnlyOnceErrorHandler);
	}
}
