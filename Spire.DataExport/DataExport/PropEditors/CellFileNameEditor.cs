using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x02000214 RID: 532
	public class CellFileNameEditor : FileNameEditor
	{
		// Token: 0x06000FF7 RID: 4087 RVA: 0x000AC8F0 File Offset: 0x000AB8F0
		protected override void InitializeDialog(OpenFileDialog openFileDialog)
		{
			int a_ = 17;
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
			openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("唬䌮䈰", a_);
			openFileDialog.Filter = HyperlinksCollectionEditor.b("怬簮ᄰ瘲䴴吶尸场ᴼ夾⡀⽂⁄㑆楈捊杌慎⥐㽒♔繖╘煚獜❞ൠၢ", a_);
		}
	}
}
