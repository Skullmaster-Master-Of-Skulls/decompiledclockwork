using System;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002B8 RID: 696
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogWatcher : IDisposable
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06001951 RID: 6481 RVA: 0x0005C4A8 File Offset: 0x0005A6A8
		// (remove) Token: 0x06001952 RID: 6482 RVA: 0x0005C4E0 File Offset: 0x0005A6E0
		public event EventHandler<EventRecordWrittenEventArgs> EventRecordWritten;

		// Token: 0x06001953 RID: 6483 RVA: 0x0005C515 File Offset: 0x0005A715
		public EventLogWatcher(string path) : this(new EventLogQuery(path, PathType.LogName), null, false)
		{
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0005C526 File Offset: 0x0005A726
		public EventLogWatcher(EventLogQuery eventQuery) : this(eventQuery, null, false)
		{
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0005C531 File Offset: 0x0005A731
		public EventLogWatcher(EventLogQuery eventQuery, EventBookmark bookmark) : this(eventQuery, bookmark, false)
		{
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0005C53C File Offset: 0x0005A73C
		public EventLogWatcher(EventLogQuery eventQuery, EventBookmark bookmark, bool readExistingEvents)
		{
			if (eventQuery == null)
			{
				throw new ArgumentNullException("eventQuery");
			}
			if (bookmark != null)
			{
				readExistingEvents = false;
			}
			this.eventQuery = eventQuery;
			this.readExistingEvents = readExistingEvents;
			if (this.eventQuery.ReverseDirection)
			{
				throw new InvalidOperationException();
			}
			this.eventsBuffer = new IntPtr[64];
			this.cachedMetadataInformation = new ProviderMetadataCachedInformation(eventQuery.Session, null, 50);
			this.bookmark = bookmark;
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x0005C5AC File Offset: 0x0005A7AC
		// (set) Token: 0x06001958 RID: 6488 RVA: 0x0005C5B4 File Offset: 0x0005A7B4
		public bool Enabled
		{
			get
			{
				return this.isSubscribing;
			}
			set
			{
				if (value && !this.isSubscribing)
				{
					this.StartSubscribing();
					return;
				}
				if (!value && this.isSubscribing)
				{
					this.StopSubscribing();
				}
			}
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0005C5DC File Offset: 0x0005A7DC
		[SecuritySafeCritical]
		internal void StopSubscribing()
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			this.isSubscribing = false;
			if (this.registeredWaitHandle != null)
			{
				this.registeredWaitHandle.Unregister(this.unregisterDoneHandle);
				if (this.callbackThreadId != Thread.CurrentThread.ManagedThreadId && this.unregisterDoneHandle != null)
				{
					this.unregisterDoneHandle.WaitOne();
				}
				this.registeredWaitHandle = null;
			}
			if (this.unregisterDoneHandle != null)
			{
				this.unregisterDoneHandle.Close();
				this.unregisterDoneHandle = null;
			}
			if (this.subscriptionWaitHandle != null)
			{
				this.subscriptionWaitHandle.Close();
				this.subscriptionWaitHandle = null;
			}
			for (int i = 0; i < this.numEventsInBuffer; i++)
			{
				if (this.eventsBuffer[i] != IntPtr.Zero)
				{
					NativeWrapper.EvtClose(this.eventsBuffer[i]);
					this.eventsBuffer[i] = IntPtr.Zero;
				}
			}
			this.numEventsInBuffer = 0;
			if (this.handle != null && !this.handle.IsInvalid)
			{
				this.handle.Dispose();
			}
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0005C6DC File Offset: 0x0005A8DC
		[SecuritySafeCritical]
		internal void StartSubscribing()
		{
			if (this.isSubscribing)
			{
				throw new InvalidOperationException();
			}
			int num = 0;
			if (this.bookmark != null)
			{
				num |= 3;
			}
			else if (this.readExistingEvents)
			{
				num |= 2;
			}
			else
			{
				num |= 1;
			}
			if (this.eventQuery.TolerateQueryErrors)
			{
				num |= 4096;
			}
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			this.callbackThreadId = -1;
			this.unregisterDoneHandle = new AutoResetEvent(false);
			this.subscriptionWaitHandle = new AutoResetEvent(false);
			EventLogHandle bookmarkHandleFromBookmark = EventLogRecord.GetBookmarkHandleFromBookmark(this.bookmark);
			using (bookmarkHandleFromBookmark)
			{
				this.handle = NativeWrapper.EvtSubscribe(this.eventQuery.Session.Handle, this.subscriptionWaitHandle.SafeWaitHandle, this.eventQuery.Path, this.eventQuery.Query, bookmarkHandleFromBookmark, IntPtr.Zero, IntPtr.Zero, num);
			}
			this.isSubscribing = true;
			this.RequestEvents();
			this.registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(this.subscriptionWaitHandle, new WaitOrTimerCallback(this.SubscribedEventsAvailableCallback), null, -1, false);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0005C7F8 File Offset: 0x0005A9F8
		internal void SubscribedEventsAvailableCallback(object state, bool timedOut)
		{
			this.callbackThreadId = Thread.CurrentThread.ManagedThreadId;
			try
			{
				this.RequestEvents();
			}
			finally
			{
				this.callbackThreadId = -1;
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0005C838 File Offset: 0x0005AA38
		[SecuritySafeCritical]
		private void RequestEvents()
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			this.asyncException = null;
			bool flag = false;
			while (this.isSubscribing)
			{
				try
				{
					flag = NativeWrapper.EvtNext(this.handle, this.eventsBuffer.Length, this.eventsBuffer, 0, 0, ref this.numEventsInBuffer);
					if (!flag)
					{
						break;
					}
				}
				catch (Exception value)
				{
					this.asyncException = new EventLogException();
					this.asyncException.Data.Add("RealException", value);
				}
				this.HandleEventsRequestCompletion();
				if (!flag)
				{
					break;
				}
			}
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0005C8C8 File Offset: 0x0005AAC8
		private void IssueCallback(EventRecordWrittenEventArgs eventArgs)
		{
			if (this.EventRecordWritten != null)
			{
				this.EventRecordWritten(this, eventArgs);
			}
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0005C8E0 File Offset: 0x0005AAE0
		[SecurityCritical]
		private void HandleEventsRequestCompletion()
		{
			if (this.asyncException != null)
			{
				EventRecordWrittenEventArgs eventArgs = new EventRecordWrittenEventArgs(this.asyncException.Data["RealException"] as Exception);
				this.IssueCallback(eventArgs);
			}
			int num = 0;
			while (num < this.numEventsInBuffer && this.isSubscribing)
			{
				EventLogRecord record = new EventLogRecord(new EventLogHandle(this.eventsBuffer[num], true), this.eventQuery.Session, this.cachedMetadataInformation);
				EventRecordWrittenEventArgs eventArgs2 = new EventRecordWrittenEventArgs(record);
				this.eventsBuffer[num] = IntPtr.Zero;
				this.IssueCallback(eventArgs2);
				num++;
			}
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0005C975 File Offset: 0x0005AB75
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0005C984 File Offset: 0x0005AB84
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.StopSubscribing();
				return;
			}
			for (int i = 0; i < this.numEventsInBuffer; i++)
			{
				if (this.eventsBuffer[i] != IntPtr.Zero)
				{
					NativeWrapper.EvtClose(this.eventsBuffer[i]);
					this.eventsBuffer[i] = IntPtr.Zero;
				}
			}
			this.numEventsInBuffer = 0;
		}

		// Token: 0x04000C5A RID: 3162
		private EventLogQuery eventQuery;

		// Token: 0x04000C5B RID: 3163
		private EventBookmark bookmark;

		// Token: 0x04000C5C RID: 3164
		private bool readExistingEvents;

		// Token: 0x04000C5D RID: 3165
		private EventLogHandle handle;

		// Token: 0x04000C5E RID: 3166
		private IntPtr[] eventsBuffer;

		// Token: 0x04000C5F RID: 3167
		private int numEventsInBuffer;

		// Token: 0x04000C60 RID: 3168
		private bool isSubscribing;

		// Token: 0x04000C61 RID: 3169
		private int callbackThreadId;

		// Token: 0x04000C62 RID: 3170
		private AutoResetEvent subscriptionWaitHandle;

		// Token: 0x04000C63 RID: 3171
		private AutoResetEvent unregisterDoneHandle;

		// Token: 0x04000C64 RID: 3172
		private RegisteredWaitHandle registeredWaitHandle;

		// Token: 0x04000C65 RID: 3173
		private ProviderMetadataCachedInformation cachedMetadataInformation;

		// Token: 0x04000C66 RID: 3174
		private EventLogException asyncException;
	}
}
