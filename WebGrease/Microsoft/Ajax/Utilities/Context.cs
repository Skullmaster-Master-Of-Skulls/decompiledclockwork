using System;
using System.ComponentModel;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000082 RID: 130
	public class Context
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x000249B3 File Offset: 0x00022BB3
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x000249BB File Offset: 0x00022BBB
		public DocumentContext Document { get; private set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x000249C4 File Offset: 0x00022BC4
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x000249CC File Offset: 0x00022BCC
		public int StartLineNumber { get; internal set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x000249D5 File Offset: 0x00022BD5
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x000249DD File Offset: 0x00022BDD
		public int StartLinePosition { get; internal set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x000249E6 File Offset: 0x00022BE6
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x000249EE File Offset: 0x00022BEE
		public int StartPosition { get; internal set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x000249F7 File Offset: 0x00022BF7
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x000249FF File Offset: 0x00022BFF
		public int EndLineNumber { get; internal set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x00024A08 File Offset: 0x00022C08
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x00024A10 File Offset: 0x00022C10
		public int EndLinePosition { get; internal set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00024A19 File Offset: 0x00022C19
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x00024A21 File Offset: 0x00022C21
		public int EndPosition { get; internal set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x00024A2A File Offset: 0x00022C2A
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x00024A32 File Offset: 0x00022C32
		public int SourceOffsetStart { get; internal set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00024A3B File Offset: 0x00022C3B
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x00024A43 File Offset: 0x00022C43
		public int SourceOffsetEnd { get; internal set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00024A4C File Offset: 0x00022C4C
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x00024A54 File Offset: 0x00022C54
		public int OutputLine { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00024A5D File Offset: 0x00022C5D
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x00024A65 File Offset: 0x00022C65
		public int OutputColumn { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00024A6E File Offset: 0x00022C6E
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x00024A76 File Offset: 0x00022C76
		public JSToken Token { get; internal set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00024A7F File Offset: 0x00022C7F
		public int StartColumn
		{
			get
			{
				return this.StartPosition - this.StartLinePosition;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00024A8E File Offset: 0x00022C8E
		public int EndColumn
		{
			get
			{
				return this.EndPosition - this.EndLinePosition;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00024AA0 File Offset: 0x00022CA0
		public bool HasCode
		{
			get
			{
				return !this.Document.IsGenerated && this.EndPosition > this.StartPosition && this.EndPosition <= this.Document.Source.Length && this.EndPosition != this.StartPosition;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00024AF4 File Offset: 0x00022CF4
		public string Code
		{
			get
			{
				if (this.Document.IsGenerated || this.EndPosition <= this.StartPosition || this.EndPosition > this.Document.Source.Length)
				{
					return null;
				}
				return this.Document.Source.Substring(this.StartPosition, this.EndPosition - this.StartPosition);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00024B5C File Offset: 0x00022D5C
		private string ErrorSegment
		{
			get
			{
				string source = this.Document.Source;
				if (this.StartPosition >= source.Length)
				{
					return string.Empty;
				}
				int num = this.EndPosition - this.StartPosition;
				if (this.StartPosition + num <= source.Length)
				{
					return source.Substring(this.StartPosition, num).Trim();
				}
				return source.Substring(this.StartPosition).Trim();
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00024BD4 File Offset: 0x00022DD4
		public Context(DocumentContext document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this.Document = document;
			this.StartLineNumber = 1;
			this.EndLineNumber = 1;
			this.EndPosition = this.Document.Source.IfNotNull((string s) => s.Length);
			this.Token = JSToken.None;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00024C44 File Offset: 0x00022E44
		public Context(DocumentContext document, int startLineNumber, int startLinePosition, int startPosition, int endLineNumber, int endLinePosition, int endPosition, JSToken token) : this(document)
		{
			this.StartLineNumber = startLineNumber;
			this.StartLinePosition = startLinePosition;
			this.StartPosition = startPosition;
			this.EndLineNumber = endLineNumber;
			this.EndLinePosition = endLinePosition;
			this.EndPosition = endPosition;
			this.Token = token;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00024C84 File Offset: 0x00022E84
		public Context Clone()
		{
			return new Context(this.Document)
			{
				StartLineNumber = this.StartLineNumber,
				StartLinePosition = this.StartLinePosition,
				StartPosition = this.StartPosition,
				EndLineNumber = this.EndLineNumber,
				EndLinePosition = this.EndLinePosition,
				EndPosition = this.EndPosition,
				SourceOffsetStart = this.SourceOffsetStart,
				SourceOffsetEnd = this.SourceOffsetEnd,
				Token = this.Token
			};
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00024D0C File Offset: 0x00022F0C
		public Context FlattenToStart()
		{
			Context context = this.Clone();
			context.EndLineNumber = context.StartLineNumber;
			context.EndLinePosition = context.StartLinePosition;
			context.EndPosition = context.StartPosition;
			context.Token = JSToken.None;
			return context;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00024D4C File Offset: 0x00022F4C
		public Context FlattenToEnd()
		{
			Context context = this.Clone();
			context.StartLineNumber = context.EndLineNumber;
			context.StartLinePosition = context.EndLinePosition;
			context.StartPosition = context.EndPosition;
			context.Token = JSToken.None;
			return context;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00024D8C File Offset: 0x00022F8C
		public Context CombineWith(Context other)
		{
			return this.Clone().UpdateWith(other);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00024D9C File Offset: 0x00022F9C
		public Context SplitStart(int length)
		{
			Context context = this.Clone();
			context.EndPosition = (this.StartPosition += length);
			context.EndLineNumber = context.StartLineNumber;
			context.EndLinePosition = context.StartLinePosition;
			return context;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00024DE0 File Offset: 0x00022FE0
		public Context UpdateWith(Context other)
		{
			if (other != null)
			{
				if (other.StartPosition < this.StartPosition)
				{
					this.StartPosition = other.StartPosition;
					this.StartLineNumber = other.StartLineNumber;
					this.StartLinePosition = other.StartLinePosition;
					this.SourceOffsetStart = other.SourceOffsetStart;
				}
				if (other.EndPosition > this.EndPosition)
				{
					this.EndPosition = other.EndPosition;
					this.EndLineNumber = other.EndLineNumber;
					this.EndLinePosition = other.EndLinePosition;
					this.SourceOffsetEnd = other.SourceOffsetEnd;
				}
				if (this.Token != other.Token)
				{
					this.Token = JSToken.None;
				}
			}
			return this;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00024E85 File Offset: 0x00023085
		public bool Is(JSToken token)
		{
			return this.Token == token;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00024E90 File Offset: 0x00023090
		public bool IsOne(params JSToken[] tokens)
		{
			if (tokens != null)
			{
				JSToken token = this.Token;
				for (int i = tokens.Length - 1; i >= 0; i--)
				{
					if (tokens[i] == token)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00024EC0 File Offset: 0x000230C0
		public bool IsNot(JSToken token)
		{
			return this.Token != token;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00024ED0 File Offset: 0x000230D0
		public bool IsNotAny(params JSToken[] tokens)
		{
			if (tokens != null)
			{
				JSToken token = this.Token;
				for (int i = tokens.Length - 1; i >= 0; i--)
				{
					if (tokens[i] == token)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00024F00 File Offset: 0x00023100
		[Localizable(false)]
		public bool Is(string text)
		{
			return text != null && this.EndPosition - this.StartPosition == text.Length && this.EndPosition <= this.Document.Source.Length && this.StartPosition >= 0 && this.StartPosition <= this.EndPosition && string.CompareOrdinal(this.Document.Source, this.StartPosition, text, 0, text.Length) == 0;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00024F78 File Offset: 0x00023178
		internal void ReportUndefined(Lookup lookup)
		{
			UndefinedReference referernce = new UndefinedReference(lookup, this);
			this.Document.ReportUndefined(referernce);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00024F99 File Offset: 0x00023199
		internal void ChangeFileContext(string fileContext)
		{
			if (string.Compare(this.Document.FileContext, fileContext, StringComparison.OrdinalIgnoreCase) != 0)
			{
				this.Document = this.Document.Clone();
				this.Document.FileContext = fileContext;
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00024FCC File Offset: 0x000231CC
		public static string GetErrorString(JSError errorCode)
		{
			return JScript.ResourceManager.GetString(errorCode.ToString(), JScript.Culture);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00024FE8 File Offset: 0x000231E8
		internal void HandleError(JSError errorId, bool forceToError = false)
		{
			if ((errorId != JSError.UndeclaredVariable && errorId != JSError.UndeclaredFunction) || !this.Document.HasAlreadySeenErrorFor(this.Code))
			{
				int severity = Context.GetSeverity(errorId);
				string text = Context.GetErrorString(errorId);
				string errorSegment = this.ErrorSegment;
				if (!errorSegment.IsNullOrWhiteSpace())
				{
					text = text + CommonStrings.ContextSeparator + errorSegment;
				}
				ContextError error = new ContextError
				{
					IsError = (forceToError || severity < 2),
					File = this.Document.FileContext,
					Severity = severity,
					Subcategory = ContextError.GetSubcategory(severity),
					ErrorNumber = (int)errorId,
					ErrorCode = "JS{0}".FormatInvariant(new object[]
					{
						(int)errorId
					}),
					StartLine = this.StartLineNumber,
					StartColumn = this.StartColumn + 1,
					EndLine = this.EndLineNumber,
					EndColumn = this.EndColumn + 1,
					Message = text
				};
				this.Document.HandleError(error);
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00025100 File Offset: 0x00023300
		public bool IsBefore(Context other)
		{
			return other == null || this.StartLineNumber < other.StartLineNumber || (this.StartLineNumber == other.StartLineNumber && this.StartColumn < other.StartColumn);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00025133 File Offset: 0x00023333
		public override string ToString()
		{
			return this.Code;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0002513C File Offset: 0x0002333C
		private static int GetSeverity(JSError errorCode)
		{
			if (errorCode > JSError.UndeclaredFunction)
			{
				if (errorCode <= JSError.DuplicateConstantDeclaration)
				{
					switch (errorCode)
					{
					case JSError.SuspectAssignment:
					case JSError.SuspectSemicolon:
						return 4;
					default:
						switch (errorCode)
						{
						case JSError.StatementBlockExpected:
						case JSError.WithNotRecommended:
						case JSError.ObjectConstructorTakesNoArguments:
						case JSError.NumericMaximum:
						case JSError.NumericMinimum:
						case JSError.OctalLiteralsDeprecated:
						case JSError.FunctionNameMustBeIdentifier:
							return 4;
						case JSError.VariableDefinedNotReferenced:
						case JSError.ArgumentNotReferenced:
						case JSError.FunctionNotReferenced:
							return 3;
						case (JSError)1269:
						case JSError.FunctionExpressionExpected:
						case JSError.JSParserException:
						case JSError.ResourceReferenceMustBeConstant:
						case JSError.ConditionalCompilationTooComplex:
						case JSError.UnterminatedAspNetBlock:
						case JSError.StrictModeNoWith:
						case JSError.StrictModeDuplicateArgument:
						case JSError.StrictModeVariableName:
						case JSError.StrictModeFunctionName:
						case JSError.StrictModeDuplicateProperty:
						case JSError.StrictModeInvalidAssign:
						case JSError.StrictModeInvalidPreOrPost:
						case JSError.StrictModeInvalidDelete:
						case JSError.StrictModeArgumentName:
							return 0;
						case JSError.AmbiguousCatchVar:
						case JSError.NumericOverflow:
						case JSError.AmbiguousNamedFunctionExpression:
						case JSError.StrictComparisonIsAlwaysTrueOrFalse:
							break;
						case JSError.MisplacedFunctionDeclaration:
						case JSError.DuplicateConstantDeclaration:
							return 2;
						default:
							return 0;
						}
						break;
					}
				}
				else
				{
					switch (errorCode)
					{
					case JSError.ObjectLiteralKeyword:
					case JSError.DuplicateLexicalDeclaration:
					case JSError.DuplicateCatch:
					case JSError.ArrayLiteralTrailingComma:
						return 2;
					case JSError.NoEndIfDirective:
					case JSError.NoEndDebugDirective:
					case JSError.BadNumericLiteral:
						return 0;
					case JSError.SuspectEquality:
					case JSError.SemicolonInsertion:
						return 4;
					default:
						switch (errorCode)
						{
						case JSError.NoModuleExport:
						case JSError.NewLineNotAllowed:
							return 4;
						case JSError.NoExpectedFrom:
						case JSError.NoStringLiteral:
						case JSError.NoSpecifierSet:
						case JSError.ArrowCannotBeConstructor:
							return 0;
						case JSError.ExportNotAtModuleLevel:
							break;
						case JSError.HighSurrogate:
						case JSError.LowSurrogate:
							return 2;
						default:
							return 0;
						}
						break;
					}
				}
				return 1;
			}
			if (errorCode == JSError.UnusedLabel)
			{
				return 4;
			}
			if (errorCode == JSError.DuplicateName)
			{
				return 3;
			}
			switch (errorCode)
			{
			case JSError.UndeclaredVariable:
			case JSError.UndeclaredFunction:
				return 3;
			case (JSError)1136:
				return 0;
			case JSError.KeywordUsedAsIdentifier:
				break;
			default:
				return 0;
			}
			return 2;
		}
	}
}
