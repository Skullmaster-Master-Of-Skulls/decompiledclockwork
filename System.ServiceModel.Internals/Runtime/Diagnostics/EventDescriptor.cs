using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Diagnostics
{
	// Token: 0x0200004B RID: 75
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct EventDescriptor
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0000C360 File Offset: 0x0000A560
		public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
		{
			if (id < 0)
			{
				throw Fx.Exception.ArgumentOutOfRange("id", id, InternalSR.ValueMustBeNonNegative);
			}
			if (id > 65535)
			{
				throw Fx.Exception.ArgumentOutOfRange("id", id, string.Empty);
			}
			this.m_id = (ushort)id;
			this.m_version = version;
			this.m_channel = channel;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_keywords = keywords;
			if (task < 0)
			{
				throw Fx.Exception.ArgumentOutOfRange("task", task, InternalSR.ValueMustBeNonNegative);
			}
			if (task > 65535)
			{
				throw Fx.Exception.ArgumentOutOfRange("task", task, string.Empty);
			}
			this.m_task = (ushort)task;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000C42C File Offset: 0x0000A62C
		public int EventId
		{
			get
			{
				return (int)this.m_id;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000C434 File Offset: 0x0000A634
		public byte Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000C43C File Offset: 0x0000A63C
		public byte Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000C444 File Offset: 0x0000A644
		public byte Level
		{
			get
			{
				return this.m_level;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000C44C File Offset: 0x0000A64C
		public byte Opcode
		{
			get
			{
				return this.m_opcode;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000C454 File Offset: 0x0000A654
		public int Task
		{
			get
			{
				return (int)this.m_task;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000C45C File Offset: 0x0000A65C
		public long Keywords
		{
			get
			{
				return this.m_keywords;
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000C464 File Offset: 0x0000A664
		public override bool Equals(object obj)
		{
			return obj is EventDescriptor && this.Equals((EventDescriptor)obj);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000C47C File Offset: 0x0000A67C
		public override int GetHashCode()
		{
			return (int)(this.m_id ^ (ushort)this.m_version ^ (ushort)this.m_channel ^ (ushort)this.m_level ^ (ushort)this.m_opcode ^ this.m_task) ^ (int)this.m_keywords;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
		public bool Equals(EventDescriptor other)
		{
			return this.m_id == other.m_id && this.m_version == other.m_version && this.m_channel == other.m_channel && this.m_level == other.m_level && this.m_opcode == other.m_opcode && this.m_task == other.m_task && this.m_keywords == other.m_keywords;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000C522 File Offset: 0x0000A722
		public static bool operator ==(EventDescriptor event1, EventDescriptor event2)
		{
			return event1.Equals(event2);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C52C File Offset: 0x0000A72C
		public static bool operator !=(EventDescriptor event1, EventDescriptor event2)
		{
			return !event1.Equals(event2);
		}

		// Token: 0x0400019C RID: 412
		[FieldOffset(0)]
		private ushort m_id;

		// Token: 0x0400019D RID: 413
		[FieldOffset(2)]
		private byte m_version;

		// Token: 0x0400019E RID: 414
		[FieldOffset(3)]
		private byte m_channel;

		// Token: 0x0400019F RID: 415
		[FieldOffset(4)]
		private byte m_level;

		// Token: 0x040001A0 RID: 416
		[FieldOffset(5)]
		private byte m_opcode;

		// Token: 0x040001A1 RID: 417
		[FieldOffset(6)]
		private ushort m_task;

		// Token: 0x040001A2 RID: 418
		[FieldOffset(8)]
		private long m_keywords;
	}
}
