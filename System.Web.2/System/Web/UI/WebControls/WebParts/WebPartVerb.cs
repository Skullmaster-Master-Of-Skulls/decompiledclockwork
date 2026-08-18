using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005B3 RID: 1459
	[TypeConverter(typeof(EmptyStringExpandableObjectConverter))]
	public class WebPartVerb : IStateManager
	{
		// Token: 0x060049C1 RID: 18881 RVA: 0x000F4F9F File Offset: 0x000F319F
		internal WebPartVerb()
		{
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x000F4FAE File Offset: 0x000F31AE
		private WebPartVerb(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("id");
			}
			this._id = id;
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x000F4FD7 File Offset: 0x000F31D7
		public WebPartVerb(string id, WebPartEventHandler serverClickHandler) : this(id)
		{
			if (serverClickHandler == null)
			{
				throw new ArgumentNullException("serverClickHandler");
			}
			this._serverClickHandler = serverClickHandler;
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x000F4FF5 File Offset: 0x000F31F5
		public WebPartVerb(string id, string clientClickHandler) : this(id)
		{
			if (string.IsNullOrEmpty(clientClickHandler))
			{
				throw new ArgumentNullException("clientClickHandler");
			}
			this._clientClickHandler = clientClickHandler;
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x000F5018 File Offset: 0x000F3218
		public WebPartVerb(string id, WebPartEventHandler serverClickHandler, string clientClickHandler) : this(id)
		{
			if (serverClickHandler == null)
			{
				throw new ArgumentNullException("serverClickHandler");
			}
			if (string.IsNullOrEmpty(clientClickHandler))
			{
				throw new ArgumentNullException("clientClickHandler");
			}
			this._serverClickHandler = serverClickHandler;
			this._clientClickHandler = clientClickHandler;
		}

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x060049C6 RID: 18886 RVA: 0x000F5050 File Offset: 0x000F3250
		// (set) Token: 0x060049C7 RID: 18887 RVA: 0x000F5079 File Offset: 0x000F3279
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Themeable(false)]
		[WebSysDescription("WebPartVerb_Checked")]
		public virtual bool Checked
		{
			get
			{
				object obj = this.ViewState["Checked"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["Checked"] = value;
			}
		}

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x060049C8 RID: 18888 RVA: 0x000F5091 File Offset: 0x000F3291
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ClientClickHandler
		{
			get
			{
				if (this._clientClickHandler != null)
				{
					return this._clientClickHandler;
				}
				return string.Empty;
			}
		}

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x060049C9 RID: 18889 RVA: 0x000F50A8 File Offset: 0x000F32A8
		// (set) Token: 0x060049CA RID: 18890 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[WebSysDefaultValue("")]
		[WebSysDescription("WebPartVerb_Description")]
		public virtual string Description
		{
			get
			{
				object obj = this.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Description"] = value;
			}
		}

		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x060049CB RID: 18891 RVA: 0x000F50D8 File Offset: 0x000F32D8
		// (set) Token: 0x060049CC RID: 18892 RVA: 0x000F5101 File Offset: 0x000F3301
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Themeable(false)]
		[WebSysDescription("WebPartVerb_Enabled")]
		public virtual bool Enabled
		{
			get
			{
				object obj = this.ViewState["Enabled"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x060049CD RID: 18893 RVA: 0x000F5119 File Offset: 0x000F3319
		// (set) Token: 0x060049CE RID: 18894 RVA: 0x000F512F File Offset: 0x000F332F
		internal string EventArgument
		{
			get
			{
				if (this._eventArgument == null)
				{
					return string.Empty;
				}
				return this._eventArgument;
			}
			set
			{
				this._eventArgument = value;
			}
		}

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x060049CF RID: 18895 RVA: 0x000F5138 File Offset: 0x000F3338
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ID
		{
			get
			{
				if (this._id == null)
				{
					return string.Empty;
				}
				return this._id;
			}
		}

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x060049D0 RID: 18896 RVA: 0x000F5150 File Offset: 0x000F3350
		// (set) Token: 0x060049D1 RID: 18897 RVA: 0x000F517D File Offset: 0x000F337D
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[UrlProperty]
		[WebSysDescription("WebPartVerb_ImageUrl")]
		public virtual string ImageUrl
		{
			get
			{
				object obj = this.ViewState["ImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x060049D2 RID: 18898 RVA: 0x000F5190 File Offset: 0x000F3390
		protected virtual bool IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x060049D3 RID: 18899 RVA: 0x000F5198 File Offset: 0x000F3398
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebPartEventHandler ServerClickHandler
		{
			get
			{
				return this._serverClickHandler;
			}
		}

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x060049D4 RID: 18900 RVA: 0x000F51A0 File Offset: 0x000F33A0
		// (set) Token: 0x060049D5 RID: 18901 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[WebSysDefaultValue("")]
		[WebSysDescription("WebPartVerb_Text")]
		public virtual string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x060049D6 RID: 18902 RVA: 0x000F51CD File Offset: 0x000F33CD
		// (set) Token: 0x060049D7 RID: 18903 RVA: 0x000F51D5 File Offset: 0x000F33D5
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Themeable(false)]
		[WebSysDescription("WebPartVerb_Visible")]
		public virtual bool Visible
		{
			get
			{
				return this._visible;
			}
			set
			{
				this._visible = value;
				this.ViewState["Visible"] = value;
			}
		}

		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x060049D8 RID: 18904 RVA: 0x000F51F4 File Offset: 0x000F33F4
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

		// Token: 0x060049D9 RID: 18905 RVA: 0x000F5224 File Offset: 0x000F3424
		internal string GetEventArgument(string webPartID)
		{
			if (string.IsNullOrEmpty(this._eventArgumentPrefix))
			{
				return string.Empty;
			}
			if (this._id == null)
			{
				return this._eventArgumentPrefix + webPartID;
			}
			return this._eventArgumentPrefix + this._id + ":" + webPartID;
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x000F5270 File Offset: 0x000F3470
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				((IStateManager)this.ViewState).LoadViewState(savedState);
				object obj = this.ViewState["Visible"];
				if (obj != null)
				{
					this._visible = (bool)obj;
				}
			}
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x000F52AC File Offset: 0x000F34AC
		protected virtual object SaveViewState()
		{
			if (this._viewState != null)
			{
				return ((IStateManager)this._viewState).SaveViewState();
			}
			return null;
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x000F52C3 File Offset: 0x000F34C3
		internal void SetEventArgumentPrefix(string eventArgumentPrefix)
		{
			this._eventArgumentPrefix = eventArgumentPrefix;
		}

		// Token: 0x060049DD RID: 18909 RVA: 0x000F52CC File Offset: 0x000F34CC
		protected virtual void TrackViewState()
		{
			this._isTrackingViewState = true;
			if (this._viewState != null)
			{
				((IStateManager)this._viewState).TrackViewState();
			}
		}

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x060049DE RID: 18910 RVA: 0x000F52E8 File Offset: 0x000F34E8
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x000F52F0 File Offset: 0x000F34F0
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x000F52F9 File Offset: 0x000F34F9
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x000F5301 File Offset: 0x000F3501
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x040027B7 RID: 10167
		private bool _isTrackingViewState;

		// Token: 0x040027B8 RID: 10168
		private StateBag _viewState;

		// Token: 0x040027B9 RID: 10169
		private bool _visible = true;

		// Token: 0x040027BA RID: 10170
		private string _id;

		// Token: 0x040027BB RID: 10171
		private string _clientClickHandler;

		// Token: 0x040027BC RID: 10172
		private WebPartEventHandler _serverClickHandler;

		// Token: 0x040027BD RID: 10173
		private string _eventArgument;

		// Token: 0x040027BE RID: 10174
		private string _eventArgumentPrefix;
	}
}
