using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Groups;
using Telerik.Web.UI.PivotGrid.DataProviders.Queryable.Groups;
using Telerik.Web.UI.PivotGrid.Queryable.Groups;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x02000D64 RID: 3428
	[DataContract]
	public sealed class QueryableDoubleGroupDescription : QueryablePropertyGroupDescriptionBase, IDoubleGroupDescription
	{
		// Token: 0x06007FE8 RID: 32744 RVA: 0x001D3F19 File Offset: 0x001D2119
		public QueryableDoubleGroupDescription()
		{
			this.Step = 100.0;
		}

		// Token: 0x170028AD RID: 10413
		// (get) Token: 0x06007FE9 RID: 32745 RVA: 0x001D3F30 File Offset: 0x001D2130
		// (set) Token: 0x06007FEA RID: 32746 RVA: 0x001D3F38 File Offset: 0x001D2138
		[DataMember]
		[DefaultValue(100.0)]
		public double Step
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

		// Token: 0x170028AE RID: 10414
		// (get) Token: 0x06007FEB RID: 32747 RVA: 0x001D3F55 File Offset: 0x001D2155
		protected internal override bool NeedsProcessing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007FEC RID: 32748 RVA: 0x001D42EC File Offset: 0x001D24EC
		protected internal override IEnumerable<object> GetAllNames(IEnumerable<object> uniqueNames, IEnumerable<object> parentGroupNames)
		{
			double start = double.NegativeInfinity;
			double end = double.PositiveInfinity;
			foreach (object obj in parentGroupNames)
			{
				if (obj is DoubleGroup)
				{
					DoubleGroup doubleGroup = (DoubleGroup)obj;
					start = Math.Max(start, doubleGroup.Start);
					end = Math.Min(end, doubleGroup.End);
				}
			}
			if (start != double.NegativeInfinity && end != double.PositiveInfinity)
			{
				double groupIndexOffset = start;
				int groupIndex = (int)(groupIndexOffset / this.step) + ((groupIndexOffset < 0.0) ? 1 : 0);
				for (;;)
				{
					double groupStart = this.step * (double)groupIndex;
					double groupEnd = this.step * (double)(groupIndex + 1);
					if (groupStart >= end)
					{
						break;
					}
					yield return new DoubleGroup(groupStart, groupEnd);
					groupIndex++;
				}
			}
			else
			{
				foreach (object uniqueGroup in uniqueNames)
				{
					if (uniqueGroup is DoubleGroup)
					{
						DoubleGroup group = (DoubleGroup)uniqueGroup;
						if (group.Start != double.NegativeInfinity && group.End != double.PositiveInfinity)
						{
							yield return group;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06007FED RID: 32749 RVA: 0x001D4318 File Offset: 0x001D2518
		internal override object ProcessGroupItem(object data)
		{
			QueryableGroup queryableGroup = data as QueryableGroup;
			if (queryableGroup == null || !queryableGroup.IsValid)
			{
				return null;
			}
			QueryableDoubleGroup queryableDoubleGroup = data as QueryableDoubleGroup;
			if (queryableDoubleGroup != null)
			{
				return new DoubleGroup(queryableDoubleGroup.Start, queryableDoubleGroup.End);
			}
			return null;
		}

		// Token: 0x06007FEE RID: 32750 RVA: 0x001D435C File Offset: 0x001D255C
		protected internal override Expression CreateGroupKeyExpression(IEnumerable<Expression> valueExpressions)
		{
			List<Expression> list = valueExpressions.ToList<Expression>();
			Type typeFromHandle = typeof(QueryableDoubleGroup);
			NewExpression newExpression = Expression.New(typeFromHandle);
			MemberAssignment memberAssignment = Expression.Bind(typeFromHandle.GetProperty("Start"), list[0]);
			MemberAssignment memberAssignment2 = Expression.Bind(typeFromHandle.GetProperty("End"), list[1]);
			MemberAssignment memberAssignment3 = Expression.Bind(typeFromHandle.GetProperty("IsValid"), list[2]);
			MemberAssignment[] bindings = new MemberAssignment[]
			{
				memberAssignment,
				memberAssignment2,
				memberAssignment3
			};
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x06007FEF RID: 32751 RVA: 0x001D43F8 File Offset: 0x001D25F8
		protected internal override IEnumerable<Expression> CreateGroupKeyValuesExpressions(ParameterExpression itemExpression)
		{
			Expression memberAccess = QueryableExpressionHelper.MakeMemberAccess(itemExpression, base.PropertyName);
			Expression propertyAccessExpression = QueryableExpressionHelper.LiftMemberAccessToNull(memberAccess);
			Expression expression = this.GenerateExpressionForGroupIndex(propertyAccessExpression);
			Expression expression2 = this.GenerateExpressionForNormalStart(expression);
			Expression expression3 = this.GenerateExpressionForNormalEnd(expression);
			Expression expression4 = QueryableExpressionHelper.LiftMemberAccessToNull(memberAccess, Expression.Constant(true, typeof(bool)), Expression.Constant(false, typeof(bool)));
			return new Expression[]
			{
				expression2,
				expression3,
				expression4
			};
		}

		// Token: 0x06007FF0 RID: 32752 RVA: 0x001D4480 File Offset: 0x001D2680
		private Expression GenerateExpressionForNormalStart(Expression expression)
		{
			return Expression.Multiply(expression, Expression.Constant(this.Step));
		}

		// Token: 0x06007FF1 RID: 32753 RVA: 0x001D44A8 File Offset: 0x001D26A8
		private Expression GenerateExpressionForNormalEnd(Expression expression)
		{
			BinaryExpression left = Expression.Add(expression, Expression.Constant(1.0));
			return Expression.Multiply(left, Expression.Constant(this.Step));
		}

		// Token: 0x06007FF2 RID: 32754 RVA: 0x001D44FC File Offset: 0x001D26FC
		private Expression GenerateExpressionForGroupIndex(Expression propertyAccessExpression)
		{
			UnaryExpression left = Expression.Convert(propertyAccessExpression, typeof(double));
			BinaryExpression arg = Expression.Divide(left, Expression.Convert(Expression.Constant(this.Step), typeof(double)));
			List<MethodInfo> list = (from mi in typeof(Math).GetMethods(BindingFlags.Static | BindingFlags.Public)
			where mi.Name == "Floor"
			select mi).ToList<MethodInfo>();
			MethodCallExpression expression = Expression.Call(list[1], arg);
			UnaryExpression expression2 = Expression.Convert(expression, typeof(int));
			return Expression.Convert(expression2, typeof(double));
		}

		// Token: 0x06007FF3 RID: 32755 RVA: 0x001D45AE File Offset: 0x001D27AE
		protected override Cloneable CreateInstanceCore()
		{
			return new QueryableDoubleGroupDescription();
		}

		// Token: 0x06007FF4 RID: 32756 RVA: 0x001D45B8 File Offset: 0x001D27B8
		protected override void CloneCore(Cloneable source)
		{
			QueryableDoubleGroupDescription queryableDoubleGroupDescription = source as QueryableDoubleGroupDescription;
			if (queryableDoubleGroupDescription != null)
			{
				this.Step = queryableDoubleGroupDescription.Step;
			}
			base.CloneCore(source);
		}

		// Token: 0x04002332 RID: 9010
		private double step;
	}
}
