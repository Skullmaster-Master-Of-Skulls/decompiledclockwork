using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000CB RID: 203
	public sealed class V3SourceMap : ISourceMap, IDisposable
	{
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0004121E File Offset: 0x0003F41E
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x00041226 File Offset: 0x0003F426
		public string SourceRoot { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0004122F File Offset: 0x0003F42F
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x00041237 File Offset: 0x0003F437
		public bool SafeHeader { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00041240 File Offset: 0x0003F440
		public static string ImplementationName
		{
			get
			{
				return "V3";
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00041247 File Offset: 0x0003F447
		public string Name
		{
			get
			{
				return V3SourceMap.ImplementationName;
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00041250 File Offset: 0x0003F450
		public V3SourceMap(TextWriter writer)
		{
			this.m_writer = writer;
			this.m_sourceFiles = new HashSet<string>();
			this.m_sourceFileList = new List<string>();
			this.m_names = new HashSet<string>();
			this.m_nameList = new List<string>();
			this.m_segments = new List<V3SourceMap.Segment>();
			this.m_lastDestinationLine = -1;
			this.m_lastDestinationColumn = -1;
			this.m_lastSourceLine = -1;
			this.m_lastSourceColumn = -1;
			this.m_lastFileIndex = -1;
			this.m_lastNameIndex = -1;
			this.m_lineOffset = 0;
			this.m_columnOffset = 0;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x000412D9 File Offset: 0x0003F4D9
		public void StartPackage(string sourcePath, string mapPath)
		{
			this.m_minifiedPath = sourcePath;
			this.m_mapPath = mapPath;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000412E9 File Offset: 0x0003F4E9
		public void EndPackage()
		{
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000412EB File Offset: 0x0003F4EB
		public void NewLineInsertedInOutput()
		{
			this.m_columnOffset = 0;
			this.m_lineOffset++;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00041302 File Offset: 0x0003F502
		public void EndOutputRun(int lineNumber, int columnPosition)
		{
			this.m_lineOffset += lineNumber;
			this.m_columnOffset += columnPosition;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00041320 File Offset: 0x0003F520
		public object StartSymbol(AstNode node, int startLine, int startColumn)
		{
			return null;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0004133C File Offset: 0x0003F53C
		public void MarkSegment(AstNode node, int startLine, int startColumn, string name, Context context)
		{
			if (startLine == 2147483647)
			{
				throw new ArgumentOutOfRangeException("startLine");
			}
			startLine += this.m_lineOffset;
			startColumn += this.m_columnOffset;
			if (!string.IsNullOrEmpty(name) && this.m_names.Add(name))
			{
				this.m_nameList.Add(name);
			}
			this.m_maxMinifiedLine = Math.Max(this.m_maxMinifiedLine, startLine);
			if (context != null && context.Document != null && context.Document.FileContext != null && this.m_sourceFiles.Add(context.Document.FileContext))
			{
				this.m_sourceFileList.Add(V3SourceMap.MakeRelative(context.Document.FileContext, this.m_mapPath));
			}
			V3SourceMap.Segment item = this.CreateSegment(startLine + 1, startColumn, (context == null || context.StartLineNumber < 1) ? -1 : (context.StartLineNumber - 1), (context == null || context.StartColumn < 0) ? -1 : context.StartColumn, context.IfNotNull((Context c) => V3SourceMap.MakeRelative(c.Document.FileContext, this.m_mapPath)), name);
			this.m_segments.Add(item);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0004145B File Offset: 0x0003F65B
		public void EndSymbol(object symbol, int endLine, int endColumn, string parentContext)
		{
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0004145D File Offset: 0x0003F65D
		public void EndFile(TextWriter writer, string newLine)
		{
			if (writer != null && !this.m_mapPath.IsNullOrWhiteSpace())
			{
				writer.Write(newLine);
				writer.Write("//# sourceMappingURL={0}", V3SourceMap.MakeRelative(this.m_mapPath, this.m_minifiedPath));
				writer.Write(newLine);
			}
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0004149C File Offset: 0x0003F69C
		public void Dispose()
		{
			if (this.m_writer != null)
			{
				if (this.SafeHeader)
				{
					this.m_writer.WriteLine(")]}'");
				}
				this.m_writer.WriteLine("{");
				this.WriteProperty("version", 3);
				this.WriteProperty("file", V3SourceMap.MakeRelative(this.m_minifiedPath, this.m_mapPath));
				this.WriteProperty("lineCount", this.m_maxMinifiedLine + 1);
				this.WriteProperty("mappings", this.GenerateMappings(this.m_sourceFileList, this.m_nameList));
				if (!this.SourceRoot.IsNullOrWhiteSpace())
				{
					this.WriteProperty("sourceRoot", this.SourceRoot);
				}
				this.WriteProperty("sources", this.m_sourceFileList);
				this.WriteProperty("names", this.m_nameList);
				this.m_writer.WriteLine();
				this.m_writer.WriteLine("}");
				this.m_writer.Close();
				this.m_writer = null;
			}
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x000415A4 File Offset: 0x0003F7A4
		private V3SourceMap.Segment CreateSegment(int destinationLine, int destinationColumn, int sourceLine, int sourceColumn, string fileName, string symbolName)
		{
			V3SourceMap.Segment result = new V3SourceMap.Segment
			{
				DestinationLine = destinationLine,
				DestinationColumn = ((this.m_lastDestinationColumn < 0 || this.m_lastDestinationLine < destinationLine) ? destinationColumn : (destinationColumn - this.m_lastDestinationColumn)),
				SourceLine = ((fileName == null) ? -1 : ((this.m_lastSourceLine < 0) ? sourceLine : (sourceLine - this.m_lastSourceLine))),
				SourceColumn = ((fileName == null) ? -1 : ((this.m_lastSourceColumn < 0) ? sourceColumn : (sourceColumn - this.m_lastSourceColumn))),
				FileName = fileName,
				SymbolName = symbolName
			};
			this.m_lastDestinationLine = destinationLine;
			this.m_lastDestinationColumn = destinationColumn;
			if (!string.IsNullOrEmpty(fileName))
			{
				this.m_lastSourceLine = sourceLine;
				this.m_lastSourceColumn = sourceColumn;
			}
			return result;
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00041660 File Offset: 0x0003F860
		private string GenerateMappings(IList<string> fileList, IList<string> nameList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			foreach (V3SourceMap.Segment segment in this.m_segments)
			{
				if (num < segment.DestinationLine)
				{
					do
					{
						stringBuilder.Append(';');
					}
					while (++num < segment.DestinationLine);
				}
				else if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(',');
				}
				this.EncodeNumbers(stringBuilder, segment, fileList, nameList);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x000416F8 File Offset: 0x0003F8F8
		private void EncodeNumbers(StringBuilder sb, V3SourceMap.Segment segment, IList<string> files, IList<string> names)
		{
			V3SourceMap.EncodeNumber(sb, segment.DestinationColumn);
			if (!segment.FileName.IsNullOrWhiteSpace())
			{
				int num = files.IndexOf(segment.FileName);
				V3SourceMap.EncodeNumber(sb, (this.m_lastFileIndex < 0) ? num : (num - this.m_lastFileIndex));
				this.m_lastFileIndex = num;
				V3SourceMap.EncodeNumber(sb, segment.SourceLine);
				V3SourceMap.EncodeNumber(sb, segment.SourceColumn);
				if (!string.IsNullOrEmpty(segment.SymbolName))
				{
					num = names.IndexOf(segment.SymbolName);
					V3SourceMap.EncodeNumber(sb, (this.m_lastNameIndex < 0) ? num : (num - this.m_lastNameIndex));
					this.m_lastNameIndex = num;
				}
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000417A4 File Offset: 0x0003F9A4
		private static void EncodeNumber(StringBuilder sb, int value)
		{
			value = ((value < 0) ? (-value << 1 | 1) : (value << 1));
			do
			{
				int num = value & 31;
				value >>= 5;
				if (value > 0)
				{
					num |= 32;
				}
				sb.Append(V3SourceMap.s_base64[num]);
			}
			while (value > 0);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x000417EC File Offset: 0x0003F9EC
		private static string MakeRelative(string path, string relativeFrom)
		{
			if (!path.IsNullOrWhiteSpace() && !relativeFrom.IsNullOrWhiteSpace())
			{
				try
				{
					Uri uri = new Uri(V3SourceMap.Normalize(relativeFrom));
					Uri uri2 = new Uri(V3SourceMap.Normalize(path));
					Uri uri3 = uri.MakeRelativeUri(uri2);
					return uri3.ToString();
				}
				catch (UriFormatException)
				{
				}
				return path;
			}
			return path;
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00041848 File Offset: 0x0003FA48
		private static string Normalize(string path)
		{
			if (!Path.IsPathRooted(path))
			{
				return Path.Combine(Environment.CurrentDirectory, path);
			}
			return path;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0004185F File Offset: 0x0003FA5F
		private void WriteProperty(string name, int number)
		{
			this.WritePropertyStart(name);
			this.m_writer.Write(number.ToStringInvariant());
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00041879 File Offset: 0x0003FA79
		private void WriteProperty(string name, string text)
		{
			this.WritePropertyStart(name);
			this.OutputEscapedString(text ?? string.Empty);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00041894 File Offset: 0x0003FA94
		private void WriteProperty(string name, ICollection<string> collection)
		{
			this.WritePropertyStart(name);
			this.m_writer.Write('[');
			bool flag = true;
			foreach (string text in collection)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.m_writer.Write(',');
				}
				this.OutputEscapedString(text);
			}
			this.m_writer.Write(']');
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00041914 File Offset: 0x0003FB14
		private void WritePropertyStart(string name)
		{
			if (this.m_hasProperty)
			{
				this.m_writer.WriteLine(',');
			}
			this.OutputEscapedString(name);
			this.m_writer.Write(':');
			this.m_hasProperty = true;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00041948 File Offset: 0x0003FB48
		private void OutputEscapedString(string text)
		{
			this.m_writer.Write('"');
			int i = 0;
			while (i < text.Length)
			{
				char c = text[i];
				char c2 = c;
				switch (c2)
				{
				case '\b':
					this.m_writer.Write("\\b");
					break;
				case '\t':
					this.m_writer.Write("\\t");
					break;
				case '\n':
					this.m_writer.Write("\\n");
					break;
				case '\v':
					goto IL_B2;
				case '\f':
					this.m_writer.Write("\\f");
					break;
				case '\r':
					this.m_writer.Write("\\r");
					break;
				default:
					if (c2 != '"')
					{
						goto IL_B2;
					}
					this.m_writer.Write("\\\"");
					break;
				}
				IL_DB:
				i++;
				continue;
				IL_B2:
				if (c < ' ')
				{
					this.m_writer.Write("\\u{0:x4}", (int)c);
					goto IL_DB;
				}
				this.m_writer.Write(c);
				goto IL_DB;
			}
			this.m_writer.Write('"');
		}

		// Token: 0x04000554 RID: 1364
		private string m_minifiedPath;

		// Token: 0x04000555 RID: 1365
		private string m_mapPath;

		// Token: 0x04000556 RID: 1366
		private TextWriter m_writer;

		// Token: 0x04000557 RID: 1367
		private int m_maxMinifiedLine;

		// Token: 0x04000558 RID: 1368
		private bool m_hasProperty;

		// Token: 0x04000559 RID: 1369
		private HashSet<string> m_sourceFiles;

		// Token: 0x0400055A RID: 1370
		private List<string> m_sourceFileList;

		// Token: 0x0400055B RID: 1371
		private HashSet<string> m_names;

		// Token: 0x0400055C RID: 1372
		private List<string> m_nameList;

		// Token: 0x0400055D RID: 1373
		private List<V3SourceMap.Segment> m_segments;

		// Token: 0x0400055E RID: 1374
		private int m_lastDestinationLine;

		// Token: 0x0400055F RID: 1375
		private int m_lastDestinationColumn;

		// Token: 0x04000560 RID: 1376
		private int m_lastSourceLine;

		// Token: 0x04000561 RID: 1377
		private int m_lastSourceColumn;

		// Token: 0x04000562 RID: 1378
		private int m_lastFileIndex;

		// Token: 0x04000563 RID: 1379
		private int m_lastNameIndex;

		// Token: 0x04000564 RID: 1380
		private int m_lineOffset;

		// Token: 0x04000565 RID: 1381
		private int m_columnOffset;

		// Token: 0x04000566 RID: 1382
		private static string s_base64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

		// Token: 0x020000CC RID: 204
		private class Segment
		{
			// Token: 0x1700035D RID: 861
			// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00041A59 File Offset: 0x0003FC59
			// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x00041A61 File Offset: 0x0003FC61
			public int DestinationLine { get; set; }

			// Token: 0x1700035E RID: 862
			// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x00041A6A File Offset: 0x0003FC6A
			// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x00041A72 File Offset: 0x0003FC72
			public int DestinationColumn { get; set; }

			// Token: 0x1700035F RID: 863
			// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x00041A7B File Offset: 0x0003FC7B
			// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x00041A83 File Offset: 0x0003FC83
			public int SourceLine { get; set; }

			// Token: 0x17000360 RID: 864
			// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00041A8C File Offset: 0x0003FC8C
			// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x00041A94 File Offset: 0x0003FC94
			public int SourceColumn { get; set; }

			// Token: 0x17000361 RID: 865
			// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00041A9D File Offset: 0x0003FC9D
			// (set) Token: 0x06000DEB RID: 3563 RVA: 0x00041AA5 File Offset: 0x0003FCA5
			public string FileName { get; set; }

			// Token: 0x17000362 RID: 866
			// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00041AAE File Offset: 0x0003FCAE
			// (set) Token: 0x06000DED RID: 3565 RVA: 0x00041AB6 File Offset: 0x0003FCB6
			public string SymbolName { get; set; }
		}
	}
}
