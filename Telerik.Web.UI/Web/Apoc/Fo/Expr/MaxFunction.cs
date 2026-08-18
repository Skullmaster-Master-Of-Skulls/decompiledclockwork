using System;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013B6 RID: 5046
	internal class MaxFunction : FunctionBase
	{
		// Token: 0x170042EA RID: 17130
		// (get) Token: 0x0600D14B RID: 53579 RVA: 0x002E47BB File Offset: 0x002E29BB
		public override int NumArgs
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x0600D14C RID: 53580 RVA: 0x002E47C0 File Offset: 0x002E29C0
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			Numeric numeric = args[0].GetNumeric();
			Numeric numeric2 = args[1].GetNumeric();
			if (numeric == null || numeric2 == null)
			{
				throw new PropertyException("Non numeric operands to max function");
			}
			return new NumericProperty(numeric.max(numeric2));
		}
	}
}
