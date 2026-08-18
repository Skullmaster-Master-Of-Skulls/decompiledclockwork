using System;
using System.Data;
using NLog;

namespace ClockWorkLogger
{
	// Token: 0x02000003 RID: 3
	public class cwlog
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002298 File Offset: 0x00000498
		public static Logger Logger
		{
			get
			{
				bool flag = cwlog.logger == null;
				if (flag)
				{
					cwlog.logger = LogManager.GetCurrentClassLogger();
				}
				return cwlog.logger;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000022C8 File Offset: 0x000004C8
		public static Logger GetLoggerByName(string logName)
		{
			bool flag = cwlog.logger == null || !cwlog.logger.Name.ToLower().Equals(logName.ToLower());
			if (flag)
			{
				cwlog.logger = LogManager.GetLogger(logName);
			}
			return cwlog.logger;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002318 File Offset: 0x00000518
		public static string ToString(DataView dv)
		{
			return dv.ToLoggerFormat();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002330 File Offset: 0x00000530
		public static string ToString(DataTable t)
		{
			return t.DefaultView.ToLoggerFormat();
		}

		// Token: 0x04000001 RID: 1
		private static Logger logger;
	}
}
