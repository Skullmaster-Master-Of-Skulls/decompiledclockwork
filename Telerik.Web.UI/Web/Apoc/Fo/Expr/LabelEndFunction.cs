using System;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Fo.Flow;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013B5 RID: 5045
	internal class LabelEndFunction : FunctionBase
	{
		// Token: 0x170042E9 RID: 17129
		// (get) Token: 0x0600D148 RID: 53576 RVA: 0x002E46BC File Offset: 0x002E28BC
		public override int NumArgs
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600D149 RID: 53577 RVA: 0x002E46C0 File Offset: 0x002E28C0
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			Length length = pInfo.getPropertyList().GetProperty("provisional-distance-between-starts").GetLength();
			Length length2 = pInfo.getPropertyList().GetNearestSpecifiedProperty("provisional-label-separation").GetLength();
			FObj fobj = pInfo.getFO();
			while (fobj != null && !(fobj is ListItem))
			{
				fobj = fobj.getParent();
			}
			if (fobj == null)
			{
				throw new PropertyException("label-end() called from outside an fo:list-item");
			}
			Length length3 = fobj.properties.GetProperty("start-indent").GetLength();
			LinearCombinationLength linearCombinationLength = new LinearCombinationLength();
			LengthBase lbase = new LengthBase(fobj, pInfo.getPropertyList(), 3);
			PercentLength length4 = new PercentLength(1.0, lbase);
			linearCombinationLength.AddTerm(1.0, length4);
			linearCombinationLength.AddTerm(-1.0, length);
			linearCombinationLength.AddTerm(-1.0, length3);
			linearCombinationLength.AddTerm(1.0, length2);
			linearCombinationLength.ComputeValue();
			return new LengthProperty(linearCombinationLength);
		}
	}
}
