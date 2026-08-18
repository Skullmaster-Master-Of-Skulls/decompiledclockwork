using System;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000282 RID: 642
	internal class Logger
	{
		// Token: 0x06001923 RID: 6435 RVA: 0x00108260 File Offset: 0x00106460
		public static Logger GetLogger(string className)
		{
			if (Logger.s_vInstance == null)
			{
				Logger.s_vInstance = new Logger();
			}
			return Logger.s_vInstance;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00108278 File Offset: 0x00106478
		public void Log(LoggerLevel loggerLevel, string stackTrace, Exception e)
		{
		}

		// Token: 0x04001B78 RID: 7032
		private static Logger s_vInstance;
	}
}
