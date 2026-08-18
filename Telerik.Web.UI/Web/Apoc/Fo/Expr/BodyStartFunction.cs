using System;
using Telerik.Web.Apoc.Fo.Flow;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013AE RID: 5038
	internal class BodyStartFunction : FunctionBase
	{
		// Token: 0x170042E2 RID: 17122
		// (get) Token: 0x0600D133 RID: 53555 RVA: 0x002E44CF File Offset: 0x002E26CF
		public override int NumArgs
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600D134 RID: 53556 RVA: 0x002E44D4 File Offset: 0x002E26D4
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			Numeric numeric = pInfo.getPropertyList().GetProperty("provisional-distance-between-starts").GetNumeric();
			FObj fobj = pInfo.getFO();
			while (fobj != null && !(fobj is ListItem))
			{
				fobj = fobj.getParent();
			}
			if (fobj == null)
			{
				throw new PropertyException("body-start() called from outside an fo:list-item");
			}
			Numeric numeric2 = fobj.properties.GetProperty("start-indent").GetNumeric();
			return new NumericProperty(numeric.add(numeric2));
		}
	}
}
