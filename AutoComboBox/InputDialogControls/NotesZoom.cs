using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AutoComboBox.Properties;
using SpellCheckerEx;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;

namespace AutoComboBox.InputDialogControls
{
	// Token: 0x0200007E RID: 126
	public partial class NotesZoom : Form
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0002873A File Offset: 0x0002773A
		public NotesZoom()
		{
			this.InitializeComponent();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0002875C File Offset: 0x0002775C
		public NotesZoom(bool ReadOnly)
		{
			this.InitializeComponent();
			if (ReadOnly)
			{
				this.textBox1.ReadOnly = ReadOnly;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0002879C File Offset: 0x0002779C
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x000287CD File Offset: 0x000277CD
		public string TextEntered
		{
			get
			{
				return this.textBox1.Text.Replace('\n'.ToString(), Environment.NewLine);
			}
			set
			{
				this.textBox1.Text = value;
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000287E0 File Offset: 0x000277E0
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Add && e.Control)
			{
				this.IncreaseDecreaseFontSize(0.3f);
			}
			else if (e.KeyCode == Keys.Subtract && e.Control)
			{
				this.IncreaseDecreaseFontSize(-0.3f);
			}
			else if (e.KeyCode == Keys.F7)
			{
				this.SpellCheck();
			}
			else
			{
				base.OnKeyUp(e);
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00028868 File Offset: 0x00027868
		private void IncreaseDecreaseFontSize(float increment)
		{
			float num = this.textBox1.ZoomFactor + increment;
			if (num <= 0f)
			{
				num = 0.1f;
			}
			if (num > 1000f)
			{
				num = 1000f;
			}
			this.textBox1.ZoomFactor = num;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000288B8 File Offset: 0x000278B8
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000288C2 File Offset: 0x000278C2
		private void btn_save_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000288D4 File Offset: 0x000278D4
		private void btn_spellCheck_Click(object sender, EventArgs e)
		{
			this.SpellCheck();
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000288DE File Offset: 0x000278DE
		private void SpellCheck()
		{
			this.sharpSpell.SpellCheck();
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000288ED File Offset: 0x000278ED
		private void btn_increaseFontSize_Click(object sender, EventArgs e)
		{
			this.IncreaseDecreaseFontSize(0.3f);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000288FC File Offset: 0x000278FC
		private void btn_decreaseFontSize_Click(object sender, EventArgs e)
		{
			this.IncreaseDecreaseFontSize(-0.3f);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0002890C File Offset: 0x0002790C
		private void NotesZoom_Load(object sender, EventArgs e)
		{
			try
			{
				this.sharpSpell = new SpellCheckEx(this.textBox1, this.GetDictionaryPath(), ClientCache.CurrentInstance.DefaultDictionaryFile);
				this.sharpSpell.UnderlineMisSpelledEnabled = true;
			}
			catch
			{
			}
			base.ActiveControl = this.textBox1;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00028970 File Offset: 0x00027970
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0002898D File Offset: 0x0002798D
		public float ZoomFactor
		{
			get
			{
				return this.textBox1.ZoomFactor;
			}
			set
			{
				this.textBox1.ZoomFactor = value;
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000289A0 File Offset: 0x000279A0
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

		// Token: 0x060004FD RID: 1277 RVA: 0x000289DA File Offset: 0x000279DA
		private void btn_suspend_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x04000427 RID: 1063
		private SpellCheckEx sharpSpell = null;
	}
}
