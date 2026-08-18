using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002C8 RID: 712
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventLogInformation
	{
		// Token: 0x060019BE RID: 6590 RVA: 0x0005D5A0 File Offset: 0x0005B7A0
		[SecuritySafeCritical]
		internal EventLogInformation(EventLogSession session, string channelName, PathType pathType)
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			EventLogHandle eventLogHandle = NativeWrapper.EvtOpenLog(session.Handle, channelName, pathType);
			using (eventLogHandle)
			{
				this.creationTime = (DateTime?)NativeWrapper.EvtGetLogInfo(eventLogHandle, UnsafeNativeMethods.EvtLogPropertyId.EvtLogCreationTime);
				this.lastAccessTime = (DateTime?)NativeWrapper.EvtGetLogInfo(eventLogHandle, UnsafeNativeMethods.EvtLogPropertyId.EvtLogLastAccessTime);
				this.lastWriteTime = (DateTime?)NativeWrapper.EvtGetLogInfo(eventLogHandle, UnsafeNativeMethods.EvtLogPropertyId.EvtLogLastWriteTime);
				ulong? num = (ulong?)NativeWrapper.EvtGetLogInfo(eventLogHandle, UnsafeNativeMethods.EvtLogPropertyId.EvtLogFileSize);
				this.fileSize = ((num != null) ? new long?((long)num.GetValueOrDefault()) : null);
				uint? num2 = (uint?)NativeWrapper.EvtGetLogInfo(eventLogHandle, UnsafeNativeMethods.EvtLogPropertyId.EvtLogAttributes);
				this.fileAttributes = ((num2 != null) ? new int?((int)num2.GetValueOrDefault()) : null);
				num = (ulong?)NativeWrapper.EvtGetLogInfo(eventLogHandle, UnsafeNativeMethods.EvtLogPropertyId.EvtLogNumberOfLogRecords);
				this.recordCount = ((num != null) ? new long?((long)num.GetValueOrDefault()) : null);
				num = (ulong?)NativeWrapper.EvtGetLogInfo(eventLogHandle, UnsafeNativeMethods.EvtLogPropertyId.EvtLogOldestRecordNumber);
				this.oldestRecordNumber = ((num != null) ? new long?((long)num.GetValueOrDefault()) : null);
				this.isLogFull = (bool?)NativeWrapper.EvtGetLogInfo(eventLogHandle, UnsafeNativeMethods.EvtLogPropertyId.EvtLogFull);
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x0005D70C File Offset: 0x0005B90C
		public DateTime? CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x0005D714 File Offset: 0x0005B914
		public DateTime? LastAccessTime
		{
			get
			{
				return this.lastAccessTime;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x0005D71C File Offset: 0x0005B91C
		public DateTime? LastWriteTime
		{
			get
			{
				return this.lastWriteTime;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x0005D724 File Offset: 0x0005B924
		public long? FileSize
		{
			get
			{
				return this.fileSize;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x0005D72C File Offset: 0x0005B92C
		public int? Attributes
		{
			get
			{
				return this.fileAttributes;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x0005D734 File Offset: 0x0005B934
		public long? RecordCount
		{
			get
			{
				return this.recordCount;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x0005D73C File Offset: 0x0005B93C
		public long? OldestRecordNumber
		{
			get
			{
				return this.oldestRecordNumber;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x0005D744 File Offset: 0x0005B944
		public bool? IsLogFull
		{
			get
			{
				return this.isLogFull;
			}
		}

		// Token: 0x04000C98 RID: 3224
		private DateTime? creationTime;

		// Token: 0x04000C99 RID: 3225
		private DateTime? lastAccessTime;

		// Token: 0x04000C9A RID: 3226
		private DateTime? lastWriteTime;

		// Token: 0x04000C9B RID: 3227
		private long? fileSize;

		// Token: 0x04000C9C RID: 3228
		private int? fileAttributes;

		// Token: 0x04000C9D RID: 3229
		private long? recordCount;

		// Token: 0x04000C9E RID: 3230
		private long? oldestRecordNumber;

		// Token: 0x04000C9F RID: 3231
		private bool? isLogFull;
	}
}
