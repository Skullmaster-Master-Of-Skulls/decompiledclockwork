using System;
using System.Collections;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	public class RewriteRuleTokenStream : RewriteRuleElementStream
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x0000A741 File Offset: 0x00008941
		public RewriteRuleTokenStream(ITreeAdaptor adaptor, string elementDescription) : base(adaptor, elementDescription)
		{
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000A74B File Offset: 0x0000894B
		public RewriteRuleTokenStream(ITreeAdaptor adaptor, string elementDescription, object oneElement) : base(adaptor, elementDescription, oneElement)
		{
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000A756 File Offset: 0x00008956
		public RewriteRuleTokenStream(ITreeAdaptor adaptor, string elementDescription, IList elements) : base(adaptor, elementDescription, elements)
		{
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000A764 File Offset: 0x00008964
		public virtual object NextNode()
		{
			IToken payload = (IToken)this.NextCore();
			return this.adaptor.Create(payload);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000A789 File Offset: 0x00008989
		public virtual IToken NextToken()
		{
			return (IToken)this.NextCore();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000A796 File Offset: 0x00008996
		protected override object ToTree(object el)
		{
			return el;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000A799 File Offset: 0x00008999
		protected override object Dup(object el)
		{
			throw new NotSupportedException("dup can't be called for a token stream.");
		}
	}
}
