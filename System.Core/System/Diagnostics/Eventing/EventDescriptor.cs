using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing
{
	// Token: 0x020002A7 RID: 679
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct EventDescriptor
	{
		// Token: 0x0600188C RID: 6284 RVA: 0x00059990 File Offset: 0x00057B90
		public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
		{
			if (id < 0)
			{
				throw new ArgumentOutOfRangeException("id", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (id > 65535)
			{
				throw new ArgumentOutOfRangeException("id", SR.GetString("ArgumentOutOfRange_NeedValidId", new object[]
				{
					1,
					ushort.MaxValue
				}));
			}
			this.m_id = (ushort)id;
			this.m_version = version;
			this.m_channel = channel;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_keywords = keywords;
			if (task < 0)
			{
				throw new ArgumentOutOfRangeException("task", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (task > 65535)
			{
				throw new ArgumentOutOfRangeException("task", SR.GetString("ArgumentOutOfRange_NeedValidId", new object[]
				{
					1,
					ushort.MaxValue
				}));
			}
			this.m_task = (ushort)task;
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x00059A7A File Offset: 0x00057C7A
		public int EventId
		{
			get
			{
				return (int)this.m_id;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x00059A82 File Offset: 0x00057C82
		public byte Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00059A8A File Offset: 0x00057C8A
		public byte Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x00059A92 File Offset: 0x00057C92
		public byte Level
		{
			get
			{
				return this.m_level;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00059A9A File Offset: 0x00057C9A
		public byte Opcode
		{
			get
			{
				return this.m_opcode;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x00059AA2 File Offset: 0x00057CA2
		public int Task
		{
			get
			{
				return (int)this.m_task;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x00059AAA File Offset: 0x00057CAA
		public long Keywords
		{
			get
			{
				return this.m_keywords;
			}
		}

		// Token: 0x04000BF5 RID: 3061
		[FieldOffset(0)]
		private ushort m_id;

		// Token: 0x04000BF6 RID: 3062
		[FieldOffset(2)]
		private byte m_version;

		// Token: 0x04000BF7 RID: 3063
		[FieldOffset(3)]
		private byte m_channel;

		// Token: 0x04000BF8 RID: 3064
		[FieldOffset(4)]
		private byte m_level;

		// Token: 0x04000BF9 RID: 3065
		[FieldOffset(5)]
		private byte m_opcode;

		// Token: 0x04000BFA RID: 3066
		[FieldOffset(6)]
		private ushort m_task;

		// Token: 0x04000BFB RID: 3067
		[FieldOffset(8)]
		private long m_keywords;
	}
}
