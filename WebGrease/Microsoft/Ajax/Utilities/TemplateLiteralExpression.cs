using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000021 RID: 33
	public class TemplateLiteralExpression : AstNode
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00006C36 File Offset: 0x00004E36
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00006C7F File Offset: 0x00004E7F
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00006CB8 File Offset: 0x00004EB8
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00006CC0 File Offset: 0x00004EC0
		public string Text { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00006CC9 File Offset: 0x00004EC9
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00006CD1 File Offset: 0x00004ED1
		public Context TextContext { get; set; }

		// Token: 0x060002BF RID: 703 RVA: 0x00006CDA File Offset: 0x00004EDA
		public TemplateLiteralExpression(Context context) : base(context)
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00006CE3 File Offset: 0x00004EE3
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00006CEF File Offset: 0x00004EEF
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.m_expression, null, null, null);
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00006CFF File Offset: 0x00004EFF
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Expression == oldNode)
			{
				this.Expression = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x0400007C RID: 124
		private AstNode m_expression;
	}
}
