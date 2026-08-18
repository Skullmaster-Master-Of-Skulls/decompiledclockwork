using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200047D RID: 1149
	internal sealed class MemberPath : InternalBase, IEquatable<MemberPath>
	{
		// Token: 0x06002A57 RID: 10839 RVA: 0x000CC59C File Offset: 0x000CA79C
		internal MemberPath(EntitySetBase extent, IEnumerable<EdmMember> path)
		{
			this.m_extent = extent;
			this.m_path = path.ToList<EdmMember>();
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000CC5B7 File Offset: 0x000CA7B7
		internal MemberPath(EntitySetBase extent) : this(extent, Enumerable.Empty<EdmMember>())
		{
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000CC5C5 File Offset: 0x000CA7C5
		internal MemberPath(EntitySetBase extent, EdmMember member) : this(extent, Enumerable.Repeat<EdmMember>(member, 1))
		{
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000CC5D5 File Offset: 0x000CA7D5
		internal MemberPath(MemberPath prefix, EdmMember last)
		{
			this.m_extent = prefix.m_extent;
			this.m_path = new List<EdmMember>(prefix.m_path);
			this.m_path.Add(last);
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002A5B RID: 10843 RVA: 0x000CC606 File Offset: 0x000CA806
		internal EdmMember RootEdmMember
		{
			get
			{
				if (this.m_path.Count <= 0)
				{
					return null;
				}
				return this.m_path[0];
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x000CC624 File Offset: 0x000CA824
		internal EdmMember LeafEdmMember
		{
			get
			{
				if (this.m_path.Count <= 0)
				{
					return null;
				}
				return this.m_path[this.m_path.Count - 1];
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x000CC64E File Offset: 0x000CA84E
		internal string LeafName
		{
			get
			{
				if (this.m_path.Count == 0)
				{
					return this.m_extent.Name;
				}
				return this.LeafEdmMember.Name;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x000CC674 File Offset: 0x000CA874
		internal bool IsComputed
		{
			get
			{
				return this.m_path.Count != 0 && this.RootEdmMember.IsStoreGeneratedComputed;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x000CC690 File Offset: 0x000CA890
		internal object DefaultValue
		{
			get
			{
				if (this.m_path.Count == 0)
				{
					return null;
				}
				Facet facet;
				if (this.LeafEdmMember.TypeUsage.Facets.TryGetValue("DefaultValue", false, out facet))
				{
					return facet.Value;
				}
				return null;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06002A60 RID: 10848 RVA: 0x000CC6D3 File Offset: 0x000CA8D3
		internal bool IsPartOfKey
		{
			get
			{
				return this.m_path.Count != 0 && MetadataHelper.IsPartOfEntityTypeKey(this.LeafEdmMember);
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06002A61 RID: 10849 RVA: 0x000CC6EF File Offset: 0x000CA8EF
		internal bool IsNullable
		{
			get
			{
				return this.m_path.Count != 0 && MetadataHelper.IsMemberNullable(this.LeafEdmMember);
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x000CC70C File Offset: 0x000CA90C
		internal EntitySet EntitySet
		{
			get
			{
				if (this.m_path.Count == 0)
				{
					return this.m_extent as EntitySet;
				}
				if (this.m_path.Count == 1)
				{
					AssociationEndMember associationEndMember = this.RootEdmMember as AssociationEndMember;
					if (associationEndMember != null)
					{
						return MetadataHelper.GetEntitySetAtEnd((AssociationSet)this.m_extent, associationEndMember);
					}
				}
				return null;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06002A63 RID: 10851 RVA: 0x000CC764 File Offset: 0x000CA964
		internal EntitySetBase Extent
		{
			get
			{
				return this.m_extent;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002A64 RID: 10852 RVA: 0x000CC76C File Offset: 0x000CA96C
		internal EdmType EdmType
		{
			get
			{
				if (this.m_path.Count > 0)
				{
					return this.LeafEdmMember.TypeUsage.EdmType;
				}
				return this.m_extent.ElementType;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002A65 RID: 10853 RVA: 0x000CC798 File Offset: 0x000CA998
		internal string CqlFieldAlias
		{
			get
			{
				string text = this.PathToString(new bool?(true));
				if (!text.Contains("_"))
				{
					text = text.Replace('.', '_');
				}
				StringBuilder stringBuilder = new StringBuilder();
				CqlWriter.AppendEscapedName(stringBuilder, text);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000CC7E0 File Offset: 0x000CA9E0
		internal bool IsAlwaysDefined(Dictionary<EntityType, Set<EntityType>> inheritanceGraph)
		{
			if (this.m_path.Count == 0)
			{
				return true;
			}
			EdmMember member = this.m_path.Last<EdmMember>();
			for (int i = 0; i < this.m_path.Count - 1; i++)
			{
				EdmMember member2 = this.m_path[i];
				if (MetadataHelper.IsMemberNullable(member2))
				{
					return false;
				}
			}
			if (this.m_path[0].DeclaringType is AssociationType)
			{
				return true;
			}
			EntityType entityType = this.m_extent.ElementType as EntityType;
			if (entityType == null)
			{
				return true;
			}
			EntityType entityType2 = this.m_path[0].DeclaringType as EntityType;
			EntityType entityType3 = entityType2.BaseType as EntityType;
			return entityType.EdmEquals(entityType2) || MetadataHelper.IsParentOf(entityType2, entityType) || entityType3 == null || ((entityType3.Abstract || MetadataHelper.DoesMemberExist(entityType3, member)) && !MemberPath.RecurseToFindMemberAbsentInConcreteType(entityType3, entityType2, member, entityType, inheritanceGraph));
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000CC8E8 File Offset: 0x000CAAE8
		private static bool RecurseToFindMemberAbsentInConcreteType(EntityType current, EntityType avoidEdge, EdmMember member, EntityType entitySetType, Dictionary<EntityType, Set<EntityType>> inheritanceGraph)
		{
			Set<EntityType> source = inheritanceGraph[current];
			foreach (EntityType entityType in from type in source
			where !type.EdmEquals(avoidEdge)
			select type)
			{
				if (entitySetType.BaseType == null || !entitySetType.BaseType.EdmEquals(entityType))
				{
					if (!entityType.Abstract && !MetadataHelper.DoesMemberExist(entityType, member))
					{
						return true;
					}
					if (MemberPath.RecurseToFindMemberAbsentInConcreteType(entityType, current, member, entitySetType, inheritanceGraph))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000CC994 File Offset: 0x000CAB94
		internal void GetIdentifiers(CqlIdentifiers identifiers)
		{
			identifiers.AddIdentifier(this.m_extent.Name);
			identifiers.AddIdentifier(this.m_extent.ElementType.Name);
			foreach (EdmMember edmMember in this.m_path)
			{
				identifiers.AddIdentifier(edmMember.Name);
			}
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000CCA14 File Offset: 0x000CAC14
		internal static bool AreAllMembersNullable(IEnumerable<MemberPath> members)
		{
			foreach (MemberPath memberPath in members)
			{
				if (memberPath.m_path.Count == 0)
				{
					return false;
				}
				if (!memberPath.IsNullable)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000CCA78 File Offset: 0x000CAC78
		internal static string PropertiesToUserString(IEnumerable<MemberPath> members, bool fullPath)
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MemberPath memberPath in members)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				if (fullPath)
				{
					stringBuilder.Append(memberPath.PathToString(new bool?(false)));
				}
				else
				{
					stringBuilder.Append(memberPath.LeafName);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000CCB88 File Offset: 0x000CAD88
		internal StringBuilder AsEsql(StringBuilder inputBuilder, string blockAlias)
		{
			StringBuilder builder = new StringBuilder();
			CqlWriter.AppendEscapedName(builder, blockAlias);
			this.AsCql(delegate(string memberName)
			{
				builder.Append('.');
				CqlWriter.AppendEscapedName(builder, memberName);
			}, delegate
			{
				builder.Insert(0, "Key(");
				builder.Append(")");
			}, delegate(StructuralType treatAsType)
			{
				builder.Insert(0, "TREAT(");
				builder.Append(" AS ");
				CqlWriter.AppendEscapedTypeName(builder, treatAsType);
				builder.Append(')');
			});
			inputBuilder.Append(builder);
			return inputBuilder;
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000CCC44 File Offset: 0x000CAE44
		internal DbExpression AsCqt(DbExpression row)
		{
			this.AsCql(delegate(string memberName)
			{
				row = row.Property(memberName);
			}, delegate
			{
				row = row.GetRefKey();
			}, delegate(StructuralType treatAsType)
			{
				TypeUsage treatType = TypeUsage.Create(treatAsType);
				row = row.TreatAs(treatType);
			});
			return row;
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000CCC90 File Offset: 0x000CAE90
		internal void AsCql(Action<string> accessMember, Action getKey, Action<StructuralType> treatAs)
		{
			EdmType edmType = this.m_extent.ElementType;
			foreach (EdmMember edmMember in this.m_path)
			{
				RefType refType;
				StructuralType type;
				if (Helper.IsRefType(edmType))
				{
					refType = (RefType)edmType;
					type = refType.ElementType;
				}
				else
				{
					refType = null;
					type = (StructuralType)edmType;
				}
				bool flag = MetadataHelper.DoesMemberExist(type, edmMember);
				if (refType != null)
				{
					getKey();
				}
				else if (!flag)
				{
					treatAs(edmMember.DeclaringType);
				}
				accessMember(edmMember.Name);
				edmType = edmMember.TypeUsage.EdmType;
			}
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000CCD48 File Offset: 0x000CAF48
		public bool Equals(MemberPath right)
		{
			return MemberPath.EqualityComparer.Equals(this, right);
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000CCD58 File Offset: 0x000CAF58
		public override bool Equals(object obj)
		{
			MemberPath right = obj as MemberPath;
			return obj != null && this.Equals(right);
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000CCD78 File Offset: 0x000CAF78
		public override int GetHashCode()
		{
			return MemberPath.EqualityComparer.GetHashCode(this);
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000CCD85 File Offset: 0x000CAF85
		internal bool IsScalarType()
		{
			return this.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType || this.EdmType.BuiltInTypeKind == BuiltInTypeKind.EnumType;
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000CCDA8 File Offset: 0x000CAFA8
		internal static IEnumerable<MemberPath> GetKeyMembers(EntitySetBase extent, MemberDomainMap domainMap)
		{
			MemberPath memberPath = new MemberPath(extent);
			return new List<MemberPath>(memberPath.GetMembers(memberPath.Extent.ElementType, null, null, new bool?(true), domainMap));
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000CD1F8 File Offset: 0x000CB3F8
		internal IEnumerable<MemberPath> GetMembers(EdmType edmType, bool? isScalar, bool? isConditional, bool? isPartOfKey, MemberDomainMap domainMap)
		{
			StructuralType structuralType = (StructuralType)edmType;
			foreach (EdmMember edmMember in structuralType.Members)
			{
				if (edmMember is AssociationEndMember)
				{
					foreach (MemberPath endKey in new MemberPath(this, edmMember).GetMembers(((RefType)edmMember.TypeUsage.EdmType).ElementType, isScalar, isConditional, new bool?(true), domainMap))
					{
						yield return endKey;
					}
				}
				bool isActuallyScalar = MetadataHelper.IsNonRefSimpleMember(edmMember);
				if (isScalar == null || isScalar == isActuallyScalar)
				{
					EdmProperty childProperty = edmMember as EdmProperty;
					if (childProperty != null)
					{
						bool isActuallyKey = MetadataHelper.IsPartOfEntityTypeKey(childProperty);
						if (isPartOfKey == null || isPartOfKey == isActuallyKey)
						{
							MemberPath childPath = new MemberPath(this, childProperty);
							bool isActuallyConditional = domainMap.IsConditionMember(childPath);
							if (isConditional == null || isConditional == isActuallyConditional)
							{
								yield return childPath;
							}
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000CD23C File Offset: 0x000CB43C
		internal bool IsEquivalentViaRefConstraint(MemberPath path1)
		{
			if (this.EdmType is EntityTypeBase || path1.EdmType is EntityTypeBase || !MetadataHelper.IsNonRefSimpleMember(this.LeafEdmMember) || !MetadataHelper.IsNonRefSimpleMember(path1.LeafEdmMember))
			{
				return false;
			}
			AssociationSet associationSet = this.Extent as AssociationSet;
			AssociationSet associationSet2 = path1.Extent as AssociationSet;
			EntitySet entitySet = this.Extent as EntitySet;
			EntitySet entitySet2 = path1.Extent as EntitySet;
			bool result = false;
			if (associationSet != null && associationSet2 != null)
			{
				if (!associationSet.Equals(associationSet2))
				{
					return false;
				}
				result = MemberPath.AreAssocationEndPathsEquivalentViaRefConstraint(this, path1, associationSet);
			}
			else
			{
				if (entitySet != null && entitySet2 != null)
				{
					List<AssociationSet> associationsForEntitySets = MetadataHelper.GetAssociationsForEntitySets(entitySet, entitySet2);
					using (List<AssociationSet>.Enumerator enumerator = associationsForEntitySets.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							AssociationSet assocSet = enumerator.Current;
							MemberPath correspondingAssociationPath = this.GetCorrespondingAssociationPath(assocSet);
							MemberPath correspondingAssociationPath2 = path1.GetCorrespondingAssociationPath(assocSet);
							if (MemberPath.AreAssocationEndPathsEquivalentViaRefConstraint(correspondingAssociationPath, correspondingAssociationPath2, assocSet))
							{
								result = true;
								break;
							}
						}
						return result;
					}
				}
				AssociationSet assocSet2 = (associationSet != null) ? associationSet : associationSet2;
				MemberPath assocPath = (this.Extent is AssociationSet) ? this : path1;
				MemberPath memberPath = (this.Extent is EntitySet) ? this : path1;
				MemberPath correspondingAssociationPath3 = memberPath.GetCorrespondingAssociationPath(assocSet2);
				result = (correspondingAssociationPath3 != null && MemberPath.AreAssocationEndPathsEquivalentViaRefConstraint(assocPath, correspondingAssociationPath3, assocSet2));
			}
			return result;
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000CD39C File Offset: 0x000CB59C
		private static bool AreAssocationEndPathsEquivalentViaRefConstraint(MemberPath assocPath0, MemberPath assocPath1, AssociationSet assocSet)
		{
			AssociationEndMember associationEndMember = assocPath0.RootEdmMember as AssociationEndMember;
			AssociationEndMember associationEndMember2 = assocPath1.RootEdmMember as AssociationEndMember;
			EdmProperty edmProperty = assocPath0.LeafEdmMember as EdmProperty;
			EdmProperty edmProperty2 = assocPath1.LeafEdmMember as EdmProperty;
			if (associationEndMember == null || associationEndMember2 == null || edmProperty == null || edmProperty2 == null)
			{
				return false;
			}
			AssociationType elementType = assocSet.ElementType;
			bool result = false;
			foreach (ReferentialConstraint referentialConstraint in elementType.ReferentialConstraints)
			{
				bool flag = associationEndMember.Name == referentialConstraint.FromRole.Name && associationEndMember2.Name == referentialConstraint.ToRole.Name;
				bool flag2 = associationEndMember2.Name == referentialConstraint.FromRole.Name && associationEndMember.Name == referentialConstraint.ToRole.Name;
				if (flag || flag2)
				{
					ReadOnlyMetadataCollection<EdmProperty> readOnlyMetadataCollection = flag ? referentialConstraint.FromProperties : referentialConstraint.ToProperties;
					ReadOnlyMetadataCollection<EdmProperty> readOnlyMetadataCollection2 = flag ? referentialConstraint.ToProperties : referentialConstraint.FromProperties;
					int num = readOnlyMetadataCollection.IndexOf(edmProperty);
					int num2 = readOnlyMetadataCollection2.IndexOf(edmProperty2);
					if (num == num2 && num != -1)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000CD500 File Offset: 0x000CB700
		private MemberPath GetCorrespondingAssociationPath(AssociationSet assocSet)
		{
			AssociationEndMember someEndForEntitySet = MetadataHelper.GetSomeEndForEntitySet(assocSet, this.m_extent);
			if (someEndForEntitySet == null)
			{
				return null;
			}
			List<EdmMember> list = new List<EdmMember>();
			list.Add(someEndForEntitySet);
			list.AddRange(this.m_path);
			return new MemberPath(assocSet, list);
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000CD544 File Offset: 0x000CB744
		internal EntitySet GetScopeOfRelationEnd()
		{
			if (this.m_path.Count == 0)
			{
				return null;
			}
			AssociationEndMember associationEndMember = this.LeafEdmMember as AssociationEndMember;
			if (associationEndMember == null)
			{
				return null;
			}
			AssociationSet associationSet = (AssociationSet)this.m_extent;
			return MetadataHelper.GetEntitySetAtEnd(associationSet, associationEndMember);
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000CD588 File Offset: 0x000CB788
		internal string PathToString(bool? forAlias)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (forAlias != null)
			{
				if (forAlias == true)
				{
					if (this.m_path.Count == 0)
					{
						EntityTypeBase elementType = this.m_extent.ElementType;
						return elementType.Name;
					}
					stringBuilder.Append(this.m_path[0].DeclaringType.Name);
				}
				else
				{
					stringBuilder.Append(this.m_extent.Name);
				}
			}
			for (int i = 0; i < this.m_path.Count; i++)
			{
				stringBuilder.Append('.');
				stringBuilder.Append(this.m_path[i].Name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000CD649 File Offset: 0x000CB849
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.PathToString(new bool?(false)));
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000CD660 File Offset: 0x000CB860
		internal void ToCompactString(StringBuilder builder, string instanceToken)
		{
			builder.Append(instanceToken + this.PathToString(null));
		}

		// Token: 0x04000FAB RID: 4011
		private readonly EntitySetBase m_extent;

		// Token: 0x04000FAC RID: 4012
		private readonly List<EdmMember> m_path;

		// Token: 0x04000FAD RID: 4013
		internal static readonly IEqualityComparer<MemberPath> EqualityComparer = new MemberPath.Comparer();

		// Token: 0x0200047E RID: 1150
		private sealed class Comparer : IEqualityComparer<MemberPath>
		{
			// Token: 0x06002A7C RID: 10876 RVA: 0x000CD698 File Offset: 0x000CB898
			public bool Equals(MemberPath left, MemberPath right)
			{
				if (object.ReferenceEquals(left, right))
				{
					return true;
				}
				if (left == null || right == null)
				{
					return false;
				}
				if (!left.m_extent.Equals(right.m_extent) || left.m_path.Count != right.m_path.Count)
				{
					return false;
				}
				for (int i = 0; i < left.m_path.Count; i++)
				{
					if (!left.m_path[i].Equals(right.m_path[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06002A7D RID: 10877 RVA: 0x000CD720 File Offset: 0x000CB920
			public int GetHashCode(MemberPath key)
			{
				int num = key.m_extent.GetHashCode();
				foreach (EdmMember edmMember in key.m_path)
				{
					num ^= edmMember.GetHashCode();
				}
				return num;
			}
		}
	}
}
