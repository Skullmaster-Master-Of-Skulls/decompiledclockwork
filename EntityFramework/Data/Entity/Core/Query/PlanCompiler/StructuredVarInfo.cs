using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x020006A3 RID: 1699
	internal class StructuredVarInfo : VarInfo
	{
		// Token: 0x06004355 RID: 17237 RVA: 0x0013F960 File Offset: 0x0013DB60
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal StructuredVarInfo(RowType newType, List<Var> newVars, List<EdmProperty> newTypeProperties, bool newVarsIncludeNullSentinelVar)
		{
			PlanCompiler.Assert(newVars.Count == newTypeProperties.Count, "count mismatch");
			this.m_newVars = newVars;
			this.m_newProperties = newTypeProperties;
			this.m_newType = newType;
			this.m_newVarsIncludeNullSentinelVar = newVarsIncludeNullSentinelVar;
			this.m_newTypeUsage = TypeUsage.Create(newType);
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06004356 RID: 17238 RVA: 0x0013F9B4 File Offset: 0x0013DBB4
		internal override VarInfoKind Kind
		{
			get
			{
				return VarInfoKind.StructuredTypeVarInfo;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06004357 RID: 17239 RVA: 0x0013F9B7 File Offset: 0x0013DBB7
		internal override List<Var> NewVars
		{
			get
			{
				return this.m_newVars;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06004358 RID: 17240 RVA: 0x0013F9BF File Offset: 0x0013DBBF
		internal List<EdmProperty> Fields
		{
			get
			{
				return this.m_newProperties;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06004359 RID: 17241 RVA: 0x0013F9C7 File Offset: 0x0013DBC7
		internal bool NewVarsIncludeNullSentinelVar
		{
			get
			{
				return this.m_newVarsIncludeNullSentinelVar;
			}
		}

		// Token: 0x0600435A RID: 17242 RVA: 0x0013F9CF File Offset: 0x0013DBCF
		internal bool TryGetVar(EdmProperty p, out Var v)
		{
			if (this.m_propertyToVarMap == null)
			{
				this.InitPropertyToVarMap();
			}
			return this.m_propertyToVarMap.TryGetValue(p, out v);
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x0600435B RID: 17243 RVA: 0x0013F9EC File Offset: 0x0013DBEC
		internal RowType NewType
		{
			get
			{
				return this.m_newType;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x0600435C RID: 17244 RVA: 0x0013F9F4 File Offset: 0x0013DBF4
		internal TypeUsage NewTypeUsage
		{
			get
			{
				return this.m_newTypeUsage;
			}
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x0013F9FC File Offset: 0x0013DBFC
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

		// Token: 0x040018EB RID: 6379
		private Dictionary<EdmProperty, Var> m_propertyToVarMap;

		// Token: 0x040018EC RID: 6380
		private readonly List<Var> m_newVars;

		// Token: 0x040018ED RID: 6381
		private readonly bool m_newVarsIncludeNullSentinelVar;

		// Token: 0x040018EE RID: 6382
		private readonly List<EdmProperty> m_newProperties;

		// Token: 0x040018EF RID: 6383
		private readonly RowType m_newType;

		// Token: 0x040018F0 RID: 6384
		private readonly TypeUsage m_newTypeUsage;
	}
}
