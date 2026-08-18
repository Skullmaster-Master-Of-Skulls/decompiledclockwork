using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002B7 RID: 695
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogReader : IDisposable
	{
		// Token: 0x0600193F RID: 6463 RVA: 0x0005BF37 File Offset: 0x0005A137
		public EventLogReader(string path) : this(new EventLogQuery(path, PathType.LogName), null)
		{
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0005BF47 File Offset: 0x0005A147
		public EventLogReader(string path, PathType pathType) : this(new EventLogQuery(path, pathType), null)
		{
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0005BF57 File Offset: 0x0005A157
		public EventLogReader(EventLogQuery eventQuery) : this(eventQuery, null)
		{
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0005BF64 File Offset: 0x0005A164
		[SecurityCritical]
		public EventLogReader(EventLogQuery eventQuery, EventBookmark bookmark)
		{
			if (eventQuery == null)
			{
				throw new ArgumentNullException("eventQuery");
			}
			string logfile = null;
			if (eventQuery.ThePathType == PathType.FilePath)
			{
				logfile = eventQuery.Path;
			}
			this.cachedMetadataInformation = new ProviderMetadataCachedInformation(eventQuery.Session, logfile, 50);
			this.eventQuery = eventQuery;
			this.batchSize = 64;
			this.eventsBuffer = new IntPtr[this.batchSize];
			int num = 0;
			if (this.eventQuery.ThePathType == PathType.LogName)
			{
				num |= 1;
			}
			else
			{
				num |= 2;
			}
			if (this.eventQuery.ReverseDirection)
			{
				num |= 512;
			}
			if (this.eventQuery.TolerateQueryErrors)
			{
				num |= 4096;
			}
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			this.handle = NativeWrapper.EvtQuery(this.eventQuery.Session.Handle, this.eventQuery.Path, this.eventQuery.Query, num);
			EventLogHandle bookmarkHandleFromBookmark = EventLogRecord.GetBookmarkHandleFromBookmark(bookmark);
			if (!bookmarkHandleFromBookmark.IsInvalid)
			{
				using (bookmarkHandleFromBookmark)
				{
					NativeWrapper.EvtSeek(this.handle, 1L, bookmarkHandleFromBookmark, 0, UnsafeNativeMethods.EvtSeekFlags.EvtSeekRelativeToBookmark);
				}
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x0005C088 File Offset: 0x0005A288
		// (set) Token: 0x06001944 RID: 6468 RVA: 0x0005C090 File Offset: 0x0005A290
		public int BatchSize
		{
			get
			{
				return this.batchSize;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.batchSize = value;
			}
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0005C0A8 File Offset: 0x0005A2A8
		[SecurityCritical]
		private bool GetNextBatch(TimeSpan ts)
		{
			int timeout;
			if (ts == TimeSpan.MaxValue)
			{
				timeout = -1;
			}
			else
			{
				timeout = (int)ts.TotalMilliseconds;
			}
			if (this.batchSize != this.eventsBuffer.Length)
			{
				this.eventsBuffer = new IntPtr[this.batchSize];
			}
			int num = 0;
			if (!NativeWrapper.EvtNext(this.handle, this.batchSize, this.eventsBuffer, timeout, 0, ref num))
			{
				this.eventCount = 0;
				this.currentIndex = 0;
				return false;
			}
			this.currentIndex = 0;
			this.eventCount = num;
			return true;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0005C130 File Offset: 0x0005A330
		public EventRecord ReadEvent()
		{
			return this.ReadEvent(TimeSpan.MaxValue);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0005C140 File Offset: 0x0005A340
		[SecurityCritical]
		public EventRecord ReadEvent(TimeSpan timeout)
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			if (this.isEof)
			{
				throw new InvalidOperationException();
			}
			if (this.currentIndex >= this.eventCount)
			{
				this.GetNextBatch(timeout);
				if (this.currentIndex >= this.eventCount)
				{
					this.isEof = true;
					return null;
				}
			}
			EventLogRecord result = new EventLogRecord(new EventLogHandle(this.eventsBuffer[this.currentIndex], true), this.eventQuery.Session, this.cachedMetadataInformation);
			this.currentIndex++;
			return result;
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0005C1CB File Offset: 0x0005A3CB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0005C1DC File Offset: 0x0005A3DC
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				EventLogPermissionHolder.GetEventLogPermission().Demand();
			}
			while (this.currentIndex < this.eventCount)
			{
				NativeWrapper.EvtClose(this.eventsBuffer[this.currentIndex]);
				this.currentIndex++;
			}
			if (this.handle != null && !this.handle.IsInvalid)
			{
				this.handle.Dispose();
			}
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0005C248 File Offset: 0x0005A448
		[SecurityCritical]
		internal void SeekReset()
		{
			while (this.currentIndex < this.eventCount)
			{
				NativeWrapper.EvtClose(this.eventsBuffer[this.currentIndex]);
				this.currentIndex++;
			}
			this.currentIndex = 0;
			this.eventCount = 0;
			this.isEof = false;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0005C29A File Offset: 0x0005A49A
		[SecurityCritical]
		internal void SeekCommon(long offset)
		{
			offset -= (long)(this.eventCount - this.currentIndex);
			this.SeekReset();
			NativeWrapper.EvtSeek(this.handle, offset, EventLogHandle.Zero, 0, UnsafeNativeMethods.EvtSeekFlags.EvtSeekRelativeToCurrent);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0005C2C7 File Offset: 0x0005A4C7
		public void Seek(EventBookmark bookmark)
		{
			this.Seek(bookmark, 0L);
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0005C2D4 File Offset: 0x0005A4D4
		[SecurityCritical]
		public void Seek(EventBookmark bookmark, long offset)
		{
			if (bookmark == null)
			{
				throw new ArgumentNullException("bookmark");
			}
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			this.SeekReset();
			using (EventLogHandle bookmarkHandleFromBookmark = EventLogRecord.GetBookmarkHandleFromBookmark(bookmark))
			{
				NativeWrapper.EvtSeek(this.handle, offset, bookmarkHandleFromBookmark, 0, UnsafeNativeMethods.EvtSeekFlags.EvtSeekRelativeToBookmark);
			}
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0005C334 File Offset: 0x0005A534
		[SecurityCritical]
		public void Seek(SeekOrigin origin, long offset)
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.SeekReset();
				NativeWrapper.EvtSeek(this.handle, offset, EventLogHandle.Zero, 0, UnsafeNativeMethods.EvtSeekFlags.EvtSeekRelativeToFirst);
				return;
			case SeekOrigin.Current:
				if (offset >= 0L)
				{
					if ((long)this.currentIndex + offset < (long)this.eventCount)
					{
						int num = this.currentIndex;
						while ((long)num < (long)this.currentIndex + offset)
						{
							NativeWrapper.EvtClose(this.eventsBuffer[num]);
							num++;
						}
						this.currentIndex = (int)((long)this.currentIndex + offset);
						return;
					}
					this.SeekCommon(offset);
					return;
				}
				else
				{
					if ((long)this.currentIndex + offset >= 0L)
					{
						this.SeekCommon(offset);
						return;
					}
					this.SeekCommon(offset);
					return;
				}
				break;
			case SeekOrigin.End:
				this.SeekReset();
				NativeWrapper.EvtSeek(this.handle, offset, EventLogHandle.Zero, 0, UnsafeNativeMethods.EvtSeekFlags.EvtSeekRelativeToLast);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0005C405 File Offset: 0x0005A605
		public void CancelReading()
		{
			NativeWrapper.EvtCancel(this.handle);
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x0005C414 File Offset: 0x0005A614
		public IList<EventLogStatus> LogStatus
		{
			[SecurityCritical]
			get
			{
				EventLogPermissionHolder.GetEventLogPermission().Demand();
				EventLogHandle eventLogHandle = this.handle;
				if (eventLogHandle.IsInvalid)
				{
					throw new InvalidOperationException();
				}
				string[] array = (string[])NativeWrapper.EvtGetQueryInfo(eventLogHandle, UnsafeNativeMethods.EvtQueryPropertyId.EvtQueryNames);
				int[] array2 = (int[])NativeWrapper.EvtGetQueryInfo(eventLogHandle, UnsafeNativeMethods.EvtQueryPropertyId.EvtQueryStatuses);
				if (array.Length != array2.Length)
				{
					throw new InvalidOperationException();
				}
				List<EventLogStatus> list = new List<EventLogStatus>(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					EventLogStatus item = new EventLogStatus(array[i], array2[i]);
					list.Add(item);
				}
				return list.AsReadOnly();
			}
		}

		// Token: 0x04000C51 RID: 3153
		private EventLogQuery eventQuery;

		// Token: 0x04000C52 RID: 3154
		private int batchSize;

		// Token: 0x04000C53 RID: 3155
		private EventLogHandle handle;

		// Token: 0x04000C54 RID: 3156
		private IntPtr[] eventsBuffer;

		// Token: 0x04000C55 RID: 3157
		private int currentIndex;

		// Token: 0x04000C56 RID: 3158
		private int eventCount;

		// Token: 0x04000C57 RID: 3159
		private bool isEof;

		// Token: 0x04000C58 RID: 3160
		private ProviderMetadataCachedInformation cachedMetadataInformation;
	}
}
