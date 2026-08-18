using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013F5 RID: 5109
	internal class MultiSwitch : ToBeImplementedElement
	{
		// Token: 0x0600D23D RID: 53821 RVA: 0x002E99DD File Offset: 0x002E7BDD
		public new static FObj.Maker GetMaker()
		{
			return new MultiSwitch.Maker();
		}

		// Token: 0x0600D23E RID: 53822 RVA: 0x002E99E4 File Offset: 0x002E7BE4
		protected MultiSwitch(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:multi-switch";
		}

		// Token: 0x0600D23F RID: 53823 RVA: 0x002E99F9 File Offset: 0x002E7BF9
		public override Status Layout(Area area)
		{
			this.propMgr.GetAccessibilityProps();
			return base.Layout(area);
		}

		// Token: 0x020013F6 RID: 5110
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D240 RID: 53824 RVA: 0x002E9A0E File Offset: 0x002E7C0E
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new MultiSwitch(parent, propertyList);
			}
		}
	}
}
