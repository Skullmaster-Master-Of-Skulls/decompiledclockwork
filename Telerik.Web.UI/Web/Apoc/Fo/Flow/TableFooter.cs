using System;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x0200140F RID: 5135
	internal class TableFooter : AbstractTableBody
	{
		// Token: 0x0600D2A3 RID: 53923 RVA: 0x002EBD86 File Offset: 0x002E9F86
		public override int GetYPosition()
		{
			return this.areaContainer.GetCurrentYPosition() - this.spaceBefore;
		}

		// Token: 0x0600D2A4 RID: 53924 RVA: 0x002EBD9A File Offset: 0x002E9F9A
		public override void SetYPosition(int value)
		{
			this.areaContainer.setYPosition(value + 2 * this.spaceBefore);
		}

		// Token: 0x0600D2A5 RID: 53925 RVA: 0x002EBDB1 File Offset: 0x002E9FB1
		public new static FObj.Maker GetMaker()
		{
			return new TableFooter.Maker();
		}

		// Token: 0x0600D2A6 RID: 53926 RVA: 0x002EBDB8 File Offset: 0x002E9FB8
		public TableFooter(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:table-footer";
		}

		// Token: 0x02001410 RID: 5136
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D2A7 RID: 53927 RVA: 0x002EBDCD File Offset: 0x002E9FCD
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new TableFooter(parent, propertyList);
			}
		}
	}
}
