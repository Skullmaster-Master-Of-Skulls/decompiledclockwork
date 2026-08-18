using System;

namespace Google.Apis.Logging
{
	// Token: 0x0200001D RID: 29
	public class NullLogger : ILogger
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000036C8 File Offset: 0x000018C8
		public bool IsDebugEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000036CB File Offset: 0x000018CB
		public ILogger ForType(Type type)
		{
			return new NullLogger();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000036CB File Offset: 0x000018CB
		public ILogger ForType<T>()
		{
			return new NullLogger();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000036D2 File Offset: 0x000018D2
		public void Info(string message, params object[] formatArgs)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000036D2 File Offset: 0x000018D2
		public void Warning(string message, params object[] formatArgs)
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000036D2 File Offset: 0x000018D2
		public void Debug(string message, params object[] formatArgs)
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000036D2 File Offset: 0x000018D2
		public void Error(Exception exception, string message, params object[] formatArgs)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000036D2 File Offset: 0x000018D2
		public void Error(string message, params object[] formatArgs)
		{
		}
	}
}
