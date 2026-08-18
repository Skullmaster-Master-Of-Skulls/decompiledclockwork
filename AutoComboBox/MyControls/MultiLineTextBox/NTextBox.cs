using System;
using System.IO;
using System.Windows.Forms;
using SpellCheckerEx;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;

namespace AutoComboBox.MyControls.MultiLineTextBox
{
	// Token: 0x02000039 RID: 57
	public class NTextBox : TextBox
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00011DF8 File Offset: 0x00010DF8
		public NTextBox(MyMultiLinePopupEdit popup)
		{
			this.popup = popup;
			this.Multiline = true;
			base.ScrollBars = ScrollBars.Vertical;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00011E36 File Offset: 0x00010E36
		public NTextBox()
		{
			this.popup = null;
			this.Multiline = true;
			base.ScrollBars = ScrollBars.Vertical;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00011E74 File Offset: 0x00010E74
		protected override void Dispose(bool disposing)
		{
			if (this.sharpSpell != null)
			{
				this.sharpSpell.Dispose();
				this.sharpSpell = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00011EAC File Offset: 0x00010EAC
		public void EnableSpellCheck()
		{
			if (this.sharpSpell == null)
			{
				try
				{
					string dictionaryPath = this.GetDictionaryPath();
					this.sharpSpell = new SpellCheckEx(this, dictionaryPath, ClientCache.CurrentInstance.DefaultDictionaryFile);
					this.sharpSpell.UnderlineMisSpelledEnabled = true;
				}
				catch
				{
				}
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00011F14 File Offset: 0x00010F14
		public void SpellCheck()
		{
			if (this.sharpSpell != null)
			{
				this.sharpSpell.SpellCheck();
			}
			else
			{
				MessageBox.Show("Spell check is not initialized.");
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00011F4C File Offset: 0x00010F4C
		private string GetDictionaryPath()
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TechnoPro\\ClockWork\\SharpSpell.1033.xml");
			string result;
			if (File.Exists(text))
			{
				result = text;
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00011F88 File Offset: 0x00010F88
		public void ShowSpellChecker()
		{
			if (this.sharpSpell != null)
			{
				this.sharpSpell.SpellCheck();
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00011FB4 File Offset: 0x00010FB4
		protected override void OnKeyUp(KeyEventArgs e)
		{
			this.EnableSpellCheck();
			if (this.brementer)
			{
				this.Text = "";
				this.brementer = false;
			}
			base.OnKeyUp(e);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00011FF3 File Offset: 0x00010FF3
		private void HideMe()
		{
			this.UpdateItem(this.index, this.Text);
			this.popup.HideMe();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00012018 File Offset: 0x00011018
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (e.KeyChar == '\t')
			{
				if (this.Text.Trim() == "")
				{
					this.errshown = true;
					this.brementer = true;
					this.HideMe();
				}
				else
				{
					this.errshown = false;
					this.HideMe();
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00012086 File Offset: 0x00011086
		private void UpdateItem(int index, string text)
		{
			this.popup.editingItem.Text = this.Text;
			this.popup.mllb.FixItem(this.popup.editingItem);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000120BC File Offset: 0x000110BC
		protected override void OnLostFocus(EventArgs e)
		{
			if (this.Text.Trim() == "")
			{
				if (!this.errshown)
				{
					this.HideMe();
				}
				this.errshown = false;
			}
			else
			{
				this.errshown = false;
				this.HideMe();
			}
			base.OnLostFocus(e);
		}

		// Token: 0x040001CF RID: 463
		public MyMultiLinePopupEdit popup;

		// Token: 0x040001D0 RID: 464
		public int index = -1;

		// Token: 0x040001D1 RID: 465
		private bool errshown = false;

		// Token: 0x040001D2 RID: 466
		private bool brementer = false;

		// Token: 0x040001D3 RID: 467
		private SpellCheckEx sharpSpell = null;
	}
}
