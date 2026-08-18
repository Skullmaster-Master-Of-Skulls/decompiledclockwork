using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200062C RID: 1580
	internal class SubTreeId
	{
		// Token: 0x06003D85 RID: 15749 RVA: 0x0011B3E4 File Offset: 0x001195E4
		internal SubTreeId(RuleProcessingContext context, Node node, Node parent, int childIndex)
		{
			this.m_subTreeRoot = node;
			this.m_parent = parent;
			this.m_childIndex = childIndex;
			this.m_hashCode = context.GetHashCode(node);
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x0011B40F File Offset: 0x0011960F
		public override int GetHashCode()
		{
			return this.m_hashCode;
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x0011B418 File Offset: 0x00119618
		public override bool Equals(object obj)
		{
			SubTreeId subTreeId = obj as SubTreeId;
			return subTreeId != null && this.m_hashCode == subTreeId.m_hashCode && (subTreeId.m_subTreeRoot == this.m_subTreeRoot || (subTreeId.m_parent == this.m_parent && subTreeId.m_childIndex == this.m_childIndex));
		}

		// Token: 0x0400173B RID: 5947
		public Node m_subTreeRoot;

		// Token: 0x0400173C RID: 5948
		private readonly int m_hashCode;

		// Token: 0x0400173D RID: 5949
		private readonly Node m_parent;

		// Token: 0x0400173E RID: 5950
		private readonly int m_childIndex;
	}
}
