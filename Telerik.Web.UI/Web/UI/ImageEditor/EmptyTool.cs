using System;
using System.ComponentModel;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EB2 RID: 3762
	[ToolboxItem(false)]
	public class EmptyTool : ImageEditorDialog
	{
		// Token: 0x06008F4F RID: 36687 RVA: 0x00204B36 File Offset: 0x00202D36
		public EmptyTool(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17002D50 RID: 11600
		// (get) Token: 0x06008F50 RID: 36688 RVA: 0x00204B40 File Offset: 0x00202D40
		public override string DialogName
		{
			get
			{
				return "EmptyDialog";
			}
		}

		// Token: 0x17002D51 RID: 11601
		// (get) Token: 0x06008F51 RID: 36689 RVA: 0x00204B47 File Offset: 0x00202D47
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002D52 RID: 11602
		// (get) Token: 0x06008F52 RID: 36690 RVA: 0x00204B4E File Offset: 0x00202D4E
		public override string Title
		{
			get
			{
				return "Dialog Control";
			}
		}
	}
}
