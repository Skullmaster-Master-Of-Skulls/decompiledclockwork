using System;
using System.Web.UI;
using AjaxControlToolkit.HtmlEditor.Popups;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000F1 RID: 241
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.DesignModePopupImageButton", "HtmlEditor.ToolbarButtons.DesignModePopupImageButton")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public abstract class DesignModePopupImageButton : MethodButton
	{
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x000132D7 File Offset: 0x000114D7
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x000132E0 File Offset: 0x000114E0
		protected Popup RelatedPopup
		{
			get
			{
				return this._popup;
			}
			set
			{
				this._popup = value;
				if (base.IsDesign)
				{
					return;
				}
				Popup existingPopup = Popup.GetExistingPopup(this.Parent, this.RelatedPopup.GetType());
				if (existingPopup == null)
				{
					base.ExportedControls.Add(this._popup);
					return;
				}
				this._popup = existingPopup;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00013330 File Offset: 0x00011530
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x00013338 File Offset: 0x00011538
		protected bool AutoClose
		{
			get
			{
				return this._autoClose;
			}
			set
			{
				this._autoClose = value;
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00013344 File Offset: 0x00011544
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (this.RelatedPopup != null && !base.IsDesign)
			{
				descriptor.AddComponentProperty("relatedPopup", this.RelatedPopup.ClientID);
			}
			descriptor.AddProperty("autoClose", this.AutoClose);
		}

		// Token: 0x04000305 RID: 773
		private Popup _popup;

		// Token: 0x04000306 RID: 774
		private bool _autoClose = true;
	}
}
