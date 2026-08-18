using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013D5 RID: 5077
	internal class Float : ToBeImplementedElement
	{
		// Token: 0x0600D1DB RID: 53723 RVA: 0x002E7E08 File Offset: 0x002E6008
		public new static FObj.Maker GetMaker()
		{
			return new Float.Maker();
		}

		// Token: 0x0600D1DC RID: 53724 RVA: 0x002E7E0F File Offset: 0x002E600F
		protected Float(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:float";
		}

		// Token: 0x0600D1DD RID: 53725 RVA: 0x002E7E24 File Offset: 0x002E6024
		public override Status Layout(Area area)
		{
			return base.Layout(area);
		}

		// Token: 0x020013D6 RID: 5078
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D1DE RID: 53726 RVA: 0x002E7E2D File Offset: 0x002E602D
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Float(parent, propertyList);
			}
		}
	}
}
