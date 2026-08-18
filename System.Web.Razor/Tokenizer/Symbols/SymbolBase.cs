using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Tokenizer.Symbols
{
	// Token: 0x0200007A RID: 122
	public abstract class SymbolBase<TType> : ISymbol
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x0001520C File Offset: 0x0001340C
		protected SymbolBase(SourceLocation start, string content, TType type, IEnumerable<RazorError> errors)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.Start = start;
			this.Content = content;
			this.Type = type;
			this.Errors = errors;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001525D File Offset: 0x0001345D
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00015265 File Offset: 0x00013465
		public SourceLocation Start { get; private set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0001526E File Offset: 0x0001346E
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x00015276 File Offset: 0x00013476
		public string Content { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001527F File Offset: 0x0001347F
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00015287 File Offset: 0x00013487
		public IEnumerable<RazorError> Errors { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00015290 File Offset: 0x00013490
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00015298 File Offset: 0x00013498
		public TType Type { get; private set; }

		// Token: 0x0600055D RID: 1373 RVA: 0x000152A4 File Offset: 0x000134A4
		public override bool Equals(object obj)
		{
			SymbolBase<TType> symbolBase = obj as SymbolBase<TType>;
			if (symbolBase != null && this.Start.Equals(symbolBase.Start) && string.Equals(this.Content, symbolBase.Content, StringComparison.Ordinal))
			{
				TType type = this.Type;
				return type.Equals(symbolBase.Type);
			}
			return false;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00015306 File Offset: 0x00013506
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add(this.Start).Add(this.Content).Add(this.Type).CombinedHash;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00015340 File Offset: 0x00013540
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} {1} - [{2}]", new object[]
			{
				this.Start,
				this.Type,
				this.Content
			});
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00015389 File Offset: 0x00013589
		public void OffsetStart(SourceLocation documentStart)
		{
			this.Start = documentStart + this.Start;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001539D File Offset: 0x0001359D
		public void ChangeStart(SourceLocation newStart)
		{
			this.Start = newStart;
		}
	}
}
