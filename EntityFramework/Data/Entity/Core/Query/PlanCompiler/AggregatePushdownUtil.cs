using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200064E RID: 1614
	internal static class AggregatePushdownUtil
	{
		// Token: 0x06003F28 RID: 16168 RVA: 0x00120F11 File Offset: 0x0011F111
		internal static bool IsVarRefOverGivenVar(Node node, Var var)
		{
			return node.Op.OpType == OpType.VarRef && ((VarRefOp)node.Op).Var == var;
		}
	}
}
