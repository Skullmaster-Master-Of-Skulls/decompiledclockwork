using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000083 RID: 131
	public sealed class ContinueNode : AstNode
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0002529E File Offset: 0x0002349E
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x000252A6 File Offset: 0x000234A6
		public string Label { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x000252AF File Offset: 0x000234AF
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x000252B7 File Offset: 0x000234B7
		public Context LabelContext { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x000252C0 File Offset: 0x000234C0
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x000252C8 File Offset: 0x000234C8
		public LabelInfo LabelInfo { get; set; }

		// Token: 0x0600082B RID: 2091 RVA: 0x000252D1 File Offset: 0x000234D1
		public ContinueNode(Context context) : base(context)
		{
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000252DA File Offset: 0x000234DA
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}
	}
}
