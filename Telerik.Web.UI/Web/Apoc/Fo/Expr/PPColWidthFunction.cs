using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013BC RID: 5052
	internal class PPColWidthFunction : FunctionBase
	{
		// Token: 0x170042ED RID: 17133
		// (get) Token: 0x0600D16E RID: 53614 RVA: 0x002E4F86 File Offset: 0x002E3186
		public override int NumArgs
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600D16F RID: 53615 RVA: 0x002E4F8C File Offset: 0x002E318C
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			Number number = args[0].GetNumber();
			if (number == null)
			{
				throw new PropertyException("Non number operand to proportional-column-width function");
			}
			if (!pInfo.getPropertyList().GetElement().Equals("table-column"))
			{
				throw new PropertyException("proportional-column-width function may only be used on table-column FO");
			}
			return new LengthProperty(new TableColLength(number.DoubleValue()));
		}
	}
}
