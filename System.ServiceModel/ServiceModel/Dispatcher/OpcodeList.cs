using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004D4 RID: 1236
	internal class OpcodeList
	{
		// Token: 0x06002EDF RID: 11999 RVA: 0x000B5755 File Offset: 0x000B3955
		public OpcodeList(int capacity)
		{
			this.opcodes = new QueryBuffer<Opcode>(capacity);
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06002EE0 RID: 12000 RVA: 0x000B5769 File Offset: 0x000B3969
		public int Count
		{
			get
			{
				return this.opcodes.count;
			}
		}

		// Token: 0x17000B1F RID: 2847
		public Opcode this[int index]
		{
			get
			{
				return this.opcodes[index];
			}
			set
			{
				this.opcodes[index] = value;
			}
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000B5793 File Offset: 0x000B3993
		public void Add(Opcode opcode)
		{
			this.opcodes.Add(opcode);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000B57A1 File Offset: 0x000B39A1
		public int IndexOf(Opcode opcode)
		{
			return this.opcodes.IndexOf(opcode);
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000B57AF File Offset: 0x000B39AF
		public void Remove(Opcode opcode)
		{
			this.opcodes.Remove(opcode);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000B57BD File Offset: 0x000B39BD
		public void Trim()
		{
			this.opcodes.TrimToCount();
		}

		// Token: 0x040025AA RID: 9642
		private QueryBuffer<Opcode> opcodes;
	}
}
