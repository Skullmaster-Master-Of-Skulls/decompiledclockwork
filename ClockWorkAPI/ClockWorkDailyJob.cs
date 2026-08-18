using System;
using System.Data;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x020000AA RID: 170
	public class ClockWorkDailyJob
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x00032AF8 File Offset: 0x00031AF8
		public static int CreateDailyJob(UnivDataAdapter da, int searchInfoId)
		{
			da.SelectCommand.CommandText = "DECLARE @ordernum int; SET @ordernum = (SELECT coalesce(MAX(ordernum),0) FROM windowstaskjob); INSERT INTO windowstaskjob (searchinfoid,ordernum) SELECT @sid,@ordernum WHERE NOT EXISTS(SELECT windowstaskjobid FROM windowstaskjob WHERE searchinfoid=@sid)";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@sid", searchInfoId);
			return da.FillReturnIdentity(new DataTable(), "windowstaskjobid", "windowstaskjob");
		}
	}
}
