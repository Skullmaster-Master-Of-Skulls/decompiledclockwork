using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x02000213 RID: 531
	public class ClipViewerEditor : FileNameEditor
	{
		// Token: 0x06000FF5 RID: 4085 RVA: 0x000AC860 File Offset: 0x000AB860
		protected override void InitializeDialog(OpenFileDialog openFileDialog)
		{
			int a_ = 2;
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
			openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("笝堟䜡", a_);
			openFileDialog.Filter = HyperlinksCollectionEditor.b("嬝堟䜡䜣匥尧䬩丫䈭唯ሱ刳張吷弹伻ḽ栿桁橃⍅ぇ⽉敋㉍穏籑ㅓ⹕㵗♙ᵛ㉝౟䉡ɣཥѧཀྵὫ乭塯塱婳屵具ٹ噻偽ꩿ", a_);
		}
	}
}
