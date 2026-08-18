using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001052 RID: 4178
	[ToolboxItem(false)]
	[ClientScriptResource("Telerik.Web.UI.Widgets.TableLayouts", "Telerik.Web.UI.Common.Core.js")]
	[RequiredScript(typeof(LayoutBuilderEngine))]
	public class TableLayouts : UserControlBase
	{
		// Token: 0x17003635 RID: 13877
		// (get) Token: 0x0600A8ED RID: 43245 RVA: 0x0024B544 File Offset: 0x00249744
		public override string DialogName
		{
			get
			{
				return "TableLayouts";
			}
		}
	}
}
