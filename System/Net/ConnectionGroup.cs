using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace System.Net
{
	// Token: 0x020004CD RID: 1229
	internal class ConnectionGroup
	{
		// Token: 0x06002601 RID: 9729 RVA: 0x000993C4 File Offset: 0x000983C4
		internal ConnectionGroup(ServicePoint servicePoint, string connName)
		{
			this.m_ServicePoint = servicePoint;
			this.m_ConnectionLimit = servicePoint.ConnectionLimit;
			this.m_ConnectionList = new ArrayList(3);
			this.m_Name = ConnectionGroup.MakeQueryStr(connName);
			this.m_AbortDelegate = new HttpAbortDelegate(this.Abort);
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x0009941B File Offset: 0x0009841B
		internal ServicePoint ServicePoint
		{
			get
			{
				return this.m_ServicePoint;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x00099423 File Offset: 0x00098423
		internal int CurrentConnections
		{
			get
			{
				return this.m_ConnectionList.Count;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x00099430 File Offset: 0x00098430
		// (set) Token: 0x06002605 RID: 9733 RVA: 0x00099438 File Offset: 0x00098438
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

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x00099448 File Offset: 0x00098448
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

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x00099480 File Offset: 0x00098480
		// (set) Token: 0x06002608 RID: 9736 RVA: 0x000994D4 File Offset: 0x000984D4
		private Queue AuthenticationRequestQueue
		{
			get
			{
				if (this.m_AuthenticationRequestQueue == null)
				{
					lock (this.m_ConnectionList)
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

		// Token: 0x06002609 RID: 9737 RVA: 0x000994DD File Offset: 0x000984DD
		internal static string MakeQueryStr(string connName)
		{
			if (connName != null)
			{
				return connName;
			}
			return "";
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000994EC File Offset: 0x000984EC
		internal void Associate(Connection connection)
		{
			lock (this.m_ConnectionList)
			{
				this.m_ConnectionList.Add(connection);
			}
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x0009952C File Offset: 0x0009852C
		internal void Disassociate(Connection connection)
		{
			lock (this.m_ConnectionList)
			{
				this.m_ConnectionList.Remove(connection);
			}
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x0009956C File Offset: 0x0009856C
		internal void ConnectionGoneIdle()
		{
			if (this.m_AuthenticationGroup)
			{
				lock (this.m_ConnectionList)
				{
					this.AsyncWaitHandle.Set();
				}
			}
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000995B4 File Offset: 0x000985B4
		private bool Abort(HttpWebRequest request, WebException webException)
		{
			lock (this.m_ConnectionList)
			{
				this.AsyncWaitHandle.Set();
			}
			return true;
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000995F4 File Offset: 0x000985F4
		private void PruneAbortedRequests()
		{
			lock (this.m_ConnectionList)
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

		// Token: 0x0600260F RID: 9743 RVA: 0x00099688 File Offset: 0x00098688
		private void PruneExcesiveConnections()
		{
			ArrayList arrayList = new ArrayList();
			lock (this.m_ConnectionList)
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

		// Token: 0x06002610 RID: 9744 RVA: 0x00099754 File Offset: 0x00098754
		internal void DisableKeepAliveOnConnections()
		{
			ArrayList arrayList = new ArrayList();
			lock (this.m_ConnectionList)
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

		// Token: 0x06002611 RID: 9745 RVA: 0x00099834 File Offset: 0x00098834
		private Connection FindMatchingConnection(HttpWebRequest request, string connName, out Connection leastbusyConnection)
		{
			bool flag = false;
			leastbusyConnection = null;
			lock (this.m_ConnectionList)
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
					if (connection.BusyCount < num && connection.LockedRequest == null)
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

		// Token: 0x06002612 RID: 9746 RVA: 0x00099910 File Offset: 0x00098910
		private Connection FindConnectionAuthenticationGroup(HttpWebRequest request, string connName)
		{
			Connection connection = null;
			lock (this.m_ConnectionList)
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
				lock (this.m_ConnectionList)
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

		// Token: 0x06002613 RID: 9747 RVA: 0x00099AD8 File Offset: 0x00098AD8
		internal Connection FindConnection(HttpWebRequest request, string connName)
		{
			Connection connection = null;
			Connection connection2 = null;
			bool flag = false;
			if (this.m_AuthenticationGroup || request.LockConnection)
			{
				this.m_AuthenticationGroup = true;
				return this.FindConnectionAuthenticationGroup(request, connName);
			}
			lock (this.m_ConnectionList)
			{
				int num = int.MaxValue;
				foreach (object obj in this.m_ConnectionList)
				{
					Connection connection3 = (Connection)obj;
					if (connection3.BusyCount < num)
					{
						connection = connection3;
						num = connection3.BusyCount;
						if (num == 0)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag && this.CurrentConnections < this.ConnectionLimit)
				{
					connection2 = new Connection(this);
				}
				else
				{
					connection2 = connection;
				}
				connection2.MarkAsReserved();
			}
			return connection2;
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x00099BC4 File Offset: 0x00098BC4
		[Conditional("DEBUG")]
		internal void Debug(int requestHash)
		{
			foreach (object obj in this.m_ConnectionList)
			{
				Connection connection = (Connection)obj;
			}
		}

		// Token: 0x040025BF RID: 9663
		private const int DefaultConnectionListSize = 3;

		// Token: 0x040025C0 RID: 9664
		private ServicePoint m_ServicePoint;

		// Token: 0x040025C1 RID: 9665
		private string m_Name;

		// Token: 0x040025C2 RID: 9666
		private int m_ConnectionLimit;

		// Token: 0x040025C3 RID: 9667
		private ArrayList m_ConnectionList;

		// Token: 0x040025C4 RID: 9668
		private object m_Event;

		// Token: 0x040025C5 RID: 9669
		private Queue m_AuthenticationRequestQueue;

		// Token: 0x040025C6 RID: 9670
		internal bool m_AuthenticationGroup;

		// Token: 0x040025C7 RID: 9671
		private HttpAbortDelegate m_AbortDelegate;

		// Token: 0x040025C8 RID: 9672
		private bool m_NtlmNegGroup;

		// Token: 0x040025C9 RID: 9673
		private int m_IISVersion = -1;
	}
}
