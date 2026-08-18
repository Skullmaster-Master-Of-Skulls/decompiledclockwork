using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x0200143C RID: 5180
	internal class RegionStart : Region
	{
		// Token: 0x0600D37B RID: 54139 RVA: 0x002EF160 File Offset: 0x002ED360
		public new static FObj.Maker GetMaker()
		{
			return new RegionStart.Maker();
		}

		// Token: 0x0600D37C RID: 54140 RVA: 0x002EF167 File Offset: 0x002ED367
		protected RegionStart(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
		}

		// Token: 0x0600D37D RID: 54141 RVA: 0x002EF174 File Offset: 0x002ED374
		internal RegionArea MakeRegionArea(int allocationRectangleXPosition, int allocationRectangleYPosition, int allocationRectangleWidth, int allocationRectangleHeight, bool beforePrecedence, bool afterPrecedence, int beforeHeight, int afterHeight)
		{
			int width = this.properties.GetProperty("extent").GetLength().MValue();
			int num = allocationRectangleYPosition;
			int num2 = allocationRectangleHeight;
			if (beforePrecedence)
			{
				num -= beforeHeight;
				num2 -= beforeHeight;
			}
			if (afterPrecedence)
			{
				num2 -= afterHeight;
			}
			RegionArea regionArea = new RegionArea(allocationRectangleXPosition, num, width, num2);
			regionArea.setBackground(this.propMgr.GetBackgroundProps());
			return regionArea;
		}

		// Token: 0x0600D37E RID: 54142 RVA: 0x002EF1D4 File Offset: 0x002ED3D4
		public override RegionArea MakeRegionArea(int allocationRectangleXPosition, int allocationRectangleYPosition, int allocationRectangleWidth, int allocationRectangleHeight)
		{
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			int allocationRectangleHeight2 = this.properties.GetProperty("extent").GetLength().MValue();
			return this.MakeRegionArea(allocationRectangleXPosition, allocationRectangleYPosition, allocationRectangleWidth, allocationRectangleHeight2, false, false, 0, 0);
		}

		// Token: 0x0600D37F RID: 54143 RVA: 0x002EF222 File Offset: 0x002ED422
		protected override string GetDefaultRegionName()
		{
			return "xsl-region-start";
		}

		// Token: 0x0600D380 RID: 54144 RVA: 0x002EF229 File Offset: 0x002ED429
		protected override string GetElementName()
		{
			return "fo:region-start";
		}

		// Token: 0x0600D381 RID: 54145 RVA: 0x002EF230 File Offset: 0x002ED430
		public override string GetRegionClass()
		{
			return "start";
		}

		// Token: 0x0400395F RID: 14687
		public const string REGION_CLASS = "start";

		// Token: 0x0200143D RID: 5181
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D382 RID: 54146 RVA: 0x002EF237 File Offset: 0x002ED437
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new RegionStart(parent, propertyList);
			}
		}
	}
}
