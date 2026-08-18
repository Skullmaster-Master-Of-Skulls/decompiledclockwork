using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000086 RID: 134
	internal class VarInfoMap
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x0003369C File Offset: 0x0003189C
		internal VarInfoMap()
		{
			this.m_map = new Dictionary<Var, VarInfo>();
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x000336B0 File Offset: 0x000318B0
		internal VarInfo CreateStructuredVarInfo(Var v, RowType newType, List<Var> newVars, List<EdmProperty> newProperties, bool newVarsIncludeNullSentinelVar)
		{
			VarInfo varInfo = new StructuredVarInfo(newType, newVars, newProperties, newVarsIncludeNullSentinelVar);
			this.m_map.Add(v, varInfo);
			return varInfo;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000336D7 File Offset: 0x000318D7
		internal VarInfo CreateStructuredVarInfo(Var v, RowType newType, List<Var> newVars, List<EdmProperty> newProperties)
		{
			return this.CreateStructuredVarInfo(v, newType, newVars, newProperties, false);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000336E8 File Offset: 0x000318E8
		internal VarInfo CreateCollectionVarInfo(Var v, Var newVar)
		{
			VarInfo varInfo = new CollectionVarInfo(newVar);
			this.m_map.Add(v, varInfo);
			return varInfo;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0003370C File Offset: 0x0003190C
		internal VarInfo CreatePrimitiveTypeVarInfo(Var v, Var newVar)
		{
			PlanCompiler.Assert(TypeSemantics.IsScalarType(v.Type), "The current variable should be of primitive or enum type.");
			PlanCompiler.Assert(TypeSemantics.IsScalarType(newVar.Type), "The new variable should be of primitive or enum type.");
			VarInfo varInfo = new PrimitiveTypeVarInfo(newVar);
			this.m_map.Add(v, varInfo);
			return varInfo;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00033758 File Offset: 0x00031958
		internal bool TryGetVarInfo(Var v, out VarInfo varInfo)
		{
			return this.m_map.TryGetValue(v, out varInfo);
		}

		// Token: 0x0400088C RID: 2188
		private Dictionary<Var, VarInfo> m_map;
	}
}
