using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000F9 RID: 249
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.BulletedList", "HtmlEditor.ToolbarButtons.BulletedList")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class BulletedList : MethodButton
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x000135B8 File Offset: 0x000117B8
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-ListBullet");
			base.OnPreRender(e);
		}
	}
}
