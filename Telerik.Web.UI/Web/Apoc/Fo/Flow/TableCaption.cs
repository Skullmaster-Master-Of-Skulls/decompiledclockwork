using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x02001409 RID: 5129
	internal class TableCaption : ToBeImplementedElement
	{
		// Token: 0x0600D283 RID: 53891 RVA: 0x002EB47E File Offset: 0x002E967E
		public new static FObj.Maker GetMaker()
		{
			return new TableCaption.Maker();
		}

		// Token: 0x0600D284 RID: 53892 RVA: 0x002EB485 File Offset: 0x002E9685
		protected TableCaption(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:table-caption";
		}

		// Token: 0x0600D285 RID: 53893 RVA: 0x002EB49C File Offset: 0x002E969C
		public override Status Layout(Area area)
		{
			this.propMgr.GetAccessibilityProps();
			this.propMgr.GetAuralProps();
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			this.propMgr.GetRelativePositionProps();
			return base.Layout(area);
		}

		// Token: 0x0200140A RID: 5130
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D286 RID: 53894 RVA: 0x002EB4EC File Offset: 0x002E96EC
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new TableCaption(parent, propertyList);
			}
		}
	}
}
