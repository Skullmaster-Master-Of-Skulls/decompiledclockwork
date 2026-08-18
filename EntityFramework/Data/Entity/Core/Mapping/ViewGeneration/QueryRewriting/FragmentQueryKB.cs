using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x020001E6 RID: 486
	internal class FragmentQueryKB : KnowledgeBase<DomainConstraint<BoolLiteral, Constant>>
	{
		// Token: 0x06001112 RID: 4370 RVA: 0x00048724 File Offset: 0x00046924
		internal override void AddFact(BoolExpr<DomainConstraint<BoolLiteral, Constant>> fact)
		{
			base.AddFact(fact);
			this._kbExpression = new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
			{
				this._kbExpression,
				fact
			});
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00048758 File Offset: 0x00046958
		internal BoolExpr<DomainConstraint<BoolLiteral, Constant>> KbExpression
		{
			get
			{
				return this._kbExpression;
			}
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00048760 File Offset: 0x00046960
		internal void CreateVariableConstraints(EntitySetBase extent, MemberDomainMap domainMap, EdmItemCollection edmItemCollection)
		{
			this.CreateVariableConstraintsRecursion(extent.ElementType, new MemberPath(extent), domainMap, edmItemCollection);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000487A8 File Offset: 0x000469A8
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
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
					type.KeyMembers.All(delegate(EdmMember member)
					{
						associationkeys.Add(new Pair<EdmMember, EntityType>(member, type));
						return true;
					});
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

		// Token: 0x06001116 RID: 4374 RVA: 0x00048A80 File Offset: 0x00046C80
		internal void CreateEquivalenceConstraintForOneToOneForeignKeyAssociation(AssociationSet assocSet, MemberDomainMap domainMap)
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

		// Token: 0x06001117 RID: 4375 RVA: 0x00048B60 File Offset: 0x00046D60
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

		// Token: 0x06001118 RID: 4376 RVA: 0x00048D14 File Offset: 0x00046F14
		private static BoolExpression CreateIsOfTypeCondition(MemberPath currentPath, IEnumerable<EdmType> derivedTypes, MemberDomainMap domainMap)
		{
			Domain domain = new Domain(from derivedType in derivedTypes
			select new TypeConstant(derivedType), domainMap.GetDomain(currentPath));
			return BoolExpression.CreateLiteral(new TypeRestriction(new MemberProjectedSlot(currentPath), domain), domainMap);
		}

		// Token: 0x0400051A RID: 1306
		private BoolExpr<DomainConstraint<BoolLiteral, Constant>> _kbExpression = TrueExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
	}
}
