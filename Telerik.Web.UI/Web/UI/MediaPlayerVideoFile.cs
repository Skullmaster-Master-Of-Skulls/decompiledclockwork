using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020005C8 RID: 1480
	public class MediaPlayerVideoFile : MediaPlayerFile
	{
		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000AFAA9 File Offset: 0x000ADCA9
		// (set) Token: 0x0600351A RID: 13594 RVA: 0x000AFAB1 File Offset: 0x000ADCB1
		public string HDPath { get; set; }

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x000AFABA File Offset: 0x000ADCBA
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[MergableProperty(false)]
		public MediaPlayerSourcesCollection HDSources
		{
			get
			{
				if (this.hdSources == null)
				{
					this.hdSources = new MediaPlayerSourcesCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.hdSources).TrackViewState();
					}
				}
				return this.hdSources;
			}
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000AFAE8 File Offset: 0x000ADCE8
		protected override void LoadState(object state)
		{
			base.LoadState(state);
			object[] array = state as object[];
			if (array.Length > 7 && array[7] != null)
			{
				this.HDPath = array[7].ToString();
			}
			if (array.Length > 8)
			{
				((IStateManager)this.HDSources).LoadViewState(array[8]);
			}
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x000AFB30 File Offset: 0x000ADD30
		protected override ArrayList SaveState()
		{
			ArrayList arrayList = base.SaveState();
			arrayList.Add(this.HDPath);
			arrayList.Add(((IStateManager)this.HDSources).SaveViewState());
			return arrayList;
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000AFB64 File Offset: 0x000ADD64
		protected override void TrackState()
		{
			base.TrackState();
			((IStateManager)this.HDSources).TrackViewState();
		}

		// Token: 0x04000E63 RID: 3683
		private MediaPlayerSourcesCollection hdSources;
	}
}
