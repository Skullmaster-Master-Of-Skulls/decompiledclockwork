using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Common.Utils.Boolean;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000289 RID: 649
	internal class FragmentQueryKB : KnowledgeBase<DomainConstraint<BoolLiteral, Constant>>
	{
		// Token: 0x060026D4 RID: 9940 RVA: 0x0009615C File Offset: 0x0009435C
		internal override void AddFact(BoolExpr<DomainConstraint<BoolLiteral, Constant>> fact)
		{
			base.AddFact(fact);
			this._kbExpression = new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
			{
				this._kbExpression,
				fact
			});
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x00096183 File Offset: 0x00094383
		internal BoolExpr<DomainConstraint<BoolLiteral, Constant>> KbExpression
		{
			get
			{
				return this._kbExpression;
			}
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x0009618B File Offset: 0x0009438B
		internal void CreateVariableConstraints(EntitySetBase extent, MemberDomainMap domainMap, EdmItemCollection edmItemCollection)
		{
			this.CreateVariableConstraintsRecursion(extent.ElementType, new MemberPath(extent), domainMap, edmItemCollection);
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x000961A4 File Offset: 0x000943A4
		internal void CreateAssociationConstraints(EntitySetBase extent, MemberDomainMap domainMap, EdmItemCollection edmItemCollection)
		{
			AssociationSet associationSet = extent as AssociationSet;
			if (associationSet != null)
			{
				BoolExpression boolExpression = BoolExpression.CreateLiteral(new RoleBoolean(associationSet), domainMap);
				HashSet<Pair<EdmMember, EntityType>> associationkeys = new HashSet<Pair<EdmMember, EntityType>>();
				foreach (AssociationEndMember associationEndMember in associationSet.ElementType.AssociationEndMembers)
				{
					EntityType type = (EntityType)((RefType)associationEndMember.TypeUsage.EdmType).ElementType;
					type.KeyMembers.All((EdmMember member) => associationkeys.Add(new Pair<EdmMember, EntityType>(member, type)) || true);
				}
				foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
				{
					HashSet<EdmType> hashSet = new HashSet<EdmType>();
					hashSet.UnionWith(MetadataHelper.GetTypeAndSubtypesOf(associationSetEnd.CorrespondingAssociationEndMember.TypeUsage.EdmType, edmItemCollection, false));
					BoolExpression boolExpression2 = FragmentQueryKB.CreateIsOfTypeCondition(new MemberPath(associationSetEnd.EntitySet), hashSet, domainMap);
					BoolExpression boolExpression3 = BoolExpression.CreateLiteral(new RoleBoolean(associationSetEnd), domainMap);
					BoolExpression boolExpression4 = BoolExpression.CreateAnd(new BoolExpression[]
					{
						BoolExpression.CreateLiteral(new RoleBoolean(associationSetEnd.EntitySet), domainMap),
						boolExpression2
					});
					base.AddImplication(boolExpression3.Tree, boolExpression4.Tree);
					if (MetadataHelper.IsEveryOtherEndAtLeastOne(associationSet, associationSetEnd.CorrespondingAssociationEndMember))
					{
						base.AddImplication(boolExpression4.Tree, boolExpression3.Tree);
					}
					if (MetadataHelper.DoesEndKeySubsumeAssociationSetKey(associationSet, associationSetEnd.CorrespondingAssociationEndMember, associationkeys))
					{
						base.AddEquivalence(boolExpression3.Tree, boolExpression.Tree);
					}
				}
				AssociationType elementType = associationSet.ElementType;
				foreach (ReferentialConstraint referentialConstraint in elementType.ReferentialConstraints)
				{
					AssociationEndMember endMember = (AssociationEndMember)referentialConstraint.ToRole;
					EntitySet entitySetAtEnd = MetadataHelper.GetEntitySetAtEnd(associationSet, endMember);
					IEnumerable<EdmMember> list = Helpers.AsSuperTypeList<EdmProperty, EdmMember>(referentialConstraint.ToProperties);
					if (Helpers.IsSetEqual<EdmMember>(list, entitySetAtEnd.ElementType.KeyMembers, EqualityComparer<EdmMember>.Default) && referentialConstraint.FromRole.RelationshipMultiplicity.Equals(RelationshipMultiplicity.One))
					{
						BoolExpression boolExpression5 = BoolExpression.CreateLiteral(new RoleBoolean(associationSet.AssociationSetEnds[0]), domainMap);
						BoolExpression boolExpression6 = BoolExpression.CreateLiteral(new RoleBoolean(associationSet.AssociationSetEnds[1]), domainMap);
						base.AddEquivalence(boolExpression5.Tree, boolExpression6.Tree);
					}
				}
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0009647C File Offset: 0x0009467C
		internal void CreateEquivalenceConstraintForOneToOneForeignKeyAssociation(AssociationSet assocSet, MemberDomainMap domainMap, EdmItemCollection edmItemCollection)
		{
			AssociationType elementType = assocSet.ElementType;
			foreach (ReferentialConstraint referentialConstraint in elementType.ReferentialConstraints)
			{
				AssociationEndMember endMember = (AssociationEndMember)referentialConstraint.ToRole;
				AssociationEndMember endMember2 = (AssociationEndMember)referentialConstraint.FromRole;
				EntitySet entitySetAtEnd = MetadataHelper.GetEntitySetAtEnd(assocSet, endMember);
				EntitySet entitySetAtEnd2 = MetadataHelper.GetEntitySetAtEnd(assocSet, endMember2);
				IEnumerable<EdmMember> list = Helpers.AsSuperTypeList<EdmProperty, EdmMember>(referentialConstraint.ToProperties);
				if (Helpers.IsSetEqual<EdmMember>(list, entitySetAtEnd.ElementType.KeyMembers, EqualityComparer<EdmMember>.Default))
				{
					BoolExpression boolExpression = BoolExpression.CreateLiteral(new RoleBoolean(entitySetAtEnd2), domainMap);
					BoolExpression boolExpression2 = BoolExpression.CreateLiteral(new RoleBoolean(entitySetAtEnd), domainMap);
					base.AddEquivalence(boolExpression.Tree, boolExpression2.Tree);
				}
			}
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x0009655C File Offset: 0x0009475C
		private void CreateVariableConstraintsRecursion(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap, EdmItemCollection edmItemCollection)
		{
			HashSet<EdmType> hashSet = new HashSet<EdmType>();
			hashSet.UnionWith(MetadataHelper.GetTypeAndSubtypesOf(edmType, edmItemCollection, true));
			foreach (EdmType edmType2 in hashSet)
			{
				HashSet<EdmType> hashSet2 = new HashSet<EdmType>();
				hashSet2.UnionWith(MetadataHelper.GetTypeAndSubtypesOf(edmType2, edmItemCollection, false));
				if (hashSet2.Count != 0)
				{
					BoolExpression expression = FragmentQueryKB.CreateIsOfTypeCondition(currentPath, hashSet2, domainMap);
					BoolExpression boolExpression = BoolExpression.CreateNot(expression);
					if (boolExpression.IsSatisfiable())
					{
						StructuralType structuralType = (StructuralType)edmType2;
						foreach (EdmProperty edmProperty in structuralType.GetDeclaredOnlyMembers<EdmProperty>())
						{
							MemberPath memberPath = new MemberPath(currentPath, edmProperty);
							bool flag = MetadataHelper.IsNonRefSimpleMember(edmProperty);
							if (domainMap.IsConditionMember(memberPath) || domainMap.IsProjectedConditionMember(memberPath))
							{
								List<Constant> possibleDiscreteValues = new List<Constant>(domainMap.GetDomain(memberPath));
								BoolExpression boolExpression2;
								if (flag)
								{
									boolExpression2 = BoolExpression.CreateLiteral(new ScalarRestriction(new MemberProjectedSlot(memberPath), new Domain(Constant.Undefined, possibleDiscreteValues)), domainMap);
								}
								else
								{
									boolExpression2 = BoolExpression.CreateLiteral(new TypeRestriction(new MemberProjectedSlot(memberPath), new Domain(Constant.Undefined, possibleDiscreteValues)), domainMap);
								}
								base.AddEquivalence(boolExpression.Tree, boolExpression2.Tree);
							}
							if (!flag)
							{
								this.CreateVariableConstraintsRecursion(memberPath.EdmType, memberPath, domainMap, edmItemCollection);
							}
						}
					}
				}
			}
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x00096708 File Offset: 0x00094908
		private static BoolExpression CreateIsOfTypeCondition(MemberPath currentPath, IEnumerable<EdmType> derivedTypes, MemberDomainMap domainMap)
		{
			Domain domain = new Domain(from derivedType in derivedTypes
			select new TypeConstant(derivedType), domainMap.GetDomain(currentPath));
			return BoolExpression.CreateLiteral(new TypeRestriction(new MemberProjectedSlot(currentPath), domain), domainMap);
		}

		// Token: 0x040011E8 RID: 4584
		private BoolExpr<DomainConstraint<BoolLiteral, Constant>> _kbExpression = TrueExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
	}
}
