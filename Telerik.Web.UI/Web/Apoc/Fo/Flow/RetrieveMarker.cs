using System;
using System.Collections;
using Telerik.Web.Apoc.Fo.Pagination;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013FD RID: 5117
	internal class RetrieveMarker : FObjMixed
	{
		// Token: 0x0600D251 RID: 53841 RVA: 0x002E9ECA File Offset: 0x002E80CA
		public new static FObj.Maker GetMaker()
		{
			return new RetrieveMarker.Maker();
		}

		// Token: 0x0600D252 RID: 53842 RVA: 0x002E9ED4 File Offset: 0x002E80D4
		public RetrieveMarker(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:retrieve-marker";
			this.retrieveClassName = this.properties.GetProperty("retrieve-class-name").GetString();
			this.retrievePosition = this.properties.GetProperty("retrieve-position").GetEnum();
			this.retrieveBoundary = this.properties.GetProperty("retrieve-boundary").GetEnum();
		}

		// Token: 0x0600D253 RID: 53843 RVA: 0x002E9F48 File Offset: 0x002E8148
		public override Status Layout(Area area)
		{
			if (this.marker == -1000)
			{
				this.marker = 0;
				Page page = area.getPage();
				this.bestMarker = this.SearchPage(page);
				if (this.bestMarker != null)
				{
					this.bestMarker.resetMarkerContent();
					return this.bestMarker.LayoutMarker(area);
				}
				AreaTree areaTree = page.getAreaTree();
				if (this.retrieveBoundary == 59)
				{
					PageSequence currentPageSequence = areaTree.GetCurrentPageSequence();
					if (currentPageSequence == page.getPageSequence())
					{
						return this.LayoutBestMarker(areaTree.GetCurrentPageSequenceMarkers(), area);
					}
				}
				else
				{
					if (this.retrieveBoundary == 18)
					{
						return this.LayoutBestMarker(areaTree.GetDocumentMarkers(), area);
					}
					if (this.retrieveBoundary != 58)
					{
						throw new ApocException("Illegal 'retrieve-boundary' value");
					}
				}
			}
			else if (this.bestMarker != null)
			{
				return this.bestMarker.LayoutMarker(area);
			}
			return new Status(1);
		}

		// Token: 0x0600D254 RID: 53844 RVA: 0x002EA018 File Offset: 0x002E8218
		private Status LayoutBestMarker(ArrayList markers, Area area)
		{
			if (markers != null)
			{
				for (int i = markers.Count - 1; i >= 0; i--)
				{
					Marker marker = (Marker)markers[i];
					if (marker.GetMarkerClassName().Equals(this.retrieveClassName))
					{
						this.bestMarker = marker;
						this.bestMarker.resetMarkerContent();
						return this.bestMarker.LayoutMarker(area);
					}
				}
			}
			return new Status(1);
		}

		// Token: 0x0600D255 RID: 53845 RVA: 0x002EA080 File Offset: 0x002E8280
		private Marker SearchPage(Page page)
		{
			ArrayList markers = page.getMarkers();
			if (markers.Count == 0)
			{
				return null;
			}
			if (this.retrievePosition == 28)
			{
				for (int i = 0; i < markers.Count; i++)
				{
					Marker marker = (Marker)markers[i];
					if (marker.GetMarkerClassName().Equals(this.retrieveClassName))
					{
						return marker;
					}
				}
			}
			else if (this.retrievePosition == 32)
			{
				for (int j = 0; j < markers.Count; j++)
				{
					Marker marker2 = (Marker)markers[j];
					if (marker2.GetMarkerClassName().Equals(this.retrieveClassName) && marker2.GetRegistryArea().isFirst())
					{
						return marker2;
					}
				}
			}
			else if (this.retrievePosition == 42)
			{
				for (int k = markers.Count - 1; k >= 0; k--)
				{
					Marker marker3 = (Marker)markers[k];
					if (marker3.GetMarkerClassName().Equals(this.retrieveClassName) && marker3.GetRegistryArea().isFirst())
					{
						return marker3;
					}
				}
			}
			else
			{
				if (this.retrievePosition != 39)
				{
					throw new ApocException("Illegal 'retrieve-position' value");
				}
				for (int l = markers.Count - 1; l >= 0; l--)
				{
					Marker marker4 = (Marker)markers[l];
					if (marker4.GetMarkerClassName().Equals(this.retrieveClassName) && marker4.GetRegistryArea().isLast())
					{
						return marker4;
					}
				}
			}
			return null;
		}

		// Token: 0x040038BD RID: 14525
		private string retrieveClassName;

		// Token: 0x040038BE RID: 14526
		private int retrievePosition;

		// Token: 0x040038BF RID: 14527
		private int retrieveBoundary;

		// Token: 0x040038C0 RID: 14528
		private Marker bestMarker;

		// Token: 0x020013FE RID: 5118
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D256 RID: 53846 RVA: 0x002EA1ED File Offset: 0x002E83ED
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new RetrieveMarker(parent, propertyList);
			}
		}
	}
}
