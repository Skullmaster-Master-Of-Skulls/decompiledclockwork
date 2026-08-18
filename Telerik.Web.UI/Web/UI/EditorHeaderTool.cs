using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200028E RID: 654
	public class EditorHeaderTool : EditorTool
	{
		// Token: 0x06001762 RID: 5986 RVA: 0x0004E921 File Offset: 0x0004CB21
		public EditorHeaderTool()
		{
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0004E929 File Offset: 0x0004CB29
		public EditorHeaderTool(string name) : base(name)
		{
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0004E932 File Offset: 0x0004CB32
		public EditorHeaderTool(string name, EditorHeaderToolPosition position) : base(name)
		{
			this.Position = position;
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0004E942 File Offset: 0x0004CB42
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x0004E96D File Offset: 0x0004CB6D
		[NotifyParentProperty(true)]
		[DefaultValue(EditorHeaderToolPosition.Left)]
		public EditorHeaderToolPosition Position
		{
			get
			{
				if (base.ViewState["Position"] == null)
				{
					return EditorHeaderToolPosition.Left;
				}
				return (EditorHeaderToolPosition)base.ViewState["Position"];
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}
	}
}
