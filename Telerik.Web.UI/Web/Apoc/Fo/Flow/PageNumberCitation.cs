using System;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013FB RID: 5115
	internal class PageNumberCitation : FObj
	{
		// Token: 0x0600D24C RID: 53836 RVA: 0x002E9C1D File Offset: 0x002E7E1D
		public new static FObj.Maker GetMaker()
		{
			return new PageNumberCitation.Maker();
		}

		// Token: 0x0600D24D RID: 53837 RVA: 0x002E9C24 File Offset: 0x002E7E24
		public PageNumberCitation(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:page-number-citation";
		}

		// Token: 0x0600D24E RID: 53838 RVA: 0x002E9C3C File Offset: 0x002E7E3C
		public override Status Layout(Area area)
		{
			BlockArea blockArea = area as BlockArea;
			if (blockArea == null)
			{
				ApocDriver.ActiveDriver.FireApocWarning("Page-number-citation outside block area");
				return new Status(1);
			}
			IDReferences idreferences = area.getIDReferences();
			this.area = area;
			if (this.marker == -1000)
			{
				this.propMgr.GetAccessibilityProps();
				this.propMgr.GetAuralProps();
				this.propMgr.GetBorderAndPadding();
				this.propMgr.GetBackgroundProps();
				this.propMgr.GetMarginInlineProps();
				this.propMgr.GetRelativePositionProps();
				ColorType colorType = this.properties.GetProperty("color").GetColorType();
				this.red = colorType.Red;
				this.green = colorType.Green;
				this.blue = colorType.Blue;
				this.wrapOption = this.properties.GetProperty("wrap-option").GetEnum();
				this.whiteSpaceCollapse = this.properties.GetProperty("white-space-collapse").GetEnum();
				this.refId = this.properties.GetProperty("ref-id").GetString();
				if (string.IsNullOrEmpty(this.refId))
				{
					throw new ApocException("page-number-citation must contain \"ref-id\"");
				}
				this.id = this.properties.GetProperty("id").GetString();
				idreferences.CreateID(this.id);
				this.ts = new TextState();
				this.marker = 0;
			}
			if (this.marker == 0)
			{
				idreferences.ConfigureID(this.id, area);
			}
			this.pageNumber = idreferences.getPageNumber(this.refId);
			if (this.pageNumber != null)
			{
				this.marker = FOText.addText(blockArea, this.propMgr.GetFontState(area.getFontInfo()), this.red, this.green, this.blue, this.wrapOption, null, this.whiteSpaceCollapse, this.pageNumber.ToCharArray(), 0, this.pageNumber.Length, this.ts, 8);
			}
			else
			{
				LineArea currentLineArea = blockArea.getCurrentLineArea();
				if (currentLineArea == null)
				{
					return new Status(2);
				}
				currentLineArea.changeFont(this.propMgr.GetFontState(area.getFontInfo()));
				currentLineArea.changeColor(this.red, this.green, this.blue);
				currentLineArea.changeWrapOption(this.wrapOption);
				currentLineArea.changeWhiteSpaceCollapse(this.whiteSpaceCollapse);
				currentLineArea.addPageNumberCitation(this.refId, null);
				this.marker = -1;
			}
			if (this.marker == -1)
			{
				return new Status(1);
			}
			return new Status(2);
		}

		// Token: 0x040038B3 RID: 14515
		private float red;

		// Token: 0x040038B4 RID: 14516
		private float green;

		// Token: 0x040038B5 RID: 14517
		private float blue;

		// Token: 0x040038B6 RID: 14518
		private int wrapOption;

		// Token: 0x040038B7 RID: 14519
		private int whiteSpaceCollapse;

		// Token: 0x040038B8 RID: 14520
		private Area area;

		// Token: 0x040038B9 RID: 14521
		private string pageNumber;

		// Token: 0x040038BA RID: 14522
		private string refId;

		// Token: 0x040038BB RID: 14523
		private string id;

		// Token: 0x040038BC RID: 14524
		private TextState ts;

		// Token: 0x020013FC RID: 5116
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D24F RID: 53839 RVA: 0x002E9EB9 File Offset: 0x002E80B9
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new PageNumberCitation(parent, propertyList);
			}
		}
	}
}
