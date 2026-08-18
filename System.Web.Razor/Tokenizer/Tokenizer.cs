using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Razor.Parser;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Resources;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Razor.Tokenizer
{
	// Token: 0x02000076 RID: 118
	public abstract class Tokenizer<TSymbol, TSymbolType> : StateMachine<TSymbol>, ITokenizer where TSymbol : SymbolBase<TSymbolType>
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x00013946 File Offset: 0x00011B46
		protected Tokenizer(ITextDocument source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.Source = new TextDocumentReader(source);
			this.Buffer = new StringBuilder();
			this.CurrentErrors = new List<RazorError>();
			this.StartSymbol();
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00013984 File Offset: 0x00011B84
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x0001398C File Offset: 0x00011B8C
		public TextDocumentReader Source { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00013995 File Offset: 0x00011B95
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0001399D File Offset: 0x00011B9D
		private protected StringBuilder Buffer { protected get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x000139A6 File Offset: 0x00011BA6
		protected bool EndOfFile
		{
			get
			{
				return this.Source.Peek() == -1;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000139B6 File Offset: 0x00011BB6
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x000139BE File Offset: 0x00011BBE
		private protected IList<RazorError> CurrentErrors { protected get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004F7 RID: 1271
		public abstract TSymbolType RazorCommentStarType { get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004F8 RID: 1272
		public abstract TSymbolType RazorCommentType { get; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004F9 RID: 1273
		public abstract TSymbolType RazorCommentTransitionType { get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x000139C7 File Offset: 0x00011BC7
		protected bool HaveContent
		{
			get
			{
				return this.Buffer.Length > 0;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x000139D8 File Offset: 0x00011BD8
		protected char CurrentCharacter
		{
			get
			{
				int num = this.Source.Peek();
				if (num != -1)
				{
					return (char)num;
				}
				return '\0';
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x000139F9 File Offset: 0x00011BF9
		protected SourceLocation CurrentLocation
		{
			get
			{
				return this.Source.Location;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00013A06 File Offset: 0x00011C06
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00013A0E File Offset: 0x00011C0E
		private protected SourceLocation CurrentStart { protected get; private set; }

		// Token: 0x060004FF RID: 1279 RVA: 0x00013A18 File Offset: 0x00011C18
		public virtual TSymbol NextSymbol()
		{
			this.StartSymbol();
			if (this.EndOfFile)
			{
				return default(TSymbol);
			}
			return this.Turn();
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00013A45 File Offset: 0x00011C45
		public void Reset()
		{
			base.CurrentState = this.StartState;
		}

		// Token: 0x06000501 RID: 1281
		protected abstract TSymbol CreateSymbol(SourceLocation start, string content, TSymbolType type, IEnumerable<RazorError> errors);

		// Token: 0x06000502 RID: 1282 RVA: 0x00013A53 File Offset: 0x00011C53
		protected TSymbol Single(TSymbolType type)
		{
			this.TakeCurrent();
			return this.EndSymbol(type);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00013A68 File Offset: 0x00011C68
		protected bool TakeString(string input, bool caseSensitive)
		{
			int num = 0;
			Func<char, char> func = (char c) => c;
			if (caseSensitive)
			{
				func = new Func<char, char>(char.ToLower);
			}
			while (!this.EndOfFile && num < input.Length && func(this.CurrentCharacter) == func(input[num++]))
			{
				this.TakeCurrent();
			}
			return num == input.Length;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00013AE6 File Offset: 0x00011CE6
		protected void StartSymbol()
		{
			this.Buffer.Clear();
			this.CurrentStart = this.CurrentLocation;
			this.CurrentErrors.Clear();
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00013B0B File Offset: 0x00011D0B
		protected TSymbol EndSymbol(TSymbolType type)
		{
			return this.EndSymbol(this.CurrentStart, type);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00013B1C File Offset: 0x00011D1C
		protected TSymbol EndSymbol(SourceLocation start, TSymbolType type)
		{
			TSymbol result = default(TSymbol);
			if (this.HaveContent)
			{
				result = this.CreateSymbol(start, this.Buffer.ToString(), type, this.CurrentErrors.ToArray<RazorError>());
			}
			this.StartSymbol();
			return result;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00013B60 File Offset: 0x00011D60
		protected void ResumeSymbol(TSymbol previous)
		{
			if (previous.Start.AbsoluteIndex + previous.Content.Length != this.CurrentStart.AbsoluteIndex)
			{
				throw new InvalidOperationException(RazorResources.Tokenizer_CannotResumeSymbolUnlessIsPrevious);
			}
			this.CurrentStart = previous.Start;
			string value = this.Buffer.ToString();
			this.Buffer.Clear();
			this.Buffer.Append(previous.Content);
			this.Buffer.Append(value);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00013C01 File Offset: 0x00011E01
		protected bool TakeUntil(Func<char, bool> predicate)
		{
			while (!this.EndOfFile && !predicate(this.CurrentCharacter))
			{
				this.TakeCurrent();
			}
			return !this.EndOfFile;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00013C50 File Offset: 0x00011E50
		protected Func<char, bool> CharOrWhiteSpace(char character)
		{
			return (char c) => c == character || ParserHelpers.IsWhitespace(c) || ParserHelpers.IsNewLine(c);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00013C76 File Offset: 0x00011E76
		protected void TakeCurrent()
		{
			if (this.EndOfFile)
			{
				return;
			}
			this.Buffer.Append(this.CurrentCharacter);
			this.MoveNext();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00013C99 File Offset: 0x00011E99
		protected void MoveNext()
		{
			this.Source.Read();
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00013CA7 File Offset: 0x00011EA7
		protected bool TakeAll(string expected, bool caseSensitive)
		{
			return this.Lookahead(expected, true, caseSensitive);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00013CB2 File Offset: 0x00011EB2
		protected bool At(string expected, bool caseSensitive)
		{
			return this.Lookahead(expected, false, caseSensitive);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00013CC0 File Offset: 0x00011EC0
		protected char Peek()
		{
			char currentCharacter;
			using (this.Source.BeginLookahead())
			{
				this.MoveNext();
				currentCharacter = this.CurrentCharacter;
			}
			return currentCharacter;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00013D04 File Offset: 0x00011F04
		protected StateMachine<TSymbol>.StateResult AfterRazorCommentTransition()
		{
			if (this.CurrentCharacter != '*')
			{
				return base.Transition(this.StartState);
			}
			this.TakeCurrent();
			return base.Transition(this.EndSymbol(this.RazorCommentStarType), new StateMachine<TSymbol>.State(this.RazorCommentBody));
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00013E14 File Offset: 0x00012014
		protected StateMachine<TSymbol>.StateResult RazorCommentBody()
		{
			this.TakeUntil((char c) => c == '*');
			if (this.CurrentCharacter != '*')
			{
				return base.Transition(this.EndSymbol(this.RazorCommentType), this.StartState);
			}
			char star = this.CurrentCharacter;
			SourceLocation start = this.CurrentLocation;
			this.MoveNext();
			if (this.EndOfFile || this.CurrentCharacter != '@')
			{
				this.Buffer.Append(star);
				return base.Stay();
			}
			StateMachine<TSymbol>.State newState = delegate()
			{
				this.Buffer.Append(star);
				return this.Transition(this.EndSymbol(start, this.RazorCommentStarType), delegate()
				{
					if (this.CurrentCharacter != '@')
					{
						return this.Transition(this.StartState);
					}
					this.TakeCurrent();
					return this.Transition(this.EndSymbol(this.RazorCommentTransitionType), this.StartState);
				});
			};
			if (this.HaveContent)
			{
				return base.Transition(this.EndSymbol(this.RazorCommentType), newState);
			}
			return base.Transition(newState);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00013EFC File Offset: 0x000120FC
		private bool Lookahead(string expected, bool takeIfMatch, bool caseSensitive)
		{
			Func<char, char> func = (char c) => c;
			if (!caseSensitive)
			{
				func = new Func<char, char>(char.ToLowerInvariant);
			}
			if (expected.Length == 0 || func(this.CurrentCharacter) != func(expected[0]))
			{
				return false;
			}
			string value = null;
			if (takeIfMatch)
			{
				this.Buffer.ToString();
			}
			using (LookaheadToken lookaheadToken = this.Source.BeginLookahead())
			{
				for (int i = 0; i < expected.Length; i++)
				{
					if (func(this.CurrentCharacter) != func(expected[i]))
					{
						if (takeIfMatch)
						{
							this.Buffer.Clear();
							this.Buffer.Append(value);
						}
						return false;
					}
					if (takeIfMatch)
					{
						this.TakeCurrent();
					}
					else
					{
						this.MoveNext();
					}
				}
				if (takeIfMatch)
				{
					lookaheadToken.Accept();
				}
			}
			return true;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00014000 File Offset: 0x00012200
		[Conditional("DEBUG")]
		internal void AssertCurrent(char current)
		{
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00014002 File Offset: 0x00012202
		ISymbol ITokenizer.NextSymbol()
		{
			return this.NextSymbol();
		}
	}
}
