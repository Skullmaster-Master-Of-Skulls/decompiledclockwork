using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000432 RID: 1074
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public abstract class HotSpot : IStateManager
	{
		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06003406 RID: 13318 RVA: 0x000A99F0 File Offset: 0x000A7BF0
		// (set) Token: 0x06003407 RID: 13319 RVA: 0x000A9A1D File Offset: 0x000A7C1D
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Accessibility")]
		[WebSysDescription("HotSpot_AccessKey")]
		public virtual string AccessKey
		{
			get
			{
				string text = (string)this.ViewState["AccessKey"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value != null && value.Length > 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["AccessKey"] = value;
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06003408 RID: 13320 RVA: 0x000A9A48 File Offset: 0x000A7C48
		// (set) Token: 0x06003409 RID: 13321 RVA: 0x000A9A75 File Offset: 0x000A7C75
		[Localizable(true)]
		[Bindable(true)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("HotSpot_AlternateText")]
		[NotifyParentProperty(true)]
		public virtual string AlternateText
		{
			get
			{
				object obj = this.ViewState["AlternateText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["AlternateText"] = value;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x0600340A RID: 13322 RVA: 0x000A9A88 File Offset: 0x000A7C88
		// (set) Token: 0x0600340B RID: 13323 RVA: 0x000A9AB1 File Offset: 0x000A7CB1
		[WebCategory("Behavior")]
		[DefaultValue(HotSpotMode.NotSet)]
		[WebSysDescription("HotSpot_HotSpotMode")]
		[NotifyParentProperty(true)]
		public virtual HotSpotMode HotSpotMode
		{
			get
			{
				object obj = this.ViewState["HotSpotMode"];
				if (obj != null)
				{
					return (HotSpotMode)obj;
				}
				return HotSpotMode.NotSet;
			}
			set
			{
				if (value < HotSpotMode.NotSet || value > HotSpotMode.Inactive)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["HotSpotMode"] = value;
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x0600340C RID: 13324 RVA: 0x000A9ADC File Offset: 0x000A7CDC
		// (set) Token: 0x0600340D RID: 13325 RVA: 0x000A9B09 File Offset: 0x000A7D09
		[Bindable(true)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("HotSpot_PostBackValue")]
		[NotifyParentProperty(true)]
		public string PostBackValue
		{
			get
			{
				object obj = this.ViewState["PostBackValue"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PostBackValue"] = value;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x0600340E RID: 13326
		protected internal abstract string MarkupName { get; }

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x000A9B1C File Offset: 0x000A7D1C
		// (set) Token: 0x06003410 RID: 13328 RVA: 0x000A9B49 File Offset: 0x000A7D49
		[Bindable(true)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("HotSpot_NavigateUrl")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string NavigateUrl
		{
			get
			{
				object obj = this.ViewState["NavigateUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x000A9B5C File Offset: 0x000A7D5C
		// (set) Token: 0x06003412 RID: 13330 RVA: 0x000A9B85 File Offset: 0x000A7D85
		[DefaultValue(0)]
		[WebCategory("Accessibility")]
		[WebSysDescription("HotSpot_TabIndex")]
		public virtual short TabIndex
		{
			get
			{
				object obj = this.ViewState["TabIndex"];
				if (obj != null)
				{
					return (short)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["TabIndex"] = value;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x000A9BA0 File Offset: 0x000A7DA0
		// (set) Token: 0x06003414 RID: 13332 RVA: 0x000A9BCD File Offset: 0x000A7DCD
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[TypeConverter(typeof(TargetConverter))]
		[WebSysDescription("HotSpot_Target")]
		[NotifyParentProperty(true)]
		public virtual string Target
		{
			get
			{
				object obj = this.ViewState["Target"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x000A9BE0 File Offset: 0x000A7DE0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag(false);
					if (this._isTrackingViewState)
					{
						((IStateManager)this._viewState).TrackViewState();
					}
				}
				return this._viewState;
			}
		}

		// Token: 0x06003416 RID: 13334
		public abstract string GetCoordinates();

		// Token: 0x06003417 RID: 13335 RVA: 0x000A9C0F File Offset: 0x000A7E0F
		internal void SetDirty()
		{
			if (this._viewState != null)
			{
				this._viewState.SetDirty(true);
			}
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000A9C25 File Offset: 0x000A7E25
		public override string ToString()
		{
			return base.GetType().Name;
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06003419 RID: 13337 RVA: 0x000A9C32 File Offset: 0x000A7E32
		protected virtual bool IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000A9C3A File Offset: 0x000A7E3A
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this.ViewState.LoadViewState(savedState);
			}
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000A9C4B File Offset: 0x000A7E4B
		protected virtual object SaveViewState()
		{
			if (this._viewState != null)
			{
				return this._viewState.SaveViewState();
			}
			return null;
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000A9C62 File Offset: 0x000A7E62
		protected virtual void TrackViewState()
		{
			this._isTrackingViewState = true;
			if (this._viewState != null)
			{
				this._viewState.TrackViewState();
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x0600341D RID: 13341 RVA: 0x000A9C7E File Offset: 0x000A7E7E
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000A9C86 File Offset: 0x000A7E86
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000A9C8F File Offset: 0x000A7E8F
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000A9C97 File Offset: 0x000A7E97
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x04002190 RID: 8592
		private bool _isTrackingViewState;

		// Token: 0x04002191 RID: 8593
		private StateBag _viewState;
	}
}
