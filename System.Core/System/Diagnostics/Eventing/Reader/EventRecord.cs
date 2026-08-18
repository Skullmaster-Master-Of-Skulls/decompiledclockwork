using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002B3 RID: 691
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class EventRecord : IDisposable
	{
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060018F2 RID: 6386
		public abstract int Id { get; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060018F3 RID: 6387
		public abstract byte? Version { get; }

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060018F4 RID: 6388
		public abstract byte? Level { get; }

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060018F5 RID: 6389
		public abstract int? Task { get; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060018F6 RID: 6390
		public abstract short? Opcode { get; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060018F7 RID: 6391
		public abstract long? Keywords { get; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060018F8 RID: 6392
		public abstract long? RecordId { get; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060018F9 RID: 6393
		public abstract string ProviderName { get; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060018FA RID: 6394
		public abstract Guid? ProviderId { get; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060018FB RID: 6395
		public abstract string LogName { get; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060018FC RID: 6396
		public abstract int? ProcessId { get; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060018FD RID: 6397
		public abstract int? ThreadId { get; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060018FE RID: 6398
		public abstract string MachineName { get; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060018FF RID: 6399
		public abstract SecurityIdentifier UserId { get; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001900 RID: 6400
		public abstract DateTime? TimeCreated { get; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001901 RID: 6401
		public abstract Guid? ActivityId { get; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001902 RID: 6402
		public abstract Guid? RelatedActivityId { get; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001903 RID: 6403
		public abstract int? Qualifiers { get; }

		// Token: 0x06001904 RID: 6404
		public abstract string FormatDescription();

		// Token: 0x06001905 RID: 6405
		public abstract string FormatDescription(IEnumerable<object> values);

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001906 RID: 6406
		public abstract string LevelDisplayName { get; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001907 RID: 6407
		public abstract string OpcodeDisplayName { get; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001908 RID: 6408
		public abstract string TaskDisplayName { get; }

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001909 RID: 6409
		public abstract IEnumerable<string> KeywordsDisplayNames { get; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600190A RID: 6410
		public abstract EventBookmark Bookmark { get; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600190B RID: 6411
		public abstract IList<EventProperty> Properties { get; }

		// Token: 0x0600190C RID: 6412
		public abstract string ToXml();

		// Token: 0x0600190D RID: 6413 RVA: 0x0005B43F File Offset: 0x0005963F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0005B44E File Offset: 0x0005964E
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
