using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000562 RID: 1378
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class LightBoxAnimationSettings : StateManager
	{
		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x060031A5 RID: 12709 RVA: 0x000A3034 File Offset: 0x000A1234
		// (set) Token: 0x060031A6 RID: 12710 RVA: 0x000A305D File Offset: 0x000A125D
		[DefaultValue(LightBoxAnimationType.None)]
		[NotifyParentProperty(true)]
		public LightBoxAnimationType ShowAnimation
		{
			get
			{
				object obj = base.ViewState["ShowAnimation"];
				if (obj != null)
				{
					return (LightBoxAnimationType)obj;
				}
				return LightBoxAnimationType.None;
			}
			set
			{
				base.ViewState["ShowAnimation"] = value;
			}
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x000A3078 File Offset: 0x000A1278
		// (set) Token: 0x060031A8 RID: 12712 RVA: 0x000A30A1 File Offset: 0x000A12A1
		[DefaultValue(LightBoxAnimationType.None)]
		[NotifyParentProperty(true)]
		public LightBoxAnimationType HideAnimation
		{
			get
			{
				object obj = base.ViewState["HideAnimation"];
				if (obj != null)
				{
					return (LightBoxAnimationType)obj;
				}
				return LightBoxAnimationType.None;
			}
			set
			{
				base.ViewState["HideAnimation"] = value;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x060031A9 RID: 12713 RVA: 0x000A30BC File Offset: 0x000A12BC
		// (set) Token: 0x060031AA RID: 12714 RVA: 0x000A30E5 File Offset: 0x000A12E5
		[NotifyParentProperty(true)]
		[DefaultValue(LightBoxAnimationType.None)]
		public LightBoxAnimationType PrevAnimation
		{
			get
			{
				object obj = base.ViewState["PrevAnimation"];
				if (obj != null)
				{
					return (LightBoxAnimationType)obj;
				}
				return LightBoxAnimationType.None;
			}
			set
			{
				base.ViewState["PrevAnimation"] = value;
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x000A3100 File Offset: 0x000A1300
		// (set) Token: 0x060031AC RID: 12716 RVA: 0x000A3129 File Offset: 0x000A1329
		[DefaultValue(LightBoxAnimationType.None)]
		[NotifyParentProperty(true)]
		public LightBoxAnimationType NextAnimation
		{
			get
			{
				object obj = base.ViewState["NextAnimation"];
				if (obj != null)
				{
					return (LightBoxAnimationType)obj;
				}
				return LightBoxAnimationType.None;
			}
			set
			{
				base.ViewState["NextAnimation"] = value;
			}
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x000A3141 File Offset: 0x000A1341
		[Description("RadLightBox Show Animation Settings")]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		public LightBoxAnimationSetting ShowAnimationSettings
		{
			get
			{
				if (this.showAnimationSetting == null)
				{
					this.showAnimationSetting = new LightBoxAnimationSetting();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.showAnimationSetting).TrackViewState();
					}
				}
				return this.showAnimationSetting;
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x000A316F File Offset: 0x000A136F
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("RadLightBox Hide Animation Settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public LightBoxAnimationSetting HideAnimationSettings
		{
			get
			{
				if (this.hideAnimationSetting == null)
				{
					this.hideAnimationSetting = new LightBoxAnimationSetting();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.hideAnimationSetting).TrackViewState();
					}
				}
				return this.hideAnimationSetting;
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x000A319D File Offset: 0x000A139D
		[Description("RadLightBox Next Animation Settings")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public LightBoxAnimationSetting NextAnimationSettings
		{
			get
			{
				if (this.nextAnimationSetting == null)
				{
					this.nextAnimationSetting = new LightBoxAnimationSetting();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.nextAnimationSetting).TrackViewState();
					}
				}
				return this.nextAnimationSetting;
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x060031B0 RID: 12720 RVA: 0x000A31CB File Offset: 0x000A13CB
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("RadLightBox Prev Animation Settings")]
		[Category("Appearance")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public LightBoxAnimationSetting PrevAnimationSettings
		{
			get
			{
				if (this.prevAnimationSetting == null)
				{
					this.prevAnimationSetting = new LightBoxAnimationSetting();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.prevAnimationSetting).TrackViewState();
					}
				}
				return this.prevAnimationSetting;
			}
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000A31FC File Offset: 0x000A13FC
		protected override void LoadViewState(object baseState)
		{
			if (baseState != null)
			{
				object[] array = (object[])baseState;
				base.LoadViewState(array[0]);
				((IStateManager)this.ShowAnimationSettings).LoadViewState(array[1]);
				((IStateManager)this.HideAnimationSettings).LoadViewState(array[2]);
				((IStateManager)this.PrevAnimationSettings).LoadViewState(array[3]);
				((IStateManager)this.NextAnimationSettings).LoadViewState(array[4]);
			}
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000A3254 File Offset: 0x000A1454
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.ShowAnimationSettings).SaveViewState(),
				((IStateManager)this.HideAnimationSettings).SaveViewState(),
				((IStateManager)this.PrevAnimationSettings).SaveViewState(),
				((IStateManager)this.NextAnimationSettings).SaveViewState()
			};
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000A32CE File Offset: 0x000A14CE
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ShowAnimationSettings).TrackViewState();
			((IStateManager)this.HideAnimationSettings).TrackViewState();
			((IStateManager)this.PrevAnimationSettings).TrackViewState();
			((IStateManager)this.NextAnimationSettings).TrackViewState();
		}

		// Token: 0x04000D6E RID: 3438
		private LightBoxAnimationSetting showAnimationSetting;

		// Token: 0x04000D6F RID: 3439
		private LightBoxAnimationSetting hideAnimationSetting;

		// Token: 0x04000D70 RID: 3440
		private LightBoxAnimationSetting prevAnimationSetting;

		// Token: 0x04000D71 RID: 3441
		private LightBoxAnimationSetting nextAnimationSetting;
	}
}
