using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004F6 RID: 1270
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HotSpot : IStateManager
	{
		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06003DEF RID: 15855 RVA: 0x00103230 File Offset: 0x00102230
		// (set) Token: 0x06003DF0 RID: 15856 RVA: 0x0010325D File Offset: 0x0010225D
		[WebCategory("Accessibility")]
		[WebSysDescription("HotSpot_AccessKey")]
		[Localizable(true)]
		[DefaultValue("")]
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

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06003DF1 RID: 15857 RVA: 0x00103288 File Offset: 0x00102288
		// (set) Token: 0x06003DF2 RID: 15858 RVA: 0x001032B5 File Offset: 0x001022B5
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Bindable(true)]
		[WebCategory("Behavior")]
		[WebSysDescription("HotSpot_AlternateText")]
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

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x001032C8 File Offset: 0x001022C8
		// (set) Token: 0x06003DF4 RID: 15860 RVA: 0x001032F1 File Offset: 0x001022F1
		[WebCategory("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(HotSpotMode.NotSet)]
		[WebSysDescription("HotSpot_HotSpotMode")]
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

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06003DF5 RID: 15861 RVA: 0x0010331C File Offset: 0x0010231C
		// (set) Token: 0x06003DF6 RID: 15862 RVA: 0x00103349 File Offset: 0x00102349
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("HotSpot_PostBackValue")]
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

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06003DF7 RID: 15863
		protected internal abstract string MarkupName { get; }

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x0010335C File Offset: 0x0010235C
		// (set) Token: 0x06003DF9 RID: 15865 RVA: 0x00103389 File Offset: 0x00102389
		[WebSysDescription("HotSpot_NavigateUrl")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Bindable(true)]
		[UrlProperty]
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

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06003DFA RID: 15866 RVA: 0x0010339C File Offset: 0x0010239C
		// (set) Token: 0x06003DFB RID: 15867 RVA: 0x001033C5 File Offset: 0x001023C5
		[WebSysDescription("HotSpot_TabIndex")]
		[DefaultValue(0)]
		[WebCategory("Accessibility")]
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

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06003DFC RID: 15868 RVA: 0x001033E0 File Offset: 0x001023E0
		// (set) Token: 0x06003DFD RID: 15869 RVA: 0x0010340D File Offset: 0x0010240D
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[WebCategory("Behavior")]
		[TypeConverter(typeof(TargetConverter))]
		[WebSysDescription("HotSpot_Target")]
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

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003DFE RID: 15870 RVA: 0x00103420 File Offset: 0x00102420
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x06003DFF RID: 15871
		public abstract string GetCoordinates();

		// Token: 0x06003E00 RID: 15872 RVA: 0x0010344F File Offset: 0x0010244F
		internal void SetDirty()
		{
			if (this._viewState != null)
			{
				this._viewState.SetDirty(true);
			}
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x00103465 File Offset: 0x00102465
		public override string ToString()
		{
			return base.GetType().Name;
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x00103472 File Offset: 0x00102472
		protected virtual bool IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x0010347A File Offset: 0x0010247A
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this.ViewState.LoadViewState(savedState);
			}
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x0010348B File Offset: 0x0010248B
		protected virtual object SaveViewState()
		{
			if (this._viewState != null)
			{
				return this._viewState.SaveViewState();
			}
			return null;
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x001034A2 File Offset: 0x001024A2
		protected virtual void TrackViewState()
		{
			this._isTrackingViewState = true;
			if (this._viewState != null)
			{
				this._viewState.TrackViewState();
			}
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x001034BE File Offset: 0x001024BE
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x001034C6 File Offset: 0x001024C6
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x001034CF File Offset: 0x001024CF
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x001034D7 File Offset: 0x001024D7
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x0400278F RID: 10127
		private bool _isTrackingViewState;

		// Token: 0x04002790 RID: 10128
		private StateBag _viewState;
	}
}
