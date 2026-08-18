using System;
using System.Collections.Generic;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Globalization;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200028C RID: 652
	internal class FragmentQuery : ITileQuery
	{
		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x00098942 File Offset: 0x00096B42
		public HashSet<MemberPath> Attributes
		{
			get
			{
				return this.m_attributes;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x0009894A File Offset: 0x00096B4A
		public BoolExpression Condition
		{
			get
			{
				return this.m_condition;
			}
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x00098954 File Offset: 0x00096B54
		public static FragmentQuery Create(BoolExpression fromVariable, CellQuery cellQuery)
		{
			BoolExpression boolExpression = cellQuery.WhereClause;
			boolExpression = boolExpression.MakeCopy();
			boolExpression.ExpensiveSimplify();
			return new FragmentQuery(null, fromVariable, new HashSet<MemberPath>(cellQuery.GetProjectedMembers()), boolExpression);
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x00098988 File Offset: 0x00096B88
		public static FragmentQuery Create(string label, RoleBoolean roleBoolean, CellQuery cellQuery)
		{
			BoolExpression boolExpression = cellQuery.WhereClause.Create(roleBoolean);
			boolExpression = BoolExpression.CreateAnd(new BoolExpression[]
			{
				boolExpression,
				cellQuery.WhereClause
			});
			boolExpression = boolExpression.MakeCopy();
			boolExpression.ExpensiveSimplify();
			return new FragmentQuery(label, null, new HashSet<MemberPath>(), boolExpression);
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x000989D5 File Offset: 0x00096BD5
		public static FragmentQuery Create(IEnumerable<MemberPath> attrs, BoolExpression whereClause)
		{
			return new FragmentQuery(null, null, attrs, whereClause);
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x000989E0 File Offset: 0x00096BE0
		public static FragmentQuery Create(BoolExpression whereClause)
		{
			return new FragmentQuery(null, null, new MemberPath[0], whereClause);
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x000989F0 File Offset: 0x00096BF0
		internal FragmentQuery(string label, BoolExpression fromVariable, IEnumerable<MemberPath> attrs, BoolExpression condition)
		{
			this.m_label = label;
			this.m_fromVariable = fromVariable;
			this.m_condition = condition;
			this.m_attributes = new HashSet<MemberPath>(attrs);
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x00098A1A File Offset: 0x00096C1A
		public BoolExpression FromVariable
		{
			get
			{
				return this.m_fromVariable;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x00098A24 File Offset: 0x00096C24
		public string Description
		{
			get
			{
				string text = this.m_label;
				if (text == null && this.m_fromVariable != null)
				{
					text = this.m_fromVariable.ToString();
				}
				return text;
			}
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x00098A50 File Offset: 0x00096C50
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MemberPath memberPath in this.Attributes)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(memberPath.ToString());
			}
			if (this.Description != null && this.Description != stringBuilder.ToString())
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}: [{1} where {2}]", new object[]
				{
					this.Description,
					stringBuilder,
					this.Condition
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "[{0} where {1}]", new object[]
			{
				stringBuilder,
				this.Condition
			});
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x00098B2C File Offset: 0x00096D2C
		internal static BoolExpression CreateMemberCondition(MemberPath path, Constant domainValue, MemberDomainMap domainMap)
		{
			if (domainValue is TypeConstant)
			{
				return BoolExpression.CreateLiteral(new TypeRestriction(new MemberProjectedSlot(path), new Domain(domainValue, domainMap.GetDomain(path))), domainMap);
			}
			return BoolExpression.CreateLiteral(new ScalarRestriction(new MemberProjectedSlot(path), new Domain(domainValue, domainMap.GetDomain(path))), domainMap);
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x00098B7E File Offset: 0x00096D7E
		internal static IEqualityComparer<FragmentQuery> GetEqualityComparer(FragmentQueryProcessor qp)
		{
			return new FragmentQuery.FragmentQueryEqualityComparer(qp);
		}

		// Token: 0x040011FD RID: 4605
		private BoolExpression m_fromVariable;

		// Token: 0x040011FE RID: 4606
		private string m_label;

		// Token: 0x040011FF RID: 4607
		private HashSet<MemberPath> m_attributes;

		// Token: 0x04001200 RID: 4608
		private BoolExpression m_condition;

		// Token: 0x020005B8 RID: 1464
		private class FragmentQueryEqualityComparer : IEqualityComparer<FragmentQuery>
		{
			// Token: 0x0600409F RID: 16543 RVA: 0x000ED266 File Offset: 0x000EB466
			internal FragmentQueryEqualityComparer(FragmentQueryProcessor qp)
			{
				this._qp = qp;
			}

			// Token: 0x060040A0 RID: 16544 RVA: 0x000ED275 File Offset: 0x000EB475
			public bool Equals(FragmentQuery x, FragmentQuery y)
			{
				return x.Attributes.SetEquals(y.Attributes) && this._qp.IsEquivalentTo(x, y);
			}

			// Token: 0x060040A1 RID: 16545 RVA: 0x000ED29C File Offset: 0x000EB49C
			public int GetHashCode(FragmentQuery q)
			{
				int num = 0;
				foreach (MemberPath obj in q.Attributes)
				{
					num ^= MemberPath.EqualityComparer.GetHashCode(obj);
				}
				int num2 = 0;
				int num3 = 0;
				foreach (MemberRestriction memberRestriction in q.Condition.MemberRestrictions)
				{
					num2 ^= MemberPath.EqualityComparer.GetHashCode(memberRestriction.RestrictedMemberSlot.MemberPath);
					foreach (Constant obj2 in memberRestriction.Domain.Values)
					{
						num3 ^= Constant.EqualityComparer.GetHashCode(obj2);
					}
				}
				return num * 13 + num2 * 7 + num3;
			}

			// Token: 0x04001D11 RID: 7441
			private FragmentQueryProcessor _qp;
		}
	}
}
