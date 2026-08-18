using System;
using System.Collections;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	public class RewriteRuleNodeStream : RewriteRuleElementStream
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x0000A652 File Offset: 0x00008852
		public RewriteRuleNodeStream(ITreeAdaptor adaptor, string elementDescription) : base(adaptor, elementDescription)
		{
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000A65C File Offset: 0x0000885C
		public RewriteRuleNodeStream(ITreeAdaptor adaptor, string elementDescription, object oneElement) : base(adaptor, elementDescription, oneElement)
		{
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000A667 File Offset: 0x00008867
		public RewriteRuleNodeStream(ITreeAdaptor adaptor, string elementDescription, IList elements) : base(adaptor, elementDescription, elements)
		{
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000A672 File Offset: 0x00008872
		public virtual object NextNode()
		{
			return this.NextCore();
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000A67A File Offset: 0x0000887A
		protected override object ToTree(object el)
		{
			return this.adaptor.DupNode(el);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000A688 File Offset: 0x00008888
		protected override object Dup(object el)
		{
			throw new NotSupportedException("dup can't be called for a node stream.");
		}
	}
}
