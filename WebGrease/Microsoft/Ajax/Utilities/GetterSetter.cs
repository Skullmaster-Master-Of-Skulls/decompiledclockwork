using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000096 RID: 150
	public sealed class GetterSetter : ObjectLiteralField
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00029508 File Offset: 0x00027708
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x00029510 File Offset: 0x00027710
		public bool IsGetter { get; set; }

		// Token: 0x06000919 RID: 2329 RVA: 0x00029519 File Offset: 0x00027719
		public GetterSetter(string identifier, bool isGetter, Context context) : base(identifier, PrimitiveType.String, context)
		{
			this.IsGetter = isGetter;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0002952B File Offset: 0x0002772B
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00029537 File Offset: 0x00027737
		public override string ToString()
		{
			return base.Value.ToString();
		}
	}
}
