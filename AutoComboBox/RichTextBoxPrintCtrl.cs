using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using SpellCheckerEx;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;

namespace AutoComboBox
{
	// Token: 0x02000003 RID: 3
	public class RichTextBoxPrintCtrl : RichTextBox
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00001050
		public RichTextBoxPrintCtrl()
		{
			this.textColor = RtfColor.Black;
			this.highlightColor = RtfColor.White;
			this.rtfColor = new HybridDictionary();
			this.rtfColor.Add(RtfColor.Aqua, "\\red0\\green255\\blue255");
			this.rtfColor.Add(RtfColor.Black, "\\red0\\green0\\blue0");
			this.rtfColor.Add(RtfColor.Blue, "\\red0\\green0\\blue255");
			this.rtfColor.Add(RtfColor.Fuchsia, "\\red255\\green0\\blue255");
			this.rtfColor.Add(RtfColor.Gray, "\\red128\\green128\\blue128");
			this.rtfColor.Add(RtfColor.Green, "\\red0\\green128\\blue0");
			this.rtfColor.Add(RtfColor.Lime, "\\red0\\green255\\blue0");
			this.rtfColor.Add(RtfColor.Maroon, "\\red128\\green0\\blue0");
			this.rtfColor.Add(RtfColor.Navy, "\\red0\\green0\\blue128");
			this.rtfColor.Add(RtfColor.Olive, "\\red128\\green128\\blue0");
			this.rtfColor.Add(RtfColor.Purple, "\\red128\\green0\\blue128");
			this.rtfColor.Add(RtfColor.Red, "\\red255\\green0\\blue0");
			this.rtfColor.Add(RtfColor.Silver, "\\red192\\green192\\blue192");
			this.rtfColor.Add(RtfColor.Teal, "\\red0\\green128\\blue128");
			this.rtfColor.Add(RtfColor.White, "\\red255\\green255\\blue255");
			this.rtfColor.Add(RtfColor.Yellow, "\\red255\\green255\\blue0");
			this.rtfFontFamily = new HybridDictionary();
			this.rtfFontFamily.Add(FontFamily.GenericMonospace.Name, "\\fmodern");
			this.rtfFontFamily.Add(FontFamily.GenericSansSerif, "\\fswiss");
			this.rtfFontFamily.Add(FontFamily.GenericSerif, "\\froman");
			this.rtfFontFamily.Add("UNKNOWN", "\\fnil");
			using (Graphics graphics = base.CreateGraphics())
			{
				this.xDpi = graphics.DpiX;
				this.yDpi = graphics.DpiY;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000022B8 File Offset: 0x000012B8
		public void EnableSpellCheck()
		{
			if (this.sharpSpell == null)
			{
				try
				{
					this.sharpSpell = new SpellCheckEx(this, this.GetDictionaryPath(), ClientCache.CurrentInstance.DefaultDictionaryFile);
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002310 File Offset: 0x00001310
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

		// Token: 0x06000004 RID: 4 RVA: 0x0000234C File Offset: 0x0000134C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.sharpSpell != null)
			{
				this.sharpSpell.Dispose();
				this.sharpSpell = null;
			}
		}

		// Token: 0x06000005 RID: 5
		[DllImport("USER32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002384 File Offset: 0x00001384
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000239C File Offset: 0x0000139C
		public RtfColor TextColor
		{
			get
			{
				return this.textColor;
			}
			set
			{
				this.textColor = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000023A8 File Offset: 0x000013A8
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000023C0 File Offset: 0x000013C0
		public RtfColor HiglightColor
		{
			get
			{
				return this.highlightColor;
			}
			set
			{
				this.highlightColor = value;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023CC File Offset: 0x000013CC
		public int Print(int charFrom, int charTo, PrintPageEventArgs e)
		{
			RichTextBoxPrintCtrl.RECT rc;
			rc.Top = (int)((double)e.MarginBounds.Top * 14.4);
			rc.Bottom = (int)((double)e.MarginBounds.Bottom * 14.4);
			rc.Left = (int)((double)e.MarginBounds.Left * 14.4);
			rc.Right = (int)((double)e.MarginBounds.Right * 14.4);
			RichTextBoxPrintCtrl.RECT rcPage;
			rcPage.Top = (int)((double)e.PageBounds.Top * 14.4);
			rcPage.Bottom = (int)((double)e.PageBounds.Bottom * 14.4);
			rcPage.Left = (int)((double)e.PageBounds.Left * 14.4);
			rcPage.Right = (int)((double)e.PageBounds.Right * 14.4);
			IntPtr hdc = e.Graphics.GetHdc();
			RichTextBoxPrintCtrl.FORMATRANGE formatrange;
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
			intPtr = RichTextBoxPrintCtrl.SendMessage(base.Handle, 1081, zero, intPtr2);
			Marshal.FreeCoTaskMem(intPtr2);
			e.Graphics.ReleaseHdc(hdc);
			return intPtr.ToInt32();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000025A7 File Offset: 0x000015A7
		public void printDocument1_BeginPrint(object sender, PrintEventArgs e)
		{
			this.checkPrint = 0;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000025B4 File Offset: 0x000015B4
		public void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			this.checkPrint = this.Print(this.checkPrint, this.TextLength, e);
			if (this.checkPrint < this.TextLength)
			{
				e.HasMorePages = true;
			}
			else
			{
				e.HasMorePages = false;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002602 File Offset: 0x00001602
		public void AddText(int fontSize, FontStyle fontStyle, string text)
		{
			this.AddText(new Font(base.SelectionFont.FontFamily, (float)fontSize, fontStyle), base.SelectionColor, text);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002626 File Offset: 0x00001626
		public void AddText(Font font, string text)
		{
			this.AddText(font, base.SelectionColor, text);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002638 File Offset: 0x00001638
		public void AddText(int fontSize, FontStyle fontStyle, Color colour, string text)
		{
			this.AddText(new Font(base.SelectionFont.FontFamily, (float)fontSize, fontStyle), colour, text);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002658 File Offset: 0x00001658
		public void AddText(Font font, Color colour, string text)
		{
			try
			{
				int selectionStart = base.SelectionStart;
				this.Text += text;
				base.SelectionStart = selectionStart;
				this.SelectionLength = this.Text.Length - selectionStart;
				base.SelectionFont = font;
				base.SelectionColor = colour;
				base.SelectionStart = this.Text.Length - 1;
				this.SelectionLength = 0;
			}
			catch
			{
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000026E4 File Offset: 0x000016E4
		public void AddText2(int fontSize, FontStyle fontStyle, string text)
		{
			this.AddText2(new Font(base.SelectionFont.FontFamily, (float)fontSize, fontStyle), base.SelectionColor, text);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002708 File Offset: 0x00001708
		public void AddText2(Font font, string text)
		{
			this.AddText2(font, base.SelectionColor, text);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000271A File Offset: 0x0000171A
		public void AddText2(int fontSize, FontStyle fontStyle, Color colour, string text)
		{
			this.AddText2(new Font(base.SelectionFont.FontFamily, (float)fontSize, fontStyle), colour, text);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000273C File Offset: 0x0000173C
		public void AddText2(Font font, Color colour, string text)
		{
			try
			{
				int length = text.Length;
				base.AppendText(text);
				int length2 = this.Text.Length;
				base.SelectionStart = length2 - length;
				this.SelectionLength = length;
				base.SelectionFont = font;
				base.SelectionColor = colour;
				base.SelectionProtected = true;
				this.SelectionLength = 0;
			}
			catch
			{
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000027B4 File Offset: 0x000017B4
		public void InsertTextAsRtf(string _text)
		{
			this.InsertTextAsRtf(_text, this.Font);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027C5 File Offset: 0x000017C5
		public void InsertTextAsRtf(string _text, Font _font)
		{
			this.InsertTextAsRtf(_text, _font, this.textColor);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027D7 File Offset: 0x000017D7
		public void InsertTextAsRtf(string _text, Font _font, RtfColor _textColor)
		{
			this.InsertTextAsRtf(_text, _font, _textColor, this.highlightColor);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027EC File Offset: 0x000017EC
		public void InsertTextAsRtf(string _text, Font _font, RtfColor _textColor, RtfColor _backColor)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033");
			stringBuilder.Append(this.GetFontTable(_font));
			stringBuilder.Append(this.GetColorTable(_textColor, _backColor));
			stringBuilder.Append(this.GetDocumentArea(_text, _font));
			base.SelectedRtf = stringBuilder.ToString();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002848 File Offset: 0x00001848
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

		// Token: 0x0600001A RID: 26 RVA: 0x000029CC File Offset: 0x000019CC
		private string GetFontTable(Font _font)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\\fonttbl{\\f0");
			stringBuilder.Append("\\");
			if (this.rtfFontFamily.Contains(_font.FontFamily.Name))
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

		// Token: 0x0600001B RID: 27 RVA: 0x00002A7C File Offset: 0x00001A7C
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

		// Token: 0x0600001C RID: 28 RVA: 0x00002AF0 File Offset: 0x00001AF0
		private string RemoveBadChars(string _originalRtf)
		{
			return _originalRtf.Replace("\0", "");
		}

		// Token: 0x04000012 RID: 18
		private const double anInch = 14.4;

		// Token: 0x04000013 RID: 19
		private const int MM_TEXT = 1;

		// Token: 0x04000014 RID: 20
		private const int MM_LOMETRIC = 2;

		// Token: 0x04000015 RID: 21
		private const int MM_HIMETRIC = 3;

		// Token: 0x04000016 RID: 22
		private const int MM_LOENGLISH = 4;

		// Token: 0x04000017 RID: 23
		private const int MM_HIENGLISH = 5;

		// Token: 0x04000018 RID: 24
		private const int MM_TWIPS = 6;

		// Token: 0x04000019 RID: 25
		private const int MM_ISOTROPIC = 7;

		// Token: 0x0400001A RID: 26
		private const int MM_ANISOTROPIC = 8;

		// Token: 0x0400001B RID: 27
		private const string FF_UNKNOWN = "UNKNOWN";

		// Token: 0x0400001C RID: 28
		private const int HMM_PER_INCH = 2540;

		// Token: 0x0400001D RID: 29
		private const int TWIPS_PER_INCH = 1440;

		// Token: 0x0400001E RID: 30
		private const string RTF_HEADER = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033";

		// Token: 0x0400001F RID: 31
		private const string RTF_DOCUMENT_PRE = "\\viewkind4\\uc1\\pard\\cf1\\f0\\fs20";

		// Token: 0x04000020 RID: 32
		private const string RTF_DOCUMENT_POST = "\\cf0\\fs17}";

		// Token: 0x04000021 RID: 33
		private const string RTF_TABLE_PRE = "\\viewkind4\\uc1\\trowd\\trleft-5\\trbrdrt\\brdrs\\brdrw10\\trbrdr1\\brdrs\\brdrw10";

		// Token: 0x04000022 RID: 34
		private const string RTF_TABLE_POST = "\\row\\pard\\cf0\\f0";

		// Token: 0x04000023 RID: 35
		private const int WM_USER = 1024;

		// Token: 0x04000024 RID: 36
		private const int EM_FORMATRANGE = 1081;

		// Token: 0x04000025 RID: 37
		private SpellCheckEx sharpSpell = null;

		// Token: 0x04000026 RID: 38
		private RtfColor textColor;

		// Token: 0x04000027 RID: 39
		private RtfColor highlightColor;

		// Token: 0x04000028 RID: 40
		private HybridDictionary rtfColor;

		// Token: 0x04000029 RID: 41
		private HybridDictionary rtfFontFamily;

		// Token: 0x0400002A RID: 42
		private float xDpi;

		// Token: 0x0400002B RID: 43
		private float yDpi;

		// Token: 0x0400002C RID: 44
		private string RTF_IMAGE_POST = "}";

		// Token: 0x0400002D RID: 45
		private int checkPrint;

		// Token: 0x02000004 RID: 4
		private struct RtfColorDef
		{
			// Token: 0x0400002E RID: 46
			public const string Black = "\\red0\\green0\\blue0";

			// Token: 0x0400002F RID: 47
			public const string Maroon = "\\red128\\green0\\blue0";

			// Token: 0x04000030 RID: 48
			public const string Green = "\\red0\\green128\\blue0";

			// Token: 0x04000031 RID: 49
			public const string Olive = "\\red128\\green128\\blue0";

			// Token: 0x04000032 RID: 50
			public const string Navy = "\\red0\\green0\\blue128";

			// Token: 0x04000033 RID: 51
			public const string Purple = "\\red128\\green0\\blue128";

			// Token: 0x04000034 RID: 52
			public const string Teal = "\\red0\\green128\\blue128";

			// Token: 0x04000035 RID: 53
			public const string Gray = "\\red128\\green128\\blue128";

			// Token: 0x04000036 RID: 54
			public const string Silver = "\\red192\\green192\\blue192";

			// Token: 0x04000037 RID: 55
			public const string Red = "\\red255\\green0\\blue0";

			// Token: 0x04000038 RID: 56
			public const string Lime = "\\red0\\green255\\blue0";

			// Token: 0x04000039 RID: 57
			public const string Yellow = "\\red255\\green255\\blue0";

			// Token: 0x0400003A RID: 58
			public const string Blue = "\\red0\\green0\\blue255";

			// Token: 0x0400003B RID: 59
			public const string Fuchsia = "\\red255\\green0\\blue255";

			// Token: 0x0400003C RID: 60
			public const string Aqua = "\\red0\\green255\\blue255";

			// Token: 0x0400003D RID: 61
			public const string White = "\\red255\\green255\\blue255";
		}

		// Token: 0x02000005 RID: 5
		private struct RtfFontFamilyDef
		{
			// Token: 0x0400003E RID: 62
			public const string Unknown = "\\fnil";

			// Token: 0x0400003F RID: 63
			public const string Roman = "\\froman";

			// Token: 0x04000040 RID: 64
			public const string Swiss = "\\fswiss";

			// Token: 0x04000041 RID: 65
			public const string Modern = "\\fmodern";

			// Token: 0x04000042 RID: 66
			public const string Script = "\\fscript";

			// Token: 0x04000043 RID: 67
			public const string Decor = "\\fdecor";

			// Token: 0x04000044 RID: 68
			public const string Technical = "\\ftech";

			// Token: 0x04000045 RID: 69
			public const string BiDirect = "\\fbidi";
		}

		// Token: 0x02000006 RID: 6
		private struct RECT
		{
			// Token: 0x04000046 RID: 70
			public int Left;

			// Token: 0x04000047 RID: 71
			public int Top;

			// Token: 0x04000048 RID: 72
			public int Right;

			// Token: 0x04000049 RID: 73
			public int Bottom;
		}

		// Token: 0x02000007 RID: 7
		private struct CHARRANGE
		{
			// Token: 0x0400004A RID: 74
			public int cpMin;

			// Token: 0x0400004B RID: 75
			public int cpMax;
		}

		// Token: 0x02000008 RID: 8
		private struct FORMATRANGE
		{
			// Token: 0x0400004C RID: 76
			public IntPtr hdc;

			// Token: 0x0400004D RID: 77
			public IntPtr hdcTarget;

			// Token: 0x0400004E RID: 78
			public RichTextBoxPrintCtrl.RECT rc;

			// Token: 0x0400004F RID: 79
			public RichTextBoxPrintCtrl.RECT rcPage;

			// Token: 0x04000050 RID: 80
			public RichTextBoxPrintCtrl.CHARRANGE chrg;
		}
	}
}
