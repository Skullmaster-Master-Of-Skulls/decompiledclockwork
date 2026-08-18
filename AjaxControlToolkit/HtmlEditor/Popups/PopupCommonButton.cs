using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.UI;

namespace AjaxControlToolkit.HtmlEditor.Popups
{
	// Token: 0x020000E6 RID: 230
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.Popups.PopupCommonButton", "HtmlEditor.Popups.PopupCommonButton")]
	public abstract class PopupCommonButton : ScriptControlBase
	{
		// Token: 0x0600068B RID: 1675 RVA: 0x0001269C File Offset: 0x0001089C
		protected PopupCommonButton(HtmlTextWriterTag tag) : base(false, tag)
		{
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000126B1 File Offset: 0x000108B1
		protected PopupCommonButton() : base(false, HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x000126C8 File Offset: 0x000108C8
		protected bool IsDesign
		{
			get
			{
				bool result;
				try
				{
					bool flag = this.Context == null || (base.Site != null && base.Site.DesignMode);
					result = flag;
				}
				catch
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00012718 File Offset: 0x00010918
		internal Collection<Control> ExportedControls
		{
			get
			{
				if (this._exportedControls == null)
				{
					this._exportedControls = new Collection<Control>();
				}
				return this._exportedControls;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00012733 File Offset: 0x00010933
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0001273B File Offset: 0x0001093B
		[ClientPropertyName("name")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		[Category("Behavior")]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x040002F9 RID: 761
		private Collection<Control> _exportedControls;

		// Token: 0x040002FA RID: 762
		private string _name = string.Empty;
	}
}
