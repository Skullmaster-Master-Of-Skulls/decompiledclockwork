using System;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200008E RID: 142
	internal static class AggregatePushdownUtil
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x0003450C File Offset: 0x0003270C
		internal static bool IsVarRefOverGivenVar(Node node, Var var)
		{
			return node.Op.OpType == OpType.VarRef && ((VarRefOp)node.Op).Var == var;
		}
	}
}
