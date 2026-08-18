using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Razor.Parser;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer.Symbols;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Editor
{
	// Token: 0x0200001C RID: 28
	public class ImplicitExpressionEditHandler : SpanEditHandler
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00003CEF File Offset: 0x00001EEF
		public ImplicitExpressionEditHandler(Func<string, IEnumerable<ISymbol>> tokenizer, ISet<string> keywords, bool acceptTrailingDot) : base(tokenizer)
		{
			this.Initialize(keywords, acceptTrailingDot);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003D00 File Offset: 0x00001F00
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003D08 File Offset: 0x00001F08
		public bool AcceptTrailingDot { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003D11 File Offset: 0x00001F11
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00003D19 File Offset: 0x00001F19
		public ISet<string> Keywords { get; private set; }

		// Token: 0x060000BE RID: 190 RVA: 0x00003D24 File Offset: 0x00001F24
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0};ImplicitExpression[{1}];K{2}", new object[]
			{
				base.ToString(),
				this.AcceptTrailingDot ? "ATD" : "RTD",
				this.Keywords.Count
			});
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003D7C File Offset: 0x00001F7C
		public override bool Equals(object obj)
		{
			ImplicitExpressionEditHandler implicitExpressionEditHandler = obj as ImplicitExpressionEditHandler;
			return implicitExpressionEditHandler != null && base.Equals(implicitExpressionEditHandler) && this.Keywords.SetEquals(implicitExpressionEditHandler.Keywords) && this.AcceptTrailingDot == implicitExpressionEditHandler.AcceptTrailingDot;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003DBF File Offset: 0x00001FBF
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add(base.GetHashCode()).Add(this.AcceptTrailingDot).Add(this.Keywords).CombinedHash;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003DF4 File Offset: 0x00001FF4
		protected override PartialParseResult CanAcceptChange(Span target, TextChange normalizedChange)
		{
			if (base.AcceptedCharacters == AcceptedCharacters.Any)
			{
				return PartialParseResult.Rejected;
			}
			if (ImplicitExpressionEditHandler.IsDotlessCommitInsertion(target, normalizedChange))
			{
				return this.HandleDotlessCommitInsertion(target);
			}
			if (ImplicitExpressionEditHandler.IsAcceptableReplace(target, normalizedChange))
			{
				return this.HandleReplacement(target, normalizedChange);
			}
			int num = normalizedChange.OldPosition - target.Start.AbsoluteIndex;
			char? c = null;
			if (num > 0 && target.Content.Length > 0)
			{
				c = new char?(target.Content[num - 1]);
			}
			char? c2 = c;
			int? num2 = (c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null;
			if (num2 == null)
			{
				return PartialParseResult.Rejected;
			}
			if (ImplicitExpressionEditHandler.IsAcceptableInsertion(target, normalizedChange))
			{
				return this.HandleInsertion(target, c.Value, normalizedChange);
			}
			if (ImplicitExpressionEditHandler.IsAcceptableDeletion(target, normalizedChange))
			{
				return this.HandleDeletion(target, c.Value, normalizedChange);
			}
			return PartialParseResult.Rejected;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003ED8 File Offset: 0x000020D8
		private void Initialize(ISet<string> keywords, bool acceptTrailingDot)
		{
			this.Keywords = (keywords ?? new HashSet<string>());
			this.AcceptTrailingDot = acceptTrailingDot;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003EF1 File Offset: 0x000020F1
		private static bool IsDotlessCommitInsertion(Span target, TextChange change)
		{
			return ImplicitExpressionEditHandler.IsNewDotlessCommitInsertion(target, change) || ImplicitExpressionEditHandler.IsSecondaryDotlessCommitInsertion(target, change);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003F08 File Offset: 0x00002108
		private static bool IsNewDotlessCommitInsertion(Span target, TextChange change)
		{
			return !SpanEditHandler.IsAtEndOfSpan(target, change) && change.NewPosition > 0 && change.NewLength > 0 && target.Content.Last<char>() == '.' && ParserHelpers.IsIdentifier(change.NewText, false) && (change.OldLength == 0 || ParserHelpers.IsIdentifier(change.OldText, false));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003F6C File Offset: 0x0000216C
		private static bool IsSecondaryDotlessCommitInsertion(Span target, TextChange change)
		{
			return change.NewLength == 1 && !string.IsNullOrEmpty(target.Content) && target.Content.Last<char>() == '.' && change.NewText == "." && change.OldLength == 0;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003FBE File Offset: 0x000021BE
		private static bool IsAcceptableReplace(Span target, TextChange change)
		{
			return SpanEditHandler.IsEndReplace(target, change) || (change.IsReplace && ImplicitExpressionEditHandler.RemainingIsWhitespace(target, change));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003FDD File Offset: 0x000021DD
		private static bool IsAcceptableDeletion(Span target, TextChange change)
		{
			return SpanEditHandler.IsEndDeletion(target, change) || (change.IsDelete && ImplicitExpressionEditHandler.RemainingIsWhitespace(target, change));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003FFC File Offset: 0x000021FC
		private static bool IsAcceptableInsertion(Span target, TextChange change)
		{
			return change.IsInsert && (ImplicitExpressionEditHandler.IsAcceptableEndInsertion(target, change) || ImplicitExpressionEditHandler.IsAcceptableInnerInsertion(target, change));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000401B File Offset: 0x0000221B
		private static bool IsAcceptableEndInsertion(Span target, TextChange change)
		{
			return SpanEditHandler.IsAtEndOfSpan(target, change) || ImplicitExpressionEditHandler.RemainingIsWhitespace(target, change);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000402F File Offset: 0x0000222F
		private static bool IsAcceptableInnerInsertion(Span target, TextChange change)
		{
			return change.NewPosition > 0 && change.NewText == ".";
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004050 File Offset: 0x00002250
		private static bool RemainingIsWhitespace(Span target, TextChange change)
		{
			int startIndex = change.OldPosition - target.Start.AbsoluteIndex + change.OldLength;
			return string.IsNullOrWhiteSpace(target.Content.Substring(startIndex));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004090 File Offset: 0x00002290
		private PartialParseResult HandleDotlessCommitInsertion(Span target)
		{
			PartialParseResult partialParseResult = PartialParseResult.Accepted;
			if (!this.AcceptTrailingDot && target.Content.LastOrDefault<char>() == '.')
			{
				partialParseResult |= PartialParseResult.Provisional;
			}
			return partialParseResult;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000040BC File Offset: 0x000022BC
		private PartialParseResult HandleReplacement(Span target, TextChange change)
		{
			string oldText = SpanEditHandler.GetOldText(target, change);
			PartialParseResult partialParseResult = PartialParseResult.Rejected;
			if (ImplicitExpressionEditHandler.EndsWithDot(oldText) && ImplicitExpressionEditHandler.EndsWithDot(change.NewText))
			{
				partialParseResult = PartialParseResult.Accepted;
				if (!this.AcceptTrailingDot)
				{
					partialParseResult |= PartialParseResult.Provisional;
				}
			}
			return partialParseResult;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000040F8 File Offset: 0x000022F8
		private PartialParseResult HandleDeletion(Span target, char previousChar, TextChange change)
		{
			if (previousChar == '.')
			{
				return this.TryAcceptChange(target, change, PartialParseResult.Accepted | PartialParseResult.Provisional);
			}
			if (ParserHelpers.IsIdentifierPart(previousChar))
			{
				return this.TryAcceptChange(target, change, PartialParseResult.Accepted);
			}
			return PartialParseResult.Rejected;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000411C File Offset: 0x0000231C
		private PartialParseResult HandleInsertion(Span target, char previousChar, TextChange change)
		{
			if (previousChar == '.')
			{
				return this.HandleInsertionAfterDot(target, change);
			}
			if (ParserHelpers.IsIdentifierPart(previousChar) || previousChar == ')' || previousChar == ']')
			{
				return this.HandleInsertionAfterIdPart(target, change);
			}
			return PartialParseResult.Rejected;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004148 File Offset: 0x00002348
		private PartialParseResult HandleInsertionAfterIdPart(Span target, TextChange change)
		{
			if (ParserHelpers.IsIdentifier(change.NewText, false))
			{
				return this.TryAcceptChange(target, change, PartialParseResult.Accepted);
			}
			if (ImplicitExpressionEditHandler.EndsWithDot(change.NewText))
			{
				PartialParseResult partialParseResult = PartialParseResult.Accepted;
				if (!this.AcceptTrailingDot)
				{
					partialParseResult |= PartialParseResult.Provisional;
				}
				return this.TryAcceptChange(target, change, partialParseResult);
			}
			return PartialParseResult.Rejected;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004198 File Offset: 0x00002398
		private static bool EndsWithDot(string content)
		{
			return (content.Length == 1 && content[0] == '.') || (content[content.Length - 1] == '.' && content.Take(content.Length - 1).All(new Func<char, bool>(ParserHelpers.IsIdentifierPart)));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000041EE File Offset: 0x000023EE
		private PartialParseResult HandleInsertionAfterDot(Span target, TextChange change)
		{
			if (ParserHelpers.IsIdentifier(change.NewText) || change.NewText == ".")
			{
				return this.TryAcceptChange(target, change, PartialParseResult.Accepted);
			}
			return PartialParseResult.Rejected;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000421C File Offset: 0x0000241C
		private PartialParseResult TryAcceptChange(Span target, TextChange change, PartialParseResult acceptResult = PartialParseResult.Accepted)
		{
			string newContent = change.ApplyChange(target);
			if (this.StartsWithKeyword(newContent))
			{
				return PartialParseResult.Rejected | PartialParseResult.SpanContextChanged;
			}
			return acceptResult;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004240 File Offset: 0x00002440
		private bool StartsWithKeyword(string newContent)
		{
			bool result;
			using (StringReader stringReader = new StringReader(newContent))
			{
				result = this.Keywords.Contains(stringReader.ReadWhile(new Predicate<char>(ParserHelpers.IsIdentifierPart)));
			}
			return result;
		}
	}
}
