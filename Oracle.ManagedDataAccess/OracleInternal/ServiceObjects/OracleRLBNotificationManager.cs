using System;
using System.Collections.Generic;
using OracleInternal.Common;
using OracleInternal.ConnectionPool;
using OracleInternal.I18N;
using OracleInternal.NotificationServices;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001B9 RID: 441
	internal class OracleRLBNotificationManager : OracleONSNotificationManager
	{
		// Token: 0x06001123 RID: 4387 RVA: 0x000BD384 File Offset: 0x000BB584
		protected override string GetEventTypeForNotification(string serviceName)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			string result;
			try
			{
				result = "(\"eventType=database/event/servicemetrics/" + serviceName + "\")";
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000BD3E8 File Offset: 0x000BB5E8
		protected override void HandleEvent(object notification)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				if (notification != null)
				{
					try
					{
						byte[] array = Array.ConvertAll<sbyte, byte>(((Notification)notification).body(), (sbyte a) => (byte)a);
						string text = Conv.GetInstance(871).ConvertBytesToString(array, 0, array.Length, null, true);
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.RLB, new string[]
							{
								text
							});
						}
						RLB rlb = RLBManager.Put(text);
						if (rlb != null)
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.RLB, new string[]
								{
									"RLB data available. Lets try and use it to update the service member instance DOWN names"
								});
							}
							List<OraclePoolManager> list = OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.m_listPM.GetList();
							for (int i = 0; i < list.Count; i++)
							{
								OraclePoolManager oraclePoolManager = list[i];
								ServiceCtx serviceCtx = null;
								if (rlb.m_service != null)
								{
									serviceCtx = oraclePoolManager.m_dictSvcCtx[rlb.m_service.ToLowerInvariant()];
								}
								if (serviceCtx != null)
								{
									serviceCtx.CheckAndUpdateServiceMemberDOWNNames_RLB(rlb);
								}
								else if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.RLB, new string[]
									{
										string.Format("ServiceCtx does not exist for service {0} in PM {1}", (rlb.m_service != null) ? rlb.m_service : "null", oraclePoolManager.m_id)
									});
								}
							}
						}
					}
					catch (Exception ex)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268697600, new string[]
							{
								"OracleRLBNotificationManager::HandleEvent() failed. -" + ex.Message
							});
						}
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x0400135F RID: 4959
		private string m_serviceName = string.Empty;
	}
}
