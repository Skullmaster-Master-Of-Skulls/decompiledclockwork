using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000C2 RID: 194
	public sealed class ScriptSharpSourceMap : ISourceMap, IDisposable
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00040172 File Offset: 0x0003E372
		// (set) Token: 0x06000D4E RID: 3406 RVA: 0x0004017A File Offset: 0x0003E37A
		public string SourceRoot { get; set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00040183 File Offset: 0x0003E383
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x0004018B File Offset: 0x0003E38B
		public bool SafeHeader { get; set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00040194 File Offset: 0x0003E394
		public static string ImplementationName
		{
			get
			{
				return "XML";
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0004019B File Offset: 0x0003E39B
		public string Name
		{
			get
			{
				return ScriptSharpSourceMap.ImplementationName;
			}
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x000401A4 File Offset: 0x0003E3A4
		public ScriptSharpSourceMap(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			XmlWriterSettings settings = new XmlWriterSettings
			{
				CloseOutput = true,
				Indent = true
			};
			this.m_writer = XmlWriter.Create(writer, settings);
			this.m_writer.WriteStartDocument();
			this.m_writer.WriteStartElement("map");
			ScriptSharpSourceMap.JavaScriptSymbol.WriteHeadersTo(this.m_writer);
			this.m_writer.WriteStartElement("scriptFiles");
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0004022C File Offset: 0x0003E42C
		public void StartPackage(string sourcePath, string mapPath)
		{
			this.m_currentPackagePath = sourcePath;
			this.m_mapPath = mapPath;
			this.m_writer.WriteStartElement("scriptFile");
			this.m_writer.WriteAttributeString("path", ScriptSharpSourceMap.MakeRelative(sourcePath, this.m_mapPath) ?? string.Empty);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0004027C File Offset: 0x0003E47C
		public void EndPackage()
		{
			if (this.m_currentPackagePath.IsNullOrWhiteSpace())
			{
				return;
			}
			using (FileStream fileStream = new FileStream(this.m_currentPackagePath, FileMode.Open))
			{
				using (MD5 md = MD5.Create())
				{
					byte[] value = md.ComputeHash(fileStream);
					this.m_writer.WriteStartElement("checksum");
					this.m_writer.WriteAttributeString("value", BitConverter.ToString(value));
					this.m_writer.WriteEndElement();
					this.m_writer.WriteEndElement();
				}
			}
			this.m_currentPackagePath = null;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0004032C File Offset: 0x0003E52C
		public void NewLineInsertedInOutput()
		{
			this.m_columnOffset = 0;
			this.m_lineOffset++;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00040343 File Offset: 0x0003E543
		public void EndOutputRun(int lineNumber, int columnPosition)
		{
			this.m_lineOffset += lineNumber;
			this.m_columnOffset += columnPosition;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00040364 File Offset: 0x0003E564
		public object StartSymbol(AstNode node, int startLine, int startColumn)
		{
			if (node != null && !node.Context.Document.IsGenerated)
			{
				return ScriptSharpSourceMap.JavaScriptSymbol.StartNew(node, startLine + this.m_lineOffset, startColumn + this.m_columnOffset, this.GetSourceFileIndex(node.Context.Document.FileContext));
			}
			return null;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000403B4 File Offset: 0x0003E5B4
		public void MarkSegment(AstNode node, int startLine, int startColumn, string name, Context context)
		{
			if (node == null || string.IsNullOrEmpty(name))
			{
				return;
			}
			FunctionObject functionObject = node as FunctionObject;
			if (functionObject != null && string.CompareOrdinal(name, functionObject.Binding.Name) == 0 && context != functionObject.Context)
			{
				startLine += this.m_lineOffset;
				startColumn += this.m_columnOffset;
				Lookup node2 = new Lookup(context)
				{
					Name = name
				};
				ScriptSharpSourceMap.JavaScriptSymbol javaScriptSymbol = ScriptSharpSourceMap.JavaScriptSymbol.StartNew(node2, startLine, startColumn, this.GetSourceFileIndex(functionObject.Context.Document.FileContext));
				javaScriptSymbol.End(startLine, startColumn + name.Length, name);
				javaScriptSymbol.WriteTo(this.m_writer);
			}
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0004045C File Offset: 0x0003E65C
		public void EndSymbol(object symbol, int endLine, int endColumn, string parentContext)
		{
			if (symbol == null)
			{
				return;
			}
			endLine += this.m_lineOffset;
			endColumn += this.m_columnOffset;
			ScriptSharpSourceMap.JavaScriptSymbol javaScriptSymbol = (ScriptSharpSourceMap.JavaScriptSymbol)symbol;
			javaScriptSymbol.End(endLine, endColumn, parentContext);
			javaScriptSymbol.WriteTo(this.m_writer);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0004049E File Offset: 0x0003E69E
		public void EndFile(TextWriter writer, string newLine)
		{
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x000404A0 File Offset: 0x0003E6A0
		public void Dispose()
		{
			this.EndPackage();
			this.m_writer.WriteEndElement();
			this.m_writer.WriteStartElement("sourceFiles");
			foreach (KeyValuePair<string, int> keyValuePair in this.m_sourceFileIndexMap)
			{
				this.m_writer.WriteStartElement("sourceFile");
				this.m_writer.WriteAttributeString("id", keyValuePair.Value.ToStringInvariant());
				this.m_writer.WriteAttributeString("path", ScriptSharpSourceMap.MakeRelative(keyValuePair.Key, this.m_mapPath) ?? string.Empty);
				this.m_writer.WriteEndElement();
			}
			this.m_writer.WriteEndElement();
			this.m_writer.WriteEndElement();
			this.m_writer.WriteEndDocument();
			this.m_writer.Close();
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0004059C File Offset: 0x0003E79C
		private int GetSourceFileIndex(string fileName)
		{
			int num;
			if (!this.m_sourceFileIndexMap.TryGetValue(fileName, out num))
			{
				num = ++this.currentIndex;
				this.m_sourceFileIndexMap.Add(fileName, num);
			}
			return num;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x000405DC File Offset: 0x0003E7DC
		private static string MakeRelative(string path, string relativeFrom)
		{
			if (!path.IsNullOrWhiteSpace() && !relativeFrom.IsNullOrWhiteSpace())
			{
				try
				{
					Uri uri = new Uri(ScriptSharpSourceMap.Normalize(relativeFrom));
					Uri uri2 = new Uri(ScriptSharpSourceMap.Normalize(path));
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

		// Token: 0x06000D5F RID: 3423 RVA: 0x00040638 File Offset: 0x0003E838
		private static string Normalize(string path)
		{
			if (!Path.IsPathRooted(path))
			{
				return Path.Combine(Environment.CurrentDirectory, path);
			}
			return path;
		}

		// Token: 0x0400052B RID: 1323
		private readonly XmlWriter m_writer;

		// Token: 0x0400052C RID: 1324
		private string m_currentPackagePath;

		// Token: 0x0400052D RID: 1325
		private string m_mapPath;

		// Token: 0x0400052E RID: 1326
		private Dictionary<string, int> m_sourceFileIndexMap = new Dictionary<string, int>();

		// Token: 0x0400052F RID: 1327
		private int currentIndex;

		// Token: 0x04000530 RID: 1328
		private int m_lineOffset;

		// Token: 0x04000531 RID: 1329
		private int m_columnOffset;

		// Token: 0x020000C3 RID: 195
		private class JavaScriptSymbol
		{
			// Token: 0x06000D60 RID: 3424 RVA: 0x0004064F File Offset: 0x0003E84F
			private JavaScriptSymbol()
			{
			}

			// Token: 0x06000D61 RID: 3425 RVA: 0x00040658 File Offset: 0x0003E858
			public static ScriptSharpSourceMap.JavaScriptSymbol StartNew(AstNode node, int startLine, int startColumn, int sourceFileId)
			{
				if (startLine == 2147483647)
				{
					throw new ArgumentOutOfRangeException("startLine");
				}
				if (startColumn == 2147483647)
				{
					throw new ArgumentOutOfRangeException("startColumn");
				}
				return new ScriptSharpSourceMap.JavaScriptSymbol
				{
					m_startLine = startLine + 1,
					m_startColumn = startColumn + 1,
					m_sourceContext = ((node != null) ? node.Context : null),
					m_symbolType = ((node != null) ? node.GetType().Name : "[UNKNOWN]"),
					m_sourceFileId = sourceFileId
				};
			}

			// Token: 0x06000D62 RID: 3426 RVA: 0x000406D8 File Offset: 0x0003E8D8
			public void End(int endLine, int endColumn, string parentFunction)
			{
				if (endLine == 2147483647)
				{
					throw new ArgumentOutOfRangeException("endLine");
				}
				if (endColumn == 2147483647)
				{
					throw new ArgumentOutOfRangeException("endColumn");
				}
				this.m_endLine = endLine + 1;
				this.m_endColumn = endColumn + 1;
				this.m_parentFunction = parentFunction;
			}

			// Token: 0x06000D63 RID: 3427 RVA: 0x00040724 File Offset: 0x0003E924
			public static void WriteHeadersTo(XmlWriter writer)
			{
				if (writer != null)
				{
					writer.WriteStartElement("headers");
					writer.WriteString("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}".FormatInvariant(new object[]
					{
						"DstStartLine",
						"DstStartColumn",
						"DstEndLine",
						"DstEndColumn",
						"SrcStartPosition",
						"SrcEndPosition",
						"SrcStartLine",
						"SrcStartColumn",
						"SrcEndLine",
						"SrcEndColumn",
						"SrcFileId",
						"SymbolType",
						"ParentFunction"
					}));
					writer.WriteEndElement();
				}
			}

			// Token: 0x06000D64 RID: 3428 RVA: 0x000407D0 File Offset: 0x0003E9D0
			public void WriteTo(XmlWriter writer)
			{
				if (writer != null)
				{
					writer.WriteStartElement("s");
					writer.WriteString("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}".FormatInvariant(new object[]
					{
						this.m_startLine,
						this.m_startColumn,
						this.m_endLine,
						this.m_endColumn,
						this.m_sourceContext.StartPosition - this.m_sourceContext.SourceOffsetStart,
						this.m_sourceContext.EndPosition - this.m_sourceContext.SourceOffsetEnd,
						this.m_sourceContext.StartLineNumber,
						this.m_sourceContext.StartColumn,
						this.m_sourceContext.EndLineNumber,
						this.m_sourceContext.EndColumn,
						this.m_sourceFileId,
						this.m_symbolType,
						this.m_parentFunction
					}));
					writer.WriteEndElement();
				}
			}

			// Token: 0x04000534 RID: 1332
			private const string SymbolDataFormat = "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}";

			// Token: 0x04000535 RID: 1333
			private int m_startLine;

			// Token: 0x04000536 RID: 1334
			private int m_endLine;

			// Token: 0x04000537 RID: 1335
			private int m_startColumn;

			// Token: 0x04000538 RID: 1336
			private int m_endColumn;

			// Token: 0x04000539 RID: 1337
			private Context m_sourceContext;

			// Token: 0x0400053A RID: 1338
			private int m_sourceFileId;

			// Token: 0x0400053B RID: 1339
			private string m_symbolType;

			// Token: 0x0400053C RID: 1340
			private string m_parentFunction;
		}
	}
}
