using System;
using System.IO;
using System.Windows.Forms;
using AutoComboBox.InputDialogControls;
using DevComponents.DotNetBar.Controls;
using SpellCheckerEx;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000063 RID: 99
	public class MyDevComponentsTextBox : TextBoxX
	{
		// Token: 0x06000380 RID: 896 RVA: 0x0001C6FC File Offset: 0x0001B6FC
		public void EnableSpellCheck2()
		{
			if (this.sharpSpell == null)
			{
				try
				{
					string dictionaryPath = this.GetDictionaryPath();
					this.sharpSpell = new SpellCheckEx(this, dictionaryPath, ClientCache.CurrentInstance.DefaultDictionaryFile);
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001C754 File Offset: 0x0001B754
		private string GetDictionaryPath()
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TechnoPro\\ClockWork\\Dictionaries");
			string result;
			if (Directory.Exists(text))
			{
				result = text;
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001C790 File Offset: 0x0001B790
		protected override void Dispose(bool disposing)
		{
			if (this.sharpSpell != null)
			{
				this.sharpSpell.Dispose();
				this.sharpSpell = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001C7C8 File Offset: 0x0001B7C8
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (e.Control && e.KeyCode == Keys.A)
			{
				base.SelectAll();
			}
			else if (e.KeyCode == Keys.F7 && this.sharpSpell != null)
			{
				this.ShowSpellChecker();
			}
			else if (e.KeyCode == Keys.F10)
			{
				this.Zoom();
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001C842 File Offset: 0x0001B842
		public void ShowSpellChecker()
		{
			this.sharpSpell.SpellCheck();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001C854 File Offset: 0x0001B854
		public void Zoom()
		{
			NotesZoom notesZoom = new NotesZoom(base.ReadOnly || !base.Enabled);
			notesZoom.TextEntered = this.Text;
			DialogResult dialogResult = notesZoom.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				this.Text = notesZoom.TextEntered;
			}
		}

		// Token: 0x04000367 RID: 871
		private SpellCheckEx sharpSpell = null;
	}
}
