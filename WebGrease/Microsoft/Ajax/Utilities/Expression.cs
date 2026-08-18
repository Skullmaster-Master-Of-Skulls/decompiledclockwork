using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000012 RID: 18
	public abstract class Expression : AstNode
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00003BDE File Offset: 0x00001DDE
		protected Expression(Context context) : base(context)
		{
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00003BE7 File Offset: 0x00001DE7
		public override bool IsExpression
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00003BEA File Offset: 0x00001DEA
		public override OperatorPrecedence Precedence
		{
			get
			{
				return OperatorPrecedence.Primary;
			}
		}
	}
}
