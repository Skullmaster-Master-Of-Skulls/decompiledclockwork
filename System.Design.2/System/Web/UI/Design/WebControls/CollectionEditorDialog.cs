using System;
using System.Web.UI.Design.Util;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000A9 RID: 169
	internal abstract partial class CollectionEditorDialog : DesignerForm
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x00018F58 File Offset: 0x00017158
		protected CollectionEditorDialog(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00018F64 File Offset: 0x00017164
		protected ToolStripButton CreatePushButton(string toolTipText, int imageIndex)
		{
			return new ToolStripButton
			{
				Text = toolTipText,
				AutoToolTip = true,
				DisplayStyle = ToolStripItemDisplayStyle.Image,
				ImageIndex = imageIndex,
				ImageScaling = ToolStripItemImageScaling.SizeToFit
			};
		}
	}
}
