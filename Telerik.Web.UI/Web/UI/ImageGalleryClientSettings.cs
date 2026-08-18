using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200053B RID: 1339
	public class ImageGalleryClientSettings : ImageGallerySettings
	{
		// Token: 0x06002F66 RID: 12134 RVA: 0x0009AD94 File Offset: 0x00098F94
		public ImageGalleryClientSettings(RadImageGallery gallery) : base(gallery)
		{
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06002F67 RID: 12135 RVA: 0x0009ADA0 File Offset: 0x00098FA0
		// (set) Token: 0x06002F68 RID: 12136 RVA: 0x0009ADC9 File Offset: 0x00098FC9
		[Description("Gets or sets a value determining if the RadImageGallery keyboard navigation will be turned on.")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool AllowKeyboardNavigation
		{
			get
			{
				object obj = base.ViewState["AllowKeyboardNavigation"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowKeyboardNavigation"] = value;
				this.Gallery.LightBox.ClientSettings.AllowKeyboardNavigation = value;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06002F69 RID: 12137 RVA: 0x0009ADF7 File Offset: 0x00098FF7
		[Description("Gets or sets the settings associated with the keyboard navigation. Individual settings could be customized together with defining different shortcut combinations or disabling specific shortcuts.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public ImageGalleryKeyboardNavigationSettings KeyboardNavigationSettings
		{
			get
			{
				if (this.keyboardNavigationSettings == null)
				{
					this.keyboardNavigationSettings = new ImageGalleryKeyboardNavigationSettings(this.Gallery);
				}
				if (this.IsTrackingViewState)
				{
					((IStateManager)this.keyboardNavigationSettings).TrackViewState();
				}
				return this.keyboardNavigationSettings;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06002F6A RID: 12138 RVA: 0x0009AE2B File Offset: 0x0009902B
		[Category("Appearance")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Description("Gets the animation settings for the RadImageGallery. Inner settings determine the animations between images.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ImageGalleryAnimationSettings AnimationSettings
		{
			get
			{
				if (this.animationSettings == null)
				{
					this.animationSettings = new ImageGalleryAnimationSettings(this.Gallery);
				}
				if (this.IsTrackingViewState)
				{
					((IStateManager)this.animationSettings).TrackViewState();
				}
				return this.animationSettings;
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06002F6B RID: 12139 RVA: 0x0009AE5F File Offset: 0x0009905F
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Category("Client-side events")]
		[Description("Gets a reference to ImageGalleryClientEvents, which holds properties for setting the ImageGallery client-side events")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ImageGalleryClientEvents ClientEvents
		{
			get
			{
				if (this.clientEvents == null)
				{
					this.clientEvents = new ImageGalleryClientEvents(this.Gallery);
				}
				if (this.IsTrackingViewState)
				{
					((IStateManager)this.clientEvents).TrackViewState();
				}
				return this.clientEvents;
			}
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x0009AE94 File Offset: 0x00099094
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			object value = base.SaveViewState();
			arrayList.Add(value);
			arrayList.Add(((IStateManager)this.AnimationSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.ClientEvents).SaveViewState());
			arrayList.Add(((IStateManager)this.KeyboardNavigationSettings).SaveViewState());
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x0009AEFC File Offset: 0x000990FC
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				int num = 0;
				base.LoadViewState(array[num++]);
				((IStateManager)this.AnimationSettings).LoadViewState(array[num++]);
				((IStateManager)this.ClientEvents).LoadViewState(array[num++]);
				((IStateManager)this.KeyboardNavigationSettings).LoadViewState(array[num++]);
			}
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x0009AF58 File Offset: 0x00099158
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.IsTrackingViewState)
			{
				return;
			}
			((IStateManager)this.AnimationSettings).TrackViewState();
			((IStateManager)this.ClientEvents).TrackViewState();
			((IStateManager)this.KeyboardNavigationSettings).TrackViewState();
		}

		// Token: 0x04000CB5 RID: 3253
		private ImageGalleryKeyboardNavigationSettings keyboardNavigationSettings;

		// Token: 0x04000CB6 RID: 3254
		private ImageGalleryAnimationSettings animationSettings;

		// Token: 0x04000CB7 RID: 3255
		private ImageGalleryClientEvents clientEvents;
	}
}
