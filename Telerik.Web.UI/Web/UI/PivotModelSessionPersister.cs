using System;
using Telerik.Web.UI.PivotGrid.Core.ViewModels;

namespace Telerik.Web.UI
{
	// Token: 0x02000E0F RID: 3599
	internal class PivotModelSessionPersister
	{
		// Token: 0x060085B5 RID: 34229 RVA: 0x001E79BB File Offset: 0x001E5BBB
		public PivotModelSessionPersister() : this(new SessionPersistentMedia())
		{
		}

		// Token: 0x060085B6 RID: 34230 RVA: 0x001E79C8 File Offset: 0x001E5BC8
		internal PivotModelSessionPersister(IPersistentMediaExtended media)
		{
			this.persistentMedia = media;
		}

		// Token: 0x060085B7 RID: 34231 RVA: 0x001E79D7 File Offset: 0x001E5BD7
		public void SavePivotModel(PivotViewModel model, string key)
		{
			this.persistentMedia.Add<PivotViewModel>(key, model);
		}

		// Token: 0x060085B8 RID: 34232 RVA: 0x001E79E6 File Offset: 0x001E5BE6
		public void Clear(string key)
		{
			this.persistentMedia.Remove(key);
		}

		// Token: 0x060085B9 RID: 34233 RVA: 0x001E79F4 File Offset: 0x001E5BF4
		public PivotViewModel GetPivotModel(string key)
		{
			return this.persistentMedia.Get<PivotViewModel>(key);
		}

		// Token: 0x04002546 RID: 9542
		private IPersistentMediaExtended persistentMedia;
	}
}
