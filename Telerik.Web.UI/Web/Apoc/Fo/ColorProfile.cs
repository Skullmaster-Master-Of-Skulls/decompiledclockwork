using System;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020013A1 RID: 5025
	internal class ColorProfile : ToBeImplementedElement
	{
		// Token: 0x0600D110 RID: 53520 RVA: 0x002E433D File Offset: 0x002E253D
		public new static FObj.Maker GetMaker()
		{
			return new ColorProfile.Maker();
		}

		// Token: 0x0600D111 RID: 53521 RVA: 0x002E4344 File Offset: 0x002E2544
		protected ColorProfile(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:color-profile";
		}

		// Token: 0x020013A2 RID: 5026
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D112 RID: 53522 RVA: 0x002E4359 File Offset: 0x002E2559
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new ColorProfile(parent, propertyList);
			}
		}
	}
}
