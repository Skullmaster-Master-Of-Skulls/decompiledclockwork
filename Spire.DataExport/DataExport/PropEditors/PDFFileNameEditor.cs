using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x02000215 RID: 533
	public class PDFFileNameEditor : FileNameEditor
	{
		// Token: 0x06000FF9 RID: 4089 RVA: 0x000AC980 File Offset: 0x000AB980
		protected override void InitializeDialog(OpenFileDialog openFileDialog)
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.InitializeDialog(openFileDialog);
			openFileDialog.CheckFileExists = false;
			openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("吣䈥丧", a_);
			openFileDialog.Filter = HyperlinksCollectionEditor.b("琣戥渧਩栫䄭匯䜱夳匵嘷丹᰻堽⤿⹁⅃㕅桇扉晋恍⁏㙑㉓罕⑗灙牛⹝џѡ", a_);
		}
	}
}
