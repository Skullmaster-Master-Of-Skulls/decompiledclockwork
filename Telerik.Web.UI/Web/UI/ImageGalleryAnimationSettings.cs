using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000538 RID: 1336
	public class ImageGalleryAnimationSettings : ImageGallerySettings
	{
		// Token: 0x06002F41 RID: 12097 RVA: 0x0009A93C File Offset: 0x00098B3C
		public ImageGalleryAnimationSettings(RadImageGallery gallery) : base(gallery)
		{
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x0009A945 File Offset: 0x00098B45
		internal bool IsDefault()
		{
			return false;
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06002F43 RID: 12099 RVA: 0x0009A948 File Offset: 0x00098B48
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("Gets the animation settings for the animation performed when going to the next image in the RadImageGallery.")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ImageGalleryAnimationSetting NextImagesAnimation
		{
			get
			{
				if (this.nextImagesAnimation == null)
				{
					this.nextImagesAnimation = new ImageGalleryAnimationSetting();
				}
				if (this.IsTrackingViewState)
				{
					((IStateManager)this.nextImagesAnimation).TrackViewState();
				}
				return this.nextImagesAnimation;
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x0009A976 File Offset: 0x00098B76
		[Description("Gets the animation settings for the animation performed when going to the previous image in the RadImageGallery.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ImageGalleryAnimationSetting PrevImagesAnimation
		{
			get
			{
				if (this.prevImagesAnimation == null)
				{
					this.prevImagesAnimation = new ImageGalleryAnimationSetting();
				}
				if (this.IsTrackingViewState)
				{
					((IStateManager)this.prevImagesAnimation).TrackViewState();
				}
				return this.prevImagesAnimation;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06002F45 RID: 12101 RVA: 0x0009A9A4 File Offset: 0x00098BA4
		// (set) Token: 0x06002F46 RID: 12102 RVA: 0x0009A9D1 File Offset: 0x00098BD1
		[DefaultValue(2000)]
		[Description("Gets or sets a value indicating how many milliseconds will the RadImageGallery control will wait until it switches to the next image when the slideshow functionality is turned on.")]
		[NotifyParentProperty(true)]
		public int SlideshowSlideDuration
		{
			get
			{
				object obj = base.ViewState["SlideshowSlideDuration"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 2000;
			}
			set
			{
				base.ViewState["SlideshowSlideDuration"] = value;
			}
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x0009A9EC File Offset: 0x00098BEC
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			object value = base.SaveViewState();
			arrayList.Add(value);
			arrayList.Add(((IStateManager)this.NextImagesAnimation).SaveViewState());
			arrayList.Add(((IStateManager)this.PrevImagesAnimation).SaveViewState());
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x0009AA44 File Offset: 0x00098C44
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				int num = 0;
				base.LoadViewState(array[num++]);
				((IStateManager)this.NextImagesAnimation).LoadViewState(array[num++]);
				((IStateManager)this.PrevImagesAnimation).LoadViewState(array[num++]);
			}
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x0009AA8E File Offset: 0x00098C8E
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.IsTrackingViewState)
			{
				return;
			}
			((IStateManager)this.NextImagesAnimation).TrackViewState();
			((IStateManager)this.PrevImagesAnimation).TrackViewState();
		}

		// Token: 0x04000CB3 RID: 3251
		private ImageGalleryAnimationSetting nextImagesAnimation;

		// Token: 0x04000CB4 RID: 3252
		private ImageGalleryAnimationSetting prevImagesAnimation;
	}
}
