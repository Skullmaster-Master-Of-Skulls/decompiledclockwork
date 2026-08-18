using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x02000033 RID: 51
	public class StyleSheetRuleOrCommentNode : StyleSheetRuleNode
	{
		// Token: 0x0600037A RID: 890 RVA: 0x00008370 File Offset: 0x00006570
		public StyleSheetRuleOrCommentNode(ImportantCommentNode comment, bool isComment)
		{
			this.ImportantCommentNode = comment;
			this.IsCommentNode = isComment;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00008386 File Offset: 0x00006586
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000838E File Offset: 0x0000658E
		public ImportantCommentNode ImportantCommentNode { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00008397 File Offset: 0x00006597
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000839F File Offset: 0x0000659F
		public bool IsCommentNode { get; set; }

		// Token: 0x0600037F RID: 895 RVA: 0x000083A8 File Offset: 0x000065A8
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			if (this.IsCommentNode)
			{
				return new StyleSheetRuleOrCommentNode((ImportantCommentNode)this.ImportantCommentNode.Accept(nodeVisitor), true);
			}
			return base.Accept(nodeVisitor);
		}
	}
}
