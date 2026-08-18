using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013C3 RID: 5059
	internal class RoundFunction : FunctionBase
	{
		// Token: 0x170042EF RID: 17135
		// (get) Token: 0x0600D19F RID: 53663 RVA: 0x002E5F5B File Offset: 0x002E415B
		public override int NumArgs
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600D1A0 RID: 53664 RVA: 0x002E5F60 File Offset: 0x002E4160
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			Number number = args[0].GetNumber();
			if (number == null)
			{
				throw new PropertyException("Non number operand to round function");
			}
			double num = number.DoubleValue();
			double num2 = Math.Floor(num + 0.5);
			if (num2 == 0.0 && num < 0.0)
			{
				num2 = -num2;
			}
			return new NumberProperty(num2);
		}
	}
}
