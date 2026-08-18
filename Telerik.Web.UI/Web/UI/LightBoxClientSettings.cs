using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200056E RID: 1390
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class LightBoxClientSettings : StateManager
	{
		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x000A3AE9 File Offset: 0x000A1CE9
		// (set) Token: 0x060031DC RID: 12764 RVA: 0x000A3AF1 File Offset: 0x000A1CF1
		private RadLightBox Owner { get; set; }

		// Token: 0x060031DD RID: 12765 RVA: 0x000A3AFA File Offset: 0x000A1CFA
		internal LightBoxClientSettings(RadLightBox owner)
		{
			this.Owner = owner;
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x000A3B09 File Offset: 0x000A1D09
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client-side events")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Description("RadLightBox Client Events")]
		public LightBoxClientEvents ClientEvents
		{
			get
			{
				if (this.clientEvents == null)
				{
					this.clientEvents = new LightBoxClientEvents();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.clientEvents).TrackViewState();
					}
				}
				return this.clientEvents;
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x000A3B37 File Offset: 0x000A1D37
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("RadLightBox Client Events")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client-side events")]
		public LightBoxClientDataBinding DataBinding
		{
			get
			{
				if (this.dataBinding == null)
				{
					this.dataBinding = new LightBoxClientDataBinding();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.dataBinding).TrackViewState();
					}
				}
				return this.dataBinding;
			}
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x000A3B65 File Offset: 0x000A1D65
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("RadLightBox Animation Settings")]
		public LightBoxAnimationSettings AnimationSettings
		{
			get
			{
				if (this.animationSettings == null)
				{
					this.animationSettings = new LightBoxAnimationSettings();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.animationSettings).TrackViewState();
					}
				}
				return this.animationSettings;
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x000A3B94 File Offset: 0x000A1D94
		// (set) Token: 0x060031E2 RID: 12770 RVA: 0x000A3BC2 File Offset: 0x000A1DC2
		[NotifyParentProperty(true)]
		[DefaultValue(LightBoxFullscreenMode.Emulation)]
		[Description("Determines the fullscreen mode")]
		public LightBoxFullscreenMode FullscreenMode
		{
			get
			{
				object obj = base.ViewState["FullscreenMode"];
				if (obj == null)
				{
					obj = LightBoxFullscreenMode.Emulation;
				}
				return (LightBoxFullscreenMode)obj;
			}
			set
			{
				base.ViewState["FullscreenMode"] = value;
			}
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x000A3BDC File Offset: 0x000A1DDC
		// (set) Token: 0x060031E4 RID: 12772 RVA: 0x000A3C1C File Offset: 0x000A1E1C
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool AllowKeyboardNavigation
		{
			get
			{
				object obj = base.ViewState["AllowKeyboardNavigation"];
				if (obj != null)
				{
					return (bool)obj;
				}
				return this.Owner != null && this.Owner.EnableAriaSupport;
			}
			set
			{
				base.ViewState["AllowKeyboardNavigation"] = value;
			}
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x000A3C34 File Offset: 0x000A1E34
		// (set) Token: 0x060031E6 RID: 12774 RVA: 0x000A3C5D File Offset: 0x000A1E5D
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool AutoResize
		{
			get
			{
				object obj = base.ViewState["AutoResize"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["AutoResize"] = value;
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x000A3C78 File Offset: 0x000A1E78
		// (set) Token: 0x060031E8 RID: 12776 RVA: 0x000A3CA1 File Offset: 0x000A1EA1
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool PreventOverlayClose
		{
			get
			{
				object obj = base.ViewState["PreventOverlayClose"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["PreventOverlayClose"] = value;
			}
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x000A3CBC File Offset: 0x000A1EBC
		// (set) Token: 0x060031EA RID: 12778 RVA: 0x000A3CE5 File Offset: 0x000A1EE5
		[Description("Shows/hides the items counter. Default value is true.")]
		[DefaultValue(true)]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public bool ShowItemsCounter
		{
			get
			{
				object obj = base.ViewState["ShowItemsCounter"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowItemsCounter"] = value;
			}
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x000A3D00 File Offset: 0x000A1F00
		// (set) Token: 0x060031EC RID: 12780 RVA: 0x000A3D2E File Offset: 0x000A1F2E
		[Description("Determines the navigation mode")]
		[NotifyParentProperty(true)]
		[DefaultValue(LightBoxNavigationMode.Button)]
		public LightBoxNavigationMode NavigationMode
		{
			get
			{
				object obj = base.ViewState["NavigationMode"];
				if (obj == null)
				{
					obj = LightBoxNavigationMode.Button;
				}
				return (LightBoxNavigationMode)obj;
			}
			set
			{
				base.ViewState["NavigationMode"] = value;
			}
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x000A3D48 File Offset: 0x000A1F48
		// (set) Token: 0x060031EE RID: 12782 RVA: 0x000A3D76 File Offset: 0x000A1F76
		[Description("Determines the way the image resizing works in full screen (maximized) mode")]
		[DefaultValue(LightBoxContentResizeMode.Fit)]
		[NotifyParentProperty(true)]
		public LightBoxContentResizeMode ContentResizeMode
		{
			get
			{
				object obj = base.ViewState["ContentResizeMode"];
				if (obj == null)
				{
					obj = LightBoxContentResizeMode.Fit;
				}
				return (LightBoxContentResizeMode)obj;
			}
			set
			{
				base.ViewState["ContentResizeMode"] = value;
			}
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000A3D90 File Offset: 0x000A1F90
		protected override void LoadViewState(object baseState)
		{
			if (baseState != null)
			{
				object[] array = (object[])baseState;
				base.LoadViewState(array[0]);
				((IStateManager)this.ClientEvents).LoadViewState(array[1]);
				((IStateManager)this.AnimationSettings).LoadViewState(array[2]);
			}
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000A3DCC File Offset: 0x000A1FCC
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState(),
				((IStateManager)this.AnimationSettings).SaveViewState()
			};
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000A3E22 File Offset: 0x000A2022
		protected override void TrackViewState()
		{
			((IStateManager)this.ClientEvents).TrackViewState();
			((IStateManager)this.AnimationSettings).TrackViewState();
			base.TrackViewState();
		}

		// Token: 0x04000DB5 RID: 3509
		private LightBoxClientEvents clientEvents;

		// Token: 0x04000DB6 RID: 3510
		private LightBoxAnimationSettings animationSettings;

		// Token: 0x04000DB7 RID: 3511
		private LightBoxClientDataBinding dataBinding;
	}
}
