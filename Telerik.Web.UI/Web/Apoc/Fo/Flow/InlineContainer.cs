using System;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013DF RID: 5087
	internal class InlineContainer : ToBeImplementedElement
	{
		// Token: 0x0600D1FE RID: 53758 RVA: 0x002E85B9 File Offset: 0x002E67B9
		public new static FObj.Maker GetMaker()
		{
			return new InlineContainer.Maker();
		}

		// Token: 0x0600D1FF RID: 53759 RVA: 0x002E85C0 File Offset: 0x002E67C0
		protected InlineContainer(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:inline-container";
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			this.propMgr.GetMarginInlineProps();
			this.propMgr.GetRelativePositionProps();
		}

		// Token: 0x020013E0 RID: 5088
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D200 RID: 53760 RVA: 0x002E8610 File Offset: 0x002E6810
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new InlineContainer(parent, propertyList);
			}
		}
	}
}
