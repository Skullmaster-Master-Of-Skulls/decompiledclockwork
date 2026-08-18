using System;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013B1 RID: 5041
	internal class ApocPropValFunction : FunctionBase
	{
		// Token: 0x170042E5 RID: 17125
		// (get) Token: 0x0600D13C RID: 53564 RVA: 0x002E45CC File Offset: 0x002E27CC
		public override int NumArgs
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600D13D RID: 53565 RVA: 0x002E45D0 File Offset: 0x002E27D0
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			string @string = args[0].GetString();
			if (@string == null)
			{
				throw new PropertyException("Incorrect parameter to _int-property-value function");
			}
			return pInfo.getPropertyList().GetProperty(@string);
		}
	}
}
