using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using Net.Sgoliver.NRtfTree.Core;

namespace Net.Sgoliver.NRtfTree.Util
{
	// Token: 0x02000009 RID: 9
	public class RtfDocument
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003EBC File Offset: 0x000020BC
		public RtfDocument()
		{
			this.fontTable = new RtfFontTable();
			this.fontTable.AddFont("Arial");
			this.colorTable = new RtfColorTable();
			this.colorTable.AddColor(Color.Black);
			this.currentFormat = null;
			this.currentParFormat = new RtfParFormat();
			this.docFormat = new RtfDocumentFormat();
			this.tree = new RtfTree();
			this.mainGroup = new RtfTreeNode(RtfNodeType.Group);
			this.InitializeTree();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003F4C File Offset: 0x0000214C
		public string Close()
		{
			this.InsertFontTable();
			this.InsertColorTable();
			this.InsertGenerator();
			this.InsertDocSettings();
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "par", false, 0));
			this.tree.RootNode.AppendChild(this.mainGroup);
			return this.tree.GetRtf();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003FAA File Offset: 0x000021AA
		public string GetRtf()
		{
			return this.tree.GetRtf();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003FB7 File Offset: 0x000021B7
		public void AddText(string text, RtfCharFormat format)
		{
			this.UpdateFontTable(format);
			this.UpdateColorTable(format);
			this.UpdateCharFormat(format);
			this.InsertText(text);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003FD5 File Offset: 0x000021D5
		public void AddText(string text)
		{
			this.InsertText(text);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003FE0 File Offset: 0x000021E0
		public void AddNewLine(int n)
		{
			for (int i = 0; i < n; i++)
			{
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "line", false, 0));
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004011 File Offset: 0x00002211
		public void AddNewLine()
		{
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "line", false, 0));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000402B File Offset: 0x0000222B
		public void AddNewParagraph()
		{
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "par", false, 0));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004048 File Offset: 0x00002248
		public void AddNewParagraph(int n)
		{
			for (int i = 0; i < n; i++)
			{
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "par", false, 0));
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004079 File Offset: 0x00002279
		public void AddNewParagraph(RtfParFormat format)
		{
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "par", false, 0));
			this.UpdateParFormat(format);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000409C File Offset: 0x0000229C
		public void AddImage(string path, int width, int height)
		{
			FileStream fileStream = null;
			BinaryReader binaryReader = null;
			try
			{
				FileInfo fileInfo = new FileInfo(path);
				long length = fileInfo.Length;
				fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
				binaryReader = new BinaryReader(fileStream);
				byte[] array = binaryReader.ReadBytes((int)length);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(this.GetHexa(array[i]));
				}
				Image image = Image.FromFile(path);
				RtfTreeNode rtfTreeNode = new RtfTreeNode(RtfNodeType.Group);
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "pict", false, 0));
				string key;
				if (path.ToLower().EndsWith("wmf"))
				{
					key = "emfblip";
				}
				else
				{
					key = "jpegblip";
				}
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, key, false, 0));
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "picw", true, image.Width * 20));
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "pich", true, image.Height * 20));
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "picwgoal", true, width * 20));
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "pichgoal", true, height * 20));
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Text, stringBuilder.ToString(), false, 0));
				this.mainGroup.AppendChild(rtfTreeNode);
			}
			finally
			{
				binaryReader.Close();
				fileStream.Close();
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004218 File Offset: 0x00002418
		public void ResetCharFormat()
		{
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "plain", false, 0));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004232 File Offset: 0x00002432
		public void ResetParFormat()
		{
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "pard", false, 0));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000424C File Offset: 0x0000244C
		public void ResetFormat()
		{
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "pard", false, 0));
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "plain", false, 0));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004280 File Offset: 0x00002480
		public void UpdateDocFormat(RtfDocumentFormat format)
		{
			this.docFormat.MarginL = format.MarginL;
			this.docFormat.MarginR = format.MarginR;
			this.docFormat.MarginT = format.MarginT;
			this.docFormat.MarginB = format.MarginB;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000042D4 File Offset: 0x000024D4
		public void UpdateCharFormat(RtfCharFormat format)
		{
			if (this.currentFormat != null)
			{
				this.SetFormatColor(format.Color);
				this.SetFormatSize(format.Size);
				this.SetFormatFont(format.Font);
				this.SetFormatBold(format.Bold);
				this.SetFormatItalic(format.Italic);
				this.SetFormatUnderline(format.Underline);
				return;
			}
			int num = this.colorTable.IndexOf(format.Color);
			if (num == -1)
			{
				this.colorTable.AddColor(format.Color);
				num = this.colorTable.IndexOf(format.Color);
			}
			int num2 = this.fontTable.IndexOf(format.Font);
			if (num2 == -1)
			{
				this.fontTable.AddFont(format.Font);
				num2 = this.fontTable.IndexOf(format.Font);
			}
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "cf", true, num));
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "fs", true, format.Size * 2));
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "f", true, num2));
			if (format.Bold)
			{
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "b", false, 0));
			}
			if (format.Italic)
			{
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "i", false, 0));
			}
			if (format.Underline)
			{
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "ul", false, 0));
			}
			this.currentFormat = new RtfCharFormat();
			this.currentFormat.Color = format.Color;
			this.currentFormat.Size = format.Size;
			this.currentFormat.Font = format.Font;
			this.currentFormat.Bold = format.Bold;
			this.currentFormat.Italic = format.Italic;
			this.currentFormat.Underline = format.Underline;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000044C4 File Offset: 0x000026C4
		public void UpdateParFormat(RtfParFormat format)
		{
			this.SetAlignment(format.Alignment);
			this.SetLeftIndentation(format.LeftIndentation);
			this.SetRightIndentation(format.RightIndentation);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000044EC File Offset: 0x000026EC
		public void SetAlignment(TextAlignment align)
		{
			if (this.currentParFormat.Alignment != align)
			{
				string key = "";
				switch (align)
				{
				case TextAlignment.Left:
					key = "ql";
					break;
				case TextAlignment.Right:
					key = "qr";
					break;
				case TextAlignment.Centered:
					key = "qc";
					break;
				case TextAlignment.Justified:
					key = "qj";
					break;
				}
				this.currentParFormat.Alignment = align;
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, key, false, 0));
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004565 File Offset: 0x00002765
		public void SetLeftIndentation(float val)
		{
			if (this.currentParFormat.LeftIndentation != val)
			{
				this.currentParFormat.LeftIndentation = val;
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "li", true, this.calcTwips(val)));
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000459F File Offset: 0x0000279F
		public void SetRightIndentation(float val)
		{
			if (this.currentParFormat.RightIndentation != val)
			{
				this.currentParFormat.RightIndentation = val;
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "ri", true, this.calcTwips(val)));
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000045D9 File Offset: 0x000027D9
		public void SetFormatBold(bool val)
		{
			if (this.currentFormat.Bold != val)
			{
				this.currentFormat.Bold = val;
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "b", !val, 0));
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004613 File Offset: 0x00002813
		public void SetFormatItalic(bool val)
		{
			if (this.currentFormat.Italic != val)
			{
				this.currentFormat.Italic = val;
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "i", !val, 0));
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000464D File Offset: 0x0000284D
		public void SetFormatUnderline(bool val)
		{
			if (this.currentFormat.Underline != val)
			{
				this.currentFormat.Underline = val;
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "ul", !val, 0));
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004688 File Offset: 0x00002888
		public void SetFormatColor(Color val)
		{
			if (this.currentFormat.Color.ToArgb() != val.ToArgb())
			{
				int num = this.colorTable.IndexOf(val);
				if (num == -1)
				{
					this.colorTable.AddColor(val);
					num = this.colorTable.IndexOf(val);
				}
				this.currentFormat.Color = val;
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "cf", true, num));
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000046FF File Offset: 0x000028FF
		public void SetFormatSize(int val)
		{
			if (this.currentFormat.Size != val)
			{
				this.currentFormat.Size = val;
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "fs", true, val * 2));
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004738 File Offset: 0x00002938
		public void SetFormatFont(string val)
		{
			if (this.currentFormat.Font != val)
			{
				int num = this.fontTable.IndexOf(val);
				if (num == -1)
				{
					this.fontTable.AddFont(val);
					num = this.fontTable.IndexOf(val);
				}
				this.currentFormat.Font = val;
				this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "f", true, num));
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000047A8 File Offset: 0x000029A8
		private string GetHexa(byte code)
		{
			string text = Convert.ToString(code, 16);
			if (text.Length == 1)
			{
				text = "0" + text;
			}
			return text;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000047D4 File Offset: 0x000029D4
		private void InsertFontTable()
		{
			RtfTreeNode rtfTreeNode = new RtfTreeNode(RtfNodeType.Group);
			rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "fonttbl", false, 0));
			for (int i = 0; i < this.fontTable.Count; i++)
			{
				RtfTreeNode rtfTreeNode2 = new RtfTreeNode(RtfNodeType.Group);
				rtfTreeNode2.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "f", true, i));
				rtfTreeNode2.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "fnil", false, 0));
				rtfTreeNode2.AppendChild(new RtfTreeNode(RtfNodeType.Text, this.fontTable[i] + ";", false, 0));
				rtfTreeNode.AppendChild(rtfTreeNode2);
			}
			this.mainGroup.InsertChild(5, rtfTreeNode);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004878 File Offset: 0x00002A78
		private void InsertColorTable()
		{
			RtfTreeNode rtfTreeNode = new RtfTreeNode(RtfNodeType.Group);
			rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "colortbl", false, 0));
			for (int i = 0; i < this.colorTable.Count; i++)
			{
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "red", true, (int)this.colorTable[i].R));
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "green", true, (int)this.colorTable[i].G));
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "blue", true, (int)this.colorTable[i].B));
				rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Text, ";", false, 0));
			}
			this.mainGroup.InsertChild(6, rtfTreeNode);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004950 File Offset: 0x00002B50
		private void InsertGenerator()
		{
			RtfTreeNode rtfTreeNode = new RtfTreeNode(RtfNodeType.Group);
			rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Control, "*", false, 0));
			rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "generator", false, 0));
			rtfTreeNode.AppendChild(new RtfTreeNode(RtfNodeType.Text, "NRtfTree Library 1.3.0;", false, 0));
			this.mainGroup.InsertChild(7, rtfTreeNode);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000049AC File Offset: 0x00002BAC
		private void InsertText(string text)
		{
			int i = 0;
			while (i < text.Length)
			{
				int num = char.ConvertToUtf32(text, i);
				if (num >= 32 && num < 128)
				{
					StringBuilder stringBuilder = new StringBuilder("");
					while (i < text.Length && num >= 32 && num < 128)
					{
						stringBuilder.Append(text[i]);
						i++;
						if (i < text.Length)
						{
							num = char.ConvertToUtf32(text, i);
						}
					}
					this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Text, stringBuilder.ToString(), false, 0));
				}
				else
				{
					byte[] bytes = this._encoding.GetBytes(new char[]
					{
						text[i]
					});
					this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Control, "'", true, (int)bytes[0]));
					i++;
				}
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004A83 File Offset: 0x00002C83
		private void UpdateFontTable(RtfCharFormat format)
		{
			if (this.fontTable.IndexOf(format.Font) == -1)
			{
				this.fontTable.AddFont(format.Font);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004AAA File Offset: 0x00002CAA
		private void UpdateColorTable(RtfCharFormat format)
		{
			if (this.colorTable.IndexOf(format.Color) == -1)
			{
				this.colorTable.AddColor(format.Color);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004AD4 File Offset: 0x00002CD4
		private void InitializeTree()
		{
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "rtf", true, 1));
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "ansi", false, 0));
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "ansicpg", true, this._encoding.CodePage));
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "deff", true, 0));
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "deflang", true, CultureInfo.CurrentCulture.LCID));
			this.mainGroup.AppendChild(new RtfTreeNode(RtfNodeType.Keyword, "pard", false, 0));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004B84 File Offset: 0x00002D84
		private void InsertDocSettings()
		{
			int index = this.mainGroup.ChildNodes.IndexOf("pard");
			this.mainGroup.InsertChild(index, new RtfTreeNode(RtfNodeType.Keyword, "viewkind", true, 4));
			this.mainGroup.InsertChild(index++, new RtfTreeNode(RtfNodeType.Keyword, "uc", true, 1));
			this.mainGroup.InsertChild(index++, new RtfTreeNode(RtfNodeType.Keyword, "margl", true, this.calcTwips(this.docFormat.MarginL)));
			this.mainGroup.InsertChild(index++, new RtfTreeNode(RtfNodeType.Keyword, "margr", true, this.calcTwips(this.docFormat.MarginR)));
			this.mainGroup.InsertChild(index++, new RtfTreeNode(RtfNodeType.Keyword, "margt", true, this.calcTwips(this.docFormat.MarginT)));
			this.mainGroup.InsertChild(index++, new RtfTreeNode(RtfNodeType.Keyword, "margb", true, this.calcTwips(this.docFormat.MarginB)));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004C91 File Offset: 0x00002E91
		private int calcTwips(float centimeters)
		{
			return (int)(centimeters * 1440f / 2.54f);
		}

		// Token: 0x0400002D RID: 45
		private Encoding _encoding = Encoding.Default;

		// Token: 0x0400002E RID: 46
		private RtfFontTable fontTable;

		// Token: 0x0400002F RID: 47
		private RtfColorTable colorTable;

		// Token: 0x04000030 RID: 48
		private RtfTree tree;

		// Token: 0x04000031 RID: 49
		private RtfTreeNode mainGroup;

		// Token: 0x04000032 RID: 50
		private RtfCharFormat currentFormat;

		// Token: 0x04000033 RID: 51
		private RtfParFormat currentParFormat;

		// Token: 0x04000034 RID: 52
		private RtfDocumentFormat docFormat;
	}
}
