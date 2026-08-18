using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x02001405 RID: 5125
	internal class TableAndCaption : ToBeImplementedElement
	{
		// Token: 0x0600D27A RID: 53882 RVA: 0x002EB3C6 File Offset: 0x002E95C6
		public new static FObj.Maker GetMaker()
		{
			return new TableAndCaption.Maker();
		}

		// Token: 0x0600D27B RID: 53883 RVA: 0x002EB3CD File Offset: 0x002E95CD
		protected TableAndCaption(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:table-and-caption";
		}

		// Token: 0x0600D27C RID: 53884 RVA: 0x002EB3E4 File Offset: 0x002E95E4
		public override Status Layout(Area area)
		{
			this.propMgr.GetAccessibilityProps();
			this.propMgr.GetAuralProps();
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			this.propMgr.GetMarginProps();
			this.propMgr.GetRelativePositionProps();
			return base.Layout(area);
		}

		// Token: 0x02001406 RID: 5126
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D27D RID: 53885 RVA: 0x002EB440 File Offset: 0x002E9640
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new TableAndCaption(parent, propertyList);
			}
		}
	}
}
