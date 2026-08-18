using System;
using System.Collections;

namespace Antlr.Runtime.Tree
{
	// Token: 0x02000054 RID: 84
	[Serializable]
	public class RewriteRuleSubtreeStream : RewriteRuleElementStream
	{
		// Token: 0x060003DD RID: 989 RVA: 0x0000A694 File Offset: 0x00008894
		public RewriteRuleSubtreeStream(ITreeAdaptor adaptor, string elementDescription) : base(adaptor, elementDescription)
		{
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000A69E File Offset: 0x0000889E
		public RewriteRuleSubtreeStream(ITreeAdaptor adaptor, string elementDescription, object oneElement) : base(adaptor, elementDescription, oneElement)
		{
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000A6A9 File Offset: 0x000088A9
		public RewriteRuleSubtreeStream(ITreeAdaptor adaptor, string elementDescription, IList elements) : base(adaptor, elementDescription, elements)
		{
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000A6B4 File Offset: 0x000088B4
		public virtual object NextNode()
		{
			int count = this.Count;
			if (this.dirty || (this.cursor >= count && count == 1))
			{
				object treeNode = this.NextCore();
				return this.adaptor.DupNode(treeNode);
			}
			object obj = this.NextCore();
			while (this.adaptor.IsNil(obj) && this.adaptor.GetChildCount(obj) == 1)
			{
				obj = this.adaptor.GetChild(obj, 0);
			}
			return this.adaptor.DupNode(obj);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000A733 File Offset: 0x00008933
		protected override object Dup(object el)
		{
			return this.adaptor.DupTree(el);
		}
	}
}
