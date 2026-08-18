using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Razor.Parser;

namespace System.Web.Razor.Text
{
	// Token: 0x02000065 RID: 101
	internal class LineTrackingStringBuffer
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x00012378 File Offset: 0x00010578
		public LineTrackingStringBuffer()
		{
			this._endLine = new LineTrackingStringBuffer.TextLine(0, 0);
			this._lines = new List<LineTrackingStringBuffer.TextLine>
			{
				this._endLine
			};
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x000123B1 File Offset: 0x000105B1
		public int Length
		{
			get
			{
				return this._endLine.End;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000123BE File Offset: 0x000105BE
		public SourceLocation EndLocation
		{
			get
			{
				return new SourceLocation(this.Length, this._lines.Count - 1, this._lines[this._lines.Count - 1].Length);
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000123F8 File Offset: 0x000105F8
		public void Append(string content)
		{
			for (int i = 0; i < content.Length; i++)
			{
				this.AppendCore(content[i]);
				if ((content[i] == '\r' && (i + 1 == content.Length || content[i + 1] != '\n')) || (content[i] != '\r' && ParserHelpers.IsNewLine(content[i])))
				{
					this.PushNewLine();
				}
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00012468 File Offset: 0x00010668
		public LineTrackingStringBuffer.CharacterReference CharAt(int absoluteIndex)
		{
			LineTrackingStringBuffer.TextLine textLine = this.FindLine(absoluteIndex);
			if (textLine == null)
			{
				throw new ArgumentOutOfRangeException("absoluteIndex");
			}
			int num = absoluteIndex - textLine.Start;
			return new LineTrackingStringBuffer.CharacterReference(textLine.Content[num], new SourceLocation(absoluteIndex, textLine.Index, num));
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000124B2 File Offset: 0x000106B2
		private void PushNewLine()
		{
			this._endLine = new LineTrackingStringBuffer.TextLine(this._endLine.End, this._endLine.Index + 1);
			this._lines.Add(this._endLine);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000124E8 File Offset: 0x000106E8
		private void AppendCore(char chr)
		{
			this._lines[this._lines.Count - 1].Content.Append(chr);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00012510 File Offset: 0x00010710
		private LineTrackingStringBuffer.TextLine FindLine(int absoluteIndex)
		{
			LineTrackingStringBuffer.TextLine textLine = null;
			if (this._currentLine != null)
			{
				if (this._currentLine.Contains(absoluteIndex))
				{
					textLine = this._currentLine;
				}
				else if (absoluteIndex > this._currentLine.Index && this._currentLine.Index + 1 < this._lines.Count)
				{
					textLine = this.ScanLines(absoluteIndex, this._currentLine.Index);
				}
			}
			if (textLine == null)
			{
				textLine = this.ScanLines(absoluteIndex, 0);
			}
			this._currentLine = textLine;
			return textLine;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00012590 File Offset: 0x00010790
		private LineTrackingStringBuffer.TextLine ScanLines(int absoluteIndex, int startPos)
		{
			for (int i = 0; i < this._lines.Count; i++)
			{
				int index = (i + startPos) % this._lines.Count;
				if (this._lines[index].Contains(absoluteIndex))
				{
					return this._lines[index];
				}
			}
			return null;
		}

		// Token: 0x0400014B RID: 331
		private LineTrackingStringBuffer.TextLine _currentLine;

		// Token: 0x0400014C RID: 332
		private LineTrackingStringBuffer.TextLine _endLine;

		// Token: 0x0400014D RID: 333
		private IList<LineTrackingStringBuffer.TextLine> _lines;

		// Token: 0x02000066 RID: 102
		internal class CharacterReference
		{
			// Token: 0x060004A7 RID: 1191 RVA: 0x000125E5 File Offset: 0x000107E5
			public CharacterReference(char character, SourceLocation location)
			{
				this.Character = character;
				this.Location = location;
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000125FB File Offset: 0x000107FB
			// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00012603 File Offset: 0x00010803
			public char Character { get; private set; }

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001260C File Offset: 0x0001080C
			// (set) Token: 0x060004AB RID: 1195 RVA: 0x00012614 File Offset: 0x00010814
			public SourceLocation Location { get; private set; }
		}

		// Token: 0x02000067 RID: 103
		private class TextLine
		{
			// Token: 0x060004AC RID: 1196 RVA: 0x0001261D File Offset: 0x0001081D
			public TextLine(int start, int index)
			{
				this.Start = start;
				this.Index = index;
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x060004AD RID: 1197 RVA: 0x0001263E File Offset: 0x0001083E
			public StringBuilder Content
			{
				get
				{
					return this._content;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x060004AE RID: 1198 RVA: 0x00012646 File Offset: 0x00010846
			public int Length
			{
				get
				{
					return this.Content.Length;
				}
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x060004AF RID: 1199 RVA: 0x00012653 File Offset: 0x00010853
			// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0001265B File Offset: 0x0001085B
			public int Start { get; set; }

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00012664 File Offset: 0x00010864
			// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0001266C File Offset: 0x0001086C
			public int Index { get; set; }

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00012675 File Offset: 0x00010875
			public int End
			{
				get
				{
					return this.Start + this.Length;
				}
			}

			// Token: 0x060004B4 RID: 1204 RVA: 0x00012684 File Offset: 0x00010884
			public bool Contains(int index)
			{
				return index < this.End && index >= this.Start;
			}

			// Token: 0x04000150 RID: 336
			private StringBuilder _content = new StringBuilder();
		}
	}
}
