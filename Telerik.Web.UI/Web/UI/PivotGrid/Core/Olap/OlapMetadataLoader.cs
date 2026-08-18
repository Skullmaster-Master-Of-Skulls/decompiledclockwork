using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D0D RID: 3341
	internal abstract class OlapMetadataLoader
	{
		// Token: 0x06007C82 RID: 31874 RVA: 0x001C9B39 File Offset: 0x001C7D39
		public OlapMetadataLoader()
		{
			this.cubes = new List<OlapCubeInfo>();
		}

		// Token: 0x14000131 RID: 305
		// (add) Token: 0x06007C83 RID: 31875 RVA: 0x001C9B4C File Offset: 0x001C7D4C
		// (remove) Token: 0x06007C84 RID: 31876 RVA: 0x001C9B84 File Offset: 0x001C7D84
		public event EventHandler<MetadataLoadedEventsArgs> DataLoaded;

		// Token: 0x170027B5 RID: 10165
		// (get) Token: 0x06007C85 RID: 31877 RVA: 0x001C9BB9 File Offset: 0x001C7DB9
		public IList<OlapCubeInfo> Cubes
		{
			get
			{
				return this.cubes;
			}
		}

		// Token: 0x06007C86 RID: 31878
		public abstract void LoadData();

		// Token: 0x06007C87 RID: 31879 RVA: 0x001C9BC1 File Offset: 0x001C7DC1
		protected virtual void OnDataLoaded(MetadataLoadedEventsArgs args)
		{
			if (this.DataLoaded != null)
			{
				this.DataLoaded(this, args);
			}
		}

		// Token: 0x0400221B RID: 8731
		private List<OlapCubeInfo> cubes;
	}
}
