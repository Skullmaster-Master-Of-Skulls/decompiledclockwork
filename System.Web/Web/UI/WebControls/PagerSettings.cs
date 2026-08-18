using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200060B RID: 1547
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PagerSettings : IStateManager
	{
		// Token: 0x140000F1 RID: 241
		// (add) Token: 0x06004C61 RID: 19553 RVA: 0x00136332 File Offset: 0x00135332
		// (remove) Token: 0x06004C62 RID: 19554 RVA: 0x0013634B File Offset: 0x0013534B
		[Browsable(false)]
		public event EventHandler PropertyChanged;

		// Token: 0x06004C63 RID: 19555 RVA: 0x00136364 File Offset: 0x00135364
		public PagerSettings()
		{
			this._viewState = new StateBag();
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06004C64 RID: 19556 RVA: 0x00136378 File Offset: 0x00135378
		// (set) Token: 0x06004C65 RID: 19557 RVA: 0x001363A8 File Offset: 0x001353A8
		[WebSysDescription("PagerSettings_FirstPageImageUrl")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("Appearance")]
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

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06004C66 RID: 19558 RVA: 0x001363DC File Offset: 0x001353DC
		// (set) Token: 0x06004C67 RID: 19559 RVA: 0x0013640C File Offset: 0x0013540C
		[WebSysDescription("PagerSettings_FirstPageText")]
		[NotifyParentProperty(true)]
		[WebCategory("Appearance")]
		[DefaultValue("&lt;&lt;")]
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

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06004C68 RID: 19560 RVA: 0x00136440 File Offset: 0x00135440
		internal bool IsPagerOnBottom
		{
			get
			{
				PagerPosition position = this.Position;
				return position == PagerPosition.Bottom || position == PagerPosition.TopAndBottom;
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06004C69 RID: 19561 RVA: 0x00136460 File Offset: 0x00135460
		internal bool IsPagerOnTop
		{
			get
			{
				PagerPosition position = this.Position;
				return position == PagerPosition.Top || position == PagerPosition.TopAndBottom;
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06004C6A RID: 19562 RVA: 0x00136480 File Offset: 0x00135480
		// (set) Token: 0x06004C6B RID: 19563 RVA: 0x001364B0 File Offset: 0x001354B0
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06004C6C RID: 19564 RVA: 0x001364E4 File Offset: 0x001354E4
		// (set) Token: 0x06004C6D RID: 19565 RVA: 0x00136514 File Offset: 0x00135514
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerSettings_LastPageText")]
		[DefaultValue("&gt;&gt;")]
		[WebCategory("Appearance")]
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

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06004C6E RID: 19566 RVA: 0x00136548 File Offset: 0x00135548
		// (set) Token: 0x06004C6F RID: 19567 RVA: 0x00136574 File Offset: 0x00135574
		[WebCategory("Appearance")]
		[WebSysDescription("PagerSettings_Mode")]
		[DefaultValue(PagerButtons.Numeric)]
		[NotifyParentProperty(true)]
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

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06004C70 RID: 19568 RVA: 0x001365BC File Offset: 0x001355BC
		// (set) Token: 0x06004C71 RID: 19569 RVA: 0x001365EC File Offset: 0x001355EC
		[WebSysDescription("PagerSettings_NextPageImageUrl")]
		[UrlProperty]
		[WebCategory("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06004C72 RID: 19570 RVA: 0x00136620 File Offset: 0x00135620
		// (set) Token: 0x06004C73 RID: 19571 RVA: 0x00136650 File Offset: 0x00135650
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerSettings_NextPageText")]
		[DefaultValue("&gt;")]
		[WebCategory("Appearance")]
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

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06004C74 RID: 19572 RVA: 0x00136684 File Offset: 0x00135684
		// (set) Token: 0x06004C75 RID: 19573 RVA: 0x001366B0 File Offset: 0x001356B0
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerSettings_PageButtonCount")]
		[DefaultValue(10)]
		[WebCategory("Behavior")]
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

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06004C76 RID: 19574 RVA: 0x001366F4 File Offset: 0x001356F4
		// (set) Token: 0x06004C77 RID: 19575 RVA: 0x0013671D File Offset: 0x0013571D
		[WebCategory("Layout")]
		[WebSysDescription("PagerStyle_Position")]
		[DefaultValue(PagerPosition.Bottom)]
		[NotifyParentProperty(true)]
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

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06004C78 RID: 19576 RVA: 0x00136748 File Offset: 0x00135748
		// (set) Token: 0x06004C79 RID: 19577 RVA: 0x00136778 File Offset: 0x00135778
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerSettings_PreviousPageImageUrl")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		[UrlProperty]
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

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06004C7A RID: 19578 RVA: 0x001367AC File Offset: 0x001357AC
		// (set) Token: 0x06004C7B RID: 19579 RVA: 0x001367DC File Offset: 0x001357DC
		[WebSysDescription("PagerSettings_PreviousPageText")]
		[DefaultValue("&lt;")]
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06004C7C RID: 19580 RVA: 0x00136810 File Offset: 0x00135810
		// (set) Token: 0x06004C7D RID: 19581 RVA: 0x00136839 File Offset: 0x00135839
		[NotifyParentProperty(true)]
		[WebSysDescription("PagerStyle_Visible")]
		[DefaultValue(true)]
		[WebCategory("Appearance")]
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

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06004C7E RID: 19582 RVA: 0x00136851 File Offset: 0x00135851
		private StateBag ViewState
		{
			get
			{
				return this._viewState;
			}
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x00136859 File Offset: 0x00135859
		private void OnPropertyChanged()
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x00136874 File Offset: 0x00135874
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06004C81 RID: 19585 RVA: 0x0013687B File Offset: 0x0013587B
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTracking;
			}
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x00136883 File Offset: 0x00135883
		void IStateManager.LoadViewState(object state)
		{
			if (state != null)
			{
				((IStateManager)this.ViewState).LoadViewState(state);
			}
		}

		// Token: 0x06004C83 RID: 19587 RVA: 0x00136894 File Offset: 0x00135894
		object IStateManager.SaveViewState()
		{
			return ((IStateManager)this.ViewState).SaveViewState();
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x001368AE File Offset: 0x001358AE
		void IStateManager.TrackViewState()
		{
			this._isTracking = true;
			this.ViewState.TrackViewState();
		}

		// Token: 0x04002C05 RID: 11269
		private StateBag _viewState;

		// Token: 0x04002C06 RID: 11270
		private bool _isTracking;
	}
}
