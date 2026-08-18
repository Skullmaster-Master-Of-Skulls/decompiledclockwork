using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Telerik.Web.Data.Expressions;

namespace Telerik.Web.Data
{
	// Token: 0x02001B93 RID: 7059
	public class CompositeFilterDescriptor : FilterDescriptorBase
	{
		// Token: 0x17005373 RID: 21363
		// (get) Token: 0x06011178 RID: 70008 RVA: 0x003C5655 File Offset: 0x003C3855
		// (set) Token: 0x06011179 RID: 70009 RVA: 0x003C565D File Offset: 0x003C385D
		public FilterCompositionLogicalOperator LogicalOperator
		{
			get
			{
				return this.logicalOperator;
			}
			set
			{
				if (this.logicalOperator != value)
				{
					this.logicalOperator = value;
					base.OnPropertyChanged("LogicalOperator");
				}
			}
		}

		// Token: 0x17005374 RID: 21364
		// (get) Token: 0x0601117A RID: 70010 RVA: 0x003C567A File Offset: 0x003C387A
		// (set) Token: 0x0601117B RID: 70011 RVA: 0x003C5695 File Offset: 0x003C3895
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Used for initialization from XAML")]
		public FilterDescriptorCollection FilterDescriptors
		{
			get
			{
				if (this.filterDescriptors == null)
				{
					this.SetFilterDescriptors(new FilterDescriptorCollection());
				}
				return this.filterDescriptors;
			}
			set
			{
				if (this.filterDescriptors != value)
				{
					this.SetFilterDescriptors(value);
					base.OnPropertyChanged("FilterDescriptors");
				}
			}
		}

		// Token: 0x0601117C RID: 70012 RVA: 0x003C56B4 File Offset: 0x003C38B4
		protected override Expression CreateFilterExpression(ParameterExpression parameterExpression)
		{
			FilterDescriptorCollectionExpressionBuilder filterDescriptorCollectionExpressionBuilder = new FilterDescriptorCollectionExpressionBuilder(parameterExpression, this.FilterDescriptors, this.LogicalOperator);
			return filterDescriptorCollectionExpressionBuilder.CreateBodyExpression();
		}

		// Token: 0x0601117D RID: 70013 RVA: 0x003C56DA File Offset: 0x003C38DA
		private void SetFilterDescriptors(FilterDescriptorCollection value)
		{
			FilterDescriptorCollection filterDescriptorCollection = this.filterDescriptors;
			this.filterDescriptors = value;
		}

		// Token: 0x04004C7D RID: 19581
		private FilterDescriptorCollection filterDescriptors;

		// Token: 0x04004C7E RID: 19582
		private FilterCompositionLogicalOperator logicalOperator;
	}
}
