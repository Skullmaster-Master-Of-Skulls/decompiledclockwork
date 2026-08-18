using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000481 RID: 1153
	internal class BlockEndOpcode : Opcode
	{
		// Token: 0x06002CBA RID: 11450 RVA: 0x000AE97E File Offset: 0x000ACB7E
		internal BlockEndOpcode() : base(OpcodeID.BlockEnd)
		{
			this.sourceJumps = new QueryBuffer<Opcode>(1);
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x000AE994 File Offset: 0x000ACB94
		internal void DeLinkJump(Opcode jump)
		{
			this.sourceJumps.Remove(jump);
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000AE9A2 File Offset: 0x000ACBA2
		internal void LinkJump(Opcode jump)
		{
			this.sourceJumps.Add(jump);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000AE9B0 File Offset: 0x000ACBB0
		internal override void Remove()
		{
			while (this.sourceJumps.Count > 0)
			{
				((JumpOpcode)this.sourceJumps[0]).RemoveJump(this);
			}
			base.Remove();
		}

		// Token: 0x0400244C RID: 9292
		private QueryBuffer<Opcode> sourceJumps;
	}
}
