using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data.Objects
{
	// Token: 0x0200014A RID: 330
	internal struct EntitySetQualifiedType : IEqualityComparer<EntitySetQualifiedType>
	{
		// Token: 0x06001843 RID: 6211 RVA: 0x000534B5 File Offset: 0x000516B5
		internal EntitySetQualifiedType(Type type, EntitySet set)
		{
			this.ClrType = EntityUtil.GetEntityIdentityType(type);
			this.EntitySet = set;
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x000534CA File Offset: 0x000516CA
		public bool Equals(EntitySetQualifiedType x, EntitySetQualifiedType y)
		{
			return x.ClrType == y.ClrType && x.EntitySet == y.EntitySet;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x000534EA File Offset: 0x000516EA
		public int GetHashCode(EntitySetQualifiedType obj)
		{
			return obj.ClrType.GetHashCode() + obj.EntitySet.Name.GetHashCode() + obj.EntitySet.EntityContainer.Name.GetHashCode();
		}

		// Token: 0x04000AB6 RID: 2742
		internal static readonly IEqualityComparer<EntitySetQualifiedType> EqualityComparer = default(EntitySetQualifiedType);

		// Token: 0x04000AB7 RID: 2743
		internal readonly Type ClrType;

		// Token: 0x04000AB8 RID: 2744
		internal readonly EntitySet EntitySet;
	}
}
