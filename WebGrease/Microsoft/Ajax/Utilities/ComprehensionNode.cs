using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000013 RID: 19
	public class ComprehensionNode : Expression
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00003BEE File Offset: 0x00001DEE
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public ComprehensionType ComprehensionType { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00003BFF File Offset: 0x00001DFF
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00003C07 File Offset: 0x00001E07
		public bool MozillaOrdering { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00003C10 File Offset: 0x00001E10
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00003C18 File Offset: 0x00001E18
		public Context OpenDelimiter { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00003C21 File Offset: 0x00001E21
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00003C6B File Offset: 0x00001E6B
		public AstNode Expression
		{
			get
			{
				return this.m_expression;
			}
			set
			{
				this.m_expression.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_expression = value;
				this.m_expression.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00003CA4 File Offset: 0x00001EA4
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00003CEB File Offset: 0x00001EEB
		public AstNodeList Clauses
		{
			get
			{
				return this.m_clauses;
			}
			set
			{
				this.m_clauses.IfNotNull((AstNodeList n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_clauses = value;
				this.m_clauses.IfNotNull(delegate(AstNodeList n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00003D24 File Offset: 0x00001F24
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00003D2C File Offset: 0x00001F2C
		public Context CloseDelimiter { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00003D35 File Offset: 0x00001F35
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00003D3D File Offset: 0x00001F3D
		public BlockScope BlockScope { get; set; }

		// Token: 0x0600014F RID: 335 RVA: 0x00003D46 File Offset: 0x00001F46
		public ComprehensionNode(Context context) : base(context)
		{
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003D4F File Offset: 0x00001F4F
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00003D5B File Offset: 0x00001F5B
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.m_clauses, this.m_expression, null, null);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00003D7C File Offset: 0x00001F7C
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Expression == oldNode)
			{
				this.Expression = newNode;
				return true;
			}
			if (this.Clauses == oldNode)
			{
				return (newNode as AstNodeList).IfNotNull(delegate(AstNodeList list)
				{
					this.Clauses = list;
					return true;
				});
			}
			return false;
		}

		// Token: 0x04000033 RID: 51
		private AstNode m_expression;

		// Token: 0x04000034 RID: 52
		private AstNodeList m_clauses;
	}
}
