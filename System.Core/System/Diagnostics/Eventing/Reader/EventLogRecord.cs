using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002B6 RID: 694
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogRecord : EventRecord
	{
		// Token: 0x0600191C RID: 6428 RVA: 0x0005B6D0 File Offset: 0x000598D0
		[SecuritySafeCritical]
		internal EventLogRecord(EventLogHandle handle, EventLogSession session, ProviderMetadataCachedInformation cachedMetadataInfo)
		{
			this.cachedMetadataInformation = cachedMetadataInfo;
			this.handle = handle;
			this.session = session;
			this.systemProperties = new NativeWrapper.SystemProperties();
			this.syncObject = new object();
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x0005B703 File Offset: 0x00059903
		internal EventLogHandle Handle
		{
			[SecuritySafeCritical]
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0005B70C File Offset: 0x0005990C
		internal void PrepareSystemData()
		{
			if (this.systemProperties.filled)
			{
				return;
			}
			this.session.SetupSystemContext();
			object obj = this.syncObject;
			lock (obj)
			{
				if (!this.systemProperties.filled)
				{
					NativeWrapper.EvtRenderBufferWithContextSystem(this.session.renderContextHandleSystem, this.handle, UnsafeNativeMethods.EvtRenderFlags.EvtRenderEventValues, this.systemProperties, 18);
					this.systemProperties.filled = true;
				}
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x0005B798 File Offset: 0x00059998
		public override int Id
		{
			get
			{
				this.PrepareSystemData();
				if (this.systemProperties.Id == null)
				{
					return 0;
				}
				return (int)this.systemProperties.Id.Value;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x0005B7C4 File Offset: 0x000599C4
		public override byte? Version
		{
			get
			{
				this.PrepareSystemData();
				return this.systemProperties.Version;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x0005B7D8 File Offset: 0x000599D8
		public override int? Qualifiers
		{
			get
			{
				this.PrepareSystemData();
				ushort? qualifiers = this.systemProperties.Qualifiers;
				if (qualifiers == null)
				{
					return null;
				}
				return new int?((int)qualifiers.GetValueOrDefault());
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x0005B816 File Offset: 0x00059A16
		public override byte? Level
		{
			get
			{
				this.PrepareSystemData();
				return this.systemProperties.Level;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x0005B82C File Offset: 0x00059A2C
		public override int? Task
		{
			get
			{
				this.PrepareSystemData();
				ushort? task = this.systemProperties.Task;
				if (task == null)
				{
					return null;
				}
				return new int?((int)task.GetValueOrDefault());
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x0005B86C File Offset: 0x00059A6C
		public override short? Opcode
		{
			get
			{
				this.PrepareSystemData();
				byte? opcode = this.systemProperties.Opcode;
				if (opcode == null)
				{
					return null;
				}
				return new short?((short)opcode.GetValueOrDefault());
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x0005B8AC File Offset: 0x00059AAC
		public override long? Keywords
		{
			get
			{
				this.PrepareSystemData();
				ulong? keywords = this.systemProperties.Keywords;
				if (keywords == null)
				{
					return null;
				}
				return new long?((long)keywords.GetValueOrDefault());
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x0005B8EC File Offset: 0x00059AEC
		public override long? RecordId
		{
			get
			{
				this.PrepareSystemData();
				ulong? recordId = this.systemProperties.RecordId;
				if (recordId == null)
				{
					return null;
				}
				return new long?((long)recordId.GetValueOrDefault());
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x0005B92A File Offset: 0x00059B2A
		public override string ProviderName
		{
			get
			{
				this.PrepareSystemData();
				return this.systemProperties.ProviderName;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x0005B93D File Offset: 0x00059B3D
		public override Guid? ProviderId
		{
			get
			{
				this.PrepareSystemData();
				return this.systemProperties.ProviderId;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x0005B950 File Offset: 0x00059B50
		public override string LogName
		{
			get
			{
				this.PrepareSystemData();
				return this.systemProperties.ChannelName;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x0005B964 File Offset: 0x00059B64
		public override int? ProcessId
		{
			get
			{
				this.PrepareSystemData();
				uint? processId = this.systemProperties.ProcessId;
				if (processId == null)
				{
					return null;
				}
				return new int?((int)processId.GetValueOrDefault());
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0005B9A4 File Offset: 0x00059BA4
		public override int? ThreadId
		{
			get
			{
				this.PrepareSystemData();
				uint? threadId = this.systemProperties.ThreadId;
				if (threadId == null)
				{
					return null;
				}
				return new int?((int)threadId.GetValueOrDefault());
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x0005B9E2 File Offset: 0x00059BE2
		public override string MachineName
		{
			get
			{
				this.PrepareSystemData();
				return this.systemProperties.ComputerName;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x0005B9F5 File Offset: 0x00059BF5
		public override SecurityIdentifier UserId
		{
			get
			{
				this.PrepareSystemData();
				return this.systemProperties.UserId;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x0005BA08 File Offset: 0x00059C08
		public override DateTime? TimeCreated
		{
			get
			{
				this.PrepareSystemData();
				return this.systemProperties.TimeCreated;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x0005BA1B File Offset: 0x00059C1B
		public override Guid? ActivityId
		{
			get
			{
				this.PrepareSystemData();
				return this.systemProperties.ActivityId;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x0005BA2E File Offset: 0x00059C2E
		public override Guid? RelatedActivityId
		{
			get
			{
				this.PrepareSystemData();
				return this.systemProperties.RelatedActivityId;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x0005BA44 File Offset: 0x00059C44
		public string ContainerLog
		{
			get
			{
				if (this.containerChannel != null)
				{
					return this.containerChannel;
				}
				object obj = this.syncObject;
				string result;
				lock (obj)
				{
					if (this.containerChannel == null)
					{
						this.containerChannel = (string)NativeWrapper.EvtGetEventInfo(this.Handle, UnsafeNativeMethods.EvtEventPropertyId.EvtEventPath);
					}
					result = this.containerChannel;
				}
				return result;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x0005BAB4 File Offset: 0x00059CB4
		public IEnumerable<int> MatchedQueryIds
		{
			get
			{
				if (this.matchedQueryIds != null)
				{
					return this.matchedQueryIds;
				}
				object obj = this.syncObject;
				IEnumerable<int> result;
				lock (obj)
				{
					if (this.matchedQueryIds == null)
					{
						this.matchedQueryIds = (int[])NativeWrapper.EvtGetEventInfo(this.Handle, UnsafeNativeMethods.EvtEventPropertyId.EvtEventQueryIDs);
					}
					result = this.matchedQueryIds;
				}
				return result;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x0005BB24 File Offset: 0x00059D24
		public override EventBookmark Bookmark
		{
			[SecuritySafeCritical]
			get
			{
				EventLogPermissionHolder.GetEventLogPermission().Demand();
				EventLogHandle eventLogHandle = NativeWrapper.EvtCreateBookmark(null);
				NativeWrapper.EvtUpdateBookmark(eventLogHandle, this.handle);
				string bookmarkText = NativeWrapper.EvtRenderBookmark(eventLogHandle);
				return new EventBookmark(bookmarkText);
			}
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0005BB5B File Offset: 0x00059D5B
		public override string FormatDescription()
		{
			return this.cachedMetadataInformation.GetFormatDescription(this.ProviderName, this.handle);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0005BB74 File Offset: 0x00059D74
		public override string FormatDescription(IEnumerable<object> values)
		{
			if (values == null)
			{
				return this.FormatDescription();
			}
			string[] array = new string[0];
			int num = 0;
			foreach (object obj in values)
			{
				if (array.Length == num)
				{
					Array.Resize<string>(ref array, num + 1);
				}
				array[num] = obj.ToString();
				num++;
			}
			return this.cachedMetadataInformation.GetFormatDescription(this.ProviderName, this.handle, array);
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x0005BC00 File Offset: 0x00059E00
		public override string LevelDisplayName
		{
			get
			{
				if (this.levelNameReady)
				{
					return this.levelName;
				}
				object obj = this.syncObject;
				string result;
				lock (obj)
				{
					if (!this.levelNameReady)
					{
						this.levelNameReady = true;
						this.levelName = this.cachedMetadataInformation.GetLevelDisplayName(this.ProviderName, this.handle);
					}
					result = this.levelName;
				}
				return result;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x0005BC80 File Offset: 0x00059E80
		public override string OpcodeDisplayName
		{
			get
			{
				object obj = this.syncObject;
				string result;
				lock (obj)
				{
					if (!this.opcodeNameReady)
					{
						this.opcodeNameReady = true;
						this.opcodeName = this.cachedMetadataInformation.GetOpcodeDisplayName(this.ProviderName, this.handle);
					}
					result = this.opcodeName;
				}
				return result;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x0005BCF0 File Offset: 0x00059EF0
		public override string TaskDisplayName
		{
			get
			{
				if (this.taskNameReady)
				{
					return this.taskName;
				}
				object obj = this.syncObject;
				string result;
				lock (obj)
				{
					if (!this.taskNameReady)
					{
						this.taskNameReady = true;
						this.taskName = this.cachedMetadataInformation.GetTaskDisplayName(this.ProviderName, this.handle);
					}
					result = this.taskName;
				}
				return result;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x0005BD70 File Offset: 0x00059F70
		public override IEnumerable<string> KeywordsDisplayNames
		{
			get
			{
				if (this.keywordsNames != null)
				{
					return this.keywordsNames;
				}
				object obj = this.syncObject;
				IEnumerable<string> result;
				lock (obj)
				{
					if (this.keywordsNames == null)
					{
						this.keywordsNames = this.cachedMetadataInformation.GetKeywordDisplayNames(this.ProviderName, this.handle);
					}
					result = this.keywordsNames;
				}
				return result;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x0005BDE8 File Offset: 0x00059FE8
		public override IList<EventProperty> Properties
		{
			get
			{
				this.session.SetupUserContext();
				IList<object> list = NativeWrapper.EvtRenderBufferWithContextUserOrValues(this.session.renderContextHandleUser, this.handle);
				List<EventProperty> list2 = new List<EventProperty>();
				foreach (object value in list)
				{
					list2.Add(new EventProperty(value));
				}
				return list2;
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0005BE60 File Offset: 0x0005A060
		public IList<object> GetPropertyValues(EventLogPropertySelector propertySelector)
		{
			if (propertySelector == null)
			{
				throw new ArgumentNullException("propertySelector");
			}
			return NativeWrapper.EvtRenderBufferWithContextUserOrValues(propertySelector.Handle, this.handle);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0005BE84 File Offset: 0x0005A084
		[SecuritySafeCritical]
		public override string ToXml()
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			StringBuilder stringBuilder = new StringBuilder(2000);
			NativeWrapper.EvtRender(EventLogHandle.Zero, this.handle, UnsafeNativeMethods.EvtRenderFlags.EvtRenderEventXml, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0005BEC0 File Offset: 0x0005A0C0
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			try
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
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0005BF14 File Offset: 0x0005A114
		[SecurityCritical]
		internal static EventLogHandle GetBookmarkHandleFromBookmark(EventBookmark bookmark)
		{
			if (bookmark == null)
			{
				return EventLogHandle.Zero;
			}
			return NativeWrapper.EvtCreateBookmark(bookmark.BookmarkText);
		}

		// Token: 0x04000C42 RID: 3138
		private const int SYSTEM_PROPERTY_COUNT = 18;

		// Token: 0x04000C43 RID: 3139
		[SecuritySafeCritical]
		private EventLogHandle handle;

		// Token: 0x04000C44 RID: 3140
		private EventLogSession session;

		// Token: 0x04000C45 RID: 3141
		private NativeWrapper.SystemProperties systemProperties;

		// Token: 0x04000C46 RID: 3142
		private string containerChannel;

		// Token: 0x04000C47 RID: 3143
		private int[] matchedQueryIds;

		// Token: 0x04000C48 RID: 3144
		private object syncObject;

		// Token: 0x04000C49 RID: 3145
		private string levelName;

		// Token: 0x04000C4A RID: 3146
		private string taskName;

		// Token: 0x04000C4B RID: 3147
		private string opcodeName;

		// Token: 0x04000C4C RID: 3148
		private IEnumerable<string> keywordsNames;

		// Token: 0x04000C4D RID: 3149
		private bool levelNameReady;

		// Token: 0x04000C4E RID: 3150
		private bool taskNameReady;

		// Token: 0x04000C4F RID: 3151
		private bool opcodeNameReady;

		// Token: 0x04000C50 RID: 3152
		private ProviderMetadataCachedInformation cachedMetadataInformation;
	}
}
