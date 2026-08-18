using System;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200007B RID: 123
	internal static class DistinctOpRules
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x00032FD8 File Offset: 0x000311D8
		private static bool ProcessDistinctOpOfKeys(RuleProcessingContext context, Node n, out Node newNode)
		{
			Command command = context.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(n.Child0);
			DistinctOp distinctOp = (DistinctOp)n.Op;
			if (!extendedNodeInfo.Keys.NoKeys && distinctOp.Keys.Subsumes(extendedNodeInfo.Keys.KeyVars))
			{
				ProjectOp op = command.CreateProjectOp(distinctOp.Keys);
				VarDefListOp op2 = command.CreateVarDefListOp();
				Node arg = command.CreateNode(op2);
				newNode = command.CreateNode(op, n.Child0, arg);
				return true;
			}
			newNode = n;
			return false;
		}

		// Token: 0x0400086B RID: 2155
		internal static readonly SimpleRule Rule_DistinctOpOfKeys = new SimpleRule(OpType.Distinct, new Rule.ProcessNodeDelegate(DistinctOpRules.ProcessDistinctOpOfKeys));

		// Token: 0x0400086C RID: 2156
		internal static readonly Rule[] Rules = new Rule[]
		{
			DistinctOpRules.Rule_DistinctOpOfKeys
		};
	}
}
