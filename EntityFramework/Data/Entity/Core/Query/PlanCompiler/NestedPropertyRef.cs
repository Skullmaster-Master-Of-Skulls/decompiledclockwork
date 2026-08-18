using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000683 RID: 1667
	internal class NestedPropertyRef : PropertyRef
	{
		// Token: 0x0600415C RID: 16732 RVA: 0x0012FD94 File Offset: 0x0012DF94
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "NestedPropertyRef")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "innerProperty")]
		internal NestedPropertyRef(PropertyRef innerProperty, PropertyRef outerProperty)
		{
			PlanCompiler.Assert(!(innerProperty is NestedPropertyRef), "innerProperty cannot be a NestedPropertyRef");
			this.m_inner = innerProperty;
			this.m_outer = outerProperty;
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x0600415D RID: 16733 RVA: 0x0012FDC0 File Offset: 0x0012DFC0
		internal PropertyRef OuterProperty
		{
			get
			{
				return this.m_outer;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600415E RID: 16734 RVA: 0x0012FDC8 File Offset: 0x0012DFC8
		internal PropertyRef InnerProperty
		{
			get
			{
				return this.m_inner;
			}
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x0012FDD0 File Offset: 0x0012DFD0
		public override bool Equals(object obj)
		{
			NestedPropertyRef nestedPropertyRef = obj as NestedPropertyRef;
			return nestedPropertyRef != null && this.m_inner.Equals(nestedPropertyRef.m_inner) && this.m_outer.Equals(nestedPropertyRef.m_outer);
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x0012FE0D File Offset: 0x0012E00D
		public override int GetHashCode()
		{
			return this.m_inner.GetHashCode() ^ this.m_outer.GetHashCode();
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x0012FE26 File Offset: 0x0012E026
		public override string ToString()
		{
			return this.m_inner + "." + this.m_outer;
		}

		// Token: 0x04001856 RID: 6230
		private readonly PropertyRef m_inner;

		// Token: 0x04001857 RID: 6231
		private readonly PropertyRef m_outer;
	}
}
