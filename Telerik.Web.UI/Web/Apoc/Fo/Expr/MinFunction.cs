using System;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013B7 RID: 5047
	internal class MinFunction : FunctionBase
	{
		// Token: 0x170042EB RID: 17131
		// (get) Token: 0x0600D14E RID: 53582 RVA: 0x002E4804 File Offset: 0x002E2A04
		public override int NumArgs
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x0600D14F RID: 53583 RVA: 0x002E4808 File Offset: 0x002E2A08
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			Numeric numeric = args[0].GetNumeric();
			Numeric numeric2 = args[1].GetNumeric();
			if (numeric == null || numeric2 == null)
			{
				throw new PropertyException("Non numeric operands to min function");
			}
			return new NumericProperty(numeric.min(numeric2));
		}
	}
}
