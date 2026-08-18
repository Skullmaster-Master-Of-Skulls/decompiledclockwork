using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x0200143A RID: 5178
	internal class RegionEnd : Region
	{
		// Token: 0x0600D372 RID: 54130 RVA: 0x002EF076 File Offset: 0x002ED276
		public new static FObj.Maker GetMaker()
		{
			return new RegionEnd.Maker();
		}

		// Token: 0x0600D373 RID: 54131 RVA: 0x002EF07D File Offset: 0x002ED27D
		protected RegionEnd(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
		}

		// Token: 0x0600D374 RID: 54132 RVA: 0x002EF088 File Offset: 0x002ED288
		internal RegionArea MakeRegionArea(int allocationRectangleXPosition, int allocationRectangleYPosition, int allocationRectangleWidth, int allocationRectangleHeight, bool beforePrecedence, bool afterPrecedence, int beforeHeight, int afterHeight)
		{
			int num = this.properties.GetProperty("extent").GetLength().MValue();
			int num2 = allocationRectangleYPosition;
			int num3 = allocationRectangleHeight;
			if (beforePrecedence)
			{
				num2 -= beforeHeight;
				num3 -= beforeHeight;
			}
			if (afterPrecedence)
			{
				num3 -= afterHeight;
			}
			RegionArea regionArea = new RegionArea(allocationRectangleXPosition + allocationRectangleWidth - num, num2, num, num3);
			regionArea.setBackground(this.propMgr.GetBackgroundProps());
			return regionArea;
		}

		// Token: 0x0600D375 RID: 54133 RVA: 0x002EF0EC File Offset: 0x002ED2EC
		public override RegionArea MakeRegionArea(int allocationRectangleXPosition, int allocationRectangleYPosition, int allocationRectangleWidth, int allocationRectangleHeight)
		{
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			int allocationRectangleHeight2 = this.properties.GetProperty("extent").GetLength().MValue();
			return this.MakeRegionArea(allocationRectangleXPosition, allocationRectangleYPosition, allocationRectangleWidth, allocationRectangleHeight2, false, false, 0, 0);
		}

		// Token: 0x0600D376 RID: 54134 RVA: 0x002EF13A File Offset: 0x002ED33A
		protected override string GetDefaultRegionName()
		{
			return "xsl-region-end";
		}

		// Token: 0x0600D377 RID: 54135 RVA: 0x002EF141 File Offset: 0x002ED341
		protected override string GetElementName()
		{
			return "fo:region-end";
		}

		// Token: 0x0600D378 RID: 54136 RVA: 0x002EF148 File Offset: 0x002ED348
		public override string GetRegionClass()
		{
			return "end";
		}

		// Token: 0x0400395E RID: 14686
		public const string REGION_CLASS = "end";

		// Token: 0x0200143B RID: 5179
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D379 RID: 54137 RVA: 0x002EF14F File Offset: 0x002ED34F
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new RegionEnd(parent, propertyList);
			}
		}
	}
}
