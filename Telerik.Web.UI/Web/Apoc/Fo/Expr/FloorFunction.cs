using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013B0 RID: 5040
	internal class FloorFunction : FunctionBase
	{
		// Token: 0x170042E4 RID: 17124
		// (get) Token: 0x0600D139 RID: 53561 RVA: 0x002E458C File Offset: 0x002E278C
		public override int NumArgs
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600D13A RID: 53562 RVA: 0x002E4590 File Offset: 0x002E2790
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			Number number = args[0].GetNumber();
			if (number == null)
			{
				throw new PropertyException("Non number operand to floor function");
			}
			return new NumberProperty(Math.Floor(number.DoubleValue()));
		}
	}
}
