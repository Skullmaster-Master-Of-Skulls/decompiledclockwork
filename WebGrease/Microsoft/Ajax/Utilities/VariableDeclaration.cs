using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000CE RID: 206
	public sealed class VariableDeclaration : InitializerNode
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00041ADC File Offset: 0x0003FCDC
		// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x00041AE4 File Offset: 0x0003FCE4
		public bool IsCCSpecialCase { get; set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00041AED File Offset: 0x0003FCED
		// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x00041AF5 File Offset: 0x0003FCF5
		public bool UseCCOn { get; set; }

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00041AFE File Offset: 0x0003FCFE
		public VariableDeclaration(Context context) : base(context)
		{
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00041B07 File Offset: 0x0003FD07
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00041B13 File Offset: 0x0003FD13
		public override bool IsExpression
		{
			get
			{
				return true;
			}
		}
	}
}
