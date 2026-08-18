using System;
using System.Linq.Expressions;

namespace Telerik.Web.Data
{
	// Token: 0x02001B92 RID: 7058
	public class FilterDescriptorBase : DescriptorBase, IFilterDescriptor
	{
		// Token: 0x06011175 RID: 70005 RVA: 0x003C561C File Offset: 0x003C381C
		public virtual Expression CreateFilterExpression(Expression instance)
		{
			ParameterExpression parameterExpression = instance as ParameterExpression;
			if (parameterExpression == null)
			{
				throw new ArgumentException("Parameter should be of type ParameterExpression", "instance");
			}
			return this.CreateFilterExpression(parameterExpression);
		}

		// Token: 0x06011176 RID: 70006 RVA: 0x003C564A File Offset: 0x003C384A
		protected virtual Expression CreateFilterExpression(ParameterExpression parameterExpression)
		{
			return parameterExpression;
		}
	}
}
