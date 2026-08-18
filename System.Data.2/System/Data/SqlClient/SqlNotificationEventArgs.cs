using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001E6 RID: 486
	public class SqlNotificationEventArgs : EventArgs
	{
		// Token: 0x06001E51 RID: 7761 RVA: 0x000D4A58 File Offset: 0x000D3E58
		public SqlNotificationEventArgs(SqlNotificationType type, SqlNotificationInfo info, SqlNotificationSource source)
		{
			this._info = info;
			this._source = source;
			this._type = type;
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x000D4A80 File Offset: 0x000D3E80
		public SqlNotificationType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x000D4A94 File Offset: 0x000D3E94
		public SqlNotificationInfo Info
		{
			get
			{
				return this._info;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x000D4AA8 File Offset: 0x000D3EA8
		public SqlNotificationSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x0400114A RID: 4426
		private SqlNotificationType _type;

		// Token: 0x0400114B RID: 4427
		private SqlNotificationInfo _info;

		// Token: 0x0400114C RID: 4428
		private SqlNotificationSource _source;

		// Token: 0x0400114D RID: 4429
		internal static SqlNotificationEventArgs NotifyError = new SqlNotificationEventArgs(SqlNotificationType.Subscribe, SqlNotificationInfo.Error, SqlNotificationSource.Object);
	}
}
