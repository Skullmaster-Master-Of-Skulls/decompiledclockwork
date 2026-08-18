using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap.Expressions;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000711 RID: 1809
	[DataContract]
	public class OlapValueGroupFilter : GroupFilter, IValueGroupFilter, IConditionFactory
	{
		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x000CAAEB File Offset: 0x000C8CEB
		// (set) Token: 0x06004048 RID: 16456 RVA: 0x000CAAF3 File Offset: 0x000C8CF3
		[DataMember]
		public OlapCondition Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				if (this.condition != value)
				{
					this.condition = value;
					base.OnPropertyChanged("Condition");
				}
			}
		}

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x06004049 RID: 16457 RVA: 0x000CAB10 File Offset: 0x000C8D10
		// (set) Token: 0x0600404A RID: 16458 RVA: 0x000CAB18 File Offset: 0x000C8D18
		[DataMember]
		public int AggregateIndex
		{
			get
			{
				return this.aggregateIndex;
			}
			set
			{
				if (this.aggregateIndex != value)
				{
					this.aggregateIndex = value;
					base.OnPropertyChanged("AggregateIndex");
				}
			}
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x000CAB35 File Offset: 0x000C8D35
		protected override Cloneable CreateInstanceCore()
		{
			return new OlapValueGroupFilter();
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x000CAB3C File Offset: 0x000C8D3C
		protected override void CloneCore(Cloneable source)
		{
			OlapValueGroupFilter olapValueGroupFilter = source as OlapValueGroupFilter;
			if (olapValueGroupFilter != null)
			{
				this.Condition = ((olapValueGroupFilter.Condition == null) ? null : (olapValueGroupFilter.Condition.Clone() as OlapCondition));
				this.AggregateIndex = olapValueGroupFilter.AggregateIndex;
			}
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x000CAB80 File Offset: 0x000C8D80
		internal virtual OlapExpression GetExpression(OlapExpressionOptions options)
		{
			if (this.Condition == null || options.HierarchyInfo == null || options.MemberInfo == null)
			{
				return null;
			}
			IEnumerable<OlapExpression> expressions = this.Condition.GetExpressions(options);
			return expressions.First<OlapExpression>();
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x000CABBC File Offset: 0x000C8DBC
		internal override bool TrackDescriptions(IDescriptionIndexMap map)
		{
			bool flag = base.TrackDescriptions(map);
			AggregateMapResult aggregateMapResult = DescriptionIndexMapExtensions.MapAggregate(map, this.AggregateIndex);
			this.AggregateIndex = aggregateMapResult.Index;
			return flag && aggregateMapResult.Success;
		}

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x0600404F RID: 16463 RVA: 0x000CABF7 File Offset: 0x000C8DF7
		// (set) Token: 0x06004050 RID: 16464 RVA: 0x000CABFF File Offset: 0x000C8DFF
		Condition IValueGroupFilter.Condition
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.Condition = (value as OlapCondition);
			}
		}

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x06004051 RID: 16465 RVA: 0x000CAC0D File Offset: 0x000C8E0D
		// (set) Token: 0x06004052 RID: 16466 RVA: 0x000CAC15 File Offset: 0x000C8E15
		int IValueGroupFilter.AggregateIndex
		{
			get
			{
				return this.AggregateIndex;
			}
			set
			{
				this.AggregateIndex = value;
			}
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x000CAC1E File Offset: 0x000C8E1E
		Condition IConditionFactory.CreateCondition(Type conditionType)
		{
			if (conditionType == typeof(IComparisonCondition))
			{
				return new OlapComparisonCondition();
			}
			if (conditionType == typeof(IIntervalCondition))
			{
				return new OlapIntervalCondition();
			}
			return null;
		}

		// Token: 0x0400110B RID: 4363
		private OlapCondition condition;

		// Token: 0x0400110C RID: 4364
		private int aggregateIndex;
	}
}
