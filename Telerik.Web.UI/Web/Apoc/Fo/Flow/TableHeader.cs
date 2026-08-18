using System;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x02001411 RID: 5137
	internal class TableHeader : AbstractTableBody
	{
		// Token: 0x0600D2A9 RID: 53929 RVA: 0x002EBDDE File Offset: 0x002E9FDE
		public new static FObj.Maker GetMaker()
		{
			return new TableHeader.Maker();
		}

		// Token: 0x0600D2AA RID: 53930 RVA: 0x002EBDE5 File Offset: 0x002E9FE5
		public TableHeader(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:table-header";
		}

		// Token: 0x02001412 RID: 5138
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D2AB RID: 53931 RVA: 0x002EBDFA File Offset: 0x002E9FFA
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new TableHeader(parent, propertyList);
			}
		}
	}
}
