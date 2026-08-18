using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000089 RID: 137
	public class DirectivePrologue : ConstantWrapper
	{
		// Token: 0x06000847 RID: 2119 RVA: 0x00025627 File Offset: 0x00023827
		public DirectivePrologue(string value, Context context) : base(value, PrimitiveType.String, context)
		{
			this.UseStrict = (string.CompareOrdinal(base.Context.Code, 1, "use strict", 0, 10) == 0);
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x00025654 File Offset: 0x00023854
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x0002565C File Offset: 0x0002385C
		public bool UseStrict { get; private set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00025665 File Offset: 0x00023865
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x0002566D File Offset: 0x0002386D
		public bool IsRedundant { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00025676 File Offset: 0x00023876
		public override bool IsExpression
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00025679 File Offset: 0x00023879
		public override bool IsConstant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0002567C File Offset: 0x0002387C
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
