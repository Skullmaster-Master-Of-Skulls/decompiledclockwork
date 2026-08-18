using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002B0 RID: 688
	internal sealed class MemberPath : InternalBase, IEquatable<MemberPath>
	{
		// Token: 0x060028E6 RID: 10470 RVA: 0x0009E62B File Offset: 0x0009C82B
		internal MemberPath(EntitySetBase extent, IEnumerable<EdmMember> path)
		{
			this.m_extent = extent;
			this.m_path = path.ToList<EdmMember>();
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0009E646 File Offset: 0x0009C846
		internal MemberPath(EntitySetBase extent) : this(extent, Enumerable.Empty<EdmMember>())
		{
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0009E654 File Offset: 0x0009C854
		internal MemberPath(EntitySetBase extent, EdmMember member) : this(extent, Enumerable.Repeat<EdmMember>(member, 1))
		{
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x0009E664 File Offset: 0x0009C864
		internal MemberPath(MemberPath prefix, EdmMember last)
		{
			this.m_extent = prefix.m_extent;
			this.m_path = new List<EdmMember>(prefix.m_path);
			this.m_path.Add(last);
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060028EA RID: 10474 RVA: 0x0009E695 File Offset: 0x0009C895
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

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x0009E6B3 File Offset: 0x0009C8B3
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

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x0009E6DD File Offset: 0x0009C8DD
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

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x0009E703 File Offset: 0x0009C903
		internal bool IsComputed
		{
			get
			{
				return this.m_path.Count != 0 && this.RootEdmMember.IsStoreGeneratedComputed;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x0009E720 File Offset: 0x0009C920
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

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060028EF RID: 10479 RVA: 0x0009E763 File Offset: 0x0009C963
		internal bool IsPartOfKey
		{
			get
			{
				return this.m_path.Count != 0 && MetadataHelper.IsPartOfEntityTypeKey(this.LeafEdmMember);
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x0009E77F File Offset: 0x0009C97F
		internal bool IsNullable
		{
			get
			{
				return this.m_path.Count != 0 && MetadataHelper.IsMemberNullable(this.LeafEdmMember);
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x0009E79C File Offset: 0x0009C99C
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

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x0009E7F4 File Offset: 0x0009C9F4
		internal EntitySetBase Extent
		{
			get
			{
				return this.m_extent;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x0009E7FC File Offset: 0x0009C9FC
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

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x0009E828 File Offset: 0x0009CA28
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

		// Token: 0x060028F5 RID: 10485 RVA: 0x0009E870 File Offset: 0x0009CA70
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

		// Token: 0x060028F6 RID: 10486 RVA: 0x0009E95C File Offset: 0x0009CB5C
		private static bool RecurseToFindMemberAbsentInConcreteType(EntityType current, EntityType avoidEdge, EdmMember member, EntityType entitySetType, Dictionary<EntityType, Set<EntityType>> inheritanceGraph)
		{
			Set<EntityType> set = inheritanceGraph[current];
			IEnumerable<EntityType> source = set;
			Func<EntityType, bool> <>9__0;
			Func<EntityType, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((EntityType type) => !type.EdmEquals(avoidEdge)));
			}
			foreach (EntityType entityType in source.Where(predicate))
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

		// Token: 0x060028F7 RID: 10487 RVA: 0x0009EA20 File Offset: 0x0009CC20
		internal void GetIdentifiers(CqlIdentifiers identifiers)
		{
			identifiers.AddIdentifier(this.m_extent.Name);
			identifiers.AddIdentifier(this.m_extent.ElementType.Name);
			foreach (EdmMember edmMember in this.m_path)
			{
				identifiers.AddIdentifier(edmMember.Name);
			}
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x0009EAA0 File Offset: 0x0009CCA0
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

		// Token: 0x060028F9 RID: 10489 RVA: 0x0009EB04 File Offset: 0x0009CD04
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

		// Token: 0x060028FA RID: 10490 RVA: 0x0009EB8C File Offset: 0x0009CD8C
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
			inputBuilder.Append(builder.ToString());
			return inputBuilder;
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x0009EBF4 File Offset: 0x0009CDF4
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

		// Token: 0x060028FC RID: 10492 RVA: 0x0009EC40 File Offset: 0x0009CE40
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

		// Token: 0x060028FD RID: 10493 RVA: 0x0009ECFC File Offset: 0x0009CEFC
		public bool Equals(MemberPath right)
		{
			return MemberPath.EqualityComparer.Equals(this, right);
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x0009ED0C File Offset: 0x0009CF0C
		public override bool Equals(object obj)
		{
			MemberPath right = obj as MemberPath;
			return obj != null && this.Equals(right);
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x0009ED2C File Offset: 0x0009CF2C
		public override int GetHashCode()
		{
			return MemberPath.EqualityComparer.GetHashCode(this);
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x0009ED39 File Offset: 0x0009CF39
		internal bool IsScalarType()
		{
			return this.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType || this.EdmType.BuiltInTypeKind == BuiltInTypeKind.EnumType;
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x0009ED5C File Offset: 0x0009CF5C
		internal static IEnumerable<MemberPath> GetKeyMembers(EntitySetBase extent, MemberDomainMap domainMap)
		{
			MemberPath memberPath = new MemberPath(extent);
			return new List<MemberPath>(memberPath.GetMembers(memberPath.Extent.ElementType, null, null, new bool?(true), domainMap));
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x0009EDA1 File Offset: 0x0009CFA1
		internal IEnumerable<MemberPath> GetMembers(EdmType edmType, bool? isScalar, bool? isConditional, bool? isPartOfKey, MemberDomainMap domainMap)
		{
			StructuralType structuralType = (StructuralType)edmType;
			foreach (EdmMember edmMember in structuralType.Members)
			{
				if (edmMember is AssociationEndMember)
				{
					foreach (MemberPath memberPath in new MemberPath(this, edmMember).GetMembers(((RefType)edmMember.TypeUsage.EdmType).ElementType, isScalar, isConditional, new bool?(true), domainMap))
					{
						yield return memberPath;
					}
					IEnumerator<MemberPath> enumerator2 = null;
				}
				bool flag = MetadataHelper.IsNonRefSimpleMember(edmMember);
				if (isScalar == null)
				{
					goto IL_160;
				}
				bool? flag2 = isScalar;
				bool flag3 = flag;
				if (flag2.GetValueOrDefault() == flag3 & flag2 != null)
				{
					goto IL_160;
				}
				IL_212:
				edmMember = null;
				continue;
				IL_160:
				EdmProperty edmProperty = edmMember as EdmProperty;
				if (edmProperty != null)
				{
					bool flag4 = MetadataHelper.IsPartOfEntityTypeKey(edmProperty);
					if (isPartOfKey != null)
					{
						flag2 = isPartOfKey;
						flag3 = flag4;
						if (!(flag2.GetValueOrDefault() == flag3 & flag2 != null))
						{
							goto IL_212;
						}
					}
					MemberPath memberPath2 = new MemberPath(this, edmProperty);
					bool flag5 = domainMap.IsConditionMember(memberPath2);
					if (isConditional != null)
					{
						flag2 = isConditional;
						flag3 = flag5;
						if (!(flag2.GetValueOrDefault() == flag3 & flag2 != null))
						{
							goto IL_212;
						}
					}
					yield return memberPath2;
					goto IL_212;
				}
				goto IL_212;
			}
			ReadOnlyMetadataCollection<EdmMember>.Enumerator enumerator = default(ReadOnlyMetadataCollection<EdmMember>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x0009EDD8 File Offset: 0x0009CFD8
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
				EntitySet entitySet3 = (entitySet != null) ? entitySet : entitySet2;
				MemberPath assocPath = (this.Extent is AssociationSet) ? this : path1;
				MemberPath memberPath = (this.Extent is EntitySet) ? this : path1;
				MemberPath correspondingAssociationPath3 = memberPath.GetCorrespondingAssociationPath(assocSet2);
				result = (correspondingAssociationPath3 != null && MemberPath.AreAssocationEndPathsEquivalentViaRefConstraint(assocPath, correspondingAssociationPath3, assocSet2));
			}
			return result;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x0009EF44 File Offset: 0x0009D144
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

		// Token: 0x06002905 RID: 10501 RVA: 0x0009F0A8 File Offset: 0x0009D2A8
		private MemberPath GetCorrespondingAssociationPath(AssociationSet assocSet)
		{
			AssociationEndMember someEndForEntitySet = MetadataHelper.GetSomeEndForEntitySet(assocSet, (EntitySet)this.m_extent);
			if (someEndForEntitySet == null)
			{
				return null;
			}
			List<EdmMember> list = new List<EdmMember>();
			list.Add(someEndForEntitySet);
			list.AddRange(this.m_path);
			return new MemberPath(assocSet, list);
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x0009F0F0 File Offset: 0x0009D2F0
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

		// Token: 0x06002907 RID: 10503 RVA: 0x0009F134 File Offset: 0x0009D334
		internal string PathToString(bool? forAlias)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (forAlias != null)
			{
				bool? flag = forAlias;
				bool flag2 = true;
				if (flag.GetValueOrDefault() == flag2 & flag != null)
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

		// Token: 0x06002908 RID: 10504 RVA: 0x0009F1FB File Offset: 0x0009D3FB
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.PathToString(new bool?(false)));
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x0009F210 File Offset: 0x0009D410
		internal void ToCompactString(StringBuilder builder, string instanceToken)
		{
			builder.Append(instanceToken + this.PathToString(null));
		}

		// Token: 0x04001274 RID: 4724
		private readonly EntitySetBase m_extent;

		// Token: 0x04001275 RID: 4725
		private readonly List<EdmMember> m_path;

		// Token: 0x04001276 RID: 4726
		internal static readonly IEqualityComparer<MemberPath> EqualityComparer = new MemberPath.Comparer();

		// Token: 0x020005FA RID: 1530
		private sealed class Comparer : IEqualityComparer<MemberPath>
		{
			// Token: 0x06004228 RID: 16936 RVA: 0x000F052C File Offset: 0x000EE72C
			public bool Equals(MemberPath left, MemberPath right)
			{
				if (left == right)
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

			// Token: 0x06004229 RID: 16937 RVA: 0x000F05AC File Offset: 0x000EE7AC
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
