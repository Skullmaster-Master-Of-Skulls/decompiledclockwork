using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using AutoComboBox.InputDialogControls;
using AutoComboBox.MyControls;
using SpellCheckerEx;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;

namespace AutoComboBox
{
	// Token: 0x02000031 RID: 49
	public class MyTextBox : TextBox, MyDynamicControl
	{
		// Token: 0x0600015B RID: 347 RVA: 0x0000E97C File Offset: 0x0000D97C
		public new string ToString()
		{
			return this.Text;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000E994 File Offset: 0x0000D994
		public void FromString(string s)
		{
			this.Text = s;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000E9A0 File Offset: 0x0000D9A0
		public object ReportObject
		{
			get
			{
				return this.Text;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000E9B8 File Offset: 0x0000D9B8
		public MyTextBox()
		{
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000EA7C File Offset: 0x0000DA7C
		public object DynamicControl
		{
			get
			{
				return this.dynamicControl;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000EA94 File Offset: 0x0000DA94
		public MyTextBox(object dynamicControl)
		{
			this.dynamicControl = dynamicControl;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000EB5F File Offset: 0x0000DB5F
		public void SetMaskRules(int maskCid, string maskRulesString)
		{
			this.maskRules = MaskRule.MaskRulesFromString(maskRulesString);
			this.MaskCid = maskCid;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000EBA4 File Offset: 0x0000DBA4
		public void UpdateMask(string cmbVal)
		{
			if (this.maskRules != null)
			{
				MaskRule rule = this.maskRules.Find((MaskRule mr) => mr.MaskGroup.Equals(cmbVal, StringComparison.OrdinalIgnoreCase));
				this.ApplyMask(rule);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000EBF8 File Offset: 0x0000DBF8
		private void ApplyMask(MaskRule rule)
		{
			this.DisplayMaskError(false);
			this.currentMaskRule = rule;
			if (this.currentMaskRule != null)
			{
				string text = "";
				for (int i = 0; i < this.Text.Length; i++)
				{
					char c = this.Text[i];
					if (c != ' ')
					{
						if (this.currentMaskRule.SpaceInserts.Contains(i))
						{
							text += ' ';
						}
						text += c;
					}
				}
				this.Text = text;
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000EC9C File Offset: 0x0000DC9C
		private void UpdateMask()
		{
			if (this.currentMaskRule != null)
			{
				int length = this.Text.Length;
				if (this.currentMaskRule.MaskChars.Length != length)
				{
					this.DisplayMaskError(true);
				}
				else
				{
					bool flag = false;
					for (int i = 0; i < this.currentMaskRule.MaskChars.Length; i++)
					{
						char c = this.Text[i];
						char c2 = this.currentMaskRule.MaskChars[i];
						if (c2 == ' ')
						{
							flag = (c == ' ');
						}
						else if (c2 == 'L')
						{
							flag = char.IsLetter(c);
						}
						else
						{
							flag = (c2 != '0' || char.IsDigit(c));
						}
						if (!flag)
						{
							break;
						}
					}
					this.DisplayMaskError(!flag);
				}
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000ED98 File Offset: 0x0000DD98
		private void DisplayMaskError(bool isError)
		{
			if (isError)
			{
				this.ForeColor = Color.White;
				this.BackColor = Color.Red;
			}
			else
			{
				this.BackColor = SystemColors.ControlLightLight;
				this.ForeColor = SystemColors.ControlText;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000EDE4 File Offset: 0x0000DDE4
		// (set) Token: 0x06000167 RID: 359 RVA: 0x0000EDFC File Offset: 0x0000DDFC
		public bool ActAsFolderBrowser
		{
			get
			{
				return this.actAsFolderBrowser;
			}
			set
			{
				this.actAsFolderBrowser = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000EE08 File Offset: 0x0000DE08
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000EE20 File Offset: 0x0000DE20
		public bool ActAsFileBrowser
		{
			get
			{
				return this.actAsFileBrowser;
			}
			set
			{
				this.actAsFileBrowser = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000EE2C File Offset: 0x0000DE2C
		public bool FilledIn
		{
			get
			{
				return this.Text.Trim().Length > 0;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000EE54 File Offset: 0x0000DE54
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000EE6C File Offset: 0x0000DE6C
		public DataState MyDataState
		{
			get
			{
				return this.myDataState;
			}
			set
			{
				this.myDataState = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (set) Token: 0x0600016D RID: 365 RVA: 0x0000EE76 File Offset: 0x0000DE76
		public MyCheckBox SyncedCheckbox
		{
			set
			{
				this.syncedCheckbox = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000EE80 File Offset: 0x0000DE80
		public DataRow[] MultipleCids
		{
			get
			{
				return this.multipleCids;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000EE98 File Offset: 0x0000DE98
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000EEB0 File Offset: 0x0000DEB0
		public bool OnlyAllowAdding
		{
			get
			{
				return this.onlyAllowAdding;
			}
			set
			{
				this.onlyAllowAdding = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (set) Token: 0x06000171 RID: 369 RVA: 0x0000EEBA File Offset: 0x0000DEBA
		public string WhoAmIName
		{
			set
			{
				this.whoAmIName = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000EEC4 File Offset: 0x0000DEC4
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000EEDC File Offset: 0x0000DEDC
		public bool IsCurrency
		{
			get
			{
				return this.isCurrency;
			}
			set
			{
				this.isCurrency = value;
				if (this.isCurrency)
				{
					base.TextAlign = HorizontalAlignment.Right;
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000EF08 File Offset: 0x0000DF08
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000EF20 File Offset: 0x0000DF20
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000EF2C File Offset: 0x0000DF2C
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (this.isCurrency)
			{
				NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
				string numberDecimalSeparator = numberFormat.NumberDecimalSeparator;
				string numberGroupSeparator = numberFormat.NumberGroupSeparator;
				string negativeSign = numberFormat.NegativeSign;
				string text = e.KeyChar.ToString();
				if (!char.IsDigit(e.KeyChar))
				{
					if (!text.Equals(numberDecimalSeparator) && !text.Equals(numberGroupSeparator) && !text.Equals(negativeSign))
					{
						if (e.KeyChar != '\b')
						{
							bool flag = 0 == 0;
							e.Handled = true;
						}
					}
				}
			}
			if (e.KeyChar == '\r' && this.suppressEnter)
			{
				e.Handled = true;
				this.FireEnterPressed(new KeyPressEventArgs('\r'));
			}
			else
			{
				base.OnKeyPress(e);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000F034 File Offset: 0x0000E034
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000F04C File Offset: 0x0000E04C
		public int CalcButtonCid
		{
			get
			{
				return this.calcButtonCid;
			}
			set
			{
				this.calcButtonCid = value;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000F058 File Offset: 0x0000E058
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			if (this.isCurrency)
			{
				this.FixCurrency();
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000F084 File Offset: 0x0000E084
		public void FixCurrency()
		{
			double num;
			if (double.TryParse(this.Text, out num))
			{
				this.Text = num.ToString("N2");
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000F0BC File Offset: 0x0000E0BC
		public void InformCalcButtonOfChange()
		{
			if (this.calcButtonCid > 0)
			{
				Control parent = ListViewEx.GetParent(this);
				Control control = ListViewEx.FindControl(parent, this.calcButtonCid);
				if (control != null && control is MyDynamicControl)
				{
					MyDynamicControl myDynamicControl = (MyDynamicControl)control;
					myDynamicControl.Refresh();
				}
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000F118 File Offset: 0x0000E118
		protected override void Dispose(bool disposing)
		{
			this.syncedCheckbox = null;
			if (this.sharpSpell != null)
			{
				this.sharpSpell.Dispose();
				this.sharpSpell = null;
			}
			if (this.cm != null)
			{
				this.cm.Opening -= this.cm_Opening;
				for (int i = this.baseContextMenuItemCount; i < this.cm.Items.Count; i++)
				{
					ToolStripItem toolStripItem = this.cm.Items[i];
					if (!(toolStripItem is ToolStripSeparator))
					{
						toolStripItem.Click -= this.MyTextBox_Click;
					}
				}
				for (int i = 0; i < this.baseContextMenuItemCount; i++)
				{
					ToolStripItem toolStripItem2 = this.cm.Items[i];
					toolStripItem2.Click -= this.tsi_Click;
				}
				this.cm.Items.Clear();
				this.cm = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000F230 File Offset: 0x0000E230
		public void SetTextFromDatabase(string text, DataRow dr)
		{
			this.Text = text;
			if (this.onlyAllowAdding)
			{
				bool flag = dr == null || (dr.RowState != DataRowState.Unchanged && dr.RowState == DataRowState.Added);
				base.ReadOnly = !flag;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000F29C File Offset: 0x0000E29C
		protected override void OnEnter(EventArgs e)
		{
			if (this.needToEnableSpellCheck)
			{
				this.needToEnableSpellCheck = false;
				this.EnableSpellCheck2();
			}
			base.OnEnter(e);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000F2D0 File Offset: 0x0000E2D0
		private void SetupContextMenu()
		{
			this.cm = new ContextMenuStrip();
			ToolStripItem toolStripItem = this.cm.Items.Add("Undo");
			this.cm.Items.Add(new ToolStripSeparator());
			ToolStripItem toolStripItem2 = this.cm.Items.Add("Cut");
			ToolStripItem toolStripItem3 = this.cm.Items.Add("Copy");
			ToolStripItem toolStripItem4 = this.cm.Items.Add("Paste");
			ToolStripItem toolStripItem5 = this.cm.Items.Add("Delete");
			this.cm.Items.Add(new ToolStripSeparator());
			ToolStripItem toolStripItem6 = this.cm.Items.Add("Select all");
			this.cm.Items.Add(new ToolStripSeparator());
			ToolStripItem toolStripItem7 = this.cm.Items.Add("Spell check (F7)");
			ToolStripItem toolStripItem8 = this.cm.Items.Add("Zoom (F10)");
			ToolStripItem toolStripItem9 = this.cm.Items[this.cm.Items.Add(new ToolStripSeparator())];
			toolStripItem9.Visible = false;
			this.baseContextMenuItemCount = this.cm.Items.Count;
			foreach (object obj in this.cm.Items)
			{
				ToolStripItem toolStripItem10 = (ToolStripItem)obj;
				if (!(toolStripItem10 is ToolStripSeparator))
				{
					toolStripItem10.Click += this.tsi_Click;
				}
			}
			this.cm.Opening += this.cm_Opening;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000F4C8 File Offset: 0x0000E4C8
		private void cm_Opening(object sender, CancelEventArgs e)
		{
			ToolStripItem toolStripItem = this.cm.Items[2];
			ToolStripItem toolStripItem2 = this.cm.Items[3];
			ToolStripItem toolStripItem3 = this.cm.Items[4];
			ToolStripItem toolStripItem4 = this.cm.Items[5];
			ToolStripItem toolStripItem5 = this.cm.Items[7];
			bool enabled = this.Text.Length > 0;
			bool enabled2 = this.SelectionLength > 0;
			toolStripItem.Enabled = enabled2;
			toolStripItem2.Enabled = enabled2;
			toolStripItem4.Enabled = enabled2;
			toolStripItem5.Enabled = enabled;
			toolStripItem3.Enabled = Clipboard.ContainsText();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000F580 File Offset: 0x0000E580
		private void tsi_Click(object sender, EventArgs e)
		{
			ToolStripItem toolStripItem = (ToolStripItem)sender;
			string text = toolStripItem.Text.ToLower();
			string text2 = text;
			switch (text2)
			{
			case "undo":
				if (this.AllowedToEdit)
				{
					base.Undo();
				}
				break;
			case "cut":
				if (this.AllowedToEdit)
				{
					base.Cut();
				}
				break;
			case "copy":
				base.Copy();
				break;
			case "paste":
				if (this.AllowedToEdit)
				{
					base.Paste();
				}
				break;
			case "delete":
				if (this.AllowedToEdit)
				{
					this.ReplaceText(base.SelectionStart, base.SelectionStart + this.SelectionLength, "", false);
				}
				break;
			case "select all":
				base.SelectAll();
				break;
			case "spell check (f7)":
				if (this.AllowedToEdit)
				{
					this.ShowSpellChecker();
				}
				break;
			case "zoom (f10)":
				if (this.AllowedToEdit)
				{
					this.Zoom();
				}
				break;
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000F72E File Offset: 0x0000E72E
		public void ClearAddedText()
		{
			this.addedText = "";
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000F73C File Offset: 0x0000E73C
		public void AddMultipleCid(DataRow dr)
		{
			if (this.multipleCids == null)
			{
				this.multipleCids = new DataRow[1];
				this.multipleCids[0] = dr;
			}
			else
			{
				DataRow[] array = new DataRow[this.multipleCids.Length + 1];
				for (int i = 0; i < this.multipleCids.Length; i++)
				{
					array[i] = this.multipleCids[i];
				}
				array[array.Length - 1] = dr;
				Array.Clear(this.multipleCids, 0, this.multipleCids.Length);
				this.multipleCids = array;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000F7CC File Offset: 0x0000E7CC
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000F7E4 File Offset: 0x0000E7E4
		public bool SuppressEnter
		{
			get
			{
				return this.suppressEnter;
			}
			set
			{
				this.suppressEnter = value;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000186 RID: 390 RVA: 0x0000F7F0 File Offset: 0x0000E7F0
		// (remove) Token: 0x06000187 RID: 391 RVA: 0x0000F82C File Offset: 0x0000E82C
		public event KeyPressEventHandler EnterPressed;

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000F868 File Offset: 0x0000E868
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000F880 File Offset: 0x0000E880
		public string Mask
		{
			get
			{
				return this.mask;
			}
			set
			{
				this.mask = value;
				if (this.mask.Length > 0)
				{
					this.maskCodes = MyTextBox.MaskCode.ParseMaskCodes(this.mask);
				}
				else
				{
					this.maskCodes = null;
				}
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000F8C8 File Offset: 0x0000E8C8
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000F8E0 File Offset: 0x0000E8E0
		public bool MaskEnabled
		{
			get
			{
				return this.maskEnabled;
			}
			set
			{
				this.maskEnabled = value;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000F8EA File Offset: 0x0000E8EA
		protected override void OnTextChanged(EventArgs e)
		{
			this.FireLeave();
			base.OnTextChanged(e);
			this.InformCalcButtonOfChange();
			this.UpdateMask();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000F90C File Offset: 0x0000E90C
		public void FireLeave()
		{
			if (this.syncedCheckbox != null)
			{
				bool flag = this.Text.Trim().Length > 0;
				if (this.syncedCheckbox.Checked != flag)
				{
					this.syncedCheckbox.Checked = flag;
				}
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000F95C File Offset: 0x0000E95C
		public void EnableSpellCheck()
		{
			this.needToEnableSpellCheck = true;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000F968 File Offset: 0x0000E968
		private void EnableSpellCheck2()
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

		// Token: 0x06000190 RID: 400 RVA: 0x0000F9C0 File Offset: 0x0000E9C0
		private void MyTextBox_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000F9C4 File Offset: 0x0000E9C4
		private void ReplaceText(int startInd, int endInd, string s, bool highlightNewText)
		{
			try
			{
				string str;
				string str2;
				if (startInd == 0)
				{
					str = "";
					if (endInd == this.Text.Length)
					{
						str2 = "";
					}
					else
					{
						str2 = this.Text.Substring(endInd + 1);
					}
				}
				else
				{
					str = this.Text.Substring(0, startInd);
					if (endInd == this.Text.Length - 1)
					{
						str2 = "";
					}
					else
					{
						str2 = this.Text.Substring(endInd + 1);
					}
				}
				this.Text = str + s + str2;
				if (highlightNewText)
				{
					base.SelectionStart = startInd;
					this.SelectionLength = s.Length;
				}
				else
				{
					base.SelectionStart = startInd;
					this.SelectionLength = 0;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000FAB0 File Offset: 0x0000EAB0
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

		// Token: 0x06000193 RID: 403 RVA: 0x0000FAEA File Offset: 0x0000EAEA
		public void ShowSpellChecker()
		{
			this.sharpSpell.SpellCheck();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000FAFC File Offset: 0x0000EAFC
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

		// Token: 0x06000195 RID: 405 RVA: 0x0000FB54 File Offset: 0x0000EB54
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode != Keys.Back)
			{
				int length = this.Text.Length;
				if (this.currentMaskRule != null && this.currentMaskRule.SpaceInserts.Contains(length))
				{
					this.Text += " ";
					base.SelectionStart = this.Text.Length;
				}
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000FBD4 File Offset: 0x0000EBD4
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (e.Control && e.KeyCode == Keys.A)
			{
				base.SelectAll();
			}
			else if (e.Control && e.KeyCode == Keys.Q)
			{
				this.EnableSpellCheck();
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

		// Token: 0x06000197 RID: 407 RVA: 0x0000FC74 File Offset: 0x0000EC74
		public bool AmIEnabled()
		{
			Control control = this;
			while (control.Parent != null && !(control is MyPanel))
			{
				control = control.Parent;
			}
			bool result;
			if (control is MyPanel)
			{
				result = ((MyPanel)control).IsEnabled;
			}
			else
			{
				result = control.Enabled;
			}
			return result;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000FCD4 File Offset: 0x0000ECD4
		private bool AllowedToEdit
		{
			get
			{
				return base.Enabled && (!this.onlyAllowAdding || !base.ReadOnly || !this.AmIEnabled());
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000FD1C File Offset: 0x0000ED1C
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			if (!base.Enabled)
			{
				int num = base.SelectionStart + 25;
				if (num < this.Text.Length)
				{
					base.SelectionStart = num;
					this.SelectionLength = 1;
					base.ScrollToCaret();
				}
			}
			else if (this.onlyAllowAdding && base.ReadOnly && this.AmIEnabled())
			{
				DialogResult dialogResult = MessageBox.Show("This text has been locked - would you like to append a note onto the end?", "Append note", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					NotesZoom notesZoom = new NotesZoom();
					notesZoom.Text = this.Text;
					dialogResult = notesZoom.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						string newLine = Environment.NewLine;
						this.AppendText(string.Concat(new string[]
						{
							newLine,
							newLine,
							"NOTE APPENDED by ",
							this.whoAmIName,
							" on ",
							DateTime.Now.ToString("yyyy-MM-dd h:mm tt"),
							newLine
						}));
						this.AppendText(notesZoom.TextEntered);
					}
				}
			}
			else if (this.actAsFolderBrowser)
			{
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				DialogResult dialogResult = folderBrowserDialog.ShowDialog(this);
				if (dialogResult == DialogResult.OK)
				{
					this.Text = folderBrowserDialog.SelectedPath;
				}
			}
			else if (this.actAsFileBrowser)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				DialogResult dialogResult2 = openFileDialog.ShowDialog(this);
				if (dialogResult2 == DialogResult.OK)
				{
					this.Text = openFileDialog.FileName;
				}
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000FEE4 File Offset: 0x0000EEE4
		private new void AppendText(string s)
		{
			this.Text += s;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000FEFC File Offset: 0x0000EEFC
		private void FireEnterPressed(KeyPressEventArgs e)
		{
			if (this.EnterPressed != null)
			{
				this.EnterPressed(this, e);
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000FF28 File Offset: 0x0000EF28
		private void GetPrePost(out string pre, out string post)
		{
			pre = ((base.SelectionStart > 0 && this.Text.Length > 0) ? this.Text.Substring(0, base.SelectionStart) : "");
			int num = base.SelectionStart + this.SelectionLength;
			post = ((num < this.Text.Length && num >= 0) ? this.Text.Substring(num) : "");
		}

		// Token: 0x04000199 RID: 409
		private object dynamicControl = null;

		// Token: 0x0400019A RID: 410
		public int MaskCid = -1;

		// Token: 0x0400019B RID: 411
		private List<MaskRule> maskRules = null;

		// Token: 0x0400019C RID: 412
		private MaskRule currentMaskRule = null;

		// Token: 0x0400019D RID: 413
		private bool actAsFolderBrowser = false;

		// Token: 0x0400019E RID: 414
		private bool actAsFileBrowser = false;

		// Token: 0x0400019F RID: 415
		private string mask = "";

		// Token: 0x040001A0 RID: 416
		private bool maskEnabled = true;

		// Token: 0x040001A1 RID: 417
		private MyTextBox.MaskCode[] maskCodes = null;

		// Token: 0x040001A2 RID: 418
		private bool suppressEnter = true;

		// Token: 0x040001A3 RID: 419
		private string whoAmIName = "";

		// Token: 0x040001A4 RID: 420
		private bool isReadOnly = false;

		// Token: 0x040001A5 RID: 421
		private bool onlyAllowAdding = false;

		// Token: 0x040001A6 RID: 422
		private DataRow[] multipleCids = null;

		// Token: 0x040001A7 RID: 423
		private MyCheckBox syncedCheckbox = null;

		// Token: 0x040001A8 RID: 424
		private DataState myDataState = DataState.unknown;

		// Token: 0x040001A9 RID: 425
		private string addedText = "";

		// Token: 0x040001AA RID: 426
		private bool isCurrency = false;

		// Token: 0x040001AB RID: 427
		private ContextMenuStrip cm = null;

		// Token: 0x040001AC RID: 428
		private int calcButtonCid = 0;

		// Token: 0x040001AD RID: 429
		private int baseContextMenuItemCount = 9;

		// Token: 0x040001AF RID: 431
		private bool needToEnableSpellCheck = false;

		// Token: 0x040001B0 RID: 432
		private SpellCheckEx sharpSpell = null;

		// Token: 0x02000032 RID: 50
		private class MaskCode
		{
			// Token: 0x0600019D RID: 413 RVA: 0x0000FF9D File Offset: 0x0000EF9D
			public MaskCode(MaskCodeType maskCodeType, char maskParameter)
			{
				this.maskCodeType = maskCodeType;
				this.maskParameter = maskParameter;
			}

			// Token: 0x0600019E RID: 414 RVA: 0x0000FFB8 File Offset: 0x0000EFB8
			public bool DoesCharacterFit(char c)
			{
				MaskCodeType maskCodeType = this.maskCodeType;
				if (maskCodeType <= MaskCodeType.required_unicode)
				{
					if (maskCodeType != MaskCodeType.STATIC_CHAR && maskCodeType != MaskCodeType.optional_digit_or_space_or_plus_or_minus_symbol && maskCodeType != MaskCodeType.required_unicode)
					{
						goto IL_77;
					}
				}
				else if (maskCodeType <= MaskCodeType.optional_unicode)
				{
					if (maskCodeType != MaskCodeType.required_digit)
					{
						switch (maskCodeType)
						{
						case MaskCodeType.optional_digit:
						case MaskCodeType.force_to_lower_case:
						case MaskCodeType.force_to_upper_case:
						case MaskCodeType.optional_letter:
						case MaskCodeType.required_alphanumeric:
						case MaskCodeType.optional_unicode:
							break;
						case (MaskCodeType)58:
						case (MaskCodeType)59:
						case (MaskCodeType)61:
						case (MaskCodeType)64:
						case (MaskCodeType)66:
							goto IL_77;
						default:
							goto IL_77;
						}
					}
				}
				else if (maskCodeType != MaskCodeType.required_letter && maskCodeType != MaskCodeType.optional_alphanumeric)
				{
					goto IL_77;
				}
				return c == this.maskParameter;
				IL_77:
				return false;
			}

			// Token: 0x0600019F RID: 415 RVA: 0x00010044 File Offset: 0x0000F044
			public static MyTextBox.MaskCode[] ParseMaskCodes(string maskString)
			{
				return null;
			}

			// Token: 0x040001B1 RID: 433
			private MaskCodeType maskCodeType;

			// Token: 0x040001B2 RID: 434
			private char maskParameter;
		}
	}
}
