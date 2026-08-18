using System;
using Telerik.Web.Apoc.Fo.Expr;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x0200138C RID: 5004
	internal class TableColLength : Length
	{
		// Token: 0x0600D096 RID: 53398 RVA: 0x002E363B File Offset: 0x002E183B
		public TableColLength(double tcolUnits)
		{
			this.tcolUnits = tcolUnits;
		}

		// Token: 0x0600D097 RID: 53399 RVA: 0x002E364A File Offset: 0x002E184A
		public override double GetTableUnits()
		{
			return this.tcolUnits;
		}

		// Token: 0x0600D098 RID: 53400 RVA: 0x002E3652 File Offset: 0x002E1852
		public override void ResolveTableUnit(double mpointsPerUnit)
		{
			base.SetComputedValue((int)(this.tcolUnits * mpointsPerUnit));
		}

		// Token: 0x0600D099 RID: 53401 RVA: 0x002E3663 File Offset: 0x002E1863
		public override string ToString()
		{
			return this.tcolUnits.ToString() + " table-column-units";
		}

		// Token: 0x0600D09A RID: 53402 RVA: 0x002E367A File Offset: 0x002E187A
		public override Numeric AsNumeric()
		{
			return new Numeric(this);
		}

		// Token: 0x040037FD RID: 14333
		private double tcolUnits;
	}
}
