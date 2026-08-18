using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002AE RID: 686
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogConfiguration : IDisposable
	{
		// Token: 0x060018C4 RID: 6340 RVA: 0x0005AD96 File Offset: 0x00058F96
		public EventLogConfiguration(string logName) : this(logName, null)
		{
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0005ADA0 File Offset: 0x00058FA0
		[SecurityCritical]
		public EventLogConfiguration(string logName, EventLogSession session)
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			if (session == null)
			{
				session = EventLogSession.GlobalSession;
			}
			this.session = session;
			this.channelName = logName;
			this.handle = NativeWrapper.EvtOpenChannelConfig(this.session.Handle, this.channelName, 0);
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x0005ADFD File Offset: 0x00058FFD
		public string LogName
		{
			get
			{
				return this.channelName;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x0005AE05 File Offset: 0x00059005
		public EventLogType LogType
		{
			get
			{
				return (EventLogType)((uint)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelConfigType));
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0005AE18 File Offset: 0x00059018
		public EventLogIsolation LogIsolation
		{
			get
			{
				return (EventLogIsolation)((uint)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelConfigIsolation));
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x0005AE2B File Offset: 0x0005902B
		// (set) Token: 0x060018CA RID: 6346 RVA: 0x0005AE3E File Offset: 0x0005903E
		public bool IsEnabled
		{
			get
			{
				return (bool)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelConfigEnabled);
			}
			set
			{
				NativeWrapper.EvtSetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelConfigEnabled, value);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x0005AE52 File Offset: 0x00059052
		public bool IsClassicLog
		{
			get
			{
				return (bool)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelConfigClassicEventlog);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x0005AE65 File Offset: 0x00059065
		// (set) Token: 0x060018CD RID: 6349 RVA: 0x0005AE78 File Offset: 0x00059078
		public string SecurityDescriptor
		{
			get
			{
				return (string)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelConfigAccess);
			}
			set
			{
				NativeWrapper.EvtSetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelConfigAccess, value);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0005AE87 File Offset: 0x00059087
		// (set) Token: 0x060018CF RID: 6351 RVA: 0x0005AE9B File Offset: 0x0005909B
		public string LogFilePath
		{
			get
			{
				return (string)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigLogFilePath);
			}
			set
			{
				NativeWrapper.EvtSetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigLogFilePath, value);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0005AEAB File Offset: 0x000590AB
		// (set) Token: 0x060018D1 RID: 6353 RVA: 0x0005AEBE File Offset: 0x000590BE
		public long MaximumSizeInBytes
		{
			get
			{
				return (long)((ulong)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigMaxSize));
			}
			set
			{
				NativeWrapper.EvtSetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigMaxSize, value);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0005AED4 File Offset: 0x000590D4
		// (set) Token: 0x060018D3 RID: 6355 RVA: 0x0005AF20 File Offset: 0x00059120
		public EventLogMode LogMode
		{
			get
			{
				object obj = NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigRetention);
				object obj2 = NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigAutoBackup);
				bool flag = obj != null && (bool)obj;
				bool flag2 = obj2 != null && (bool)obj2;
				if (flag2)
				{
					return EventLogMode.AutoBackup;
				}
				if (flag)
				{
					return EventLogMode.Retain;
				}
				return EventLogMode.Circular;
			}
			set
			{
				switch (value)
				{
				case EventLogMode.Circular:
					NativeWrapper.EvtSetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigAutoBackup, false);
					NativeWrapper.EvtSetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigRetention, false);
					return;
				case EventLogMode.AutoBackup:
					NativeWrapper.EvtSetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigAutoBackup, true);
					NativeWrapper.EvtSetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigRetention, true);
					return;
				case EventLogMode.Retain:
					NativeWrapper.EvtSetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigAutoBackup, false);
					NativeWrapper.EvtSetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelLoggingConfigRetention, true);
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0005AFAE File Offset: 0x000591AE
		public string OwningProviderName
		{
			get
			{
				return (string)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelConfigOwningPublisher);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x0005AFC1 File Offset: 0x000591C1
		public IEnumerable<string> ProviderNames
		{
			get
			{
				return (string[])NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelPublisherList);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0005AFD8 File Offset: 0x000591D8
		// (set) Token: 0x060018D7 RID: 6359 RVA: 0x0005B017 File Offset: 0x00059217
		public int? ProviderLevel
		{
			get
			{
				uint? num = (uint?)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelPublishingConfigLevel);
				if (num == null)
				{
					return null;
				}
				return new int?((int)num.GetValueOrDefault());
			}
			set
			{
				NativeWrapper.EvtSetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelPublishingConfigLevel, value);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x0005B02C File Offset: 0x0005922C
		// (set) Token: 0x060018D9 RID: 6361 RVA: 0x0005B06B File Offset: 0x0005926B
		public long? ProviderKeywords
		{
			get
			{
				ulong? num = (ulong?)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelPublishingConfigKeywords);
				if (num == null)
				{
					return null;
				}
				return new long?((long)num.GetValueOrDefault());
			}
			set
			{
				NativeWrapper.EvtSetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelPublishingConfigKeywords, value);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0005B080 File Offset: 0x00059280
		public int? ProviderBufferSize
		{
			get
			{
				uint? num = (uint?)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelPublishingConfigBufferSize);
				if (num == null)
				{
					return null;
				}
				return new int?((int)num.GetValueOrDefault());
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0005B0C0 File Offset: 0x000592C0
		public int? ProviderMinimumNumberOfBuffers
		{
			get
			{
				uint? num = (uint?)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelPublishingConfigMinBuffers);
				if (num == null)
				{
					return null;
				}
				return new int?((int)num.GetValueOrDefault());
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0005B100 File Offset: 0x00059300
		public int? ProviderMaximumNumberOfBuffers
		{
			get
			{
				uint? num = (uint?)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelPublishingConfigMaxBuffers);
				if (num == null)
				{
					return null;
				}
				return new int?((int)num.GetValueOrDefault());
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x0005B140 File Offset: 0x00059340
		public int? ProviderLatency
		{
			get
			{
				uint? num = (uint?)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelPublishingConfigLatency);
				if (num == null)
				{
					return null;
				}
				return new int?((int)num.GetValueOrDefault());
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x0005B17F File Offset: 0x0005937F
		public Guid? ProviderControlGuid
		{
			get
			{
				return (Guid?)NativeWrapper.EvtGetChannelConfigProperty(this.handle, UnsafeNativeMethods.EvtChannelConfigPropertyId.EvtChannelPublishingConfigControlGuid);
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0005B193 File Offset: 0x00059393
		public void SaveChanges()
		{
			NativeWrapper.EvtSaveChannelConfig(this.handle, 0);
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0005B1A1 File Offset: 0x000593A1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0005B1B0 File Offset: 0x000593B0
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				EventLogPermissionHolder.GetEventLogPermission().Demand();
			}
			if (this.handle != null && !this.handle.IsInvalid)
			{
				this.handle.Dispose();
			}
		}

		// Token: 0x04000C28 RID: 3112
		private EventLogHandle handle = EventLogHandle.Zero;

		// Token: 0x04000C29 RID: 3113
		private EventLogSession session;

		// Token: 0x04000C2A RID: 3114
		private string channelName;
	}
}
