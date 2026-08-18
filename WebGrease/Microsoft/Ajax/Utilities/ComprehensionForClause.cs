using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200000F RID: 15
	public class ComprehensionForClause : ComprehensionClause
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000039AA File Offset: 0x00001BAA
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000039F3 File Offset: 0x00001BF3
		public AstNode Binding
		{
			get
			{
				return this.m_binding;
			}
			set
			{
				this.m_binding.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_binding = value;
				this.m_binding.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003A2C File Offset: 0x00001C2C
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00003A34 File Offset: 0x00001C34
		public bool IsInOperation { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00003A3D File Offset: 0x00001C3D
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00003A45 File Offset: 0x00001C45
		public Context OfContext { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00003A4E File Offset: 0x00001C4E
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00003A97 File Offset: 0x00001C97
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

		// Token: 0x0600012E RID: 302 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public ComprehensionForClause(Context context) : base(context)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00003AD9 File Offset: 0x00001CD9
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00003AE5 File Offset: 0x00001CE5
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Binding, this.Expression, null, null);
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003AFA File Offset: 0x00001CFA
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Binding == oldNode)
			{
				this.Binding = newNode;
				return true;
			}
			if (this.Expression == oldNode)
			{
				this.Expression = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x0400002B RID: 43
		private AstNode m_binding;

		// Token: 0x0400002C RID: 44
		private AstNode m_expression;
	}
}
