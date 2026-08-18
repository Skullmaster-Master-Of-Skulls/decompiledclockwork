using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002C0 RID: 704
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventMetadata
	{
		// Token: 0x0600198B RID: 6539 RVA: 0x0005CFE4 File Offset: 0x0005B1E4
		internal EventMetadata(uint id, byte version, byte channelId, byte level, byte opcode, short task, long keywords, string template, string description, ProviderMetadata pmReference)
		{
			this.id = (long)((ulong)id);
			this.version = version;
			this.channelId = channelId;
			this.level = level;
			this.opcode = (short)opcode;
			this.task = (int)task;
			this.keywords = keywords;
			this.template = template;
			this.description = description;
			this.pmReference = pmReference;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x0005D045 File Offset: 0x0005B245
		public long Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x0005D04D File Offset: 0x0005B24D
		public byte Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x0005D055 File Offset: 0x0005B255
		public EventLogLink LogLink
		{
			get
			{
				return new EventLogLink((uint)this.channelId, this.pmReference);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x0005D068 File Offset: 0x0005B268
		public EventLevel Level
		{
			get
			{
				return new EventLevel((int)this.level, this.pmReference);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x0005D07B File Offset: 0x0005B27B
		public EventOpcode Opcode
		{
			get
			{
				return new EventOpcode((int)this.opcode, this.pmReference);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x0005D08E File Offset: 0x0005B28E
		public EventTask Task
		{
			get
			{
				return new EventTask(this.task, this.pmReference);
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x0005D0A4 File Offset: 0x0005B2A4
		public IEnumerable<EventKeyword> Keywords
		{
			get
			{
				List<EventKeyword> list = new List<EventKeyword>();
				ulong num = (ulong)this.keywords;
				ulong num2 = 9223372036854775808UL;
				for (int i = 0; i < 64; i++)
				{
					if ((num & num2) > 0UL)
					{
						list.Add(new EventKeyword((long)num2, this.pmReference));
					}
					num2 >>= 1;
				}
				return list;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x0005D0F3 File Offset: 0x0005B2F3
		public string Template
		{
			get
			{
				return this.template;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x0005D0FB File Offset: 0x0005B2FB
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x04000C80 RID: 3200
		private long id;

		// Token: 0x04000C81 RID: 3201
		private byte version;

		// Token: 0x04000C82 RID: 3202
		private byte channelId;

		// Token: 0x04000C83 RID: 3203
		private byte level;

		// Token: 0x04000C84 RID: 3204
		private short opcode;

		// Token: 0x04000C85 RID: 3205
		private int task;

		// Token: 0x04000C86 RID: 3206
		private long keywords;

		// Token: 0x04000C87 RID: 3207
		private string template;

		// Token: 0x04000C88 RID: 3208
		private string description;

		// Token: 0x04000C89 RID: 3209
		private ProviderMetadata pmReference;
	}
}
