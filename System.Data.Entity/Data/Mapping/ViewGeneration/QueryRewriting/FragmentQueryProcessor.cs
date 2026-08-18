using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Common.Utils.Boolean;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Globalization;
using System.Linq;

namespace System.Data.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200028A RID: 650
	internal class FragmentQueryProcessor : TileQueryProcessor<FragmentQuery>
	{
		// Token: 0x060026DC RID: 9948 RVA: 0x0009676E File Offset: 0x0009496E
		public FragmentQueryProcessor(FragmentQueryKB kb)
		{
			this._kb = kb;
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x00096780 File Offset: 0x00094980
		internal static FragmentQueryProcessor Merge(FragmentQueryProcessor qp1, FragmentQueryProcessor qp2)
		{
			FragmentQueryKB fragmentQueryKB = new FragmentQueryKB();
			fragmentQueryKB.AddKnowledgeBase(qp1.KnowledgeBase);
			fragmentQueryKB.AddKnowledgeBase(qp2.KnowledgeBase);
			return new FragmentQueryProcessor(fragmentQueryKB);
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x000967B1 File Offset: 0x000949B1
		internal FragmentQueryKB KnowledgeBase
		{
			get
			{
				return this._kb;
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x000967BC File Offset: 0x000949BC
		internal override FragmentQuery Union(FragmentQuery q1, FragmentQuery q2)
		{
			HashSet<MemberPath> hashSet = new HashSet<MemberPath>(q1.Attributes);
			hashSet.IntersectWith(q2.Attributes);
			BoolExpression whereClause = BoolExpression.CreateOr(new BoolExpression[]
			{
				q1.Condition,
				q2.Condition
			});
			return FragmentQuery.Create(hashSet, whereClause);
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x00096806 File Offset: 0x00094A06
		internal bool IsDisjointFrom(FragmentQuery q1, FragmentQuery q2)
		{
			return !this.IsSatisfiable(this.Intersect(q1, q2));
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x00096819 File Offset: 0x00094A19
		internal bool IsContainedIn(FragmentQuery q1, FragmentQuery q2)
		{
			return !this.IsSatisfiable(this.Difference(q1, q2));
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x0009682C File Offset: 0x00094A2C
		internal bool IsEquivalentTo(FragmentQuery q1, FragmentQuery q2)
		{
			return this.IsContainedIn(q1, q2) && this.IsContainedIn(q2, q1);
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x00096844 File Offset: 0x00094A44
		internal override FragmentQuery Intersect(FragmentQuery q1, FragmentQuery q2)
		{
			HashSet<MemberPath> hashSet = new HashSet<MemberPath>(q1.Attributes);
			hashSet.IntersectWith(q2.Attributes);
			BoolExpression whereClause = BoolExpression.CreateAnd(new BoolExpression[]
			{
				q1.Condition,
				q2.Condition
			});
			return FragmentQuery.Create(hashSet, whereClause);
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x0009688E File Offset: 0x00094A8E
		internal override FragmentQuery Difference(FragmentQuery qA, FragmentQuery qB)
		{
			return FragmentQuery.Create(qA.Attributes, BoolExpression.CreateAndNot(qA.Condition, qB.Condition));
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000968AC File Offset: 0x00094AAC
		internal override bool IsSatisfiable(FragmentQuery query)
		{
			return this.IsSatisfiable(query.Condition);
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000968BC File Offset: 0x00094ABC
		private bool IsSatisfiable(BoolExpression condition)
		{
			BoolExpression boolExpression = condition.Create(new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
			{
				this._kb.KbExpression,
				condition.Tree
			}));
			ConversionContext<DomainConstraint<BoolLiteral, Constant>> context = IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext();
			Converter<DomainConstraint<BoolLiteral, Constant>> converter = new Converter<DomainConstraint<BoolLiteral, Constant>>(boolExpression.Tree, context);
			return !converter.Vertex.IsZero();
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x0009691C File Offset: 0x00094B1C
		internal override FragmentQuery CreateDerivedViewBySelectingConstantAttributes(FragmentQuery view)
		{
			HashSet<MemberPath> hashSet = new HashSet<MemberPath>();
			IEnumerable<DomainVariable<BoolLiteral, Constant>> variables = view.Condition.Variables;
			foreach (DomainVariable<BoolLiteral, Constant> domainVariable in variables)
			{
				MemberRestriction memberRestriction = domainVariable.Identifier as MemberRestriction;
				if (memberRestriction != null)
				{
					MemberPath memberPath = memberRestriction.RestrictedMemberSlot.MemberPath;
					Domain domain = memberRestriction.Domain;
					if (!view.Attributes.Contains(memberPath))
					{
						if (!domain.AllPossibleValues.Any((Constant it) => it.HasNotNull()))
						{
							foreach (Constant constant in domain.Values)
							{
								DomainConstraint<BoolLiteral, Constant> identifier = new DomainConstraint<BoolLiteral, Constant>(domainVariable, new Set<Constant>(new Constant[]
								{
									constant
								}, Constant.EqualityComparer));
								BoolExpression condition = view.Condition.Create(new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
								{
									view.Condition.Tree,
									new NotExpr<DomainConstraint<BoolLiteral, Constant>>(new TermExpr<DomainConstraint<BoolLiteral, Constant>>(identifier))
								}));
								bool flag = !this.IsSatisfiable(condition);
								if (flag)
								{
									hashSet.Add(memberPath);
								}
							}
						}
					}
				}
			}
			if (hashSet.Count > 0)
			{
				hashSet.UnionWith(view.Attributes);
				return new FragmentQuery(string.Format(CultureInfo.InvariantCulture, "project({0})", new object[]
				{
					view.Description
				}), view.FromVariable, hashSet, view.Condition);
			}
			return null;
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x00096AF0 File Offset: 0x00094CF0
		public override string ToString()
		{
			return this._kb.ToString();
		}

		// Token: 0x040011E9 RID: 4585
		private FragmentQueryKB _kb;

		// Token: 0x020005B0 RID: 1456
		private class AttributeSetComparator : IEqualityComparer<HashSet<MemberPath>>
		{
			// Token: 0x0600407F RID: 16511 RVA: 0x000ECF93 File Offset: 0x000EB193
			public bool Equals(HashSet<MemberPath> x, HashSet<MemberPath> y)
			{
				return x.SetEquals(y);
			}

			// Token: 0x06004080 RID: 16512 RVA: 0x000ECF9C File Offset: 0x000EB19C
			public int GetHashCode(HashSet<MemberPath> attrs)
			{
				int num = 123;
				foreach (MemberPath obj in attrs)
				{
					num += MemberPath.EqualityComparer.GetHashCode(obj) * 7;
				}
				return num;
			}

			// Token: 0x04001CFD RID: 7421
			internal static readonly FragmentQueryProcessor.AttributeSetComparator DefaultInstance = new FragmentQueryProcessor.AttributeSetComparator();
		}
	}
}
