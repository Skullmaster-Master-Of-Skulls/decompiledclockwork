using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000438 RID: 1080
	internal class FragmentQuery : ITileQuery
	{
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x000C005B File Offset: 0x000BE25B
		public HashSet<MemberPath> Attributes
		{
			get
			{
				return this.m_attributes;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x000C0063 File Offset: 0x000BE263
		public BoolExpression Condition
		{
			get
			{
				return this.m_condition;
			}
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000C006C File Offset: 0x000BE26C
		public static FragmentQuery Create(BoolExpression fromVariable, CellQuery cellQuery)
		{
			BoolExpression boolExpression = cellQuery.WhereClause;
			boolExpression = boolExpression.MakeCopy();
			boolExpression.ExpensiveSimplify();
			return new FragmentQuery(null, fromVariable, new HashSet<MemberPath>(cellQuery.GetProjectedMembers()), boolExpression);
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x000C00A0 File Offset: 0x000BE2A0
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

		// Token: 0x0600279B RID: 10139 RVA: 0x000C00EF File Offset: 0x000BE2EF
		public static FragmentQuery Create(IEnumerable<MemberPath> attrs, BoolExpression whereClause)
		{
			return new FragmentQuery(null, null, attrs, whereClause);
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000C00FA File Offset: 0x000BE2FA
		public static FragmentQuery Create(BoolExpression whereClause)
		{
			return new FragmentQuery(null, null, new MemberPath[0], whereClause);
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x000C010A File Offset: 0x000BE30A
		internal FragmentQuery(string label, BoolExpression fromVariable, IEnumerable<MemberPath> attrs, BoolExpression condition)
		{
			this.m_label = label;
			this.m_fromVariable = fromVariable;
			this.m_condition = condition;
			this.m_attributes = new HashSet<MemberPath>(attrs);
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x000C0134 File Offset: 0x000BE334
		public BoolExpression FromVariable
		{
			get
			{
				return this.m_fromVariable;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x000C013C File Offset: 0x000BE33C
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

		// Token: 0x060027A0 RID: 10144 RVA: 0x000C0168 File Offset: 0x000BE368
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MemberPath value in this.Attributes)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(value);
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

		// Token: 0x060027A1 RID: 10145 RVA: 0x000C0248 File Offset: 0x000BE448
		internal static BoolExpression CreateMemberCondition(MemberPath path, Constant domainValue, MemberDomainMap domainMap)
		{
			if (domainValue is TypeConstant)
			{
				return BoolExpression.CreateLiteral(new TypeRestriction(new MemberProjectedSlot(path), new Domain(domainValue, domainMap.GetDomain(path))), domainMap);
			}
			return BoolExpression.CreateLiteral(new ScalarRestriction(new MemberProjectedSlot(path), new Domain(domainValue, domainMap.GetDomain(path))), domainMap);
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000C029A File Offset: 0x000BE49A
		internal static IEqualityComparer<FragmentQuery> GetEqualityComparer(FragmentQueryProcessor qp)
		{
			return new FragmentQuery.FragmentQueryEqualityComparer(qp);
		}

		// Token: 0x04000EF2 RID: 3826
		private readonly BoolExpression m_fromVariable;

		// Token: 0x04000EF3 RID: 3827
		private readonly string m_label;

		// Token: 0x04000EF4 RID: 3828
		private readonly HashSet<MemberPath> m_attributes;

		// Token: 0x04000EF5 RID: 3829
		private readonly BoolExpression m_condition;

		// Token: 0x02000439 RID: 1081
		private class FragmentQueryEqualityComparer : IEqualityComparer<FragmentQuery>
		{
			// Token: 0x060027A3 RID: 10147 RVA: 0x000C02A2 File Offset: 0x000BE4A2
			internal FragmentQueryEqualityComparer(FragmentQueryProcessor qp)
			{
				this._qp = qp;
			}

			// Token: 0x060027A4 RID: 10148 RVA: 0x000C02B1 File Offset: 0x000BE4B1
			[SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCode", Justification = "Based on Bug VSTS Pioneer #433188: IsVisibleOutsideAssembly is wrong on generic instantiations.")]
			public bool Equals(FragmentQuery x, FragmentQuery y)
			{
				return x.Attributes.SetEquals(y.Attributes) && this._qp.IsEquivalentTo(x, y);
			}

			// Token: 0x060027A5 RID: 10149 RVA: 0x000C02D8 File Offset: 0x000BE4D8
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

			// Token: 0x04000EF6 RID: 3830
			private readonly FragmentQueryProcessor _qp;
		}
	}
}
