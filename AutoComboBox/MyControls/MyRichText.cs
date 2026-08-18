using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AutoComboBox.InputDialogControls;
using AutoComboBox.MyControls.ExtendedRichTextBox;
using Common.Compression;
using SpellCheckerEx;
using TechnoPro.Common.Compression.Entity;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using TechnoPro.Common.Win32;
using UnivOleDb;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000074 RID: 116
	public class MyRichText : UserControl, MyDynamicControl
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x00023FC4 File Offset: 0x00022FC4
		public MyRichText()
		{
			this.InitializeComponent();
			this.rtf.KeyPress += this.MyRichText_KeyPress;
			this.rtf.KeyUp += this.MyRichText_KeyUp;
			this.rtf.OnShowClockWorkDocumentRequested += this.rtf_OnShowClockWorkDocumentRequested;
			this.rtf.ParentMyRichText = this;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00024058 File Offset: 0x00023058
		private void rtf_OnShowClockWorkDocumentRequested(object sender, ClockWorkDocumentEventArgs e)
		{
			if (e.DocumentId > 0)
			{
				if (this.textFromDatabase.IndexOf("http://click here to open doc#" + e.DocumentId + ">") >= 0 || this.textFromDatabase.IndexOf("http://click_here_to_open_doc#" + e.DocumentId + ">") >= 0)
				{
					MyRichText.ShowFile(e.DocumentId);
				}
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000240E0 File Offset: 0x000230E0
		public static void ShowFile(int fileId)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			if (da != null)
			{
				da.SelectCommand.CommandText = "SELECT fileid,filebytes,filename,isencrypted FROM files WHERE fileid=@fileid";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@fileid", fileId);
				DataTable dataTable = new DataTable();
				da.Fill(dataTable);
				if (dataTable.Rows.Count >= 1)
				{
					string fileName = Path.GetFileName(dataTable.Rows[0]["filename"].ToString().Trim());
					string extension = Path.GetExtension(fileName);
					if (fileName.Length < 1 || extension.Length < 1 || dataTable.Rows[0]["filebytes"] is DBNull)
					{
						MessageBox.Show("Can't find file with id=" + fileId.ToString());
					}
					else
					{
						byte[] array = (byte[])dataTable.Rows[0]["filebytes"];
						if (extension == ".zipcw")
						{
							CompressionBinaryFile compressionBinaryFile = new CompressionBinaryFile
							{
								FileName = fileName,
								FileBytes = array
							};
							IList<CompressionBinaryFile> list = CompressDataAdapter.UncompressFirstLevelFiles(compressionBinaryFile);
							if (list.Count > 0)
							{
								fileName = list[0].FileName;
								array = list[0].FileBytes;
								extension = Path.GetExtension(fileName);
							}
						}
						string tempFileName = FileSystem.GetTempFileName(extension);
						File.WriteAllBytes(tempFileName, array);
						Process.Start(tempFileName);
					}
				}
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000242A4 File Offset: 0x000232A4
		~MyRichText()
		{
			if (this.rtf != null)
			{
				this.rtf.KeyPress -= this.MyRichText_KeyPress;
				this.rtf.KeyUp -= this.MyRichText_KeyUp;
				this.rtf.OnShowClockWorkDocumentRequested -= this.rtf_OnShowClockWorkDocumentRequested;
				this.rtf.ParentMyRichText = null;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00024334 File Offset: 0x00023334
		public object ReportObject
		{
			get
			{
				return this.rtf.Rtf;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00024354 File Offset: 0x00023354
		public bool FilledIn
		{
			get
			{
				return !this.IsEmpty;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00024370 File Offset: 0x00023370
		public new string ToString()
		{
			return this.rtf.Rtf;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00024390 File Offset: 0x00023390
		public void FromString(string s)
		{
			try
			{
				this.rtf.Rtf = s;
			}
			catch
			{
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x000243C8 File Offset: 0x000233C8
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x000243E5 File Offset: 0x000233E5
		public bool OnlyAllowAdding
		{
			get
			{
				return this.rtf.OnlyAllowAdding;
			}
			set
			{
				this.rtf.OnlyAllowAdding = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x000243F8 File Offset: 0x000233F8
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x00024415 File Offset: 0x00023415
		public string WhoAmIName
		{
			get
			{
				return this.rtf.WhoAmIName;
			}
			set
			{
				this.rtf.WhoAmIName = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00024428 File Offset: 0x00023428
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x00024445 File Offset: 0x00023445
		public string PlainText
		{
			get
			{
				return this.rtf.PlainText;
			}
			set
			{
				this.rtf.Text = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00024458 File Offset: 0x00023458
		public bool IsEmpty
		{
			get
			{
				return this.rtf.IsEmpty;
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00024475 File Offset: 0x00023475
		public void SetTextFromDatabase(string rtfText, DataRow dr)
		{
			this.rtf.SetTextFromDatabase(rtfText, dr);
			this.textFromDatabase = this.rtf.Text;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00024498 File Offset: 0x00023498
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x000244B5 File Offset: 0x000234B5
		public bool BaseReadOnly
		{
			get
			{
				return this.rtf.ReadOnly;
			}
			set
			{
				this.rtf.ReadOnly = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x000244C8 File Offset: 0x000234C8
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x000244E8 File Offset: 0x000234E8
		public override string Text
		{
			get
			{
				return this.rtf.Rtf;
			}
			set
			{
				try
				{
					this.rtf.Rtf = value;
				}
				catch
				{
					this.rtf.Text = value;
				}
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0002452C File Offset: 0x0002352C
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x00024544 File Offset: 0x00023544
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00024550 File Offset: 0x00023550
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x0002456D File Offset: 0x0002356D
		public RichTextBoxScrollBars ScrollBars
		{
			get
			{
				return this.rtf.ScrollBars;
			}
			set
			{
				this.rtf.ScrollBars = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00024580 File Offset: 0x00023580
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x000245B4 File Offset: 0x000235B4
		public string Caption
		{
			get
			{
				return this.lbl.Visible ? this.lbl.Text : "";
			}
			set
			{
				if (value.Length > 0)
				{
					this.lbl.Text = value;
				}
				else
				{
					this.lbl.Visible = false;
				}
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x000245F0 File Offset: 0x000235F0
		public RichTextBox RichTextBox
		{
			get
			{
				return this.rtf;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00024608 File Offset: 0x00023608
		public void EnableSpellCheck()
		{
			if (this.sharpSpell == null)
			{
				try
				{
					string dictionaryPath = this.GetDictionaryPath();
					this.sharpSpell = new SpellCheckEx(this.rtf, dictionaryPath, ClientCache.CurrentInstance.DefaultDictionaryFile);
					this.sharpSpell.UnderlineMisSpelledEnabled = true;
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00024674 File Offset: 0x00023674
		private string GetDictionaryPath()
		{
			string text = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "TechnoPro\\ClockWork\\Dictionaries");
			string result;
			if (Directory.Exists(text))
			{
				result = text;
			}
			else
			{
				text = Path.Combine(Directory.GetCurrentDirectory(), "Dictionaries");
				if (Directory.Exists(text))
				{
					result = text;
				}
				else
				{
					result = "";
				}
			}
			return result;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x000246D0 File Offset: 0x000236D0
		public SpellCheckEx SharpSpell
		{
			get
			{
				return this.sharpSpell;
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000246E8 File Offset: 0x000236E8
		private void btn_alignLeft_Click(object sender, EventArgs e)
		{
			this.rtf.SelectionAlignment = HorizontalAlignment.Left;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000246F8 File Offset: 0x000236F8
		private void btn_alignCenter_Click(object sender, EventArgs e)
		{
			this.rtf.SelectionAlignment = HorizontalAlignment.Center;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00024708 File Offset: 0x00023708
		private void btn_alignRight_Click(object sender, EventArgs e)
		{
			this.rtf.SelectionAlignment = HorizontalAlignment.Right;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00024718 File Offset: 0x00023718
		private void ChangeFontStyle(FontStyle fs)
		{
			try
			{
				if (this.rtf.SelectionFont != null)
				{
					Font selectionFont = this.rtf.SelectionFont;
					FontStyle style;
					if ((this.rtf.SelectionFont.Style & fs) == fs)
					{
						style = (this.rtf.SelectionFont.Style ^ fs);
					}
					else
					{
						style = (this.rtf.SelectionFont.Style ^ fs);
					}
					this.rtf.SelectionFont = new Font(selectionFont.FontFamily, selectionFont.Size, style);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(), "Error");
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000247D8 File Offset: 0x000237D8
		private void txt_fontBold_Click(object sender, EventArgs e)
		{
			this.ChangeFontStyle(FontStyle.Bold);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000247E3 File Offset: 0x000237E3
		private void txt_fontItalic_Click(object sender, EventArgs e)
		{
			this.ChangeFontStyle(FontStyle.Italic);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000247EE File Offset: 0x000237EE
		private void txt_fontUnderline_Click(object sender, EventArgs e)
		{
			this.ChangeFontStyle(FontStyle.Underline);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000247F9 File Offset: 0x000237F9
		private void btn_fontStrikeout_Click(object sender, EventArgs e)
		{
			this.ChangeFontStyle(FontStyle.Strikeout);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00024804 File Offset: 0x00023804
		private void btn_bullets_Click(object sender, EventArgs e)
		{
			this.ToggleBullets();
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00024810 File Offset: 0x00023810
		private void ToggleBullets()
		{
			try
			{
				if (this.rtf.SelectionBullet)
				{
					this.rtf.SelectionBullet = false;
				}
				else
				{
					this.rtf.BulletIndent = 10;
					this.rtf.SelectionBullet = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(), "Error");
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0002488C File Offset: 0x0002388C
		public void SetHeight(Control parent, int numLines, out AnchorStyles anchorStyles)
		{
			anchorStyles = this.Anchor;
			bool flag = false;
			if (numLines == -1)
			{
				if (parent != null)
				{
					base.Height = parent.Height - base.Top;
					anchorStyles = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
					flag = true;
				}
				else
				{
					numLines = 5;
				}
			}
			if (!flag)
			{
				if (numLines < 1)
				{
					numLines = 1;
				}
				int num = numLines - 1;
				if (num < 0)
				{
					num++;
				}
				base.Height += this.rtf.Height * numLines;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00024920 File Offset: 0x00023920
		public bool Empty
		{
			get
			{
				return this.rtf.Text.Trim().Length < 1;
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0002494C File Offset: 0x0002394C
		private void MyRichText_KeyPress(object sender, KeyPressEventArgs e)
		{
			bool flag = (Control.ModifierKeys & Keys.Control) == Keys.Control;
			if (flag)
			{
				char keyChar = e.KeyChar;
				if (keyChar <= 'Z')
				{
					switch (keyChar)
					{
					case 'A':
						goto IL_181;
					case 'B':
						break;
					case 'C':
					case 'F':
					case 'G':
					case 'H':
						goto IL_1CB;
					case 'D':
						goto IL_19C;
					case 'E':
						goto IL_19E;
					case 'I':
						goto IL_127;
					default:
						switch (keyChar)
						{
						case 'L':
							goto IL_1AD;
						case 'M':
						case 'N':
						case 'O':
						case 'Q':
						case 'S':
						case 'W':
						case 'X':
							goto IL_1CB;
						case 'P':
							goto IL_191;
						case 'R':
							goto IL_1BC;
						case 'T':
							goto IL_193;
						case 'U':
							goto IL_134;
						case 'V':
							goto IL_18F;
						case 'Y':
							goto IL_161;
						case 'Z':
							goto IL_141;
						default:
							goto IL_1CB;
						}
						break;
					}
				}
				else
				{
					switch (keyChar)
					{
					case 'a':
						goto IL_181;
					case 'b':
						break;
					case 'c':
					case 'f':
					case 'g':
					case 'h':
						goto IL_1CB;
					case 'd':
						goto IL_19C;
					case 'e':
						goto IL_19E;
					case 'i':
						goto IL_127;
					default:
						switch (keyChar)
						{
						case 'l':
							goto IL_1AD;
						case 'm':
						case 'n':
						case 'o':
						case 'q':
						case 's':
						case 'w':
						case 'x':
							goto IL_1CB;
						case 'p':
							goto IL_191;
						case 'r':
							goto IL_1BC;
						case 't':
							goto IL_193;
						case 'u':
							goto IL_134;
						case 'v':
							goto IL_18F;
						case 'y':
							goto IL_161;
						case 'z':
							goto IL_141;
						default:
							goto IL_1CB;
						}
						break;
					}
				}
				this.ChangeFontStyle(FontStyle.Bold);
				goto IL_1CB;
				IL_127:
				this.ChangeFontStyle(FontStyle.Italic);
				goto IL_1CB;
				IL_134:
				this.ChangeFontStyle(FontStyle.Underline);
				goto IL_1CB;
				IL_141:
				if (this.rtf.CanUndo)
				{
					this.rtf.Undo();
				}
				goto IL_1CB;
				IL_161:
				if (this.rtf.CanRedo)
				{
					this.rtf.Redo();
				}
				goto IL_1CB;
				IL_181:
				this.rtf.SelectAll();
				IL_18F:
				IL_191:
				goto IL_1CB;
				IL_193:
				this.ToggleBullets();
				IL_19C:
				goto IL_1CB;
				IL_19E:
				this.rtf.SelectionAlignment = HorizontalAlignment.Center;
				goto IL_1CB;
				IL_1AD:
				this.rtf.SelectionAlignment = HorizontalAlignment.Left;
				goto IL_1CB;
				IL_1BC:
				this.rtf.SelectionAlignment = HorizontalAlignment.Right;
				IL_1CB:;
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00024B28 File Offset: 0x00023B28
		public void ShowSpellChecker()
		{
			if (!this.NotesAreLocked)
			{
				this.sharpSpell.SpellCheck();
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00024B50 File Offset: 0x00023B50
		public void Zoom()
		{
			NotesZoom2 notesZoom = new NotesZoom2(this.ReadOnly || !base.Enabled);
			notesZoom.TextEntered = this.Text;
			DialogResult dialogResult = notesZoom.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				this.Text = notesZoom.TextEntered;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00024BA8 File Offset: 0x00023BA8
		public bool NotesAreLocked
		{
			get
			{
				return this.rtf.ReadOnly || !this.rtf.Enabled;
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00024BD8 File Offset: 0x00023BD8
		private void MyRichText_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F7)
			{
				if (!this.NotesAreLocked)
				{
					this.ShowSpellChecker();
				}
			}
			else if (e.KeyCode == Keys.F10)
			{
				if (!this.NotesAreLocked)
				{
					this.Zoom();
				}
			}
			else
			{
				base.OnKeyUp(e);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00024C3C File Offset: 0x00023C3C
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

		// Token: 0x06000494 RID: 1172 RVA: 0x00024C98 File Offset: 0x00023C98
		private void rtf_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control)
			{
				if (e.KeyValue == 187)
				{
					this.rtf.Font = new Font(this.rtf.Font.FontFamily, this.rtf.Font.Size + 2f);
				}
				else if (e.KeyValue == 189)
				{
					this.rtf.Font = new Font(this.rtf.Font.FontFamily, this.rtf.Font.Size - 2f);
				}
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00024D50 File Offset: 0x00023D50
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				if (this.sharpSpell != null)
				{
					this.sharpSpell.Dispose();
					this.sharpSpell = null;
				}
				this.rtf.KeyPress -= this.MyRichText_KeyPress;
				this.rtf.KeyUp -= this.MyRichText_KeyUp;
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00024DDC File Offset: 0x00023DDC
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MyRichText));
			this.toolStrip1 = new ToolStrip();
			this.txt_fontBold = new ToolStripButton();
			this.txt_fontItalic = new ToolStripButton();
			this.txt_fontUnderline = new ToolStripButton();
			this.btn_fontStrikeout = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.btn_alignLeft = new ToolStripButton();
			this.btn_alignCenter = new ToolStripButton();
			this.btn_alignRight = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.btn_bullets = new ToolStripButton();
			this.lbl = new Label();
			this.rtf = new ExtendedRichTextBoxCtrl();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.txt_fontBold,
				this.txt_fontItalic,
				this.txt_fontUnderline,
				this.btn_fontStrikeout,
				this.toolStripSeparator1,
				this.btn_alignLeft,
				this.btn_alignCenter,
				this.btn_alignRight,
				this.toolStripSeparator2,
				this.btn_bullets
			});
			this.toolStrip1.Location = new Point(0, 16);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(457, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			this.txt_fontBold.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.txt_fontBold.Image = (Image)componentResourceManager.GetObject("txt_fontBold.Image");
			this.txt_fontBold.ImageTransparentColor = Color.Magenta;
			this.txt_fontBold.Name = "txt_fontBold";
			this.txt_fontBold.Size = new Size(23, 22);
			this.txt_fontBold.Text = "Bold";
			this.txt_fontBold.Click += this.txt_fontBold_Click;
			this.txt_fontItalic.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.txt_fontItalic.Image = (Image)componentResourceManager.GetObject("txt_fontItalic.Image");
			this.txt_fontItalic.ImageTransparentColor = Color.Magenta;
			this.txt_fontItalic.Name = "txt_fontItalic";
			this.txt_fontItalic.Size = new Size(23, 22);
			this.txt_fontItalic.Text = "Italic";
			this.txt_fontItalic.Click += this.txt_fontItalic_Click;
			this.txt_fontUnderline.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.txt_fontUnderline.Image = (Image)componentResourceManager.GetObject("txt_fontUnderline.Image");
			this.txt_fontUnderline.ImageTransparentColor = Color.Magenta;
			this.txt_fontUnderline.Name = "txt_fontUnderline";
			this.txt_fontUnderline.Size = new Size(23, 22);
			this.txt_fontUnderline.Text = "Underline";
			this.txt_fontUnderline.Click += this.txt_fontUnderline_Click;
			this.btn_fontStrikeout.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.btn_fontStrikeout.Image = (Image)componentResourceManager.GetObject("btn_fontStrikeout.Image");
			this.btn_fontStrikeout.ImageTransparentColor = Color.Magenta;
			this.btn_fontStrikeout.Name = "btn_fontStrikeout";
			this.btn_fontStrikeout.Size = new Size(23, 22);
			this.btn_fontStrikeout.Text = "Strikeout";
			this.btn_fontStrikeout.Click += this.btn_fontStrikeout_Click;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.btn_alignLeft.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.btn_alignLeft.Image = (Image)componentResourceManager.GetObject("btn_alignLeft.Image");
			this.btn_alignLeft.ImageTransparentColor = Color.Magenta;
			this.btn_alignLeft.Name = "btn_alignLeft";
			this.btn_alignLeft.Size = new Size(23, 22);
			this.btn_alignLeft.Text = "Align &left";
			this.btn_alignLeft.Click += this.btn_alignLeft_Click;
			this.btn_alignCenter.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.btn_alignCenter.Image = (Image)componentResourceManager.GetObject("btn_alignCenter.Image");
			this.btn_alignCenter.ImageTransparentColor = Color.Magenta;
			this.btn_alignCenter.Name = "btn_alignCenter";
			this.btn_alignCenter.Size = new Size(23, 22);
			this.btn_alignCenter.Text = "Align &center";
			this.btn_alignCenter.Click += this.btn_alignCenter_Click;
			this.btn_alignRight.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.btn_alignRight.Image = (Image)componentResourceManager.GetObject("btn_alignRight.Image");
			this.btn_alignRight.ImageTransparentColor = Color.Magenta;
			this.btn_alignRight.Name = "btn_alignRight";
			this.btn_alignRight.Size = new Size(23, 22);
			this.btn_alignRight.Text = "Align &right";
			this.btn_alignRight.Click += this.btn_alignRight_Click;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.btn_bullets.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.btn_bullets.Image = (Image)componentResourceManager.GetObject("btn_bullets.Image");
			this.btn_bullets.ImageTransparentColor = Color.Magenta;
			this.btn_bullets.Name = "btn_bullets";
			this.btn_bullets.Size = new Size(23, 22);
			this.btn_bullets.Text = "Bullets";
			this.btn_bullets.Click += this.btn_bullets_Click;
			this.lbl.AutoSize = true;
			this.lbl.Dock = DockStyle.Top;
			this.lbl.Location = new Point(0, 0);
			this.lbl.Name = "lbl";
			this.lbl.Size = new Size(42, 16);
			this.lbl.TabIndex = 2;
			this.lbl.Text = "&Notes";
			this.rtf.Dock = DockStyle.Fill;
			this.rtf.EnableAutoDragDrop = true;
			this.rtf.Location = new Point(0, 41);
			this.rtf.Name = "rtf";
			this.rtf.OnlyAllowAdding = false;
			this.rtf.Size = new Size(457, 21);
			this.rtf.TabIndex = 3;
			this.rtf.Text = "";
			this.rtf.WhoAmIName = "";
			this.rtf.KeyDown += this.rtf_KeyDown;
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.rtf);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.lbl);
			this.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "MyRichText";
			base.Size = new Size(457, 62);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040003E2 RID: 994
		private string textFromDatabase = "";

		// Token: 0x040003E3 RID: 995
		private bool readOnly = false;

		// Token: 0x040003E4 RID: 996
		private SpellCheckEx sharpSpell = null;

		// Token: 0x040003E5 RID: 997
		private IContainer components = null;

		// Token: 0x040003E6 RID: 998
		private ToolStrip toolStrip1;

		// Token: 0x040003E7 RID: 999
		private ToolStripButton txt_fontBold;

		// Token: 0x040003E8 RID: 1000
		private ToolStripButton txt_fontItalic;

		// Token: 0x040003E9 RID: 1001
		private ToolStripButton txt_fontUnderline;

		// Token: 0x040003EA RID: 1002
		private ToolStripSeparator toolStripSeparator1;

		// Token: 0x040003EB RID: 1003
		private ToolStripButton btn_alignLeft;

		// Token: 0x040003EC RID: 1004
		private Label lbl;

		// Token: 0x040003ED RID: 1005
		private ExtendedRichTextBoxCtrl rtf;

		// Token: 0x040003EE RID: 1006
		private ToolStripButton btn_alignCenter;

		// Token: 0x040003EF RID: 1007
		private ToolStripButton btn_alignRight;

		// Token: 0x040003F0 RID: 1008
		private ToolStripSeparator toolStripSeparator2;

		// Token: 0x040003F1 RID: 1009
		private ToolStripButton btn_fontStrikeout;

		// Token: 0x040003F2 RID: 1010
		private ToolStripButton btn_bullets;
	}
}
