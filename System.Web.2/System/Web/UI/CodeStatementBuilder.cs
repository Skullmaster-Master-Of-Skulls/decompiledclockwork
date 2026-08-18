using System;
using System.CodeDom;

namespace System.Web.UI
{
	// Token: 0x02000257 RID: 599
	public abstract class CodeStatementBuilder : ControlBuilder
	{
		// Token: 0x06001BA8 RID: 7080
		public abstract CodeStatement BuildStatement(CodeArgumentReferenceExpression writerReferenceExpression);
	}
}
