using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200006C RID: 108
	internal static class ProviderCommandInfoUtils
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x0002CF6C File Offset: 0x0002B16C
		internal static ProviderCommandInfo Create(Command command, Node node, List<ProviderCommandInfo> children)
		{
			PhysicalProjectOp physicalProjectOp = node.Op as PhysicalProjectOp;
			PlanCompiler.Assert(physicalProjectOp != null, "Expected root Op to be a physical Project");
			DbCommandTree dbCommandTree = CTreeGenerator.Generate(command, node);
			DbQueryCommandTree dbQueryCommandTree = dbCommandTree as DbQueryCommandTree;
			PlanCompiler.Assert(dbQueryCommandTree != null, "null query command tree");
			CollectionType edmType = TypeHelpers.GetEdmType<CollectionType>(dbQueryCommandTree.Query.ResultType);
			PlanCompiler.Assert(TypeSemantics.IsRowType(edmType.TypeUsage), "command rowtype is not a record");
			Dictionary<Var, EdmProperty> dictionary = ProviderCommandInfoUtils.BuildOutputVarMap(physicalProjectOp, edmType.TypeUsage);
			return new ProviderCommandInfo(dbCommandTree, children);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0002CFEB File Offset: 0x0002B1EB
		internal static ProviderCommandInfo Create(Command command, Node node)
		{
			return ProviderCommandInfoUtils.Create(command, node, new List<ProviderCommandInfo>());
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002CFFC File Offset: 0x0002B1FC
		private static Dictionary<Var, EdmProperty> BuildOutputVarMap(PhysicalProjectOp projectOp, TypeUsage outputType)
		{
			Dictionary<Var, EdmProperty> dictionary = new Dictionary<Var, EdmProperty>();
			PlanCompiler.Assert(TypeSemantics.IsRowType(outputType), "PhysicalProjectOp result type is not a RowType?");
			IEnumerator<EdmProperty> enumerator = TypeHelpers.GetEdmType<RowType>(outputType).Properties.GetEnumerator();
			IEnumerator<Var> enumerator2 = projectOp.Outputs.GetEnumerator();
			for (;;)
			{
				bool flag = enumerator.MoveNext();
				bool flag2 = enumerator2.MoveNext();
				if (flag != flag2)
				{
					break;
				}
				if (!flag)
				{
					return dictionary;
				}
				dictionary[enumerator2.Current] = enumerator.Current;
			}
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.ColumnCountMismatch, 1);
		}
	}
}
