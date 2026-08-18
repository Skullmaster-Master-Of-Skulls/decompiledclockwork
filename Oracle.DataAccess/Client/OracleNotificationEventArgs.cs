using System;
using System.Data;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200003C RID: 60
	public class OracleNotificationEventArgs : EventArgs
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0001D743 File Offset: 0x0001C743
		static OracleNotificationEventArgs()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0001D751 File Offset: 0x0001C751
		public OracleNotificationInfo Info
		{
			get
			{
				return this.m_info;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0001D759 File Offset: 0x0001C759
		public OracleNotificationSource Source
		{
			get
			{
				return this.m_source;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0001D761 File Offset: 0x0001C761
		public OracleNotificationType Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0001D769 File Offset: 0x0001C769
		public string[] ResourceNames
		{
			get
			{
				return this.m_resources;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0001D771 File Offset: 0x0001C771
		public DataTable Details
		{
			get
			{
				return this.m_details;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0001D77C File Offset: 0x0001C77C
		internal OracleNotificationEventArgs()
		{
			this.m_type = OracleNotificationType.Change;
			this.m_source = OracleNotificationSource.Data;
			this.m_info = OracleNotificationInfo.Update;
			this.m_resources = new string[0];
			this.m_details = new DataTable();
			this.m_details.Columns.Add("ResourceName", typeof(string));
			this.m_details.Columns.Add("Info", typeof(OracleNotificationInfo));
			this.m_details.Columns.Add("Rowid", typeof(string));
			this.m_details.Columns.Add("QueryId", typeof(long));
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0001D83C File Offset: 0x0001C83C
		internal void AddRowDetail(string name, OracleNotificationInfo info, string rowid, long queryid)
		{
			DataRow dataRow = this.m_details.NewRow();
			dataRow[0] = name;
			dataRow[1] = info;
			dataRow[2] = rowid;
			dataRow[3] = queryid;
			this.m_details.Rows.Add(dataRow);
		}

		// Token: 0x040001FD RID: 509
		internal OracleNotificationType m_type;

		// Token: 0x040001FE RID: 510
		internal OracleNotificationSource m_source;

		// Token: 0x040001FF RID: 511
		internal OracleNotificationInfo m_info;

		// Token: 0x04000200 RID: 512
		internal string[] m_resources;

		// Token: 0x04000201 RID: 513
		internal DataTable m_details;
	}
}
