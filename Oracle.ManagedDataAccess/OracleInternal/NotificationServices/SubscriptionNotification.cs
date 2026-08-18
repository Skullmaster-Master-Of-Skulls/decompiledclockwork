using System;
using System.Text;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000193 RID: 403
	internal class SubscriptionNotification
	{
		// Token: 0x06000F50 RID: 3920 RVA: 0x0009FFC4 File Offset: 0x0009E1C4
		protected internal SubscriptionNotification(int sid, string subs, bool w)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.subsid = sid;
			this.subscription = subs;
			this.waiter = w;
			this.ex = null;
			this.success = false;
			this.replyrecvd = false;
			if (this.waiter)
			{
				this.lock_Renamed = new object();
			}
			else
			{
				this.lock_Renamed = null;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x000A0050 File Offset: 0x0009E250
		protected internal virtual void waitForReply(long timeout)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				lock (this.lock_Renamed)
				{
					if (!this.replyrecvd)
					{
						if (timeout <= 0L)
						{
							Monitor.Wait(this.lock_Renamed);
						}
						else
						{
							Monitor.Wait(this.lock_Renamed, TimeSpan.FromMilliseconds((double)timeout));
						}
					}
				}
			}
			catch (Exception)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, this.ex, null);
			}
			try
			{
				if (timeout >= 0L && !this.replyrecvd)
				{
					throw new SubscriptionException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ONS_SUBSCR_TIMED_OUT, new string[]
					{
						timeout.ToString()
					}));
				}
				if (!this.success)
				{
					if (this.ex != null)
					{
						throw this.ex;
					}
					throw new SubscriptionException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.ONS_SUBSCR_FAILED, new string[0]));
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

		// Token: 0x06000F52 RID: 3922 RVA: 0x000A0194 File Offset: 0x0009E394
		protected internal virtual void wakeup(bool s, SubscriptionException sexcept)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.success = s;
				this.ex = sexcept;
				if (this.waiter)
				{
					lock (this.lock_Renamed)
					{
						this.replyrecvd = true;
						Monitor.PulseAll(this.lock_Renamed);
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

		// Token: 0x06000F53 RID: 3923 RVA: 0x000A0254 File Offset: 0x0009E454
		protected internal virtual void send(OutputBuffer obuf)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				obuf.putBytes(SubscriptionNotification.subsmessageline, SubscriptionNotification.subsmessageline.Length);
				obuf.putBytes(SubscriptionNotification.contentlengthline, SubscriptionNotification.contentlengthline.Length);
				obuf.putBytes(SubscriptionNotification.subsidheader, SubscriptionNotification.subsidheader.Length);
				string text = Convert.ToString(this.subsid);
				obuf.putBytes(SupportClass.ToSByteArray(SupportClass.ToByteArray(text)), text.Length);
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(SubscriptionNotification.subsheader, SubscriptionNotification.subsheader.Length);
				if (this.subscription != null && this.subscription.Length > 0)
				{
					obuf.putBytes(SupportClass.ToSByteArray(SupportClass.ToByteArray(this.subscription)), this.subscription.Length);
				}
				obuf.putBytes(Notification.crlf, 2);
				obuf.putBytes(Notification.crlf, 2);
				obuf.flush();
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

		// Token: 0x06000F54 RID: 3924 RVA: 0x000A0390 File Offset: 0x0009E590
		static SubscriptionNotification()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				SubscriptionNotification.subsmessageline = new sbyte[26];
				Array.Copy(SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("POST /subscribe HTTP/1.1").ToString())), 0, SubscriptionNotification.subsmessageline, 0, 24);
				Array.Copy(Notification.crlf, 0, SubscriptionNotification.subsmessageline, 24, 2);
				SubscriptionNotification.subsidheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("SubscriberID: ").ToString()));
				SubscriptionNotification.subsheader = SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("Subscription: ").ToString()));
				SubscriptionNotification.contentlengthline = new sbyte[19];
				Array.Copy(SupportClass.ToSByteArray(SupportClass.ToByteArray(new StringBuilder("Content-Length: 0").ToString())), 0, SubscriptionNotification.contentlengthline, 0, 17);
				Array.Copy(Notification.crlf, 0, SubscriptionNotification.contentlengthline, 17, 2);
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

		// Token: 0x040011ED RID: 4589
		protected internal int subsid;

		// Token: 0x040011EE RID: 4590
		protected internal string subscription;

		// Token: 0x040011EF RID: 4591
		protected internal object lock_Renamed;

		// Token: 0x040011F0 RID: 4592
		protected internal bool waiter;

		// Token: 0x040011F1 RID: 4593
		protected internal SubscriptionException ex;

		// Token: 0x040011F2 RID: 4594
		protected internal bool success;

		// Token: 0x040011F3 RID: 4595
		private bool replyrecvd;

		// Token: 0x040011F4 RID: 4596
		private static sbyte[] subsmessageline;

		// Token: 0x040011F5 RID: 4597
		private static sbyte[] subsidheader;

		// Token: 0x040011F6 RID: 4598
		private static sbyte[] subsheader;

		// Token: 0x040011F7 RID: 4599
		private static sbyte[] contentlengthline;
	}
}
