using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer.Symbols;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Editor
{
	// Token: 0x02000005 RID: 5
	public class SpanEditHandler
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002335 File Offset: 0x00000535
		public SpanEditHandler(Func<string, IEnumerable<ISymbol>> tokenizer) : this(tokenizer, AcceptedCharacters.Any)
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000233F File Offset: 0x0000053F
		public SpanEditHandler(Func<string, IEnumerable<ISymbol>> tokenizer, AcceptedCharacters accepted)
		{
			this.AcceptedCharacters = accepted;
			this.Tokenizer = tokenizer;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002355 File Offset: 0x00000555
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000235D File Offset: 0x0000055D
		public AcceptedCharacters AcceptedCharacters { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002366 File Offset: 0x00000566
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000236E File Offset: 0x0000056E
		public EditorHints EditorHints { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002377 File Offset: 0x00000577
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000237F File Offset: 0x0000057F
		public Func<string, IEnumerable<ISymbol>> Tokenizer { get; set; }

		// Token: 0x06000023 RID: 35 RVA: 0x0000238F File Offset: 0x0000058F
		public static SpanEditHandler CreateDefault()
		{
			return SpanEditHandler.CreateDefault((string s) => Enumerable.Empty<ISymbol>());
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023B3 File Offset: 0x000005B3
		public static SpanEditHandler CreateDefault(Func<string, IEnumerable<ISymbol>> tokenizer)
		{
			return new SpanEditHandler(tokenizer);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023BB File Offset: 0x000005BB
		public virtual EditResult ApplyChange(Span target, TextChange change)
		{
			return this.ApplyChange(target, change, false);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023C8 File Offset: 0x000005C8
		public virtual EditResult ApplyChange(Span target, TextChange change, bool force)
		{
			PartialParseResult partialParseResult = PartialParseResult.Accepted;
			TextChange normalizedChange = change.Normalize();
			if (!force)
			{
				partialParseResult = this.CanAcceptChange(target, normalizedChange);
			}
			if (partialParseResult.HasFlag(PartialParseResult.Accepted))
			{
				return new EditResult(partialParseResult, this.UpdateSpan(target, normalizedChange));
			}
			return new EditResult(partialParseResult, new SpanBuilder(target));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000241C File Offset: 0x0000061C
		public virtual bool OwnsChange(Span target, TextChange change)
		{
			int num = target.Start.AbsoluteIndex + target.Length;
			int num2 = change.OldPosition + change.OldLength;
			return change.OldPosition >= target.Start.AbsoluteIndex && (num2 < num || (num2 == num && this.AcceptedCharacters != AcceptedCharacters.None));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002480 File Offset: 0x00000680
		protected virtual PartialParseResult CanAcceptChange(Span target, TextChange normalizedChange)
		{
			return PartialParseResult.Rejected;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002484 File Offset: 0x00000684
		protected virtual SpanBuilder UpdateSpan(Span target, TextChange normalizedChange)
		{
			string text = normalizedChange.ApplyChange(target);
			SpanBuilder spanBuilder = new SpanBuilder(target);
			spanBuilder.ClearSymbols();
			foreach (ISymbol symbol in this.Tokenizer(text))
			{
				symbol.OffsetStart(target.Start);
				spanBuilder.Accept(symbol);
			}
			if (target.Next != null)
			{
				SourceLocation newStart = SourceLocationTracker.CalculateNewLocation(target.Start, text);
				target.Next.ChangeStart(newStart);
			}
			return spanBuilder;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002528 File Offset: 0x00000728
		protected internal static bool IsAtEndOfFirstLine(Span target, TextChange change)
		{
			int num = target.Content.IndexOfAny(new char[]
			{
				'\r',
				'\n',
				'\u2028',
				'\u2029'
			});
			return num == -1 || change.OldPosition - target.Start.AbsoluteIndex <= num;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002574 File Offset: 0x00000774
		protected internal static bool IsEndInsertion(Span target, TextChange change)
		{
			return change.IsInsert && SpanEditHandler.IsAtEndOfSpan(target, change);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002588 File Offset: 0x00000788
		protected internal static bool IsEndDeletion(Span target, TextChange change)
		{
			return change.IsDelete && SpanEditHandler.IsAtEndOfSpan(target, change);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000259C File Offset: 0x0000079C
		protected internal static bool IsEndReplace(Span target, TextChange change)
		{
			return change.IsReplace && SpanEditHandler.IsAtEndOfSpan(target, change);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025B0 File Offset: 0x000007B0
		protected internal static bool IsAtEndOfSpan(Span target, TextChange change)
		{
			return change.OldPosition + change.OldLength == target.Start.AbsoluteIndex + target.Length;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000025E4 File Offset: 0x000007E4
		protected internal static string GetOldText(Span target, TextChange change)
		{
			return target.Content.Substring(change.OldPosition - target.Start.AbsoluteIndex, change.OldLength);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000261C File Offset: 0x0000081C
		internal static bool IsAdjacentOnRight(Span target, Span other)
		{
			return target.Start.AbsoluteIndex < other.Start.AbsoluteIndex && target.Start.AbsoluteIndex + target.Length == other.Start.AbsoluteIndex;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002670 File Offset: 0x00000870
		internal static bool IsAdjacentOnLeft(Span target, Span other)
		{
			return other.Start.AbsoluteIndex < target.Start.AbsoluteIndex && other.Start.AbsoluteIndex + other.Length == target.Start.AbsoluteIndex;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026C4 File Offset: 0x000008C4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.GetType().Name,
				";Accepts:",
				this.AcceptedCharacters,
				(this.EditorHints == EditorHints.None) ? string.Empty : (";Hints: " + this.EditorHints.ToString())
			});
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002730 File Offset: 0x00000930
		public override bool Equals(object obj)
		{
			SpanEditHandler spanEditHandler = obj as SpanEditHandler;
			return spanEditHandler != null && this.AcceptedCharacters == spanEditHandler.AcceptedCharacters && this.EditorHints == spanEditHandler.EditorHints;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002765 File Offset: 0x00000965
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add(this.AcceptedCharacters).Add(this.EditorHints).CombinedHash;
		}
	}
}
