using System;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x0200140D RID: 5133
	internal class TableColumn : FObj
	{
		// Token: 0x0600D296 RID: 53910 RVA: 0x002EBB61 File Offset: 0x002E9D61
		public new static FObj.Maker GetMaker()
		{
			return new TableColumn.Maker();
		}

		// Token: 0x0600D297 RID: 53911 RVA: 0x002EBB68 File Offset: 0x002E9D68
		public TableColumn(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:table-column";
		}

		// Token: 0x0600D298 RID: 53912 RVA: 0x002EBB7D File Offset: 0x002E9D7D
		public Length GetColumnWidthAsLength()
		{
			return this.columnWidthPropVal;
		}

		// Token: 0x0600D299 RID: 53913 RVA: 0x002EBB85 File Offset: 0x002E9D85
		public int GetColumnWidth()
		{
			return this.columnWidth;
		}

		// Token: 0x0600D29A RID: 53914 RVA: 0x002EBB8D File Offset: 0x002E9D8D
		public void SetColumnWidth(int columnWidth)
		{
			this.columnWidth = columnWidth;
		}

		// Token: 0x0600D29B RID: 53915 RVA: 0x002EBB96 File Offset: 0x002E9D96
		public int GetColumnNumber()
		{
			return this.iColumnNumber;
		}

		// Token: 0x0600D29C RID: 53916 RVA: 0x002EBB9E File Offset: 0x002E9D9E
		public int GetNumColumnsRepeated()
		{
			return this.numColumnsRepeated;
		}

		// Token: 0x0600D29D RID: 53917 RVA: 0x002EBBA8 File Offset: 0x002E9DA8
		public void DoSetup(Area area)
		{
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			this.iColumnNumber = this.properties.GetProperty("column-number").GetNumber().IntValue();
			this.numColumnsRepeated = this.properties.GetProperty("number-columns-repeated").GetNumber().IntValue();
			this.columnWidthPropVal = this.properties.GetProperty("column-width").GetLength();
			this.columnWidth = this.columnWidthPropVal.MValue();
			string @string = this.properties.GetProperty("id").GetString();
			area.getIDReferences().InitializeID(@string, area);
			this.setup = true;
		}

		// Token: 0x0600D29E RID: 53918 RVA: 0x002EBC64 File Offset: 0x002E9E64
		public override Status Layout(Area area)
		{
			if (this.marker == -1001)
			{
				return new Status(1);
			}
			if (this.marker == -1000 && !this.setup)
			{
				this.DoSetup(area);
			}
			if (this.columnWidth > 0)
			{
				this.areaContainer = new AreaContainer(this.propMgr.GetFontState(area.getFontInfo()), this.columnOffset, 0, this.columnWidth, area.getContentHeight(), 61);
				this.areaContainer.foCreator = this;
				this.areaContainer.setPage(area.getPage());
				this.areaContainer.setBorderAndPadding(this.propMgr.GetBorderAndPadding());
				this.areaContainer.setBackground(this.propMgr.GetBackgroundProps());
				this.areaContainer.SetHeight(area.GetHeight());
				area.addChild(this.areaContainer);
			}
			return new Status(1);
		}

		// Token: 0x0600D29F RID: 53919 RVA: 0x002EBD4A File Offset: 0x002E9F4A
		public void SetColumnOffset(int columnOffset)
		{
			this.columnOffset = columnOffset;
		}

		// Token: 0x0600D2A0 RID: 53920 RVA: 0x002EBD53 File Offset: 0x002E9F53
		public void SetHeight(int height)
		{
			if (this.areaContainer != null)
			{
				this.areaContainer.setMaxHeight(height);
				this.areaContainer.SetHeight(height);
			}
		}

		// Token: 0x040038EE RID: 14574
		private Length columnWidthPropVal;

		// Token: 0x040038EF RID: 14575
		private int columnWidth;

		// Token: 0x040038F0 RID: 14576
		private int columnOffset;

		// Token: 0x040038F1 RID: 14577
		private int numColumnsRepeated;

		// Token: 0x040038F2 RID: 14578
		private int iColumnNumber;

		// Token: 0x040038F3 RID: 14579
		private bool setup;

		// Token: 0x040038F4 RID: 14580
		private AreaContainer areaContainer;

		// Token: 0x0200140E RID: 5134
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D2A1 RID: 53921 RVA: 0x002EBD75 File Offset: 0x002E9F75
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new TableColumn(parent, propertyList);
			}
		}
	}
}
