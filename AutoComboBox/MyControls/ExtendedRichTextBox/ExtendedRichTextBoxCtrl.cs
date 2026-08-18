using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AutoComboBox.InputDialogControls;

namespace AutoComboBox.MyControls.ExtendedRichTextBox
{
	// Token: 0x0200004C RID: 76
	public class ExtendedRichTextBoxCtrl : RichTextBox
	{
		// Token: 0x060002EA RID: 746 RVA: 0x00017F48 File Offset: 0x00016F48
		protected override void Dispose(bool disposing)
		{
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

		// Token: 0x060002EB RID: 747 RVA: 0x00018038 File Offset: 0x00017038
		public ExtendedRichTextBoxCtrl()
		{
			this.myToolTip = new ToolTip();
			base.LinkClicked += this.MudRichTextBox_LinkClicked;
			this.textColor = RtfColor.Black;
			this.highlightColor = RtfColor.White;
			this.rtfColor = new Dictionary<RtfColor, string>();
			this.rtfColor[RtfColor.Aqua] = "\\red0\\green255\\blue255";
			this.rtfColor[RtfColor.Black] = "\\red0\\green0\\blue0";
			this.rtfColor[RtfColor.Blue] = "\\red0\\green0\\blue255";
			this.rtfColor[RtfColor.Fuchsia] = "\\red255\\green0\\blue255";
			this.rtfColor[RtfColor.Gray] = "\\red128\\green128\\blue128";
			this.rtfColor[RtfColor.Green] = "\\red0\\green128\\blue0";
			this.rtfColor[RtfColor.Lime] = "\\red0\\green255\\blue0";
			this.rtfColor[RtfColor.Maroon] = "\\red128\\green0\\blue0";
			this.rtfColor[RtfColor.Navy] = "\\red0\\green0\\blue128";
			this.rtfColor[RtfColor.Olive] = "\\red128\\green128\\blue0";
			this.rtfColor[RtfColor.Purple] = "\\red128\\green0\\blue128";
			this.rtfColor[RtfColor.Red] = "\\red255\\green0\\blue0";
			this.rtfColor[RtfColor.Silver] = "\\red192\\green192\\blue192";
			this.rtfColor[RtfColor.Teal] = "\\red0\\green128\\blue128";
			this.rtfColor[RtfColor.White] = "\\red255\\green255\\blue255";
			this.rtfColor[RtfColor.Yellow] = "\\red255\\green255\\blue0";
			this.rtfFontFamily = new Dictionary<string, string>();
			this.rtfFontFamily[FontFamily.GenericMonospace.Name] = "\\fmodern";
			this.rtfFontFamily[FontFamily.GenericSansSerif.Name] = "\\fswiss";
			this.rtfFontFamily[FontFamily.GenericSerif.Name] = "\\froman";
			this.rtfFontFamily["UNKNOWN"] = "\\fnil";
			using (Graphics graphics = base.CreateGraphics())
			{
				this.xDpi = graphics.DpiX;
				this.yDpi = graphics.DpiY;
			}
			base.EnableAutoDragDrop = true;
			ExtendedRichTextBoxCtrl.HideCaret(base.Handle);
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000182B8 File Offset: 0x000172B8
		public string PlainText
		{
			get
			{
				return this.Text;
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000182D0 File Offset: 0x000172D0
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

		// Token: 0x060002EE RID: 750 RVA: 0x000184C8 File Offset: 0x000174C8
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

		// Token: 0x060002EF RID: 751 RVA: 0x00018580 File Offset: 0x00017580
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
				this.ReplaceText(base.SelectionStart, base.SelectionStart + this.SelectionLength, "", false);
				break;
			case "select all":
				base.SelectAll();
				break;
			case "spell check (f7)":
				if (this.parentMyRichText != null && !this.parentMyRichText.NotesAreLocked)
				{
					this.parentMyRichText.ShowSpellChecker();
				}
				break;
			case "zoom (f10)":
				if (this.parentMyRichText != null && !this.parentMyRichText.NotesAreLocked)
				{
					this.parentMyRichText.Zoom();
				}
				break;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00018744 File Offset: 0x00017744
		private bool AllowedToEdit
		{
			get
			{
				return !this.onlyAllowAdding || !base.ReadOnly;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00018774 File Offset: 0x00017774
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00018860 File Offset: 0x00017860
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00018878 File Offset: 0x00017878
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00018884 File Offset: 0x00017884
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0001889C File Offset: 0x0001789C
		public string WhoAmIName
		{
			get
			{
				return this.whoAmIName;
			}
			set
			{
				this.whoAmIName = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x000188A8 File Offset: 0x000178A8
		public bool IsEmpty
		{
			get
			{
				return this.PlainText.Trim().Length < 1;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000188D0 File Offset: 0x000178D0
		public void SetTextFromDatabase(string rtfText, DataRow dr)
		{
			base.Rtf = rtfText;
			if (this.onlyAllowAdding)
			{
				bool flag = dr == null || (dr.RowState != DataRowState.Unchanged && dr.RowState == DataRowState.Added);
				base.ReadOnly = !flag;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0001893C File Offset: 0x0001793C
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)
			{
				if (e.KeyCode == Keys.Return)
				{
					if (this.onlyAllowAdding && base.ReadOnly)
					{
						this.AppendNote();
					}
				}
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000189A0 File Offset: 0x000179A0
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)
			{
				if (this.onlyAllowAdding && base.ReadOnly)
				{
					this.AppendNote();
				}
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000189F0 File Offset: 0x000179F0
		private void AppendNote()
		{
			MyPanel topMyPanel = this.GetTopMyPanel();
			if (topMyPanel == null || topMyPanel.Enabled)
			{
				DialogResult dialogResult = MessageBox.Show("This text has been locked - would you like to append a note onto the end?", "Append note", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					NotesZoom2 notesZoom = new NotesZoom2();
					notesZoom.Text = this.Text;
					dialogResult = notesZoom.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						string newLine = Environment.NewLine;
						base.AppendText(string.Concat(new string[]
						{
							newLine,
							newLine,
							"NOTE APPENDED by ",
							this.whoAmIName,
							" on ",
							DateTime.Now.ToString("yyyy-MM-dd h:mm tt"),
							newLine
						}));
						this.AppendRtf(notesZoom.TextEntered);
					}
				}
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00018AD8 File Offset: 0x00017AD8
		private MyPanel GetTopMyPanel()
		{
			Control parent;
			for (parent = base.Parent; parent != null; parent = parent.Parent)
			{
				if (parent is MyPanel)
				{
					MyPanel myPanel = (MyPanel)parent;
					if (myPanel.IsTopLevelDynamicControlsContainer)
					{
						break;
					}
				}
			}
			MyPanel result;
			if (parent != null && parent is MyPanel)
			{
				result = (MyPanel)parent;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060002FC RID: 764
		[DllImport("user32", CharSet = CharSet.Auto)]
		private static extern int SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

		// Token: 0x060002FD RID: 765
		[DllImport("user32", CharSet = CharSet.Auto)]
		private static extern int SendMessage(HandleRef hWnd, int msg, int wParam, ref ExtendedRichTextBoxCtrl.CHARFORMAT2 lParam);

		// Token: 0x060002FE RID: 766
		[DllImport("user32", CharSet = CharSet.Auto)]
		private static extern int SendMessage(HandleRef hWnd, int msg, int wParam, ref ExtendedRichTextBoxCtrl.PARAFORMAT2 lParam);

		// Token: 0x060002FF RID: 767
		[DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
		private static extern int SetWindowTheme(HandleRef hWnd, [MarshalAs(UnmanagedType.LPWStr)] string pszSubAppName, [MarshalAs(UnmanagedType.LPWStr)] string pszSubIdList);

		// Token: 0x06000300 RID: 768
		[DllImport("user32.dll")]
		protected static extern bool HideCaret(IntPtr hWnd);

		// Token: 0x06000301 RID: 769
		[DllImport("user32", CharSet = CharSet.Auto)]
		private static extern int GetScrollInfo(HandleRef hWnd, int nBar, ref ExtendedRichTextBoxCtrl.SCROLLINFO info);

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00018B50 File Offset: 0x00017B50
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00018BC0 File Offset: 0x00017BC0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public UnderlineStyle SelectionUnderlineStyle
		{
			get
			{
				ExtendedRichTextBoxCtrl.CHARFORMAT2 charformat = default(ExtendedRichTextBoxCtrl.CHARFORMAT2);
				charformat.cbSize = Marshal.SizeOf(charformat);
				ExtendedRichTextBoxCtrl.SendMessage(new HandleRef(this, base.Handle), 1082, 1, ref charformat);
				UnderlineStyle result;
				if ((charformat.dwMask & 8388608U) == 0U)
				{
					result = UnderlineStyle.None;
				}
				else
				{
					byte b = charformat.bUnderlineType & 15;
					result = (UnderlineStyle)b;
				}
				return result;
			}
			set
			{
				UnderlineColor underlineColor = this.SelectionUnderlineColor;
				if (value == UnderlineStyle.None)
				{
					underlineColor = UnderlineColor.Black;
				}
				ExtendedRichTextBoxCtrl.CHARFORMAT2 charformat = default(ExtendedRichTextBoxCtrl.CHARFORMAT2);
				charformat.cbSize = Marshal.SizeOf(charformat);
				charformat.dwMask = 8388608U;
				charformat.bUnderlineType = ((byte)value | (byte)underlineColor);
				ExtendedRichTextBoxCtrl.SendMessage(new HandleRef(this, base.Handle), 1092, 1, ref charformat);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00018C30 File Offset: 0x00017C30
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00018CA4 File Offset: 0x00017CA4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public UnderlineColor SelectionUnderlineColor
		{
			get
			{
				ExtendedRichTextBoxCtrl.CHARFORMAT2 charformat = default(ExtendedRichTextBoxCtrl.CHARFORMAT2);
				charformat.cbSize = Marshal.SizeOf(charformat);
				ExtendedRichTextBoxCtrl.SendMessage(new HandleRef(this, base.Handle), 1082, 1, ref charformat);
				UnderlineColor result;
				if ((charformat.dwMask & 8388608U) == 0U)
				{
					result = UnderlineColor.Black;
				}
				else
				{
					byte b = charformat.bUnderlineType & 240;
					result = (UnderlineColor)b;
				}
				return result;
			}
			set
			{
				if (value == UnderlineColor.Black)
				{
					this.SelectionUnderlineStyle = UnderlineStyle.None;
				}
				else
				{
					UnderlineStyle selectionUnderlineStyle = this.SelectionUnderlineStyle;
					if (selectionUnderlineStyle == UnderlineStyle.None)
					{
						value = UnderlineColor.Black;
					}
					ExtendedRichTextBoxCtrl.CHARFORMAT2 charformat = default(ExtendedRichTextBoxCtrl.CHARFORMAT2);
					charformat.cbSize = Marshal.SizeOf(charformat);
					charformat.dwMask = 8388608U;
					charformat.bUnderlineType = ((byte)selectionUnderlineStyle | (byte)value);
					ExtendedRichTextBoxCtrl.SendMessage(new HandleRef(this, base.Handle), 1092, 1, ref charformat);
				}
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00018D30 File Offset: 0x00017D30
		public void BeginUpdate()
		{
			this._Updating++;
			if (this._Updating <= 1)
			{
				this._OldEventMask = ExtendedRichTextBoxCtrl.SendMessage(new HandleRef(this, base.Handle), 1073, 0, 0);
				ExtendedRichTextBoxCtrl.SendMessage(new HandleRef(this, base.Handle), 11, 0, 0);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00018D94 File Offset: 0x00017D94
		public void EndUpdate()
		{
			this._Updating--;
			if (this._Updating <= 0)
			{
				ExtendedRichTextBoxCtrl.SendMessage(new HandleRef(this, base.Handle), 11, 1, 0);
				ExtendedRichTextBoxCtrl.SendMessage(new HandleRef(this, base.Handle), 1073, 0, this._OldEventMask);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00018DF6 File Offset: 0x00017DF6
		public void ScrollToBottom()
		{
			ExtendedRichTextBoxCtrl.SendMessage(new HandleRef(this, base.Handle), 277, 7, 0);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00018E12 File Offset: 0x00017E12
		public void ScrollPageUp()
		{
			ExtendedRichTextBoxCtrl.SendMessage(new HandleRef(this, base.Handle), 277, 2, 0);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00018E2E File Offset: 0x00017E2E
		public void ScrollPageDown()
		{
			ExtendedRichTextBoxCtrl.SendMessage(new HandleRef(this, base.Handle), 277, 3, 0);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00018E4C File Offset: 0x00017E4C
		public void ScrollLineUp(int num)
		{
			for (int i = 0; i < num; i++)
			{
				ExtendedRichTextBoxCtrl.SendMessage(new HandleRef(this, base.Handle), 277, 0, 0);
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00018E88 File Offset: 0x00017E88
		public void ScrollLineDown(int num)
		{
			for (int i = 0; i < num; i++)
			{
				ExtendedRichTextBoxCtrl.SendMessage(new HandleRef(this, base.Handle), 277, 1, 0);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00018EC4 File Offset: 0x00017EC4
		public ScrollBarInformation VerticalScrollInformation
		{
			get
			{
				ExtendedRichTextBoxCtrl.SCROLLINFO scrollinfo = default(ExtendedRichTextBoxCtrl.SCROLLINFO);
				scrollinfo.cbSize = Marshal.SizeOf(scrollinfo);
				scrollinfo.fMask = 23;
				int scrollInfo = ExtendedRichTextBoxCtrl.GetScrollInfo(new HandleRef(this, base.Handle), 1, ref scrollinfo);
				return new ScrollBarInformation(scrollinfo.nMin, scrollinfo.nMax, scrollinfo.nPage, scrollinfo.nPos, scrollinfo.nTrackPos);
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00018F36 File Offset: 0x00017F36
		public void AppendRtf(string _rtf)
		{
			base.Select(this.TextLength, 0);
			base.SelectedRtf = _rtf;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00018F4F File Offset: 0x00017F4F
		public void InsertRtf(string _rtf)
		{
			base.SelectedRtf = _rtf;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00018F5A File Offset: 0x00017F5A
		public void AppendTextAsRtf(string _text)
		{
			this.AppendTextAsRtf(_text, this.Font);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00018F6B File Offset: 0x00017F6B
		public void AppendTextAsRtf(string _text, Font _font)
		{
			this.AppendTextAsRtf(_text, _font, this.textColor);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00018F7D File Offset: 0x00017F7D
		public void AppendTextAsRtf(string _text, Font _font, RtfColor _textColor)
		{
			this.AppendTextAsRtf(_text, _font, _textColor, this.highlightColor);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00018F90 File Offset: 0x00017F90
		public void AppendTextAsRtf(string _text, Font _font, RtfColor _textColor, RtfColor _backColor)
		{
			base.Select(this.TextLength, 0);
			this.InsertTextAsRtf(_text, _font, _textColor, _backColor);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00018FAD File Offset: 0x00017FAD
		public void InsertTextAsRtf(string _text)
		{
			this.InsertTextAsRtf(_text, this.Font);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00018FBE File Offset: 0x00017FBE
		public void InsertTextAsRtf(string _text, Font _font)
		{
			this.InsertTextAsRtf(_text, _font, this.textColor);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00018FD0 File Offset: 0x00017FD0
		public void InsertTextAsRtf(string _text, Font _font, RtfColor _textColor)
		{
			this.InsertTextAsRtf(_text, _font, _textColor, this.highlightColor);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00018FE4 File Offset: 0x00017FE4
		public void InsertTextAsRtf(string _text, Font _font, RtfColor _textColor, RtfColor _backColor)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033");
			stringBuilder.Append(this.GetFontTable(_font));
			stringBuilder.Append(this.GetColorTable(_textColor, _backColor));
			stringBuilder.Append(this.GetDocumentArea(_text, _font));
			base.SelectedRtf = stringBuilder.ToString();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00019040 File Offset: 0x00018040
		private string GetDocumentArea(string _text, Font _font)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\\viewkind4\\uc1\\pard\\cf1\\f0\\fs20");
			stringBuilder.Append("\\highlight2");
			if (_font.Bold)
			{
				stringBuilder.Append("\\b");
			}
			if (_font.Italic)
			{
				stringBuilder.Append("\\i");
			}
			if (_font.Strikeout)
			{
				stringBuilder.Append("\\strike");
			}
			if (_font.Underline)
			{
				stringBuilder.Append("\\ul");
			}
			stringBuilder.Append("\\f0");
			stringBuilder.Append("\\fs");
			stringBuilder.Append((int)Math.Round((double)(2f * _font.SizeInPoints)));
			stringBuilder.Append(" ");
			stringBuilder.Append(_text.Replace("\n", "\\par "));
			stringBuilder.Append("\\highlight0");
			if (_font.Bold)
			{
				stringBuilder.Append("\\b0");
			}
			if (_font.Italic)
			{
				stringBuilder.Append("\\i0");
			}
			if (_font.Strikeout)
			{
				stringBuilder.Append("\\strike0");
			}
			if (_font.Underline)
			{
				stringBuilder.Append("\\ulnone");
			}
			stringBuilder.Append("\\f0");
			stringBuilder.Append("\\fs20");
			stringBuilder.Append("\\cf0\\fs17}");
			return stringBuilder.ToString();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000191C4 File Offset: 0x000181C4
		public void InsertImage(Image _image)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033");
			stringBuilder.Append(this.GetFontTable(this.Font));
			stringBuilder.Append(this.GetImagePrefix(_image));
			stringBuilder.Append(this.GetRtfImage(_image));
			stringBuilder.Append(this.RTF_IMAGE_POST);
			base.SelectedRtf = stringBuilder.ToString();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00019230 File Offset: 0x00018230
		private string GetImagePrefix(Image _image)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int value = (int)Math.Round((double)((float)_image.Width / this.xDpi * 2540f));
			int value2 = (int)Math.Round((double)((float)_image.Height / this.yDpi * 2540f));
			int value3 = (int)Math.Round((double)((float)_image.Width / this.xDpi * 1440f));
			int value4 = (int)Math.Round((double)((float)_image.Height / this.yDpi * 1440f));
			stringBuilder.Append("{\\pict\\wmetafile8");
			stringBuilder.Append("\\picw");
			stringBuilder.Append(value);
			stringBuilder.Append("\\pich");
			stringBuilder.Append(value2);
			stringBuilder.Append("\\picwgoal");
			stringBuilder.Append(value3);
			stringBuilder.Append("\\pichgoal");
			stringBuilder.Append(value4);
			stringBuilder.Append(" ");
			return stringBuilder.ToString();
		}

		// Token: 0x0600031B RID: 795
		[DllImport("gdiplus.dll")]
		private static extern uint GdipEmfToWmfBits(IntPtr _hEmf, uint _bufferSize, byte[] _buffer, int _mappingMode, ExtendedRichTextBoxCtrl.EmfToWmfBitsFlags _flags);

		// Token: 0x0600031C RID: 796 RVA: 0x0001932C File Offset: 0x0001832C
		private string GetRtfImage(Image _image)
		{
			StringBuilder stringBuilder = null;
			MemoryStream memoryStream = null;
			Graphics graphics = null;
			Metafile metafile = null;
			string result;
			try
			{
				stringBuilder = new StringBuilder();
				memoryStream = new MemoryStream();
				Graphics graphics2;
				graphics = (graphics2 = base.CreateGraphics());
				try
				{
					IntPtr hdc = graphics.GetHdc();
					metafile = new Metafile(memoryStream, hdc);
					graphics.ReleaseHdc(hdc);
				}
				finally
				{
					if (graphics2 != null)
					{
						((IDisposable)graphics2).Dispose();
					}
				}
				graphics = (graphics2 = Graphics.FromImage(metafile));
				try
				{
					graphics.DrawImage(_image, new Rectangle(0, 0, _image.Width, _image.Height));
				}
				finally
				{
					if (graphics2 != null)
					{
						((IDisposable)graphics2).Dispose();
					}
				}
				IntPtr henhmetafile = metafile.GetHenhmetafile();
				uint num = ExtendedRichTextBoxCtrl.GdipEmfToWmfBits(henhmetafile, 0U, null, 8, ExtendedRichTextBoxCtrl.EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);
				byte[] array = new byte[num];
				uint num2 = ExtendedRichTextBoxCtrl.GdipEmfToWmfBits(henhmetafile, num, array, 8, ExtendedRichTextBoxCtrl.EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(string.Format("{0:X2}", array[i]));
				}
				result = stringBuilder.ToString();
			}
			finally
			{
				if (graphics != null)
				{
					graphics.Dispose();
				}
				if (metafile != null)
				{
					metafile.Dispose();
				}
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
			}
			return result;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001949C File Offset: 0x0001849C
		private string GetFontTable(Font _font)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\\fonttbl{\\f0");
			stringBuilder.Append("\\");
			if (this.rtfFontFamily.ContainsKey(_font.FontFamily.Name))
			{
				stringBuilder.Append(this.rtfFontFamily[_font.FontFamily.Name]);
			}
			else
			{
				stringBuilder.Append(this.rtfFontFamily["UNKNOWN"]);
			}
			stringBuilder.Append("\\fcharset0 ");
			stringBuilder.Append(_font.Name);
			stringBuilder.Append(";}}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00019550 File Offset: 0x00018550
		private string GetColorTable(RtfColor _textColor, RtfColor _backColor)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\\colortbl ;");
			stringBuilder.Append(this.rtfColor[_textColor]);
			stringBuilder.Append(";");
			stringBuilder.Append(this.rtfColor[_backColor]);
			stringBuilder.Append(";}\\n");
			return stringBuilder.ToString();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000195B8 File Offset: 0x000185B8
		private string RemoveBadChars(string _originalRtf)
		{
			return _originalRtf.Replace("\0", "");
		}

		// Token: 0x06000320 RID: 800
		[DllImport("USER32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		// Token: 0x06000321 RID: 801 RVA: 0x000195DC File Offset: 0x000185DC
		public int Print(int charFrom, int charTo, PrintPageEventArgs e)
		{
			ExtendedRichTextBoxCtrl.RECT rc;
			rc.Top = (int)((double)e.MarginBounds.Top * 14.4);
			rc.Bottom = (int)((double)e.MarginBounds.Bottom * 14.4);
			rc.Left = (int)((double)e.MarginBounds.Left * 14.4);
			rc.Right = (int)((double)e.MarginBounds.Right * 14.4);
			ExtendedRichTextBoxCtrl.RECT rcPage;
			rcPage.Top = (int)((double)e.PageBounds.Top * 14.4);
			rcPage.Bottom = (int)((double)e.PageBounds.Bottom * 14.4);
			rcPage.Left = (int)((double)e.PageBounds.Left * 14.4);
			rcPage.Right = (int)((double)e.PageBounds.Right * 14.4);
			IntPtr hdc = e.Graphics.GetHdc();
			ExtendedRichTextBoxCtrl.FORMATRANGE formatrange;
			formatrange.chrg.cpMax = charTo;
			formatrange.chrg.cpMin = charFrom;
			formatrange.hdc = hdc;
			formatrange.hdcTarget = hdc;
			formatrange.rc = rc;
			formatrange.rcPage = rcPage;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			zero = new IntPtr(1);
			IntPtr intPtr2 = IntPtr.Zero;
			intPtr2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(formatrange));
			Marshal.StructureToPtr(formatrange, intPtr2, false);
			intPtr = ExtendedRichTextBoxCtrl.SendMessage(base.Handle, 1081, zero, intPtr2);
			Marshal.FreeCoTaskMem(intPtr2);
			e.Graphics.ReleaseHdc(hdc);
			return intPtr.ToInt32();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000197B8 File Offset: 0x000187B8
		private void MudRichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			if (e.LinkText.StartsWith("http://click_here_to_open_doc#", StringComparison.OrdinalIgnoreCase) || e.LinkText.StartsWith("http://click here to open doc#", StringComparison.OrdinalIgnoreCase))
			{
				string s = e.LinkText.Substring(30);
				int docId;
				if (int.TryParse(s, out docId))
				{
					this.FireOnShowClockWorkDocumentRequested(docId);
					return;
				}
			}
			Process.Start(e.LinkText);
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000323 RID: 803 RVA: 0x0001982C File Offset: 0x0001882C
		// (remove) Token: 0x06000324 RID: 804 RVA: 0x00019868 File Offset: 0x00018868
		public event EventHandler<ClockWorkDocumentEventArgs> OnShowClockWorkDocumentRequested;

		// Token: 0x06000325 RID: 805 RVA: 0x000198A4 File Offset: 0x000188A4
		private void FireOnShowClockWorkDocumentRequested(int docId)
		{
			EventHandler<ClockWorkDocumentEventArgs> onShowClockWorkDocumentRequested = this.OnShowClockWorkDocumentRequested;
			if (onShowClockWorkDocumentRequested != null)
			{
				onShowClockWorkDocumentRequested(this, new ClockWorkDocumentEventArgs
				{
					DocumentId = docId
				});
			}
		}

		// Token: 0x170000AF RID: 175
		// (set) Token: 0x06000326 RID: 806 RVA: 0x000198DA File Offset: 0x000188DA
		public MyRichText ParentMyRichText
		{
			set
			{
				this.parentMyRichText = value;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000198E4 File Offset: 0x000188E4
		private void MyTextBox_Click(object sender, EventArgs e)
		{
			if (this.cm.Tag is Point && (Point)this.cm.Tag != Point.Empty)
			{
				Point point = (Point)this.cm.Tag;
				int x = point.X;
				int y = point.Y;
				ToolStripItem toolStripItem = (ToolStripItem)sender;
				string text = toolStripItem.Text;
				this.ReplaceText(x, y, text, true);
			}
		}

		// Token: 0x0400026A RID: 618
		private const string RTF_HEADER = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033";

		// Token: 0x0400026B RID: 619
		private const string RTF_DOCUMENT_PRE = "\\viewkind4\\uc1\\pard\\cf1\\f0\\fs20";

		// Token: 0x0400026C RID: 620
		private const string RTF_DOCUMENT_POST = "\\cf0\\fs17}";

		// Token: 0x0400026D RID: 621
		private const string FF_UNKNOWN = "UNKNOWN";

		// Token: 0x0400026E RID: 622
		private const int WM_VSCROLL = 277;

		// Token: 0x0400026F RID: 623
		private const int WM_HSCROLL = 276;

		// Token: 0x04000270 RID: 624
		private const int SB_LINEUP = 0;

		// Token: 0x04000271 RID: 625
		private const int SB_LINEDOWN = 1;

		// Token: 0x04000272 RID: 626
		private const int SB_PAGEUP = 2;

		// Token: 0x04000273 RID: 627
		private const int SB_PAGEDOWN = 3;

		// Token: 0x04000274 RID: 628
		private const int SB_THUMBPOSITION = 4;

		// Token: 0x04000275 RID: 629
		private const int SB_THUMBTRACK = 5;

		// Token: 0x04000276 RID: 630
		private const int SB_TOP = 6;

		// Token: 0x04000277 RID: 631
		private const int SB_BOTTOM = 7;

		// Token: 0x04000278 RID: 632
		private const int SB_ENDSCROLL = 8;

		// Token: 0x04000279 RID: 633
		private const int WM_SETREDRAW = 11;

		// Token: 0x0400027A RID: 634
		private const int EM_SETEVENTMASK = 1073;

		// Token: 0x0400027B RID: 635
		private const int EM_SETCHARFORMAT = 1092;

		// Token: 0x0400027C RID: 636
		private const int EM_GETCHARFORMAT = 1082;

		// Token: 0x0400027D RID: 637
		private const int EM_GETPARAFORMAT = 1085;

		// Token: 0x0400027E RID: 638
		private const int EM_SETPARAFORMAT = 1095;

		// Token: 0x0400027F RID: 639
		private const int EM_SETTYPOGRAPHYOPTIONS = 1226;

		// Token: 0x04000280 RID: 640
		private const int CFM_UNDERLINETYPE = 8388608;

		// Token: 0x04000281 RID: 641
		private const int CFM_BACKCOLOR = 67108864;

		// Token: 0x04000282 RID: 642
		private const int CFE_AUTOBACKCOLOR = 67108864;

		// Token: 0x04000283 RID: 643
		private const int SCF_SELECTION = 1;

		// Token: 0x04000284 RID: 644
		private const int PFM_ALIGNMENT = 8;

		// Token: 0x04000285 RID: 645
		private const int TO_ADVANCEDTYPOGRAPHY = 1;

		// Token: 0x04000286 RID: 646
		private const int SBS_HORIZ = 0;

		// Token: 0x04000287 RID: 647
		private const int SBS_VERT = 1;

		// Token: 0x04000288 RID: 648
		private const int SIF_RANGE = 1;

		// Token: 0x04000289 RID: 649
		private const int SIF_PAGE = 2;

		// Token: 0x0400028A RID: 650
		private const int SIF_POS = 4;

		// Token: 0x0400028B RID: 651
		private const int SIF_DISABLENOSCROLL = 8;

		// Token: 0x0400028C RID: 652
		private const int SIF_TRACKPOS = 16;

		// Token: 0x0400028D RID: 653
		private const int SIF_ALL = 23;

		// Token: 0x0400028E RID: 654
		private const int MM_TEXT = 1;

		// Token: 0x0400028F RID: 655
		private const int MM_LOMETRIC = 2;

		// Token: 0x04000290 RID: 656
		private const int MM_HIMETRIC = 3;

		// Token: 0x04000291 RID: 657
		private const int MM_LOENGLISH = 4;

		// Token: 0x04000292 RID: 658
		private const int MM_HIENGLISH = 5;

		// Token: 0x04000293 RID: 659
		private const int MM_TWIPS = 6;

		// Token: 0x04000294 RID: 660
		private const int MM_ISOTROPIC = 7;

		// Token: 0x04000295 RID: 661
		private const int MM_ANISOTROPIC = 8;

		// Token: 0x04000296 RID: 662
		private const int HMM_PER_INCH = 2540;

		// Token: 0x04000297 RID: 663
		private const int TWIPS_PER_INCH = 1440;

		// Token: 0x04000298 RID: 664
		private const double anInch = 14.4;

		// Token: 0x04000299 RID: 665
		private const int WM_USER = 1024;

		// Token: 0x0400029A RID: 666
		private const int EM_FORMATRANGE = 1081;

		// Token: 0x0400029B RID: 667
		private const int WM_RBUTTONDBLCLK = 518;

		// Token: 0x0400029C RID: 668
		private const int WM_RBUTTONDOWN = 516;

		// Token: 0x0400029D RID: 669
		private int _Updating = 0;

		// Token: 0x0400029E RID: 670
		private int _OldEventMask = 0;

		// Token: 0x0400029F RID: 671
		private ToolTip myToolTip;

		// Token: 0x040002A0 RID: 672
		private bool onlyAllowAdding = false;

		// Token: 0x040002A1 RID: 673
		private string whoAmIName = "";

		// Token: 0x040002A2 RID: 674
		private ContextMenuStrip cm = null;

		// Token: 0x040002A3 RID: 675
		private int baseContextMenuItemCount = 9;

		// Token: 0x040002A4 RID: 676
		private string RTF_IMAGE_POST = "}";

		// Token: 0x040002A5 RID: 677
		private RtfColor textColor;

		// Token: 0x040002A6 RID: 678
		private RtfColor highlightColor;

		// Token: 0x040002A7 RID: 679
		private Dictionary<RtfColor, string> rtfColor;

		// Token: 0x040002A8 RID: 680
		private Dictionary<string, string> rtfFontFamily;

		// Token: 0x040002A9 RID: 681
		private float xDpi;

		// Token: 0x040002AA RID: 682
		private float yDpi;

		// Token: 0x040002AC RID: 684
		private MyRichText parentMyRichText = null;

		// Token: 0x0200004D RID: 77
		private enum EmfToWmfBitsFlags
		{
			// Token: 0x040002AE RID: 686
			EmfToWmfBitsFlagsDefault,
			// Token: 0x040002AF RID: 687
			EmfToWmfBitsFlagsEmbedEmf,
			// Token: 0x040002B0 RID: 688
			EmfToWmfBitsFlagsIncludePlaceable,
			// Token: 0x040002B1 RID: 689
			EmfToWmfBitsFlagsNoXORClip = 4
		}

		// Token: 0x0200004E RID: 78
		private struct CHARFORMAT
		{
			// Token: 0x040002B2 RID: 690
			public int cbSize;

			// Token: 0x040002B3 RID: 691
			public uint dwMask;

			// Token: 0x040002B4 RID: 692
			public uint dwEffects;

			// Token: 0x040002B5 RID: 693
			public int yHeight;

			// Token: 0x040002B6 RID: 694
			public int yOffset;

			// Token: 0x040002B7 RID: 695
			public int crTextColor;

			// Token: 0x040002B8 RID: 696
			public byte bCharSet;

			// Token: 0x040002B9 RID: 697
			public byte bPitchAndFamily;

			// Token: 0x040002BA RID: 698
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public char[] szFaceName;
		}

		// Token: 0x0200004F RID: 79
		private struct CHARFORMAT2
		{
			// Token: 0x040002BB RID: 699
			public int cbSize;

			// Token: 0x040002BC RID: 700
			public uint dwMask;

			// Token: 0x040002BD RID: 701
			public uint dwEffects;

			// Token: 0x040002BE RID: 702
			public int yHeight;

			// Token: 0x040002BF RID: 703
			public int yOffset;

			// Token: 0x040002C0 RID: 704
			public int crTextColor;

			// Token: 0x040002C1 RID: 705
			public byte bCharSet;

			// Token: 0x040002C2 RID: 706
			public byte bPitchAndFamily;

			// Token: 0x040002C3 RID: 707
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public char[] szFaceName;

			// Token: 0x040002C4 RID: 708
			public short wWeight;

			// Token: 0x040002C5 RID: 709
			public short sSpacing;

			// Token: 0x040002C6 RID: 710
			public int crBackColor;

			// Token: 0x040002C7 RID: 711
			public int LCID;

			// Token: 0x040002C8 RID: 712
			public uint dwReserved;

			// Token: 0x040002C9 RID: 713
			public short sStyle;

			// Token: 0x040002CA RID: 714
			public short wKerning;

			// Token: 0x040002CB RID: 715
			public byte bUnderlineType;

			// Token: 0x040002CC RID: 716
			public byte bAnimation;

			// Token: 0x040002CD RID: 717
			public byte bRevAuthor;
		}

		// Token: 0x02000050 RID: 80
		private struct PARAFORMAT
		{
			// Token: 0x040002CE RID: 718
			public int cbSize;

			// Token: 0x040002CF RID: 719
			public uint dwMask;

			// Token: 0x040002D0 RID: 720
			public short wNumbering;

			// Token: 0x040002D1 RID: 721
			public short wReserved;

			// Token: 0x040002D2 RID: 722
			public int dxStartIndent;

			// Token: 0x040002D3 RID: 723
			public int dxRightIndent;

			// Token: 0x040002D4 RID: 724
			public int dxOffset;

			// Token: 0x040002D5 RID: 725
			public short wAlignment;

			// Token: 0x040002D6 RID: 726
			public short cTabCount;

			// Token: 0x040002D7 RID: 727
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public int[] rgxTabs;
		}

		// Token: 0x02000051 RID: 81
		private struct PARAFORMAT2
		{
			// Token: 0x040002D8 RID: 728
			public int cbSize;

			// Token: 0x040002D9 RID: 729
			public uint dwMask;

			// Token: 0x040002DA RID: 730
			public short wNumbering;

			// Token: 0x040002DB RID: 731
			public short wReserved;

			// Token: 0x040002DC RID: 732
			public int dxStartIndent;

			// Token: 0x040002DD RID: 733
			public int dxRightIndent;

			// Token: 0x040002DE RID: 734
			public int dxOffset;

			// Token: 0x040002DF RID: 735
			public short wAlignment;

			// Token: 0x040002E0 RID: 736
			public short cTabCount;

			// Token: 0x040002E1 RID: 737
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public int[] rgxTabs;

			// Token: 0x040002E2 RID: 738
			public int dySpaceBefore;

			// Token: 0x040002E3 RID: 739
			public int dySpaceAfter;

			// Token: 0x040002E4 RID: 740
			public int dyLineSpacing;

			// Token: 0x040002E5 RID: 741
			public short sStyle;

			// Token: 0x040002E6 RID: 742
			public byte bLineSpacingRule;

			// Token: 0x040002E7 RID: 743
			public byte bOutlineLevel;

			// Token: 0x040002E8 RID: 744
			public short wShadingWeight;

			// Token: 0x040002E9 RID: 745
			public short wShadingStyle;

			// Token: 0x040002EA RID: 746
			public short wNumberingStart;

			// Token: 0x040002EB RID: 747
			public short wNumberingStyle;

			// Token: 0x040002EC RID: 748
			public short wNumberingTab;

			// Token: 0x040002ED RID: 749
			public short wBorderSpace;

			// Token: 0x040002EE RID: 750
			public short wBorderWidth;

			// Token: 0x040002EF RID: 751
			public short wBorders;
		}

		// Token: 0x02000052 RID: 82
		private struct SCROLLINFO
		{
			// Token: 0x040002F0 RID: 752
			public int cbSize;

			// Token: 0x040002F1 RID: 753
			public int fMask;

			// Token: 0x040002F2 RID: 754
			public int nMin;

			// Token: 0x040002F3 RID: 755
			public int nMax;

			// Token: 0x040002F4 RID: 756
			public int nPage;

			// Token: 0x040002F5 RID: 757
			public int nPos;

			// Token: 0x040002F6 RID: 758
			public int nTrackPos;
		}

		// Token: 0x02000053 RID: 83
		private struct RtfColorDef
		{
			// Token: 0x040002F7 RID: 759
			public const string Black = "\\red0\\green0\\blue0";

			// Token: 0x040002F8 RID: 760
			public const string Maroon = "\\red128\\green0\\blue0";

			// Token: 0x040002F9 RID: 761
			public const string Green = "\\red0\\green128\\blue0";

			// Token: 0x040002FA RID: 762
			public const string Olive = "\\red128\\green128\\blue0";

			// Token: 0x040002FB RID: 763
			public const string Navy = "\\red0\\green0\\blue128";

			// Token: 0x040002FC RID: 764
			public const string Purple = "\\red128\\green0\\blue128";

			// Token: 0x040002FD RID: 765
			public const string Teal = "\\red0\\green128\\blue128";

			// Token: 0x040002FE RID: 766
			public const string Gray = "\\red128\\green128\\blue128";

			// Token: 0x040002FF RID: 767
			public const string Silver = "\\red192\\green192\\blue192";

			// Token: 0x04000300 RID: 768
			public const string Red = "\\red255\\green0\\blue0";

			// Token: 0x04000301 RID: 769
			public const string Lime = "\\red0\\green255\\blue0";

			// Token: 0x04000302 RID: 770
			public const string Yellow = "\\red255\\green255\\blue0";

			// Token: 0x04000303 RID: 771
			public const string Blue = "\\red0\\green0\\blue255";

			// Token: 0x04000304 RID: 772
			public const string Fuchsia = "\\red255\\green0\\blue255";

			// Token: 0x04000305 RID: 773
			public const string Aqua = "\\red0\\green255\\blue255";

			// Token: 0x04000306 RID: 774
			public const string White = "\\red255\\green255\\blue255";
		}

		// Token: 0x02000054 RID: 84
		private struct RtfFontFamilyDef
		{
			// Token: 0x04000307 RID: 775
			public const string Unknown = "\\fnil";

			// Token: 0x04000308 RID: 776
			public const string Roman = "\\froman";

			// Token: 0x04000309 RID: 777
			public const string Swiss = "\\fswiss";

			// Token: 0x0400030A RID: 778
			public const string Modern = "\\fmodern";

			// Token: 0x0400030B RID: 779
			public const string Script = "\\fscript";

			// Token: 0x0400030C RID: 780
			public const string Decor = "\\fdecor";

			// Token: 0x0400030D RID: 781
			public const string Technical = "\\ftech";

			// Token: 0x0400030E RID: 782
			public const string BiDirect = "\\fbidi";
		}

		// Token: 0x02000055 RID: 85
		private struct RECT
		{
			// Token: 0x0400030F RID: 783
			public int Left;

			// Token: 0x04000310 RID: 784
			public int Top;

			// Token: 0x04000311 RID: 785
			public int Right;

			// Token: 0x04000312 RID: 786
			public int Bottom;
		}

		// Token: 0x02000056 RID: 86
		private struct CHARRANGE
		{
			// Token: 0x04000313 RID: 787
			public int cpMin;

			// Token: 0x04000314 RID: 788
			public int cpMax;
		}

		// Token: 0x02000057 RID: 87
		private struct FORMATRANGE
		{
			// Token: 0x04000315 RID: 789
			public IntPtr hdc;

			// Token: 0x04000316 RID: 790
			public IntPtr hdcTarget;

			// Token: 0x04000317 RID: 791
			public ExtendedRichTextBoxCtrl.RECT rc;

			// Token: 0x04000318 RID: 792
			public ExtendedRichTextBoxCtrl.RECT rcPage;

			// Token: 0x04000319 RID: 793
			public ExtendedRichTextBoxCtrl.CHARRANGE chrg;
		}
	}
}
