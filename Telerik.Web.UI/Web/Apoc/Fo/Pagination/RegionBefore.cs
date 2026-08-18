using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x02001436 RID: 5174
	internal class RegionBefore : Region
	{
		// Token: 0x0600D361 RID: 54113 RVA: 0x002EEE6C File Offset: 0x002ED06C
		public new static FObj.Maker GetMaker()
		{
			return new RegionBefore.Maker();
		}

		// Token: 0x0600D362 RID: 54114 RVA: 0x002EEE73 File Offset: 0x002ED073
		protected RegionBefore(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.precedence = this.properties.GetProperty("precedence").GetEnum();
		}

		// Token: 0x0600D363 RID: 54115 RVA: 0x002EEE98 File Offset: 0x002ED098
		public override RegionArea MakeRegionArea(int allocationRectangleXPosition, int allocationRectangleYPosition, int allocationRectangleWidth, int allocationRectangleHeight)
		{
			this.propMgr.GetBorderAndPadding();
			BackgroundProps backgroundProps = this.propMgr.GetBackgroundProps();
			int height = this.properties.GetProperty("extent").GetLength().MValue();
			RegionArea regionArea = new RegionArea(allocationRectangleXPosition, allocationRectangleYPosition, allocationRectangleWidth, height);
			regionArea.setBackground(backgroundProps);
			return regionArea;
		}

		// Token: 0x0600D364 RID: 54116 RVA: 0x002EEEEA File Offset: 0x002ED0EA
		protected override string GetDefaultRegionName()
		{
			return "xsl-region-before";
		}

		// Token: 0x0600D365 RID: 54117 RVA: 0x002EEEF1 File Offset: 0x002ED0F1
		protected override string GetElementName()
		{
			return "fo:region-before";
		}

		// Token: 0x0600D366 RID: 54118 RVA: 0x002EEEF8 File Offset: 0x002ED0F8
		public override string GetRegionClass()
		{
			return "before";
		}

		// Token: 0x0600D367 RID: 54119 RVA: 0x002EEEFF File Offset: 0x002ED0FF
		public bool getPrecedence()
		{
			return this.precedence == 81;
		}

		// Token: 0x0400395B RID: 14683
		public const string REGION_CLASS = "before";

		// Token: 0x0400395C RID: 14684
		private int precedence;

		// Token: 0x02001437 RID: 5175
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D368 RID: 54120 RVA: 0x002EEF0E File Offset: 0x002ED10E
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new RegionBefore(parent, propertyList);
			}
		}
	}
}
