using System;
using System.IO;
using System.Text;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000190 RID: 400
	internal class SenderThread : SupportClass.ThreadClass
	{
		// Token: 0x06000F3C RID: 3900 RVA: 0x0009F24C File Offset: 0x0009D44C
		protected internal SenderThread(NodeList list, Connection co)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.lock_Renamed = new object();
				this.shutdown_Renamed_Field = false;
				this.quiescent_Renamed_Field = false;
				this.waitQ = false;
				base.IsBackground = true;
				this.nodeList = list;
				this.oems = list.ons;
				this.connection = co;
				this.q = new NotificationQueue(this.oems);
				this.connection.ClientSender = this;
				this.id_Renamed_Field = new StringBuilder("SenderThread[" + this.connection.Id + "]").ToString();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0009F348 File Offset: 0x0009D548
		public override void Run()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.runRemote();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0009F3BC File Offset: 0x0009D5BC
		private void runRemote()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				while (!this.shutdown_Renamed_Field && !this.connection.shutdown)
				{
					if (this.quiescent_Renamed_Field)
					{
						while (this.quiescent_Renamed_Field)
						{
							if (this.shutdown_Renamed_Field)
							{
								break;
							}
							lock (this.lock_Renamed)
							{
								try
								{
									Monitor.Wait(this.lock_Renamed, TimeSpan.FromMilliseconds(1000.0));
								}
								catch (Exception ex)
								{
									OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
								}
							}
						}
					}
					else
					{
						SendElement sendElement = (SendElement)this.q.dequeue(false);
						if (sendElement == null)
						{
							Notification firstPublished = this.nodeList.getFirstPublished(this);
							if (firstPublished == null)
							{
								lock (this.lock_Renamed)
								{
									this.waitQ = true;
								}
								sendElement = (SendElement)this.q.dequeue(true);
								lock (this.lock_Renamed)
								{
									this.waitQ = false;
								}
								if (sendElement == null)
								{
									continue;
								}
								if (this.shutdown_Renamed_Field)
								{
									continue;
								}
							}
							else
							{
								sendElement = new SendElement(firstPublished);
							}
						}
						ONSTcpClient clientSocket = this.connection.getClientSocket(false);
						if (clientSocket == null)
						{
							this.nodeList.clearPublishedSender(this);
							sendElement = null;
							clientSocket = this.connection.getClientSocket(true);
						}
						else
						{
							try
							{
								if (sendElement.e != null)
								{
									sendElement.e.send(new OutputBuffer(clientSocket.GetStream()), this.oems, this.connection);
									this.nodeList.removeFirstPublished(sendElement.e, this);
								}
								else if (sendElement.s != null)
								{
									sendElement.s.send(new OutputBuffer(clientSocket.GetStream()));
								}
							}
							catch (IOException ex2)
							{
								OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex2, null);
								this.nodeList.clearPublishedSender(this);
							}
						}
					}
				}
				this.nodeList.clearPublishedSender(this);
			}
			catch (Exception ex3)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex3, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0009F6A8 File Offset: 0x0009D8A8
		protected internal virtual void send(SubscriptionNotification e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				SendElement e2 = new SendElement(e);
				this.q.enqueue(e2);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0009F72C File Offset: 0x0009D92C
		protected internal virtual void send(SubscriptionNotification e, int p)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				SendElement e2 = new SendElement(e);
				this.q.push(e2, p);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0009F7B0 File Offset: 0x0009D9B0
		protected internal virtual void shutdown()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.shutdown_Renamed_Field = true;
				this.quiescent_Renamed_Field = false;
				try
				{
					this.q.drain_and_close(this.nodeList.shutdownTimeout);
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				}
				this.wakeThread();
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex2, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0009F868 File Offset: 0x0009DA68
		protected internal virtual void quiescent(bool status)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.quiescent_Renamed_Field = status;
				if (!this.quiescent_Renamed_Field)
				{
					lock (this.lock_Renamed)
					{
						Monitor.Pulse(this.lock_Renamed);
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0009F91C File Offset: 0x0009DB1C
		protected internal virtual void wakeThread()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				lock (this.lock_Renamed)
				{
					if (this.waitQ)
					{
						this.q.wake();
					}
					Monitor.Pulse(this.lock_Renamed);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0009F9D4 File Offset: 0x0009DBD4
		protected internal virtual void flushSenderQueue()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				while ((SendElement)this.q.dequeue(false) != null)
				{
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x040011D8 RID: 4568
		private ONS oems;

		// Token: 0x040011D9 RID: 4569
		private NodeList nodeList;

		// Token: 0x040011DA RID: 4570
		private Connection connection;

		// Token: 0x040011DB RID: 4571
		private bool shutdown_Renamed_Field;

		// Token: 0x040011DC RID: 4572
		private bool quiescent_Renamed_Field;

		// Token: 0x040011DD RID: 4573
		private bool waitQ;

		// Token: 0x040011DE RID: 4574
		private NotificationQueue q;

		// Token: 0x040011DF RID: 4575
		private string id_Renamed_Field;

		// Token: 0x040011E0 RID: 4576
		private object lock_Renamed;
	}
}
