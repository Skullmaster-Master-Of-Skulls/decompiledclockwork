using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000183 RID: 387
	internal class NodeList
	{
		// Token: 0x06000ED8 RID: 3800 RVA: 0x00099720 File Offset: 0x00097920
		public string getId()
		{
			return this.id;
		}

		// Token: 0x170002BE RID: 702
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x00099728 File Offset: 0x00097928
		protected internal virtual NodeList FailOver
		{
			set
			{
				if (value == null)
				{
					this.joinStaleRecievers(null);
					for (int i = 0; i < this.connections.Length; i++)
					{
						this.connections[i].ClientShutdown = false;
					}
				}
				this.failOverList = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00099768 File Offset: 0x00097968
		public virtual string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x00099770 File Offset: 0x00097970
		protected internal virtual long Shutdown
		{
			set
			{
				this.setConnectionsBusy();
				lock (this.connLock)
				{
					if (this.shutdown)
					{
						this.clearConnectionsBusy();
						return;
					}
					this.shutdown = true;
					this.shutdownTimeout = value;
				}
				this.clearConnectionsBusy();
			}
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x000997D4 File Offset: 0x000979D4
		public NodeList(string listId, string listNodes, int listConcurrency, bool listActive, ONS listONS)
		{
			this.ons = listONS;
			this.id = listId;
			this.concurrency = listConcurrency;
			this.active = listActive;
			this.shutdownTimeout = this.ons.shutdowntimeout;
			this.failedOver_Renamed_Field = false;
			this.shutdown = false;
			this.buildConnectionArray(listNodes);
			this.publishLock = new object();
			this.publishQueue = new List<Notification>();
			this.connLock = new object();
			this.staleReceivers = new List<ReceiverThread>();
			this.notifications = new Hashtable();
			this.lastCleanupTime = 0L;
			this.concurrency = this.connections.Length;
			if (this.concurrency > listConcurrency)
			{
				this.concurrency = listConcurrency;
			}
			this.concurrencies = new Concurrency[this.concurrency];
			for (int i = 0; i < this.concurrency; i++)
			{
				this.concurrencies[i] = new Concurrency(i);
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x000998B8 File Offset: 0x00097AB8
		protected internal virtual void start(bool force)
		{
			lock (this.connLock)
			{
				if (this.shutdown)
				{
					return;
				}
				if (!this.active && !force)
				{
					return;
				}
			}
			lock (this.connLock)
			{
				for (int i = 0; i < this.concurrency; i++)
				{
					this.concurrencies[i].assign(this.connections[i]);
					this.startConnection(this.connections[i]);
				}
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00099968 File Offset: 0x00097B68
		private void startConnection(Connection conn)
		{
			lock (this.connLock)
			{
				if (this.shutdown)
				{
					return;
				}
			}
			ReceiverThread receiverThread = new ReceiverThread(this, conn);
			SenderThread senderThread = new SenderThread(this, conn);
			receiverThread.Start();
			senderThread.Start();
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000999C8 File Offset: 0x00097BC8
		private SenderThread getSenderThread(bool wakeNew)
		{
			if (this.senderThread == null)
			{
				int i = 0;
				int num = this.senderIndex;
				while (i < this.connections.Length)
				{
					if (num == this.connections.Length)
					{
						num = 0;
					}
					if (this.connections[num].sender != null && this.connections[num].socket != null)
					{
						this.senderThread = this.connections[num].sender;
						if (this.senderIndex != num)
						{
							this.senderIndex = num;
						}
						if (wakeNew)
						{
							this.senderThread.wakeThread();
							break;
						}
						break;
					}
					else
					{
						num++;
						i++;
					}
				}
			}
			return this.senderThread;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00099A64 File Offset: 0x00097C64
		protected internal virtual Notification getFirstPublished(SenderThread st)
		{
			Notification result = null;
			lock (this.connLock)
			{
				SenderThread senderThread = this.getSenderThread(true);
				if (st == senderThread)
				{
					lock (this.publishLock)
					{
						int count = this.publishQueue.Count;
						if (count != 0)
						{
							result = this.publishQueue[0];
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00099AF8 File Offset: 0x00097CF8
		protected internal virtual void removeFirstPublished(Notification re, SenderThread st)
		{
			lock (this.connLock)
			{
				SenderThread senderThread = this.getSenderThread(true);
				if (st == senderThread)
				{
					lock (this.publishLock)
					{
						int count = this.publishQueue.Count;
						if (count != 0)
						{
							Notification notification = this.publishQueue[0];
							this.publishQueue.RemoveAt(0);
							if (notification != re)
							{
								this.publishQueue.Insert(0, notification);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00099BA8 File Offset: 0x00097DA8
		protected internal virtual void clearPublishedSender(SenderThread st)
		{
			lock (this.connLock)
			{
				if (st == this.senderThread)
				{
					this.senderThread = null;
					this.getSenderThread(true);
				}
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00099BFC File Offset: 0x00097DFC
		protected internal virtual void send(SubscriptionNotification se)
		{
			lock (this.connLock)
			{
				if (!this.shutdown)
				{
					for (int i = 0; i < this.connections.Length; i++)
					{
						if (this.connections[i].sender != null)
						{
							this.connections[i].sender.send(se);
						}
					}
				}
			}
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00099C74 File Offset: 0x00097E74
		private void buildConnectionArray(string hplist)
		{
			string[] array;
			if (this.ons.useSCAN)
			{
				array = this.scanNodes(hplist);
			}
			else
			{
				array = hplist.Split(new char[]
				{
					','
				});
			}
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (j != i && array[j] != null && array[i].Equals(array[j]))
						{
							array[j] = null;
							break;
						}
					}
					int num2 = array[i].LastIndexOf(':');
					if (num2 != -1)
					{
						int num3;
						try
						{
							num3 = int.Parse(array[i].Substring(num2 + 1));
							if (num3 > 0)
							{
								num++;
							}
						}
						catch (Exception ex)
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
								{
									"NodeList::buildConnectionArray() failed. -" + ex.Message
								});
							}
							num3 = 0;
						}
						if (num3 <= 0)
						{
							array[i] = null;
						}
					}
					else
					{
						array[i] = null;
					}
				}
			}
			if (num == 0)
			{
				throw new ONSException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ONS_NO_VALID_HOST_PORT_VALUES, new string[]
				{
					this.id,
					hplist
				}));
			}
			this.connections = new Connection[num];
			int num4 = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					int num2 = array[i].LastIndexOf(':');
					if (num2 != -1)
					{
						try
						{
							int num3 = int.Parse(array[i].Substring(num2 + 1));
							this.connections[num4] = new Connection(this, array[i].Substring(0, num2), num3, i);
							num4++;
						}
						catch (Exception ex2)
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
								{
									"NodeList::buildConnectionArray() failed. -" + ex2.Message
								});
							}
						}
					}
				}
			}
			if (num4 != num)
			{
				this.connections = null;
				throw new ONSException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ONS_HOST_PORT_PARSE_ERROR, new string[]
				{
					this.id,
					hplist
				}));
			}
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00099EA4 File Offset: 0x000980A4
		private string[] scanNodes(string hpList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = hpList.Split(new char[]
			{
				','
			});
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					':'
				});
				if (array3.Length != 2)
				{
					throw new ONSException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ONS_BAD_FORMAT_OF_NODES, new string[]
					{
						this.id,
						text
					}));
				}
				string hostNameOrAddress = array3[0];
				string str = array3[1];
				IPAddress[] array4 = null;
				try
				{
					array4 = Dns.GetHostAddresses(hostNameOrAddress);
				}
				catch (Exception)
				{
					throw new ONSException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ONS_WRONG_SERVER_NODES_CONFIG, new string[]
					{
						this.id,
						hpList
					}));
				}
				if (array4.Length == 1)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(text);
				}
				else
				{
					foreach (IPAddress ipaddress in array4)
					{
						if (stringBuilder.Length != 0)
						{
							stringBuilder.Append(',');
						}
						string value = ipaddress.ToString() + ":" + str;
						stringBuilder.Append(value);
					}
				}
			}
			string text2 = stringBuilder.ToString();
			string[] array6 = text2.Split(new char[]
			{
				','
			});
			if (this.ons.defaultList)
			{
				this.nodesRandomize(array6);
			}
			return array6;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0009A030 File Offset: 0x00098230
		private void nodesRandomize(string[] nodes)
		{
			int num = nodes.Length;
			if (num <= 1)
			{
				return;
			}
			Random random = new Random();
			for (int i = 0; i < num; i++)
			{
				int num2 = random.Next(num);
				string text = nodes[i];
				nodes[i] = nodes[num2];
				nodes[num2] = text;
			}
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0009A070 File Offset: 0x00098270
		protected internal virtual void checkConnections(Connection conn)
		{
			this.joinStaleRecievers(conn.receiver);
			long num = 0L;
			this.setConnectionsBusy();
			int concurrencyIndex = conn.ConcurrencyIndex;
			if (conn.receiver != null && !conn.shutdown && !this.shutdown)
			{
				int num2 = this.concurrencies[concurrencyIndex].assignedIndex + 1;
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (num2 == this.connections.Length)
					{
						num2 = 0;
					}
					if (num2 == this.concurrencies[concurrencyIndex].scanIndex)
					{
						long num3 = (DateTime.Now.Ticks - 621355968000000000L) / 10000L;
						if (!this.failedOver_Renamed_Field)
						{
							this.concurrencies[concurrencyIndex].setListFailed();
							if (this.active)
							{
								bool flag = true;
								for (int j = 0; j < this.concurrency; j++)
								{
									if (!this.concurrencies[j].listFailed)
									{
										flag = false;
										break;
									}
								}
								if (flag && this.ons.nodeListFailOver(this, concurrencyIndex))
								{
									break;
								}
							}
						}
						long num4 = num3 - this.concurrencies[concurrencyIndex].scanTime;
						if (num4 < 30000L)
						{
							num = 30000L - num4;
						}
						if (num < 5000L)
						{
							num = 5000L;
						}
						this.concurrencies[concurrencyIndex].ScanTime = num3;
					}
					else if (this.concurrencies[concurrencyIndex].listFailed)
					{
						num = 5000L;
					}
					if (this.connections[num2] != conn && this.connections[num2].receiver == null)
					{
						this.connections[num2].ScanDelay = num;
						this.replaceConnection(conn, this.connections[num2], concurrencyIndex);
						num = 0L;
						break;
					}
					num2++;
				}
				if (num != 0L)
				{
					conn.ScanDelay = num;
				}
			}
			else if (conn.receiver != null)
			{
				this.replaceConnection(conn, null, concurrencyIndex);
			}
			this.clearConnectionsBusy();
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0009A248 File Offset: 0x00098448
		private void replaceConnection(Connection oConn, Connection nConn, int index)
		{
			this.stopConnection(oConn);
			this.joinConnection(oConn, false);
			if (nConn != null)
			{
				this.concurrencies[index].assign(nConn);
				this.startConnection(nConn);
				return;
			}
			this.concurrencies[index].clear();
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0009A280 File Offset: 0x00098480
		private void stopConnection(Connection conn)
		{
			conn.ClientShutdown = true;
			conn.receiver.shutdown();
			conn.sender.shutdown();
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0009A2A0 File Offset: 0x000984A0
		private void joinConnection(Connection conn, bool shutdownState)
		{
			lock (this.staleReceivers)
			{
				this.staleReceivers.Add(conn.receiver);
			}
			bool flag2;
			do
			{
				try
				{
					conn.sender.Join();
					flag2 = true;
				}
				catch (Exception ex)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
						{
							"NodeList::joinConnection() failed. -" + ex.Message
						});
					}
					flag2 = false;
				}
			}
			while (!flag2);
			conn.ClientSender = null;
			conn.ClientReceiver = null;
			conn.ClientShutdown = shutdownState;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0009A354 File Offset: 0x00098554
		protected internal virtual void establishedConnection(Connection conn)
		{
			this.setConnectionsBusy();
			int concurrencyIndex = conn.ConcurrencyIndex;
			this.concurrencies[concurrencyIndex].connected();
			if (this.failedOver_Renamed_Field)
			{
				this.ons.nodeListFallBack(this, concurrencyIndex);
			}
			this.clearConnectionsBusy();
			this.joinStaleRecievers(conn.receiver);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0009A3A4 File Offset: 0x000985A4
		protected internal virtual void failedOver(NodeList failOver, int cIndex)
		{
			this.failedOver_Renamed_Field = true;
			this.failOverList = failOver;
			for (int i = 0; i < this.concurrency; i++)
			{
				if (i != cIndex)
				{
					int assignedIndex = this.concurrencies[i].assignedIndex;
					if (this.connections[assignedIndex].receiver != null)
					{
						this.replaceConnection(this.connections[assignedIndex], null, i);
					}
				}
			}
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0009A404 File Offset: 0x00098604
		protected internal virtual void fallBack(int cIndex)
		{
			this.failedOver_Renamed_Field = false;
			this.failOverList = null;
			int i = 0;
			for (int j = 0; j < this.concurrency; j++)
			{
				if (j != cIndex)
				{
					while (i < this.connections.Length)
					{
						if (this.connections[i].receiver == null)
						{
							this.concurrencies[j].assign(this.connections[i]);
							this.startConnection(this.connections[i]);
							break;
						}
						i++;
					}
				}
			}
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0009A47C File Offset: 0x0009867C
		private void joinStaleRecievers(ReceiverThread caller)
		{
			ReceiverThread receiverThread = null;
			lock (this.staleReceivers)
			{
				try
				{
					if (this.staleReceivers.Count > 0)
					{
						receiverThread = this.staleReceivers[0];
						this.staleReceivers.RemoveAt(0);
					}
					while (receiverThread != null)
					{
						if (receiverThread != caller)
						{
							bool flag2;
							do
							{
								try
								{
									receiverThread.Join();
									flag2 = true;
								}
								catch (ThreadInterruptedException ex)
								{
									if (ProviderConfig.m_bTraceLevelPrivate)
									{
										Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
										{
											"NodeList::joinStaleReceivers() failed. -" + ex.Message
										});
									}
									flag2 = false;
								}
							}
							while (!flag2);
						}
						receiverThread = this.staleReceivers[0];
						this.staleReceivers.RemoveAt(0);
					}
				}
				catch (ArgumentOutOfRangeException ex2)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
						{
							"NodeList::joinStaleReceivers() failed. -" + ex2.Message
						});
					}
				}
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0009A594 File Offset: 0x00098794
		protected internal virtual void stop()
		{
			this.setConnectionsBusy();
			for (int i = 0; i < this.concurrency; i++)
			{
				int assignedIndex = this.concurrencies[i].assignedIndex;
				if (assignedIndex != -1)
				{
					this.stopConnection(this.connections[assignedIndex]);
				}
			}
			this.clearConnectionsBusy();
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0009A5E0 File Offset: 0x000987E0
		protected internal virtual void join()
		{
			this.setConnectionsBusy();
			for (int i = 0; i < this.concurrency; i++)
			{
				int assignedIndex = this.concurrencies[i].assignedIndex;
				if (assignedIndex != -1)
				{
					this.joinConnection(this.connections[assignedIndex], true);
				}
				this.concurrencies[i].clear();
			}
			this.clearConnectionsBusy();
			this.joinStaleRecievers(null);
			lock (this.publishLock)
			{
				while (this.publishQueue.Count != 0)
				{
					this.publishQueue.RemoveAt(0);
				}
			}
			lock (this.notifications)
			{
				this.notifications.Clear();
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0009A6C0 File Offset: 0x000988C0
		private void setConnectionsBusy()
		{
			bool flag = true;
			do
			{
				lock (this.connLock)
				{
					if (!this.connCheck)
					{
						this.connCheck = true;
						flag = false;
					}
					if (flag)
					{
						try
						{
							Monitor.Wait(this.connLock);
						}
						catch (Exception ex)
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
								{
									"NodeList::setConnectionBusy() failed. -" + ex.Message
								});
							}
						}
					}
				}
			}
			while (flag);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0009A764 File Offset: 0x00098964
		private void clearConnectionsBusy()
		{
			lock (this.connLock)
			{
				this.connCheck = false;
				Monitor.Pulse(this.connLock);
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0009A7B0 File Offset: 0x000989B0
		protected internal virtual void deliver(Notification e)
		{
			if (!this.ons.localConn && this.isDupNotification(e))
			{
				return;
			}
			this.ons.deliver(e);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0009A7D8 File Offset: 0x000989D8
		private bool isDupNotification(Notification e)
		{
			if (e.id_Renamed_Field == null || e.instanceName_Renamed_Field == null)
			{
				return false;
			}
			long num = (DateTime.Now.Ticks - 621355968000000000L) / 10000L;
			lock (this.notifications)
			{
				if (num - this.lastCleanupTime >= this.ons.notificationtimeout)
				{
					this.lastCleanupTime = num;
					this.cleanupNotificationTable(num);
				}
				string key = e.instanceName_Renamed_Field + e.id_Renamed_Field;
				NotificationInformation notificationInformation = (NotificationInformation)this.notifications[key];
				if (notificationInformation == null)
				{
					notificationInformation = new NotificationInformation(num);
					notificationInformation.addCount();
					this.notifications[key] = notificationInformation;
					return false;
				}
				notificationInformation.addCount();
				this.cleanupNotificationTable(key, notificationInformation);
			}
			return true;
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0009A8C4 File Offset: 0x00098AC4
		private void cleanupNotificationTable(string key, NotificationInformation elem)
		{
			if (elem.Count >= this.concurrency)
			{
				this.notifications.Remove(key);
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0009A8E0 File Offset: 0x00098AE0
		private void cleanupNotificationTable(long currentTime)
		{
			List<string> list = new List<string>();
			lock (this.notifications)
			{
				foreach (object obj2 in this.notifications)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
					NotificationInformation notificationInformation = (NotificationInformation)dictionaryEntry.Value;
					if (currentTime - notificationInformation.Timestamp > this.ons.notificationtimeout)
					{
						list.Add((string)dictionaryEntry.Key);
					}
				}
				foreach (string key in list)
				{
					this.notifications.Remove(key);
				}
			}
		}

		// Token: 0x04001127 RID: 4391
		private const long ConnectionDelay = 30000L;

		// Token: 0x04001128 RID: 4392
		private const char ONS_HP_SEPARATOR = ',';

		// Token: 0x04001129 RID: 4393
		private object publishLock;

		// Token: 0x0400112A RID: 4394
		private List<Notification> publishQueue;

		// Token: 0x0400112B RID: 4395
		private object connLock;

		// Token: 0x0400112C RID: 4396
		private Connection[] connections;

		// Token: 0x0400112D RID: 4397
		private bool connCheck;

		// Token: 0x0400112E RID: 4398
		private List<ReceiverThread> staleReceivers;

		// Token: 0x0400112F RID: 4399
		private int concurrency;

		// Token: 0x04001130 RID: 4400
		private int senderIndex;

		// Token: 0x04001131 RID: 4401
		private SenderThread senderThread;

		// Token: 0x04001132 RID: 4402
		private string id;

		// Token: 0x04001133 RID: 4403
		private bool failedOver_Renamed_Field;

		// Token: 0x04001134 RID: 4404
		private bool shutdown;

		// Token: 0x04001135 RID: 4405
		private Concurrency[] concurrencies;

		// Token: 0x04001136 RID: 4406
		private Hashtable notifications;

		// Token: 0x04001137 RID: 4407
		private long lastCleanupTime;

		// Token: 0x04001138 RID: 4408
		protected internal ONS ons;

		// Token: 0x04001139 RID: 4409
		protected internal NodeList failOverList;

		// Token: 0x0400113A RID: 4410
		protected internal bool active;

		// Token: 0x0400113B RID: 4411
		protected internal long shutdownTimeout;
	}
}
