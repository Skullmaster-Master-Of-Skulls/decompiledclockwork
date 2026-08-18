using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020015C4 RID: 5572
	internal class Unknown : FObj
	{
		// Token: 0x0600D94E RID: 55630 RVA: 0x002FB446 File Offset: 0x002F9646
		public new static FObj.Maker GetMaker()
		{
			return new Unknown.Maker();
		}

		// Token: 0x0600D94F RID: 55631 RVA: 0x002FB44D File Offset: 0x002F964D
		protected Unknown(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "unknown";
		}

		// Token: 0x0600D950 RID: 55632 RVA: 0x002FB462 File Offset: 0x002F9662
		public override Status Layout(Area area)
		{
			return new Status(1);
		}

		// Token: 0x020015C5 RID: 5573
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D951 RID: 55633 RVA: 0x002FB46A File Offset: 0x002F966A
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Unknown(parent, propertyList);
			}
		}
	}
}
