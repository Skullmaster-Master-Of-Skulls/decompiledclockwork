using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002CA RID: 714
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class ProviderMetadata : IDisposable
	{
		// Token: 0x060019FC RID: 6652 RVA: 0x0005F4EC File Offset: 0x0005D6EC
		public ProviderMetadata(string providerName) : this(providerName, null, null, null)
		{
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0005F4F8 File Offset: 0x0005D6F8
		public ProviderMetadata(string providerName, EventLogSession session, CultureInfo targetCultureInfo) : this(providerName, session, targetCultureInfo, null)
		{
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0005F504 File Offset: 0x0005D704
		[SecuritySafeCritical]
		internal ProviderMetadata(string providerName, EventLogSession session, CultureInfo targetCultureInfo, string logFilePath)
		{
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			if (targetCultureInfo == null)
			{
				targetCultureInfo = CultureInfo.CurrentCulture;
			}
			if (session == null)
			{
				session = EventLogSession.GlobalSession;
			}
			this.session = session;
			this.providerName = providerName;
			this.cultureInfo = targetCultureInfo;
			this.logFilePath = logFilePath;
			this.handle = NativeWrapper.EvtOpenProviderMetadata(this.session.Handle, this.providerName, this.logFilePath, this.cultureInfo.LCID, 0);
			this.syncObject = new object();
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0005F5A1 File Offset: 0x0005D7A1
		internal EventLogHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x0005F5A9 File Offset: 0x0005D7A9
		public string Name
		{
			get
			{
				return this.providerName;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0005F5B1 File Offset: 0x0005D7B1
		public Guid Id
		{
			get
			{
				return (Guid)NativeWrapper.EvtGetPublisherMetadataProperty(this.handle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataPublisherGuid);
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x0005F5C4 File Offset: 0x0005D7C4
		public string MessageFilePath
		{
			get
			{
				return (string)NativeWrapper.EvtGetPublisherMetadataProperty(this.handle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataMessageFilePath);
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x0005F5D7 File Offset: 0x0005D7D7
		public string ResourceFilePath
		{
			get
			{
				return (string)NativeWrapper.EvtGetPublisherMetadataProperty(this.handle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataResourceFilePath);
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x0005F5EA File Offset: 0x0005D7EA
		public string ParameterFilePath
		{
			get
			{
				return (string)NativeWrapper.EvtGetPublisherMetadataProperty(this.handle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataParameterFilePath);
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0005F600 File Offset: 0x0005D800
		public Uri HelpLink
		{
			get
			{
				string text = (string)NativeWrapper.EvtGetPublisherMetadataProperty(this.handle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataHelpLink);
				if (text == null || text.Length == 0)
				{
					return null;
				}
				return new Uri(text);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x0005F632 File Offset: 0x0005D832
		private uint ProviderMessageID
		{
			get
			{
				return (uint)NativeWrapper.EvtGetPublisherMetadataProperty(this.handle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataPublisherMessageID);
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0005F648 File Offset: 0x0005D848
		public string DisplayName
		{
			[SecurityCritical]
			get
			{
				uint providerMessageID = this.ProviderMessageID;
				if (providerMessageID == 4294967295U)
				{
					return null;
				}
				EventLogPermissionHolder.GetEventLogPermission().Demand();
				return NativeWrapper.EvtFormatMessage(this.handle, providerMessageID);
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x0005F678 File Offset: 0x0005D878
		public IList<EventLogLink> LogLinks
		{
			[SecurityCritical]
			get
			{
				EventLogHandle eventLogHandle = EventLogHandle.Zero;
				IList<EventLogLink> result;
				try
				{
					object obj = this.syncObject;
					lock (obj)
					{
						if (this.channelReferences != null)
						{
							return this.channelReferences;
						}
						EventLogPermissionHolder.GetEventLogPermission().Demand();
						eventLogHandle = NativeWrapper.EvtGetPublisherMetadataPropertyHandle(this.handle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataChannelReferences);
						int num = NativeWrapper.EvtGetObjectArraySize(eventLogHandle);
						List<EventLogLink> list = new List<EventLogLink>(num);
						for (int i = 0; i < num; i++)
						{
							string text = (string)NativeWrapper.EvtGetObjectArrayProperty(eventLogHandle, i, 7);
							uint channelId = (uint)NativeWrapper.EvtGetObjectArrayProperty(eventLogHandle, i, 9);
							uint num2 = (uint)NativeWrapper.EvtGetObjectArrayProperty(eventLogHandle, i, 10);
							bool flag2 = num2 == 1U;
							int num3 = (int)((uint)NativeWrapper.EvtGetObjectArrayProperty(eventLogHandle, i, 11));
							string text2;
							if (num3 == -1)
							{
								text2 = null;
							}
							else
							{
								text2 = NativeWrapper.EvtFormatMessage(this.handle, (uint)num3);
							}
							if (text2 == null && flag2)
							{
								if (string.Compare(text, "Application", StringComparison.OrdinalIgnoreCase) == 0)
								{
									num3 = 256;
								}
								else if (string.Compare(text, "System", StringComparison.OrdinalIgnoreCase) == 0)
								{
									num3 = 258;
								}
								else if (string.Compare(text, "Security", StringComparison.OrdinalIgnoreCase) == 0)
								{
									num3 = 257;
								}
								else
								{
									num3 = -1;
								}
								if (num3 != -1)
								{
									if (this.defaultProviderHandle.IsInvalid)
									{
										this.defaultProviderHandle = NativeWrapper.EvtOpenProviderMetadata(this.session.Handle, null, null, this.cultureInfo.LCID, 0);
									}
									text2 = NativeWrapper.EvtFormatMessage(this.defaultProviderHandle, (uint)num3);
								}
							}
							list.Add(new EventLogLink(text, flag2, text2, channelId));
						}
						this.channelReferences = list.AsReadOnly();
					}
					result = this.channelReferences;
				}
				finally
				{
					eventLogHandle.Close();
				}
				return result;
			}
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0005F868 File Offset: 0x0005DA68
		internal string FindStandardLevelDisplayName(string name, uint value)
		{
			if (this.standardLevels == null)
			{
				this.standardLevels = (List<EventLevel>)this.GetProviderListProperty(this.defaultProviderHandle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataLevels);
			}
			foreach (EventLevel eventLevel in this.standardLevels)
			{
				if (eventLevel.Name == name && (long)eventLevel.Value == (long)((ulong)value))
				{
					return eventLevel.DisplayName;
				}
			}
			return null;
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0005F8F4 File Offset: 0x0005DAF4
		internal string FindStandardOpcodeDisplayName(string name, uint value)
		{
			if (this.standardOpcodes == null)
			{
				this.standardOpcodes = (List<EventOpcode>)this.GetProviderListProperty(this.defaultProviderHandle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataOpcodes);
			}
			foreach (EventOpcode eventOpcode in this.standardOpcodes)
			{
				if (eventOpcode.Name == name && (long)eventOpcode.Value == (long)((ulong)value))
				{
					return eventOpcode.DisplayName;
				}
			}
			return null;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0005F980 File Offset: 0x0005DB80
		internal string FindStandardKeywordDisplayName(string name, long value)
		{
			if (this.standardKeywords == null)
			{
				this.standardKeywords = (List<EventKeyword>)this.GetProviderListProperty(this.defaultProviderHandle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataKeywords);
			}
			foreach (EventKeyword eventKeyword in this.standardKeywords)
			{
				if (eventKeyword.Name == name && eventKeyword.Value == value)
				{
					return eventKeyword.DisplayName;
				}
			}
			return null;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0005FA0C File Offset: 0x0005DC0C
		internal string FindStandardTaskDisplayName(string name, uint value)
		{
			if (this.standardTasks == null)
			{
				this.standardTasks = (List<EventTask>)this.GetProviderListProperty(this.defaultProviderHandle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataTasks);
			}
			foreach (EventTask eventTask in this.standardTasks)
			{
				if (eventTask.Name == name && (long)eventTask.Value == (long)((ulong)value))
				{
					return eventTask.DisplayName;
				}
			}
			return null;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0005FA98 File Offset: 0x0005DC98
		[SecuritySafeCritical]
		internal object GetProviderListProperty(EventLogHandle providerHandle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId metadataProperty)
		{
			EventLogHandle eventLogHandle = EventLogHandle.Zero;
			EventLogPermissionHolder.GetEventLogPermission().Demand();
			object result;
			try
			{
				List<EventLevel> list = null;
				List<EventOpcode> list2 = null;
				List<EventKeyword> list3 = null;
				List<EventTask> list4 = null;
				eventLogHandle = NativeWrapper.EvtGetPublisherMetadataPropertyHandle(providerHandle, metadataProperty);
				int num = NativeWrapper.EvtGetObjectArraySize(eventLogHandle);
				ProviderMetadata.ObjectTypeName objectTypeName;
				if (metadataProperty <= UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataTasks)
				{
					if (metadataProperty == UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataLevels)
					{
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId = UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataLevelName;
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId2 = UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataLevelValue;
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId3 = UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataLevelMessageID;
						objectTypeName = ProviderMetadata.ObjectTypeName.Level;
						list = new List<EventLevel>(num);
						goto IL_AD;
					}
					if (metadataProperty == UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataTasks)
					{
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId = UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataTaskName;
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId2 = UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataTaskValue;
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId3 = UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataTaskMessageID;
						objectTypeName = ProviderMetadata.ObjectTypeName.Task;
						list4 = new List<EventTask>(num);
						goto IL_AD;
					}
				}
				else
				{
					if (metadataProperty == UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataOpcodes)
					{
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId = UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataOpcodeName;
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId2 = UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataOpcodeValue;
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId3 = UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataOpcodeMessageID;
						objectTypeName = ProviderMetadata.ObjectTypeName.Opcode;
						list2 = new List<EventOpcode>(num);
						goto IL_AD;
					}
					if (metadataProperty == UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataKeywords)
					{
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId = UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataKeywordName;
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId2 = UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataKeywordValue;
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId3 = UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataKeywordMessageID;
						objectTypeName = ProviderMetadata.ObjectTypeName.Keyword;
						list3 = new List<EventKeyword>(num);
						goto IL_AD;
					}
				}
				return null;
				IL_AD:
				for (int i = 0; i < num; i++)
				{
					UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId;
					string name = (string)NativeWrapper.EvtGetObjectArrayProperty(eventLogHandle, i, (int)thePropertyId);
					uint num2 = 0U;
					long value = 0L;
					if (objectTypeName != ProviderMetadata.ObjectTypeName.Keyword)
					{
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId2;
						num2 = (uint)NativeWrapper.EvtGetObjectArrayProperty(eventLogHandle, i, (int)thePropertyId2);
					}
					else
					{
						UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId2;
						value = (long)((ulong)NativeWrapper.EvtGetObjectArrayProperty(eventLogHandle, i, (int)thePropertyId2));
					}
					UnsafeNativeMethods.EvtPublisherMetadataPropertyId thePropertyId3;
					int num3 = (int)((uint)NativeWrapper.EvtGetObjectArrayProperty(eventLogHandle, i, (int)thePropertyId3));
					string displayName = null;
					if (num3 == -1)
					{
						if (providerHandle != this.defaultProviderHandle)
						{
							if (this.defaultProviderHandle.IsInvalid)
							{
								this.defaultProviderHandle = NativeWrapper.EvtOpenProviderMetadata(this.session.Handle, null, null, this.cultureInfo.LCID, 0);
							}
							switch (objectTypeName)
							{
							case ProviderMetadata.ObjectTypeName.Level:
								displayName = this.FindStandardLevelDisplayName(name, num2);
								break;
							case ProviderMetadata.ObjectTypeName.Opcode:
								displayName = this.FindStandardOpcodeDisplayName(name, num2 >> 16);
								break;
							case ProviderMetadata.ObjectTypeName.Task:
								displayName = this.FindStandardTaskDisplayName(name, num2);
								break;
							case ProviderMetadata.ObjectTypeName.Keyword:
								displayName = this.FindStandardKeywordDisplayName(name, value);
								break;
							default:
								displayName = null;
								break;
							}
						}
					}
					else
					{
						displayName = NativeWrapper.EvtFormatMessage(providerHandle, (uint)num3);
					}
					switch (objectTypeName)
					{
					case ProviderMetadata.ObjectTypeName.Level:
						list.Add(new EventLevel(name, (int)num2, displayName));
						break;
					case ProviderMetadata.ObjectTypeName.Opcode:
						list2.Add(new EventOpcode(name, (int)(num2 >> 16), displayName));
						break;
					case ProviderMetadata.ObjectTypeName.Task:
					{
						Guid guid = (Guid)NativeWrapper.EvtGetObjectArrayProperty(eventLogHandle, i, 18);
						list4.Add(new EventTask(name, (int)num2, displayName, guid));
						break;
					}
					case ProviderMetadata.ObjectTypeName.Keyword:
						list3.Add(new EventKeyword(name, value, displayName));
						break;
					default:
						return null;
					}
				}
				switch (objectTypeName)
				{
				case ProviderMetadata.ObjectTypeName.Level:
					result = list;
					break;
				case ProviderMetadata.ObjectTypeName.Opcode:
					result = list2;
					break;
				case ProviderMetadata.ObjectTypeName.Task:
					result = list4;
					break;
				case ProviderMetadata.ObjectTypeName.Keyword:
					result = list3;
					break;
				default:
					result = null;
					break;
				}
			}
			finally
			{
				eventLogHandle.Close();
			}
			return result;
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x0005FD44 File Offset: 0x0005DF44
		public IList<EventLevel> Levels
		{
			get
			{
				object obj = this.syncObject;
				lock (obj)
				{
					if (this.levels != null)
					{
						return this.levels;
					}
					List<EventLevel> list = (List<EventLevel>)this.GetProviderListProperty(this.handle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataLevels);
					this.levels = list.AsReadOnly();
				}
				return this.levels;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x0005FDB8 File Offset: 0x0005DFB8
		public IList<EventOpcode> Opcodes
		{
			get
			{
				object obj = this.syncObject;
				lock (obj)
				{
					if (this.opcodes != null)
					{
						return this.opcodes;
					}
					List<EventOpcode> list = (List<EventOpcode>)this.GetProviderListProperty(this.handle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataOpcodes);
					this.opcodes = list.AsReadOnly();
				}
				return this.opcodes;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x0005FE2C File Offset: 0x0005E02C
		public IList<EventKeyword> Keywords
		{
			get
			{
				object obj = this.syncObject;
				lock (obj)
				{
					if (this.keywords != null)
					{
						return this.keywords;
					}
					List<EventKeyword> list = (List<EventKeyword>)this.GetProviderListProperty(this.handle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataKeywords);
					this.keywords = list.AsReadOnly();
				}
				return this.keywords;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x0005FEA0 File Offset: 0x0005E0A0
		public IList<EventTask> Tasks
		{
			get
			{
				object obj = this.syncObject;
				lock (obj)
				{
					if (this.tasks != null)
					{
						return this.tasks;
					}
					List<EventTask> list = (List<EventTask>)this.GetProviderListProperty(this.handle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataTasks);
					this.tasks = list.AsReadOnly();
				}
				return this.tasks;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x0005FF14 File Offset: 0x0005E114
		public IEnumerable<EventMetadata> Events
		{
			[SecurityCritical]
			get
			{
				EventLogPermissionHolder.GetEventLogPermission().Demand();
				List<EventMetadata> list = new List<EventMetadata>();
				EventLogHandle eventLogHandle = NativeWrapper.EvtOpenEventMetadataEnum(this.handle, 0);
				IEnumerable<EventMetadata> result;
				using (eventLogHandle)
				{
					for (;;)
					{
						EventLogHandle eventLogHandle3 = NativeWrapper.EvtNextEventMetadata(eventLogHandle, 0);
						if (eventLogHandle3 != null)
						{
							using (eventLogHandle3)
							{
								uint id = (uint)NativeWrapper.EvtGetEventMetadataProperty(eventLogHandle3, UnsafeNativeMethods.EvtEventMetadataPropertyId.EventMetadataEventID);
								byte version = (byte)((uint)NativeWrapper.EvtGetEventMetadataProperty(eventLogHandle3, UnsafeNativeMethods.EvtEventMetadataPropertyId.EventMetadataEventVersion));
								byte channelId = (byte)((uint)NativeWrapper.EvtGetEventMetadataProperty(eventLogHandle3, UnsafeNativeMethods.EvtEventMetadataPropertyId.EventMetadataEventChannel));
								byte level = (byte)((uint)NativeWrapper.EvtGetEventMetadataProperty(eventLogHandle3, UnsafeNativeMethods.EvtEventMetadataPropertyId.EventMetadataEventLevel));
								byte opcode = (byte)((uint)NativeWrapper.EvtGetEventMetadataProperty(eventLogHandle3, UnsafeNativeMethods.EvtEventMetadataPropertyId.EventMetadataEventOpcode));
								short task = (short)((uint)NativeWrapper.EvtGetEventMetadataProperty(eventLogHandle3, UnsafeNativeMethods.EvtEventMetadataPropertyId.EventMetadataEventTask));
								long num = (long)((ulong)NativeWrapper.EvtGetEventMetadataProperty(eventLogHandle3, UnsafeNativeMethods.EvtEventMetadataPropertyId.EventMetadataEventKeyword));
								string template = (string)NativeWrapper.EvtGetEventMetadataProperty(eventLogHandle3, UnsafeNativeMethods.EvtEventMetadataPropertyId.EventMetadataEventTemplate);
								int num2 = (int)((uint)NativeWrapper.EvtGetEventMetadataProperty(eventLogHandle3, UnsafeNativeMethods.EvtEventMetadataPropertyId.EventMetadataEventMessageID));
								string description;
								if (num2 == -1)
								{
									description = null;
								}
								else
								{
									description = NativeWrapper.EvtFormatMessage(this.handle, (uint)num2);
								}
								EventMetadata item = new EventMetadata(id, version, channelId, level, opcode, task, num, template, description, this);
								list.Add(item);
								continue;
							}
							break;
						}
						break;
					}
					result = list.AsReadOnly();
				}
				return result;
			}
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00060054 File Offset: 0x0005E254
		internal void CheckReleased()
		{
			object obj = this.syncObject;
			lock (obj)
			{
				this.GetProviderListProperty(this.handle, UnsafeNativeMethods.EvtPublisherMetadataPropertyId.EvtPublisherMetadataTasks);
			}
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x000600A0 File Offset: 0x0005E2A0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000600AF File Offset: 0x0005E2AF
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

		// Token: 0x04000CA1 RID: 3233
		private EventLogHandle handle = EventLogHandle.Zero;

		// Token: 0x04000CA2 RID: 3234
		private EventLogHandle defaultProviderHandle = EventLogHandle.Zero;

		// Token: 0x04000CA3 RID: 3235
		private EventLogSession session;

		// Token: 0x04000CA4 RID: 3236
		private string providerName;

		// Token: 0x04000CA5 RID: 3237
		private CultureInfo cultureInfo;

		// Token: 0x04000CA6 RID: 3238
		private string logFilePath;

		// Token: 0x04000CA7 RID: 3239
		private IList<EventLevel> levels;

		// Token: 0x04000CA8 RID: 3240
		private IList<EventOpcode> opcodes;

		// Token: 0x04000CA9 RID: 3241
		private IList<EventTask> tasks;

		// Token: 0x04000CAA RID: 3242
		private IList<EventKeyword> keywords;

		// Token: 0x04000CAB RID: 3243
		private IList<EventLevel> standardLevels;

		// Token: 0x04000CAC RID: 3244
		private IList<EventOpcode> standardOpcodes;

		// Token: 0x04000CAD RID: 3245
		private IList<EventTask> standardTasks;

		// Token: 0x04000CAE RID: 3246
		private IList<EventKeyword> standardKeywords;

		// Token: 0x04000CAF RID: 3247
		private IList<EventLogLink> channelReferences;

		// Token: 0x04000CB0 RID: 3248
		private object syncObject;

		// Token: 0x0200046A RID: 1130
		internal enum ObjectTypeName
		{
			// Token: 0x04001349 RID: 4937
			Level,
			// Token: 0x0400134A RID: 4938
			Opcode,
			// Token: 0x0400134B RID: 4939
			Task,
			// Token: 0x0400134C RID: 4940
			Keyword
		}
	}
}
