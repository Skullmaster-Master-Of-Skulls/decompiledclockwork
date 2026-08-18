using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020008E7 RID: 2279
	[TelerikToolboxCategory("Navigation")]
	[PersistChildren(false)]
	[Designer("Telerik.Web.Design.RadMultiPageDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadMultiPage), "Telerik.Web.UI.MultiPage.png")]
	[ToolboxData("<{0}:RadMultiPage Runat=\"server\"><{0}:RadPageView runat=\"server\" id=\"RadPageView1\">RadPageView</{0}:RadPageView></{0}:RadMultiPage>")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[DefaultEvent("PageViewCreated")]
	[DefaultProperty("PageViews")]
	[EmbeddedSkin("MultiPage", typeof(RadMultiPage))]
	[RequiredScript(typeof(jQueryPlugins))]
	[ClientScriptResource("Telerik.Web.UI.RadMultiPage", "Telerik.Web.UI.TabStrip.MultiPage.RadMultiPageScripts.js")]
	[ParseChildren(typeof(RadPageView))]
	public class RadMultiPage : RadWebControl
	{
		// Token: 0x17001C7C RID: 7292
		// (get) Token: 0x0600561F RID: 22047 RVA: 0x0010793B File Offset: 0x00105B3B
		// (set) Token: 0x06005620 RID: 22048 RVA: 0x0010795C File Offset: 0x00105B5C
		[Description("The index of the currently selected PageView.")]
		[Category("Behavior")]
		[DefaultValue(-1)]
		public int SelectedIndex
		{
			get
			{
				return (int)(this.ViewState["SelectedIndex"] ?? -1);
			}
			set
			{
				this.ViewState["SelectedIndex"] = value;
				this.ApplyRenderSelectedPage();
			}
		}

		// Token: 0x17001C7D RID: 7293
		// (get) Token: 0x06005621 RID: 22049 RVA: 0x0010797A File Offset: 0x00105B7A
		[Browsable(false)]
		public RadPageView SelectedPageView
		{
			get
			{
				if (this.SelectedIndex < 0 || this.SelectedIndex >= this.PageViews.Count)
				{
					return null;
				}
				return this.PageViews[this.SelectedIndex];
			}
		}

		// Token: 0x17001C7E RID: 7294
		// (get) Token: 0x06005622 RID: 22050 RVA: 0x001079AB File Offset: 0x00105BAB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public RadPageViewCollection PageViews
		{
			get
			{
				return (RadPageViewCollection)this.Controls;
			}
		}

		// Token: 0x17001C7F RID: 7295
		// (get) Token: 0x06005623 RID: 22051 RVA: 0x001079B8 File Offset: 0x00105BB8
		// (set) Token: 0x06005624 RID: 22052 RVA: 0x001079DC File Offset: 0x00105BDC
		[DefaultValue(false)]
		[Description("Specified whether only the selected RadMultiPage should be rendered.")]
		[Category("Behavior")]
		public bool RenderSelectedPageOnly
		{
			get
			{
				return (bool)(this.ViewState["RenderSelectedPageOnly"] ?? false);
			}
			set
			{
				this.ViewState["RenderSelectedPageOnly"] = value;
				this.ApplyRenderSelectedPage();
				if (!value)
				{
					foreach (object obj in this.PageViews)
					{
						RadPageView radPageView = (RadPageView)obj;
						radPageView.Visible = true;
					}
				}
			}
		}

		// Token: 0x17001C80 RID: 7296
		// (get) Token: 0x06005625 RID: 22053 RVA: 0x00107A54 File Offset: 0x00105C54
		// (set) Token: 0x06005626 RID: 22054 RVA: 0x00107A75 File Offset: 0x00105C75
		[Description("The visibility and position of scroll bars")]
		[Category("Layout")]
		[DefaultValue(MultiPageScrollBars.None)]
		public MultiPageScrollBars ScrollBars
		{
			get
			{
				return (MultiPageScrollBars)(this.ViewState["ScrollBars"] ?? MultiPageScrollBars.None);
			}
			set
			{
				this.ViewState["ScrollBars"] = value;
			}
		}

		// Token: 0x17001C81 RID: 7297
		// (get) Token: 0x06005627 RID: 22055 RVA: 0x00107A8D File Offset: 0x00105C8D
		// (set) Token: 0x06005628 RID: 22056 RVA: 0x00107A90 File Offset: 0x00105C90
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17001C82 RID: 7298
		// (get) Token: 0x06005629 RID: 22057 RVA: 0x00107A92 File Offset: 0x00105C92
		// (set) Token: 0x0600562A RID: 22058 RVA: 0x00107A9A File Offset: 0x00105C9A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				base.Skin = value;
			}
		}

		// Token: 0x17001C83 RID: 7299
		// (get) Token: 0x0600562B RID: 22059 RVA: 0x00107AA3 File Offset: 0x00105CA3
		// (set) Token: 0x0600562C RID: 22060 RVA: 0x00107AC4 File Offset: 0x00105CC4
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("When set to true enables support for WAI-ARIA")]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x140000CD RID: 205
		// (add) Token: 0x0600562D RID: 22061 RVA: 0x00107ADC File Offset: 0x00105CDC
		// (remove) Token: 0x0600562E RID: 22062 RVA: 0x00107AEF File Offset: 0x00105CEF
		public event RadMultiPageEventHandler PageViewCreated
		{
			add
			{
				base.Events.AddHandler(RadMultiPage.PageViewCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMultiPage.PageViewCreatedEvent, value);
			}
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x00107B04 File Offset: 0x00105D04
		public RadPageView FindPageViewByID(string id)
		{
			foreach (object obj in this.PageViews)
			{
				RadPageView radPageView = (RadPageView)obj;
				if (radPageView.ID == id)
				{
					return radPageView;
				}
			}
			return null;
		}

		// Token: 0x17001C84 RID: 7300
		// (get) Token: 0x06005630 RID: 22064 RVA: 0x00107B6C File Offset: 0x00105D6C
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x00107B70 File Offset: 0x00105D70
		protected override ControlCollection CreateControlCollection()
		{
			return new RadPageViewCollection(this);
		}

		// Token: 0x06005632 RID: 22066 RVA: 0x00107B78 File Offset: 0x00105D78
		protected virtual RadPageView CreatePageView()
		{
			return new RadPageView();
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x00107B80 File Offset: 0x00105D80
		protected override void AddedControl(Control control, int index)
		{
			base.AddedControl(control, index);
			RadPageView radPageView = control as RadPageView;
			if (radPageView == null)
			{
				return;
			}
			if (radPageView.cachedSelected)
			{
				radPageView.Selected = true;
			}
			this.ApplyRenderSelectedPage();
			this.OnPageViewCreated(new RadMultiPageEventArgs(radPageView));
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x00107BC4 File Offset: 0x00105DC4
		protected override void AddParsedSubObject(object obj)
		{
			RadPageView radPageView = obj as RadPageView;
			if (radPageView != null)
			{
				this.PageViews.Add(radPageView);
			}
		}

		// Token: 0x17001C85 RID: 7301
		// (get) Token: 0x06005635 RID: 22069 RVA: 0x00107BE7 File Offset: 0x00105DE7
		protected override string CssClassFormatString
		{
			get
			{
				return "RadMultiPage RadMultiPage_{0}";
			}
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x00107BF0 File Offset: 0x00105DF0
		private void ApplyRenderSelectedPage()
		{
			if (!this.RenderSelectedPageOnly)
			{
				return;
			}
			foreach (object obj in this.PageViews)
			{
				RadPageView radPageView = (RadPageView)obj;
				radPageView.Visible = radPageView.Selected;
			}
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x00107C58 File Offset: 0x00105E58
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (this.SelectedIndex > -1 && this.SelectedIndex < this.PageViews.Count)
			{
				descriptor.AddProperty("selectedIndex", this.SelectedIndex);
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new PageViewJavaScriptConverter()
			});
			descriptor.AddScriptProperty("pageViewData", javaScriptSerializer.Serialize(this.PageViews));
			if (this.EnableAriaSupport)
			{
				descriptor.AddProperty("enableAriaSupport", this.EnableAriaSupport);
			}
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x00107CF0 File Offset: 0x00105EF0
		protected override object SaveViewState()
		{
			List<string> list = new List<string>();
			foreach (object obj in this.PageViews)
			{
				RadPageView radPageView = (RadPageView)obj;
				list.Add(radPageView.ID);
			}
			return new object[]
			{
				base.SaveViewState(),
				list.ToArray()
			};
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x00107D74 File Offset: 0x00105F74
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			string[] array2 = (string[])array[1];
			for (int i = this.PageViews.Count; i < array2.Length; i++)
			{
				RadPageView radPageView = this.CreatePageView();
				radPageView.ID = array2[i];
				this.PageViews.Add(radPageView);
			}
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x00107DD0 File Offset: 0x00105FD0
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (!string.IsNullOrEmpty(text))
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				try
				{
					this.LoadClientState(javaScriptSerializer.Deserialize<MultiPageClientState>(text));
				}
				catch (InvalidOperationException)
				{
				}
				catch (ArgumentException)
				{
				}
			}
			return false;
		}

		// Token: 0x0600563B RID: 22075 RVA: 0x00107E2C File Offset: 0x0010602C
		private void LoadClientState(MultiPageClientState clientState)
		{
			this.SelectedIndex = clientState.SelectedIndex;
			if (clientState.ChangeLog == null)
			{
				return;
			}
			foreach (ClientStateLogEntry clientStateLogEntry in clientState.ChangeLog)
			{
				switch (clientStateLogEntry.Type)
				{
				case ClientStateLogEntryType.Insert:
				{
					RadPageView radPageView = this.CreatePageView();
					if (clientStateLogEntry.Data != null && clientStateLogEntry.Data.ContainsKey("id"))
					{
						radPageView.ID = clientStateLogEntry.Data["id"].ToString();
					}
					this.PageViews.AddAt(Convert.ToInt32(clientStateLogEntry.Index), radPageView);
					break;
				}
				case ClientStateLogEntryType.Remove:
					this.PageViews.RemoveAt(Convert.ToInt32(clientStateLogEntry.Index));
					break;
				}
			}
		}

		// Token: 0x0600563C RID: 22076 RVA: 0x00107F14 File Offset: 0x00106114
		protected virtual void OnPageViewCreated(RadMultiPageEventArgs eventArgs)
		{
			RadMultiPageEventHandler radMultiPageEventHandler = base.Events[RadMultiPage.PageViewCreatedEvent] as RadMultiPageEventHandler;
			if (radMultiPageEventHandler != null)
			{
				radMultiPageEventHandler(this, eventArgs);
			}
		}

		// Token: 0x0600563E RID: 22078 RVA: 0x00107F42 File Offset: 0x00106142
		// Note: this type is marked as 'beforefieldinit'.
		static RadMultiPage()
		{
			RadMultiPage.PageViewCreatedEvent = new object();
		}
	}
}
