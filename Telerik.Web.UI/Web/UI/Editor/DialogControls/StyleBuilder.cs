using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x020019E7 RID: 6631
	[ClientScriptResource("Telerik.Web.UI.Widgets.StyleBuilder", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	public class StyleBuilder : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x17004D78 RID: 19832
		// (get) Token: 0x060100A1 RID: 65697 RVA: 0x003996D1 File Offset: 0x003978D1
		public override string DialogName
		{
			get
			{
				return "StyleBuilder";
			}
		}

		// Token: 0x060100A2 RID: 65698 RVA: 0x00399850 File Offset: 0x00397A50
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.ApplyToControl<ImageDialogCaller>("backgroundImage", delegate(ImageDialogCaller c)
			{
				c.Text = base.ToolsLocalization.ImageManager;
			});
			this.ApplyToControl<ImageDialogCaller>("listBulletImage", delegate(ImageDialogCaller c)
			{
				c.Text = base.ToolsLocalization.ImageManager;
			});
			this.ApplyToControl<RadNumericTextBox>("fontSizeValue", delegate(RadNumericTextBox c)
			{
				c.Label = string.Format("{0}:", this.Localization.GetString("StyleBuilder_Absolute"));
			});
			this.ApplyToControl<RadComboBox>("fontSizeRelative", delegate(RadComboBox c)
			{
				c.Label = string.Format("{0}:", this.Localization.GetString("StyleBuilder_Relative"));
			});
			this.ApplyToControl<RadTabStrip>("styleBuilderTabs", delegate(RadTabStrip c)
			{
				RadTabCollection tabs = c.Tabs;
				tabs.FindTabByValue("Font").Text = this.Localization.GetString("StyleBuilder_FontTab");
				tabs.FindTabByValue("Background").Text = this.Localization.GetString("StyleBuilder_BackgroundTab");
				tabs.FindTabByValue("Text").Text = this.Localization.GetString("StyleBuilder_TextTab");
				tabs.FindTabByValue("Layout").Text = this.Localization.GetString("StyleBuilder_LayoutTab");
				tabs.FindTabByValue("Box").Text = this.Localization.GetString("StyleBuilder_BoxTab");
				tabs.FindTabByValue("Border").Text = this.Localization.GetString("StyleBuilder_BorderTab");
				tabs.FindTabByValue("Lists").Text = this.Localization.GetString("StyleBuilder_ListsTab");
			});
			this.ApplyToControl<RadButton>("transparent", delegate(RadButton c)
			{
				c.Text = this.Localization.GetString("Common_Transparent");
			});
		}

		// Token: 0x060100A3 RID: 65699 RVA: 0x003998F0 File Offset: 0x00397AF0
		private void ApplyToControl<T>(string id, Action<T> action) where T : Control
		{
			T t = (T)((object)base.FindControlRecursive(id));
			if (t != null)
			{
				action(t);
			}
		}
	}
}
