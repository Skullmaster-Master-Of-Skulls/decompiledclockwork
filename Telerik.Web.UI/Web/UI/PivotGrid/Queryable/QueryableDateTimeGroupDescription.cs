using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Groups;
using Telerik.Web.UI.PivotGrid.Queryable.Groups;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x02000D63 RID: 3427
	[DataContract]
	public sealed class QueryableDateTimeGroupDescription : QueryablePropertyGroupDescriptionBase
	{
		// Token: 0x170028A9 RID: 10409
		// (get) Token: 0x06007FC9 RID: 32713 RVA: 0x001D2F86 File Offset: 0x001D1186
		// (set) Token: 0x06007FCA RID: 32714 RVA: 0x001D2F8E File Offset: 0x001D118E
		[DataMember]
		public DateTimeStep Step
		{
			get
			{
				return this.step;
			}
			set
			{
				if (this.step != value)
				{
					this.step = value;
					base.OnPropertyChanged("Step");
				}
			}
		}

		// Token: 0x170028AA RID: 10410
		// (get) Token: 0x06007FCB RID: 32715 RVA: 0x001D2FAB File Offset: 0x001D11AB
		private static Calendar Calendar
		{
			get
			{
				return CultureInfo.InvariantCulture.Calendar;
			}
		}

		// Token: 0x170028AB RID: 10411
		// (get) Token: 0x06007FCC RID: 32716 RVA: 0x001D2FB7 File Offset: 0x001D11B7
		protected internal override bool NeedsProcessing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170028AC RID: 10412
		// (get) Token: 0x06007FCD RID: 32717 RVA: 0x001D2FBA File Offset: 0x001D11BA
		internal override bool TransformsData
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007FCE RID: 32718 RVA: 0x001D2FC0 File Offset: 0x001D11C0
		public override string GetUniqueName()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				base.PropertyName,
				this.Step
			});
		}

		// Token: 0x06007FCF RID: 32719 RVA: 0x001D3000 File Offset: 0x001D1200
		protected internal override IEnumerable<object> GetAllNames(IEnumerable<object> uniqueNames, IEnumerable<object> parentGroupNames)
		{
			IEnumerable<object> result = null;
			switch (this.Step)
			{
			case DateTimeStep.Year:
				result = uniqueNames;
				break;
			case DateTimeStep.Quarter:
				result = QueryableDateTimeGroupDescription.QuartersInYear();
				break;
			case DateTimeStep.Month:
			{
				QuarterGroup quarterGroup;
				if (QueryableDateTimeGroupDescription.FirstOrDefault<QuarterGroup>(parentGroupNames, out quarterGroup))
				{
					result = QueryableDateTimeGroupDescription.MonthsInQuarter(quarterGroup.Quarter);
				}
				else
				{
					result = QueryableDateTimeGroupDescription.MonthsInYear();
				}
				break;
			}
			case DateTimeStep.Day:
			{
				YearGroup yearGroup;
				bool flag = QueryableDateTimeGroupDescription.FirstOrDefault<YearGroup>(parentGroupNames, out yearGroup);
				MonthGroup monthGroup;
				bool flag2 = QueryableDateTimeGroupDescription.FirstOrDefault<MonthGroup>(parentGroupNames, out monthGroup);
				if (!flag && !flag2)
				{
					result = QueryableDateTimeGroupDescription.DaysInLeapYear();
				}
				else if (flag && flag2)
				{
					result = QueryableDateTimeGroupDescription.DaysInMonth(yearGroup.Year, monthGroup.Month);
				}
				else if (flag)
				{
					result = QueryableDateTimeGroupDescription.DaysInYear(yearGroup.Year);
				}
				else
				{
					result = QueryableDateTimeGroupDescription.DaysInMonth(monthGroup.Month);
				}
				break;
			}
			}
			return result;
		}

		// Token: 0x06007FD0 RID: 32720 RVA: 0x001D30C8 File Offset: 0x001D12C8
		internal override object ProcessGroupItem(object data)
		{
			QueryableGroup queryableGroup = data as QueryableGroup;
			if (queryableGroup == null || !queryableGroup.IsValid)
			{
				return null;
			}
			QueryableYearGroup queryableYearGroup = data as QueryableYearGroup;
			if (queryableYearGroup != null)
			{
				YearGroup yearGroup = new YearGroup(queryableYearGroup.Year);
				return yearGroup;
			}
			QueryableQuarterGroup queryableQuarterGroup = data as QueryableQuarterGroup;
			if (queryableQuarterGroup != null)
			{
				QuarterGroup quarterGroup = new QuarterGroup(queryableQuarterGroup.Quarter);
				return quarterGroup;
			}
			QueryableMonthGroup queryableMonthGroup = data as QueryableMonthGroup;
			if (queryableMonthGroup != null)
			{
				MonthGroup monthGroup = new MonthGroup(queryableMonthGroup.Month);
				return monthGroup;
			}
			QueryableDayGroup queryableDayGroup = data as QueryableDayGroup;
			if (queryableDayGroup != null)
			{
				DayGroup dayGroup = new DayGroup(queryableDayGroup.Month, queryableDayGroup.Day);
				return dayGroup;
			}
			return null;
		}

		// Token: 0x06007FD1 RID: 32721 RVA: 0x001D325C File Offset: 0x001D145C
		private static IEnumerable<object> MonthsInYear()
		{
			for (int i = 1; i <= 12; i++)
			{
				yield return new MonthGroup(i);
			}
			yield break;
		}

		// Token: 0x06007FD2 RID: 32722 RVA: 0x001D3398 File Offset: 0x001D1598
		private static IEnumerable<object> MonthsInQuarter(int quarter)
		{
			if (quarter < 1 || quarter > 4)
			{
				throw new ArgumentException("Must be between 1 and 4.", "quarter");
			}
			for (int month = quarter * 3 - 2; month <= quarter * 3; month++)
			{
				yield return new MonthGroup(month);
			}
			yield break;
		}

		// Token: 0x06007FD3 RID: 32723 RVA: 0x001D34A0 File Offset: 0x001D16A0
		private static IEnumerable<object> QuartersInYear()
		{
			for (int i = 1; i <= 4; i++)
			{
				yield return new QuarterGroup(i);
			}
			yield break;
		}

		// Token: 0x06007FD4 RID: 32724 RVA: 0x001D34B8 File Offset: 0x001D16B8
		private static bool FirstOrDefault<T>(IEnumerable<object> source, out T value) where T : struct
		{
			if (source != null)
			{
				foreach (object obj in source)
				{
					if (obj is T)
					{
						value = (T)((object)obj);
						return true;
					}
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x06007FD5 RID: 32725 RVA: 0x001D3648 File Offset: 0x001D1848
		private static IEnumerable<object> DaysInLeapYear()
		{
			for (int month = 1; month <= 12; month++)
			{
				int daysInMonth = QueryableDateTimeGroupDescription.daysCountInMonth[month - 1];
				for (int day = 1; day <= daysInMonth; day++)
				{
					yield return new DayGroup(month, day);
				}
			}
			yield break;
		}

		// Token: 0x06007FD6 RID: 32726 RVA: 0x001D37A0 File Offset: 0x001D19A0
		private static IEnumerable<object> DaysInNonLeapYear()
		{
			for (int month = 1; month <= 12; month++)
			{
				int daysInMonth = QueryableDateTimeGroupDescription.daysCountInMonth[month - 1];
				if (month == 2)
				{
					daysInMonth = 28;
				}
				for (int day = 1; day <= daysInMonth; day++)
				{
					yield return new DayGroup(month, day);
				}
			}
			yield break;
		}

		// Token: 0x06007FD7 RID: 32727 RVA: 0x001D37B6 File Offset: 0x001D19B6
		private static IEnumerable<object> DaysInYear(int year)
		{
			if (QueryableDateTimeGroupDescription.Calendar.IsLeapYear(year))
			{
				return QueryableDateTimeGroupDescription.DaysInLeapYear();
			}
			return QueryableDateTimeGroupDescription.DaysInNonLeapYear();
		}

		// Token: 0x06007FD8 RID: 32728 RVA: 0x001D37D0 File Offset: 0x001D19D0
		private static IEnumerable<object> DaysInMonth(int year, int month)
		{
			if (month == 2 && !QueryableDateTimeGroupDescription.Calendar.IsLeapYear(year))
			{
				return QueryableDateTimeGroupDescription.NonLeapDaysInDebruary();
			}
			return QueryableDateTimeGroupDescription.DaysInMonth(month);
		}

		// Token: 0x06007FD9 RID: 32729 RVA: 0x001D3928 File Offset: 0x001D1B28
		private static IEnumerable<object> DaysInMonth(int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentException("Must be between 1 and 12.", "month");
			}
			int daysInMonth = QueryableDateTimeGroupDescription.daysCountInMonth[month - 1];
			for (int day = 1; day <= daysInMonth; day++)
			{
				yield return new DayGroup(month, day);
			}
			yield break;
		}

		// Token: 0x06007FDA RID: 32730 RVA: 0x001D3A30 File Offset: 0x001D1C30
		private static IEnumerable<object> NonLeapDaysInDebruary()
		{
			for (int day = 1; day <= 28; day++)
			{
				yield return new DayGroup(2, day);
			}
			yield break;
		}

		// Token: 0x06007FDB RID: 32731 RVA: 0x001D3A48 File Offset: 0x001D1C48
		protected internal override Expression CreateGroupKeyExpression(IEnumerable<Expression> valueExpressions)
		{
			List<Expression> list = valueExpressions.ToList<Expression>();
			switch (this.Step)
			{
			case DateTimeStep.Year:
				return QueryableDateTimeGroupDescription.GenerateYearGroupInitExpression(list[0], list[1]);
			case DateTimeStep.Quarter:
				return QueryableDateTimeGroupDescription.GenerateQuarterGroupInitExpression(list[0], list[1]);
			case DateTimeStep.Month:
				return QueryableDateTimeGroupDescription.GenerateMonthGroupInitExpression(list[0], list[1]);
			case DateTimeStep.Day:
				return QueryableDateTimeGroupDescription.GenerateDayGroupInitExpression(list[0], list[1], list[2]);
			}
			return null;
		}

		// Token: 0x06007FDC RID: 32732 RVA: 0x001D3AD8 File Offset: 0x001D1CD8
		private static Expression GenerateYearGroupInitExpression(Expression yearPropertyAccess, Expression isValidExpression)
		{
			Type typeFromHandle = typeof(QueryableYearGroup);
			NewExpression newExpression = Expression.New(typeFromHandle);
			MemberAssignment memberAssignment = Expression.Bind(typeFromHandle.GetProperty("Year"), yearPropertyAccess);
			MemberAssignment memberAssignment2 = Expression.Bind(typeFromHandle.GetProperty("IsValid"), isValidExpression);
			MemberAssignment[] bindings = new MemberAssignment[]
			{
				memberAssignment,
				memberAssignment2
			};
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x06007FDD RID: 32733 RVA: 0x001D3B40 File Offset: 0x001D1D40
		private static Expression GenerateQuarterGroupInitExpression(Expression propertyAccessExpression, Expression isValidExpression)
		{
			Type typeFromHandle = typeof(QueryableQuarterGroup);
			NewExpression newExpression = Expression.New(typeFromHandle);
			MemberAssignment memberAssignment = Expression.Bind(typeFromHandle.GetProperty("Quarter"), propertyAccessExpression);
			MemberAssignment memberAssignment2 = Expression.Bind(typeFromHandle.GetProperty("IsValid"), isValidExpression);
			MemberAssignment[] bindings = new MemberAssignment[]
			{
				memberAssignment,
				memberAssignment2
			};
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x06007FDE RID: 32734 RVA: 0x001D3BA8 File Offset: 0x001D1DA8
		private static Expression GenerateMonthGroupInitExpression(Expression expression, Expression isValidExpression)
		{
			Type typeFromHandle = typeof(QueryableMonthGroup);
			NewExpression newExpression = Expression.New(typeFromHandle);
			MemberAssignment memberAssignment = Expression.Bind(typeFromHandle.GetProperty("Month"), expression);
			MemberAssignment memberAssignment2 = Expression.Bind(typeFromHandle.GetProperty("IsValid"), isValidExpression);
			MemberAssignment[] bindings = new MemberAssignment[]
			{
				memberAssignment,
				memberAssignment2
			};
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x06007FDF RID: 32735 RVA: 0x001D3C10 File Offset: 0x001D1E10
		private static Expression GenerateDayGroupInitExpression(Expression dayPropertyAccessExpression, Expression monthPropertyAccessExpression, Expression isValidExpression)
		{
			Type typeFromHandle = typeof(QueryableDayGroup);
			NewExpression newExpression = Expression.New(typeFromHandle);
			MemberAssignment memberAssignment = Expression.Bind(typeFromHandle.GetProperty("Month"), monthPropertyAccessExpression);
			MemberAssignment memberAssignment2 = Expression.Bind(typeFromHandle.GetProperty("Day"), dayPropertyAccessExpression);
			MemberAssignment memberAssignment3 = Expression.Bind(typeFromHandle.GetProperty("IsValid"), isValidExpression);
			MemberAssignment[] bindings = new MemberAssignment[]
			{
				memberAssignment,
				memberAssignment2,
				memberAssignment3
			};
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x06007FE0 RID: 32736 RVA: 0x001D3C90 File Offset: 0x001D1E90
		protected internal override IEnumerable<Expression> CreateGroupKeyValuesExpressions(ParameterExpression itemExpression)
		{
			Expression memberAccess = QueryableExpressionHelper.MakeMemberAccess(itemExpression, base.PropertyName);
			Expression propertyAccessExpression = QueryableExpressionHelper.LiftMemberAccessToNull(memberAccess);
			Expression item = QueryableExpressionHelper.LiftMemberAccessToNull(memberAccess, Expression.Constant(true, typeof(bool)), Expression.Constant(false, typeof(bool)));
			List<Expression> list = new List<Expression>();
			switch (this.Step)
			{
			case DateTimeStep.Year:
				list.AddRange(QueryableDateTimeGroupDescription.CreateValueExpressionsForYear(propertyAccessExpression));
				break;
			case DateTimeStep.Quarter:
				list.AddRange(QueryableDateTimeGroupDescription.CreateValueExpressionsForQuarter(propertyAccessExpression));
				break;
			case DateTimeStep.Month:
				list.AddRange(QueryableDateTimeGroupDescription.CreateValueExpressionsForMonth(propertyAccessExpression));
				break;
			case DateTimeStep.Day:
				list.AddRange(QueryableDateTimeGroupDescription.CreateValueExpressionsForDay(propertyAccessExpression));
				break;
			}
			list.Add(item);
			return list;
		}

		// Token: 0x06007FE1 RID: 32737 RVA: 0x001D3D4C File Offset: 0x001D1F4C
		private static IEnumerable<Expression> CreateValueExpressionsForYear(Expression propertyAccessExpression)
		{
			Expression expression = propertyAccessExpression;
			bool flag = PivotTypeExtensions.IsNullableType(propertyAccessExpression.Type);
			if (flag)
			{
				expression = Expression.Property(propertyAccessExpression, "Value");
			}
			return new MemberExpression[]
			{
				Expression.Property(expression, "Year")
			};
		}

		// Token: 0x06007FE2 RID: 32738 RVA: 0x001D3D8C File Offset: 0x001D1F8C
		private static IEnumerable<Expression> CreateValueExpressionsForQuarter(Expression propertyAccessExpression)
		{
			Expression expression = propertyAccessExpression;
			bool flag = PivotTypeExtensions.IsNullableType(propertyAccessExpression.Type);
			if (flag)
			{
				expression = Expression.Property(propertyAccessExpression, "Value");
			}
			MemberExpression left = Expression.Property(expression, "Month");
			BinaryExpression left2 = Expression.Subtract(left, Expression.Constant(1));
			BinaryExpression left3 = Expression.Divide(left2, Expression.Constant(3));
			BinaryExpression binaryExpression = Expression.Add(left3, Expression.Constant(1));
			return new BinaryExpression[]
			{
				binaryExpression
			};
		}

		// Token: 0x06007FE3 RID: 32739 RVA: 0x001D3E0C File Offset: 0x001D200C
		private static IEnumerable<Expression> CreateValueExpressionsForMonth(Expression propertyAccessExpression)
		{
			Expression expression = propertyAccessExpression;
			bool flag = PivotTypeExtensions.IsNullableType(propertyAccessExpression.Type);
			if (flag)
			{
				expression = Expression.Property(propertyAccessExpression, "Value");
			}
			return new MemberExpression[]
			{
				Expression.Property(expression, "Month")
			};
		}

		// Token: 0x06007FE4 RID: 32740 RVA: 0x001D3E4C File Offset: 0x001D204C
		private static IEnumerable<Expression> CreateValueExpressionsForDay(Expression propertyAccessExpression)
		{
			Expression expression = propertyAccessExpression;
			bool flag = PivotTypeExtensions.IsNullableType(propertyAccessExpression.Type);
			if (flag)
			{
				expression = Expression.Property(propertyAccessExpression, "Value");
			}
			return new MemberExpression[]
			{
				Expression.Property(expression, "Day"),
				Expression.Property(expression, "Month")
			};
		}

		// Token: 0x06007FE5 RID: 32741 RVA: 0x001D3E9A File Offset: 0x001D209A
		protected override Cloneable CreateInstanceCore()
		{
			return new QueryableDateTimeGroupDescription();
		}

		// Token: 0x06007FE6 RID: 32742 RVA: 0x001D3EA4 File Offset: 0x001D20A4
		protected override void CloneCore(Cloneable source)
		{
			QueryableDateTimeGroupDescription queryableDateTimeGroupDescription = source as QueryableDateTimeGroupDescription;
			if (queryableDateTimeGroupDescription != null)
			{
				this.Step = queryableDateTimeGroupDescription.Step;
			}
			base.CloneCore(source);
		}

		// Token: 0x04002330 RID: 9008
		private static int[] daysCountInMonth = new int[]
		{
			31,
			29,
			31,
			30,
			31,
			30,
			31,
			31,
			30,
			31,
			30,
			31
		};

		// Token: 0x04002331 RID: 9009
		private DateTimeStep step;
	}
}
