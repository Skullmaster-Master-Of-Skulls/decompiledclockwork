using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Resources;
using System.Web.Razor.Text;
using System.Web.Razor.Utils;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000020 RID: 32
	public class CodeGeneratorContext
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x000046B8 File Offset: 0x000028B8
		private CodeGeneratorContext()
		{
			this.ExpressionRenderingMode = ExpressionRenderingMode.WriteToOutput;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000046D9 File Offset: 0x000028D9
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000046E1 File Offset: 0x000028E1
		internal ExpressionRenderingMode ExpressionRenderingMode { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000046EA File Offset: 0x000028EA
		// (set) Token: 0x060000EA RID: 234 RVA: 0x000046F2 File Offset: 0x000028F2
		private Action<string, CodeLinePragma> StatementCollector { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000046FB File Offset: 0x000028FB
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00004703 File Offset: 0x00002903
		private Func<CodeWriter> CodeWriterFactory { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000470C File Offset: 0x0000290C
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00004714 File Offset: 0x00002914
		public string SourceFile { get; internal set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000471D File Offset: 0x0000291D
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00004725 File Offset: 0x00002925
		public CodeCompileUnit CompileUnit { get; internal set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000472E File Offset: 0x0000292E
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00004736 File Offset: 0x00002936
		public CodeNamespace Namespace { get; internal set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000473F File Offset: 0x0000293F
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00004747 File Offset: 0x00002947
		public CodeTypeDeclaration GeneratedClass { get; internal set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004750 File Offset: 0x00002950
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00004758 File Offset: 0x00002958
		public RazorEngineHost Host { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00004761 File Offset: 0x00002961
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00004769 File Offset: 0x00002969
		public IDictionary<int, GeneratedCodeMapping> CodeMappings { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004772 File Offset: 0x00002972
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000477A File Offset: 0x0000297A
		public string TargetWriterName { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004783 File Offset: 0x00002983
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000478B File Offset: 0x0000298B
		public CodeMemberMethod TargetMethod { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004794 File Offset: 0x00002994
		public string CurrentBufferedStatement
		{
			get
			{
				if (this._currentBuffer != null)
				{
					return this._currentBuffer.Builder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000047B4 File Offset: 0x000029B4
		public static CodeGeneratorContext Create(RazorEngineHost host, string className, string rootNamespace, string sourceFile, bool shouldGenerateLinePragmas)
		{
			return CodeGeneratorContext.Create(host, null, className, rootNamespace, sourceFile, shouldGenerateLinePragmas);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000047CC File Offset: 0x000029CC
		internal static CodeGeneratorContext Create(RazorEngineHost host, Func<CodeWriter> writerFactory, string className, string rootNamespace, string sourceFile, bool shouldGenerateLinePragmas)
		{
			CodeGeneratorContext codeGeneratorContext = new CodeGeneratorContext
			{
				Host = host,
				CodeWriterFactory = writerFactory,
				SourceFile = (shouldGenerateLinePragmas ? sourceFile : null),
				CompileUnit = new CodeCompileUnit(),
				Namespace = new CodeNamespace(rootNamespace),
				GeneratedClass = new CodeTypeDeclaration(className)
				{
					IsClass = true
				},
				TargetMethod = new CodeMemberMethod
				{
					Name = host.GeneratedClassContext.ExecuteMethodName,
					Attributes = (MemberAttributes)24580
				},
				CodeMappings = new Dictionary<int, GeneratedCodeMapping>()
			};
			codeGeneratorContext.CompileUnit.Namespaces.Add(codeGeneratorContext.Namespace);
			codeGeneratorContext.Namespace.Types.Add(codeGeneratorContext.GeneratedClass);
			codeGeneratorContext.GeneratedClass.Members.Add(codeGeneratorContext.TargetMethod);
			codeGeneratorContext.Namespace.Imports.AddRange((from s in host.NamespaceImports
			select new CodeNamespaceImport(s)).ToArray<CodeNamespaceImport>());
			return codeGeneratorContext;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000048F8 File Offset: 0x00002AF8
		public void AddDesignTimeHelperStatement(CodeSnippetStatement statement)
		{
			if (this._designTimeHelperMethod == null)
			{
				this._designTimeHelperMethod = new CodeMemberMethod
				{
					Name = "__RazorDesignTimeHelpers__",
					Attributes = MemberAttributes.Private
				};
				this._designTimeHelperMethod.Statements.Add(new CodeSnippetStatement(this.BuildCodeString(delegate(CodeWriter cw)
				{
					cw.WriteDisableUnusedFieldWarningPragma();
				})));
				this._designTimeHelperMethod.Statements.Add(new CodeSnippetStatement(this.BuildCodeString(delegate(CodeWriter cw)
				{
					cw.WriteRestoreUnusedFieldWarningPragma();
				})));
				this.GeneratedClass.Members.Insert(0, this._designTimeHelperMethod);
			}
			this._designTimeHelperMethod.Statements.Insert(this._designTimeHelperMethod.Statements.Count - 1, statement);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000049E0 File Offset: 0x00002BE0
		public int AddCodeMapping(SourceLocation sourceLocation, int generatedCodeStart, int generatedCodeLength)
		{
			if (generatedCodeStart == 2147483647)
			{
				throw new ArgumentOutOfRangeException("generatedCodeStart");
			}
			GeneratedCodeMapping value = new GeneratedCodeMapping(sourceLocation.AbsoluteIndex, sourceLocation.LineIndex + 1, sourceLocation.CharacterIndex + 1, generatedCodeStart + 1, generatedCodeLength);
			int num = this._nextDesignTimePragmaId++;
			this.CodeMappings[num] = value;
			return num;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004A44 File Offset: 0x00002C44
		public CodeLinePragma GenerateLinePragma(Span target)
		{
			return this.GenerateLinePragma(target, 0);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004A4E File Offset: 0x00002C4E
		public CodeLinePragma GenerateLinePragma(Span target, int generatedCodeStart)
		{
			return this.GenerateLinePragma(target, generatedCodeStart, target.Content.Length);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004A63 File Offset: 0x00002C63
		public CodeLinePragma GenerateLinePragma(Span target, int generatedCodeStart, int codeLength)
		{
			return this.GenerateLinePragma(target.Start, generatedCodeStart, codeLength);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004A74 File Offset: 0x00002C74
		public CodeLinePragma GenerateLinePragma(SourceLocation start, int generatedCodeStart, int codeLength)
		{
			if (string.IsNullOrEmpty(this.SourceFile))
			{
				return null;
			}
			if (this.Host.DesignTimeMode)
			{
				int lineNumber = this.AddCodeMapping(start, generatedCodeStart, codeLength);
				return new CodeLinePragma(this.SourceFile, lineNumber);
			}
			return new CodeLinePragma(this.SourceFile, start.LineIndex + 1);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public void BufferStatementFragment(Span sourceSpan)
		{
			this.BufferStatementFragment(sourceSpan.Content, sourceSpan);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004AD7 File Offset: 0x00002CD7
		public void BufferStatementFragment(string fragment)
		{
			this.BufferStatementFragment(fragment, null);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public void BufferStatementFragment(string fragment, Span sourceSpan)
		{
			if (sourceSpan != null && this._currentBuffer.LinePragmaSpan == null)
			{
				this._currentBuffer.LinePragmaSpan = sourceSpan;
				int num = this._currentBuffer.Builder.Length;
				if (this._currentBuffer.GeneratedCodeStart != null)
				{
					num = this._currentBuffer.GeneratedCodeStart.Value;
				}
				int num2;
				string text = CodeGeneratorPaddingHelper.Pad(this.Host, this._currentBuffer.Builder.ToString(), sourceSpan, num, out num2);
				this._currentBuffer.GeneratedCodeStart = new int?(num + (text.Length - this._currentBuffer.Builder.Length));
				this._currentBuffer.Builder.Clear();
				this._currentBuffer.Builder.Append(text);
			}
			this._currentBuffer.Builder.Append(fragment);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004BC5 File Offset: 0x00002DC5
		public void MarkStartOfGeneratedCode()
		{
			this._currentBuffer.MarkStart();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004BD2 File Offset: 0x00002DD2
		public void MarkEndOfGeneratedCode()
		{
			this._currentBuffer.MarkEnd();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004BE0 File Offset: 0x00002DE0
		public void FlushBufferedStatement()
		{
			if (this._currentBuffer.Builder.Length > 0)
			{
				CodeLinePragma pragma = null;
				if (this._currentBuffer.LinePragmaSpan != null)
				{
					int num = this._currentBuffer.Builder.Length;
					if (this._currentBuffer.GeneratedCodeStart != null)
					{
						num = this._currentBuffer.GeneratedCodeStart.Value;
					}
					int codeLength = this._currentBuffer.Builder.Length - num;
					if (this._currentBuffer.CodeLength != null)
					{
						codeLength = this._currentBuffer.CodeLength.Value;
					}
					pragma = this.GenerateLinePragma(this._currentBuffer.LinePragmaSpan, num, codeLength);
				}
				this.AddStatement(this._currentBuffer.Builder.ToString(), pragma);
				this._currentBuffer.Reset();
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004CB2 File Offset: 0x00002EB2
		public void AddStatement(string generatedCode)
		{
			this.AddStatement(generatedCode, null);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004CBC File Offset: 0x00002EBC
		public void AddStatement(string body, CodeLinePragma pragma)
		{
			if (this.StatementCollector == null)
			{
				this.TargetMethod.Statements.Add(new CodeSnippetStatement(body)
				{
					LinePragma = pragma
				});
				return;
			}
			this.StatementCollector(body, pragma);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004D00 File Offset: 0x00002F00
		public void EnsureExpressionHelperVariable()
		{
			if (!this._expressionHelperVariableWriten)
			{
				this.GeneratedClass.Members.Insert(0, new CodeMemberField(typeof(object), "__o")
				{
					Attributes = (MemberAttributes)20483
				});
				this._expressionHelperVariableWriten = true;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004D6C File Offset: 0x00002F6C
		public IDisposable ChangeStatementCollector(Action<string, CodeLinePragma> collector)
		{
			Action<string, CodeLinePragma> oldCollector = this.StatementCollector;
			this.StatementCollector = collector;
			return new DisposableAction(delegate()
			{
				this.StatementCollector = oldCollector;
			});
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004E8C File Offset: 0x0000308C
		public void AddContextCall(Span contentSpan, string methodName, bool isLiteral)
		{
			this.AddStatement(this.BuildCodeString(delegate(CodeWriter cw)
			{
				cw.WriteStartMethodInvoke(methodName);
				if (!string.IsNullOrEmpty(this.TargetWriterName))
				{
					cw.WriteSnippet(this.TargetWriterName);
					cw.WriteParameterSeparator();
				}
				cw.WriteStringLiteral(this.Host.InstrumentedSourceFilePath);
				cw.WriteParameterSeparator();
				cw.WriteSnippet(contentSpan.Start.AbsoluteIndex.ToString(CultureInfo.InvariantCulture));
				cw.WriteParameterSeparator();
				cw.WriteSnippet(contentSpan.Content.Length.ToString(CultureInfo.InvariantCulture));
				cw.WriteParameterSeparator();
				cw.WriteSnippet(isLiteral.ToString().ToLowerInvariant());
				cw.WriteEndMethodInvoke();
				cw.WriteEndStatement();
			}));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004ED3 File Offset: 0x000030D3
		internal CodeWriter CreateCodeWriter()
		{
			if (this.CodeWriterFactory == null)
			{
				throw new InvalidOperationException(RazorResources.CreateCodeWriter_NoCodeWriter);
			}
			return this.CodeWriterFactory();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004EF4 File Offset: 0x000030F4
		internal string BuildCodeString(Action<CodeWriter> action)
		{
			string content;
			using (CodeWriter codeWriter = this.CodeWriterFactory())
			{
				action(codeWriter);
				content = codeWriter.Content;
			}
			return content;
		}

		// Token: 0x04000042 RID: 66
		private const string DesignTimeHelperMethodName = "__RazorDesignTimeHelpers__";

		// Token: 0x04000043 RID: 67
		private int _nextDesignTimePragmaId = 1;

		// Token: 0x04000044 RID: 68
		private bool _expressionHelperVariableWriten;

		// Token: 0x04000045 RID: 69
		private CodeMemberMethod _designTimeHelperMethod;

		// Token: 0x04000046 RID: 70
		private CodeGeneratorContext.StatementBuffer _currentBuffer = new CodeGeneratorContext.StatementBuffer();

		// Token: 0x02000021 RID: 33
		private class StatementBuffer
		{
			// Token: 0x06000116 RID: 278 RVA: 0x00004F38 File Offset: 0x00003138
			public void Reset()
			{
				this.Builder.Clear();
				this.GeneratedCodeStart = null;
				this.CodeLength = null;
				this.LinePragmaSpan = null;
			}

			// Token: 0x06000117 RID: 279 RVA: 0x00004F65 File Offset: 0x00003165
			public void MarkStart()
			{
				this.GeneratedCodeStart = new int?(this.Builder.Length);
			}

			// Token: 0x06000118 RID: 280 RVA: 0x00004F80 File Offset: 0x00003180
			public void MarkEnd()
			{
				this.CodeLength = this.Builder.Length - this.GeneratedCodeStart;
			}

			// Token: 0x04000055 RID: 85
			public StringBuilder Builder = new StringBuilder();

			// Token: 0x04000056 RID: 86
			public int? GeneratedCodeStart;

			// Token: 0x04000057 RID: 87
			public int? CodeLength;

			// Token: 0x04000058 RID: 88
			public Span LinePragmaSpan;
		}
	}
}
