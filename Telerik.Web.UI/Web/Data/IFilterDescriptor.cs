using System;
using System.Linq.Expressions;

namespace Telerik.Web.Data
{
	// Token: 0x02001B91 RID: 7057
	public interface IFilterDescriptor
	{
		// Token: 0x06011174 RID: 70004
		Expression CreateFilterExpression(Expression instance);
	}
}
