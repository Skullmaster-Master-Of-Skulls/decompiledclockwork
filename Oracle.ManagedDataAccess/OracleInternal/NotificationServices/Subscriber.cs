using System;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000191 RID: 401
	internal class Subscriber
	{
		// Token: 0x170002C2 RID: 706
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x0009FA58 File Offset: 0x0009DC58
		protected internal virtual int ID
		{
			set
			{
				this.id_Renamed_Field = value;
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0009FA64 File Offset: 0x0009DC64
		internal Subscriber(ONS o, string s, string c, long timeout)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.oems = o;
				this.realStartup(s, c, timeout);
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

		// Token: 0x06000F47 RID: 3911 RVA: 0x0009FAEC File Offset: 0x0009DCEC
		private void realStartup(string s, string c, long timeout)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.subscription_Renamed_Field = s;
				this.pub = new Publisher(this.oems, c);
				this.component_Renamed_Field = c;
				this.id_Renamed_Field = -1;
				this.queue = new NotificationQueue(this.oems);
				this.oems.addSubscriber(this, timeout);
				this.cb = null;
				this.cbmode = 0;
				this.cblock = new object();
				this.numcbthreads = 0;
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

		// Token: 0x06000F48 RID: 3912 RVA: 0x0009FBC0 File Offset: 0x0009DDC0
		public virtual Notification receive(bool blocking)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			Notification result;
			try
			{
				if (!this.oems.shutdown_Renamed_Field)
				{
					result = (Notification)this.queue.dequeue(blocking);
				}
				else
				{
					result = null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				result = null;
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

		// Token: 0x06000F49 RID: 3913 RVA: 0x0009FC54 File Offset: 0x0009DE54
		public virtual void close()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				if (this.cb != null)
				{
					this.cancel_callback();
				}
				this.oems.removeSubscriber(this.id_Renamed_Field);
				this.pub.close();
				this.queue.close();
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

		// Token: 0x06000F4A RID: 3914 RVA: 0x0009FCF8 File Offset: 0x0009DEF8
		public virtual string subscription()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
			return this.subscription_Renamed_Field;
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0009FD30 File Offset: 0x0009DF30
		public virtual int id()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
			return this.id_Renamed_Field;
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0009FD68 File Offset: 0x0009DF68
		protected internal virtual void deliver(Notification n)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				if (this.cb != null && this.cbmode == 2)
				{
					CallBackThread callBackThread = new CallBackThread(this, this.cb, n);
					lock (this.cblock)
					{
						this.numcbthreads--;
					}
					callBackThread.Start();
				}
				else
				{
					this.queue.enqueue(n);
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

		// Token: 0x06000F4D RID: 3917 RVA: 0x0009FE40 File Offset: 0x0009E040
		public virtual void cancel_callback()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.cb = null;
				int num;
				lock (this.cblock)
				{
					num = this.numcbthreads;
					goto IL_B6;
				}
				try
				{
					IL_45:
					SupportClass.ThreadClass.Current();
					Thread.Sleep(new TimeSpan(10000000L));
				}
				catch (Exception ex)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
						{
							"CallBackThread::threadpercb() failed. -" + ex.Message
						});
					}
				}
				lock (this.cblock)
				{
					num = this.numcbthreads;
				}
				IL_B6:
				if (num > 0)
				{
					goto IL_45;
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex2, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x040011E1 RID: 4577
		public const int CBMODE_ONETHREAD = 1;

		// Token: 0x040011E2 RID: 4578
		public const int CBMODE_THREADPERCB = 2;

		// Token: 0x040011E3 RID: 4579
		private string subscription_Renamed_Field;

		// Token: 0x040011E4 RID: 4580
		private string component_Renamed_Field;

		// Token: 0x040011E5 RID: 4581
		private Publisher pub;

		// Token: 0x040011E6 RID: 4582
		private ONS oems;

		// Token: 0x040011E7 RID: 4583
		private int id_Renamed_Field;

		// Token: 0x040011E8 RID: 4584
		protected internal NotificationQueue queue;

		// Token: 0x040011E9 RID: 4585
		private CallBack cb;

		// Token: 0x040011EA RID: 4586
		private int cbmode;

		// Token: 0x040011EB RID: 4587
		private object cblock;

		// Token: 0x040011EC RID: 4588
		private int numcbthreads;
	}
}
