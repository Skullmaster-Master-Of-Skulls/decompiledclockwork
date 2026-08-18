using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.ButtonBase;
using Telerik.Web.UI.ButtonRendering;

namespace Telerik.Web.UI
{
	// Token: 0x020000CE RID: 206
	[DefaultProperty("Text")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadButton))]
	[ToolboxData("<{0}:RadRadioButton runat=\"server\" Text=\"RadRadioButton\"></{0}:RadRadioButton>")]
	[DefaultEvent("Click")]
	[SupportsEventValidation]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadRadioButton), "Telerik.Web.UI.Button.png")]
	[Designer("Telerik.Web.Design.RadRadioButtonDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[RequiredScript(typeof(CheckableButton))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(jQueryPlugins))]
	[EmbeddedSkin("Button")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadButton))]
	[ClientScriptResource("Telerik.Web.UI.RadRadioButton", "Telerik.Web.UI.Button.RadButtonScripts.js")]
	[EmbeddedSkin("Button", "Default")]
	public class RadRadioButton : CheckableButton
	{
		// Token: 0x060007EF RID: 2031 RVA: 0x0001E0C7 File Offset: 0x0001C2C7
		protected override IRenderer CreateControlRenderer()
		{
			return RendererFactory.GetRenderer(this);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001E0CF File Offset: 0x0001C2CF
		protected override void Render(HtmlTextWriter writer)
		{
			this.RegisterForEventValidation();
			base.Render(writer);
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0001E0DE File Offset: 0x0001C2DE
		public override string ButtonName
		{
			get
			{
				return "RadRadioButton";
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001E0E5 File Offset: 0x0001C2E5
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001E0EE File Offset: 0x0001C2EE
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}
	}
}
