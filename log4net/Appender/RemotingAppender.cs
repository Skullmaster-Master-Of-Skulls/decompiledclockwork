using System;
using System.Collections;
using System.Security;
using System.Threading;
using log4net.Core;

namespace log4net.Appender
{
	// Token: 0x02000039 RID: 57
	public class RemotingAppender : BufferingAppenderSkeleton
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00006A0D File Offset: 0x00004C0D
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00006A15 File Offset: 0x00004C15
		public string Sink
		{
			get
			{
				return this.m_sinkUrl;
			}
			set
			{
				this.m_sinkUrl = value;
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006A20 File Offset: 0x00004C20
		[SecuritySafeCritical]
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			IDictionary dictionary = new Hashtable();
			dictionary["typeFilterLevel"] = "Full";
			this.m_sinkObj = (RemotingAppender.IRemoteLoggingSink)Activator.GetObject(typeof(RemotingAppender.IRemoteLoggingSink), this.m_sinkUrl, dictionary);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006A6C File Offset: 0x00004C6C
		protected override void SendBuffer(LoggingEvent[] events)
		{
			this.BeginAsyncSend();
			if (!ThreadPool.QueueUserWorkItem(new WaitCallback(this.SendBufferCallback), events))
			{
				this.EndAsyncSend();
				this.ErrorHandler.Error("RemotingAppender [" + base.Name + "] failed to ThreadPool.QueueUserWorkItem logging events in SendBuffer.");
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006AB9 File Offset: 0x00004CB9
		protected override void OnClose()
		{
			base.OnClose();
			if (!this.m_workQueueEmptyEvent.WaitOne(30000, false))
			{
				this.ErrorHandler.Error("RemotingAppender [" + base.Name + "] failed to send all queued events before close, in OnClose.");
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006AF4 File Offset: 0x00004CF4
		public override bool Flush(int millisecondsTimeout)
		{
			base.Flush();
			return this.m_workQueueEmptyEvent.WaitOne(millisecondsTimeout, false);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006B09 File Offset: 0x00004D09
		private void BeginAsyncSend()
		{
			this.m_workQueueEmptyEvent.Reset();
			Interlocked.Increment(ref this.m_queuedCallbackCount);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006B23 File Offset: 0x00004D23
		private void EndAsyncSend()
		{
			if (Interlocked.Decrement(ref this.m_queuedCallbackCount) <= 0)
			{
				this.m_workQueueEmptyEvent.Set();
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006B40 File Offset: 0x00004D40
		private void SendBufferCallback(object state)
		{
			try
			{
				LoggingEvent[] events = (LoggingEvent[])state;
				this.m_sinkObj.LogEvents(events);
			}
			catch (Exception e)
			{
				this.ErrorHandler.Error("Failed in SendBufferCallback", e);
			}
			finally
			{
				this.EndAsyncSend();
			}
		}

		// Token: 0x040000FC RID: 252
		private string m_sinkUrl;

		// Token: 0x040000FD RID: 253
		private RemotingAppender.IRemoteLoggingSink m_sinkObj;

		// Token: 0x040000FE RID: 254
		private int m_queuedCallbackCount;

		// Token: 0x040000FF RID: 255
		private ManualResetEvent m_workQueueEmptyEvent = new ManualResetEvent(true);

		// Token: 0x0200003A RID: 58
		public interface IRemoteLoggingSink
		{
			// Token: 0x060001FF RID: 511
			void LogEvents(LoggingEvent[] events);
		}
	}
}
