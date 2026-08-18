using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x02000031 RID: 49
	public sealed class ImportantCommentNode : AstNode
	{
		// Token: 0x06000375 RID: 885 RVA: 0x0000833F File Offset: 0x0000653F
		public ImportantCommentNode(string text)
		{
			this.Text = text;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000834E File Offset: 0x0000654E
		// (set) Token: 0x06000377 RID: 887 RVA: 0x00008356 File Offset: 0x00006556
		public string Text { get; private set; }

		// Token: 0x06000378 RID: 888 RVA: 0x0000835F File Offset: 0x0000655F
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitImportantCommentNode(this);
		}
	}
}
