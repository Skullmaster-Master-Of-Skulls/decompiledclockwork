using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001A4 RID: 420
	internal class ConnectionGroup
	{
		// Token: 0x06001046 RID: 4166 RVA: 0x00056C70 File Offset: 0x00054E70
		internal ConnectionGroup(ServicePoint servicePoint, string connName)
		{
			this.m_ServicePoint = servicePoint;
			this.m_ConnectionLimit = servicePoint.ConnectionLimit;
			this.m_ConnectionList = new ArrayList(3);
			this.m_Name = ConnectionGroup.MakeQueryStr(connName);
			this.m_AbortDelegate = new HttpAbortDelegate(this.Abort);
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x00056CC7 File Offset: 0x00054EC7
		internal string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x00056CCF File Offset: 0x00054ECF
		internal ServicePoint ServicePoint
		{
			get
			{
				return this.m_ServicePoint;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x00056CD7 File Offset: 0x00054ED7
		internal int CurrentConnections
		{
			get
			{
				return this.m_ConnectionList.Count;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00056CE4 File Offset: 0x00054EE4
		// (set) Token: 0x0600104B RID: 4171 RVA: 0x00056CEC File Offset: 0x00054EEC
		internal int ConnectionLimit
		{
			get
			{
				return this.m_ConnectionLimit;
			}
			set
			{
				this.m_ConnectionLimit = value;
				this.PruneExcesiveConnections();
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x00056CFC File Offset: 0x00054EFC
		private ManualResetEvent AsyncWaitHandle
		{
			get
			{
				if (this.m_Event == null)
				{
					Interlocked.CompareExchange(ref this.m_Event, new ManualResetEvent(false), null);
				}
				return (ManualResetEvent)this.m_Event;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x00056D34 File Offset: 0x00054F34
		// (set) Token: 0x0600104E RID: 4174 RVA: 0x00056D90 File Offset: 0x00054F90
		private Queue AuthenticationRequestQueue
		{
			get
			{
				if (this.m_AuthenticationRequestQueue == null)
				{
					ArrayList connectionList = this.m_ConnectionList;
					lock (connectionList)
					{
						if (this.m_AuthenticationRequestQueue == null)
						{
							this.m_AuthenticationRequestQueue = new Queue();
						}
					}
				}
				return this.m_AuthenticationRequestQueue;
			}
			set
			{
				this.m_AuthenticationRequestQueue = value;
			}
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00056D99 File Offset: 0x00054F99
		internal static string MakeQueryStr(string connName)
		{
			if (connName != null)
			{
				return connName;
			}
			return "";
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00056DA8 File Offset: 0x00054FA8
		internal void Associate(Connection connection)
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				this.m_ConnectionList.Add(connection);
			}
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00056DF0 File Offset: 0x00054FF0
		internal void Disassociate(Connection connection)
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				this.m_ConnectionList.Remove(connection);
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00056E38 File Offset: 0x00055038
		internal void ConnectionGoneIdle()
		{
			if (this.m_AuthenticationGroup)
			{
				ArrayList connectionList = this.m_ConnectionList;
				lock (connectionList)
				{
					this.AsyncWaitHandle.Set();
				}
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00056E88 File Offset: 0x00055088
		internal void IncrementConnection()
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				this.m_ActiveConnections++;
				if (this.m_ActiveConnections == 1)
				{
					this.CancelIdleTimer();
				}
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00056EE0 File Offset: 0x000550E0
		internal void DecrementConnection()
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				this.m_ActiveConnections--;
				if (this.m_ActiveConnections == 0)
				{
					this.m_ExpiringTimer = this.ServicePoint.CreateConnectionGroupTimer(this);
				}
				else if (this.m_ActiveConnections < 0)
				{
					this.m_ActiveConnections = 0;
				}
			}
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00056F54 File Offset: 0x00055154
		internal void CancelIdleTimer()
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				TimerThread.Timer expiringTimer = this.m_ExpiringTimer;
				this.m_ExpiringTimer = null;
				if (expiringTimer != null)
				{
					expiringTimer.Cancel();
				}
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00056FA8 File Offset: 0x000551A8
		private bool Abort(HttpWebRequest request, WebException webException)
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				this.AsyncWaitHandle.Set();
			}
			return true;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00056FF0 File Offset: 0x000551F0
		private void PruneAbortedRequests()
		{
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				Queue queue = new Queue();
				foreach (object obj in this.AuthenticationRequestQueue)
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)obj;
					if (!httpWebRequest.Aborted)
					{
						queue.Enqueue(httpWebRequest);
					}
				}
				this.AuthenticationRequestQueue = queue;
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00057090 File Offset: 0x00055290
		private void PruneExcesiveConnections()
		{
			ArrayList arrayList = new ArrayList();
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				int connectionLimit = this.ConnectionLimit;
				if (this.CurrentConnections > connectionLimit)
				{
					int num = this.CurrentConnections - connectionLimit;
					for (int i = 0; i < num; i++)
					{
						arrayList.Add(this.m_ConnectionList[i]);
					}
					this.m_ConnectionList.RemoveRange(0, num);
				}
			}
			foreach (object obj in arrayList)
			{
				Connection connection = (Connection)obj;
				connection.CloseOnIdle();
			}
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00057168 File Offset: 0x00055368
		internal void DisableKeepAliveOnConnections()
		{
			ArrayList arrayList = new ArrayList();
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				foreach (object obj in this.m_ConnectionList)
				{
					Connection value = (Connection)obj;
					arrayList.Add(value);
				}
				this.m_ConnectionList.Clear();
			}
			foreach (object obj2 in arrayList)
			{
				Connection connection = (Connection)obj2;
				connection.CloseOnIdle();
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0005724C File Offset: 0x0005544C
		private Connection FindMatchingConnection(HttpWebRequest request, string connName, out Connection leastbusyConnection)
		{
			bool flag = false;
			leastbusyConnection = null;
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				int num = int.MaxValue;
				foreach (object obj in this.m_ConnectionList)
				{
					Connection connection = (Connection)obj;
					if (connection.LockedRequest == request)
					{
						leastbusyConnection = connection;
						return connection;
					}
					if (!connection.NonKeepAliveRequestPipelined && connection.BusyCount < num && connection.LockedRequest == null)
					{
						leastbusyConnection = connection;
						num = connection.BusyCount;
						if (num == 0)
						{
							flag = true;
						}
					}
				}
				if (!flag && this.CurrentConnections < this.ConnectionLimit)
				{
					leastbusyConnection = new Connection(this);
				}
			}
			return null;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00057340 File Offset: 0x00055540
		private Connection FindConnectionAuthenticationGroup(HttpWebRequest request, string connName)
		{
			Connection connection = null;
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				Connection connection2 = this.FindMatchingConnection(request, connName, out connection);
				if (connection2 != null)
				{
					connection2.MarkAsReserved();
					return connection2;
				}
				if (this.AuthenticationRequestQueue.Count == 0)
				{
					if (connection != null)
					{
						if (request.LockConnection)
						{
							this.m_NtlmNegGroup = true;
							this.m_IISVersion = connection.IISVersion;
						}
						if (request.LockConnection || (this.m_NtlmNegGroup && !request.Pipelined && request.UnsafeOrProxyAuthenticatedConnectionSharing && this.m_IISVersion >= 6))
						{
							connection.LockedRequest = request;
						}
						connection.MarkAsReserved();
						return connection;
					}
				}
				else if (connection != null)
				{
					this.AsyncWaitHandle.Set();
				}
				this.AuthenticationRequestQueue.Enqueue(request);
			}
			Connection result;
			for (;;)
			{
				request.AbortDelegate = this.m_AbortDelegate;
				if (!request.Aborted)
				{
					this.AsyncWaitHandle.WaitOne();
				}
				ArrayList connectionList2 = this.m_ConnectionList;
				lock (connectionList2)
				{
					if (!request.Aborted)
					{
						this.FindMatchingConnection(request, connName, out connection);
						if (this.AuthenticationRequestQueue.Peek() == request)
						{
							this.AuthenticationRequestQueue.Dequeue();
							if (connection != null)
							{
								if (request.LockConnection)
								{
									this.m_NtlmNegGroup = true;
									this.m_IISVersion = connection.IISVersion;
								}
								if (request.LockConnection || (this.m_NtlmNegGroup && !request.Pipelined && request.UnsafeOrProxyAuthenticatedConnectionSharing && this.m_IISVersion >= 6))
								{
									connection.LockedRequest = request;
								}
								connection.MarkAsReserved();
								result = connection;
								break;
							}
							this.AuthenticationRequestQueue.Enqueue(request);
						}
						if (connection == null)
						{
							this.AsyncWaitHandle.Reset();
						}
						continue;
					}
					this.PruneAbortedRequests();
					result = null;
				}
				break;
			}
			return result;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00057520 File Offset: 0x00055720
		internal Connection FindConnection(HttpWebRequest request, string connName, out bool forcedsubmit)
		{
			Connection connection = null;
			Connection connection2 = null;
			bool flag = false;
			ArrayList arrayList = new ArrayList();
			forcedsubmit = false;
			if (this.m_AuthenticationGroup || request.LockConnection)
			{
				this.m_AuthenticationGroup = true;
				return this.FindConnectionAuthenticationGroup(request, connName);
			}
			ArrayList connectionList = this.m_ConnectionList;
			lock (connectionList)
			{
				int num = int.MaxValue;
				bool flag3 = false;
				foreach (object obj in this.m_ConnectionList)
				{
					Connection connection3 = (Connection)obj;
					bool flag4 = false;
					if (!connection3.IsInitalizing && !connection3.NetworkStream.Connected)
					{
						arrayList.Add(connection3);
					}
					else if (flag3)
					{
						flag4 = (!connection3.NonKeepAliveRequestPipelined && num > connection3.BusyCount);
					}
					else
					{
						flag4 = (!connection3.NonKeepAliveRequestPipelined || num > connection3.BusyCount);
					}
					if (flag4)
					{
						connection = connection3;
						num = connection3.BusyCount;
						if (!flag3)
						{
							flag3 = !connection3.NonKeepAliveRequestPipelined;
						}
						if (flag3 && num == 0)
						{
							flag = true;
							break;
						}
					}
				}
				foreach (object obj2 in arrayList)
				{
					Connection connection4 = (Connection)obj2;
					connection4.RemoveFromConnectionList();
				}
				if (!flag && this.CurrentConnections < this.ConnectionLimit)
				{
					connection2 = new Connection(this);
					forcedsubmit = false;
				}
				else
				{
					connection2 = connection;
					forcedsubmit = !flag3;
				}
				connection2.MarkAsReserved();
			}
			return connection2;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0005770C File Offset: 0x0005590C
		[Conditional("DEBUG")]
		internal void DebugMembers(int requestHash)
		{
			foreach (object obj in this.m_ConnectionList)
			{
				Connection connection = (Connection)obj;
			}
		}

		// Token: 0x04001374 RID: 4980
		private const int DefaultConnectionListSize = 3;

		// Token: 0x04001375 RID: 4981
		private ServicePoint m_ServicePoint;

		// Token: 0x04001376 RID: 4982
		private string m_Name;

		// Token: 0x04001377 RID: 4983
		private int m_ConnectionLimit;

		// Token: 0x04001378 RID: 4984
		private ArrayList m_ConnectionList;

		// Token: 0x04001379 RID: 4985
		private object m_Event;

		// Token: 0x0400137A RID: 4986
		private Queue m_AuthenticationRequestQueue;

		// Token: 0x0400137B RID: 4987
		internal bool m_AuthenticationGroup;

		// Token: 0x0400137C RID: 4988
		private HttpAbortDelegate m_AbortDelegate;

		// Token: 0x0400137D RID: 4989
		private bool m_NtlmNegGroup;

		// Token: 0x0400137E RID: 4990
		private int m_IISVersion = -1;

		// Token: 0x0400137F RID: 4991
		private int m_ActiveConnections;

		// Token: 0x04001380 RID: 4992
		private TimerThread.Timer m_ExpiringTimer;
	}
}
