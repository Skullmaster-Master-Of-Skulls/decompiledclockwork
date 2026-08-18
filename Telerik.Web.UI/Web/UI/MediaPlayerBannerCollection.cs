using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020005C1 RID: 1473
	[ParseChildren(false)]
	[PersistChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class MediaPlayerBannerCollection : StronglyTypedStateManagedCollection<MediaPlayerBanner>
	{
		// Token: 0x0600348D RID: 13453 RVA: 0x000ADFF0 File Offset: 0x000AC1F0
		protected override void SetDirtyObject(object stateManagerObject)
		{
			StateManager stateManager = stateManagerObject as StateManager;
			if (stateManager != null)
			{
				stateManager.SetDirty();
			}
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000AE00D File Offset: 0x000AC20D
		public override void Add(MediaPlayerBanner item)
		{
			base.Add(item);
		}
	}
}
