using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013AF RID: 5039
	internal class CeilingFunction : FunctionBase
	{
		// Token: 0x170042E3 RID: 17123
		// (get) Token: 0x0600D136 RID: 53558 RVA: 0x002E454A File Offset: 0x002E274A
		public override int NumArgs
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600D137 RID: 53559 RVA: 0x002E4550 File Offset: 0x002E2750
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			Number number = args[0].GetNumber();
			if (number == null)
			{
				throw new PropertyException("Non number operand to ceiling function");
			}
			return new NumberProperty(Math.Ceiling(number.DoubleValue()));
		}
	}
}
