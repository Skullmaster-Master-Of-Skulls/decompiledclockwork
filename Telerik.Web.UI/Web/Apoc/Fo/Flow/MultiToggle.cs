using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013F7 RID: 5111
	internal class MultiToggle : ToBeImplementedElement
	{
		// Token: 0x0600D242 RID: 53826 RVA: 0x002E9A1F File Offset: 0x002E7C1F
		public new static FObj.Maker GetMaker()
		{
			return new MultiToggle.Maker();
		}

		// Token: 0x0600D243 RID: 53827 RVA: 0x002E9A26 File Offset: 0x002E7C26
		protected MultiToggle(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:multi-toggle";
		}

		// Token: 0x0600D244 RID: 53828 RVA: 0x002E9A3B File Offset: 0x002E7C3B
		public override Status Layout(Area area)
		{
			this.propMgr.GetAccessibilityProps();
			return base.Layout(area);
		}

		// Token: 0x020013F8 RID: 5112
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D245 RID: 53829 RVA: 0x002E9A50 File Offset: 0x002E7C50
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new MultiToggle(parent, propertyList);
			}
		}
	}
}
