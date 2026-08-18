using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x02000220 RID: 544
	public class RTFFileNameEditor : FileNameEditor
	{
		// Token: 0x06001016 RID: 4118 RVA: 0x000ADF04 File Offset: 0x000ACF04
		protected override void InitializeDialog(OpenFileDialog openFileDialog)
		{
			int a_ = 9;
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
			openFileDialog.DefaultExt = HyperlinksCollectionEditor.b("䄤䠦䨨", a_);
			openFileDialog.Filter = HyperlinksCollectionEditor.b("栤琦न簪䈬崮唰ጲ匴帶唸帺丼Ἶ楀楂歄⍆♈⡊摌㍎筐絒ㅔ㡖㩘❚ཛྷ୞❠䍢ͤ๦ը๪Ṭ佮奰奲孴ն൸ᵺ呼;ꮀ궂", a_);
		}
	}
}
