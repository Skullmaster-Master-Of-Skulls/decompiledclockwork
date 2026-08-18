using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013CB RID: 5067
	internal class BidiOverride : ToBeImplementedElement
	{
		// Token: 0x0600D1BB RID: 53691 RVA: 0x002E6A86 File Offset: 0x002E4C86
		public new static FObj.Maker GetMaker()
		{
			return new BidiOverride.Maker();
		}

		// Token: 0x0600D1BC RID: 53692 RVA: 0x002E6A8D File Offset: 0x002E4C8D
		protected BidiOverride(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:bidi-override";
		}

		// Token: 0x0600D1BD RID: 53693 RVA: 0x002E6AA2 File Offset: 0x002E4CA2
		public override Status Layout(Area area)
		{
			this.propMgr.GetAuralProps();
			this.propMgr.GetRelativePositionProps();
			return base.Layout(area);
		}

		// Token: 0x020013CC RID: 5068
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D1BE RID: 53694 RVA: 0x002E6AC3 File Offset: 0x002E4CC3
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new BidiOverride(parent, propertyList);
			}
		}
	}
}
