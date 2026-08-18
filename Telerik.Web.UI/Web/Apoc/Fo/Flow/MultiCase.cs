using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013EF RID: 5103
	internal class MultiCase : ToBeImplementedElement
	{
		// Token: 0x0600D22E RID: 53806 RVA: 0x002E9923 File Offset: 0x002E7B23
		public new static FObj.Maker GetMaker()
		{
			return new MultiCase.Maker();
		}

		// Token: 0x0600D22F RID: 53807 RVA: 0x002E992A File Offset: 0x002E7B2A
		protected MultiCase(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:multi-case";
		}

		// Token: 0x0600D230 RID: 53808 RVA: 0x002E993F File Offset: 0x002E7B3F
		public override Status Layout(Area area)
		{
			this.propMgr.GetAccessibilityProps();
			return base.Layout(area);
		}

		// Token: 0x020013F0 RID: 5104
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D231 RID: 53809 RVA: 0x002E9954 File Offset: 0x002E7B54
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new MultiCase(parent, propertyList);
			}
		}
	}
}
