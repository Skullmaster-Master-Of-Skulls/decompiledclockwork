using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x02001434 RID: 5172
	internal class RegionAfter : Region
	{
		// Token: 0x0600D358 RID: 54104 RVA: 0x002EEDB2 File Offset: 0x002ECFB2
		public new static FObj.Maker GetMaker()
		{
			return new RegionAfter.Maker();
		}

		// Token: 0x0600D359 RID: 54105 RVA: 0x002EEDB9 File Offset: 0x002ECFB9
		protected RegionAfter(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.precedence = this.properties.GetProperty("precedence").GetEnum();
		}

		// Token: 0x0600D35A RID: 54106 RVA: 0x002EEDE0 File Offset: 0x002ECFE0
		public override RegionArea MakeRegionArea(int allocationRectangleXPosition, int allocationRectangleYPosition, int allocationRectangleWidth, int allocationRectangleHeight)
		{
			this.propMgr.GetBorderAndPadding();
			BackgroundProps backgroundProps = this.propMgr.GetBackgroundProps();
			int num = this.properties.GetProperty("extent").GetLength().MValue();
			RegionArea regionArea = new RegionArea(allocationRectangleXPosition, allocationRectangleYPosition - allocationRectangleHeight + num, allocationRectangleWidth, num);
			regionArea.setBackground(backgroundProps);
			return regionArea;
		}

		// Token: 0x0600D35B RID: 54107 RVA: 0x002EEE37 File Offset: 0x002ED037
		protected override string GetDefaultRegionName()
		{
			return "xsl-region-after";
		}

		// Token: 0x0600D35C RID: 54108 RVA: 0x002EEE3E File Offset: 0x002ED03E
		protected override string GetElementName()
		{
			return "fo:region-after";
		}

		// Token: 0x0600D35D RID: 54109 RVA: 0x002EEE45 File Offset: 0x002ED045
		public override string GetRegionClass()
		{
			return "after";
		}

		// Token: 0x0600D35E RID: 54110 RVA: 0x002EEE4C File Offset: 0x002ED04C
		public bool getPrecedence()
		{
			return this.precedence == 81;
		}

		// Token: 0x04003959 RID: 14681
		public const string REGION_CLASS = "after";

		// Token: 0x0400395A RID: 14682
		private int precedence;

		// Token: 0x02001435 RID: 5173
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D35F RID: 54111 RVA: 0x002EEE5B File Offset: 0x002ED05B
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new RegionAfter(parent, propertyList);
			}
		}
	}
}
