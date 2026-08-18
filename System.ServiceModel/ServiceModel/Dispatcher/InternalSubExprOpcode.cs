using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000504 RID: 1284
	internal class InternalSubExprOpcode : SubExprOpcode
	{
		// Token: 0x06003099 RID: 12441 RVA: 0x000BA58C File Offset: 0x000B878C
		internal InternalSubExprOpcode(SubExpr expr) : base(expr)
		{
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000BA595 File Offset: 0x000B8795
		internal override Opcode Eval(ProcessingContext context)
		{
			if (!context.LoadVariable(this.expr.Variable))
			{
				this.expr.Eval(context);
			}
			return this.next;
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000BA5BC File Offset: 0x000B87BC
		internal override Opcode EvalSpecial(ProcessingContext context)
		{
			this.expr.EvalSpecial(context);
			return this.next;
		}
	}
}
