using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200006A RID: 106
	public sealed class Break : AstNode
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x000221F6 File Offset: 0x000203F6
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x000221FE File Offset: 0x000203FE
		public string Label { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00022207 File Offset: 0x00020407
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0002220F File Offset: 0x0002040F
		public Context LabelContext { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00022218 File Offset: 0x00020418
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x00022220 File Offset: 0x00020420
		public LabelInfo LabelInfo { get; set; }

		// Token: 0x060006FB RID: 1787 RVA: 0x00022229 File Offset: 0x00020429
		public Break(Context context) : base(context)
		{
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00022232 File Offset: 0x00020432
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
