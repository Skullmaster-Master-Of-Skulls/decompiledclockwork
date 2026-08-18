using System;
using System.IO;

namespace ClockWorkAPI
{
	// Token: 0x0200000F RID: 15
	public class Log
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002D99 File Offset: 0x00001D99
		public Log(LogType logType, string connection)
		{
			this.logType = logType;
			this.connection = connection;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002DB4 File Offset: 0x00001DB4
		public void AddLogEntry(LogGroup logGroup, LogTitle logTitle, string description, int pid, string ip, bool success)
		{
			LogType logType = this.logType;
			if (logType == LogType.File)
			{
				this.AddFileLogEntry(logGroup, logTitle, description, pid, ip, success);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002DE4 File Offset: 0x00001DE4
		private void AddFileLogEntry(LogGroup logGroup, LogTitle logTitle, string description, int pid, string ip, bool success)
		{
			string name = Enum.GetName(typeof(LogGroup), logGroup);
			string name2 = Enum.GetName(typeof(LogTitle), logTitle);
			TextWriter textWriter = new StreamWriter(this.connection, true);
			textWriter.WriteLine(string.Concat(new string[]
			{
				DateTime.Now.ToString("yyyy-MM-dd H:mm"),
				"  ",
				name,
				": ",
				name2
			}));
			textWriter.WriteLine(string.Concat(new string[]
			{
				success ? "SUCCESS" : "FAIL",
				"  pid=",
				pid.ToString(),
				"  ip=",
				ip
			}));
			textWriter.WriteLine(description);
			textWriter.WriteLine("");
			textWriter.Close();
		}

		// Token: 0x04000025 RID: 37
		private LogType logType;

		// Token: 0x04000026 RID: 38
		private string connection;
	}
}
