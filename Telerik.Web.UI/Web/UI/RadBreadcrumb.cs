using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.Breadcrumb;

namespace Telerik.Web.UI
{
	// Token: 0x0200000B RID: 11
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Mobile, typeof(RadBreadcrumb))]
	[ClientScriptResource("Telerik.Web.UI.RadBreadcrumb", "Telerik.Web.UI.Breadcrumb.Scripts.RadBreadcrumb.js")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Lightweight, typeof(RadBreadcrumb))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadBreadcrumb))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadBreadcrumb))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadBreadcrumb))]
	[EmbeddedSkin("Breadcrumb", typeof(RadBreadcrumb))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Classic, typeof(RadBreadcrumb))]
	[ParseChildren(ChildrenAsProperties = true)]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(Html5Breadcrumb))]
	[EmbeddedSkin("Breadcrumb", "Default", typeof(RadBreadcrumb))]
	public class RadBreadcrumb : RadWebControl
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x000031E4 File Offset: 0x000013E4
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "initialize", this.ClientEvents.OnInitialize);
			RadWebControl.DescribeEvent(descriptor, "load", this.ClientEvents.OnLoad);
			RadWebControl.DescribeEvent(descriptor, "click", this.ClientEvents.OnClick);
			RadWebControl.DescribeEvent(descriptor, "change", this.ClientEvents.OnChange);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003250 File Offset: 0x00001450
		public RadBreadcrumb()
		{
			this.RegisterJSConverters();
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003269 File Offset: 0x00001469
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000328A File Offset: 0x0000148A
		[DefaultValue(false)]
		public bool BindToLocation
		{
			get
			{
				return (bool)(this.ViewState["BindToLocation"] ?? false);
			}
			set
			{
				this.ViewState["BindToLocation"] = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000032A2 File Offset: 0x000014A2
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x000032C2 File Offset: 0x000014C2
		[DefaultValue("arrow-chevron-right")]
		public string DelimiterIcon
		{
			get
			{
				return (string)(this.ViewState["DelimiterIcon"] ?? "arrow-chevron-right");
			}
			set
			{
				this.ViewState["DelimiterIcon"] = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000032D5 File Offset: 0x000014D5
		// (set) Token: 0x060000DA RID: 218 RVA: 0x000032F6 File Offset: 0x000014F6
		[DefaultValue(false)]
		public bool Editable
		{
			get
			{
				return (bool)(this.ViewState["Editable"] ?? false);
			}
			set
			{
				this.ViewState["Editable"] = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000330E File Offset: 0x0000150E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public BreadcrumbItemsCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new BreadcrumbItemsCollection();
				}
				return this._items;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003329 File Offset: 0x00001529
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00003352 File Offset: 0x00001552
		[DefaultValue(0.0)]
		public double Gap
		{
			get
			{
				return (double)(this.ViewState["Gap"] ?? 0.0);
			}
			set
			{
				this.ViewState["Gap"] = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000336A File Offset: 0x0000156A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Messages MessagesSettings
		{
			get
			{
				if (this._messages == null)
				{
					this._messages = new Messages();
				}
				return this._messages;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003385 File Offset: 0x00001585
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x000033A6 File Offset: 0x000015A6
		[DefaultValue(false)]
		public bool Navigational
		{
			get
			{
				return (bool)(this.ViewState["Navigational"] ?? false);
			}
			set
			{
				this.ViewState["Navigational"] = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000033BE File Offset: 0x000015BE
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000033DE File Offset: 0x000015DE
		[DefaultValue("home")]
		public string RootIcon
		{
			get
			{
				return (string)(this.ViewState["RootIcon"] ?? "home");
			}
			set
			{
				this.ViewState["RootIcon"] = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000033F1 File Offset: 0x000015F1
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00003411 File Offset: 0x00001611
		[DefaultValue("")]
		public string Value
		{
			get
			{
				return (string)(this.ViewState["Value"] ?? "");
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003424 File Offset: 0x00001624
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00003444 File Offset: 0x00001644
		[DefaultValue("")]
		[Browsable(true)]
		[Bindable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public string ClientItemTemplate
		{
			get
			{
				return (string)(this.ViewState["ClientItemTemplate"] ?? "");
			}
			set
			{
				this.ViewState["ClientItemTemplate"] = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003457 File Offset: 0x00001657
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public BreadcrumbClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new BreadcrumbClientEvents();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003472 File Offset: 0x00001672
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddScriptProperty("_options", this.serializer.Serialize(this));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003492 File Offset: 0x00001692
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptObjectBuilder.RegisterCssReferences(this);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000034A4 File Offset: 0x000016A4
		private void RegisterJSConverters()
		{
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new RadBreadcrumbConverter(),
				new BreadcrumbItemConverter(),
				new MessagesConverter()
			};
			this.serializer.RegisterConverters(converters);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000034E6 File Offset: 0x000016E6
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000034EA File Offset: 0x000016EA
		protected override string CssClassFormatString
		{
			get
			{
				return "RadBreadcrumb RadBreadcrumb_{0}";
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000034F4 File Offset: 0x000016F4
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.ClientEvents).LoadViewState(array[num++]);
			((IStateManager)this.Items).LoadViewState(array[num++]);
			((IStateManager)this.MessagesSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003550 File Offset: 0x00001750
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState(),
				((IStateManager)this.Items).SaveViewState(),
				((IStateManager)this.MessagesSettings).SaveViewState()
			};
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000359A File Offset: 0x0000179A
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ClientEvents).TrackViewState();
			((IStateManager)this.Items).TrackViewState();
			((IStateManager)this.MessagesSettings).TrackViewState();
		}

		// Token: 0x0400000B RID: 11
		private BreadcrumbItemsCollection _items;

		// Token: 0x0400000C RID: 12
		private Messages _messages;

		// Token: 0x0400000D RID: 13
		private BreadcrumbClientEvents _clientEvents;

		// Token: 0x0400000E RID: 14
		private readonly AdvancedJavaScriptSerializer serializer = new AdvancedJavaScriptSerializer();
	}
}
