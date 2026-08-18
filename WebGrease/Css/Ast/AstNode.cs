using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x02000030 RID: 48
	public abstract class AstNode
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00008334 File Offset: 0x00006534
		public virtual AstNode Accept(NodeVisitor nodeVisitor)
		{
			return this;
		}
	}
}
