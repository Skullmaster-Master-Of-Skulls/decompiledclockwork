using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000099 RID: 153
	public sealed class IfNode : AstNode
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0002A079 File Offset: 0x00028279
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x0002A0C3 File Offset: 0x000282C3
		public AstNode Condition
		{
			get
			{
				return this.m_condition;
			}
			set
			{
				this.m_condition.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_condition = value;
				this.m_condition.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0002A0FC File Offset: 0x000282FC
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x0002A143 File Offset: 0x00028343
		public Block TrueBlock
		{
			get
			{
				return this.m_trueBlock;
			}
			set
			{
				this.m_trueBlock.IfNotNull((Block n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_trueBlock = value;
				this.m_trueBlock.IfNotNull(delegate(Block n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0002A17C File Offset: 0x0002837C
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0002A1C3 File Offset: 0x000283C3
		public Block FalseBlock
		{
			get
			{
				return this.m_falseBlock;
			}
			set
			{
				this.m_falseBlock.IfNotNull((Block n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_falseBlock = value;
				this.m_falseBlock.IfNotNull(delegate(Block n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0002A1FC File Offset: 0x000283FC
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x0002A204 File Offset: 0x00028404
		public Context ElseContext { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0002A218 File Offset: 0x00028418
		public override Context TerminatingContext
		{
			get
			{
				Context context = base.TerminatingContext;
				if (context == null)
				{
					if (this.FalseBlock != null)
					{
						context = this.FalseBlock.TerminatingContext;
					}
					else
					{
						context = this.TrueBlock.IfNotNull((Block b) => b.TerminatingContext);
					}
				}
				return context;
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0002A26F File Offset: 0x0002846F
		public IfNode(Context context) : base(context)
		{
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0002A278 File Offset: 0x00028478
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0002A284 File Offset: 0x00028484
		public void SwapBranches()
		{
			Block trueBlock = this.m_trueBlock;
			this.m_trueBlock = this.m_falseBlock;
			this.m_falseBlock = trueBlock;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0002A2AB File Offset: 0x000284AB
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Condition, this.TrueBlock, this.FalseBlock, null);
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0002A2C8 File Offset: 0x000284C8
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Condition == oldNode)
			{
				this.Condition = newNode;
				return true;
			}
			if (this.TrueBlock == oldNode)
			{
				this.TrueBlock = AstNode.ForceToBlock(newNode);
				return true;
			}
			if (this.FalseBlock == oldNode)
			{
				this.FalseBlock = AstNode.ForceToBlock(newNode);
				return true;
			}
			return false;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0002A318 File Offset: 0x00028518
		internal override bool EncloseBlock(EncloseBlockType type)
		{
			if (this.FalseBlock != null && (this.FalseBlock.ForceBraces || this.FalseBlock.Count > 0))
			{
				return this.FalseBlock.EncloseBlock(type);
			}
			return type == EncloseBlockType.IfWithoutElse || (this.TrueBlock != null && this.TrueBlock.EncloseBlock(type));
		}

		// Token: 0x04000349 RID: 841
		private AstNode m_condition;

		// Token: 0x0400034A RID: 842
		private Block m_trueBlock;

		// Token: 0x0400034B RID: 843
		private Block m_falseBlock;
	}
}
