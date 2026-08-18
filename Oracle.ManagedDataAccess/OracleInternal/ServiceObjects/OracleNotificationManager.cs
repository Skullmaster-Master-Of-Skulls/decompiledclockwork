using System;
using System.Collections.Generic;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.Network;
using OracleInternal.TTC;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001B5 RID: 437
	internal class OracleNotificationManager
	{
		// Token: 0x0600108E RID: 4238 RVA: 0x000B4FC0 File Offset: 0x000B31C0
		internal static void StartListener(ref int portNo)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			OracleNotificationManager.m_ntfLsnr.Start(ref portNo);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x000B4FE8 File Offset: 0x000B31E8
		internal static bool IsListenerRunning()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool bListenerStarted;
			try
			{
				bListenerStarted = OracleNotificationManager.m_ntfLsnr.m_bListenerStarted;
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
			return bListenerStarted;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x000B5060 File Offset: 0x000B3260
		internal static void SetCallbackForNotification(OracleNotificationManager.SendNtfDetailsToUpperLayer callBackFn)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			OracleNotificationManager.s_sendNtfDetailsToUpperLayer = callBackFn;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x000B5098 File Offset: 0x000B3298
		internal static void RegisterForChangeNotification(OracleConnectionImpl connectionImpl, OracleDependencyImpl orclDependencyImpl, bool bIncludeRowId)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				string s_machineAddress = OracleDependencyImpl.s_machineAddress;
				if (!OracleNotificationManager.IsListenerRunning())
				{
					int portForlistening = OracleDependencyImpl.m_portForlistening;
					OracleNotificationManager.StartListener(ref portForlistening);
					if (OracleDependencyImpl.m_portForlistening != portForlistening)
					{
						OracleDependencyImpl.m_portForlistening = portForlistening;
					}
				}
				orclDependencyImpl.m_RegIdFromServer = OracleNotificationManager.SendRegistrationInfo(connectionImpl, orclDependencyImpl.m_bQueryBasedNTFN, orclDependencyImpl.m_bIsPersistent, orclDependencyImpl.m_bIsNotifiedOnce, bIncludeRowId, orclDependencyImpl.m_timeout, orclDependencyImpl.m_clientRegistrationId);
				orclDependencyImpl.m_bIsRegistered = true;
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

		// Token: 0x06001092 RID: 4242 RVA: 0x000B5160 File Offset: 0x000B3360
		private static int SendRegistrationInfo(OracleConnectionImpl connectionImpl, bool bQueryBasedRegistration, bool bIsPersistent, bool bIsNotifiedOnce, bool bIncludeRowId, long timeout, long clientRegId)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			int num = 0;
			int result;
			try
			{
				string item = connectionImpl.m_databaseDomainName.Trim() + "|" + connectionImpl.m_databaseName.Trim();
				int mode = 0;
				if (!OracleNotificationManager.m_lstOfDbsRegd.Contains(item))
				{
					lock (OracleNotificationManager.m_syncObjForMode)
					{
						if (!OracleNotificationManager.m_lstOfDbsRegd.Contains(item))
						{
							mode = 1;
							OracleNotificationManager.m_lstOfDbsRegd.Add(item);
						}
					}
				}
				if (bQueryBasedRegistration)
				{
					num = 96;
				}
				if (bIncludeRowId)
				{
					num |= 16;
				}
				int num2 = 0;
				if (bIsPersistent)
				{
					num2 = 1;
				}
				if (bIsNotifiedOnce)
				{
					num2 |= 16;
				}
				int[] nameSpace = new int[]
				{
					2
				};
				string[] registeredAgentName = new string[1];
				int[] payloadType = new int[]
				{
					23
				};
				int[] qosFlags = new int[]
				{
					num2
				};
				int[] timeout2 = new int[]
				{
					(int)timeout
				};
				int[] dbchangeOpFilter = new int[]
				{
					num
				};
				int[] array = new int[1];
				int[] dbchangeTxnLag = array;
				int[] array2 = new int[1];
				int[] dbchangeRegistrationId = array2;
				byte[][] array3 = new byte[][]
				{
					new byte[4]
				};
				array3[0][0] = (byte)((clientRegId & (long)((ulong)-16777216)) >> 24);
				array3[0][1] = (byte)((clientRegId & 16711680L) >> 16);
				array3[0][2] = (byte)((clientRegId & 65280L) >> 8);
				array3[0][3] = (byte)(clientRegId & 255L);
				string location = string.Concat(new string[]
				{
					"(ADDRESS=(PROTOCOL=tcp)(HOST=",
					OracleDependencyImpl.s_machineAddress,
					")(PORT=",
					OracleDependencyImpl.m_portForlistening.ToString(),
					"))?PR=0"
				});
				int num3 = 0;
				try
				{
					connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					connectionImpl.AddAllPiggyBackRequests();
					TTCNotification ttcnotificationObject = connectionImpl.TTCNotificationObject;
					ttcnotificationObject.WriteOKPNMessage(1, mode, connectionImpl.m_cs.m_userId, location, 1, nameSpace, registeredAgentName, array3, payloadType, qosFlags, timeout2, dbchangeOpFilter, dbchangeTxnLag, dbchangeRegistrationId);
					num3 = ttcnotificationObject.ReceiveOKPNResponse();
					OracleConnectionImpl.CheckForAnyErrorFromDB(connectionImpl.m_marshallingEngine.TTCErrorObject);
				}
				finally
				{
					connectionImpl.m_connectionFreeToUseEvent.Set();
				}
				result = num3;
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
			return result;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000B542C File Offset: 0x000B362C
		internal static void UnRegisterFromChangeNotification(OracleConnectionImpl connectionImpl, int dbChangeRegistrationId)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				int[] nameSpace = new int[]
				{
					2
				};
				string[] registeredAgentName = new string[1];
				int[] array = new int[1];
				int[] payloadType = array;
				int[] array2 = new int[1];
				int[] qosFlags = array2;
				int[] array3 = new int[1];
				int[] timeout = array3;
				int[] array4 = new int[1];
				int[] dbchangeOpFilter = array4;
				int[] array5 = new int[1];
				int[] dbchangeTxnLag = array5;
				int[] dbchangeRegistrationId = new int[]
				{
					dbChangeRegistrationId
				};
				byte[][] kpdnrcx = new byte[1][];
				string location = string.Concat(new string[]
				{
					"(ADDRESS=(PROTOCOL=tcp)(HOST=",
					OracleDependencyImpl.s_machineAddress,
					")(PORT=",
					OracleDependencyImpl.m_portForlistening.ToString(),
					"))?PR=0"
				});
				try
				{
					connectionImpl.m_connectionFreeToUseEvent.WaitOne();
					connectionImpl.AddAllPiggyBackRequests();
					TTCNotification ttcnotificationObject = connectionImpl.TTCNotificationObject;
					ttcnotificationObject.WriteOKPNMessage(2, 0, null, location, 1, nameSpace, registeredAgentName, kpdnrcx, payloadType, qosFlags, timeout, dbchangeOpFilter, dbchangeTxnLag, dbchangeRegistrationId);
					ttcnotificationObject.ReceiveOKPNResponse();
					OracleConnectionImpl.CheckForAnyErrorFromDB(connectionImpl.m_marshallingEngine.TTCErrorObject);
				}
				finally
				{
					connectionImpl.m_connectionFreeToUseEvent.Set();
				}
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

		// Token: 0x06001094 RID: 4244 RVA: 0x000B55D8 File Offset: 0x000B37D8
		internal static void HandleNotification(OracleCommunication dataEP)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				NotificationHandler @object = new NotificationHandler(dataEP);
				new Thread(new ThreadStart(@object.ProcessNotification))
				{
					IsBackground = true
				}.Start();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x0400134D RID: 4941
		internal static NotificationListener m_ntfLsnr = NotificationListener.CreateListener();

		// Token: 0x0400134E RID: 4942
		private static List<string> m_lstOfDbsRegd = new List<string>();

		// Token: 0x0400134F RID: 4943
		internal static OracleNotificationManager.SendNtfDetailsToUpperLayer s_sendNtfDetailsToUpperLayer;

		// Token: 0x04001350 RID: 4944
		private static object m_syncObjForMode = new object();

		// Token: 0x020001B6 RID: 438
		// (Invoke) Token: 0x06001098 RID: 4248
		internal delegate void SendNtfDetailsToUpperLayer(object obj);
	}
}
