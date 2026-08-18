using System;
using System.Collections;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x02001444 RID: 5188
	internal class SimplePageMaster : FObj
	{
		// Token: 0x0600D3A1 RID: 54177 RVA: 0x002EF5A7 File Offset: 0x002ED7A7
		public new static FObj.Maker GetMaker()
		{
			return new SimplePageMaster.Maker();
		}

		// Token: 0x0600D3A2 RID: 54178 RVA: 0x002EF5B0 File Offset: 0x002ED7B0
		protected SimplePageMaster(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:simple-page-master";
			if (parent.GetName().Equals("fo:layout-master-set"))
			{
				this.layoutMasterSet = (LayoutMasterSet)parent;
				this.masterName = this.properties.GetProperty("master-name").GetString();
				if (this.masterName == null)
				{
					ApocDriver.ActiveDriver.FireApocWarning("simple-page-master does not have a master-name and so is being ignored");
				}
				else
				{
					this.layoutMasterSet.addSimplePageMaster(this);
				}
				this._regions = new Hashtable();
				return;
			}
			throw new ApocException("fo:simple-page-master must be child of fo:layout-master-set, not " + parent.GetName());
		}

		// Token: 0x0600D3A3 RID: 54179 RVA: 0x002EF654 File Offset: 0x002ED854
		protected internal override void End()
		{
			int num = this.properties.GetProperty("page-width").GetLength().MValue();
			int num2 = this.properties.GetProperty("page-height").GetLength().MValue();
			MarginProps marginProps = this.propMgr.GetMarginProps();
			int marginLeft = marginProps.marginLeft;
			int allocationRectangleYPosition = num2 - marginProps.marginTop;
			int allocationRectangleWidth = num - marginProps.marginLeft - marginProps.marginRight;
			int allocationRectangleHeight = num2 - marginProps.marginTop - marginProps.marginBottom;
			this.pageMaster = new PageMaster(num, num2);
			if (this.getRegion("body") != null)
			{
				BodyRegionArea region = (BodyRegionArea)this.getRegion("body").MakeRegionArea(marginLeft, allocationRectangleYPosition, allocationRectangleWidth, allocationRectangleHeight);
				this.pageMaster.addBody(region);
			}
			else
			{
				ApocDriver.ActiveDriver.FireApocError("simple-page-master must have a region of class body");
			}
			if (this.getRegion("before") != null)
			{
				RegionArea regionArea = this.getRegion("before").MakeRegionArea(marginLeft, allocationRectangleYPosition, allocationRectangleWidth, allocationRectangleHeight);
				this.pageMaster.addBefore(regionArea);
				this.beforePrecedence = ((RegionBefore)this.getRegion("before")).getPrecedence();
				this.beforeHeight = regionArea.GetHeight();
			}
			else
			{
				this.beforePrecedence = false;
			}
			if (this.getRegion("after") != null)
			{
				RegionArea regionArea2 = this.getRegion("after").MakeRegionArea(marginLeft, allocationRectangleYPosition, allocationRectangleWidth, allocationRectangleHeight);
				this.pageMaster.addAfter(regionArea2);
				this.afterPrecedence = ((RegionAfter)this.getRegion("after")).getPrecedence();
				this.afterHeight = regionArea2.GetHeight();
			}
			else
			{
				this.afterPrecedence = false;
			}
			if (this.getRegion("start") != null)
			{
				RegionArea region2 = ((RegionStart)this.getRegion("start")).MakeRegionArea(marginLeft, allocationRectangleYPosition, allocationRectangleWidth, allocationRectangleHeight, this.beforePrecedence, this.afterPrecedence, this.beforeHeight, this.afterHeight);
				this.pageMaster.addStart(region2);
			}
			if (this.getRegion("end") != null)
			{
				RegionArea region3 = ((RegionEnd)this.getRegion("end")).MakeRegionArea(marginLeft, allocationRectangleYPosition, allocationRectangleWidth, allocationRectangleHeight, this.beforePrecedence, this.afterPrecedence, this.beforeHeight, this.afterHeight);
				this.pageMaster.addEnd(region3);
			}
		}

		// Token: 0x0600D3A4 RID: 54180 RVA: 0x002EF894 File Offset: 0x002EDA94
		public PageMaster getPageMaster()
		{
			return this.pageMaster;
		}

		// Token: 0x0600D3A5 RID: 54181 RVA: 0x002EF89C File Offset: 0x002EDA9C
		public PageMaster GetNextPageMaster()
		{
			return this.pageMaster;
		}

		// Token: 0x0600D3A6 RID: 54182 RVA: 0x002EF8A4 File Offset: 0x002EDAA4
		public string GetMasterName()
		{
			return this.masterName;
		}

		// Token: 0x0600D3A7 RID: 54183 RVA: 0x002EF8AC File Offset: 0x002EDAAC
		protected internal void addRegion(Region region)
		{
			if (this._regions.ContainsKey(region.GetRegionClass()))
			{
				throw new ApocException("Only one region of class " + region.GetRegionClass() + " allowed within a simple-page-master.");
			}
			this._regions.Add(region.GetRegionClass(), region);
		}

		// Token: 0x0600D3A8 RID: 54184 RVA: 0x002EF8F9 File Offset: 0x002EDAF9
		protected internal Region getRegion(string regionClass)
		{
			return (Region)this._regions[regionClass];
		}

		// Token: 0x0600D3A9 RID: 54185 RVA: 0x002EF90C File Offset: 0x002EDB0C
		protected internal Hashtable getRegions()
		{
			return this._regions;
		}

		// Token: 0x0600D3AA RID: 54186 RVA: 0x002EF914 File Offset: 0x002EDB14
		protected internal bool regionNameExists(string regionName)
		{
			foreach (object obj in this._regions.Values)
			{
				Region region = (Region)obj;
				if (region.getRegionName().Equals(regionName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400396B RID: 14699
		private Hashtable _regions;

		// Token: 0x0400396C RID: 14700
		private LayoutMasterSet layoutMasterSet;

		// Token: 0x0400396D RID: 14701
		private PageMaster pageMaster;

		// Token: 0x0400396E RID: 14702
		private string masterName;

		// Token: 0x0400396F RID: 14703
		private bool beforePrecedence;

		// Token: 0x04003970 RID: 14704
		private int beforeHeight;

		// Token: 0x04003971 RID: 14705
		private bool afterPrecedence;

		// Token: 0x04003972 RID: 14706
		private int afterHeight;

		// Token: 0x02001445 RID: 5189
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D3AB RID: 54187 RVA: 0x002EF980 File Offset: 0x002EDB80
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new SimplePageMaster(parent, propertyList);
			}
		}
	}
}
