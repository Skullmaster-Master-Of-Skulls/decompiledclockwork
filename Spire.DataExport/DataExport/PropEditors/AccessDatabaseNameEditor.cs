using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x02000223 RID: 547
	public class AccessDatabaseNameEditor : FileNameEditor
	{
		// Token: 0x0600101D RID: 4125 RVA: 0x000AE458 File Offset: 0x000AD458
		protected override void InitializeDialog(OpenFileDialog openFileDialog)
		{
			int a_ = 15;
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
			openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("弪䠬圮", a_);
			openFileDialog.Filter = HyperlinksCollectionEditor.b("昪縬༮瀰倲嘴制䨸䠺ᴼ夾⡀⽂⁄㑆楈捊杌慎㱐㝒㝔繖╘煚獜㉞ՠŢ", a_);
		}
	}
}
