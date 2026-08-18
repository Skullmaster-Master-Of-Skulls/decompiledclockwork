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
	// Token: 0x0200001F RID: 31
	[DefaultProperty("Checked")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[DefaultEvent("Click")]
	[SupportsEventValidation]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadSwitch), "Telerik.Web.UI.Button.png")]
	[Designer("Telerik.Web.Design.RadSwitchDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadSwitch runat=\"server\" ></{0}:RadSwitch>")]
	[EmbeddedSkin("Button")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(CheckableButton))]
	[ClientScriptResource("Telerik.Web.UI.RadSwitch", "Telerik.Web.UI.Button.RadButtonScripts.js")]
	[RequiredScript(typeof(jQueryPlugins))]
	[EmbeddedSkin("Button", "Default")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadButton))]
	public class RadSwitch : CheckableButton, IJavaScriptConverterProvider
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000509F File Offset: 0x0000329F
		protected override IRenderer CreateControlRenderer()
		{
			return RendererFactory.GetRenderer(this);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000050A7 File Offset: 0x000032A7
		protected override void Render(HtmlTextWriter writer)
		{
			this.RegisterForEventValidation();
			base.Render(writer);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000050B8 File Offset: 0x000032B8
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptSerializer javaScriptSerializer = JavaScriptSerializeProvider.CreateSerializer(this);
			descriptor.AddScriptProperty("confirmSettings", javaScriptSerializer.Serialize(this.ConfirmSettings));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000050EC File Offset: 0x000032EC
		public virtual IEnumerable<JavaScriptConverter> GetJsConverters()
		{
			return new JavaScriptConverter[]
			{
				new RadButtonConfirmSettingsConverter()
			};
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000510C File Offset: 0x0000330C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.ConfirmSettings).LoadViewState(array[1]);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005138 File Offset: 0x00003338
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ConfirmSettings).SaveViewState()
			};
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005166 File Offset: 0x00003366
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ConfirmSettings).TrackViewState();
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00005179 File Offset: 0x00003379
		public override string ButtonName
		{
			get
			{
				return "RadSwitch";
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00005180 File Offset: 0x00003380
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00005188 File Offset: 0x00003388
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new string Text
		{
			get
			{
				return base.Text;
			}
			internal set
			{
				base.Text = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00005191 File Offset: 0x00003391
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00005199 File Offset: 0x00003399
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new string SingleClickText
		{
			get
			{
				return base.SingleClickText;
			}
			internal set
			{
				base.SingleClickText = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000051A2 File Offset: 0x000033A2
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		[Description("Gets the object that controls the built-in confirmation dialog properties.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000051BD File Offset: 0x000033BD
		[Description("Gets the object that controls the settings of the toggle states.")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SwitchToggleStatesSettings ToggleStates
		{
			get
			{
				if (this._toggleStatesSettings == null)
				{
					this._toggleStatesSettings = new SwitchToggleStatesSettings();
				}
				return this._toggleStatesSettings;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000051D8 File Offset: 0x000033D8
		[DefaultValue(null)]
		[Description("Gets the On or Off toggle state based on the Checked property.")]
		public SwitchToggleState CurrentToggleState
		{
			get
			{
				if (base.Checked == true)
				{
					return this.ToggleStates.ToggleStateOn;
				}
				return this.ToggleStates.ToggleStateOff;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005218 File Offset: 0x00003418
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005221 File Offset: 0x00003421
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0400001E RID: 30
		private RadButtonConfirmSettings _confirmSettings;

		// Token: 0x0400001F RID: 31
		private SwitchToggleStatesSettings _toggleStatesSettings;
	}
}
