using System;
using System.ComponentModel;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000FE RID: 254
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.ModeButton", "HtmlEditor.ToolbarButtons.ModeButton")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(Enums))]
	public abstract class ModeButton : ImageButton
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x00013644 File Offset: 0x00011844
		public ModeButton()
		{
			base.ActiveModes.Add(ActiveModeType.Design);
			base.ActiveModes.Add(ActiveModeType.Html);
			base.ActiveModes.Add(ActiveModeType.Preview);
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00013670 File Offset: 0x00011870
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x00013691 File Offset: 0x00011891
		[ClientPropertyName("activeMode")]
		[Category("Behavior")]
		[ExtenderControlProperty]
		[DefaultValue(ActiveModeType.Design)]
		public ActiveModeType ActiveMode
		{
			get
			{
				return (ActiveModeType)(this.ViewState["ActiveMode"] ?? ActiveModeType.Design);
			}
			set
			{
				this.ViewState["ActiveMode"] = value;
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000136A9 File Offset: 0x000118A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeActiveMode()
		{
			return base.IsRenderingScript;
		}
	}
}
