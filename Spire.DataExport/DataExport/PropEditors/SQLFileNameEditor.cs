using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x0200021D RID: 541
	public class SQLFileNameEditor : FileNameEditor
	{
		// Token: 0x06001010 RID: 4112 RVA: 0x000ADD14 File Offset: 0x000ACD14
		protected override void InitializeDialog(OpenFileDialog openFileDialog)
		{
			int a_ = 4;
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
			openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("匟匡䠣", a_);
			openFileDialog.Filter = HyperlinksCollectionEditor.b("猟猡栣إ丧䌩䀫䬭䌯ሱᰳᰵᘷ䤹䴻刽椿㹁湃桅㭇㭉⁋", a_);
		}
	}
}
