using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.ButtonBase;
using Telerik.Web.UI.ButtonNS.JavaScriptSerialization;
using Telerik.Web.UI.ButtonRendering;

namespace Telerik.Web.UI
{
	// Token: 0x020000A9 RID: 169
	[SupportsEventValidation]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadButton))]
	[DefaultProperty("Text")]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadCheckBox), "Telerik.Web.UI.Button.png")]
	[Designer("Telerik.Web.Design.RadCheckBoxDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadCheckBox runat=\"server\" Text=\"RadCheckBox\"></{0}:RadCheckBox>")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[DefaultEvent("Click")]
	[RequiredScript(typeof(CheckableButton))]
	[ClientScriptResource("Telerik.Web.UI.RadCheckBox", "Telerik.Web.UI.Button.RadButtonScripts.js")]
	[EmbeddedSkin("Button")]
	[EmbeddedSkin("Button", "Default")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadButton))]
	public class RadCheckBox : CheckableButton, IJavaScriptConverterProvider
	{
		// Token: 0x06000689 RID: 1673 RVA: 0x0001A9D9 File Offset: 0x00018BD9
		protected override IRenderer CreateControlRenderer()
		{
			return RendererFactory.GetRenderer(this);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001A9E1 File Offset: 0x00018BE1
		protected override void Render(HtmlTextWriter writer)
		{
			this.RegisterForEventValidation();
			base.Render(writer);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001A9F0 File Offset: 0x00018BF0
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptSerializer javaScriptSerializer = JavaScriptSerializeProvider.CreateSerializer(this);
			descriptor.AddScriptProperty("confirmSettings", javaScriptSerializer.Serialize(this.ConfirmSettings));
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001AA24 File Offset: 0x00018C24
		public virtual IEnumerable<JavaScriptConverter> GetJsConverters()
		{
			return new JavaScriptConverter[]
			{
				new RadButtonConfirmSettingsConverter()
			};
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001AA44 File Offset: 0x00018C44
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.ConfirmSettings).LoadViewState(array[1]);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001AA70 File Offset: 0x00018C70
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ConfirmSettings).SaveViewState()
			};
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001AA9E File Offset: 0x00018C9E
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ConfirmSettings).TrackViewState();
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001AAB1 File Offset: 0x00018CB1
		public override string ButtonName
		{
			get
			{
				return "RadCheckBox";
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001AAB8 File Offset: 0x00018CB8
		[Description("Gets the object that controls the built-in confirmation dialog properties.")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
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

		// Token: 0x06000692 RID: 1682 RVA: 0x0001AAD3 File Offset: 0x00018CD3
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001AADC File Offset: 0x00018CDC
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04000164 RID: 356
		private RadButtonConfirmSettings _confirmSettings;
	}
}
