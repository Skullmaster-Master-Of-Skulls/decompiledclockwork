using System;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace System.ComponentModel.Design
{
	// Token: 0x02000197 RID: 407
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public class ByteViewer : TableLayoutPanel
	{
		// Token: 0x06000EBB RID: 3771 RVA: 0x00055398 File Offset: 0x00053598
		public ByteViewer()
		{
			base.SuspendLayout();
			base.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
			base.ColumnCount = 1;
			base.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			base.RowCount = 1;
			base.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.InitUI();
			base.ResumeLayout();
			this.displayMode = DisplayMode.Hexdump;
			this.realDisplayMode = DisplayMode.Hexdump;
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00055430 File Offset: 0x00053630
		private static int AnalizeByteOrderMark(byte[] buffer, int index)
		{
			int c = (int)buffer[index] << 8 | (int)buffer[index + 1];
			int c2 = (int)buffer[index + 2] << 8 | (int)buffer[index + 3];
			int encodingIndex = ByteViewer.GetEncodingIndex(c);
			int encodingIndex2 = ByteViewer.GetEncodingIndex(c2);
			int[,] array = new int[,]
			{
				{
					1,
					5,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1
				},
				{
					1,
					1,
					1,
					11,
					1,
					10,
					4,
					1,
					1,
					1,
					1,
					1,
					1
				},
				{
					2,
					9,
					5,
					2,
					2,
					2,
					2,
					2,
					2,
					2,
					2,
					2,
					2
				},
				{
					3,
					7,
					3,
					7,
					3,
					3,
					3,
					3,
					3,
					3,
					3,
					3,
					3
				},
				{
					14,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1
				},
				{
					1,
					6,
					1,
					1,
					1,
					1,
					1,
					3,
					1,
					1,
					1,
					1,
					1
				},
				{
					1,
					8,
					1,
					1,
					1,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					1
				},
				{
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1
				},
				{
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1
				},
				{
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					13,
					1,
					1
				},
				{
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1
				},
				{
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					12
				},
				{
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1
				}
			};
			return array[encodingIndex, encodingIndex2];
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00055484 File Offset: 0x00053684
		private int CellToIndex(int column, int row)
		{
			return row * this.columnCount + column;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00055490 File Offset: 0x00053690
		private byte[] ComposeLineBuffer(int startLine, int line)
		{
			int num = startLine * this.columnCount;
			byte[] array;
			if (num + (line + 1) * this.columnCount > this.dataBuf.Length)
			{
				array = new byte[this.dataBuf.Length % this.columnCount];
			}
			else
			{
				array = new byte[this.columnCount];
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.dataBuf[num + this.CellToIndex(i, line)];
			}
			return array;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00055504 File Offset: 0x00053704
		private void DrawAddress(Graphics g, int startLine, int line)
		{
			Font address_FONT = ByteViewer.ADDRESS_FONT;
			string s = ((startLine + line) * this.columnCount).ToString("X8", CultureInfo.InvariantCulture);
			using (Brush brush = new SolidBrush(this.ForeColor))
			{
				g.DrawString(s, address_FONT, brush, 5f, (float)(7 + line * 21));
			}
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00055574 File Offset: 0x00053774
		private void DrawClient(Graphics g)
		{
			using (Brush brush = new SolidBrush(SystemColors.ControlLightLight))
			{
				g.FillRectangle(brush, new Rectangle(74, 5, 538, this.rowCount * 21));
			}
			using (Pen pen = new Pen(SystemColors.ControlDark))
			{
				g.DrawRectangle(pen, new Rectangle(74, 5, 537, this.rowCount * 21 - 1));
				g.DrawLine(pen, 474, 5, 474, 5 + this.rowCount * 21 - 1);
			}
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00055628 File Offset: 0x00053828
		private static bool CharIsPrintable(char c)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
			return unicodeCategory != UnicodeCategory.Control || unicodeCategory == UnicodeCategory.Format || unicodeCategory == UnicodeCategory.LineSeparator || unicodeCategory == UnicodeCategory.ParagraphSeparator || unicodeCategory == UnicodeCategory.OtherNotAssigned;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00055658 File Offset: 0x00053858
		private void DrawDump(Graphics g, byte[] lineBuffer, int line)
		{
			StringBuilder stringBuilder = new StringBuilder(lineBuffer.Length);
			for (int i = 0; i < lineBuffer.Length; i++)
			{
				char c = Convert.ToChar(lineBuffer[i]);
				if (ByteViewer.CharIsPrintable(c))
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append('.');
				}
			}
			Font hexdump_FONT = ByteViewer.HEXDUMP_FONT;
			using (Brush brush = new SolidBrush(this.ForeColor))
			{
				g.DrawString(stringBuilder.ToString(), hexdump_FONT, brush, 479f, (float)(7 + line * 21));
			}
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x000556F0 File Offset: 0x000538F0
		private void DrawHex(Graphics g, byte[] lineBuffer, int line)
		{
			Font hexdump_FONT = ByteViewer.HEXDUMP_FONT;
			StringBuilder stringBuilder = new StringBuilder(lineBuffer.Length * 3 + 1);
			for (int i = 0; i < lineBuffer.Length; i++)
			{
				stringBuilder.Append(lineBuffer[i].ToString("X2", CultureInfo.InvariantCulture));
				stringBuilder.Append(" ");
				if (i == this.columnCount / 2 - 1)
				{
					stringBuilder.Append(" ");
				}
			}
			using (Brush brush = new SolidBrush(this.ForeColor))
			{
				g.DrawString(stringBuilder.ToString(), hexdump_FONT, brush, 76f, (float)(7 + line * 21));
			}
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x000557A4 File Offset: 0x000539A4
		private void DrawLines(Graphics g, int startLine, int linesCount)
		{
			for (int i = 0; i < linesCount; i++)
			{
				byte[] lineBuffer = this.ComposeLineBuffer(startLine, i);
				this.DrawAddress(g, startLine, i);
				this.DrawHex(g, lineBuffer, i);
				this.DrawDump(g, lineBuffer, i);
			}
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x000557E4 File Offset: 0x000539E4
		private DisplayMode GetAutoDisplayMode()
		{
			int num = 0;
			int num2 = 0;
			if (this.dataBuf == null || (this.dataBuf.Length >= 0 && this.dataBuf.Length < 8))
			{
				return DisplayMode.Hexdump;
			}
			switch (ByteViewer.AnalizeByteOrderMark(this.dataBuf, 0))
			{
			case 2:
				return DisplayMode.Hexdump;
			case 3:
				return DisplayMode.Unicode;
			case 4:
			case 5:
				return DisplayMode.Hexdump;
			case 6:
			case 7:
				return DisplayMode.Hexdump;
			case 8:
			case 9:
				return DisplayMode.Hexdump;
			case 10:
			case 11:
				return DisplayMode.Hexdump;
			case 12:
				return DisplayMode.Hexdump;
			case 13:
				return DisplayMode.Ansi;
			case 14:
				return DisplayMode.Ansi;
			default:
			{
				int num3;
				if (this.dataBuf.Length > 1024)
				{
					num3 = 512;
				}
				else
				{
					num3 = this.dataBuf.Length / 2;
				}
				for (int i = 0; i < num3; i++)
				{
					char c = (char)this.dataBuf[i];
					if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
					{
						num++;
					}
				}
				for (int j = 0; j < num3; j += 2)
				{
					char[] array = new char[1];
					Encoding.Unicode.GetChars(this.dataBuf, j, 2, array, 0);
					if (ByteViewer.CharIsPrintable(array[0]))
					{
						num2++;
					}
				}
				if (num2 * 100 / (num3 / 2) > 80)
				{
					return DisplayMode.Unicode;
				}
				if (num * 100 / num3 > 80)
				{
					return DisplayMode.Ansi;
				}
				return DisplayMode.Hexdump;
			}
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0005591E File Offset: 0x00053B1E
		public virtual byte[] GetBytes()
		{
			return this.dataBuf;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00055926 File Offset: 0x00053B26
		public virtual DisplayMode GetDisplayMode()
		{
			return this.displayMode;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00055930 File Offset: 0x00053B30
		private static int GetEncodingIndex(int c1)
		{
			if (c1 <= 16128)
			{
				if (c1 <= 63)
				{
					if (c1 == 0)
					{
						return 1;
					}
					if (c1 == 60)
					{
						return 6;
					}
					if (c1 == 63)
					{
						return 8;
					}
				}
				else
				{
					if (c1 == 15360)
					{
						return 5;
					}
					if (c1 == 15423)
					{
						return 9;
					}
					if (c1 == 16128)
					{
						return 7;
					}
				}
			}
			else if (c1 <= 42900)
			{
				if (c1 == 19567)
				{
					return 11;
				}
				if (c1 == 30829)
				{
					return 10;
				}
				if (c1 == 42900)
				{
					return 12;
				}
			}
			else
			{
				if (c1 == 61371)
				{
					return 4;
				}
				if (c1 == 65279)
				{
					return 2;
				}
				if (c1 == 65534)
				{
					return 3;
				}
			}
			return 0;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x000559CC File Offset: 0x00053BCC
		private void InitAnsi()
		{
			int num = this.dataBuf.Length;
			char[] array = new char[num + 1];
			num = NativeMethods.MultiByteToWideChar(0, 0, this.dataBuf, num, array, num);
			array[num] = '\0';
			for (int i = 0; i < num; i++)
			{
				if (array[i] == '\0')
				{
					array[i] = '\v';
				}
			}
			this.edit.Text = new string(array);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00055A28 File Offset: 0x00053C28
		private void InitUnicode()
		{
			char[] array = new char[this.dataBuf.Length / 2 + 1];
			Encoding.Unicode.GetChars(this.dataBuf, 0, this.dataBuf.Length, array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == '\0')
				{
					array[i] = '\v';
				}
			}
			array[array.Length - 1] = '\0';
			this.edit.Text = new string(array);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00055A94 File Offset: 0x00053C94
		private void InitUI()
		{
			this.SCROLLBAR_HEIGHT = SystemInformation.HorizontalScrollBarHeight;
			this.SCROLLBAR_WIDTH = SystemInformation.VerticalScrollBarWidth;
			base.Size = new Size(612 + this.SCROLLBAR_WIDTH + 2 + 3, 10 + this.rowCount * 21);
			this.scrollBar = new VScrollBar();
			this.scrollBar.ValueChanged += this.ScrollChanged;
			this.scrollBar.TabStop = true;
			this.scrollBar.TabIndex = 0;
			this.scrollBar.Dock = DockStyle.Right;
			this.scrollBar.Visible = false;
			this.edit = new TextBox();
			this.edit.AutoSize = false;
			this.edit.BorderStyle = BorderStyle.None;
			this.edit.Multiline = true;
			this.edit.ReadOnly = true;
			this.edit.ScrollBars = ScrollBars.Both;
			this.edit.AcceptsTab = true;
			this.edit.AcceptsReturn = true;
			this.edit.Dock = DockStyle.Fill;
			this.edit.Margin = Padding.Empty;
			this.edit.WordWrap = false;
			this.edit.Visible = false;
			base.Controls.Add(this.scrollBar, 0, 0);
			base.Controls.Add(this.edit, 0, 0);
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00055BEC File Offset: 0x00053DEC
		private void InitState()
		{
			this.linesCount = (this.dataBuf.Length + this.columnCount - 1) / this.columnCount;
			this.startLine = 0;
			if (this.linesCount > this.rowCount)
			{
				this.displayLinesCount = this.rowCount;
				this.scrollBar.Hide();
				this.scrollBar.Maximum = this.linesCount - 1;
				this.scrollBar.LargeChange = this.rowCount;
				this.scrollBar.Show();
				this.scrollBar.Enabled = true;
			}
			else
			{
				this.displayLinesCount = this.linesCount;
				this.scrollBar.Hide();
				this.scrollBar.Maximum = this.rowCount;
				this.scrollBar.LargeChange = this.rowCount;
				this.scrollBar.Show();
				this.scrollBar.Enabled = false;
			}
			this.scrollBar.Select();
			base.Invalidate();
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00055CE1 File Offset: 0x00053EE1
		protected override void OnKeyDown(KeyEventArgs e)
		{
			this.scrollBar.Select();
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00055CF0 File Offset: 0x00053EF0
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			switch (this.realDisplayMode)
			{
			case DisplayMode.Hexdump:
				base.SuspendLayout();
				this.edit.Hide();
				this.scrollBar.Show();
				base.ResumeLayout();
				this.DrawClient(graphics);
				this.DrawLines(graphics, this.startLine, this.displayLinesCount);
				return;
			case DisplayMode.Ansi:
				this.edit.Invalidate();
				return;
			case DisplayMode.Unicode:
				this.edit.Invalidate();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00055D7C File Offset: 0x00053F7C
		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			int num = (base.ClientSize.Height - 10) / 21;
			if (num >= 0 && num != this.rowCount)
			{
				this.rowCount = num;
				if (this.Dock == DockStyle.None)
				{
					base.Size = new Size(612 + this.SCROLLBAR_WIDTH + 2 + 3, 10 + this.rowCount * 21);
				}
				if (this.scrollBar != null)
				{
					if (this.linesCount > this.rowCount)
					{
						this.scrollBar.Hide();
						this.scrollBar.Maximum = this.linesCount - 1;
						this.scrollBar.LargeChange = this.rowCount;
						this.scrollBar.Show();
						this.scrollBar.Enabled = true;
						this.scrollBar.Select();
					}
					else
					{
						this.scrollBar.Enabled = false;
					}
				}
				this.displayLinesCount = ((this.startLine + this.rowCount < this.linesCount) ? this.rowCount : (this.linesCount - this.startLine));
				return;
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00055E90 File Offset: 0x00054090
		public virtual void SaveToFile(string path)
		{
			if (this.dataBuf != null)
			{
				FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
				try
				{
					fileStream.Write(this.dataBuf, 0, this.dataBuf.Length);
					fileStream.Close();
				}
				catch
				{
					fileStream.Close();
					throw;
				}
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00055EE8 File Offset: 0x000540E8
		protected virtual void ScrollChanged(object source, EventArgs e)
		{
			this.startLine = this.scrollBar.Value;
			base.Invalidate();
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00055F01 File Offset: 0x00054101
		public virtual void SetBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (this.dataBuf != null)
			{
				this.dataBuf = null;
			}
			this.dataBuf = bytes;
			this.InitState();
			this.SetDisplayMode(this.displayMode);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00055F3C File Offset: 0x0005413C
		public virtual void SetDisplayMode(DisplayMode mode)
		{
			if (!ClientUtils.IsEnumValid(mode, (int)mode, 1, 4))
			{
				throw new InvalidEnumArgumentException("mode", (int)mode, typeof(DisplayMode));
			}
			this.displayMode = mode;
			this.realDisplayMode = ((mode == DisplayMode.Auto) ? this.GetAutoDisplayMode() : mode);
			switch (this.realDisplayMode)
			{
			case DisplayMode.Hexdump:
				base.SuspendLayout();
				this.edit.Hide();
				if (this.linesCount <= this.rowCount)
				{
					base.ResumeLayout();
					return;
				}
				if (!this.scrollBar.Visible)
				{
					this.scrollBar.Show();
					base.ResumeLayout();
					this.scrollBar.Invalidate();
					this.scrollBar.Select();
					return;
				}
				base.ResumeLayout();
				return;
			case DisplayMode.Ansi:
				this.InitAnsi();
				base.SuspendLayout();
				this.edit.Show();
				this.scrollBar.Hide();
				base.ResumeLayout();
				base.Invalidate();
				return;
			case DisplayMode.Unicode:
				this.InitUnicode();
				base.SuspendLayout();
				this.edit.Show();
				this.scrollBar.Hide();
				base.ResumeLayout();
				base.Invalidate();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00056064 File Offset: 0x00054264
		public virtual void SetFile(string path)
		{
			FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
			try
			{
				int num = (int)fileStream.Length;
				byte[] array = new byte[num + 1];
				fileStream.Read(array, 0, num);
				this.SetBytes(array);
				fileStream.Close();
			}
			catch
			{
				fileStream.Close();
				throw;
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000560C0 File Offset: 0x000542C0
		public virtual void SetStartLine(int line)
		{
			if (line < 0 || line >= this.linesCount || line > this.dataBuf.Length / this.columnCount)
			{
				this.startLine = 0;
				return;
			}
			this.startLine = line;
		}

		// Token: 0x040008B5 RID: 2229
		private const int DEFAULT_COLUMN_COUNT = 16;

		// Token: 0x040008B6 RID: 2230
		private const int DEFAULT_ROW_COUNT = 25;

		// Token: 0x040008B7 RID: 2231
		private const int COLUMN_COUNT = 16;

		// Token: 0x040008B8 RID: 2232
		private const int BORDER_GAP = 2;

		// Token: 0x040008B9 RID: 2233
		private const int INSET_GAP = 3;

		// Token: 0x040008BA RID: 2234
		private const int CELL_HEIGHT = 21;

		// Token: 0x040008BB RID: 2235
		private const int CELL_WIDTH = 25;

		// Token: 0x040008BC RID: 2236
		private const int CHAR_WIDTH = 8;

		// Token: 0x040008BD RID: 2237
		private const int ADDRESS_WIDTH = 69;

		// Token: 0x040008BE RID: 2238
		private const int HEX_WIDTH = 400;

		// Token: 0x040008BF RID: 2239
		private const int DUMP_WIDTH = 128;

		// Token: 0x040008C0 RID: 2240
		private int SCROLLBAR_HEIGHT;

		// Token: 0x040008C1 RID: 2241
		private int SCROLLBAR_WIDTH;

		// Token: 0x040008C2 RID: 2242
		private const int HEX_DUMP_GAP = 5;

		// Token: 0x040008C3 RID: 2243
		private const int ADDRESS_START_X = 5;

		// Token: 0x040008C4 RID: 2244
		private const int CLIENT_START_Y = 5;

		// Token: 0x040008C5 RID: 2245
		private const int LINE_START_Y = 7;

		// Token: 0x040008C6 RID: 2246
		private const int HEX_START_X = 74;

		// Token: 0x040008C7 RID: 2247
		private const int DUMP_START_X = 479;

		// Token: 0x040008C8 RID: 2248
		private const int SCROLLBAR_START_X = 612;

		// Token: 0x040008C9 RID: 2249
		private static readonly Font ADDRESS_FONT = new Font("Microsoft Sans Serif", 8f);

		// Token: 0x040008CA RID: 2250
		private static readonly Font HEXDUMP_FONT = new Font("Courier New", 8f);

		// Token: 0x040008CB RID: 2251
		private VScrollBar scrollBar;

		// Token: 0x040008CC RID: 2252
		private TextBox edit;

		// Token: 0x040008CD RID: 2253
		private int columnCount = 16;

		// Token: 0x040008CE RID: 2254
		private int rowCount = 25;

		// Token: 0x040008CF RID: 2255
		private byte[] dataBuf;

		// Token: 0x040008D0 RID: 2256
		private int startLine;

		// Token: 0x040008D1 RID: 2257
		private int displayLinesCount;

		// Token: 0x040008D2 RID: 2258
		private int linesCount;

		// Token: 0x040008D3 RID: 2259
		private DisplayMode displayMode;

		// Token: 0x040008D4 RID: 2260
		private DisplayMode realDisplayMode;
	}
}
