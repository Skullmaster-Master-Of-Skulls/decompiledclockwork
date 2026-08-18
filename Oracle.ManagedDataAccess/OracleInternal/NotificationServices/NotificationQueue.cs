using System;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000186 RID: 390
	internal class NotificationQueue
	{
		// Token: 0x06000EFE RID: 3838 RVA: 0x0009B848 File Offset: 0x00099A48
		internal NotificationQueue(ONS myONS)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.ons = myONS;
			this.head = null;
			this.tail = null;
			this.waiters_Renamed_Field = 0;
			this.count_Renamed_Field = 0;
			this.lock_Renamed = new object();
			this.closed = false;
			this.closelock = null;
			this.closewaiters = 0;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0009B8D0 File Offset: 0x00099AD0
		protected internal virtual void enqueue(object e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				QueueElement next = new QueueElement(e);
				lock (this.lock_Renamed)
				{
					if (!this.closed && !this.closing)
					{
						if (this.head == null)
						{
							this.head = next;
						}
						else if (this.tail == null)
						{
							this.head.next = (this.tail = next);
						}
						else
						{
							this.tail.next = next;
							this.tail = next;
						}
						if (this.waiters_Renamed_Field > 0)
						{
							Monitor.Pulse(this.lock_Renamed);
							this.waiters_Renamed_Field--;
						}
						this.count_Renamed_Field++;
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

		// Token: 0x06000F00 RID: 3840 RVA: 0x0009B9F4 File Offset: 0x00099BF4
		protected internal virtual void push(object e, int p)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				QueueElement queueElement = new QueueElement(e, p);
				lock (this.lock_Renamed)
				{
					if (!this.closed && !this.closing)
					{
						if (this.head == null)
						{
							this.head = queueElement;
						}
						else if (p <= this.head.priority)
						{
							if (this.tail == null)
							{
								this.tail = this.head;
							}
							queueElement.next = this.head;
							this.head = queueElement;
						}
						else
						{
							QueueElement queueElement2 = this.head;
							QueueElement next = this.head.next;
							while (next != null && p > next.priority)
							{
								queueElement2 = next;
								next = next.next;
							}
							if (next != null)
							{
								queueElement2.next = queueElement;
								queueElement.next = next;
							}
							else
							{
								queueElement2.next = queueElement;
								this.tail = queueElement;
							}
						}
						if (this.waiters_Renamed_Field > 0)
						{
							Monitor.Pulse(this.lock_Renamed);
							this.waiters_Renamed_Field--;
						}
						this.count_Renamed_Field++;
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

		// Token: 0x06000F01 RID: 3841 RVA: 0x0009BB98 File Offset: 0x00099D98
		protected internal virtual void close()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			lock (this.lock_Renamed)
			{
				this.closed = true;
				if (this.waiters_Renamed_Field > 0)
				{
					Monitor.PulseAll(this.lock_Renamed);
				}
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0009BC20 File Offset: 0x00099E20
		protected internal virtual object dequeue(bool blocking)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			object result;
			try
			{
				result = this.internalDequeue(blocking, -1L);
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
			return result;
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0009BC98 File Offset: 0x00099E98
		private object internalDequeue(bool blocking, long wait)
		{
			object obj = null;
			bool flag = false;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			object result;
			try
			{
				lock (this.lock_Renamed)
				{
					flag = this.closing;
					if (!this.closed)
					{
						if (blocking && this.head == null && !this.closing)
						{
							this.waiters_Renamed_Field++;
							try
							{
								if (wait != -1L)
								{
									Monitor.Wait(this.lock_Renamed, TimeSpan.FromMilliseconds((double)wait));
								}
								else
								{
									Monitor.Wait(this.lock_Renamed);
								}
							}
							catch (Exception)
							{
							}
						}
						if (this.head != null)
						{
							obj = this.head.obj;
							if (this.head.next == this.tail)
							{
								this.tail = null;
							}
							this.head = this.head.next;
							this.count_Renamed_Field--;
						}
					}
				}
				if (obj == null && flag)
				{
					lock (this.closelock)
					{
						if (this.closewaiters > 0)
						{
							Monitor.PulseAll(this.closelock);
						}
					}
				}
				result = obj;
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
			return result;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0009BE74 File Offset: 0x0009A074
		protected internal virtual void wake()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			lock (this.lock_Renamed)
			{
				if (this.waiters_Renamed_Field > 0)
				{
					Monitor.PulseAll(this.lock_Renamed);
				}
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0009BEF4 File Offset: 0x0009A0F4
		protected internal virtual void drain_and_close(long timeout)
		{
			bool flag = false;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				lock (this.lock_Renamed)
				{
					this.closing = true;
					this.closelock = new object();
					if (this.count_Renamed_Field > 0)
					{
						flag = true;
					}
				}
				if (flag)
				{
					lock (this.closelock)
					{
						lock (this.lock_Renamed)
						{
							if (this.count_Renamed_Field == 0)
							{
								flag = false;
							}
						}
						if (flag)
						{
							try
							{
								this.closewaiters++;
								Monitor.Wait(this.closelock, TimeSpan.FromMilliseconds((double)timeout));
							}
							catch (Exception ex)
							{
								OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
							}
							finally
							{
								this.closewaiters--;
							}
						}
					}
				}
				this.close();
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

		// Token: 0x0400116C RID: 4460
		protected internal const int DEFAULT_PRIORITY = 10;

		// Token: 0x0400116D RID: 4461
		protected internal const int HIGH_PRIORITY = 1;

		// Token: 0x0400116E RID: 4462
		private ONS ons;

		// Token: 0x0400116F RID: 4463
		private QueueElement head;

		// Token: 0x04001170 RID: 4464
		private QueueElement tail;

		// Token: 0x04001171 RID: 4465
		private object lock_Renamed;

		// Token: 0x04001172 RID: 4466
		private object closelock;

		// Token: 0x04001173 RID: 4467
		private int waiters_Renamed_Field;

		// Token: 0x04001174 RID: 4468
		private int count_Renamed_Field;

		// Token: 0x04001175 RID: 4469
		private bool closed;

		// Token: 0x04001176 RID: 4470
		private bool closing;

		// Token: 0x04001177 RID: 4471
		private int closewaiters;

		// Token: 0x04001178 RID: 4472
		protected internal static readonly int LOW_PRIORITY = 10;
	}
}
