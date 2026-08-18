using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013DD RID: 5085
	internal class InitialPropertySet : ToBeImplementedElement
	{
		// Token: 0x0600D1F9 RID: 53753 RVA: 0x002E853B File Offset: 0x002E673B
		public new static FObj.Maker GetMaker()
		{
			return new InitialPropertySet.Maker();
		}

		// Token: 0x0600D1FA RID: 53754 RVA: 0x002E8542 File Offset: 0x002E6742
		protected InitialPropertySet(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:initial-property-set";
		}

		// Token: 0x0600D1FB RID: 53755 RVA: 0x002E8558 File Offset: 0x002E6758
		public override Status Layout(Area area)
		{
			this.propMgr.GetAccessibilityProps();
			this.propMgr.GetAuralProps();
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			this.propMgr.GetRelativePositionProps();
			return base.Layout(area);
		}

		// Token: 0x020013DE RID: 5086
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D1FC RID: 53756 RVA: 0x002E85A8 File Offset: 0x002E67A8
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new InitialPropertySet(parent, propertyList);
			}
		}
	}
}
