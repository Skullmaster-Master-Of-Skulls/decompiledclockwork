using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000C9 RID: 201
	public sealed class TryNode : AstNode
	{
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00040D42 File Offset: 0x0003EF42
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x00040D8B File Offset: 0x0003EF8B
		public Block TryBlock
		{
			get
			{
				return this.m_tryBlock;
			}
			set
			{
				this.m_tryBlock.IfNotNull((Block n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_tryBlock = value;
				this.m_tryBlock.IfNotNull(delegate(Block n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00040DC4 File Offset: 0x0003EFC4
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x00040E0B File Offset: 0x0003F00B
		public Block CatchBlock
		{
			get
			{
				return this.m_catchBlock;
			}
			set
			{
				this.m_catchBlock.IfNotNull((Block n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_catchBlock = value;
				this.m_catchBlock.IfNotNull(delegate(Block n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00040E44 File Offset: 0x0003F044
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x00040E8B File Offset: 0x0003F08B
		public Block FinallyBlock
		{
			get
			{
				return this.m_finallyBlock;
			}
			set
			{
				this.m_finallyBlock.IfNotNull((Block n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_finallyBlock = value;
				this.m_finallyBlock.IfNotNull(delegate(Block n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00040EC4 File Offset: 0x0003F0C4
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x00040F0B File Offset: 0x0003F10B
		public ParameterDeclaration CatchParameter
		{
			get
			{
				return this.m_catchParameter;
			}
			set
			{
				this.m_catchParameter.IfNotNull((ParameterDeclaration n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_catchParameter = value;
				this.m_catchParameter.IfNotNull(delegate(ParameterDeclaration n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x00040F44 File Offset: 0x0003F144
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x00040F4C File Offset: 0x0003F14C
		public Context CatchContext { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00040F55 File Offset: 0x0003F155
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x00040F5D File Offset: 0x0003F15D
		public Context FinallyContext { get; set; }

		// Token: 0x06000D9E RID: 3486 RVA: 0x00040F66 File Offset: 0x0003F166
		public TryNode(Context context) : base(context)
		{
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00040F6F File Offset: 0x0003F16F
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00040F7B File Offset: 0x0003F17B
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.TryBlock, this.CatchParameter, this.CatchBlock, this.FinallyBlock);
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00040FA4 File Offset: 0x0003F1A4
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.TryBlock == oldNode)
			{
				this.TryBlock = AstNode.ForceToBlock(newNode);
				return true;
			}
			if (this.CatchParameter == oldNode)
			{
				return (newNode as ParameterDeclaration).IfNotNull(delegate(ParameterDeclaration p)
				{
					this.CatchParameter = p;
					return true;
				});
			}
			if (this.CatchBlock == oldNode)
			{
				this.CatchBlock = AstNode.ForceToBlock(newNode);
				return true;
			}
			if (this.FinallyBlock == oldNode)
			{
				this.FinallyBlock = AstNode.ForceToBlock(newNode);
				return true;
			}
			return false;
		}

		// Token: 0x04000546 RID: 1350
		private Block m_tryBlock;

		// Token: 0x04000547 RID: 1351
		private Block m_catchBlock;

		// Token: 0x04000548 RID: 1352
		private Block m_finallyBlock;

		// Token: 0x04000549 RID: 1353
		private ParameterDeclaration m_catchParameter;
	}
}
