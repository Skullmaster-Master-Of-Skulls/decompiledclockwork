using System;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020013A7 RID: 5031
	internal class Declarations : ToBeImplementedElement
	{
		// Token: 0x0600D11E RID: 53534 RVA: 0x002E441C File Offset: 0x002E261C
		public new static FObj.Maker GetMaker()
		{
			return new Declarations.Maker();
		}

		// Token: 0x0600D11F RID: 53535 RVA: 0x002E4423 File Offset: 0x002E2623
		protected Declarations(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:declarations";
		}

		// Token: 0x020013A8 RID: 5032
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D120 RID: 53536 RVA: 0x002E4438 File Offset: 0x002E2638
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Declarations(parent, propertyList);
			}
		}
	}
}
