using System;
using System.Collections.Generic;
using System.Data;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200006E RID: 110
	public class OracleNotificationEventArgs : EventArgs
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0003141C File Offset: 0x0002F61C
		public OracleNotificationInfo Info
		{
			get
			{
				if (this.m_bInfoNotPopulated)
				{
					this.m_notificationDetails.ParseNotificationInfo();
					this.m_bInfoNotPopulated = false;
				}
				return this.m_notificationDetails.m_info;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00031444 File Offset: 0x0002F644
		public OracleNotificationSource Source
		{
			get
			{
				if (this.m_bInfoNotPopulated)
				{
					this.m_notificationDetails.ParseNotificationInfo();
					this.m_bInfoNotPopulated = false;
				}
				return this.m_notificationDetails.m_source;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0003146C File Offset: 0x0002F66C
		public OracleNotificationType Type
		{
			get
			{
				if (this.m_bInfoNotPopulated)
				{
					this.m_notificationDetails.ParseNotificationInfo();
					this.m_bInfoNotPopulated = false;
				}
				return this.m_notificationDetails.m_type;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00031494 File Offset: 0x0002F694
		public string[] ResourceNames
		{
			get
			{
				if (this.m_bInfoNotPopulated)
				{
					this.m_notificationDetails.ParseNotificationInfo();
					this.m_bInfoNotPopulated = false;
				}
				return this.m_notificationDetails.m_resources;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x000314BC File Offset: 0x0002F6BC
		public DataTable Details
		{
			get
			{
				if (this.m_bInfoNotPopulated)
				{
					this.m_notificationDetails.ParseNotificationInfo();
					this.m_bInfoNotPopulated = false;
				}
				return this.m_notificationDetails.m_details;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000314E4 File Offset: 0x0002F6E4
		internal OracleNotificationEventArgs Clone()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleNotificationEventArgs result;
			try
			{
				OracleNotificationEventArgs oracleNotificationEventArgs = (OracleNotificationEventArgs)base.MemberwiseClone();
				result = oracleNotificationEventArgs;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00031560 File Offset: 0x0002F760
		internal OracleNotificationEventArgs(NotificationDetails notifDetails)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_notificationDetails = notifDetails;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x000315C4 File Offset: 0x0002F7C4
		internal List<long> QueryIdList
		{
			get
			{
				if (this.m_bInfoNotPopulated)
				{
					this.m_notificationDetails.ParseNotificationInfo();
					this.m_bInfoNotPopulated = false;
				}
				return this.m_notificationDetails.m_queryIdList;
			}
		}

		// Token: 0x04000662 RID: 1634
		internal NotificationDetails m_notificationDetails;

		// Token: 0x04000663 RID: 1635
		internal bool m_bInfoNotPopulated = true;
	}
}
