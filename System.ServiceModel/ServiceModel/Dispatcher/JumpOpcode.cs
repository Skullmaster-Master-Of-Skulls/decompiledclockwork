using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200047B RID: 1147
	internal abstract class JumpOpcode : Opcode
	{
		// Token: 0x06002CA4 RID: 11428 RVA: 0x000AE4F0 File Offset: 0x000AC6F0
		internal JumpOpcode(OpcodeID id, Opcode jump) : base(id)
		{
			this.Jump = jump;
			this.flags |= OpcodeFlags.Jump;
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06002CA5 RID: 11429 RVA: 0x000AE50F File Offset: 0x000AC70F
		// (set) Token: 0x06002CA6 RID: 11430 RVA: 0x000AE517 File Offset: 0x000AC717
		internal Opcode Jump
		{
			get
			{
				return this.jump;
			}
			set
			{
				this.AddJump((BlockEndOpcode)value);
			}
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000AE528 File Offset: 0x000AC728
		internal void AddJump(BlockEndOpcode jumpTo)
		{
			bool flag = base.IsReachableFromConditional();
			if (flag)
			{
				this.prev.DelinkFromConditional(this);
			}
			if (this.jump == null)
			{
				this.jump = jumpTo;
			}
			else
			{
				BranchOpcode branchOpcode;
				if (this.jump.ID == OpcodeID.Branch)
				{
					branchOpcode = (BranchOpcode)this.jump;
				}
				else
				{
					BlockEndOpcode opcode = (BlockEndOpcode)this.jump;
					branchOpcode = new BranchOpcode();
					branchOpcode.Branches.Add(opcode);
					this.jump = branchOpcode;
				}
				branchOpcode.Branches.Add(jumpTo);
			}
			jumpTo.LinkJump(this);
			if (flag && this.jump != null)
			{
				this.prev.LinkToConditional(this);
			}
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000AE5C7 File Offset: 0x000AC7C7
		internal override void Remove()
		{
			if (this.jump == null)
			{
				base.Remove();
			}
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000AE5D8 File Offset: 0x000AC7D8
		internal void RemoveJump(BlockEndOpcode jumpTo)
		{
			bool flag = base.IsReachableFromConditional();
			if (flag)
			{
				this.prev.DelinkFromConditional(this);
			}
			if (this.jump.ID == OpcodeID.Branch)
			{
				BranchOpcode branchOpcode = (BranchOpcode)this.jump;
				jumpTo.DeLinkJump(this);
				branchOpcode.RemoveChild(jumpTo);
				if (branchOpcode.Branches.Count == 0)
				{
					this.jump = null;
				}
			}
			else
			{
				jumpTo.DeLinkJump(this);
				this.jump = null;
			}
			if (flag && this.jump != null)
			{
				this.prev.LinkToConditional(this);
			}
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x000AE65E File Offset: 0x000AC85E
		internal override void Trim()
		{
			if (this.jump.ID == OpcodeID.Branch)
			{
				this.jump.Trim();
			}
		}

		// Token: 0x04002449 RID: 9289
		private Opcode jump;
	}
}
