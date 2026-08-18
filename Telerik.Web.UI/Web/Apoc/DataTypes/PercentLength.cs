using System;
using Telerik.Web.Apoc.Fo.Expr;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x0200138A RID: 5002
	internal class PercentLength : Length
	{
		// Token: 0x0600D087 RID: 53383 RVA: 0x002E351D File Offset: 0x002E171D
		public PercentLength(double factor) : this(factor, null)
		{
		}

		// Token: 0x0600D088 RID: 53384 RVA: 0x002E3527 File Offset: 0x002E1727
		public PercentLength(double factor, IPercentBase lbase)
		{
			this.factor = factor;
			this.lbase = lbase;
		}

		// Token: 0x170042D5 RID: 17109
		// (get) Token: 0x0600D089 RID: 53385 RVA: 0x002E353D File Offset: 0x002E173D
		// (set) Token: 0x0600D08A RID: 53386 RVA: 0x002E3545 File Offset: 0x002E1745
		public IPercentBase BaseLength
		{
			get
			{
				return this.lbase;
			}
			set
			{
				this.lbase = value;
			}
		}

		// Token: 0x0600D08B RID: 53387 RVA: 0x002E354E File Offset: 0x002E174E
		public override void ComputeValue()
		{
			base.SetComputedValue((int)(this.factor * (double)this.lbase.GetBaseLength()));
		}

		// Token: 0x0600D08C RID: 53388 RVA: 0x002E356A File Offset: 0x002E176A
		public double value()
		{
			return this.factor;
		}

		// Token: 0x0600D08D RID: 53389 RVA: 0x002E3574 File Offset: 0x002E1774
		public override string ToString()
		{
			return (this.factor * 100.0).ToString() + "%";
		}

		// Token: 0x0600D08E RID: 53390 RVA: 0x002E35A3 File Offset: 0x002E17A3
		public override Numeric AsNumeric()
		{
			return new Numeric(this);
		}

		// Token: 0x040037F9 RID: 14329
		private double factor;

		// Token: 0x040037FA RID: 14330
		private IPercentBase lbase;
	}
}
