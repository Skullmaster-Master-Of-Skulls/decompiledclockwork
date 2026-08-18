using System;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013F9 RID: 5113
	internal class PageNumber : FObj
	{
		// Token: 0x0600D247 RID: 53831 RVA: 0x002E9A61 File Offset: 0x002E7C61
		public new static FObj.Maker GetMaker()
		{
			return new PageNumber.Maker();
		}

		// Token: 0x0600D248 RID: 53832 RVA: 0x002E9A68 File Offset: 0x002E7C68
		public PageNumber(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:page-number";
		}

		// Token: 0x0600D249 RID: 53833 RVA: 0x002E9A80 File Offset: 0x002E7C80
		public override Status Layout(Area area)
		{
			BlockArea blockArea = area as BlockArea;
			if (blockArea == null)
			{
				ApocDriver.ActiveDriver.FireApocWarning("Page-number outside block area");
				return new Status(1);
			}
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
				this.ts = new TextState();
				this.marker = 0;
				string @string = this.properties.GetProperty("id").GetString();
				area.getIDReferences().InitializeID(@string, area);
			}
			string formattedNumber = area.getPage().getFormattedNumber();
			this.marker = FOText.addText(blockArea, this.propMgr.GetFontState(area.getFontInfo()), this.red, this.green, this.blue, this.wrapOption, null, this.whiteSpaceCollapse, formattedNumber.ToCharArray(), 0, formattedNumber.Length, this.ts, 8);
			return new Status(1);
		}

		// Token: 0x040038AD RID: 14509
		private float red;

		// Token: 0x040038AE RID: 14510
		private float green;

		// Token: 0x040038AF RID: 14511
		private float blue;

		// Token: 0x040038B0 RID: 14512
		private int wrapOption;

		// Token: 0x040038B1 RID: 14513
		private int whiteSpaceCollapse;

		// Token: 0x040038B2 RID: 14514
		private TextState ts;

		// Token: 0x020013FA RID: 5114
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D24A RID: 53834 RVA: 0x002E9C0C File Offset: 0x002E7E0C
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new PageNumber(parent, propertyList);
			}
		}
	}
}
