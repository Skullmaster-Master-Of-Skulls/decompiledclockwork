using System;

namespace System.Data.SqlClient
{
	// Token: 0x020002EF RID: 751
	internal class SqlNotification : MarshalByRefObject
	{
		// Token: 0x060026F1 RID: 9969 RVA: 0x002A80E8 File Offset: 0x002A74E8
		internal SqlNotification(SqlNotificationInfo info, SqlNotificationSource source, SqlNotificationType type, string key)
		{
			this._info = info;
			this._source = source;
			this._type = type;
			this._key = key;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060026F2 RID: 9970 RVA: 0x002A8118 File Offset: 0x002A7518
		internal SqlNotificationInfo Info
		{
			get
			{
				return this._info;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x002A8138 File Offset: 0x002A7538
		internal string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060026F4 RID: 9972 RVA: 0x002A8158 File Offset: 0x002A7558
		internal SqlNotificationSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x002A8178 File Offset: 0x002A7578
		internal SqlNotificationType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x040018A7 RID: 6311
		private readonly SqlNotificationInfo _info;

		// Token: 0x040018A8 RID: 6312
		private readonly SqlNotificationSource _source;

		// Token: 0x040018A9 RID: 6313
		private readonly SqlNotificationType _type;

		// Token: 0x040018AA RID: 6314
		private readonly string _key;
	}
}
