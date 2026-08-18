using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.DataProviders;
using Telerik.Web.UI.PivotGrid.Core.Fields;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Filtering;
using Telerik.Web.UI.PivotGrid.Queryable.Filtering;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x0200072A RID: 1834
	[DataContract]
	public abstract class QueryableFilterDescription : FilterDescription, IReportFilterDescription, IFilteringDescription, IDistinctValuesDescription, IConditionFactory, IFilterOperatorsProvider, IInitializeDescription
	{
		// Token: 0x06004106 RID: 16646 RVA: 0x000CC91C File Offset: 0x000CAB1C
		internal QueryableFilterDescription()
		{
		}

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x06004107 RID: 16647 RVA: 0x000CC924 File Offset: 0x000CAB24
		// (set) Token: 0x06004108 RID: 16648 RVA: 0x000CC92C File Offset: 0x000CAB2C
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

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06004109 RID: 16649 RVA: 0x000CC964 File Offset: 0x000CAB64
		// (set) Token: 0x0600410A RID: 16650 RVA: 0x000CC96C File Offset: 0x000CAB6C
		[DataMember]
		public QueryableCondition Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				if (this.condition != value)
				{
					base.ChangeSettingsProperty<QueryableCondition>(ref this.condition, value);
					base.OnPropertyChanged("Condition");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x0600410B RID: 16651 RVA: 0x000CC99A File Offset: 0x000CAB9A
		// (set) Token: 0x0600410C RID: 16652 RVA: 0x000CC9A2 File Offset: 0x000CABA2
		internal PropertyFieldInfo FieldInfo { get; set; }

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x0600410D RID: 16653 RVA: 0x000CC9AB File Offset: 0x000CABAB
		// (set) Token: 0x0600410E RID: 16654 RVA: 0x000CC9B3 File Offset: 0x000CABB3
		Condition IReportFilterDescription.Condition
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.Condition = (value as QueryableCondition);
			}
		}

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x0600410F RID: 16655 RVA: 0x000CC9C1 File Offset: 0x000CABC1
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

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x06004110 RID: 16656 RVA: 0x000CC9E1 File Offset: 0x000CABE1
		bool IFilteringDescription.PrefersDistinct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06004111 RID: 16657 RVA: 0x000CC9E4 File Offset: 0x000CABE4
		bool IInitializeDescription.Initialized
		{
			get
			{
				return this.FieldInfo != null;
			}
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x000CC9F2 File Offset: 0x000CABF2
		public override string GetUniqueName()
		{
			return this.PropertyName;
		}

		// Token: 0x06004113 RID: 16659
		internal abstract IEnumerable<Expression> CreateFilterKeyValuesExpressions(ParameterExpression itemExpression);

		// Token: 0x06004114 RID: 16660
		internal abstract Expression CreateFilterKeyExpression(IEnumerable<Expression> valueExpressions);

		// Token: 0x06004115 RID: 16661 RVA: 0x000CC9FA File Offset: 0x000CABFA
		internal override IPivotFieldInfo GetFieldInfo()
		{
			return this.FieldInfo;
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x000CCA04 File Offset: 0x000CAC04
		internal void ThrowExceptionOnDataProvider(Exception ex)
		{
			DataProviderBase dataProviderBase = this.dataProvider as DataProviderBase;
			if (dataProviderBase != null)
			{
				dataProviderBase.UpdateStatus(DataProviderStatus.Faulted, false, ex);
			}
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x000CCA2C File Offset: 0x000CAC2C
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

		// Token: 0x06004118 RID: 16664 RVA: 0x000CCA6C File Offset: 0x000CAC6C
		protected override void CloneCore(Cloneable source)
		{
			QueryableFilterDescription queryableFilterDescription = source as QueryableFilterDescription;
			if (queryableFilterDescription != null)
			{
				this.PropertyName = queryableFilterDescription.PropertyName;
				this.FieldInfo = queryableFilterDescription.FieldInfo;
				this.Condition = Cloneable.CloneOrDefault<QueryableCondition>(queryableFilterDescription.Condition);
			}
			base.CloneCore(source);
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x000CCAB4 File Offset: 0x000CACB4
		DistinctValuesProvider IDistinctValuesDescription.GetDisctinctValuesProvider()
		{
			if (this.dataProvider == null)
			{
				return null;
			}
			if (!(this.dataProvider is QueryableDataProvider))
			{
				return null;
			}
			QueryableDataProvider queryableDataProvider = this.dataProvider as QueryableDataProvider;
			return new QueryableDistinctValuesProvider(queryableDataProvider.Source, this);
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x000CCAF4 File Offset: 0x000CACF4
		Condition IConditionFactory.CreateCondition(Type conditionType)
		{
			if (conditionType == typeof(ISetCondition))
			{
				return new QueryableSetCondition();
			}
			if (conditionType == typeof(IComparisonCondition))
			{
				return new QueryableComparisonCondition();
			}
			if (conditionType == typeof(ITextCondition))
			{
				return new QueryableTextCondition();
			}
			if (conditionType == typeof(IIntervalCondition))
			{
				return new QueryableIntervalCondition();
			}
			if (conditionType == typeof(IItemsFilterCondition))
			{
				return new QueryableItemsFilterCondition();
			}
			return null;
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x000CCB7C File Offset: 0x000CAD7C
		IEnumerable<object> IFilterOperatorsProvider.GetAvailableConditions()
		{
			return new List<object>
			{
				Comparison.Equals,
				Comparison.DoesNotEqual,
				Comparison.IsGreaterThan,
				Comparison.IsGreaterThanOrEqualTo,
				Comparison.IsLessThan,
				Comparison.IsLessThanOrEqualTo,
				TextComparison.Contains,
				TextComparison.DoesNotContain,
				TextComparison.BeginsWith,
				TextComparison.DoesNotBeginWith,
				TextComparison.EndsWith,
				TextComparison.DoesNotEndWith,
				IntervalComparison.IsBetween,
				IntervalComparison.IsNotBetween
			};
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x000CCC38 File Offset: 0x000CAE38
		void IInitializeDescription.Initialize(IDataProvider provider)
		{
			if (provider == null)
			{
				return;
			}
			this.dataProvider = provider;
			this.FieldInfo = (provider.FieldInfos.GetFieldDescriptionByMember(this.PropertyName) as PropertyFieldInfo);
		}

		// Token: 0x04001141 RID: 4417
		private QueryableCondition condition;

		// Token: 0x04001142 RID: 4418
		private string propertyName;

		// Token: 0x04001143 RID: 4419
		private IDataProvider dataProvider;
	}
}
