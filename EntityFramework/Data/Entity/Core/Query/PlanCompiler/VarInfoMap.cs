using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x020006AB RID: 1707
	internal class VarInfoMap
	{
		// Token: 0x0600438F RID: 17295 RVA: 0x001408A9 File Offset: 0x0013EAA9
		internal VarInfoMap()
		{
			this.m_map = new Dictionary<Var, VarInfo>();
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x001408BC File Offset: 0x0013EABC
		internal VarInfo CreateStructuredVarInfo(Var v, RowType newType, List<Var> newVars, List<EdmProperty> newProperties, bool newVarsIncludeNullSentinelVar)
		{
			VarInfo varInfo = new StructuredVarInfo(newType, newVars, newProperties, newVarsIncludeNullSentinelVar);
			this.m_map.Add(v, varInfo);
			return varInfo;
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x001408E3 File Offset: 0x0013EAE3
		internal VarInfo CreateStructuredVarInfo(Var v, RowType newType, List<Var> newVars, List<EdmProperty> newProperties)
		{
			return this.CreateStructuredVarInfo(v, newType, newVars, newProperties, false);
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x001408F4 File Offset: 0x0013EAF4
		internal VarInfo CreateCollectionVarInfo(Var v, Var newVar)
		{
			VarInfo varInfo = new CollectionVarInfo(newVar);
			this.m_map.Add(v, varInfo);
			return varInfo;
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x00140918 File Offset: 0x0013EB18
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal VarInfo CreatePrimitiveTypeVarInfo(Var v, Var newVar)
		{
			PlanCompiler.Assert(TypeSemantics.IsScalarType(v.Type), "The current variable should be of primitive or enum type.");
			PlanCompiler.Assert(TypeSemantics.IsScalarType(newVar.Type), "The new variable should be of primitive or enum type.");
			VarInfo varInfo = new PrimitiveTypeVarInfo(newVar);
			this.m_map.Add(v, varInfo);
			return varInfo;
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x00140964 File Offset: 0x0013EB64
		internal bool TryGetVarInfo(Var v, out VarInfo varInfo)
		{
			return this.m_map.TryGetValue(v, out varInfo);
		}

		// Token: 0x0400190E RID: 6414
		private readonly Dictionary<Var, VarInfo> m_map;
	}
}
