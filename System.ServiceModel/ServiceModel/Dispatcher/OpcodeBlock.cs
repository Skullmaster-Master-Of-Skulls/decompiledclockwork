using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004D3 RID: 1235
	internal struct OpcodeBlock
	{
		// Token: 0x06002ED9 RID: 11993 RVA: 0x000B563C File Offset: 0x000B383C
		internal OpcodeBlock(Opcode first)
		{
			this.first = first;
			this.first.Prev = null;
			this.last = this.first;
			while (this.last.Next != null)
			{
				this.last = this.last.Next;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x000B5688 File Offset: 0x000B3888
		internal Opcode First
		{
			get
			{
				return this.first;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x000B5690 File Offset: 0x000B3890
		internal Opcode Last
		{
			get
			{
				return this.last;
			}
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000B5698 File Offset: 0x000B3898
		internal void Append(Opcode opcode)
		{
			if (this.last == null)
			{
				this.first = opcode;
				this.last = opcode;
				return;
			}
			this.last.Attach(opcode);
			opcode.Next = null;
			this.last = opcode;
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000B56CB File Offset: 0x000B38CB
		internal void Append(OpcodeBlock block)
		{
			if (this.last == null)
			{
				this.first = block.first;
				this.last = block.last;
				return;
			}
			this.last.Attach(block.first);
			this.last = block.last;
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000B570C File Offset: 0x000B390C
		internal void DetachLast()
		{
			if (this.last == null)
			{
				return;
			}
			Opcode prev = this.last.Prev;
			this.last.Prev = null;
			this.last = prev;
			if (this.last != null)
			{
				this.last.Next = null;
			}
		}

		// Token: 0x040025A8 RID: 9640
		private Opcode first;

		// Token: 0x040025A9 RID: 9641
		private Opcode last;
	}
}
