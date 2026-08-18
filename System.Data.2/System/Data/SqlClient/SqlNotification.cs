using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001CA RID: 458
	internal class SqlNotification : MarshalByRefObject
	{
		// Token: 0x06001CEA RID: 7402 RVA: 0x000CC734 File Offset: 0x000CBB34
		internal SqlNotification(SqlNotificationInfo info, SqlNotificationSource source, SqlNotificationType type, string key)
		{
			this._info = info;
			this._source = source;
			this._type = type;
			this._key = key;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x000CC764 File Offset: 0x000CBB64
		internal SqlNotificationInfo Info
		{
			get
			{
				return this._info;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x000CC778 File Offset: 0x000CBB78
		internal string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x000CC78C File Offset: 0x000CBB8C
		internal SqlNotificationSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x000CC7A0 File Offset: 0x000CBBA0
		internal SqlNotificationType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04001068 RID: 4200
		private readonly SqlNotificationInfo _info;

		// Token: 0x04001069 RID: 4201
		private readonly SqlNotificationSource _source;

		// Token: 0x0400106A RID: 4202
		private readonly SqlNotificationType _type;

		// Token: 0x0400106B RID: 4203
		private readonly string _key;
	}
}
