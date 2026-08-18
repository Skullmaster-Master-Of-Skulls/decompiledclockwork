using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Telerik.Web.Data.Expressions;

namespace Telerik.Web.Data
{
	// Token: 0x02001B97 RID: 7063
	public class FilterDescriptorCollection : Collection<IFilterDescriptor>
	{
		// Token: 0x06011195 RID: 70037 RVA: 0x003C59A4 File Offset: 0x003C3BA4
		internal LambdaExpression CreateFilterExpression(Type itemType)
		{
			ParameterExpression parameterExpression = Expression.Parameter(itemType, "item");
			FilterDescriptorCollectionExpressionBuilder filterDescriptorCollectionExpressionBuilder = new FilterDescriptorCollectionExpressionBuilder(parameterExpression, this);
			return filterDescriptorCollectionExpressionBuilder.CreateFilterExpression();
		}
	}
}
