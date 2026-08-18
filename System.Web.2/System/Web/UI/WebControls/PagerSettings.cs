using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000498 RID: 1176
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public sealed class PagerSettings : IStateManager
	{
		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x06003A5E RID: 14942 RVA: 0x000BD638 File Offset: 0x000BB838
		// (remove) Token: 0x06003A5F RID: 14943 RVA: 0x000BD670 File Offset: 0x000BB870
		[Browsable(false)]
		public event EventHandler PropertyChanged;

		// Token: 0x06003A60 RID: 14944 RVA: 0x000BD6A5 File Offset: 0x000BB8A5
		public PagerSettings()
		{
			this._viewState = new StateBag();
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06003A61 RID: 14945 RVA: 0x000BD6B8 File Offset: 0x000BB8B8
		// (set) Token: 0x06003A62 RID: 14946 RVA: 0x000BD6E8 File Offset: 0x000BB8E8
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("PagerSettings_FirstPageImageUrl")]
		public string FirstPageImageUrl
		{
			get
			{
				object obj = this.ViewState["FirstPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				string firstPageImageUrl = this.FirstPageImageUrl;
				if (firstPageImageUrl != value)
				{
					this.ViewState["FirstPageImageUrl"] = value;
					this.OnPropertyChanged();
				}
			}
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06003A63 RID: 14947 RVA: 0x000BD71C File Offset: 0x000BB91C
		// (set) Token: 0x06003A64 RID: 14948 RVA: 0x000BD74C File Offset: 0x000BB94C
		[WebCategory("Appearance")]
		[DefaultValue("&lt;&lt;")]
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerSettings_FirstPageText")]
		public string FirstPageText
		{
			get
			{
				object obj = this.ViewState["FirstPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&lt;&lt;";
			}
			set
			{
				string firstPageText = this.FirstPageText;
				if (firstPageText != value)
				{
					this.ViewState["FirstPageText"] = value;
					this.OnPropertyChanged();
				}
			}
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x000BD780 File Offset: 0x000BB980
		internal bool IsPagerOnBottom
		{
			get
			{
				PagerPosition position = this.Position;
				return position == PagerPosition.Bottom || position == PagerPosition.TopAndBottom;
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06003A66 RID: 14950 RVA: 0x000BD7A0 File Offset: 0x000BB9A0
		internal bool IsPagerOnTop
		{
			get
			{
				PagerPosition position = this.Position;
				return position == PagerPosition.Top || position == PagerPosition.TopAndBottom;
			}
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06003A67 RID: 14951 RVA: 0x000BD7C0 File Offset: 0x000BB9C0
		// (set) Token: 0x06003A68 RID: 14952 RVA: 0x000BD7F0 File Offset: 0x000BB9F0
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("PagerSettings_LastPageImageUrl")]
		public string LastPageImageUrl
		{
			get
			{
				object obj = this.ViewState["LastPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				string lastPageImageUrl = this.LastPageImageUrl;
				if (lastPageImageUrl != value)
				{
					this.ViewState["LastPageImageUrl"] = value;
					this.OnPropertyChanged();
				}
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x000BD824 File Offset: 0x000BBA24
		// (set) Token: 0x06003A6A RID: 14954 RVA: 0x000BD854 File Offset: 0x000BBA54
		[WebCategory("Appearance")]
		[DefaultValue("&gt;&gt;")]
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerSettings_LastPageText")]
		public string LastPageText
		{
			get
			{
				object obj = this.ViewState["LastPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&gt;&gt;";
			}
			set
			{
				string lastPageText = this.LastPageText;
				if (lastPageText != value)
				{
					this.ViewState["LastPageText"] = value;
					this.OnPropertyChanged();
				}
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06003A6B RID: 14955 RVA: 0x000BD888 File Offset: 0x000BBA88
		// (set) Token: 0x06003A6C RID: 14956 RVA: 0x000BD8B4 File Offset: 0x000BBAB4
		[WebCategory("Appearance")]
		[DefaultValue(PagerButtons.Numeric)]
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerSettings_Mode")]
		public PagerButtons Mode
		{
			get
			{
				object obj = this.ViewState["PagerMode"];
				if (obj != null)
				{
					return (PagerButtons)obj;
				}
				return PagerButtons.Numeric;
			}
			set
			{
				if (value < PagerButtons.NextPrevious || value > PagerButtons.NumericFirstLast)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				PagerButtons mode = this.Mode;
				if (mode != value)
				{
					this.ViewState["PagerMode"] = value;
					this.OnPropertyChanged();
				}
			}
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x000BD8FC File Offset: 0x000BBAFC
		// (set) Token: 0x06003A6E RID: 14958 RVA: 0x000BD92C File Offset: 0x000BBB2C
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("PagerSettings_NextPageImageUrl")]
		public string NextPageImageUrl
		{
			get
			{
				object obj = this.ViewState["NextPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				string nextPageImageUrl = this.NextPageImageUrl;
				if (nextPageImageUrl != value)
				{
					this.ViewState["NextPageImageUrl"] = value;
					this.OnPropertyChanged();
				}
			}
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06003A6F RID: 14959 RVA: 0x000BD960 File Offset: 0x000BBB60
		// (set) Token: 0x06003A70 RID: 14960 RVA: 0x000BD990 File Offset: 0x000BBB90
		[WebCategory("Appearance")]
		[DefaultValue("&gt;")]
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerSettings_NextPageText")]
		public string NextPageText
		{
			get
			{
				object obj = this.ViewState["NextPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&gt;";
			}
			set
			{
				string nextPageText = this.NextPageText;
				if (nextPageText != value)
				{
					this.ViewState["NextPageText"] = value;
					this.OnPropertyChanged();
				}
			}
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06003A71 RID: 14961 RVA: 0x000BD9C4 File Offset: 0x000BBBC4
		// (set) Token: 0x06003A72 RID: 14962 RVA: 0x000BD9F0 File Offset: 0x000BBBF0
		[WebCategory("Behavior")]
		[DefaultValue(10)]
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerSettings_PageButtonCount")]
		public int PageButtonCount
		{
			get
			{
				object obj = this.ViewState["PageButtonCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				int pageButtonCount = this.PageButtonCount;
				if (pageButtonCount != value)
				{
					this.ViewState["PageButtonCount"] = value;
					this.OnPropertyChanged();
				}
			}
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06003A73 RID: 14963 RVA: 0x000BDA34 File Offset: 0x000BBC34
		// (set) Token: 0x06003A74 RID: 14964 RVA: 0x000BDA5D File Offset: 0x000BBC5D
		[WebCategory("Layout")]
		[DefaultValue(PagerPosition.Bottom)]
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerStyle_Position")]
		public PagerPosition Position
		{
			get
			{
				object obj = this.ViewState["Position"];
				if (obj != null)
				{
					return (PagerPosition)obj;
				}
				return PagerPosition.Bottom;
			}
			set
			{
				if (value < PagerPosition.Bottom || value > PagerPosition.TopAndBottom)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Position"] = value;
			}
		}

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x000BDA88 File Offset: 0x000BBC88
		// (set) Token: 0x06003A76 RID: 14966 RVA: 0x000BDAB8 File Offset: 0x000BBCB8
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("PagerSettings_PreviousPageImageUrl")]
		public string PreviousPageImageUrl
		{
			get
			{
				object obj = this.ViewState["PreviousPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				string previousPageImageUrl = this.PreviousPageImageUrl;
				if (previousPageImageUrl != value)
				{
					this.ViewState["PreviousPageImageUrl"] = value;
					this.OnPropertyChanged();
				}
			}
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06003A77 RID: 14967 RVA: 0x000BDAEC File Offset: 0x000BBCEC
		// (set) Token: 0x06003A78 RID: 14968 RVA: 0x000BDB1C File Offset: 0x000BBD1C
		[WebCategory("Appearance")]
		[DefaultValue("&lt;")]
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerSettings_PreviousPageText")]
		public string PreviousPageText
		{
			get
			{
				object obj = this.ViewState["PreviousPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&lt;";
			}
			set
			{
				string previousPageText = this.PreviousPageText;
				if (previousPageText != value)
				{
					this.ViewState["PreviousPageText"] = value;
					this.OnPropertyChanged();
				}
			}
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06003A79 RID: 14969 RVA: 0x000BDB50 File Offset: 0x000BBD50
		// (set) Token: 0x06003A7A RID: 14970 RVA: 0x000BDB79 File Offset: 0x000BBD79
		[WebCategory("Appearance")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerStyle_Visible")]
		public bool Visible
		{
			get
			{
				object obj = this.ViewState["PagerVisible"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["PagerVisible"] = value;
			}
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06003A7B RID: 14971 RVA: 0x000BDB91 File Offset: 0x000BBD91
		private StateBag ViewState
		{
			get
			{
				return this._viewState;
			}
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x000BDB99 File Offset: 0x000BBD99
		private void OnPropertyChanged()
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x00028752 File Offset: 0x00026952
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x000BDBB4 File Offset: 0x000BBDB4
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTracking;
			}
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x000BDBBC File Offset: 0x000BBDBC
		void IStateManager.LoadViewState(object state)
		{
			if (state != null)
			{
				((IStateManager)this.ViewState).LoadViewState(state);
			}
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x000BDBD0 File Offset: 0x000BBDD0
		object IStateManager.SaveViewState()
		{
			return ((IStateManager)this.ViewState).SaveViewState();
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x000BDBEA File Offset: 0x000BBDEA
		void IStateManager.TrackViewState()
		{
			this._isTracking = true;
			this.ViewState.TrackViewState();
		}

		// Token: 0x040022FF RID: 8959
		private StateBag _viewState;

		// Token: 0x04002300 RID: 8960
		private bool _isTracking;
	}
}
