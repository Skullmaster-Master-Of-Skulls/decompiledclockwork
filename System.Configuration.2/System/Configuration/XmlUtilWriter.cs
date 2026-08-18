using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020000A4 RID: 164
	internal class XmlUtilWriter
	{
		// Token: 0x0600067D RID: 1661 RVA: 0x0001ED78 File Offset: 0x0001CF78
		internal XmlUtilWriter(TextWriter writer, bool trackPosition)
		{
			this._writer = writer;
			this._trackPosition = trackPosition;
			this._lineNumber = 1;
			this._linePosition = 1;
			this._isLastLineBlank = true;
			if (this._trackPosition)
			{
				this._baseStream = ((StreamWriter)this._writer).BaseStream;
				this._lineStartCheckpoint = this.CreateStreamCheckpoint();
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001EDD8 File Offset: 0x0001CFD8
		internal TextWriter Writer
		{
			get
			{
				return this._writer;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0001EDE0 File Offset: 0x0001CFE0
		internal bool TrackPosition
		{
			get
			{
				return this._trackPosition;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001EDE8 File Offset: 0x0001CFE8
		internal int LineNumber
		{
			get
			{
				return this._lineNumber;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001EDF0 File Offset: 0x0001CFF0
		internal int LinePosition
		{
			get
			{
				return this._linePosition;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001EDF8 File Offset: 0x0001CFF8
		internal bool IsLastLineBlank
		{
			get
			{
				return this._isLastLineBlank;
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001EE00 File Offset: 0x0001D000
		private void UpdatePosition(char ch)
		{
			switch (ch)
			{
			case '\t':
				break;
			case '\n':
				this._lineStartCheckpoint = this.CreateStreamCheckpoint();
				return;
			case '\v':
			case '\f':
				goto IL_5D;
			case '\r':
				this._lineNumber++;
				this._linePosition = 1;
				this._isLastLineBlank = true;
				return;
			default:
				if (ch != ' ')
				{
					goto IL_5D;
				}
				break;
			}
			this._linePosition++;
			return;
			IL_5D:
			this._linePosition++;
			this._isLastLineBlank = false;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001EE80 File Offset: 0x0001D080
		internal int Write(string s)
		{
			if (this._trackPosition)
			{
				foreach (char c in s)
				{
					this._writer.Write(c);
					this.UpdatePosition(c);
				}
			}
			else
			{
				this._writer.Write(s);
			}
			return s.Length;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001EED5 File Offset: 0x0001D0D5
		internal int Write(char ch)
		{
			this._writer.Write(ch);
			if (this._trackPosition)
			{
				this.UpdatePosition(ch);
			}
			return 1;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001EEF3 File Offset: 0x0001D0F3
		internal void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001EF00 File Offset: 0x0001D100
		internal int AppendEscapeTextString(string s)
		{
			return this.AppendEscapeXmlString(s, false, 'A');
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001EF0C File Offset: 0x0001D10C
		internal int AppendEscapeXmlString(string s, bool inAttribute, char quoteChar)
		{
			int num = 0;
			foreach (char c in s)
			{
				bool flag = false;
				string text = null;
				if ((c < ' ' && c != '\t' && c != '\r' && c != '\n') || c > '�')
				{
					flag = true;
				}
				else if (c <= '"')
				{
					if (c != '\n' && c != '\r')
					{
						if (c == '"')
						{
							if (inAttribute && quoteChar == c)
							{
								text = "quot";
							}
						}
					}
					else
					{
						flag = inAttribute;
					}
				}
				else if (c <= '\'')
				{
					if (c != '&')
					{
						if (c == '\'')
						{
							if (inAttribute && quoteChar == c)
							{
								text = "apos";
							}
						}
					}
					else
					{
						text = "amp";
					}
				}
				else if (c != '<')
				{
					if (c == '>')
					{
						text = "gt";
					}
				}
				else
				{
					text = "lt";
				}
				if (flag)
				{
					num += this.AppendCharEntity(c);
				}
				else if (text != null)
				{
					num += this.AppendEntityRef(text);
				}
				else
				{
					num += this.Write(c);
				}
			}
			return num;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001EFFA File Offset: 0x0001D1FA
		internal int AppendEntityRef(string entityRef)
		{
			this.Write('&');
			this.Write(entityRef);
			this.Write(';');
			return entityRef.Length + 2;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001F020 File Offset: 0x0001D220
		internal int AppendCharEntity(char ch)
		{
			int num = (int)ch;
			string text = num.ToString("X", CultureInfo.InvariantCulture);
			this.Write('&');
			this.Write('#');
			this.Write('x');
			this.Write(text);
			this.Write(';');
			return text.Length + 4;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001F075 File Offset: 0x0001D275
		internal int AppendCData(string cdata)
		{
			this.Write("<![CDATA[");
			this.Write(cdata);
			this.Write("]]>");
			return cdata.Length + 12;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001F0A0 File Offset: 0x0001D2A0
		internal int AppendProcessingInstruction(string name, string value)
		{
			this.Write("<?");
			this.Write(name);
			this.AppendSpace();
			this.Write(value);
			this.Write("?>");
			return name.Length + value.Length + 5;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001F0E0 File Offset: 0x0001D2E0
		internal int AppendComment(string comment)
		{
			this.Write("<!--");
			this.Write(comment);
			this.Write("-->");
			return comment.Length + 7;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001F10C File Offset: 0x0001D30C
		internal int AppendAttributeValue(XmlTextReader reader)
		{
			int num = 0;
			char c = reader.QuoteChar;
			if (c != '"' && c != '\'')
			{
				c = '"';
			}
			num += this.Write(c);
			while (reader.ReadAttributeValue())
			{
				if (reader.NodeType == XmlNodeType.Text)
				{
					num += this.AppendEscapeXmlString(reader.Value, true, c);
				}
				else
				{
					num += this.AppendEntityRef(reader.Name);
				}
			}
			return num + this.Write(c);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001F17C File Offset: 0x0001D37C
		internal int AppendRequiredWhiteSpace(int fromLineNumber, int fromLinePosition, int toLineNumber, int toLinePosition)
		{
			int num = this.AppendWhiteSpace(fromLineNumber, fromLinePosition, toLineNumber, toLinePosition);
			if (num == 0)
			{
				num += this.AppendSpace();
			}
			return num;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001F1A4 File Offset: 0x0001D3A4
		internal int AppendWhiteSpace(int fromLineNumber, int fromLinePosition, int toLineNumber, int toLinePosition)
		{
			int num = 0;
			while (fromLineNumber++ < toLineNumber)
			{
				num += this.AppendNewLine();
				fromLinePosition = 1;
			}
			return num + this.AppendSpaces(toLinePosition - fromLinePosition);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001F1D8 File Offset: 0x0001D3D8
		internal int AppendIndent(int linePosition, int indent, int depth, bool newLine)
		{
			int num = 0;
			if (newLine)
			{
				num += this.AppendNewLine();
			}
			int count = linePosition - 1 + indent * depth;
			return num + this.AppendSpaces(count);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001F208 File Offset: 0x0001D408
		internal int AppendSpacesToLinePosition(int linePosition)
		{
			if (linePosition <= 0)
			{
				return 0;
			}
			int num = linePosition - this._linePosition;
			if (num < 0 && this.IsLastLineBlank)
			{
				this.SeekToLineStart();
			}
			return this.AppendSpaces(linePosition - this._linePosition);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001F244 File Offset: 0x0001D444
		internal int AppendNewLine()
		{
			return this.Write("\r\n");
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001F254 File Offset: 0x0001D454
		internal int AppendSpaces(int count)
		{
			int i = count;
			while (i > 0)
			{
				if (i >= 8)
				{
					this.Write(XmlUtilWriter.SPACES_8);
					i -= 8;
				}
				else if (i >= 4)
				{
					this.Write(XmlUtilWriter.SPACES_4);
					i -= 4;
				}
				else
				{
					if (i < 2)
					{
						this.Write(' ');
						break;
					}
					this.Write(XmlUtilWriter.SPACES_2);
					i -= 2;
				}
			}
			if (count <= 0)
			{
				return 0;
			}
			return count;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001F2BD File Offset: 0x0001D4BD
		internal int AppendSpace()
		{
			return this.Write(' ');
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001F2C7 File Offset: 0x0001D4C7
		internal void SeekToLineStart()
		{
			this.RestoreStreamCheckpoint(this._lineStartCheckpoint);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001F2D5 File Offset: 0x0001D4D5
		internal object CreateStreamCheckpoint()
		{
			return new XmlUtilWriter.StreamWriterCheckpoint(this);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001F2E0 File Offset: 0x0001D4E0
		internal void RestoreStreamCheckpoint(object o)
		{
			XmlUtilWriter.StreamWriterCheckpoint streamWriterCheckpoint = (XmlUtilWriter.StreamWriterCheckpoint)o;
			this.Flush();
			this._lineNumber = streamWriterCheckpoint._lineNumber;
			this._linePosition = streamWriterCheckpoint._linePosition;
			this._isLastLineBlank = streamWriterCheckpoint._isLastLineBlank;
			this._baseStream.Seek(streamWriterCheckpoint._streamPosition, SeekOrigin.Begin);
			this._baseStream.SetLength(streamWriterCheckpoint._streamLength);
			this._baseStream.Flush();
		}

		// Token: 0x04000372 RID: 882
		private const char SPACE = ' ';

		// Token: 0x04000373 RID: 883
		private const string NL = "\r\n";

		// Token: 0x04000374 RID: 884
		private static string SPACES_8 = new string(' ', 8);

		// Token: 0x04000375 RID: 885
		private static string SPACES_4 = new string(' ', 4);

		// Token: 0x04000376 RID: 886
		private static string SPACES_2 = new string(' ', 2);

		// Token: 0x04000377 RID: 887
		private TextWriter _writer;

		// Token: 0x04000378 RID: 888
		private Stream _baseStream;

		// Token: 0x04000379 RID: 889
		private bool _trackPosition;

		// Token: 0x0400037A RID: 890
		private int _lineNumber;

		// Token: 0x0400037B RID: 891
		private int _linePosition;

		// Token: 0x0400037C RID: 892
		private bool _isLastLineBlank;

		// Token: 0x0400037D RID: 893
		private object _lineStartCheckpoint;

		// Token: 0x020000D9 RID: 217
		private class StreamWriterCheckpoint
		{
			// Token: 0x06000806 RID: 2054 RVA: 0x00021068 File Offset: 0x0001F268
			internal StreamWriterCheckpoint(XmlUtilWriter writer)
			{
				writer.Flush();
				this._lineNumber = writer._lineNumber;
				this._linePosition = writer._linePosition;
				this._isLastLineBlank = writer._isLastLineBlank;
				writer._baseStream.Flush();
				this._streamPosition = writer._baseStream.Position;
				this._streamLength = writer._baseStream.Length;
			}

			// Token: 0x040004B3 RID: 1203
			internal int _lineNumber;

			// Token: 0x040004B4 RID: 1204
			internal int _linePosition;

			// Token: 0x040004B5 RID: 1205
			internal bool _isLastLineBlank;

			// Token: 0x040004B6 RID: 1206
			internal long _streamLength;

			// Token: 0x040004B7 RID: 1207
			internal long _streamPosition;
		}
	}
}
