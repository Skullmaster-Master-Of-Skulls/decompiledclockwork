using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x02001438 RID: 5176
	internal class RegionBody : Region
	{
		// Token: 0x0600D36A RID: 54122 RVA: 0x002EEF1F File Offset: 0x002ED11F
		public new static FObj.Maker GetMaker()
		{
			return new RegionBody.Maker();
		}

		// Token: 0x0600D36B RID: 54123 RVA: 0x002EEF26 File Offset: 0x002ED126
		protected RegionBody(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
		}

		// Token: 0x0600D36C RID: 54124 RVA: 0x002EEF30 File Offset: 0x002ED130
		public override RegionArea MakeRegionArea(int allocationRectangleXPosition, int allocationRectangleYPosition, int allocationRectangleWidth, int allocationRectangleHeight)
		{
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			MarginProps marginProps = this.propMgr.GetMarginProps();
			BodyRegionArea bodyRegionArea = new BodyRegionArea(allocationRectangleXPosition + marginProps.marginLeft, allocationRectangleYPosition - marginProps.marginTop, allocationRectangleWidth - marginProps.marginLeft - marginProps.marginRight, allocationRectangleHeight - marginProps.marginTop - marginProps.marginBottom);
			bodyRegionArea.setBackground(this.propMgr.GetBackgroundProps());
			int @enum = this.properties.GetProperty("overflow").GetEnum();
			string @string = this.properties.GetProperty("column-count").GetString();
			int num = 1;
			try
			{
				num = int.Parse(@string);
			}
			catch (FormatException)
			{
				ApocDriver.ActiveDriver.FireApocError("Bad value on region body 'column-count'");
				num = 1;
			}
			if (num > 1 && @enum == 67)
			{
				ApocDriver.ActiveDriver.FireApocError("Setting 'column-count' to 1 because 'overflow' is set to 'scroll'");
				num = 1;
			}
			bodyRegionArea.setColumnCount(num);
			int columnGap = this.properties.GetProperty("column-gap").GetLength().MValue();
			bodyRegionArea.setColumnGap(columnGap);
			return bodyRegionArea;
		}

		// Token: 0x0600D36D RID: 54125 RVA: 0x002EF050 File Offset: 0x002ED250
		protected override string GetDefaultRegionName()
		{
			return "xsl-region-body";
		}

		// Token: 0x0600D36E RID: 54126 RVA: 0x002EF057 File Offset: 0x002ED257
		protected override string GetElementName()
		{
			return "fo:region-body";
		}

		// Token: 0x0600D36F RID: 54127 RVA: 0x002EF05E File Offset: 0x002ED25E
		public override string GetRegionClass()
		{
			return "body";
		}

		// Token: 0x0400395D RID: 14685
		public const string REGION_CLASS = "body";

		// Token: 0x02001439 RID: 5177
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D370 RID: 54128 RVA: 0x002EF065 File Offset: 0x002ED265
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new RegionBody(parent, propertyList);
			}
		}
	}
}
