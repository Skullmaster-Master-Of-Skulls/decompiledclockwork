using System;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000068 RID: 104
	internal class NestedPropertyRef : PropertyRef
	{
		// Token: 0x0600088A RID: 2186 RVA: 0x0002CC40 File Offset: 0x0002AE40
		internal NestedPropertyRef(PropertyRef innerProperty, PropertyRef outerProperty)
		{
			PlanCompiler.Assert(!(innerProperty is NestedPropertyRef), "innerProperty cannot be a NestedPropertyRef");
			this.m_inner = innerProperty;
			this.m_outer = outerProperty;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0002CC6C File Offset: 0x0002AE6C
		internal PropertyRef OuterProperty
		{
			get
			{
				return this.m_outer;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0002CC74 File Offset: 0x0002AE74
		internal PropertyRef InnerProperty
		{
			get
			{
				return this.m_inner;
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0002CC7C File Offset: 0x0002AE7C
		public override bool Equals(object obj)
		{
			NestedPropertyRef nestedPropertyRef = obj as NestedPropertyRef;
			return nestedPropertyRef != null && this.m_inner.Equals(nestedPropertyRef.m_inner) && this.m_outer.Equals(nestedPropertyRef.m_outer);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002CCB9 File Offset: 0x0002AEB9
		public override int GetHashCode()
		{
			return this.m_inner.GetHashCode() ^ this.m_outer.GetHashCode();
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0002CCD2 File Offset: 0x0002AED2
		public override string ToString()
		{
			PropertyRef inner = this.m_inner;
			string str = (inner != null) ? inner.ToString() : null;
			string str2 = ".";
			PropertyRef outer = this.m_outer;
			return str + str2 + ((outer != null) ? outer.ToString() : null);
		}

		// Token: 0x040007FB RID: 2043
		private readonly PropertyRef m_inner;

		// Token: 0x040007FC RID: 2044
		private readonly PropertyRef m_outer;
	}
}
