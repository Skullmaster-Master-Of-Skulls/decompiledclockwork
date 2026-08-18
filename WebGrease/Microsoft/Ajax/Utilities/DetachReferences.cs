using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000088 RID: 136
	public class DetachReferences : TreeVisitor
	{
		// Token: 0x06000842 RID: 2114 RVA: 0x000255AA File Offset: 0x000237AA
		private DetachReferences()
		{
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000255B2 File Offset: 0x000237B2
		public static void Apply(AstNode node)
		{
			if (node != null)
			{
				node.Accept(DetachReferences.s_instance);
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000255C4 File Offset: 0x000237C4
		public static void Apply(params AstNode[] nodes)
		{
			if (nodes != null)
			{
				foreach (AstNode astNode in nodes)
				{
					astNode.Accept(DetachReferences.s_instance);
				}
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000255F4 File Offset: 0x000237F4
		public override void Visit(Lookup node)
		{
			if (node != null)
			{
				JSVariableField variableField = node.VariableField;
				if (variableField != null)
				{
					variableField.References.Remove(node);
				}
			}
		}

		// Token: 0x04000311 RID: 785
		private static readonly DetachReferences s_instance = new DetachReferences();
	}
}
