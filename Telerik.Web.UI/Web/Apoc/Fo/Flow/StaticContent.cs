using System;
using Telerik.Web.Apoc.Fo.Pagination;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x02001401 RID: 5121
	internal class StaticContent : Flow
	{
		// Token: 0x0600D266 RID: 53862 RVA: 0x002EA3EA File Offset: 0x002E85EA
		public new static FObj.Maker GetMaker()
		{
			return new StaticContent.Maker();
		}

		// Token: 0x0600D267 RID: 53863 RVA: 0x002EA3F1 File Offset: 0x002E85F1
		protected StaticContent(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			((PageSequence)parent).IsFlowSet = false;
		}

		// Token: 0x0600D268 RID: 53864 RVA: 0x002EA407 File Offset: 0x002E8607
		public override Status Layout(Area area)
		{
			return this.Layout(area, null);
		}

		// Token: 0x0600D269 RID: 53865 RVA: 0x002EA414 File Offset: 0x002E8614
		public override Status Layout(Area area, Region region)
		{
			int count = this.children.Count;
			string areaName = "none";
			if (region != null)
			{
				areaName = region.GetRegionClass();
			}
			else if (base.GetFlowName().Equals("xsl-region-before"))
			{
				areaName = "before";
			}
			else if (base.GetFlowName().Equals("xsl-region-after"))
			{
				areaName = "after";
			}
			else if (base.GetFlowName().Equals("xsl-region-start"))
			{
				areaName = "start";
			}
			else if (base.GetFlowName().Equals("xsl-region-end"))
			{
				areaName = "end";
			}
			AreaContainer areaContainer = area as AreaContainer;
			if (areaContainer != null)
			{
				areaContainer.setAreaName(areaName);
			}
			area.setAbsoluteHeight(0);
			base.SetContentWidth(area.getContentWidth());
			for (int i = 0; i < count; i++)
			{
				FObj fobj = (FObj)this.children[i];
				Status status;
				Status result = status = fobj.Layout(area);
				if (status.isIncomplete())
				{
					ApocDriver.ActiveDriver.FireApocWarning("Some static content could not fit in the area.");
					this.marker = i;
					if (i != 0 && result.getCode() == 2)
					{
						result = new Status(3);
					}
					return result;
				}
			}
			this.ResetMarker();
			return new Status(1);
		}

		// Token: 0x0600D26A RID: 53866 RVA: 0x002EA538 File Offset: 0x002E8738
		protected override string GetElementName()
		{
			return "fo:static-content";
		}

		// Token: 0x0600D26B RID: 53867 RVA: 0x002EA53F File Offset: 0x002E873F
		protected override void SetFlowName(string name)
		{
			if (name == null || string.IsNullOrEmpty(name))
			{
				throw new ApocException("A 'flow-name' is required for " + this.GetElementName() + ".");
			}
			base.SetFlowName(name);
		}

		// Token: 0x02001402 RID: 5122
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D26C RID: 53868 RVA: 0x002EA56E File Offset: 0x002E876E
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new StaticContent(parent, propertyList);
			}
		}
	}
}
