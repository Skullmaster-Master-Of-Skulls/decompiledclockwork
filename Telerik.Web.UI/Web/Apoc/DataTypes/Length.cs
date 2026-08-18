using System;
using Telerik.Web.Apoc.Fo.Expr;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x02001378 RID: 4984
	internal class Length
	{
		// Token: 0x0600CFFE RID: 53246 RVA: 0x002E1472 File Offset: 0x002DF672
		public int MValue()
		{
			if (!this.bIsComputed)
			{
				this.ComputeValue();
			}
			return this.millipoints;
		}

		// Token: 0x0600CFFF RID: 53247 RVA: 0x002E1488 File Offset: 0x002DF688
		public virtual void ComputeValue()
		{
		}

		// Token: 0x0600D000 RID: 53248 RVA: 0x002E148A File Offset: 0x002DF68A
		protected void SetComputedValue(int millipoints)
		{
			this.SetComputedValue(millipoints, true);
		}

		// Token: 0x0600D001 RID: 53249 RVA: 0x002E1494 File Offset: 0x002DF694
		protected void SetComputedValue(int millipoints, bool bSetComputed)
		{
			this.millipoints = millipoints;
			this.bIsComputed = bSetComputed;
		}

		// Token: 0x0600D002 RID: 53250 RVA: 0x002E14A4 File Offset: 0x002DF6A4
		public virtual bool IsAuto()
		{
			return false;
		}

		// Token: 0x0600D003 RID: 53251 RVA: 0x002E14A7 File Offset: 0x002DF6A7
		public bool IsComputed()
		{
			return this.bIsComputed;
		}

		// Token: 0x0600D004 RID: 53252 RVA: 0x002E14AF File Offset: 0x002DF6AF
		public virtual double GetTableUnits()
		{
			return 0.0;
		}

		// Token: 0x0600D005 RID: 53253 RVA: 0x002E14BA File Offset: 0x002DF6BA
		public virtual void ResolveTableUnit(double dTableUnit)
		{
		}

		// Token: 0x0600D006 RID: 53254 RVA: 0x002E14BC File Offset: 0x002DF6BC
		public virtual Numeric AsNumeric()
		{
			return null;
		}

		// Token: 0x0600D007 RID: 53255 RVA: 0x002E14BF File Offset: 0x002DF6BF
		public override string ToString()
		{
			return this.millipoints + "mpt";
		}

		// Token: 0x040037BF RID: 14271
		protected int millipoints;

		// Token: 0x040037C0 RID: 14272
		protected bool bIsComputed;
	}
}
