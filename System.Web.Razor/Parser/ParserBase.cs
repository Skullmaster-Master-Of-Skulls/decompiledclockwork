using System;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Resources;
using System.Web.Razor.Text;

namespace System.Web.Razor.Parser
{
	// Token: 0x0200003A RID: 58
	public abstract class ParserBase
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000792D File Offset: 0x00005B2D
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00007935 File Offset: 0x00005B35
		public virtual ParserContext Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000793E File Offset: 0x00005B3E
		public virtual bool IsMarkupParser
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000225 RID: 549
		protected abstract ParserBase OtherParser { get; }

		// Token: 0x06000226 RID: 550
		public abstract void BuildSpan(SpanBuilder span, SourceLocation start, string content);

		// Token: 0x06000227 RID: 551
		public abstract void ParseBlock();

		// Token: 0x06000228 RID: 552 RVA: 0x00007941 File Offset: 0x00005B41
		public virtual void ParseDocument()
		{
			throw new NotSupportedException(RazorResources.ParserIsNotAMarkupParser);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000794D File Offset: 0x00005B4D
		public virtual void ParseSection(Tuple<string, string> nestingSequences, bool caseSensitive)
		{
			throw new NotSupportedException(RazorResources.ParserIsNotAMarkupParser);
		}

		// Token: 0x0400009D RID: 157
		private ParserContext _context;
	}
}
