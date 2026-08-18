using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000612 RID: 1554
	internal sealed class PatternMatchRule : Rule
	{
		// Token: 0x06003D06 RID: 15622 RVA: 0x0011ABBF File Offset: 0x00118DBF
		internal PatternMatchRule(Node pattern, Rule.ProcessNodeDelegate processDelegate) : base(pattern.Op.OpType, processDelegate)
		{
			this.m_pattern = pattern;
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x0011ABDC File Offset: 0x00118DDC
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

		// Token: 0x06003D08 RID: 15624 RVA: 0x0011AC67 File Offset: 0x00118E67
		internal override bool Match(Node node)
		{
			return this.Match(this.m_pattern, node);
		}

		// Token: 0x04001713 RID: 5907
		private readonly Node m_pattern;
	}
}
