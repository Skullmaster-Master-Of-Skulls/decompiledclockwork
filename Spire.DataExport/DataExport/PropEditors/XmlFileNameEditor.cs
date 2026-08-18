using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x02000211 RID: 529
	public class XmlFileNameEditor : FileNameEditor
	{
		// Token: 0x06000FF0 RID: 4080 RVA: 0x000ABBB8 File Offset: 0x000AABB8
		protected override void InitializeDialog(OpenFileDialog openFileDialog)
		{
			int a_ = 12;
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
			openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("倧䜩䀫", a_);
			openFileDialog.Filter = HyperlinksCollectionEditor.b("瀧朩怫อ嘯嬱堳匵䬷ᨹᐻᐽ渿㩁⥃⩅慇㙉晋恍⡏㽑㡓", a_);
		}
	}
}
