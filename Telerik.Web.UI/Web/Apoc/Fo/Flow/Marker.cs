using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013ED RID: 5101
	internal class Marker : FObjMixed
	{
		// Token: 0x0600D222 RID: 53794 RVA: 0x002E9798 File Offset: 0x002E7998
		public new static FObj.Maker GetMaker()
		{
			return new Marker.Maker();
		}

		// Token: 0x0600D223 RID: 53795 RVA: 0x002E97A0 File Offset: 0x002E79A0
		public Marker(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:marker";
			this.markerClassName = this.properties.GetProperty("marker-class-name").GetString();
			this.ts = this.propMgr.getTextDecoration(parent);
			try
			{
				parent.AddMarker(this.markerClassName);
			}
			catch (ApocException)
			{
			}
		}

		// Token: 0x0600D224 RID: 53796 RVA: 0x002E9810 File Offset: 0x002E7A10
		public override Status Layout(Area area)
		{
			this.registryArea = area;
			area.getPage().registerMarker(this);
			return new Status(1);
		}

		// Token: 0x0600D225 RID: 53797 RVA: 0x002E982C File Offset: 0x002E7A2C
		public Status LayoutMarker(Area area)
		{
			if (this.marker == -1000)
			{
				this.marker = 0;
			}
			int count = this.children.Count;
			for (int i = this.marker; i < count; i++)
			{
				FONode fonode = (FONode)this.children[i];
				Status status;
				Status result = status = fonode.Layout(area);
				if (status.isIncomplete())
				{
					this.marker = i;
					return result;
				}
			}
			return new Status(1);
		}

		// Token: 0x0600D226 RID: 53798 RVA: 0x002E989F File Offset: 0x002E7A9F
		public string GetMarkerClassName()
		{
			return this.markerClassName;
		}

		// Token: 0x0600D227 RID: 53799 RVA: 0x002E98A7 File Offset: 0x002E7AA7
		public Area GetRegistryArea()
		{
			return this.registryArea;
		}

		// Token: 0x0600D228 RID: 53800 RVA: 0x002E98AF File Offset: 0x002E7AAF
		public void releaseRegistryArea()
		{
			this.isFirst = this.registryArea.isFirst();
			this.isLast = this.registryArea.isLast();
			this.registryArea = null;
		}

		// Token: 0x0600D229 RID: 53801 RVA: 0x002E98DC File Offset: 0x002E7ADC
		public void resetMarker()
		{
			if (this.registryArea != null)
			{
				Page page = this.registryArea.getPage();
				if (page != null)
				{
					page.unregisterMarker(this);
				}
			}
		}

		// Token: 0x0600D22A RID: 53802 RVA: 0x002E9907 File Offset: 0x002E7B07
		public void resetMarkerContent()
		{
			base.ResetMarker();
		}

		// Token: 0x0600D22B RID: 53803 RVA: 0x002E990F File Offset: 0x002E7B0F
		public override bool MayPrecedeMarker()
		{
			return true;
		}

		// Token: 0x040038A9 RID: 14505
		private string markerClassName;

		// Token: 0x040038AA RID: 14506
		private Area registryArea;

		// Token: 0x040038AB RID: 14507
		private bool isFirst;

		// Token: 0x040038AC RID: 14508
		private bool isLast;

		// Token: 0x020013EE RID: 5102
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D22C RID: 53804 RVA: 0x002E9912 File Offset: 0x002E7B12
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Marker(parent, propertyList);
			}
		}
	}
}
