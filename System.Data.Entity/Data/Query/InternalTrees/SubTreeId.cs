using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000E7 RID: 231
	internal class SubTreeId
	{
		// Token: 0x06000CBE RID: 3262 RVA: 0x0003C6FB File Offset: 0x0003A8FB
		internal SubTreeId(RuleProcessingContext context, Node node, Node parent, int childIndex)
		{
			this.m_subTreeRoot = node;
			this.m_parent = parent;
			this.m_childIndex = childIndex;
			this.m_hashCode = context.GetHashCode(node);
			this.m_parentHashCode = ((parent == null) ? 0 : context.GetHashCode(parent));
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0003C739 File Offset: 0x0003A939
		public override int GetHashCode()
		{
			return this.m_hashCode;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0003C744 File Offset: 0x0003A944
		public override bool Equals(object obj)
		{
			SubTreeId subTreeId = obj as SubTreeId;
			return subTreeId != null && this.m_hashCode == subTreeId.m_hashCode && (subTreeId.m_subTreeRoot == this.m_subTreeRoot || (subTreeId.m_parent == this.m_parent && subTreeId.m_childIndex == this.m_childIndex));
		}

		// Token: 0x04000993 RID: 2451
		public Node m_subTreeRoot;

		// Token: 0x04000994 RID: 2452
		private int m_hashCode;

		// Token: 0x04000995 RID: 2453
		private Node m_parent;

		// Token: 0x04000996 RID: 2454
		private int m_parentHashCode;

		// Token: 0x04000997 RID: 2455
		private int m_childIndex;
	}
}
