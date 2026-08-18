using System;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit.HtmlEditor.Popups;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000F4 RID: 244
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.ColorSelector", "HtmlEditor.ToolbarButtons.ColorSelector")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public abstract class ColorSelector : Selector
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x000133EF File Offset: 0x000115EF
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x000133F7 File Offset: 0x000115F7
		[DefaultValue("")]
		public string FixedColorButtonId
		{
			get
			{
				return this._fixedColorButtonId;
			}
			set
			{
				this._fixedColorButtonId = value;
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00013400 File Offset: 0x00011600
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.RelatedPopup = new BaseColorsPopup();
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00013414 File Offset: 0x00011614
		protected override void OnPreRender(EventArgs e)
		{
			if (this.FixedColorButtonId.Length > 0 && !base.IsDesign)
			{
				FixedColorButton fixedColorButton = this.Parent.FindControl(this.FixedColorButtonId) as FixedColorButton;
				if (fixedColorButton != null)
				{
					this.ToolTip = fixedColorButton.ToolTip;
				}
			}
			base.OnPreRender(e);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00013464 File Offset: 0x00011664
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (this.FixedColorButtonId.Length <= 0 || base.IsDesign)
			{
				return;
			}
			FixedColorButton fixedColorButton = this.Parent.FindControl(this.FixedColorButtonId) as FixedColorButton;
			if (fixedColorButton != null)
			{
				descriptor.AddComponentProperty("fixedColorButton", fixedColorButton.ClientID);
				return;
			}
			throw new ArgumentException("FixedColorButton control's ID expected");
		}

		// Token: 0x04000307 RID: 775
		private string _fixedColorButtonId = string.Empty;
	}
}
