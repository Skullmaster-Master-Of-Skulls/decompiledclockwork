using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Web.Data.Extensions;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BB4 RID: 7092
	internal class GroupDescriptorExpressionBuilder : GroupDescriptorExpressionBuilderBase
	{
		// Token: 0x170053A3 RID: 21411
		// (get) Token: 0x0601124F RID: 70223 RVA: 0x003C7DFD File Offset: 0x003C5FFD
		public GroupDescriptorExpressionBuilder ChildBuilder
		{
			get
			{
				return this.childBuilder;
			}
		}

		// Token: 0x170053A4 RID: 21412
		// (get) Token: 0x06011250 RID: 70224 RVA: 0x003C7E05 File Offset: 0x003C6005
		public IGroupDescriptor GroupDescriptor
		{
			get
			{
				return this.groupDescriptor;
			}
		}

		// Token: 0x170053A5 RID: 21413
		// (get) Token: 0x06011251 RID: 70225 RVA: 0x003C7E0D File Offset: 0x003C600D
		public bool HasSubgroups
		{
			get
			{
				return this.childBuilder != null;
			}
		}

		// Token: 0x170053A6 RID: 21414
		// (get) Token: 0x06011252 RID: 70226 RVA: 0x003C7E1B File Offset: 0x003C601B
		protected override ListSortDirection? SortDirection
		{
			get
			{
				return this.groupDescriptor.SortDirection;
			}
		}

		// Token: 0x170053A7 RID: 21415
		// (get) Token: 0x06011253 RID: 70227 RVA: 0x003C7E28 File Offset: 0x003C6028
		private IEnumerable<AggregateFunction> AggregateFunctions
		{
			get
			{
				return this.groupDescriptor.GetAggregateFunctions();
			}
		}

		// Token: 0x170053A8 RID: 21416
		// (get) Token: 0x06011254 RID: 70228 RVA: 0x003C7E38 File Offset: 0x003C6038
		private ParameterExpression GroupingParameterExpression
		{
			get
			{
				if (this.groupingParameterExpression == null)
				{
					Type type = typeof(IGrouping<, >).MakeGenericType(new Type[]
					{
						this.GroupKeySelectorExpression.Body.Type,
						base.ItemType
					});
					this.groupingParameterExpression = Expression.Parameter(type, "group");
				}
				return this.groupingParameterExpression;
			}
		}

		// Token: 0x170053A9 RID: 21417
		// (get) Token: 0x06011255 RID: 70229 RVA: 0x003C7E98 File Offset: 0x003C6098
		protected override LambdaExpression GroupKeySelectorExpression
		{
			get
			{
				if (this.groupKeySelectorExpression == null)
				{
					this.groupKeySelectorExpression = this.CreateGroupKeySelectorExpression();
				}
				return this.groupKeySelectorExpression;
			}
		}

		// Token: 0x170053AA RID: 21418
		// (get) Token: 0x06011256 RID: 70230 RVA: 0x003C7EB4 File Offset: 0x003C60B4
		protected override LambdaExpression GroupSortKeySelectorExpression
		{
			get
			{
				if (this.groupSortKeySelectorExpression == null)
				{
					this.groupSortKeySelectorExpression = this.CreateSortKeySelectorExpression();
				}
				return this.groupSortKeySelectorExpression;
			}
		}

		// Token: 0x170053AB RID: 21419
		// (get) Token: 0x06011257 RID: 70231 RVA: 0x003C7ED0 File Offset: 0x003C60D0
		protected override LambdaExpression ResultSelectorExpression
		{
			get
			{
				if (this.resultSelectorExpression == null)
				{
					this.resultSelectorExpression = this.CreateResultSelectorExpression();
				}
				return this.resultSelectorExpression;
			}
		}

		// Token: 0x06011258 RID: 70232 RVA: 0x003C7EEC File Offset: 0x003C60EC
		public GroupDescriptorExpressionBuilder(IQueryable queryable, IGroupDescriptor groupDescriptor) : this(queryable, groupDescriptor, null)
		{
		}

		// Token: 0x06011259 RID: 70233 RVA: 0x003C7EF7 File Offset: 0x003C60F7
		public GroupDescriptorExpressionBuilder(IQueryable queryable, IGroupDescriptor groupDescriptor, GroupDescriptorExpressionBuilder childBuilder) : base(queryable)
		{
			this.groupDescriptor = groupDescriptor;
			this.childBuilder = childBuilder;
			this.InitilializeExpressionBuilderOptions();
		}

		// Token: 0x0601125A RID: 70234 RVA: 0x003C7F14 File Offset: 0x003C6114
		private void InitilializeExpressionBuilderOptions()
		{
			DescriptorBase descriptorBase = this.groupDescriptor as DescriptorBase;
			if (descriptorBase != null)
			{
				descriptorBase.ExpressionBuilderOptions.LiftMemberAccessToNull = base.Queryable.Provider.IsLinqToObjectsProvider();
			}
		}

		// Token: 0x0601125B RID: 70235 RVA: 0x003C7F4C File Offset: 0x003C614C
		private LambdaExpression CreateGroupKeySelectorExpression()
		{
			Expression body = this.GroupDescriptor.CreateGroupKeyExpression(base.ParameterExpression);
			return Expression.Lambda(body, new ParameterExpression[]
			{
				base.ParameterExpression
			});
		}

		// Token: 0x0601125C RID: 70236 RVA: 0x003C7F84 File Offset: 0x003C6184
		private LambdaExpression CreateSortKeySelectorExpression()
		{
			Expression body = this.GroupDescriptor.CreateGroupSortExpression(this.GroupingParameterExpression);
			return Expression.Lambda(body, new ParameterExpression[]
			{
				this.GroupingParameterExpression
			});
		}

		// Token: 0x0601125D RID: 70237 RVA: 0x003C7FBC File Offset: 0x003C61BC
		private LambdaExpression CreateResultSelectorExpression()
		{
			return Expression.Lambda(this.CreateSelectBodyExpression(), new ParameterExpression[]
			{
				this.GroupingParameterExpression
			});
		}

		// Token: 0x0601125E RID: 70238 RVA: 0x003C7FE8 File Offset: 0x003C61E8
		private Expression CreateSelectBodyExpression()
		{
			NewExpression newExpression = Expression.New(typeof(AggregateFunctionsGroup));
			IEnumerable<MemberBinding> bindings = this.CreateMemberBindings();
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x0601125F RID: 70239 RVA: 0x003C818C File Offset: 0x003C638C
		private IEnumerable<MemberBinding> CreateMemberBindings()
		{
			yield return this.CreateKeyMemberBinding();
			yield return this.CreateCountMemberBinding();
			yield return this.CreateHasSubgroupsMemberBinding();
			yield return this.CreateItemsMemberBinding();
			if (this.AggregateFunctions.Count() > 0)
			{
				yield return this.CreateAggregateFunctionsProjectionMemberBinding();
			}
			yield break;
		}

		// Token: 0x06011260 RID: 70240 RVA: 0x003C81AC File Offset: 0x003C63AC
		private MemberBinding CreateItemsMemberBinding()
		{
			PropertyInfo property = typeof(AggregateFunctionsGroup).GetProperty("Items");
			Expression expression = this.CreateItemsExpression();
			return Expression.Bind(property, expression);
		}

		// Token: 0x06011261 RID: 70241 RVA: 0x003C81DC File Offset: 0x003C63DC
		private Expression CreateItemsExpression()
		{
			if (this.HasSubgroups)
			{
				return this.CreateItemsExpressionFromChildBuilder();
			}
			return this.GroupingParameterExpression;
		}

		// Token: 0x06011262 RID: 70242 RVA: 0x003C81F4 File Offset: 0x003C63F4
		private Expression CreateItemsExpressionFromChildBuilder()
		{
			LambdaExpression predicate = this.CreateChildItemsFilterExpression();
			IQueryable queryable = base.Queryable.Where(predicate);
			this.childBuilder.Queryable = queryable;
			return this.childBuilder.CreateQuery().Expression;
		}

		// Token: 0x06011263 RID: 70243 RVA: 0x003C8234 File Offset: 0x003C6434
		private LambdaExpression CreateChildItemsFilterExpression()
		{
			Expression right = Expression.Property(this.GroupingParameterExpression, "Key");
			BinaryExpression body = Expression.Equal(this.GroupKeySelectorExpression.Body, right);
			return Expression.Lambda(body, new ParameterExpression[]
			{
				base.ParameterExpression
			});
		}

		// Token: 0x06011264 RID: 70244 RVA: 0x003C827C File Offset: 0x003C647C
		private MemberBinding CreateKeyMemberBinding()
		{
			PropertyInfo property = typeof(AggregateFunctionsGroup).GetProperty("Key");
			Expression expression = Expression.Property(this.GroupingParameterExpression, "Key");
			if (expression.Type.IsValueType && base.Queryable.Provider.IsLinqToObjectsProvider())
			{
				expression = Expression.Convert(expression, typeof(object));
			}
			return Expression.Bind(property, expression);
		}

		// Token: 0x06011265 RID: 70245 RVA: 0x003C82E8 File Offset: 0x003C64E8
		private MemberBinding CreateCountMemberBinding()
		{
			PropertyInfo property = typeof(AggregateFunctionsGroup).GetProperty("ItemCount");
			Expression expression = Expression.Call(typeof(Enumerable), "Count", new Type[]
			{
				base.ItemType
			}, new Expression[]
			{
				this.GroupingParameterExpression
			});
			return Expression.Bind(property, expression);
		}

		// Token: 0x06011266 RID: 70246 RVA: 0x003C8348 File Offset: 0x003C6548
		private MemberBinding CreateHasSubgroupsMemberBinding()
		{
			PropertyInfo property = typeof(AggregateFunctionsGroup).GetProperty("HasSubgroups");
			Expression expression = Expression.Constant(this.HasSubgroups);
			return Expression.Bind(property, expression);
		}

		// Token: 0x06011267 RID: 70247 RVA: 0x003C8384 File Offset: 0x003C6584
		private MemberBinding CreateAggregateFunctionsProjectionMemberBinding()
		{
			PropertyInfo property = typeof(AggregateFunctionsGroup).GetProperty("AggregateFunctionsProjection");
			Expression expression = this.CreateProjectionInitExpression();
			return Expression.Bind(property, expression);
		}

		// Token: 0x06011268 RID: 70248 RVA: 0x003C83B4 File Offset: 0x003C65B4
		private Expression CreateProjectionInitExpression()
		{
			List<Expression> propertyValuesExpressions = this.ProjectionPropertyValueExpressions().ToList<Expression>();
			NewExpression newExpression = this.CreateProjectionNewExpression(propertyValuesExpressions);
			IEnumerable<MemberBinding> bindings = this.CreateProjectionMemberBindings(newExpression.Type, propertyValuesExpressions);
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x06011269 RID: 70249 RVA: 0x003C83F8 File Offset: 0x003C65F8
		private IEnumerable<Expression> ProjectionPropertyValueExpressions()
		{
			return from f in this.AggregateFunctions
			select f.CreateAggregateExpression(this.GroupingParameterExpression);
		}

		// Token: 0x0601126A RID: 70250 RVA: 0x003C8424 File Offset: 0x003C6624
		private NewExpression CreateProjectionNewExpression(IEnumerable<Expression> propertyValuesExpressions)
		{
			IEnumerable<DynamicProperty> properties = this.AggregateFunctions.Zip(propertyValuesExpressions, (AggregateFunction f, Expression e) => new DynamicProperty(f.FunctionName, e.Type));
			Type dynamicClass = ClassFactory.Instance.GetDynamicClass(properties);
			return Expression.New(dynamicClass);
		}

		// Token: 0x0601126B RID: 70251 RVA: 0x003C8490 File Offset: 0x003C6690
		private IEnumerable<MemberBinding> CreateProjectionMemberBindings(Type projectionType, IEnumerable<Expression> propertyValuesExpressions)
		{
			return this.AggregateFunctions.Zip(propertyValuesExpressions, (AggregateFunction f, Expression e) => Expression.Bind(projectionType.GetProperty(f.FunctionName), e)).Cast<MemberBinding>();
		}

		// Token: 0x04004CBE RID: 19646
		private readonly IGroupDescriptor groupDescriptor;

		// Token: 0x04004CBF RID: 19647
		private readonly GroupDescriptorExpressionBuilder childBuilder;

		// Token: 0x04004CC0 RID: 19648
		private ParameterExpression groupingParameterExpression;

		// Token: 0x04004CC1 RID: 19649
		private LambdaExpression groupKeySelectorExpression;

		// Token: 0x04004CC2 RID: 19650
		private LambdaExpression groupSortKeySelectorExpression;

		// Token: 0x04004CC3 RID: 19651
		private LambdaExpression resultSelectorExpression;
	}
}
