using System;
using System.Collections.Generic;
using Telerik.Web.Data;

namespace Telerik.Web.UI
{
	// Token: 0x02001970 RID: 6512
	internal class WPFDataEngineExpressionBuilder
	{
		// Token: 0x0600FC34 RID: 64564 RVA: 0x0038D0C4 File Offset: 0x0038B2C4
		public WPFDataEngineExpressionBuilder(IEnumerable<RadListViewFilterExpression> expressions)
		{
			this._expressions = expressions;
		}

		// Token: 0x0600FC35 RID: 64565 RVA: 0x0038D138 File Offset: 0x0038B338
		public IEnumerable<IFilterDescriptor> Build()
		{
			List<IFilterDescriptor> list = new List<IFilterDescriptor>();
			foreach (RadListViewFilterExpression item in this._expressions)
			{
				IFilterDescriptor item2 = this.Convert(item);
				list.Add(item2);
			}
			return list;
		}

		// Token: 0x0600FC36 RID: 64566 RVA: 0x0038D194 File Offset: 0x0038B394
		private IFilterDescriptor Convert(RadListViewFilterExpression item)
		{
			RadListViewGroupFilterExpression radListViewGroupFilterExpression = item as RadListViewGroupFilterExpression;
			if (radListViewGroupFilterExpression != null)
			{
				return this.HandleGroupExpression(radListViewGroupFilterExpression);
			}
			IRadListViewSingleValueExpression radListViewSingleValueExpression = item as IRadListViewSingleValueExpression;
			if (radListViewSingleValueExpression != null)
			{
				return new FilterDescriptor(item.FieldName, this._filterFunctionMapper[item.FilterFunction], radListViewSingleValueExpression.CurrentValue)
				{
					MemberType = radListViewSingleValueExpression.ItemType
				};
			}
			return this.HandleNonValueExpression(item);
		}

		// Token: 0x0600FC37 RID: 64567 RVA: 0x0038D1F8 File Offset: 0x0038B3F8
		protected virtual IFilterDescriptor HandleGroupExpression(RadListViewGroupFilterExpression sourceExpression)
		{
			CompositeFilterDescriptor compositeFilterDescriptor = new CompositeFilterDescriptor
			{
				LogicalOperator = this.MapGroupOperator(sourceExpression.GroupOperator)
			};
			foreach (RadListViewFilterExpression item in sourceExpression.Expressions)
			{
				IFilterDescriptor filterDescriptor = this.Convert(item);
				if (filterDescriptor != null)
				{
					compositeFilterDescriptor.FilterDescriptors.Add(filterDescriptor);
				}
			}
			return compositeFilterDescriptor;
		}

		// Token: 0x0600FC38 RID: 64568 RVA: 0x0038D278 File Offset: 0x0038B478
		protected virtual FilterCompositionLogicalOperator MapGroupOperator(RadListViewGroupFilterOperator groupOperator)
		{
			if (groupOperator != RadListViewGroupFilterOperator.Or)
			{
				return FilterCompositionLogicalOperator.And;
			}
			return FilterCompositionLogicalOperator.Or;
		}

		// Token: 0x0600FC39 RID: 64569 RVA: 0x0038D284 File Offset: 0x0038B484
		protected virtual IFilterDescriptor HandleNonValueExpression(RadListViewFilterExpression item)
		{
			if (item.FilterFunction == RadListViewFilterFunction.IsNull)
			{
				return new FilterDescriptor(item.FieldName, FilterOperator.IsEqualTo, null);
			}
			if (item.FilterFunction == RadListViewFilterFunction.IsEmpty)
			{
				return new FilterDescriptor(item.FieldName, FilterOperator.IsEqualTo, string.Empty)
				{
					MemberType = typeof(string)
				};
			}
			if (item.FilterFunction == RadListViewFilterFunction.NotIsEmpty)
			{
				return new FilterDescriptor(item.FieldName, FilterOperator.IsNotEqualTo, string.Empty)
				{
					MemberType = typeof(string)
				};
			}
			return new FilterDescriptor(item.FieldName, FilterOperator.IsNotEqualTo, null);
		}

		// Token: 0x040047BE RID: 18366
		private IEnumerable<RadListViewFilterExpression> _expressions;

		// Token: 0x040047BF RID: 18367
		private Dictionary<RadListViewFilterFunction, FilterOperator> _filterFunctionMapper = new Dictionary<RadListViewFilterFunction, FilterOperator>
		{
			{
				RadListViewFilterFunction.EqualTo,
				FilterOperator.IsEqualTo
			},
			{
				RadListViewFilterFunction.Contains,
				FilterOperator.Contains
			},
			{
				RadListViewFilterFunction.StartsWith,
				FilterOperator.StartsWith
			},
			{
				RadListViewFilterFunction.EndsWith,
				FilterOperator.EndsWith
			},
			{
				RadListViewFilterFunction.GreaterThan,
				FilterOperator.IsGreaterThan
			},
			{
				RadListViewFilterFunction.GreaterThanOrEqualTo,
				FilterOperator.IsGreaterThanOrEqualTo
			},
			{
				RadListViewFilterFunction.LessThan,
				FilterOperator.IsLessThan
			},
			{
				RadListViewFilterFunction.LessThanOrEqualTo,
				FilterOperator.IsLessThanOrEqualTo
			},
			{
				RadListViewFilterFunction.NotEqualTo,
				FilterOperator.IsNotEqualTo
			}
		};
	}
}
