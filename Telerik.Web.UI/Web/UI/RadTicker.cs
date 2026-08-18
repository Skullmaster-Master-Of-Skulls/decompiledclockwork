using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.UI;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020019D3 RID: 6611
	[Designer("Telerik.Web.Design.RadTickerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Visualization")]
	[ToolboxBitmap(typeof(RadTicker), "Telerik.Web.UI.Ticker.png")]
	[ToolboxData("<{0}:RadTicker runat=\"server\"></{0}:RadTicker>")]
	[ClientScriptResource("Telerik.Web.UI.RadTicker", "Telerik.Web.UI.Rotator.RadTicker.js")]
	public class RadTicker : RadDataBoundControl, INamingContainer
	{
		// Token: 0x0600FF76 RID: 65398 RVA: 0x00395578 File Offset: 0x00393778
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "autoAdvance", this.AutoAdvance, true);
			base.DescribeProperty<bool>(descriptor, "autoStart", this.AutoStart, false);
			base.DescribeProperty<int>(descriptor, "lineDuration", this.LineDuration, 2000);
			base.DescribeProperty<bool>(descriptor, "loop", this.Loop, false);
			base.DescribeProperty<int>(descriptor, "tickSpeed", this.TickSpeed, 20);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600FF77 RID: 65399 RVA: 0x003955F0 File Offset: 0x003937F0
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17004D16 RID: 19734
		// (get) Token: 0x0600FF78 RID: 65400 RVA: 0x003955F9 File Offset: 0x003937F9
		[DefaultValue(false)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004D17 RID: 19735
		// (get) Token: 0x0600FF79 RID: 65401 RVA: 0x003955FC File Offset: 0x003937FC
		[DefaultValue(false)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600FF7A RID: 65402 RVA: 0x00395600 File Offset: 0x00393800
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (this.AutoPostBack)
			{
				descriptor.AddScriptProperty("_postBackReference", "\"" + this.GetPostbackEventReference() + "\"");
			}
			descriptor.AddScriptProperty("itemsData", this.Items.Serialize());
		}

		// Token: 0x0600FF7B RID: 65403 RVA: 0x00395654 File Offset: 0x00393854
		internal PostBackOptions GetPostBackOptions(Control control, string argument, string postBackUrl)
		{
			PostBackOptions postBackOptions = new PostBackOptions(control, argument);
			postBackOptions.ClientSubmit = true;
			if (this.Page != null && !string.IsNullOrEmpty(postBackUrl))
			{
				postBackOptions.ActionUrl = postBackUrl;
			}
			return postBackOptions;
		}

		// Token: 0x0600FF7C RID: 65404 RVA: 0x00395688 File Offset: 0x00393888
		internal PostBackOptions GetPostBackOptions(Control control, string argument)
		{
			string postBackUrl = string.Empty;
			if (!string.IsNullOrEmpty(this.PostBackUrl))
			{
				postBackUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
			}
			return this.GetPostBackOptions(control, argument, postBackUrl);
		}

		// Token: 0x0600FF7D RID: 65405 RVA: 0x003956C4 File Offset: 0x003938C4
		protected virtual string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(this, "arguments"));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x0600FF7E RID: 65406 RVA: 0x00395700 File Offset: 0x00393900
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data == null && this.DataSource == null)
			{
				foreach (object obj in this.Items)
				{
					RadTickerItem radTickerItem = (RadTickerItem)obj;
					radTickerItem.DataBind();
				}
				return;
			}
			this.PrepareForDataBinding();
			this.BindToEnumerableData(data);
		}

		// Token: 0x0600FF7F RID: 65407 RVA: 0x00395774 File Offset: 0x00393974
		public void BindToEnumerableData(IEnumerable dataSource)
		{
			foreach (object dataObject in dataSource)
			{
				this.BindItem(this.Items, dataObject);
			}
		}

		// Token: 0x0600FF80 RID: 65408 RVA: 0x003957CC File Offset: 0x003939CC
		private RadTickerItem BindItem(RadTickerItemCollection items, object dataObject)
		{
			RadTickerItem radTickerItem = new RadTickerItem();
			items.Add(radTickerItem);
			this.RaiseItemDataBound(radTickerItem, dataObject);
			return radTickerItem;
		}

		// Token: 0x0600FF81 RID: 65409 RVA: 0x003957F0 File Offset: 0x003939F0
		private void RaiseItemDataBound(RadTickerItem item, object dataItem)
		{
			if (string.IsNullOrEmpty(this.DataTextField))
			{
				try
				{
					item.Text = (string)dataItem;
					goto IL_4E;
				}
				catch (InvalidCastException)
				{
					throw new NotSupportedException(string.Format("Unable to bind the RadTicker to an object of type {0}. Please use the DataTextField ticker property to select which field to bind to.", dataItem.GetType().Name));
				}
			}
			item.Text = (string)DataBinder.Eval(dataItem, this.DataTextField);
			IL_4E:
			if (!string.IsNullOrEmpty(this.DataNavigateUrlField))
			{
				object obj = DataBinder.Eval(dataItem, this.DataNavigateUrlField);
				item.NavigateUrl = ((obj is DBNull) ? null : ((string)obj));
			}
			item.DataBind();
		}

		// Token: 0x0600FF82 RID: 65410 RVA: 0x00395894 File Offset: 0x00393A94
		protected void PrepareForDataBinding()
		{
			if (!this.AppendDataBoundItems)
			{
				this.Items.Clear();
				base.ClearChildViewState();
			}
			this.TrackViewState();
		}

		// Token: 0x0600FF83 RID: 65411 RVA: 0x003958B5 File Offset: 0x00393AB5
		protected internal virtual void InitializeItem(RadTickerItem item)
		{
		}

		// Token: 0x0600FF84 RID: 65412 RVA: 0x003958B8 File Offset: 0x00393AB8
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] == null)
			{
				this.Items.Clear();
				return;
			}
			((IStateManager)this.Items).LoadViewState(array[1]);
		}

		// Token: 0x0600FF85 RID: 65413 RVA: 0x003958F4 File Offset: 0x00393AF4
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Items).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600FF86 RID: 65414 RVA: 0x0039592C File Offset: 0x00393B2C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x0600FF87 RID: 65415 RVA: 0x0039593F File Offset: 0x00393B3F
		protected override void RenderContents(HtmlTextWriter writer)
		{
		}

		// Token: 0x17004D18 RID: 19736
		// (get) Token: 0x0600FF88 RID: 65416 RVA: 0x00395941 File Offset: 0x00393B41
		[MergableProperty(false)]
		[DefaultValue(null)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadTickerItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new RadTickerItemCollection(this);
					this._items.SetItemContainer(this);
				}
				return this._items;
			}
		}

		// Token: 0x17004D19 RID: 19737
		// (get) Token: 0x0600FF89 RID: 65417 RVA: 0x0039596C File Offset: 0x00393B6C
		// (set) Token: 0x0600FF8A RID: 65418 RVA: 0x00395995 File Offset: 0x00393B95
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Description("Specifies whether the ticker begins ticking automatically.")]
		public bool AutoStart
		{
			get
			{
				object obj = this.ViewState["AutoStart"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AutoStart"] = value;
			}
		}

		// Token: 0x17004D1A RID: 19738
		// (get) Token: 0x0600FF8B RID: 65419 RVA: 0x003959B0 File Offset: 0x00393BB0
		// (set) Token: 0x0600FF8C RID: 65420 RVA: 0x003959D9 File Offset: 0x00393BD9
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Specifies whether RadTicker will begin ticking the next tickerline automatically.")]
		[ClientControlProperty]
		public bool AutoAdvance
		{
			get
			{
				object obj = this.ViewState["AutoAdvance"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["AutoAdvance"] = value;
			}
		}

		// Token: 0x17004D1B RID: 19739
		// (get) Token: 0x0600FF8D RID: 65421 RVA: 0x003959F4 File Offset: 0x00393BF4
		// (set) Token: 0x0600FF8E RID: 65422 RVA: 0x00395A1D File Offset: 0x00393C1D
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Specifies whether RadTicker will repeat the first tickerline after displaying the last one.")]
		[ClientControlProperty]
		public bool Loop
		{
			get
			{
				object obj = this.ViewState["Loop"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["Loop"] = value;
			}
		}

		// Token: 0x17004D1C RID: 19740
		// (get) Token: 0x0600FF8F RID: 65423 RVA: 0x00395A38 File Offset: 0x00393C38
		// (set) Token: 0x0600FF90 RID: 65424 RVA: 0x00395A62 File Offset: 0x00393C62
		[DefaultValue(20)]
		[Description("Specifies the duration in milliseconds between ticking each character of a tickerline.")]
		[Category("Behavior")]
		[ClientControlProperty]
		public int TickSpeed
		{
			get
			{
				object obj = this.ViewState["TickSpeed"];
				if (obj == null)
				{
					return 20;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["TickSpeed"] = value;
			}
		}

		// Token: 0x17004D1D RID: 19741
		// (get) Token: 0x0600FF91 RID: 65425 RVA: 0x00395A7C File Offset: 0x00393C7C
		// (set) Token: 0x0600FF92 RID: 65426 RVA: 0x00395AA9 File Offset: 0x00393CA9
		[DefaultValue(2000)]
		[Description("Specifies in milliseconds the pause RadTicker makes before starting to tick the next line (if AutoAdvance=True).")]
		[ClientControlProperty]
		[Category("Behavior")]
		public int LineDuration
		{
			get
			{
				object obj = this.ViewState["LineDuration"];
				if (obj == null)
				{
					return 2000;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["LineDuration"] = value;
			}
		}

		// Token: 0x17004D1E RID: 19742
		// (get) Token: 0x0600FF93 RID: 65427 RVA: 0x00395AC4 File Offset: 0x00393CC4
		// (set) Token: 0x0600FF94 RID: 65428 RVA: 0x00395AED File Offset: 0x00393CED
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether a postback to the server automatically occurs when the user interacts with the control.")]
		public virtual bool AutoPostBack
		{
			get
			{
				object obj = this.ViewState["AutoPostBack"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x17004D1F RID: 19743
		// (get) Token: 0x0600FF95 RID: 65429 RVA: 0x00395B05 File Offset: 0x00393D05
		// (set) Token: 0x0600FF96 RID: 65430 RVA: 0x00395B26 File Offset: 0x00393D26
		[DefaultValue(false)]
		public bool AppendDataBoundItems
		{
			get
			{
				return (bool)(this.ViewState["AppendDataBoundItems"] ?? false);
			}
			set
			{
				this.ViewState["AppendDataBoundItems"] = value;
			}
		}

		// Token: 0x17004D20 RID: 19744
		// (get) Token: 0x0600FF97 RID: 65431 RVA: 0x00395B3E File Offset: 0x00393D3E
		// (set) Token: 0x0600FF98 RID: 65432 RVA: 0x00395B5E File Offset: 0x00393D5E
		[UrlProperty("*.aspx")]
		[Themeable(false)]
		[Description("The URL to post to when an item is clicked.")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string PostBackUrl
		{
			get
			{
				return ((string)this.ViewState["PostBackUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		// Token: 0x17004D21 RID: 19745
		// (get) Token: 0x0600FF99 RID: 65433 RVA: 0x00395B71 File Offset: 0x00393D71
		// (set) Token: 0x0600FF9A RID: 65434 RVA: 0x00395B91 File Offset: 0x00393D91
		[Category("Data")]
		[DefaultValue("")]
		[Description("The field in the data source, which provides the ticker item text.")]
		public string DataTextField
		{
			get
			{
				return ((string)this.ViewState["DataTextField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataTextField"] = value;
			}
		}

		// Token: 0x17004D22 RID: 19746
		// (get) Token: 0x0600FF9B RID: 65435 RVA: 0x00395BA4 File Offset: 0x00393DA4
		// (set) Token: 0x0600FF9C RID: 65436 RVA: 0x00395BC4 File Offset: 0x00393DC4
		[DefaultValue("")]
		[Category("Data")]
		[Description("The field in the data source, which provides the ticker item NavigateUrl.")]
		public string DataNavigateUrlField
		{
			get
			{
				return ((string)this.ViewState["DataNavigateUrlField"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataNavigateUrlField"] = value;
			}
		}

		// Token: 0x04004867 RID: 18535
		private RadTickerItemCollection _items;
	}
}
