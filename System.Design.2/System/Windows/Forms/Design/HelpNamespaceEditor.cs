using System;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002EB RID: 747
	internal class HelpNamespaceEditor : FileNameEditor
	{
		// Token: 0x06001E05 RID: 7685 RVA: 0x000B65BF File Offset: 0x000B47BF
		protected override void InitializeDialog(OpenFileDialog openFileDialog)
		{
			openFileDialog.Filter = SR.GetString("HelpProviderEditorFilter");
			openFileDialog.Title = SR.GetString("HelpProviderEditorTitle");
		}
	}
}
