using System;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02000292 RID: 658
	[ClientScriptResource("Telerik.Web.UI.EditorColorList", "Telerik.Web.UI.Common.Core.js")]
	public class EditorColorList : EditorToolsBase
	{
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x0004ED42 File Offset: 0x0004CF42
		public override string Name
		{
			get
			{
				return "ColorList";
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0004ED49 File Offset: 0x0004CF49
		protected override string CssClassFormatString
		{
			get
			{
				return "reColorPicker reDropDownBody reColorList_{0}";
			}
		}
	}
}
