using System;
using System.Collections;
using System.Data;
using System.Threading;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000066 RID: 102
	public class OracleDependency
	{
		// Token: 0x06000510 RID: 1296 RVA: 0x0002F478 File Offset: 0x0002D678
		static OracleDependency()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			if (ConfigBaseClass.m_DBNotificationPort != -1)
			{
				OracleDependency.Port = ConfigBaseClass.m_DBNotificationPort;
			}
			try
			{
				OracleNotificationManager.SetCallbackForNotification(new OracleNotificationManager.SendNtfDetailsToUpperLayer(OracleDependency.SetNotificationDetails));
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
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0002F518 File Offset: 0x0002D718
		public OracleDependency()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_orclDependencyImpl = new OracleDependencyImpl(OracleNotificationRequest.s_bDefIsNotifiedOnce, (long)OracleNotificationRequest.s_DefRegTimeout, OracleNotificationRequest.s_bDefIsPersistent);
				OracleDependency.s_depTable.Add(this.m_guid, this);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0002F600 File Offset: 0x0002D800
		public OracleDependency(OracleCommand cmd) : this(cmd, OracleNotificationRequest.s_bDefIsNotifiedOnce, (long)OracleNotificationRequest.s_DefRegTimeout, OracleNotificationRequest.s_bDefIsPersistent)
		{
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0002F61C File Offset: 0x0002D81C
		public OracleDependency(OracleCommand cmd, bool isNotifiedOnce, long timeout, bool isPersistent)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (cmd == null)
				{
					throw new ArgumentNullException("cmd");
				}
				if (timeout < 0L || timeout > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("timeout");
				}
				if (cmd.Notification != null)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.NTFN_CMD_ALREADY_EXIST, new string[0]));
				}
				this.m_orclDependencyImpl = new OracleDependencyImpl(isNotifiedOnce, timeout, isPersistent);
				long clientRegistrationId = Interlocked.Increment(ref OracleDependency.m_staticRegistrationId);
				this.m_orclDependencyImpl.m_clientRegistrationId = clientRegistrationId;
				OracleNotificationRequest oracleNotificationRequest = new OracleNotificationRequest(OracleNotificationRequest.s_ChangedNotificationName, this.m_orclDependencyImpl);
				cmd.Notification = oracleNotificationRequest;
				OracleDependency.s_depTable.Add(this.m_guid, this);
				if (OracleNotificationRequest.s_idTable[oracleNotificationRequest.Id] == null)
				{
					OracleNotificationRequest.s_idTable.Add(oracleNotificationRequest.Id, this.m_guid);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0002F79C File Offset: 0x0002D99C
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x0002F7A4 File Offset: 0x0002D9A4
		public static int Port
		{
			get
			{
				return OracleDependencyImpl.m_portForlistening;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("Port");
				}
				if (!OracleNotificationManager.IsListenerRunning())
				{
					OracleDependencyImpl.m_portForlistening = value;
					return;
				}
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.NTFN_LISTENER_ALREADY_STARTED, new string[0]));
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0002F7D8 File Offset: 0x0002D9D8
		public static OracleDependency GetOracleDependency(string guid)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDependency result;
			try
			{
				if (guid == null)
				{
					throw new ArgumentNullException("guid");
				}
				result = (OracleDependency)OracleDependency.s_depTable[guid];
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0002F864 File Offset: 0x0002DA64
		public string DataSource
		{
			get
			{
				return this.m_dataSource;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0002F86C File Offset: 0x0002DA6C
		public bool HasChanges
		{
			get
			{
				bool bHasChanges = this.m_bHasChanges;
				this.m_bHasChanges = false;
				return bHasChanges;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0002F888 File Offset: 0x0002DA88
		public string Id
		{
			get
			{
				return this.m_guid;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0002F890 File Offset: 0x0002DA90
		private long InvalidationString
		{
			get
			{
				return this.m_orclDependencyImpl.m_clientRegistrationId;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0002F8A0 File Offset: 0x0002DAA0
		public bool IsEnabled
		{
			get
			{
				return this.m_orclDependencyImpl.m_bIsEnabled;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0002F8B0 File Offset: 0x0002DAB0
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0002F8B8 File Offset: 0x0002DAB8
		public bool QueryBasedNotification
		{
			get
			{
				return this.m_bQueryBasedNTFN;
			}
			set
			{
				this.m_bQueryBasedNTFN = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0002F8C4 File Offset: 0x0002DAC4
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0002F8CC File Offset: 0x0002DACC
		public OracleRowidInfo RowidInfo
		{
			get
			{
				return this.m_OracleRowidInfo;
			}
			set
			{
				this.m_OracleRowidInfo = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0002F8D8 File Offset: 0x0002DAD8
		public ArrayList RegisteredResources
		{
			get
			{
				return (ArrayList)this.m_orclDependencyImpl.m_regList.Clone();
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0002F8F0 File Offset: 0x0002DAF0
		public ArrayList RegisteredQueryIDs
		{
			get
			{
				return (ArrayList)this.m_orclDependencyImpl.m_queryIDList.Clone();
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0002F908 File Offset: 0x0002DB08
		public string UserName
		{
			get
			{
				return this.m_userName;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000523 RID: 1315 RVA: 0x0002F910 File Offset: 0x0002DB10
		// (remove) Token: 0x06000524 RID: 1316 RVA: 0x0002F948 File Offset: 0x0002DB48
		public event OnChangeEventHandler OnChange;

		// Token: 0x06000525 RID: 1317 RVA: 0x0002F980 File Offset: 0x0002DB80
		public void AddCommandDependency(OracleCommand cmd)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (cmd == null)
				{
					throw new ArgumentNullException("cmd");
				}
				if (cmd.Notification != null && OracleNotificationRequest.s_idTable[cmd.Notification.Id] != null)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.NTFN_CMD_ALREADY_EXIST, new string[0]));
				}
				if (OracleDependency.s_depTable[this.m_guid] == null)
				{
					OracleDependency.s_depTable.Add(this.m_guid, this);
					this.m_orclDependencyImpl.m_clientRegistrationId = Interlocked.Increment(ref OracleDependency.m_staticRegistrationId);
				}
				cmd.Notification = new OracleNotificationRequest(OracleNotificationRequest.s_ChangedNotificationName, this.m_orclDependencyImpl);
				if (OracleNotificationRequest.s_idTable[cmd.Notification.Id] == null)
				{
					OracleNotificationRequest.s_idTable.Add(cmd.Notification.Id, this.m_guid);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0002FAC0 File Offset: 0x0002DCC0
		public void RemoveRegistration(OracleConnection conn)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (!this.m_orclDependencyImpl.m_bIsRegistered)
			{
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.NTFN_REG_NOTVALID, new string[0]));
			}
			if (conn.State != ConnectionState.Open)
			{
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_CLOSED, new string[0]));
			}
			try
			{
				if (conn.m_oracleConnectionImpl != null)
				{
					OracleNotificationManager.UnRegisterFromChangeNotification(conn.m_oracleConnectionImpl, this.m_orclDependencyImpl.m_RegIdFromServer);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
				this.m_orclDependencyImpl.m_bIsRegistered = false;
				this.m_orclDependencyImpl.m_bIsEnabled = false;
			}
			try
			{
				if (OracleDependency.s_depTable[this.m_guid] != null)
				{
					OracleDependency.s_depTable.Remove(this.m_guid);
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex2, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			try
			{
				long clientRegistrationId = this.m_orclDependencyImpl.m_clientRegistrationId;
				if (OracleNotificationRequest.s_idTable[clientRegistrationId] != null)
				{
					OracleNotificationRequest.s_idTable.Remove(clientRegistrationId);
				}
			}
			catch (Exception ex3)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex3, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0002FC80 File Offset: 0x0002DE80
		public void FiredEvent(OracleNotificationEventArgs e)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.OnChange != null)
				{
					try
					{
						this.OnChange(this, e);
					}
					catch
					{
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0002FD14 File Offset: 0x0002DF14
		internal void SetRegisterInfo(string username, string dataSource, OracleNotificationRequest ntfnReq)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_userName = username;
				this.m_dataSource = dataSource;
				this.m_orclDependencyImpl.SetRegisterInfo(ntfnReq.m_bIsNotifiedOnce, ntfnReq.m_bIsPersistent, ntfnReq.m_timeout);
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
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0002FDB0 File Offset: 0x0002DFB0
		internal static OracleDependency GetOracleDependencyFromNTFNId(long id)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleDependency result;
			try
			{
				string text = (string)OracleNotificationRequest.s_idTable[id];
				if (text != null)
				{
					result = (OracleDependency)OracleDependency.s_depTable[text];
				}
				else
				{
					result = null;
				}
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

		// Token: 0x0600052A RID: 1322 RVA: 0x0002FE50 File Offset: 0x0002E050
		internal static void SetNotificationDetails(object notifInfoObj)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleDependency oracleDependency = null;
			NotificationInfo notificationInfo = (NotificationInfo)notifInfoObj;
			try
			{
				short csId = notificationInfo.m_csId;
				int[] regId = notificationInfo.m_regId;
				bool bTimeoutEvent = notificationInfo.m_bTimeoutEvent;
				byte[] notifInfo = notificationInfo.m_notifInfo;
				int numOfRegistrations = notificationInfo.m_numOfRegistrations;
				OracleNotificationEventArgs oracleNotificationEventArgs = null;
				for (int i = 0; i < numOfRegistrations; i++)
				{
					oracleDependency = OracleDependency.GetOracleDependencyFromNTFNId((long)regId[i]);
					if (oracleDependency != null)
					{
						OracleDependencyImpl orclDependencyImpl = oracleDependency.m_orclDependencyImpl;
						if (orclDependencyImpl.m_bIsNotifiedOnce)
						{
							orclDependencyImpl.m_bIsRegistered = false;
							orclDependencyImpl.m_bIsEnabled = false;
							try
							{
								OracleDependency.s_depTable.Remove(oracleDependency.m_guid);
							}
							catch
							{
							}
							try
							{
								OracleNotificationRequest.s_idTable.Remove(regId);
							}
							catch
							{
							}
						}
						if (oracleNotificationEventArgs == null)
						{
							oracleNotificationEventArgs = new OracleNotificationEventArgs(new NotificationDetails(csId, notifInfo));
						}
						else
						{
							oracleNotificationEventArgs = oracleNotificationEventArgs.Clone();
						}
						if (bTimeoutEvent)
						{
							oracleNotificationEventArgs.m_notificationDetails.m_type = OracleNotificationType.Subscribe;
							oracleNotificationEventArgs.m_notificationDetails.m_source = OracleNotificationSource.Subscription;
							oracleNotificationEventArgs.m_notificationDetails.m_info = OracleNotificationInfo.End;
							oracleNotificationEventArgs.m_bInfoNotPopulated = false;
						}
						if (oracleDependency.OnChange == null)
						{
							if (oracleNotificationEventArgs.Source == OracleNotificationSource.Object || oracleNotificationEventArgs.Source == OracleNotificationSource.Data)
							{
								oracleDependency.m_bHasChanges = true;
							}
							break;
						}
						if (oracleNotificationEventArgs.Source == OracleNotificationSource.Database || oracleNotificationEventArgs.Source == OracleNotificationSource.Subscription)
						{
							if (oracleNotificationEventArgs.Source == OracleNotificationSource.Subscription && oracleNotificationEventArgs.Info == OracleNotificationInfo.End)
							{
								orclDependencyImpl.m_bIsRegistered = false;
								orclDependencyImpl.m_bIsEnabled = false;
								try
								{
									OracleDependency.s_depTable.Remove(oracleDependency.m_guid);
								}
								catch
								{
								}
								try
								{
									OracleNotificationRequest.s_idTable.Remove(regId);
								}
								catch
								{
								}
							}
							lock (oracleDependency.m_syncObject)
							{
								oracleDependency.FiredEvent(oracleNotificationEventArgs);
							}
							break;
						}
						if (oracleNotificationEventArgs.m_notificationDetails.m_regIdFromServer == orclDependencyImpl.m_RegIdFromServer)
						{
							if (oracleDependency.m_bQueryBasedNTFN)
							{
								foreach (long num in oracleNotificationEventArgs.QueryIdList)
								{
									if (!orclDependencyImpl.m_queryIDList.Contains(num))
									{
										lock (orclDependencyImpl.m_syncList)
										{
											if (!orclDependencyImpl.m_queryIDList.Contains(num))
											{
												orclDependencyImpl.m_queryIDList.Add(num);
											}
										}
									}
								}
							}
							lock (oracleDependency.m_syncObject)
							{
								oracleDependency.m_bHasChanges = true;
								oracleDependency.FiredEvent(oracleNotificationEventArgs);
							}
						}
					}
				}
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
		}

		// Token: 0x0400063F RID: 1599
		internal OracleDependencyImpl m_orclDependencyImpl;

		// Token: 0x04000640 RID: 1600
		private static Hashtable s_depTable = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04000641 RID: 1601
		private static long m_staticRegistrationId;

		// Token: 0x04000642 RID: 1602
		private string m_guid = Guid.NewGuid().ToString();

		// Token: 0x04000643 RID: 1603
		internal string m_dataSource = "";

		// Token: 0x04000644 RID: 1604
		internal string m_userName = "";

		// Token: 0x04000645 RID: 1605
		private bool m_bHasChanges;

		// Token: 0x04000646 RID: 1606
		internal OracleRowidInfo m_OracleRowidInfo = OracleRowidInfo.Default;

		// Token: 0x04000647 RID: 1607
		internal bool m_bQueryBasedNTFN = true;

		// Token: 0x04000648 RID: 1608
		private object m_syncObject = new object();
	}
}
