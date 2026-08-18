using System;
using System.Design;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x02000195 RID: 405
	internal partial class BinaryUI : Form
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x00054810 File Offset: 0x00052A10
		public BinaryUI(BinaryEditor editor)
		{
			this.editor = editor;
			this.InitializeComponent();
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x00054825 File Offset: 0x00052A25
		// (set) Token: 0x06000EA8 RID: 3752 RVA: 0x00054830 File Offset: 0x00052A30
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
				byte[] array = null;
				if (value != null)
				{
					array = this.editor.ConvertToBytes(value);
				}
				if (array != null)
				{
					this.byteViewer.SetBytes(array);
					this.byteViewer.Enabled = true;
					return;
				}
				this.byteViewer.SetBytes(new byte[0]);
				this.byteViewer.Enabled = false;
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0005488F File Offset: 0x00052A8F
		private void RadioAuto_checkedChanged(object source, EventArgs e)
		{
			if (this.radioAuto.Checked)
			{
				this.byteViewer.SetDisplayMode(DisplayMode.Auto);
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x000548AA File Offset: 0x00052AAA
		private void RadioHex_checkedChanged(object source, EventArgs e)
		{
			if (this.radioHex.Checked)
			{
				this.byteViewer.SetDisplayMode(DisplayMode.Hexdump);
			}
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000548C5 File Offset: 0x00052AC5
		private void RadioAnsi_checkedChanged(object source, EventArgs e)
		{
			if (this.radioAnsi.Checked)
			{
				this.byteViewer.SetDisplayMode(DisplayMode.Ansi);
			}
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000548E0 File Offset: 0x00052AE0
		private void RadioUnicode_checkedChanged(object source, EventArgs e)
		{
			if (this.radioUnicode.Checked)
			{
				this.byteViewer.SetDisplayMode(DisplayMode.Unicode);
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x000548FC File Offset: 0x00052AFC
		private void ButtonOK_click(object source, EventArgs e)
		{
			object obj = this.value;
			this.editor.ConvertToValue(this.byteViewer.GetBytes(), ref obj);
			this.value = obj;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00054930 File Offset: 0x00052B30
		private void ButtonSave_click(object source, EventArgs e)
		{
			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.FileName = SR.GetString("BinaryEditorFileName");
				saveFileDialog.Title = SR.GetString("BinaryEditorSaveFile");
				saveFileDialog.Filter = SR.GetString("BinaryEditorAllFiles") + " (*.*)|*.*";
				DialogResult dialogResult = saveFileDialog.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					this.byteViewer.SaveToFile(saveFileDialog.FileName);
				}
			}
			catch (IOException ex)
			{
				RTLAwareMessageBox.Show(null, SR.GetString("BinaryEditorFileError", new object[]
				{
					ex.Message
				}), SR.GetString("BinaryEditorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x000549E0 File Offset: 0x00052BE0
		private void Form_HelpRequested(object sender, HelpEventArgs e)
		{
			this.editor.ShowHelp();
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000549ED File Offset: 0x00052BED
		private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			this.editor.ShowHelp();
		}

		// Token: 0x040008A5 RID: 2213
		private BinaryEditor editor;

		// Token: 0x040008A6 RID: 2214
		private object value;
	}
}
