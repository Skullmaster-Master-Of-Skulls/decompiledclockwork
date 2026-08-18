using System;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000063 RID: 99
	internal abstract class PropertyRef
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x00002050 File Offset: 0x00000250
		internal PropertyRef()
		{
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002CB44 File Offset: 0x0002AD44
		internal virtual PropertyRef CreateNestedPropertyRef(PropertyRef p)
		{
			return new NestedPropertyRef(p, this);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002CB4D File Offset: 0x0002AD4D
		internal PropertyRef CreateNestedPropertyRef(EdmMember p)
		{
			return this.CreateNestedPropertyRef(new SimplePropertyRef(p));
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002CB5B File Offset: 0x0002AD5B
		internal PropertyRef CreateNestedPropertyRef(RelProperty p)
		{
			return this.CreateNestedPropertyRef(new RelPropertyRef(p));
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0002CB69 File Offset: 0x0002AD69
		public override string ToString()
		{
			return "";
		}
	}
}
