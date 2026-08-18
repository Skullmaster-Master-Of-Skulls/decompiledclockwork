using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000697 RID: 1687
	internal static class ProviderCommandInfoUtils
	{
		// Token: 0x060042D5 RID: 17109 RVA: 0x0013CA50 File Offset: 0x0013AC50
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "rowtype")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal static ProviderCommandInfo Create(Command command, Node node)
		{
			PhysicalProjectOp physicalProjectOp = node.Op as PhysicalProjectOp;
			PlanCompiler.Assert(physicalProjectOp != null, "Expected root Op to be a physical Project");
			DbCommandTree dbCommandTree = CTreeGenerator.Generate(command, node);
			DbQueryCommandTree dbQueryCommandTree = dbCommandTree as DbQueryCommandTree;
			PlanCompiler.Assert(dbQueryCommandTree != null, "null query command tree");
			CollectionType edmType = TypeHelpers.GetEdmType<CollectionType>(dbQueryCommandTree.Query.ResultType);
			PlanCompiler.Assert(TypeSemantics.IsRowType(edmType.TypeUsage), "command rowtype is not a record");
			ProviderCommandInfoUtils.BuildOutputVarMap(physicalProjectOp, edmType.TypeUsage);
			return new ProviderCommandInfo(dbCommandTree);
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x0013CAD4 File Offset: 0x0013ACD4
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "RowType")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "PhysicalProjectOp")]
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
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.ColumnCountMismatch, 1, null);
		}
	}
}
