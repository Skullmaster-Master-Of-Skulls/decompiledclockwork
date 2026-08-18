using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020019D5 RID: 6613
	[ParseChildren(ChildrenAsProperties = true, DefaultProperty = "Text")]
	[ToolboxItem(false)]
	[XmlRoot("Item")]
	public class RadTickerItem : WebControl, INamingContainer, IMarkableStateManager, IStateManager
	{
		// Token: 0x06010013 RID: 65555 RVA: 0x003971DB File Offset: 0x003953DB
		public RadTickerItem()
		{
		}

		// Token: 0x06010014 RID: 65556 RVA: 0x003971E3 File Offset: 0x003953E3
		public RadTickerItem(string text)
		{
			this.Text = text;
		}

		// Token: 0x06010015 RID: 65557 RVA: 0x003971F2 File Offset: 0x003953F2
		public RadTickerItem(string text, string navigateUrl) : this(text)
		{
			this.NavigateUrl = navigateUrl;
		}

		// Token: 0x17004D4C RID: 19788
		// (get) Token: 0x06010016 RID: 65558 RVA: 0x00397202 File Offset: 0x00395402
		// (set) Token: 0x06010017 RID: 65559 RVA: 0x00397222 File Offset: 0x00395422
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[MergableProperty(false)]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return ((string)this.ViewState["Text"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17004D4D RID: 19789
		// (get) Token: 0x06010018 RID: 65560 RVA: 0x00397235 File Offset: 0x00395435
		// (set) Token: 0x06010019 RID: 65561 RVA: 0x00397255 File Offset: 0x00395455
		[MergableProperty(false)]
		[DefaultValue("")]
		public string NavigateUrl
		{
			get
			{
				return ((string)this.ViewState["NavigateUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x17004D4E RID: 19790
		// (get) Token: 0x0601001A RID: 65562 RVA: 0x00397268 File Offset: 0x00395468
		// (set) Token: 0x0601001B RID: 65563 RVA: 0x00397288 File Offset: 0x00395488
		[DefaultValue("")]
		[MergableProperty(false)]
		public string Target
		{
			get
			{
				return ((string)this.ViewState["Target"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x17004D4F RID: 19791
		// (get) Token: 0x0601001C RID: 65564 RVA: 0x0039729B File Offset: 0x0039549B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Index
		{
			get
			{
				return this._container.Items.IndexOf(this);
			}
		}

		// Token: 0x0601001D RID: 65565 RVA: 0x003972AE File Offset: 0x003954AE
		protected override void RenderContents(HtmlTextWriter writer)
		{
			writer.Write(this.Text);
		}

		// Token: 0x17004D50 RID: 19792
		// (get) Token: 0x0601001E RID: 65566 RVA: 0x003972BC File Offset: 0x003954BC
		// (set) Token: 0x0601001F RID: 65567 RVA: 0x003972C4 File Offset: 0x003954C4
		private protected RadTicker Container
		{
			protected get
			{
				return this._container;
			}
			private set
			{
				this._container = value;
			}
		}

		// Token: 0x06010020 RID: 65568 RVA: 0x003972CD File Offset: 0x003954CD
		protected internal void SetItemContainer(RadTicker itemContainer)
		{
			itemContainer.InitializeItem(this);
			this.Container = itemContainer;
		}

		// Token: 0x06010021 RID: 65569 RVA: 0x003972DD File Offset: 0x003954DD
		void IMarkableStateManager.SetDirty()
		{
			this.ViewState.SetDirty(true);
			base.ControlStyle.SetDirty();
		}

		// Token: 0x17004D51 RID: 19793
		// (get) Token: 0x06010022 RID: 65570 RVA: 0x003972F6 File Offset: 0x003954F6
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return base.IsTrackingViewState;
			}
		}

		// Token: 0x06010023 RID: 65571 RVA: 0x00397300 File Offset: 0x00395500
		void IStateManager.LoadViewState(object state)
		{
			object[] array = (object[])state;
			this.LoadViewState(array[0]);
		}

		// Token: 0x06010024 RID: 65572 RVA: 0x00397320 File Offset: 0x00395520
		object IStateManager.SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState()
			};
		}

		// Token: 0x06010025 RID: 65573 RVA: 0x00397340 File Offset: 0x00395540
		void IStateManager.TrackViewState()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.A);
			webControl.CopyBaseAttributes(this);
			base.TrackViewState();
			base.CopyBaseAttributes(webControl);
		}

		// Token: 0x04004876 RID: 18550
		private RadTicker _container;
	}
}
