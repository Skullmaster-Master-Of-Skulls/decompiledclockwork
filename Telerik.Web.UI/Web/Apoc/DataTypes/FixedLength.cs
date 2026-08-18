using System;
using Telerik.Web.Apoc.Fo.Expr;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x0200137E RID: 4990
	internal class FixedLength : Length
	{
		// Token: 0x0600D02A RID: 53290 RVA: 0x002E27E9 File Offset: 0x002E09E9
		public FixedLength(double numRelUnits, int iCurFontSize)
		{
			base.SetComputedValue((int)(numRelUnits * (double)iCurFontSize));
		}

		// Token: 0x0600D02B RID: 53291 RVA: 0x002E27FC File Offset: 0x002E09FC
		public FixedLength(double numUnits, string units)
		{
			this.Convert(numUnits, units);
		}

		// Token: 0x0600D02C RID: 53292 RVA: 0x002E280C File Offset: 0x002E0A0C
		public FixedLength(int baseUnits)
		{
			base.SetComputedValue(baseUnits);
		}

		// Token: 0x0600D02D RID: 53293 RVA: 0x002E281C File Offset: 0x002E0A1C
		protected void Convert(double dvalue, string unit)
		{
			int num = 1;
			if (unit.Equals("in"))
			{
				dvalue *= 72.0;
			}
			else if (unit.Equals("cm"))
			{
				dvalue *= 28.3464567;
			}
			else if (unit.Equals("mm"))
			{
				dvalue *= 2.83464567;
			}
			else if (!unit.Equals("pt"))
			{
				if (unit.Equals("pc"))
				{
					dvalue *= 12.0;
				}
				else if (unit.Equals("px"))
				{
					dvalue *= (double)num;
				}
				else
				{
					dvalue = 0.0;
					ApocDriver.ActiveDriver.FireApocError("Unknown length unit '" + unit + "'");
				}
			}
			base.SetComputedValue((int)(dvalue * 1000.0));
		}

		// Token: 0x0600D02E RID: 53294 RVA: 0x002E28F7 File Offset: 0x002E0AF7
		public override Numeric AsNumeric()
		{
			return new Numeric(this);
		}
	}
}
