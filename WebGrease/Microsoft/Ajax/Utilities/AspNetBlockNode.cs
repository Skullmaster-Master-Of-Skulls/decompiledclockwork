using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000064 RID: 100
	public sealed class AspNetBlockNode : AstNode
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x000210E1 File Offset: 0x0001F2E1
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x000210E9 File Offset: 0x0001F2E9
		public bool IsTerminatedByExplicitSemicolon { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x000210F2 File Offset: 0x0001F2F2
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x000210FA File Offset: 0x0001F2FA
		public string AspNetBlockText { get; set; }

		// Token: 0x0600069A RID: 1690 RVA: 0x00021103 File Offset: 0x0001F303
		public AspNetBlockNode(Context context) : base(context)
		{
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0002110C File Offset: 0x0001F30C
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
