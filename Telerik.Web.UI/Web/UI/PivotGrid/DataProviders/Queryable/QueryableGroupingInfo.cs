using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Queryable;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Queryable
{
	// Token: 0x02000D70 RID: 3440
	internal class QueryableGroupingInfo
	{
		// Token: 0x0600803F RID: 32831 RVA: 0x001D53F0 File Offset: 0x001D35F0
		public QueryableGroupingInfo(Type elementType, IEnumerable<QueryableGroupDescription> rowGroupDescriptions, IEnumerable<QueryableGroupDescription> columnGroupDescriptions, IEnumerable<QueryableFilterDescription> filterDescriptions, IEnumerable<QueryableAggregateDescriptionBase> aggregateDescriptions)
		{
			this.SourceElementType = elementType;
			this.SourceItemParameterExpression = Expression.Parameter(elementType);
			this.FilterDescriptions = this.CreateFilterDescriptionInfos(filterDescriptions);
			this.RowGroupDescriptions = this.CreateGroupDescriptorInfos(rowGroupDescriptions);
			this.ColumnGroupDescriptions = this.CreateGroupDescriptorInfos(columnGroupDescriptions);
			this.AggregateDescriptions = this.CreateAggregateDescriptorInfos(aggregateDescriptions);
			this.CreateDataForProjection();
			this.CreateDataForFiltering();
			this.CreateDataForGrouping();
			this.CreateDataForAggregation();
			this.CreatePropertyAccess();
		}

		// Token: 0x170028BE RID: 10430
		// (get) Token: 0x06008040 RID: 32832 RVA: 0x001D54AC File Offset: 0x001D36AC
		// (set) Token: 0x06008041 RID: 32833 RVA: 0x001D54B4 File Offset: 0x001D36B4
		public Type SourceElementType { get; private set; }

		// Token: 0x170028BF RID: 10431
		// (get) Token: 0x06008042 RID: 32834 RVA: 0x001D54BD File Offset: 0x001D36BD
		// (set) Token: 0x06008043 RID: 32835 RVA: 0x001D54C5 File Offset: 0x001D36C5
		public IList<QueryableGroupDescription> RowGroupDescriptions { get; private set; }

		// Token: 0x170028C0 RID: 10432
		// (get) Token: 0x06008044 RID: 32836 RVA: 0x001D54CE File Offset: 0x001D36CE
		// (set) Token: 0x06008045 RID: 32837 RVA: 0x001D54D6 File Offset: 0x001D36D6
		public IList<QueryableGroupDescription> ColumnGroupDescriptions { get; private set; }

		// Token: 0x170028C1 RID: 10433
		// (get) Token: 0x06008046 RID: 32838 RVA: 0x001D54DF File Offset: 0x001D36DF
		// (set) Token: 0x06008047 RID: 32839 RVA: 0x001D54E7 File Offset: 0x001D36E7
		public IList<QueryableAggregateDescriptionBase> AggregateDescriptions { get; private set; }

		// Token: 0x170028C2 RID: 10434
		// (get) Token: 0x06008048 RID: 32840 RVA: 0x001D54F0 File Offset: 0x001D36F0
		// (set) Token: 0x06008049 RID: 32841 RVA: 0x001D54F8 File Offset: 0x001D36F8
		public IList<QueryableFilterDescription> FilterDescriptions { get; private set; }

		// Token: 0x170028C3 RID: 10435
		// (get) Token: 0x0600804A RID: 32842 RVA: 0x001D5501 File Offset: 0x001D3701
		public Dictionary<QueryableGroupDescription, GroupDescriptionInformation> DescriptorInfoMappings
		{
			get
			{
				return this.descriptorInfoMappings;
			}
		}

		// Token: 0x170028C4 RID: 10436
		// (get) Token: 0x0600804B RID: 32843 RVA: 0x001D5509 File Offset: 0x001D3709
		public List<AggregateDescriptionInformation> AggregateDescriptorInfoList
		{
			get
			{
				return this.aggregateDescriptorInfoList;
			}
		}

		// Token: 0x170028C5 RID: 10437
		// (get) Token: 0x0600804C RID: 32844 RVA: 0x001D5511 File Offset: 0x001D3711
		public List<FilterDescriptionInformation> FilterDescriptorInfoList
		{
			get
			{
				return this.filterDescriptorInfoList;
			}
		}

		// Token: 0x170028C6 RID: 10438
		// (get) Token: 0x0600804D RID: 32845 RVA: 0x001D5519 File Offset: 0x001D3719
		// (set) Token: 0x0600804E RID: 32846 RVA: 0x001D5521 File Offset: 0x001D3721
		public DynamicTypeInfo GroupKeyTypeInfo { get; private set; }

		// Token: 0x170028C7 RID: 10439
		// (get) Token: 0x0600804F RID: 32847 RVA: 0x001D552A File Offset: 0x001D372A
		// (set) Token: 0x06008050 RID: 32848 RVA: 0x001D5532 File Offset: 0x001D3732
		public DynamicTypeInfo ProjectionTypeInfo { get; private set; }

		// Token: 0x170028C8 RID: 10440
		// (get) Token: 0x06008051 RID: 32849 RVA: 0x001D553B File Offset: 0x001D373B
		// (set) Token: 0x06008052 RID: 32850 RVA: 0x001D5543 File Offset: 0x001D3743
		public DynamicTypeInfo AggregateTypeInfo { get; private set; }

		// Token: 0x170028C9 RID: 10441
		// (get) Token: 0x06008053 RID: 32851 RVA: 0x001D554C File Offset: 0x001D374C
		// (set) Token: 0x06008054 RID: 32852 RVA: 0x001D5554 File Offset: 0x001D3754
		private ParameterExpression SourceItemParameterExpression { get; set; }

		// Token: 0x170028CA RID: 10442
		// (get) Token: 0x06008055 RID: 32853 RVA: 0x001D555D File Offset: 0x001D375D
		// (set) Token: 0x06008056 RID: 32854 RVA: 0x001D5565 File Offset: 0x001D3765
		private ParameterExpression ProjectedItemParameterExpression { get; set; }

		// Token: 0x170028CB RID: 10443
		// (get) Token: 0x06008057 RID: 32855 RVA: 0x001D556E File Offset: 0x001D376E
		// (set) Token: 0x06008058 RID: 32856 RVA: 0x001D5576 File Offset: 0x001D3776
		private ParameterExpression GroupInstanceTypeParameterExpression { get; set; }

		// Token: 0x170028CC RID: 10444
		// (get) Token: 0x06008059 RID: 32857 RVA: 0x001D5588 File Offset: 0x001D3788
		private IEnumerable<AggregateDescriptionInformation> AggregateDescriptorInfos
		{
			get
			{
				return from ad in this.aggregateDescriptorInfoList
				where ad != null
				select ad;
			}
		}

		// Token: 0x0600805A RID: 32858 RVA: 0x001D55B4 File Offset: 0x001D37B4
		private static Func<object, object> CreateUntypedMemberAccessFunc(LambdaExpression memberAccessLambda)
		{
			if (memberAccessLambda != null)
			{
				Type type = memberAccessLambda.Parameters[0].Type;
				Delegate @delegate = memberAccessLambda.Compile();
				MethodInfo methodInfo = FuncExtensions.ToUntypedFuncMethod.MakeGenericMethod(new Type[]
				{
					type,
					memberAccessLambda.Body.Type
				});
				return (Func<object, object>)methodInfo.Invoke(null, new object[]
				{
					@delegate
				});
			}
			return null;
		}

		// Token: 0x0600805B RID: 32859 RVA: 0x001D5620 File Offset: 0x001D3820
		private static LambdaExpression CreateResultSelectExpression(MemberExpression groupKeyAccessExp, MemberInitExpression newInitExp, ParameterExpression paramExp)
		{
			Type typeFromHandle = typeof(PivotResultItem);
			NewExpression newExpression = Expression.New(typeFromHandle);
			MemberAssignment memberAssignment = Expression.Bind(typeFromHandle.GetProperty("Key"), groupKeyAccessExp);
			MemberAssignment memberAssignment2 = Expression.Bind(typeFromHandle.GetProperty("Aggregates"), newInitExp);
			MemberInitExpression body = Expression.MemberInit(newExpression, new MemberBinding[]
			{
				memberAssignment,
				memberAssignment2
			});
			return Expression.Lambda(body, new ParameterExpression[]
			{
				paramExp
			});
		}

		// Token: 0x0600805C RID: 32860 RVA: 0x001D569C File Offset: 0x001D389C
		private List<QueryableGroupDescription> CreateGroupDescriptorInfos(IEnumerable<QueryableGroupDescription> descriptors)
		{
			List<QueryableGroupDescription> list = new List<QueryableGroupDescription>();
			foreach (QueryableGroupDescription queryableGroupDescription in descriptors)
			{
				QueryableGroupDescription queryableGroupDescription2 = queryableGroupDescription.Clone() as QueryableGroupDescription;
				if (queryableGroupDescription2 != null)
				{
					GroupDescriptionInformation groupDescriptionInformation = new GroupDescriptionInformation(queryableGroupDescription2);
					this.descriptorInfoMappings[queryableGroupDescription2] = groupDescriptionInformation;
					this.descriptorInfoList.Add(groupDescriptionInformation);
					list.Add(queryableGroupDescription2);
				}
			}
			return list;
		}

		// Token: 0x0600805D RID: 32861 RVA: 0x001D5720 File Offset: 0x001D3920
		private IList<QueryableFilterDescription> CreateFilterDescriptionInfos(IEnumerable<QueryableFilterDescription> filterDescriptions)
		{
			List<QueryableFilterDescription> list = new List<QueryableFilterDescription>();
			foreach (QueryableFilterDescription queryableFilterDescription in filterDescriptions)
			{
				FilterDescriptionInformation filterDescriptionInformation = null;
				QueryableFilterDescription queryableFilterDescription2 = queryableFilterDescription.Clone() as QueryableFilterDescription;
				if (queryableFilterDescription2 != null)
				{
					filterDescriptionInformation = new FilterDescriptionInformation(queryableFilterDescription2);
					this.filterDescriptorInfoMappings[queryableFilterDescription2] = filterDescriptionInformation;
				}
				this.filterDescriptorInfoList.Add(filterDescriptionInformation);
				list.Add(queryableFilterDescription2);
			}
			return list;
		}

		// Token: 0x0600805E RID: 32862 RVA: 0x001D57A8 File Offset: 0x001D39A8
		private List<QueryableAggregateDescriptionBase> CreateAggregateDescriptorInfos(IEnumerable<QueryableAggregateDescriptionBase> descriptors)
		{
			List<QueryableAggregateDescriptionBase> list = new List<QueryableAggregateDescriptionBase>();
			foreach (QueryableAggregateDescriptionBase queryableAggregateDescriptionBase in descriptors)
			{
				AggregateDescriptionInformation aggregateDescriptionInformation = null;
				QueryableAggregateDescriptionBase queryableAggregateDescriptionBase2 = queryableAggregateDescriptionBase.Clone() as QueryableAggregateDescriptionBase;
				QueryableAggregateDescription queryableAggregateDescription = queryableAggregateDescriptionBase2 as QueryableAggregateDescription;
				if (queryableAggregateDescription != null)
				{
					aggregateDescriptionInformation = new AggregateDescriptionInformation(queryableAggregateDescription);
					this.aggregateDescriptorInfoMappings[queryableAggregateDescription] = aggregateDescriptionInformation;
				}
				this.aggregateDescriptorInfoList.Add(aggregateDescriptionInformation);
				list.Add(queryableAggregateDescriptionBase2);
			}
			return list;
		}

		// Token: 0x0600805F RID: 32863 RVA: 0x001D5838 File Offset: 0x001D3A38
		private IQueryable CreateFilterQuery(IQueryable currentQuery)
		{
			LambdaExpression lambdaExpression = this.CreateFilterLambdaExpression();
			if (lambdaExpression != null)
			{
				currentQuery = currentQuery.Where(lambdaExpression);
			}
			return currentQuery;
		}

		// Token: 0x06008060 RID: 32864 RVA: 0x001D585C File Offset: 0x001D3A5C
		private IQueryable CreateGroupByQuery(IQueryable currentQuery)
		{
			LambdaExpression keySelector = this.CreateGroupingLambdaExpression();
			return currentQuery.GroupBy(keySelector);
		}

		// Token: 0x06008061 RID: 32865 RVA: 0x001D587C File Offset: 0x001D3A7C
		private IQueryable CreateSelectQuery(IQueryable currentQuery)
		{
			LambdaExpression selector = this.CreateSelectLambdaExpression();
			return currentQuery.Select(selector);
		}

		// Token: 0x06008062 RID: 32866 RVA: 0x001D5898 File Offset: 0x001D3A98
		private IQueryable CreateInitialSelectQuery(IQueryable currentQuery)
		{
			LambdaExpression selector = this.CreateProjectionLambdaExpression();
			return currentQuery.Select(selector);
		}

		// Token: 0x06008063 RID: 32867 RVA: 0x001D58B4 File Offset: 0x001D3AB4
		private LambdaExpression CreateSelectLambdaExpression()
		{
			MemberExpression groupKeyAccessExp = Expression.Property(this.GroupInstanceTypeParameterExpression, this.GroupInstanceTypeParameterExpression.Type.GetProperty("Key"));
			MemberInitExpression newInitExp = this.CreateAggregateProjectionExpression();
			return QueryableGroupingInfo.CreateResultSelectExpression(groupKeyAccessExp, newInitExp, this.GroupInstanceTypeParameterExpression);
		}

		// Token: 0x06008064 RID: 32868 RVA: 0x001D5904 File Offset: 0x001D3B04
		private LambdaExpression CreateProjectionLambdaExpression()
		{
			Type sourceElementType = this.SourceElementType;
			IEnumerable<Expression> projectionExpressions = this.GetProjectionExpressions();
			Type type = this.ProjectionTypeInfo.Type;
			IEnumerable<PropertyInfo> propertyInfos = this.ProjectionTypeInfo.PropertyInfos;
			List<MemberAssignment> bindings = propertyInfos.Zip(projectionExpressions, (PropertyInfo pi, Expression gp) => Expression.Bind(pi, gp)).ToList<MemberAssignment>();
			NewExpression newExpression = Expression.New(type);
			MemberInitExpression body = Expression.MemberInit(newExpression, bindings);
			return Expression.Lambda(body, new ParameterExpression[]
			{
				this.SourceItemParameterExpression
			});
		}

		// Token: 0x06008065 RID: 32869 RVA: 0x001D59C0 File Offset: 0x001D3BC0
		private MemberInitExpression CreateAggregateProjectionExpression()
		{
			Type aggregateType = this.AggregateTypeInfo.Type;
			List<MemberAssignment> bindings = this.AggregateDescriptorInfos.Zip(from adi in this.AggregateDescriptorInfos
			select adi.CachedAggregateExpression, (AggregateDescriptionInformation m, Expression e) => Expression.Bind(aggregateType.GetProperty(m.Descriptor.FunctionName), e)).ToList<MemberAssignment>();
			NewExpression newExpression = Expression.New(aggregateType);
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x06008066 RID: 32870 RVA: 0x001D5A46 File Offset: 0x001D3C46
		private IEnumerable<Expression> GetGroupExpressions()
		{
			return from gdi in this.descriptorInfoList
			select gdi.CachedGroupingExpression;
		}

		// Token: 0x06008067 RID: 32871 RVA: 0x001D5A88 File Offset: 0x001D3C88
		private IEnumerable<Expression> GetProjectionExpressions()
		{
			return Enumerable.Empty<Expression>().Concat(this.FilterDescriptorInfoList.SelectMany((FilterDescriptionInformation fdi) => fdi.CachedProjectionFilterExpressions)).Concat(this.descriptorInfoList.SelectMany((GroupDescriptionInformation gdi) => gdi.CachedProjectionPropertyExpressions)).Concat(from adi in this.AggregateDescriptorInfos
			select adi.CachedAggregatedValueExpression);
		}

		// Token: 0x06008068 RID: 32872 RVA: 0x001D5B2C File Offset: 0x001D3D2C
		private LambdaExpression CreateFilterLambdaExpression()
		{
			IEnumerable<Expression> enumerable = from fdi in this.filterDescriptorInfoList
			select fdi.CachedFilterExpression;
			Expression expression = null;
			foreach (Expression expression2 in enumerable)
			{
				if (expression == null)
				{
					expression = expression2;
				}
				else if (expression2 != null)
				{
					expression = Expression.And(expression, expression2);
				}
			}
			if (expression == null)
			{
				return null;
			}
			return Expression.Lambda(expression, new ParameterExpression[]
			{
				this.ProjectedItemParameterExpression
			});
		}

		// Token: 0x06008069 RID: 32873 RVA: 0x001D5BDC File Offset: 0x001D3DDC
		private LambdaExpression CreateGroupingLambdaExpression()
		{
			IEnumerable<Expression> groupExpressions = this.GetGroupExpressions();
			Type type = this.GroupKeyTypeInfo.Type;
			IEnumerable<PropertyInfo> propertyInfos = this.GroupKeyTypeInfo.PropertyInfos;
			List<MemberAssignment> bindings = propertyInfos.Zip(groupExpressions, (PropertyInfo pi, Expression gp) => Expression.Bind(pi, gp)).ToList<MemberAssignment>();
			NewExpression newExpression = Expression.New(type);
			MemberInitExpression body = Expression.MemberInit(newExpression, bindings);
			return Expression.Lambda(body, new ParameterExpression[]
			{
				this.ProjectedItemParameterExpression
			});
		}

		// Token: 0x0600806A RID: 32874 RVA: 0x001D5C80 File Offset: 0x001D3E80
		private IEnumerable<Expression> GetProjectedGroupPropertyExpressions(GroupDescriptionInformation info)
		{
			List<Expression> list = new List<Expression>();
			using (List<string>.Enumerator enumerator = info.ProjectionPropertyNames.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item = enumerator.Current;
					PropertyInfo property = (from pi in this.ProjectionTypeInfo.PropertyInfos
					where pi.Name == item
					select pi).FirstOrDefault<PropertyInfo>();
					MemberExpression item2 = Expression.Property(this.ProjectedItemParameterExpression, property);
					list.Add(item2);
				}
			}
			return list;
		}

		// Token: 0x0600806B RID: 32875 RVA: 0x001D5D3C File Offset: 0x001D3F3C
		private IEnumerable<Expression> GetProjectedFilterPropertyExpressions(FilterDescriptionInformation filterInfo)
		{
			List<Expression> list = new List<Expression>();
			using (List<string>.Enumerator enumerator = filterInfo.FilterPropertyNames.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string item = enumerator.Current;
					PropertyInfo property = (from pi in this.ProjectionTypeInfo.PropertyInfos
					where pi.Name == item
					select pi).FirstOrDefault<PropertyInfo>();
					MemberExpression item2 = Expression.Property(this.ProjectedItemParameterExpression, property);
					list.Add(item2);
				}
			}
			return list;
		}

		// Token: 0x0600806C RID: 32876 RVA: 0x001D5DDC File Offset: 0x001D3FDC
		private IEnumerable<PivotDynamicProperty> GetGroupKeyTypeProperties()
		{
			int num = 0;
			List<PivotDynamicProperty> list = new List<PivotDynamicProperty>();
			foreach (GroupDescriptionInformation groupDescriptionInformation in this.descriptorInfoList)
			{
				Expression cachedGroupingExpression = groupDescriptionInformation.CachedGroupingExpression;
				Type typeFromHandle = typeof(object);
				PivotDynamicProperty pivotDynamicProperty = new PivotDynamicProperty("G" + num.ToString(CultureInfo.InvariantCulture), typeFromHandle);
				groupDescriptionInformation.GroupingTypePropertyName = pivotDynamicProperty.Name;
				list.Add(pivotDynamicProperty);
				num++;
			}
			return list;
		}

		// Token: 0x0600806D RID: 32877 RVA: 0x001D5E7C File Offset: 0x001D407C
		private void InitializeGroupInstanceTypeParameterExpression()
		{
			Type type = typeof(IGrouping<, >).MakeGenericType(new Type[]
			{
				this.GroupKeyTypeInfo.Type,
				this.ProjectionTypeInfo.Type
			});
			ParameterExpression groupInstanceTypeParameterExpression = Expression.Parameter(type, "group");
			this.GroupInstanceTypeParameterExpression = groupInstanceTypeParameterExpression;
		}

		// Token: 0x0600806E RID: 32878 RVA: 0x001D5ED0 File Offset: 0x001D40D0
		private void CreateDataForAggregation()
		{
			this.CreateAggregateTypePropertyExpressions();
			this.CreateAggregateType();
		}

		// Token: 0x0600806F RID: 32879 RVA: 0x001D5EDE File Offset: 0x001D40DE
		private void CreateDataForGrouping()
		{
			this.CreateGroupKeyPropertyExpressions();
			this.CreateGroupKeyType();
			this.InitializeGroupInstanceTypeParameterExpression();
		}

		// Token: 0x06008070 RID: 32880 RVA: 0x001D5EF2 File Offset: 0x001D40F2
		private void CreateDataForProjection()
		{
			this.CreateProjectedPropertyExressions();
			this.CreateProjectionType();
			this.InitializeProjectedItemParameterExpression();
		}

		// Token: 0x06008071 RID: 32881 RVA: 0x001D5F06 File Offset: 0x001D4106
		private void CreatePropertyAccess()
		{
			this.CreateGroupingTypePropertyAccess();
			this.CreateAggregateTypePropertyAccess();
		}

		// Token: 0x06008072 RID: 32882 RVA: 0x001D5F14 File Offset: 0x001D4114
		private void CreateDataForFiltering()
		{
			this.CreateFilterKeyPropertyExpressions();
		}

		// Token: 0x06008073 RID: 32883 RVA: 0x001D5F38 File Offset: 0x001D4138
		private void CreateGroupingTypePropertyAccess()
		{
			DynamicTypeInfo groupKeyTypeInfo = this.GroupKeyTypeInfo;
			foreach (GroupDescriptionInformation groupDescriptionInformation in this.descriptorInfoMappings.Values)
			{
				string groupingPropertyName = groupDescriptionInformation.GroupingTypePropertyName;
				Expression cachedGroupingExpression = groupDescriptionInformation.CachedGroupingExpression;
				PropertyInfo propertyInfo = (from pi in groupKeyTypeInfo.PropertyInfos
				where pi.Name == groupingPropertyName
				select pi).First<PropertyInfo>();
				ParameterExpression parameterExpression = Expression.Parameter(groupKeyTypeInfo.Type);
				MemberExpression body = Expression.Property(parameterExpression, propertyInfo);
				LambdaExpression memberAccessLambda = Expression.Lambda(body, new ParameterExpression[]
				{
					parameterExpression
				});
				Func<object, object> groupingTypePropertyAccess = QueryableGroupingInfo.CreateUntypedMemberAccessFunc(memberAccessLambda);
				groupDescriptionInformation.GroupingTypePropertyAccess = groupingTypePropertyAccess;
				groupDescriptionInformation.GroupingTypePropertyInfo = propertyInfo;
			}
		}

		// Token: 0x06008074 RID: 32884 RVA: 0x001D6034 File Offset: 0x001D4234
		private void CreateAggregateTypePropertyAccess()
		{
			DynamicTypeInfo aggregateTypeInfo = this.AggregateTypeInfo;
			foreach (AggregateDescriptionInformation aggregateDescriptionInformation in this.AggregateDescriptorInfos)
			{
				string groupingPropertyName = aggregateDescriptionInformation.AggregateTypePropertyName;
				Expression cachedAggregateExpression = aggregateDescriptionInformation.CachedAggregateExpression;
				QueryableAggregateDescription descriptor = aggregateDescriptionInformation.Descriptor;
				PropertyInfo property = (from pi in aggregateTypeInfo.PropertyInfos
				where pi.Name == groupingPropertyName
				select pi).First<PropertyInfo>();
				ParameterExpression parameterExpression = Expression.Parameter(aggregateTypeInfo.Type);
				MemberExpression body = Expression.Property(parameterExpression, property);
				LambdaExpression memberAccessLambda = Expression.Lambda(body, new ParameterExpression[]
				{
					parameterExpression
				});
				Func<object, object> aggregateTypePropertyAccess = QueryableGroupingInfo.CreateUntypedMemberAccessFunc(memberAccessLambda);
				aggregateDescriptionInformation.AggregateTypePropertyAccess = aggregateTypePropertyAccess;
			}
		}

		// Token: 0x06008075 RID: 32885 RVA: 0x001D610C File Offset: 0x001D430C
		public IQueryable CreateQuery(IQueryable source)
		{
			IQueryable currentQuery = this.CreateInitialSelectQuery(source);
			currentQuery = this.CreateFilterQuery(currentQuery);
			currentQuery = this.CreateGroupByQuery(currentQuery);
			return this.CreateSelectQuery(currentQuery);
		}

		// Token: 0x06008076 RID: 32886 RVA: 0x001D613C File Offset: 0x001D433C
		public IList<PivotResultItem> ProcessQuery(IQueryable resultQuery)
		{
			List<PivotResultItem> list = new List<PivotResultItem>();
			foreach (object obj in resultQuery)
			{
				PivotResultItem item = obj as PivotResultItem;
				this.ProcessRawData(item);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06008077 RID: 32887 RVA: 0x001D61A4 File Offset: 0x001D43A4
		private void CreateProjectedPropertyExressions()
		{
			foreach (FilterDescriptionInformation filterDescriptionInformation in this.filterDescriptorInfoList)
			{
				IEnumerable<Expression> collection = filterDescriptionInformation.Description.CreateFilterKeyValuesExpressions(this.SourceItemParameterExpression);
				filterDescriptionInformation.CachedProjectionFilterExpressions.AddRange(collection);
			}
			foreach (GroupDescriptionInformation groupDescriptionInformation in this.descriptorInfoList)
			{
				IEnumerable<Expression> collection2 = groupDescriptionInformation.Description.CreateGroupKeyValuesExpressions(this.SourceItemParameterExpression);
				groupDescriptionInformation.CachedProjectionPropertyExpressions.AddRange(collection2);
			}
			foreach (AggregateDescriptionInformation aggregateDescriptionInformation in this.AggregateDescriptorInfos)
			{
				Expression cachedAggregatedValueExpression = aggregateDescriptionInformation.Descriptor.CreateAggregateValueExpression(this.SourceItemParameterExpression);
				aggregateDescriptionInformation.CachedAggregatedValueExpression = cachedAggregatedValueExpression;
			}
		}

		// Token: 0x06008078 RID: 32888 RVA: 0x001D62C4 File Offset: 0x001D44C4
		private void InitializeProjectedItemParameterExpression()
		{
			this.ProjectedItemParameterExpression = Expression.Parameter(this.ProjectionTypeInfo.Type);
		}

		// Token: 0x06008079 RID: 32889 RVA: 0x001D62DC File Offset: 0x001D44DC
		private void ProcessRawData(PivotResultItem item)
		{
			object key = item.Key;
			foreach (GroupDescriptionInformation groupDescriptionInformation in this.descriptorInfoMappings.Values)
			{
				if (groupDescriptionInformation.Description.NeedsProcessing)
				{
					object data = groupDescriptionInformation.GroupingTypePropertyAccess(key);
					object value = groupDescriptionInformation.Description.ProcessGroupItem(data);
					groupDescriptionInformation.GroupingTypePropertyInfo.SetValue(key, value, null);
				}
			}
		}

		// Token: 0x0600807A RID: 32890 RVA: 0x001D636C File Offset: 0x001D456C
		private void CreateAggregateType()
		{
			List<PivotDynamicProperty> list = new List<PivotDynamicProperty>();
			foreach (AggregateDescriptionInformation aggregateDescriptionInformation in this.AggregateDescriptorInfos)
			{
				string functionName = aggregateDescriptionInformation.Descriptor.FunctionName;
				Type type = aggregateDescriptionInformation.CachedAggregateExpression.Type;
				aggregateDescriptionInformation.AggregateTypePropertyName = functionName;
				PivotDynamicProperty item = new PivotDynamicProperty(functionName, type);
				list.Add(item);
			}
			this.AggregateTypeInfo = DynamicTypeInfo.CreateTypeWithProperties(list);
		}

		// Token: 0x0600807B RID: 32891 RVA: 0x001D63FC File Offset: 0x001D45FC
		private void CreateAggregateTypePropertyExpressions()
		{
			foreach (AggregateDescriptionInformation aggregateDescriptionInformation in this.AggregateDescriptorInfos)
			{
				aggregateDescriptionInformation.CachedAggregateExpression = aggregateDescriptionInformation.Descriptor.CreateAggregateExpression(this.GroupInstanceTypeParameterExpression, aggregateDescriptionInformation.AggregateValuePropertyName);
			}
		}

		// Token: 0x0600807C RID: 32892 RVA: 0x001D6460 File Offset: 0x001D4660
		private void CreateGroupKeyType()
		{
			IEnumerable<PivotDynamicProperty> groupKeyTypeProperties = this.GetGroupKeyTypeProperties();
			this.GroupKeyTypeInfo = DynamicTypeInfo.CreateTypeWithProperties(groupKeyTypeProperties);
		}

		// Token: 0x0600807D RID: 32893 RVA: 0x001D6480 File Offset: 0x001D4680
		private void CreateGroupKeyPropertyExpressions()
		{
			foreach (GroupDescriptionInformation groupDescriptionInformation in this.descriptorInfoList)
			{
				IEnumerable<Expression> projectedGroupPropertyExpressions = this.GetProjectedGroupPropertyExpressions(groupDescriptionInformation);
				Expression cachedGroupingExpression = groupDescriptionInformation.Description.CreateGroupKeyExpression(projectedGroupPropertyExpressions);
				groupDescriptionInformation.CachedGroupingExpression = cachedGroupingExpression;
			}
		}

		// Token: 0x0600807E RID: 32894 RVA: 0x001D64E8 File Offset: 0x001D46E8
		private void CreateFilterKeyPropertyExpressions()
		{
			foreach (FilterDescriptionInformation filterDescriptionInformation in this.filterDescriptorInfoList)
			{
				IEnumerable<Expression> projectedFilterPropertyExpressions = this.GetProjectedFilterPropertyExpressions(filterDescriptionInformation);
				Expression cachedFilterExpression = filterDescriptionInformation.Description.CreateFilterKeyExpression(projectedFilterPropertyExpressions);
				filterDescriptionInformation.CachedFilterExpression = cachedFilterExpression;
			}
		}

		// Token: 0x0600807F RID: 32895 RVA: 0x001D6550 File Offset: 0x001D4750
		private void CreateProjectionType()
		{
			IEnumerable<PivotDynamicProperty> projectionTypeProperties = this.GetProjectionTypeProperties();
			this.ProjectionTypeInfo = DynamicTypeInfo.CreateTypeWithProperties(projectionTypeProperties);
		}

		// Token: 0x06008080 RID: 32896 RVA: 0x001D6570 File Offset: 0x001D4770
		private IEnumerable<PivotDynamicProperty> GetProjectionTypeProperties()
		{
			int num = 0;
			List<PivotDynamicProperty> list = new List<PivotDynamicProperty>();
			foreach (FilterDescriptionInformation filterDescriptionInformation in this.filterDescriptorInfoList)
			{
				foreach (Expression expression in filterDescriptionInformation.CachedProjectionFilterExpressions)
				{
					PivotDynamicProperty pivotDynamicProperty = new PivotDynamicProperty("S" + num.ToString(CultureInfo.InvariantCulture), expression.Type);
					filterDescriptionInformation.FilterPropertyNames.Add(pivotDynamicProperty.Name);
					list.Add(pivotDynamicProperty);
					num++;
				}
			}
			foreach (GroupDescriptionInformation groupDescriptionInformation in this.descriptorInfoList)
			{
				foreach (Expression expression2 in groupDescriptionInformation.CachedProjectionPropertyExpressions)
				{
					PivotDynamicProperty pivotDynamicProperty2 = new PivotDynamicProperty("S" + num.ToString(CultureInfo.InvariantCulture), expression2.Type);
					groupDescriptionInformation.ProjectionPropertyNames.Add(pivotDynamicProperty2.Name);
					list.Add(pivotDynamicProperty2);
					num++;
				}
			}
			foreach (AggregateDescriptionInformation aggregateDescriptionInformation in this.AggregateDescriptorInfos)
			{
				Expression cachedAggregatedValueExpression = aggregateDescriptionInformation.CachedAggregatedValueExpression;
				PivotDynamicProperty pivotDynamicProperty3 = new PivotDynamicProperty("S" + num.ToString(CultureInfo.InvariantCulture), cachedAggregatedValueExpression.Type);
				list.Add(pivotDynamicProperty3);
				aggregateDescriptionInformation.AggregateValuePropertyName = pivotDynamicProperty3.Name;
				num++;
			}
			return list;
		}

		// Token: 0x04002343 RID: 9027
		private List<GroupDescriptionInformation> descriptorInfoList = new List<GroupDescriptionInformation>();

		// Token: 0x04002344 RID: 9028
		private Dictionary<QueryableGroupDescription, GroupDescriptionInformation> descriptorInfoMappings = new Dictionary<QueryableGroupDescription, GroupDescriptionInformation>();

		// Token: 0x04002345 RID: 9029
		private List<AggregateDescriptionInformation> aggregateDescriptorInfoList = new List<AggregateDescriptionInformation>();

		// Token: 0x04002346 RID: 9030
		private Dictionary<QueryableAggregateDescription, AggregateDescriptionInformation> aggregateDescriptorInfoMappings = new Dictionary<QueryableAggregateDescription, AggregateDescriptionInformation>();

		// Token: 0x04002347 RID: 9031
		private List<FilterDescriptionInformation> filterDescriptorInfoList = new List<FilterDescriptionInformation>();

		// Token: 0x04002348 RID: 9032
		private Dictionary<QueryableFilterDescription, FilterDescriptionInformation> filterDescriptorInfoMappings = new Dictionary<QueryableFilterDescription, FilterDescriptionInformation>();
	}
}
