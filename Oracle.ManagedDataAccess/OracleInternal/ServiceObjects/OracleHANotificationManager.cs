using System;
using System.Collections;
using System.Globalization;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ConnectionPool;
using OracleInternal.I18N;
using OracleInternal.NotificationServices;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001B4 RID: 436
	internal class OracleHANotificationManager : OracleONSNotificationManager
	{
		// Token: 0x06001089 RID: 4233 RVA: 0x000B4880 File Offset: 0x000B2A80
		static OracleHANotificationManager()
		{
			OracleHANotificationManager.s_dfi.ShortDatePattern = "yyyy-MM-dd";
			OracleHANotificationManager.s_dfi.ShortTimePattern = "HH:mm:ss";
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x000B48AC File Offset: 0x000B2AAC
		internal OracleHANotificationManager()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000B4910 File Offset: 0x000B2B10
		protected override string GetEventTypeForNotification(string serviceName)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			string result;
			try
			{
				result = "(\"eventType=database/event/service\")|(\"eventType=database/event/host\")";
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

		// Token: 0x0600108C RID: 4236 RVA: 0x000B4968 File Offset: 0x000B2B68
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
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.HA, new string[]
							{
								text
							});
						}
						Hashtable hashtable = new Hashtable();
						text = text.Trim();
						string[] array2 = text.Split(new char[]
						{
							' ',
							'\t',
							'='
						});
						for (int i = 0; i < array2.Length; i += 2)
						{
							hashtable.Add(array2[i].ToLower(), (array2[i].ToLower() != "timestamp") ? array2[i + 1] : (array2[i + 1] + " " + array2[++i + 1]));
						}
						float num = 0f;
						float.TryParse(hashtable["version"].ToString(), out num);
						if ((double)num < 2.0)
						{
							string text2 = (hashtable["service"] != null) ? hashtable["service"].ToString().ToLowerInvariant() : string.Empty;
							string text3 = (hashtable["db_domain"] != null) ? hashtable["db_domain"].ToString().ToLowerInvariant() : string.Empty;
							string text4 = (hashtable["instance"] != null) ? hashtable["instance"].ToString().ToLowerInvariant() : string.Empty;
							string text5 = (hashtable["database"] != null) ? hashtable["database"].ToString().ToLowerInvariant() : string.Empty;
							string text6 = (hashtable["host"] != null) ? hashtable["host"].ToString().ToLowerInvariant() : string.Empty;
							string text7 = (hashtable["status"] != null) ? hashtable["status"].ToString().ToLowerInvariant() : string.Empty;
							string text8 = (hashtable["reason"] != null) ? hashtable["reason"].ToString().ToLowerInvariant() : string.Empty;
							string s = (hashtable["timestamp"] != null) ? hashtable["timestamp"].ToString().ToLowerInvariant() : string.Empty;
							string text9 = (hashtable["event_type"] != null) ? hashtable["event_type"].ToString().ToLowerInvariant() : string.Empty;
							string text10 = (hashtable["timezone"] != null) ? hashtable["timezone"].ToString().ToLowerInvariant() : string.Empty;
							int num2 = (hashtable["drain_timeout"] != null) ? Convert.ToInt32(hashtable["drain_timeout"]) : 0;
							lock (this.locking)
							{
								if (string.Equals(text9, "servicemember", StringComparison.InvariantCultureIgnoreCase))
								{
									if (!this.serviceNamesTable.ContainsKey(text2))
									{
										this.serviceNamesTable.Add(text2, num2);
									}
									else
									{
										this.serviceNamesTable[text2] = num2;
									}
								}
								if (string.Equals(text9, "service", StringComparison.InvariantCultureIgnoreCase) && string.Equals(text7, "down", StringComparison.InvariantCultureIgnoreCase) && this.serviceNamesTable.ContainsKey(text2))
								{
									num2 = Convert.ToInt32(this.serviceNamesTable[text2]);
									this.serviceNamesTable.Remove(text2);
								}
							}
							DateTime dateTime = DateTime.Parse(s, OracleHANotificationManager.s_dfi);
							if (text10 != string.Empty)
							{
								TimeSpan offset = TimeSpan.Parse(text10);
								DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime, offset);
								dateTime = dateTimeOffset.UtcDateTime;
							}
							OracleHAEventSource oracleHAEventSource;
							if (text9 == null || text9.Length == 0)
							{
								string text11 = ((Notification)notification).type();
								if (string.Equals(text11.Trim(), "database/event/service", StringComparison.InvariantCultureIgnoreCase))
								{
									if (text4 != null && text4.Length > 0)
									{
										oracleHAEventSource = OracleHAEventSource.Instance;
									}
									else
									{
										oracleHAEventSource = OracleHAEventSource.Service;
									}
								}
								else
								{
									if (!string.Equals(text11.Trim(), "database/event/host", StringComparison.InvariantCultureIgnoreCase))
									{
										return;
									}
									oracleHAEventSource = OracleHAEventSource.Node;
								}
							}
							else
							{
								oracleHAEventSource = (OracleHAEventSource)Enum.Parse(typeof(OracleHAEventSource), text9, true);
							}
							OracleHAEventStatus oracleHAEventStatus;
							if (text7.ToLowerInvariant() == "up")
							{
								oracleHAEventStatus = OracleHAEventStatus.Up;
							}
							else
							{
								oracleHAEventStatus = OracleHAEventStatus.Down;
							}
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.HA, new string[]
								{
									string.Concat(new object[]
									{
										"Event =",
										oracleHAEventSource,
										";Database=",
										text5,
										";Database domain=",
										text3,
										";Service=",
										text2,
										";Instance=",
										text4,
										";Host=",
										text6,
										";Status=",
										oracleHAEventStatus,
										";Reason=",
										text8,
										";Time=",
										dateTime.ToString(),
										";drain_timeout=",
										num2,
										"\n"
									})
								});
							}
							OracleHAEventArgs haEvent = new OracleHAEventArgs(oracleHAEventSource, oracleHAEventStatus, text2, text5, text3, text4, text6, text8, dateTime, num2);
							OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.ProcessHAEvent(haEvent);
						}
					}
					catch (Exception ex)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268697600, new string[]
							{
								"OracleHANotificationManager::HandleEvent() failed. -" + ex.Message
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

		// Token: 0x04001346 RID: 4934
		private const string EvntTypeForService = "(\"eventType=database/event/service\")";

		// Token: 0x04001347 RID: 4935
		private const string EvntTypeForHost = "(\"eventType=database/event/host\")";

		// Token: 0x04001348 RID: 4936
		private string m_serviceName = string.Empty;

		// Token: 0x04001349 RID: 4937
		private Hashtable serviceNamesTable = new Hashtable();

		// Token: 0x0400134A RID: 4938
		internal object locking = new object();

		// Token: 0x0400134B RID: 4939
		private static DateTimeFormatInfo s_dfi = new DateTimeFormatInfo();
	}
}
