using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013F1 RID: 5105
	internal class MultiProperties : ToBeImplementedElement
	{
		// Token: 0x0600D233 RID: 53811 RVA: 0x002E9965 File Offset: 0x002E7B65
		public new static FObj.Maker GetMaker()
		{
			return new MultiProperties.Maker();
		}

		// Token: 0x0600D234 RID: 53812 RVA: 0x002E996C File Offset: 0x002E7B6C
		protected MultiProperties(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:multi-properties";
		}

		// Token: 0x0600D235 RID: 53813 RVA: 0x002E9981 File Offset: 0x002E7B81
		public override Status Layout(Area area)
		{
			this.propMgr.GetAccessibilityProps();
			return base.Layout(area);
		}

		// Token: 0x020013F2 RID: 5106
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D236 RID: 53814 RVA: 0x002E9996 File Offset: 0x002E7B96
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new MultiProperties(parent, propertyList);
			}
		}
	}
}
