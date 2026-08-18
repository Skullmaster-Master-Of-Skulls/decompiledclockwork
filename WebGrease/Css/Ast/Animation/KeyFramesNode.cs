using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Animation
{
	// Token: 0x02000115 RID: 277
	public sealed class KeyFramesNode : StyleSheetRuleNode
	{
		// Token: 0x06001126 RID: 4390 RVA: 0x0004C07E File Offset: 0x0004A27E
		public KeyFramesNode(string keyFramesSymbol, string identValue, string stringValue, ReadOnlyCollection<KeyFramesBlockNode> keyFramesBlockNodes)
		{
			this.KeyFramesSymbol = keyFramesSymbol;
			this.IdentValue = identValue;
			this.StringValue = stringValue;
			this.KeyFramesBlockNodes = (keyFramesBlockNodes ?? new List<KeyFramesBlockNode>(0).AsReadOnly());
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x0004C0B2 File Offset: 0x0004A2B2
		// (set) Token: 0x06001128 RID: 4392 RVA: 0x0004C0BA File Offset: 0x0004A2BA
		public string KeyFramesSymbol { get; private set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x0004C0C3 File Offset: 0x0004A2C3
		// (set) Token: 0x0600112A RID: 4394 RVA: 0x0004C0CB File Offset: 0x0004A2CB
		public string IdentValue { get; private set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x0004C0D4 File Offset: 0x0004A2D4
		// (set) Token: 0x0600112C RID: 4396 RVA: 0x0004C0DC File Offset: 0x0004A2DC
		public string StringValue { get; private set; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x0004C0E5 File Offset: 0x0004A2E5
		// (set) Token: 0x0600112E RID: 4398 RVA: 0x0004C0ED File Offset: 0x0004A2ED
		public ReadOnlyCollection<KeyFramesBlockNode> KeyFramesBlockNodes { get; private set; }

		// Token: 0x0600112F RID: 4399 RVA: 0x0004C0F6 File Offset: 0x0004A2F6
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitKeyFramesNode(this);
		}
	}
}
