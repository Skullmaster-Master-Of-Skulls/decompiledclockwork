using System;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x02001407 RID: 5127
	internal class TableBody : AbstractTableBody
	{
		// Token: 0x0600D27F RID: 53887 RVA: 0x002EB451 File Offset: 0x002E9651
		public new static FObj.Maker GetMaker()
		{
			return new TableBody.Maker();
		}

		// Token: 0x0600D280 RID: 53888 RVA: 0x002EB458 File Offset: 0x002E9658
		public TableBody(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:table-body";
		}

		// Token: 0x02001408 RID: 5128
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D281 RID: 53889 RVA: 0x002EB46D File Offset: 0x002E966D
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new TableBody(parent, propertyList);
			}
		}
	}
}
