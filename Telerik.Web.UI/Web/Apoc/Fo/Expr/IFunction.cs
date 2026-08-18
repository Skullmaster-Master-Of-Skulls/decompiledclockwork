using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013AB RID: 5035
	internal interface IFunction
	{
		// Token: 0x170042DF RID: 17119
		// (get) Token: 0x0600D129 RID: 53545
		int NumArgs { get; }

		// Token: 0x0600D12A RID: 53546
		IPercentBase GetPercentBase();

		// Token: 0x0600D12B RID: 53547
		Property Eval(Property[] args, PropertyInfo propInfo);
	}
}
