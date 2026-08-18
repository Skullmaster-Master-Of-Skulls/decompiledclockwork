using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.Network;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001AB RID: 427
	internal class NotificationListener
	{
		// Token: 0x06000FF3 RID: 4083 RVA: 0x000A5394 File Offset: 0x000A3594
		private NotificationListener()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			this.m_bListenerStarted = false;
			this.m_syncObject = new object();
			this.m_oraBufPoolForListener = new OraBufPool(127);
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x000A53F8 File Offset: 0x000A35F8
		internal static NotificationListener CreateListener()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			NotificationListener ntfLister;
			try
			{
				ntfLister = NotificationListener.m_ntfLister;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return ntfLister;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000A5450 File Offset: 0x000A3650
		internal bool Start(ref int portNo)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool flag = false;
			bool result;
			try
			{
				if (!this.m_bListenerStarted)
				{
					lock (this.m_syncObject)
					{
						if (!this.m_bListenerStarted)
						{
							bool flag3 = portNo == -1;
							if (portNo <= 0 || !NotificationListener.IsPortAvailable(portNo))
							{
								if (!flag3)
								{
									throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.NTFN_PORT_NOT_AVAILABLE, new string[0]));
								}
								portNo = NotificationListener.FindFreePort();
							}
							this.m_lsnrEP = new OracleCommunication(new ConOraBufPool(this.m_oraBufPoolForListener));
							this.m_lsnrEP.Listen(OracleDependencyImpl.GetMachineAddress() + ":" + portNo, true);
							this.m_bListenerStarted = true;
							flag = true;
						}
						this.m_lsnrThread = new Thread(new ThreadStart(this.ListenerThreadFunction));
						this.m_lsnrThread.IsBackground = true;
						this.m_lsnrThread.Start();
					}
				}
				result = flag;
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

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000A55A8 File Offset: 0x000A37A8
		internal void ListenerThreadFunction()
		{
			if (!ProviderConfig.m_bTraceLevelPrivate)
			{
				goto IL_18;
			}
			Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			try
			{
				for (;;)
				{
					IL_18:
					OracleCommunication dataEP = new OracleCommunication(this.m_lsnrEP, null);
					OracleNotificationManager.HandleNotification(dataEP);
				}
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

		// Token: 0x06000FF7 RID: 4087 RVA: 0x000A562C File Offset: 0x000A382C
		internal static bool IsPortAvailable(int portNo)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			bool flag = true;
			bool result;
			try
			{
				IPGlobalProperties ipglobalProperties = IPGlobalProperties.GetIPGlobalProperties();
				TcpConnectionInformation[] activeTcpConnections = ipglobalProperties.GetActiveTcpConnections();
				foreach (TcpConnectionInformation tcpConnectionInformation in activeTcpConnections)
				{
					if (tcpConnectionInformation.LocalEndPoint.Port == portNo)
					{
						flag = false;
						break;
					}
				}
				result = flag;
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

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000A56DC File Offset: 0x000A38DC
		[SocketPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static int FindFreePort()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			int result;
			try
			{
				TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 0);
				tcpListener.Start();
				int port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
				tcpListener.Stop();
				result = port;
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

		// Token: 0x040012B1 RID: 4785
		internal bool m_bListenerStarted;

		// Token: 0x040012B2 RID: 4786
		private object m_syncObject;

		// Token: 0x040012B3 RID: 4787
		private Thread m_lsnrThread;

		// Token: 0x040012B4 RID: 4788
		internal OracleCommunication m_lsnrEP;

		// Token: 0x040012B5 RID: 4789
		private OraBufPool m_oraBufPoolForListener;

		// Token: 0x040012B6 RID: 4790
		internal static NotificationListener m_ntfLister = new NotificationListener();
	}
}
