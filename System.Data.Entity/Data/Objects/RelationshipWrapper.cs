using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data.Objects
{
	// Token: 0x02000150 RID: 336
	internal sealed class RelationshipWrapper : IEquatable<RelationshipWrapper>
	{
		// Token: 0x06001891 RID: 6289 RVA: 0x00054008 File Offset: 0x00052208
		internal RelationshipWrapper(AssociationSet extent, EntityKey key)
		{
			this.AssociationSet = extent;
			this.Key0 = key;
			this.Key1 = key;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00054025 File Offset: 0x00052225
		internal RelationshipWrapper(RelationshipWrapper wrapper, int ordinal, EntityKey key)
		{
			this.AssociationSet = wrapper.AssociationSet;
			this.Key0 = ((ordinal == 0) ? key : wrapper.Key0);
			this.Key1 = ((ordinal == 0) ? wrapper.Key1 : key);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0005405D File Offset: 0x0005225D
		internal RelationshipWrapper(AssociationSet extent, KeyValuePair<string, EntityKey> roleAndKey1, KeyValuePair<string, EntityKey> roleAndKey2) : this(extent, roleAndKey1.Key, roleAndKey1.Value, roleAndKey2.Key, roleAndKey2.Value)
		{
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00054084 File Offset: 0x00052284
		internal RelationshipWrapper(AssociationSet extent, string role0, EntityKey key0, string role1, EntityKey key1)
		{
			this.AssociationSet = extent;
			if (extent.ElementType.AssociationEndMembers[0].Name == role0)
			{
				this.Key0 = key0;
				this.Key1 = key1;
				return;
			}
			this.Key0 = key1;
			this.Key1 = key0;
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x000540DB File Offset: 0x000522DB
		internal ReadOnlyMetadataCollection<AssociationEndMember> AssociationEndMembers
		{
			get
			{
				return this.AssociationSet.ElementType.AssociationEndMembers;
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x000540ED File Offset: 0x000522ED
		internal AssociationEndMember GetAssociationEndMember(EntityKey key)
		{
			return this.AssociationEndMembers[(this.Key0 != key) ? 1 : 0];
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0005410C File Offset: 0x0005230C
		internal EntityKey GetOtherEntityKey(EntityKey key)
		{
			if (this.Key0 == key)
			{
				return this.Key1;
			}
			if (!(this.Key1 == key))
			{
				return null;
			}
			return this.Key0;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00054139 File Offset: 0x00052339
		internal EntityKey GetEntityKey(int ordinal)
		{
			if (ordinal == 0)
			{
				return this.Key0;
			}
			if (ordinal != 1)
			{
				throw EntityUtil.ArgumentOutOfRange("ordinal");
			}
			return this.Key1;
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0005415C File Offset: 0x0005235C
		public override int GetHashCode()
		{
			return this.AssociationSet.Name.GetHashCode() ^ this.Key0.GetHashCode() + this.Key1.GetHashCode();
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00054186 File Offset: 0x00052386
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RelationshipWrapper);
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00054194 File Offset: 0x00052394
		public bool Equals(RelationshipWrapper wrapper)
		{
			return this == wrapper || (wrapper != null && this.AssociationSet == wrapper.AssociationSet && this.Key0.Equals(wrapper.Key0) && this.Key1.Equals(wrapper.Key1));
		}

		// Token: 0x04000ACE RID: 2766
		internal readonly AssociationSet AssociationSet;

		// Token: 0x04000ACF RID: 2767
		internal readonly EntityKey Key0;

		// Token: 0x04000AD0 RID: 2768
		internal readonly EntityKey Key1;
	}
}
