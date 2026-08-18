using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000C7 RID: 199
	public sealed class ThisLiteral : Expression
	{
		// Token: 0x06000D87 RID: 3463 RVA: 0x00040C57 File Offset: 0x0003EE57
		public ThisLiteral(Context context) : base(context)
		{
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00040C60 File Offset: 0x0003EE60
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00040C6C File Offset: 0x0003EE6C
		public override bool IsEquivalentTo(AstNode otherNode)
		{
			ThisLiteral thisLiteral = otherNode as ThisLiteral;
			return thisLiteral != null;
		}
	}
}
