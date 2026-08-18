using System;
using System.Linq.Expressions;
using Telerik.Web.Data.Expressions;

namespace Telerik.Web.Data
{
	// Token: 0x02001B95 RID: 7061
	public abstract class FilterDescription : FilterDescriptorBase
	{
		// Token: 0x0601117F RID: 70015
		public abstract bool SatisfiesFilter(object dataItem);

		// Token: 0x17005375 RID: 21365
		// (get) Token: 0x06011180 RID: 70016 RVA: 0x003C56F2 File Offset: 0x003C38F2
		public virtual bool IsActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06011181 RID: 70017 RVA: 0x003C56F8 File Offset: 0x003C38F8
		protected override Expression CreateFilterExpression(ParameterExpression parameterExpression)
		{
			FilterDescriptionExpressionBuilder filterDescriptionExpressionBuilder = new FilterDescriptionExpressionBuilder(parameterExpression, this);
			return filterDescriptionExpressionBuilder.CreateBodyExpression();
		}
	}
}
