using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200064F RID: 1615
	internal abstract class PropertyRef
	{
		// Token: 0x06003F29 RID: 16169 RVA: 0x00120F36 File Offset: 0x0011F136
		internal virtual PropertyRef CreateNestedPropertyRef(PropertyRef p)
		{
			return new NestedPropertyRef(p, this);
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x00120F3F File Offset: 0x0011F13F
		internal PropertyRef CreateNestedPropertyRef(EdmMember p)
		{
			return this.CreateNestedPropertyRef(new SimplePropertyRef(p));
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x00120F4D File Offset: 0x0011F14D
		internal PropertyRef CreateNestedPropertyRef(RelProperty p)
		{
			return this.CreateNestedPropertyRef(new RelPropertyRef(p));
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x00120F5B File Offset: 0x0011F15B
		public override string ToString()
		{
			return "";
		}
	}
}
