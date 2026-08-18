using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast.Animation
{
	// Token: 0x02000114 RID: 276
	public sealed class KeyFramesBlockNode : AstNode
	{
		// Token: 0x06001120 RID: 4384 RVA: 0x0004C02E File Offset: 0x0004A22E
		public KeyFramesBlockNode(ReadOnlyCollection<string> keyFramesSelectors, ReadOnlyCollection<DeclarationNode> declarationNodes)
		{
			this.KeyFramesSelectors = keyFramesSelectors;
			this.DeclarationNodes = (declarationNodes ?? new List<DeclarationNode>(0).AsReadOnly());
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0004C053 File Offset: 0x0004A253
		// (set) Token: 0x06001122 RID: 4386 RVA: 0x0004C05B File Offset: 0x0004A25B
		public ReadOnlyCollection<string> KeyFramesSelectors { get; private set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0004C064 File Offset: 0x0004A264
		// (set) Token: 0x06001124 RID: 4388 RVA: 0x0004C06C File Offset: 0x0004A26C
		public ReadOnlyCollection<DeclarationNode> DeclarationNodes { get; private set; }

		// Token: 0x06001125 RID: 4389 RVA: 0x0004C075 File Offset: 0x0004A275
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitKeyFramesBlockNode(this);
		}
	}
}
