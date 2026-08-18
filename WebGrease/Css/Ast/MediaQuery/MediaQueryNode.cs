using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.MediaQuery
{
	// Token: 0x02000120 RID: 288
	public sealed class MediaQueryNode : AstNode
	{
		// Token: 0x06001178 RID: 4472 RVA: 0x0004C5FB File Offset: 0x0004A7FB
		public MediaQueryNode(string onlyText, string notText, string mediaType, ReadOnlyCollection<MediaExpressionNode> mediaExpressions)
		{
			this.OnlyText = onlyText;
			this.NotText = notText;
			this.MediaType = mediaType;
			this.MediaExpressions = (mediaExpressions ?? new List<MediaExpressionNode>(0).AsReadOnly());
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0004C62F File Offset: 0x0004A82F
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x0004C637 File Offset: 0x0004A837
		public string OnlyText { get; private set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x0004C640 File Offset: 0x0004A840
		// (set) Token: 0x0600117C RID: 4476 RVA: 0x0004C648 File Offset: 0x0004A848
		public string NotText { get; private set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x0004C651 File Offset: 0x0004A851
		// (set) Token: 0x0600117E RID: 4478 RVA: 0x0004C659 File Offset: 0x0004A859
		public string MediaType { get; private set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0004C662 File Offset: 0x0004A862
		// (set) Token: 0x06001180 RID: 4480 RVA: 0x0004C66A File Offset: 0x0004A86A
		public ReadOnlyCollection<MediaExpressionNode> MediaExpressions { get; private set; }

		// Token: 0x06001181 RID: 4481 RVA: 0x0004C673 File Offset: 0x0004A873
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitMediaQueryNode(this);
		}
	}
}
