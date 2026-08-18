using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F1 RID: 1265
	public sealed class NavigationProperty : EdmMember
	{
		// Token: 0x06002F0C RID: 12044 RVA: 0x000E0C11 File Offset: 0x000DEE11
		internal NavigationProperty(string name, TypeUsage typeUsage) : base(name, typeUsage)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
			this._accessor = new NavigationPropertyAccessor(name);
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000E0C3F File Offset: 0x000DEE3F
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.NavigationProperty;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002F0E RID: 12046 RVA: 0x000E0C43 File Offset: 0x000DEE43
		// (set) Token: 0x06002F0F RID: 12047 RVA: 0x000E0C4B File Offset: 0x000DEE4B
		[MetadataProperty(BuiltInTypeKind.RelationshipType, false)]
		public RelationshipType RelationshipType { get; internal set; }

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002F10 RID: 12048 RVA: 0x000E0C54 File Offset: 0x000DEE54
		// (set) Token: 0x06002F11 RID: 12049 RVA: 0x000E0C5C File Offset: 0x000DEE5C
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember ToEndMember { get; internal set; }

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x000E0C65 File Offset: 0x000DEE65
		// (set) Token: 0x06002F13 RID: 12051 RVA: 0x000E0C6D File Offset: 0x000DEE6D
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember FromEndMember { get; internal set; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002F14 RID: 12052 RVA: 0x000E0C76 File Offset: 0x000DEE76
		internal AssociationType Association
		{
			get
			{
				return (AssociationType)this.RelationshipType;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06002F15 RID: 12053 RVA: 0x000E0C83 File Offset: 0x000DEE83
		internal AssociationEndMember ResultEnd
		{
			get
			{
				return (AssociationEndMember)this.ToEndMember;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002F16 RID: 12054 RVA: 0x000E0C90 File Offset: 0x000DEE90
		internal NavigationPropertyAccessor Accessor
		{
			get
			{
				return this._accessor;
			}
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000E0C98 File Offset: 0x000DEE98
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
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
					return new ReadOnlyCollection<EdmProperty>(list);
				}
			}
			return Enumerable.Empty<EdmProperty>();
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x000E0D50 File Offset: 0x000DEF50
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly && this.ToEndMember != null && this.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One)
			{
				this.TypeUsage = this.TypeUsage.ShallowCopy(new Facet[]
				{
					Facet.Create(MetadataItem.NullableFacetDescription, false)
				});
			}
			base.SetReadOnly();
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000E0DB0 File Offset: 0x000DEFB0
		public static NavigationProperty Create(string name, TypeUsage typeUsage, RelationshipType relationshipType, RelationshipEndMember from, RelationshipEndMember to, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
			NavigationProperty navigationProperty = new NavigationProperty(name, typeUsage);
			navigationProperty.RelationshipType = relationshipType;
			navigationProperty.FromEndMember = from;
			navigationProperty.ToEndMember = to;
			if (metadataProperties != null)
			{
				navigationProperty.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
			}
			navigationProperty.SetReadOnly();
			return navigationProperty;
		}

		// Token: 0x040011DC RID: 4572
		internal const string RelationshipTypeNamePropertyName = "RelationshipType";

		// Token: 0x040011DD RID: 4573
		internal const string ToEndMemberNamePropertyName = "ToEndMember";

		// Token: 0x040011DE RID: 4574
		private readonly NavigationPropertyAccessor _accessor;
	}
}
