using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000505 RID: 1285
	internal class QueryTreeBuilder
	{
		// Token: 0x0600309C RID: 12444 RVA: 0x000BA5D0 File Offset: 0x000B87D0
		internal QueryTreeBuilder()
		{
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x000BA5D8 File Offset: 0x000B87D8
		internal Opcode LastOpcode
		{
			get
			{
				return this.lastOpcode;
			}
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000BA5E0 File Offset: 0x000B87E0
		internal Opcode Build(Opcode tree, OpcodeBlock newBlock)
		{
			if (tree == null)
			{
				this.lastOpcode = newBlock.Last;
				return newBlock.First;
			}
			this.diverger = new QueryTreeBuilder.Diverger(tree, newBlock.First);
			if (!this.diverger.Find())
			{
				this.lastOpcode = this.diverger.TreePath[this.diverger.TreePath.Count - 1];
				return tree;
			}
			if (this.diverger.TreeOpcode == null)
			{
				this.diverger.TreePath[this.diverger.TreePath.Count - 1].Attach(this.diverger.InsertOpcode);
			}
			else
			{
				this.diverger.TreeOpcode.Add(this.diverger.InsertOpcode);
			}
			this.lastOpcode = newBlock.Last;
			if (this.diverger.InsertOpcode.IsMultipleResult())
			{
				if (OpcodeID.Branch == this.diverger.TreeOpcode.ID)
				{
					OpcodeList branches = ((BranchOpcode)this.diverger.TreeOpcode).Branches;
					int i = 0;
					int count = branches.Count;
					while (i < count)
					{
						if (branches[i].IsMultipleResult())
						{
							this.lastOpcode = branches[i];
							break;
						}
						i++;
					}
				}
				else if (this.diverger.TreeOpcode.IsMultipleResult())
				{
					this.lastOpcode = this.diverger.TreeOpcode;
				}
			}
			this.FixupJumps();
			return tree;
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000BA760 File Offset: 0x000B8960
		private void FixupJumps()
		{
			QueryBuffer<Opcode> treePath = this.diverger.TreePath;
			QueryBuffer<Opcode> insertPath = this.diverger.InsertPath;
			for (int i = 0; i < insertPath.Count; i++)
			{
				if (insertPath[i].TestFlag(OpcodeFlags.Jump))
				{
					JumpOpcode jumpOpcode = (JumpOpcode)insertPath[i];
					if (-1 == insertPath.IndexOf(jumpOpcode.Jump, i + 1))
					{
						BlockEndOpcode jumpTo = (BlockEndOpcode)jumpOpcode.Jump;
						jumpOpcode.RemoveJump(jumpTo);
						JumpOpcode jumpOpcode2 = (JumpOpcode)treePath[i];
						jumpOpcode2.AddJump(jumpTo);
					}
				}
			}
		}

		// Token: 0x0400260C RID: 9740
		private QueryTreeBuilder.Diverger diverger;

		// Token: 0x0400260D RID: 9741
		private Opcode lastOpcode;

		// Token: 0x02000C4B RID: 3147
		internal struct Diverger
		{
			// Token: 0x06007774 RID: 30580 RVA: 0x001BDFBE File Offset: 0x001BC1BE
			internal Diverger(Opcode tree, Opcode insert)
			{
				this.treePath = new QueryBuffer<Opcode>(16);
				this.insertPath = new QueryBuffer<Opcode>(16);
				this.treeOpcode = tree;
				this.insertOpcode = insert;
			}

			// Token: 0x17001B4F RID: 6991
			// (get) Token: 0x06007775 RID: 30581 RVA: 0x001BDFE8 File Offset: 0x001BC1E8
			internal Opcode InsertOpcode
			{
				get
				{
					return this.insertOpcode;
				}
			}

			// Token: 0x17001B50 RID: 6992
			// (get) Token: 0x06007776 RID: 30582 RVA: 0x001BDFF0 File Offset: 0x001BC1F0
			internal QueryBuffer<Opcode> InsertPath
			{
				get
				{
					return this.insertPath;
				}
			}

			// Token: 0x17001B51 RID: 6993
			// (get) Token: 0x06007777 RID: 30583 RVA: 0x001BDFF8 File Offset: 0x001BC1F8
			internal Opcode TreeOpcode
			{
				get
				{
					return this.treeOpcode;
				}
			}

			// Token: 0x17001B52 RID: 6994
			// (get) Token: 0x06007778 RID: 30584 RVA: 0x001BE000 File Offset: 0x001BC200
			internal QueryBuffer<Opcode> TreePath
			{
				get
				{
					return this.treePath;
				}
			}

			// Token: 0x06007779 RID: 30585 RVA: 0x001BE008 File Offset: 0x001BC208
			internal bool Find()
			{
				while (this.treeOpcode != null || this.insertOpcode != null)
				{
					if (this.insertOpcode == null)
					{
						return false;
					}
					if (this.treeOpcode == null)
					{
						return true;
					}
					Opcode opcode;
					if (this.treeOpcode.TestFlag(OpcodeFlags.Branch))
					{
						opcode = this.treeOpcode.Locate(this.insertOpcode);
						if (opcode == null)
						{
							return true;
						}
						this.treeOpcode = opcode;
						opcode = opcode.Next;
					}
					else
					{
						if (!this.treeOpcode.Equals(this.insertOpcode))
						{
							return true;
						}
						opcode = this.treeOpcode.Next;
					}
					this.treePath.Add(this.treeOpcode);
					this.insertPath.Add(this.insertOpcode);
					this.insertOpcode = this.insertOpcode.Next;
					this.treeOpcode = opcode;
				}
				return false;
			}

			// Token: 0x04004454 RID: 17492
			private Opcode treeOpcode;

			// Token: 0x04004455 RID: 17493
			private QueryBuffer<Opcode> treePath;

			// Token: 0x04004456 RID: 17494
			private QueryBuffer<Opcode> insertPath;

			// Token: 0x04004457 RID: 17495
			private Opcode insertOpcode;
		}
	}
}
