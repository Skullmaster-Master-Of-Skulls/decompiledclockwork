using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000E4 RID: 228
	internal sealed class PatternMatchRule : Rule
	{
		// Token: 0x06000CB4 RID: 3252 RVA: 0x0003C4AD File Offset: 0x0003A6AD
		internal PatternMatchRule(Node pattern, Rule.ProcessNodeDelegate processDelegate) : base(pattern.Op.OpType, processDelegate)
		{
			this.m_pattern = pattern;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0003C4C8 File Offset: 0x0003A6C8
		private bool Match(Node pattern, Node original)
		{
			if (pattern.Op.OpType == OpType.Leaf)
			{
				return true;
			}
			if (pattern.Op.OpType != original.Op.OpType)
			{
				return false;
			}
			if (pattern.Children.Count != original.Children.Count)
			{
				return false;
			}
			for (int i = 0; i < pattern.Children.Count; i++)
			{
				if (!this.Match(pattern.Children[i], original.Children[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0003C553 File Offset: 0x0003A753
		internal override bool Match(Node node)
		{
			return this.Match(this.m_pattern, node);
		}

		// Token: 0x0400098F RID: 2447
		private Node m_pattern;
	}
}
