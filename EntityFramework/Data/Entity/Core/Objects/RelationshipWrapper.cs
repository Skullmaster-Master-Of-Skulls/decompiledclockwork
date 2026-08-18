using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005BA RID: 1466
	internal sealed class RelationshipWrapper : IEquatable<RelationshipWrapper>
	{
		// Token: 0x06003AD2 RID: 15058 RVA: 0x00117565 File Offset: 0x00115765
		internal RelationshipWrapper(AssociationSet extent, EntityKey key)
		{
			this.AssociationSet = extent;
			this.Key0 = key;
			this.Key1 = key;
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x00117582 File Offset: 0x00115782
		internal RelationshipWrapper(RelationshipWrapper wrapper, int ordinal, EntityKey key)
		{
			this.AssociationSet = wrapper.AssociationSet;
			this.Key0 = ((ordinal == 0) ? key : wrapper.Key0);
			this.Key1 = ((ordinal == 0) ? wrapper.Key1 : key);
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x001175BA File Offset: 0x001157BA
		internal RelationshipWrapper(AssociationSet extent, KeyValuePair<string, EntityKey> roleAndKey1, KeyValuePair<string, EntityKey> roleAndKey2) : this(extent, roleAndKey1.Key, roleAndKey1.Value, roleAndKey2.Key, roleAndKey2.Value)
		{
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x001175E0 File Offset: 0x001157E0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "role1")]
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

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x00117637 File Offset: 0x00115837
		internal ReadOnlyMetadataCollection<AssociationEndMember> AssociationEndMembers
		{
			get
			{
				return this.AssociationSet.ElementType.AssociationEndMembers;
			}
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x00117649 File Offset: 0x00115849
		internal AssociationEndMember GetAssociationEndMember(EntityKey key)
		{
			return this.AssociationEndMembers[(this.Key0 != key) ? 1 : 0];
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x00117668 File Offset: 0x00115868
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

		// Token: 0x06003AD9 RID: 15065 RVA: 0x00117698 File Offset: 0x00115898
		internal EntityKey GetEntityKey(int ordinal)
		{
			switch (ordinal)
			{
			case 0:
				return this.Key0;
			case 1:
				return this.Key1;
			default:
				throw new ArgumentOutOfRangeException("ordinal");
			}
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x001176CF File Offset: 0x001158CF
		public override int GetHashCode()
		{
			return this.AssociationSet.Name.GetHashCode() ^ this.Key0.GetHashCode() + this.Key1.GetHashCode();
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x001176F9 File Offset: 0x001158F9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RelationshipWrapper);
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x00117708 File Offset: 0x00115908
		public bool Equals(RelationshipWrapper wrapper)
		{
			return object.ReferenceEquals(this, wrapper) || (wrapper != null && object.ReferenceEquals(this.AssociationSet, wrapper.AssociationSet) && this.Key0.Equals(wrapper.Key0) && this.Key1.Equals(wrapper.Key1));
		}

		// Token: 0x0400163B RID: 5691
		internal readonly AssociationSet AssociationSet;

		// Token: 0x0400163C RID: 5692
		internal readonly EntityKey Key0;

		// Token: 0x0400163D RID: 5693
		internal readonly EntityKey Key1;
	}
}
