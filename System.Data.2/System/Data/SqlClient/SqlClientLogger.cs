using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001E1 RID: 481
	public class SqlClientLogger
	{
		// Token: 0x06001E20 RID: 7712 RVA: 0x000D3B30 File Offset: 0x000D2F30
		public void LogInfo(string type, string method, string message)
		{
			Bid.Trace(string.Format("<sc|{0}|{1}|{2}>{3}\n", new object[]
			{
				type,
				method,
				SqlClientLogger.LogLevel.Info,
				message
			}));
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000D3B68 File Offset: 0x000D2F68
		public void LogError(string type, string method, string message)
		{
			Bid.Trace(string.Format("<sc|{0}|{1}|{2}>{3}\n", new object[]
			{
				type,
				method,
				SqlClientLogger.LogLevel.Error,
				message
			}));
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000D3BA0 File Offset: 0x000D2FA0
		public bool LogAssert(bool value, string type, string method, string message)
		{
			if (!value)
			{
				this.LogError(type, method, message);
			}
			return value;
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x000D3BBC File Offset: 0x000D2FBC
		public bool IsLoggingEnabled
		{
			get
			{
				return Bid.TraceOn;
			}
		}

		// Token: 0x020003CE RID: 974
		internal enum LogLevel
		{
			// Token: 0x040020EF RID: 8431
			Info,
			// Token: 0x040020F0 RID: 8432
			Error
		}
	}
}
