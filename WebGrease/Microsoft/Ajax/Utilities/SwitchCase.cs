using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000C6 RID: 198
	public sealed class SwitchCase : AstNode
	{
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x00040ACF File Offset: 0x0003ECCF
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x00040B17 File Offset: 0x0003ED17
		public AstNode CaseValue
		{
			get
			{
				return this.m_caseValue;
			}
			set
			{
				this.m_caseValue.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_caseValue = value;
				this.m_caseValue.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00040B50 File Offset: 0x0003ED50
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x00040B97 File Offset: 0x0003ED97
		public Block Statements
		{
			get
			{
				return this.m_statements;
			}
			set
			{
				this.m_statements.IfNotNull((Block n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_statements = value;
				this.m_statements.IfNotNull(delegate(Block n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00040BD0 File Offset: 0x0003EDD0
		internal bool IsDefault
		{
			get
			{
				return this.CaseValue == null;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x00040BDB File Offset: 0x0003EDDB
		// (set) Token: 0x06000D7E RID: 3454 RVA: 0x00040BE3 File Offset: 0x0003EDE3
		public Context ColonContext { get; set; }

		// Token: 0x06000D7F RID: 3455 RVA: 0x00040BEC File Offset: 0x0003EDEC
		public SwitchCase(Context context) : base(context)
		{
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00040BF5 File Offset: 0x0003EDF5
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00040C01 File Offset: 0x0003EE01
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.CaseValue, this.Statements, null, null);
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00040C18 File Offset: 0x0003EE18
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.CaseValue == oldNode)
			{
				this.CaseValue = newNode;
				return true;
			}
			if (this.Statements == oldNode)
			{
				Block block = newNode as Block;
				if (newNode == null || block != null)
				{
					this.Statements = block;
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000542 RID: 1346
		private AstNode m_caseValue;

		// Token: 0x04000543 RID: 1347
		private Block m_statements;
	}
}
