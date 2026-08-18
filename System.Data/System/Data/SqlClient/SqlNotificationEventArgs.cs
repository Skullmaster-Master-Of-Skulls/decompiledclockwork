using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000301 RID: 769
	public class SqlNotificationEventArgs : EventArgs
	{
		// Token: 0x06002807 RID: 10247 RVA: 0x002AE528 File Offset: 0x002AD928
		public SqlNotificationEventArgs(SqlNotificationType type, SqlNotificationInfo info, SqlNotificationSource source)
		{
			this._info = info;
			this._source = source;
			this._type = type;
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x002AE558 File Offset: 0x002AD958
		public SqlNotificationType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x002AE578 File Offset: 0x002AD978
		public SqlNotificationInfo Info
		{
			get
			{
				return this._info;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x002AE598 File Offset: 0x002AD998
		public SqlNotificationSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x04001938 RID: 6456
		private SqlNotificationType _type;

		// Token: 0x04001939 RID: 6457
		private SqlNotificationInfo _info;

		// Token: 0x0400193A RID: 6458
		private SqlNotificationSource _source;

		// Token: 0x0400193B RID: 6459
		internal static SqlNotificationEventArgs NotifyError = new SqlNotificationEventArgs(SqlNotificationType.Subscribe, SqlNotificationInfo.Error, SqlNotificationSource.Object);
	}
}
