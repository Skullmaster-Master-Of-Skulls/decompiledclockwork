using System;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.NotificationServices;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001B3 RID: 435
	internal class OracleONSNotificationManager
	{
		// Token: 0x06001083 RID: 4227 RVA: 0x000B44DC File Offset: 0x000B26DC
		internal static OracleONSNotificationManager GetNotificationManager(NotificationType type)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			OracleONSNotificationManager result;
			try
			{
				switch (type)
				{
				case NotificationType.HA:
					result = new OracleHANotificationManager();
					break;
				case NotificationType.RLB:
					result = new OracleRLBNotificationManager();
					break;
				default:
					throw new InvalidOperationException();
				}
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

		// Token: 0x06001084 RID: 4228 RVA: 0x000B4558 File Offset: 0x000B2758
		internal void RegisterForNotification(string serviceName, string databaseName, int timeout, string eventTypeFromDb, string onsConfigFromDb)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_serviceName = serviceName;
				string text = string.Empty;
				string text2 = string.Empty;
				if (ConfigBaseClass.m_ONSMode == ONSConfigMode.remote)
				{
					text = ProviderConfig.GetONSConfiguration(databaseName);
					if (!this.m_bListeningOnDefaultNodes)
					{
						if (!string.IsNullOrWhiteSpace(ConfigBaseClass.m_nodeListFromConfFile))
						{
							text.IndexOf('\n');
							text = text + "," + ConfigBaseClass.m_nodeListFromConfFile;
						}
						this.m_bListeningOnDefaultNodes = true;
					}
				}
				else if (ConfigBaseClass.m_ONSMode == ONSConfigMode.local)
				{
					text = "nodes.list=127.0.0.1:" + ProviderConfig.GetPropertyFromONSConfig(ConfigBaseClass.m_ONSConfigFile, ConfigInfo.ONSRemotePort);
				}
				if (!string.IsNullOrEmpty(onsConfigFromDb))
				{
					string text3 = text;
					string text4 = onsConfigFromDb.Trim();
					char[] trimChars = new char[1];
					text = text4.TrimEnd(trimChars);
					if (!string.IsNullOrEmpty(text3))
					{
						int num = text.IndexOf("nodes.");
						if (num != -1)
						{
							int num2 = text.IndexOf('\n', num);
							if (num2 == -1)
							{
								num2 = text.Length;
							}
							text3 = text3.Split(new char[]
							{
								'='
							})[1];
							text = text.Insert(num2, "," + text3);
						}
						else
						{
							text += text3;
						}
					}
				}
				if (!string.IsNullOrEmpty(eventTypeFromDb))
				{
					text2 = eventTypeFromDb.Trim();
					string text5 = text2;
					char[] trimChars2 = new char[1];
					text2 = text5.TrimEnd(trimChars2);
				}
				else
				{
					text2 = this.GetEventTypeForNotification(this.m_serviceName);
				}
				long num3;
				if (timeout <= 0)
				{
					num3 = 15000L;
				}
				else
				{
					num3 = (long)(timeout * 1000);
					if (num3 > 2147483647L)
					{
						num3 = 2147483647L;
					}
				}
				ONS ons = new ONS(text);
				Subscriber subscriber = ons.createNewSubscriber(text2, "", num3);
				if (subscriber == null)
				{
					throw new SubscriptionException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ONS_SUBSCR_FAILED, new string[0]));
				}
				new Thread(new ParameterizedThreadStart(this.StartSubscription))
				{
					IsBackground = true
				}.Start(subscriber);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x000B47A0 File Offset: 0x000B29A0
		private void StartSubscription(object subscriberObj)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				Subscriber subscriber = (Subscriber)subscriberObj;
				try
				{
					for (;;)
					{
						Notification notification = subscriber.receive(true);
						if (notification != null)
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(this.HandleEvent), notification);
						}
					}
				}
				catch (Exception ex)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268697600, new string[]
						{
							"ONS::StartSubscription() . -" + ex.Message
						});
					}
					subscriber.close();
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

		// Token: 0x06001086 RID: 4230 RVA: 0x000B485C File Offset: 0x000B2A5C
		protected virtual string GetEventTypeForNotification(string serviceName)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000B4864 File Offset: 0x000B2A64
		protected virtual void HandleEvent(object notification)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04001342 RID: 4930
		private const string EvntTypeForService = "(\"eventType=database/event/service\")";

		// Token: 0x04001343 RID: 4931
		private const string EvntTypeForHost = "(\"eventType=database/event/host\")";

		// Token: 0x04001344 RID: 4932
		private string m_serviceName = string.Empty;

		// Token: 0x04001345 RID: 4933
		private bool m_bListeningOnDefaultNodes;
	}
}
