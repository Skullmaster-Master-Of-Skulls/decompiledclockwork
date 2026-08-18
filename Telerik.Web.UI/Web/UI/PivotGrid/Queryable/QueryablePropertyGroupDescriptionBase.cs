using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Fields;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.ReportFilter;
using Telerik.Web.UI.PivotGrid.Queryable.Groups;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x0200072F RID: 1839
	[DataContract]
	public abstract class QueryablePropertyGroupDescriptionBase : QueryableGroupDescription, IInitializeDescription, ILabelGroupFilterHost, IValueGroupFilterHost, IGroupsCountFilterHost, IGroupsPercentFilterHost, IGroupsSumFilterHost, IFilteringDescription, IDistinctValuesDescription, IConditionFactory, IFilterOperatorsProvider
	{
		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x0600415A RID: 16730 RVA: 0x000CD7B8 File Offset: 0x000CB9B8
		// (set) Token: 0x0600415B RID: 16731 RVA: 0x000CD7C0 File Offset: 0x000CB9C0
		[DataMember]
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
			set
			{
				if (this.propertyName != value)
				{
					this.propertyName = value;
					base.OnPropertyChanged("PropertyName");
					base.OnPropertyChanged("DisplayName");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x0600415C RID: 16732 RVA: 0x000CD7F8 File Offset: 0x000CB9F8
		// (set) Token: 0x0600415D RID: 16733 RVA: 0x000CD800 File Offset: 0x000CBA00
		internal PropertyFieldInfo FieldInfo { get; set; }

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x0600415E RID: 16734 RVA: 0x000CD809 File Offset: 0x000CBA09
		internal virtual bool TransformsData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x0600415F RID: 16735 RVA: 0x000CD80C File Offset: 0x000CBA0C
		protected internal override bool NeedsProcessing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x000CD80F File Offset: 0x000CBA0F
		internal override IPivotFieldInfo GetFieldInfo()
		{
			return this.FieldInfo;
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x000CD818 File Offset: 0x000CBA18
		internal override object ProcessGroupItem(object data)
		{
			QueryablePropertyGroup queryablePropertyGroup = data as QueryablePropertyGroup;
			if (queryablePropertyGroup == null || !queryablePropertyGroup.IsValid)
			{
				return null;
			}
			return queryablePropertyGroup.Value;
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x000CD840 File Offset: 0x000CBA40
		private bool IsProviderUsingLinqToObjects()
		{
			QueryableDataProvider queryableDataProvider = base.Provider as QueryableDataProvider;
			return queryableDataProvider == null || queryableDataProvider.Source == null || queryableDataProvider.Source.Provider.IsLinqToObjectsProvider();
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x000CD878 File Offset: 0x000CBA78
		protected internal override Expression CreateGroupKeyExpression(IEnumerable<Expression> valueExpressions)
		{
			if (valueExpressions == null)
			{
				throw new ArgumentNullException("valueExpressions");
			}
			if (valueExpressions.Count<Expression>() == 0)
			{
				throw new InvalidOperationException("There should be at least one value expression");
			}
			List<Expression> list = valueExpressions.ToList<Expression>();
			Type typeFromHandle = typeof(QueryablePropertyGroup);
			NewExpression newExpression = Expression.New(typeFromHandle);
			Expression expression = list[0];
			if (this.IsProviderUsingLinqToObjects())
			{
				expression = Expression.Convert(list[0], typeof(object));
			}
			MemberAssignment memberAssignment = Expression.Bind(typeFromHandle.GetProperty("Value"), expression);
			MemberAssignment memberAssignment2 = Expression.Bind(typeFromHandle.GetProperty("IsValid"), list[1]);
			MemberAssignment[] bindings = new MemberAssignment[]
			{
				memberAssignment,
				memberAssignment2
			};
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x000CD938 File Offset: 0x000CBB38
		protected internal override IEnumerable<Expression> CreateGroupKeyValuesExpressions(ParameterExpression itemExpression)
		{
			if (itemExpression == null)
			{
				throw new ArgumentNullException("itemExpression");
			}
			if (string.IsNullOrEmpty(this.PropertyName))
			{
				return new ParameterExpression[]
				{
					itemExpression
				};
			}
			Expression memberAccess = QueryableExpressionHelper.MakeMemberAccess(itemExpression, this.PropertyName);
			Expression expression = QueryableExpressionHelper.LiftMemberAccessToNull(memberAccess);
			Expression expression2 = QueryableExpressionHelper.LiftMemberAccessToNull(memberAccess, Expression.Constant(true, typeof(bool)), Expression.Constant(false, typeof(bool)));
			return new Expression[]
			{
				expression,
				expression2
			};
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x000CD9C8 File Offset: 0x000CBBC8
		protected override string GetDisplayName()
		{
			string displayName = base.GetDisplayName();
			if (displayName != null)
			{
				return displayName;
			}
			if (this.FieldInfo != null && this.FieldInfo.DisplayName != null)
			{
				return this.FieldInfo.DisplayName;
			}
			return this.PropertyName;
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x000CDA08 File Offset: 0x000CBC08
		protected override void CloneCore(Cloneable source)
		{
			QueryablePropertyGroupDescriptionBase queryablePropertyGroupDescriptionBase = source as QueryablePropertyGroupDescriptionBase;
			if (queryablePropertyGroupDescriptionBase != null)
			{
				this.PropertyName = queryablePropertyGroupDescriptionBase.PropertyName;
				this.FieldInfo = queryablePropertyGroupDescriptionBase.FieldInfo;
				base.Provider = queryablePropertyGroupDescriptionBase.Provider;
			}
			base.CloneCore(source);
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x000CDA4C File Offset: 0x000CBC4C
		internal override Expression CreateMemberAccessExpression(ParameterExpression itemExpression)
		{
			return QueryableExpressionHelper.MakeMemberAccess(itemExpression, this.PropertyName);
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000CDA67 File Offset: 0x000CBC67
		public override string GetUniqueName()
		{
			return this.PropertyName;
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06004169 RID: 16745 RVA: 0x000CDA6F File Offset: 0x000CBC6F
		bool IInitializeDescription.Initialized
		{
			get
			{
				return this.FieldInfo != null;
			}
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x000CDA7D File Offset: 0x000CBC7D
		void IInitializeDescription.Initialize(IDataProvider provider)
		{
			if (provider == null)
			{
				return;
			}
			base.Provider = provider;
			this.FieldInfo = (provider.FieldInfos.GetFieldDescriptionByMember(this.GetUniqueName()) as PropertyFieldInfo);
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x000CDAA6 File Offset: 0x000CBCA6
		ILabelGroupFilter ILabelGroupFilterHost.CreateFilter()
		{
			return new LabelGroupFilter();
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x000CDAAD File Offset: 0x000CBCAD
		IValueGroupFilter IValueGroupFilterHost.CreateFilter()
		{
			return new ValueGroupFilter();
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x000CDAB4 File Offset: 0x000CBCB4
		IGroupsCountFilter IGroupsCountFilterHost.CreateFilter()
		{
			return new GroupsCountFilter();
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x000CDABB File Offset: 0x000CBCBB
		IGroupsSumFilter IGroupsSumFilterHost.CreateFilter()
		{
			return new GroupsSumFilter();
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x000CDAC2 File Offset: 0x000CBCC2
		IGroupsPercentFilter IGroupsPercentFilterHost.CreateFilter()
		{
			return new GroupsPercentFilter();
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06004170 RID: 16752 RVA: 0x000CDAC9 File Offset: 0x000CBCC9
		Type IFilteringDescription.FilteringType
		{
			get
			{
				if (this.FieldInfo == null)
				{
					return typeof(object);
				}
				return this.FieldInfo.DataType;
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06004171 RID: 16753 RVA: 0x000CDAE9 File Offset: 0x000CBCE9
		bool IFilteringDescription.PrefersDistinct
		{
			get
			{
				return this.TransformsData;
			}
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x000CDAF4 File Offset: 0x000CBCF4
		DistinctValuesProvider IDistinctValuesDescription.GetDisctinctValuesProvider()
		{
			if (base.Provider == null)
			{
				return null;
			}
			return new LocalDistincsGroupKeysProvider(base.Provider, this);
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x000CDB19 File Offset: 0x000CBD19
		Condition IConditionFactory.CreateCondition(Type conditionType)
		{
			return DescriptionBase.CreateLocalCondition(conditionType);
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x000CDB21 File Offset: 0x000CBD21
		IEnumerable<object> IFilterOperatorsProvider.GetAvailableConditions()
		{
			return null;
		}

		// Token: 0x0400114B RID: 4427
		private string propertyName;
	}
}
