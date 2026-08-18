using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200056F RID: 1391
	internal struct EntitySetQualifiedType : IEqualityComparer<EntitySetQualifiedType>
	{
		// Token: 0x06003643 RID: 13891 RVA: 0x00102E0E File Offset: 0x0010100E
		internal EntitySetQualifiedType(Type type, EntitySet set)
		{
			this.ClrType = EntityUtil.GetEntityIdentityType(type);
			this.EntitySet = set;
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x00102E23 File Offset: 0x00101023
		public bool Equals(EntitySetQualifiedType x, EntitySetQualifiedType y)
		{
			return object.ReferenceEquals(x.ClrType, y.ClrType) && object.ReferenceEquals(x.EntitySet, y.EntitySet);
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x00102E4F File Offset: 0x0010104F
		[SuppressMessage("Microsoft.Usage", "CA2303", Justification = "ClrType is not expected to be an Embedded Interop Type.")]
		public int GetHashCode(EntitySetQualifiedType obj)
		{
			return obj.ClrType.GetHashCode() + obj.EntitySet.Name.GetHashCode() + obj.EntitySet.EntityContainer.Name.GetHashCode();
		}

		// Token: 0x040014C7 RID: 5319
		internal static readonly IEqualityComparer<EntitySetQualifiedType> EqualityComparer = default(EntitySetQualifiedType);

		// Token: 0x040014C8 RID: 5320
		internal readonly Type ClrType;

		// Token: 0x040014C9 RID: 5321
		internal readonly EntitySet EntitySet;
	}
}
