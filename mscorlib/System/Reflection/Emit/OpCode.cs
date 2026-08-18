using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200083E RID: 2110
	[ComVisible(true)]
	public struct OpCode
	{
		// Token: 0x06004BD6 RID: 19414 RVA: 0x0010A1C0 File Offset: 0x001091C0
		internal OpCode(string stringname, StackBehaviour pop, StackBehaviour push, OperandType operand, OpCodeType type, int size, byte s1, byte s2, FlowControl ctrl, bool endsjmpblk, int stack)
		{
			this.m_stringname = stringname;
			this.m_pop = pop;
			this.m_push = push;
			this.m_operand = operand;
			this.m_type = type;
			this.m_size = size;
			this.m_s1 = s1;
			this.m_s2 = s2;
			this.m_ctrl = ctrl;
			this.m_endsUncondJmpBlk = endsjmpblk;
			this.m_stackChange = stack;
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x0010A222 File Offset: 0x00109222
		internal bool EndsUncondJmpBlk()
		{
			return this.m_endsUncondJmpBlk;
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x0010A22A File Offset: 0x0010922A
		internal int StackChange()
		{
			return this.m_stackChange;
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06004BD9 RID: 19417 RVA: 0x0010A232 File Offset: 0x00109232
		public OperandType OperandType
		{
			get
			{
				return this.m_operand;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06004BDA RID: 19418 RVA: 0x0010A23A File Offset: 0x0010923A
		public FlowControl FlowControl
		{
			get
			{
				return this.m_ctrl;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06004BDB RID: 19419 RVA: 0x0010A242 File Offset: 0x00109242
		public OpCodeType OpCodeType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06004BDC RID: 19420 RVA: 0x0010A24A File Offset: 0x0010924A
		public StackBehaviour StackBehaviourPop
		{
			get
			{
				return this.m_pop;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06004BDD RID: 19421 RVA: 0x0010A252 File Offset: 0x00109252
		public StackBehaviour StackBehaviourPush
		{
			get
			{
				return this.m_push;
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06004BDE RID: 19422 RVA: 0x0010A25A File Offset: 0x0010925A
		public int Size
		{
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06004BDF RID: 19423 RVA: 0x0010A262 File Offset: 0x00109262
		public short Value
		{
			get
			{
				if (this.m_size == 2)
				{
					return (short)((int)this.m_s1 << 8 | (int)this.m_s2);
				}
				return (short)this.m_s2;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06004BE0 RID: 19424 RVA: 0x0010A284 File Offset: 0x00109284
		public string Name
		{
			get
			{
				return this.m_stringname;
			}
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x0010A28C File Offset: 0x0010928C
		public override bool Equals(object obj)
		{
			return obj is OpCode && this.Equals((OpCode)obj);
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x0010A2A4 File Offset: 0x001092A4
		public bool Equals(OpCode obj)
		{
			return obj.m_s1 == this.m_s1 && obj.m_s2 == this.m_s2;
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x0010A2C6 File Offset: 0x001092C6
		public static bool operator ==(OpCode a, OpCode b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x0010A2D0 File Offset: 0x001092D0
		public static bool operator !=(OpCode a, OpCode b)
		{
			return !(a == b);
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x0010A2DC File Offset: 0x001092DC
		public override int GetHashCode()
		{
			return this.m_stringname.GetHashCode();
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x0010A2E9 File Offset: 0x001092E9
		public override string ToString()
		{
			return this.m_stringname;
		}

		// Token: 0x0400277D RID: 10109
		internal string m_stringname;

		// Token: 0x0400277E RID: 10110
		internal StackBehaviour m_pop;

		// Token: 0x0400277F RID: 10111
		internal StackBehaviour m_push;

		// Token: 0x04002780 RID: 10112
		internal OperandType m_operand;

		// Token: 0x04002781 RID: 10113
		internal OpCodeType m_type;

		// Token: 0x04002782 RID: 10114
		internal int m_size;

		// Token: 0x04002783 RID: 10115
		internal byte m_s1;

		// Token: 0x04002784 RID: 10116
		internal byte m_s2;

		// Token: 0x04002785 RID: 10117
		internal FlowControl m_ctrl;

		// Token: 0x04002786 RID: 10118
		internal bool m_endsUncondJmpBlk;

		// Token: 0x04002787 RID: 10119
		internal int m_stackChange;
	}
}
