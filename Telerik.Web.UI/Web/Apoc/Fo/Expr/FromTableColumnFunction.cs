using System;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013B3 RID: 5043
	internal class FromTableColumnFunction : FunctionBase
	{
		// Token: 0x170042E7 RID: 17127
		// (get) Token: 0x0600D142 RID: 53570 RVA: 0x002E4644 File Offset: 0x002E2844
		public override int NumArgs
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600D143 RID: 53571 RVA: 0x002E4648 File Offset: 0x002E2848
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			if (args[0].GetString() == null)
			{
				throw new PropertyException("Incorrect parameter to from-table-column function");
			}
			throw new PropertyException("from-table-column unimplemented!");
		}
	}
}
