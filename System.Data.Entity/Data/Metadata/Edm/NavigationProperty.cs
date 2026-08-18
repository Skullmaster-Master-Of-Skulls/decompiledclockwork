using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001E7 RID: 487
	public sealed class NavigationProperty : EdmMember
	{
		// Token: 0x060020B4 RID: 8372 RVA: 0x0007252F File Offset: 0x0007072F
		internal NavigationProperty(string name, TypeUsage typeUsage) : base(name, typeUsage)
		{
			EntityUtil.CheckStringArgument(name, "name");
			EntityUtil.GenericCheckArgumentNull<TypeUsage>(typeUsage, "typeUsage");
			this._accessor = new NavigationPropertyAccessor(name);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x0007255C File Offset: 0x0007075C
		internal NavigationProperty(string name, TypeUsage typeUsage, PropertyInfo propertyInfo) : this(name, typeUsage)
		{
			if (null != propertyInfo)
			{
				MethodInfo getMethod = propertyInfo.GetGetMethod();
				this.PropertyGetterHandle = ((null != getMethod) ? getMethod.MethodHandle : default(RuntimeMethodHandle));
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x000725A1 File Offset: 0x000707A1
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.NavigationProperty;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x000725A5 File Offset: 0x000707A5
		// (set) Token: 0x060020B8 RID: 8376 RVA: 0x000725AD File Offset: 0x000707AD
		[MetadataProperty(BuiltInTypeKind.RelationshipType, false)]
		public RelationshipType RelationshipType
		{
			get
			{
				return this._relationshipType;
			}
			internal set
			{
				this._relationshipType = value;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x000725B6 File Offset: 0x000707B6
		// (set) Token: 0x060020BA RID: 8378 RVA: 0x000725BE File Offset: 0x000707BE
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember ToEndMember
		{
			get
			{
				return this._toEndMember;
			}
			internal set
			{
				this._toEndMember = value;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x000725C7 File Offset: 0x000707C7
		// (set) Token: 0x060020BC RID: 8380 RVA: 0x000725CF File Offset: 0x000707CF
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember FromEndMember
		{
			get
			{
				return this._fromEndMember;
			}
			internal set
			{
				this._fromEndMember = value;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x000725D8 File Offset: 0x000707D8
		internal NavigationPropertyAccessor Accessor
		{
			get
			{
				return this._accessor;
			}
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000725E0 File Offset: 0x000707E0
		public IEnumerable<EdmProperty> GetDependentProperties()
		{
			AssociationType associationType = (AssociationType)this.RelationshipType;
			if (associationType.ReferentialConstraints.Count > 0)
			{
				ReferentialConstraint referentialConstraint = associationType.ReferentialConstraints[0];
				RelationshipEndMember toRole = referentialConstraint.ToRole;
				if (toRole.EdmEquals(this.FromEndMember))
				{
					ReadOnlyMetadataCollection<EdmMember> keyMembers = referentialConstraint.FromRole.GetEntityType().KeyMembers;
					List<EdmProperty> list = new List<EdmProperty>(keyMembers.Count);
					for (int i = 0; i < keyMembers.Count; i++)
					{
						list.Add(referentialConstraint.ToProperties[referentialConstraint.FromProperties.IndexOf((EdmProperty)keyMembers[i])]);
					}
					return list.AsReadOnly();
				}
			}
			return Enumerable.Empty<EdmProperty>();
		}

		// Token: 0x04000E59 RID: 3673
		internal const string RelationshipTypeNamePropertyName = "RelationshipType";

		// Token: 0x04000E5A RID: 3674
		internal const string ToEndMemberNamePropertyName = "ToEndMember";

		// Token: 0x04000E5B RID: 3675
		private RelationshipType _relationshipType;

		// Token: 0x04000E5C RID: 3676
		private RelationshipEndMember _toEndMember;

		// Token: 0x04000E5D RID: 3677
		private RelationshipEndMember _fromEndMember;

		// Token: 0x04000E5E RID: 3678
		internal readonly RuntimeMethodHandle PropertyGetterHandle;

		// Token: 0x04000E5F RID: 3679
		private readonly NavigationPropertyAccessor _accessor;
	}
}
