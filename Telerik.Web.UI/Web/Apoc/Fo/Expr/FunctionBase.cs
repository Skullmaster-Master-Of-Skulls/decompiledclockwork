using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013AC RID: 5036
	internal abstract class FunctionBase : IFunction
	{
		// Token: 0x170042E0 RID: 17120
		// (get) Token: 0x0600D12C RID: 53548
		public abstract int NumArgs { get; }

		// Token: 0x0600D12D RID: 53549 RVA: 0x002E4489 File Offset: 0x002E2689
		public virtual IPercentBase GetPercentBase()
		{
			return null;
		}

		// Token: 0x0600D12E RID: 53550
		public abstract Property Eval(Property[] args, PropertyInfo propInfo);
	}
}
