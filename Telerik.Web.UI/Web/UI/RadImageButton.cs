using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.ButtonBase;
using Telerik.Web.UI.ButtonJavaScriptSerialization;
using Telerik.Web.UI.ButtonNS.JavaScriptSerialization;
using Telerik.Web.UI.ButtonRendering;

namespace Telerik.Web.UI
{
	// Token: 0x020000D6 RID: 214
	[EmbeddedSkin("Button", "Default")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ClientScriptResource("Telerik.Web.UI.RadImageButton", "Telerik.Web.UI.Button.RadButtonScripts.js")]
	[RequiredScript(typeof(PostBackButtonBase))]
	[DefaultEvent("Click")]
	[RequiredScript(typeof(jQueryPlugins))]
	[EmbeddedSkin("Button")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadButton))]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxData("<{0}:RadImageButton runat=\"server\" Text=\"RadImageButton\"></{0}:RadImageButton>")]
	[SupportsEventValidation]
	[DefaultProperty("Text")]
	[ToolboxBitmap(typeof(RadImageButton), "Telerik.Web.UI.Button.png")]
	[Designer("Telerik.Web.Design.RadImageButtonDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadImageButton : PostBackButtonBase, IButtonControl, IPostBackEventHandler, INamingContainer, IJavaScriptConverterProvider
	{
		// Token: 0x06000816 RID: 2070 RVA: 0x0001E475 File Offset: 0x0001C675
		public new void RaisePostBackEvent(string eventArgument)
		{
			base.ValidatePage(eventArgument);
			this.OnClick(new ImageButtonClickEventArgs(this.X, this.Y));
			base.OnCommand(new ButtonCommandEventArgs(base.CommandName, base.CommandArgument, false));
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptSerializer javaScriptSerializer = JavaScriptSerializeProvider.CreateSerializer(this);
			descriptor.AddScriptProperty("imageData", javaScriptSerializer.Serialize(this.Image));
			descriptor.AddProperty("_hasImage", this.HasImage);
			descriptor.AddScriptProperty("confirmSettings", javaScriptSerializer.Serialize(this.ConfirmSettings));
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001E518 File Offset: 0x0001C718
		public virtual IEnumerable<JavaScriptConverter> GetJsConverters()
		{
			return new JavaScriptConverter[]
			{
				new ButtonImageConverter
				{
					ResolveUrl = ((string url) => base.ResolveUrl(url))
				},
				new RadButtonConfirmSettingsConverter()
			};
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001E554 File Offset: 0x0001C754
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool result = base.LoadPostData(postDataKey, postCollection);
			string name = this.ClientID + ".x";
			string name2 = this.ClientID + ".y";
			if (postCollection[name] != null && postCollection[name] != "NaN")
			{
				double a = 0.0;
				if (double.TryParse(postCollection[name].ToString(), out a))
				{
					this.X = (int)Math.Round(a);
				}
			}
			if (postCollection[name2] != null && postCollection[name2] != "NaN")
			{
				double a2 = 0.0;
				if (double.TryParse(postCollection[name2].ToString(), out a2))
				{
					this.Y = (int)Math.Round(a2);
				}
			}
			return result;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001E61F File Offset: 0x0001C81F
		private void ClearTemplate()
		{
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001E621 File Offset: 0x0001C821
		private void ApplyTemplate()
		{
			if (this._contentTemplate != null)
			{
				this._contentTemplate.InstantiateIn(this);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0001E637 File Offset: 0x0001C837
		internal bool IsTemplateInitialized
		{
			get
			{
				this.EnsureChildControls();
				return this.ContentTemplate != null || this.Controls.Count > 0;
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001E658 File Offset: 0x0001C858
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Image).LoadViewState(array[1]);
			((IStateManager)this.ConfirmSettings).LoadViewState(array[2]);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001E694 File Offset: 0x0001C894
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Image).SaveViewState(),
				((IStateManager)this.ConfirmSettings).SaveViewState()
			};
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001E6D0 File Offset: 0x0001C8D0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Image).TrackViewState();
			((IStateManager)this.ConfirmSettings).TrackViewState();
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001E6EE File Offset: 0x0001C8EE
		protected override IRenderer CreateControlRenderer()
		{
			return RendererFactory.GetRenderer(this);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001E6F6 File Offset: 0x0001C8F6
		protected override void Render(HtmlTextWriter writer)
		{
			this.RegisterForEventValidation();
			base.Render(writer);
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001E705 File Offset: 0x0001C905
		internal bool HasImage
		{
			get
			{
				return !string.IsNullOrEmpty(this.Image.Url);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0001E71A File Offset: 0x0001C91A
		public override string ButtonName
		{
			get
			{
				return "RadImageButton";
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0001E721 File Offset: 0x0001C921
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x0001E729 File Offset: 0x0001C929
		[Description("Gets or sets the template for the Button control.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RadImageButton))]
		public ITemplate ContentTemplate
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				this._contentTemplate = value;
				this.ClearTemplate();
				this.ApplyTemplate();
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0001E740 File Offset: 0x0001C940
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[Description("Gets the object that controls the Primary and Secondary Icon related properties.")]
		public ButtonImage Image
		{
			get
			{
				ButtonImage result;
				if ((result = this._image) == null)
				{
					result = (this._image = new ButtonImage());
				}
				return result;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0001E765 File Offset: 0x0001C965
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MergableProperty(false)]
		[Description("Gets the object that controls the built-in confirmation dialog properties.")]
		public RadButtonConfirmSettings ConfirmSettings
		{
			get
			{
				if (this._confirmSettings == null)
				{
					this._confirmSettings = new RadButtonConfirmSettings();
				}
				return this._confirmSettings;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0001E780 File Offset: 0x0001C980
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x0001E788 File Offset: 0x0001C988
		internal int X { get; set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0001E791 File Offset: 0x0001C991
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x0001E799 File Offset: 0x0001C999
		internal int Y { get; set; }

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600082C RID: 2092 RVA: 0x0001E7A2 File Offset: 0x0001C9A2
		// (remove) Token: 0x0600082D RID: 2093 RVA: 0x0001E7B5 File Offset: 0x0001C9B5
		public new event ImageButtonClickEventHandler Click
		{
			add
			{
				base.Events.AddHandler(RadImageButton.eventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadImageButton.eventClick, value);
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001E7C8 File Offset: 0x0001C9C8
		protected void OnClick(ImageButtonClickEventArgs e)
		{
			ImageButtonClickEventHandler imageButtonClickEventHandler = (ImageButtonClickEventHandler)base.Events[RadImageButton.eventClick];
			if (imageButtonClickEventHandler != null)
			{
				imageButtonClickEventHandler(this, e);
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001E7F6 File Offset: 0x0001C9F6
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001E7FF File Offset: 0x0001C9FF
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040001EE RID: 494
		private ITemplate _contentTemplate;

		// Token: 0x040001EF RID: 495
		private ButtonImage _image;

		// Token: 0x040001F0 RID: 496
		private RadButtonConfirmSettings _confirmSettings;

		// Token: 0x040001F1 RID: 497
		private static readonly object eventClick = new object();
	}
}
