using System;
using WebGrease.Css.Ast;
using WebGrease.Css.Extensions;

namespace WebGrease.Css.Visitor
{
	// Token: 0x0200019D RID: 413
	public sealed class ExcludePropertyVisitor : NodeTransformVisitor
	{
		// Token: 0x06001532 RID: 5426 RVA: 0x0007ADBE File Offset: 0x00078FBE
		public override AstNode VisitDeclarationNode(DeclarationNode declarationNode)
		{
			if (declarationNode == null)
			{
				throw new ArgumentNullException("declarationNode");
			}
			if (!declarationNode.MinifyPrint().Contains("Exclude"))
			{
				return declarationNode;
			}
			return null;
		}

		// Token: 0x04000B5C RID: 2908
		private const string ExcludedSubstring = "Exclude";
	}
}
