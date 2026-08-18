using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013C9 RID: 5065
	internal class BasicLink : Inline
	{
		// Token: 0x0600D1B6 RID: 53686 RVA: 0x002E68CC File Offset: 0x002E4ACC
		public new static FObj.Maker GetMaker()
		{
			return new BasicLink.Maker();
		}

		// Token: 0x0600D1B7 RID: 53687 RVA: 0x002E68D3 File Offset: 0x002E4AD3
		public BasicLink(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:basic-link";
		}

		// Token: 0x0600D1B8 RID: 53688 RVA: 0x002E68E8 File Offset: 0x002E4AE8
		public override Status Layout(Area area)
		{
			this.propMgr.GetAccessibilityProps();
			this.propMgr.GetAuralProps();
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			this.propMgr.GetMarginInlineProps();
			this.propMgr.GetRelativePositionProps();
			string @string;
			int linkType;
			if (!string.IsNullOrEmpty(@string = this.properties.GetProperty("internal-destination").GetString()))
			{
				linkType = 1;
			}
			else if (!string.IsNullOrEmpty(@string = this.properties.GetProperty("external-destination").GetString()))
			{
				linkType = 1;
			}
			else
			{
				linkType = 1;
			}
			if (this.marker == -1000)
			{
				string string2 = this.properties.GetProperty("id").GetString();
				area.getIDReferences().InitializeID(string2, area);
				this.marker = 0;
			}
			LinkSet linkSet = new LinkSet(@string, area, linkType);
			AreaContainer areaContainer = area.getNearestAncestorAreaContainer();
			while (areaContainer != null && areaContainer.getPosition() != 1)
			{
				areaContainer = areaContainer.getNearestAncestorAreaContainer();
			}
			if (areaContainer == null)
			{
				areaContainer = area.getPage().getBody().getCurrentColumnArea();
			}
			int count = this.children.Count;
			for (int i = this.marker; i < count; i++)
			{
				FONode fonode = (FONode)this.children[i];
				fonode.SetLinkSet(linkSet);
				Status status;
				Status result = status = fonode.Layout(area);
				if (status.isIncomplete())
				{
					this.marker = i;
					return result;
				}
			}
			linkSet.applyAreaContainerOffsets(areaContainer, area);
			area.getPage().addLinkSet(linkSet);
			return new Status(1);
		}

		// Token: 0x020013CA RID: 5066
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D1B9 RID: 53689 RVA: 0x002E6A75 File Offset: 0x002E4C75
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new BasicLink(parent, propertyList);
			}
		}
	}
}
