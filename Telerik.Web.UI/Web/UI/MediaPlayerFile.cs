using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020005BD RID: 1469
	[Browsable(false)]
	public abstract class MediaPlayerFile : IStateManager
	{
		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06003455 RID: 13397 RVA: 0x000AD896 File Offset: 0x000ABA96
		// (set) Token: 0x06003456 RID: 13398 RVA: 0x000AD89E File Offset: 0x000ABA9E
		internal bool IsAutoPlaySet { get; set; }

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06003457 RID: 13399 RVA: 0x000AD8A7 File Offset: 0x000ABAA7
		// (set) Token: 0x06003458 RID: 13400 RVA: 0x000AD8AF File Offset: 0x000ABAAF
		internal int Duration { get; set; }

		// Token: 0x06003459 RID: 13401 RVA: 0x000AD8B8 File Offset: 0x000ABAB8
		public MediaPlayerFile()
		{
			this.StartVolume = -1;
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000AD8C7 File Offset: 0x000ABAC7
		public MediaPlayerFile(RadMediaPlayer ownerMediaPlayer) : this()
		{
			this.owner = ownerMediaPlayer;
		}

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x0600345B RID: 13403 RVA: 0x000AD8D6 File Offset: 0x000ABAD6
		public string FileType
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x000AD8E3 File Offset: 0x000ABAE3
		public RadMediaPlayer OwnerMediaPlayer
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x0600345D RID: 13405 RVA: 0x000AD8EB File Offset: 0x000ABAEB
		// (set) Token: 0x0600345E RID: 13406 RVA: 0x000AD8F3 File Offset: 0x000ABAF3
		public string Path { get; set; }

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x0600345F RID: 13407 RVA: 0x000AD8FC File Offset: 0x000ABAFC
		// (set) Token: 0x06003460 RID: 13408 RVA: 0x000AD904 File Offset: 0x000ABB04
		public string SubtitlesPath { get; set; }

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06003461 RID: 13409 RVA: 0x000AD90D File Offset: 0x000ABB0D
		// (set) Token: 0x06003462 RID: 13410 RVA: 0x000AD915 File Offset: 0x000ABB15
		public int StartVolume { get; set; }

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06003463 RID: 13411 RVA: 0x000AD91E File Offset: 0x000ABB1E
		// (set) Token: 0x06003464 RID: 13412 RVA: 0x000AD926 File Offset: 0x000ABB26
		public bool AutoPlay
		{
			get
			{
				return this.autoPlay;
			}
			set
			{
				this.IsAutoPlaySet = true;
				this.autoPlay = value;
			}
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06003465 RID: 13413 RVA: 0x000AD936 File Offset: 0x000ABB36
		// (set) Token: 0x06003466 RID: 13414 RVA: 0x000AD93E File Offset: 0x000ABB3E
		public double StartTime { get; set; }

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06003467 RID: 13415 RVA: 0x000AD947 File Offset: 0x000ABB47
		// (set) Token: 0x06003468 RID: 13416 RVA: 0x000AD94F File Offset: 0x000ABB4F
		public string Title { get; set; }

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06003469 RID: 13417 RVA: 0x000AD958 File Offset: 0x000ABB58
		// (set) Token: 0x0600346A RID: 13418 RVA: 0x000AD960 File Offset: 0x000ABB60
		public string Poster { get; set; }

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x000AD969 File Offset: 0x000ABB69
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public MediaPlayerSourcesCollection Sources
		{
			get
			{
				if (this.sources == null)
				{
					this.sources = new MediaPlayerSourcesCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.sources).TrackViewState();
					}
				}
				return this.sources;
			}
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x000AD997 File Offset: 0x000ABB97
		internal void SetOwner(RadMediaPlayer ownerMediaPlayer)
		{
			this.owner = ownerMediaPlayer;
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000AD9A0 File Offset: 0x000ABBA0
		public bool IsTrackingViewState
		{
			get
			{
				return this.trackViewState;
			}
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x000AD9A8 File Offset: 0x000ABBA8
		public void LoadViewState(object state)
		{
			this.LoadState(state);
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x000AD9B1 File Offset: 0x000ABBB1
		public object SaveViewState()
		{
			return this.SaveState().ToArray();
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000AD9BE File Offset: 0x000ABBBE
		public void TrackViewState()
		{
			this.TrackState();
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000AD9C8 File Offset: 0x000ABBC8
		protected virtual void LoadState(object state)
		{
			object[] array = state as object[];
			if (array[0] != null)
			{
				this.Path = array[0].ToString();
			}
			bool flag;
			if (array.Length > 1 && array[1] != null && bool.TryParse(array[1].ToString(), out flag))
			{
				this.AutoPlay = flag;
			}
			double startTime;
			if (array.Length > 2 && array[2] != null && double.TryParse(array[2].ToString(), out startTime))
			{
				this.StartTime = startTime;
			}
			int startVolume;
			if (array.Length > 3 && array[3] != null && int.TryParse(array[3].ToString(), out startVolume))
			{
				this.StartVolume = startVolume;
			}
			if (array.Length > 4 && array[4] != null)
			{
				this.Poster = array[4].ToString();
			}
			if (array.Length > 5 && array[5] != null)
			{
				this.Title = array[5].ToString();
			}
			if (array.Length > 6)
			{
				((IStateManager)this.Sources).LoadViewState(array[6]);
			}
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x000ADAA0 File Offset: 0x000ABCA0
		protected virtual ArrayList SaveState()
		{
			return new ArrayList
			{
				this.Path,
				this.AutoPlay,
				this.StartTime,
				this.StartVolume,
				this.Poster,
				this.Title,
				((IStateManager)this.Sources).SaveViewState()
			};
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000ADB23 File Offset: 0x000ABD23
		protected virtual void TrackState()
		{
			this.trackViewState = true;
			((IStateManager)this.Sources).TrackViewState();
		}

		// Token: 0x04000E3A RID: 3642
		private RadMediaPlayer owner;

		// Token: 0x04000E3B RID: 3643
		private MediaPlayerSourcesCollection sources;

		// Token: 0x04000E3C RID: 3644
		private bool trackViewState;

		// Token: 0x04000E3D RID: 3645
		private bool autoPlay;
	}
}
