using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200066A RID: 1642
	internal static class DistinctOpRules
	{
		// Token: 0x06004032 RID: 16434 RVA: 0x00125DC0 File Offset: 0x00123FC0
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

		// Token: 0x040017E2 RID: 6114
		internal static readonly SimpleRule Rule_DistinctOpOfKeys = new SimpleRule(OpType.Distinct, new Rule.ProcessNodeDelegate(DistinctOpRules.ProcessDistinctOpOfKeys));

		// Token: 0x040017E3 RID: 6115
		internal static readonly Rule[] Rules = new Rule[]
		{
			DistinctOpRules.Rule_DistinctOpOfKeys
		};
	}
}
