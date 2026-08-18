using System;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013AD RID: 5037
	internal class AbsFunction : FunctionBase
	{
		// Token: 0x170042E1 RID: 17121
		// (get) Token: 0x0600D130 RID: 53552 RVA: 0x002E4494 File Offset: 0x002E2694
		public override int NumArgs
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600D131 RID: 53553 RVA: 0x002E4498 File Offset: 0x002E2698
		public override Property Eval(Property[] args, PropertyInfo propInfo)
		{
			Numeric numeric = args[0].GetNumeric();
			if (numeric == null)
			{
				throw new PropertyException("Non numeric operand to abs function");
			}
			return new NumericProperty(numeric.abs());
		}
	}
}
