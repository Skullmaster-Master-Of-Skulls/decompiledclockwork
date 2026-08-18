using System;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013B2 RID: 5042
	internal class FromParentFunction : FunctionBase
	{
		// Token: 0x170042E6 RID: 17126
		// (get) Token: 0x0600D13F RID: 53567 RVA: 0x002E4608 File Offset: 0x002E2808
		public override int NumArgs
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600D140 RID: 53568 RVA: 0x002E460C File Offset: 0x002E280C
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			string @string = args[0].GetString();
			if (@string == null)
			{
				throw new PropertyException("Incorrect parameter to from-parent function");
			}
			return pInfo.getPropertyList().GetFromParentProperty(@string);
		}
	}
}
