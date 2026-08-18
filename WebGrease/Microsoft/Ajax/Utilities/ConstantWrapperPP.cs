using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000081 RID: 129
	public class ConstantWrapperPP : Expression
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0002497C File Offset: 0x00022B7C
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x00024984 File Offset: 0x00022B84
		public string VarName { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0002498D File Offset: 0x00022B8D
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x00024995 File Offset: 0x00022B95
		public bool ForceComments { get; set; }

		// Token: 0x060007F1 RID: 2033 RVA: 0x0002499E File Offset: 0x00022B9E
		public ConstantWrapperPP(Context context) : base(context)
		{
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000249A7 File Offset: 0x00022BA7
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
