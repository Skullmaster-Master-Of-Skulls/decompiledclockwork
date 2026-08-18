using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000084 RID: 132
	internal class StructuredVarInfo : VarInfo
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x00033540 File Offset: 0x00031740
		internal StructuredVarInfo(RowType newType, List<Var> newVars, List<EdmProperty> newTypeProperties, bool newVarsIncludeNullSentinelVar)
		{
			PlanCompiler.Assert(newVars.Count == newTypeProperties.Count, "count mismatch");
			this.m_newVars = newVars;
			this.m_newProperties = newTypeProperties;
			this.m_newType = newType;
			this.m_newVarsIncludeNullSentinelVar = newVarsIncludeNullSentinelVar;
			this.m_newTypeUsage = TypeUsage.Create(newType);
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00017938 File Offset: 0x00015B38
		internal override VarInfoKind Kind
		{
			get
			{
				return VarInfoKind.StructuredTypeVarInfo;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00033594 File Offset: 0x00031794
		internal override List<Var> NewVars
		{
			get
			{
				return this.m_newVars;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0003359C File Offset: 0x0003179C
		internal List<EdmProperty> Fields
		{
			get
			{
				return this.m_newProperties;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x000335A4 File Offset: 0x000317A4
		internal bool NewVarsIncludeNullSentinelVar
		{
			get
			{
				return this.m_newVarsIncludeNullSentinelVar;
			}
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x000335AC File Offset: 0x000317AC
		internal bool TryGetVar(EdmProperty p, out Var v)
		{
			if (this.m_propertyToVarMap == null)
			{
				this.InitPropertyToVarMap();
			}
			return this.m_propertyToVarMap.TryGetValue(p, out v);
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x000335C9 File Offset: 0x000317C9
		internal RowType NewType
		{
			get
			{
				return this.m_newType;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x000335D1 File Offset: 0x000317D1
		internal TypeUsage NewTypeUsage
		{
			get
			{
				return this.m_newTypeUsage;
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000335DC File Offset: 0x000317DC
		private void InitPropertyToVarMap()
		{
			if (this.m_propertyToVarMap == null)
			{
				this.m_propertyToVarMap = new Dictionary<EdmProperty, Var>();
				IEnumerator<Var> enumerator = this.m_newVars.GetEnumerator();
				foreach (EdmProperty key in this.m_newProperties)
				{
					enumerator.MoveNext();
					this.m_propertyToVarMap.Add(key, enumerator.Current);
				}
				enumerator.Dispose();
			}
		}

		// Token: 0x04000885 RID: 2181
		private Dictionary<EdmProperty, Var> m_propertyToVarMap;

		// Token: 0x04000886 RID: 2182
		private List<Var> m_newVars;

		// Token: 0x04000887 RID: 2183
		private bool m_newVarsIncludeNullSentinelVar;

		// Token: 0x04000888 RID: 2184
		private List<EdmProperty> m_newProperties;

		// Token: 0x04000889 RID: 2185
		private RowType m_newType;

		// Token: 0x0400088A RID: 2186
		private TypeUsage m_newTypeUsage;
	}
}
