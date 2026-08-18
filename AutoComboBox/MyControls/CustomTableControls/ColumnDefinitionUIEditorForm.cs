using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x0200005B RID: 91
	public partial class ColumnDefinitionUIEditorForm : Form
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00019F64 File Offset: 0x00018F64
		public ColumnDefinitionUIEditorForm(ColumnDefinition target, Dictionary<string, string> existedNames, bool showApply)
		{
			this.content = new ColumnDefinitionUIEditor(target, existedNames, this, showApply);
			base.ClientSize = this.content.Size;
			this.content.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			base.Resize += this.this_Resize;
			this.content.Resize += this.content_Resize;
			base.Controls.Add(this.content);
			base.Load += this.OnLoad;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00019FF9 File Offset: 0x00018FF9
		private void OnLoad(object sender, EventArgs e)
		{
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00019FFC File Offset: 0x00018FFC
		private void this_Resize(object sender, EventArgs e)
		{
			this.content.Size = base.ClientSize;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0001A011 File Offset: 0x00019011
		private void content_Resize(object sender, EventArgs e)
		{
			base.ClientSize = this.content.Size;
		}

		// Token: 0x04000326 RID: 806
		private Control content;
	}
}
