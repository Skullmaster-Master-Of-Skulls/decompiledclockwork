using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000EAE RID: 3758
	[ToolboxItem(false)]
	public class ImageEditorToolSeparator : ImageEditorToolBase
	{
		// Token: 0x17002D49 RID: 11593
		// (get) Token: 0x06008F3B RID: 36667 RVA: 0x00203BB4 File Offset: 0x00201DB4
		// (set) Token: 0x06008F3C RID: 36668 RVA: 0x00203BB7 File Offset: 0x00201DB7
		[DefaultValue(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[NotifyParentProperty(true)]
		public override bool IsSeparator
		{
			get
			{
				return true;
			}
			set
			{
			}
		}
	}
}
