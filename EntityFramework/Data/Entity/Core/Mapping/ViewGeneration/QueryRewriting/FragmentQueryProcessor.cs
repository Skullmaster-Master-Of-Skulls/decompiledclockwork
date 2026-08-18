using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200043B RID: 1083
	internal class FragmentQueryProcessor : TileQueryProcessor<FragmentQuery>
	{
		// Token: 0x060027AC RID: 10156 RVA: 0x000C03F4 File Offset: 0x000BE5F4
		public FragmentQueryProcessor(FragmentQueryKBChaseSupport kb)
		{
			this._kb = kb;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000C0404 File Offset: 0x000BE604
		internal static FragmentQueryProcessor Merge(FragmentQueryProcessor qp1, FragmentQueryProcessor qp2)
		{
			FragmentQueryKBChaseSupport fragmentQueryKBChaseSupport = new FragmentQueryKBChaseSupport();
			fragmentQueryKBChaseSupport.AddKnowledgeBase(qp1.KnowledgeBase);
			fragmentQueryKBChaseSupport.AddKnowledgeBase(qp2.KnowledgeBase);
			return new FragmentQueryProcessor(fragmentQueryKBChaseSupport);
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060027AE RID: 10158 RVA: 0x000C0435 File Offset: 0x000BE635
		internal FragmentQueryKB KnowledgeBase
		{
			get
			{
				return this._kb;
			}
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000C0440 File Offset: 0x000BE640
		[SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCode", Justification = "Based on Bug VSTS Pioneer #433188: IsVisibleOutsideAssembly is wrong on generic instantiations.")]
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

		// Token: 0x060027B0 RID: 10160 RVA: 0x000C048C File Offset: 0x000BE68C
		internal bool IsDisjointFrom(FragmentQuery q1, FragmentQuery q2)
		{
			return !this.IsSatisfiable(this.Intersect(q1, q2));
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000C049F File Offset: 0x000BE69F
		internal bool IsContainedIn(FragmentQuery q1, FragmentQuery q2)
		{
			return !this.IsSatisfiable(this.Difference(q1, q2));
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000C04B2 File Offset: 0x000BE6B2
		internal bool IsEquivalentTo(FragmentQuery q1, FragmentQuery q2)
		{
			return this.IsContainedIn(q1, q2) && this.IsContainedIn(q2, q1);
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000C04C8 File Offset: 0x000BE6C8
		[SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCode", Justification = "Based on Bug VSTS Pioneer #433188: IsVisibleOutsideAssembly is wrong on generic instantiations.")]
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

		// Token: 0x060027B4 RID: 10164 RVA: 0x000C0514 File Offset: 0x000BE714
		internal override FragmentQuery Difference(FragmentQuery qA, FragmentQuery qB)
		{
			return FragmentQuery.Create(qA.Attributes, BoolExpression.CreateAndNot(qA.Condition, qB.Condition));
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000C0532 File Offset: 0x000BE732
		internal override bool IsSatisfiable(FragmentQuery query)
		{
			return this.IsSatisfiable(query.Condition);
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000C0540 File Offset: 0x000BE740
		private bool IsSatisfiable(BoolExpression condition)
		{
			return this._kb.IsSatisfiable(condition.Tree);
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x000C055C File Offset: 0x000BE75C
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

		// Token: 0x060027B8 RID: 10168 RVA: 0x000C0744 File Offset: 0x000BE944
		public override string ToString()
		{
			return this._kb.ToString();
		}

		// Token: 0x04000EF7 RID: 3831
		private readonly FragmentQueryKBChaseSupport _kb;

		// Token: 0x0200043C RID: 1084
		private class AttributeSetComparator : IEqualityComparer<HashSet<MemberPath>>
		{
			// Token: 0x060027BA RID: 10170 RVA: 0x000C0751 File Offset: 0x000BE951
			[SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCode", Justification = "Based on Bug VSTS Pioneer #433188: IsVisibleOutsideAssembly is wrong on generic instantiations.")]
			public bool Equals(HashSet<MemberPath> x, HashSet<MemberPath> y)
			{
				return x.SetEquals(y);
			}

			// Token: 0x060027BB RID: 10171 RVA: 0x000C075C File Offset: 0x000BE95C
			public int GetHashCode(HashSet<MemberPath> attrs)
			{
				int num = 123;
				foreach (MemberPath obj in attrs)
				{
					num += MemberPath.EqualityComparer.GetHashCode(obj) * 7;
				}
				return num;
			}
		}
	}
}
