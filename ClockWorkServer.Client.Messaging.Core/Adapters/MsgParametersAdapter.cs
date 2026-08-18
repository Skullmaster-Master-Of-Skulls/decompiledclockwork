using System;
using TechnoPro.ClockWorkServer.Contracts;

namespace TechnoPro.ClockWorkServer.Client.Messaging.Core.Adapters
{
	// Token: 0x0200000A RID: 10
	public static class MsgParametersAdapter
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000028E4 File Offset: 0x00000AE4
		private static int GetInt(this MessageParameters Parameters, string name)
		{
			int result;
			if (Parameters.ContainsKey(name) && int.TryParse(Parameters[name], out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000290D File Offset: 0x00000B0D
		private static void SetDateTime(this MessageParameters Parameters, string name, DateTime dt)
		{
			if (Parameters.ContainsKey(name))
			{
				Parameters[name] = dt.ToString("yyyy-MM-dd H:mm");
				return;
			}
			Parameters.Add(name, dt.ToString("yyyy-MM-dd H:mm"));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002940 File Offset: 0x00000B40
		private static DateTime GetDateTime(this MessageParameters Parameters, string name)
		{
			DateTime result;
			if (Parameters.ContainsKey(name) && DateTime.TryParse(Parameters[name], out result))
			{
				return result;
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000296D File Offset: 0x00000B6D
		private static void SetInt(this MessageParameters Parameters, string name, int num)
		{
			if (Parameters.ContainsKey(name))
			{
				Parameters[name] = num.ToString();
				return;
			}
			Parameters.Add(name, num.ToString());
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002995 File Offset: 0x00000B95
		public static int GetPid(this MessageParameters Parameters)
		{
			return Parameters.GetInt("pid");
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029A2 File Offset: 0x00000BA2
		public static void SetPid(this MessageParameters Parameters, int pid)
		{
			Parameters.SetInt("pid", pid);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029B0 File Offset: 0x00000BB0
		public static int GetAppointmentId(this MessageParameters Parameters)
		{
			return Parameters.GetInt("appid");
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029BD File Offset: 0x00000BBD
		public static void SetAppointmentId(this MessageParameters Parameters, int pid)
		{
			Parameters.SetInt("appid", pid);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029CB File Offset: 0x00000BCB
		public static DateTime GetStartDateTime(this MessageParameters Parameters)
		{
			return Parameters.GetDateTime("sdt");
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000029D8 File Offset: 0x00000BD8
		public static void SetStartDateTime(this MessageParameters Parameters, DateTime dt)
		{
			Parameters.SetDateTime("sdt", dt);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029E6 File Offset: 0x00000BE6
		public static DateTime GetEndDateTime(this MessageParameters Parameters)
		{
			return Parameters.GetDateTime("edt");
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029F3 File Offset: 0x00000BF3
		public static void SetEndDateTime(this MessageParameters Parameters, DateTime dt)
		{
			Parameters.SetDateTime("edt", dt);
		}
	}
}
