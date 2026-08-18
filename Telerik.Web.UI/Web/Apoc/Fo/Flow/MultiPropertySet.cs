using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013F3 RID: 5107
	internal class MultiPropertySet : ToBeImplementedElement
	{
		// Token: 0x0600D238 RID: 53816 RVA: 0x002E99A7 File Offset: 0x002E7BA7
		public new static FObj.Maker GetMaker()
		{
			return new MultiPropertySet.Maker();
		}

		// Token: 0x0600D239 RID: 53817 RVA: 0x002E99AE File Offset: 0x002E7BAE
		protected MultiPropertySet(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:multi-property-set";
		}

		// Token: 0x0600D23A RID: 53818 RVA: 0x002E99C3 File Offset: 0x002E7BC3
		public override Status Layout(Area area)
		{
			return base.Layout(area);
		}

		// Token: 0x020013F4 RID: 5108
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D23B RID: 53819 RVA: 0x002E99CC File Offset: 0x002E7BCC
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new MultiPropertySet(parent, propertyList);
			}
		}
	}
}
