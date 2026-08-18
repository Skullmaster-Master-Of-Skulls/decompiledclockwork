using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001062 RID: 4194
	[ToolboxItem(false)]
	public class About : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x1700364B RID: 13899
		// (get) Token: 0x0600A930 RID: 43312 RVA: 0x0024BF67 File Offset: 0x0024A167
		public override string DialogName
		{
			get
			{
				return "About";
			}
		}
	}
}
