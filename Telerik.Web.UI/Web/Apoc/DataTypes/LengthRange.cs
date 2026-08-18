using System;
using Telerik.Web.Apoc.Fo;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x02001386 RID: 4998
	internal class LengthRange : ICompoundDatatype
	{
		// Token: 0x0600D06D RID: 53357 RVA: 0x002E308C File Offset: 0x002E128C
		public virtual void SetComponent(string sCmpnName, Property cmpnValue, bool bIsDefault)
		{
			if (sCmpnName.Equals("minimum"))
			{
				this.SetMinimum(cmpnValue, bIsDefault);
				return;
			}
			if (sCmpnName.Equals("optimum"))
			{
				this.SetOptimum(cmpnValue, bIsDefault);
				return;
			}
			if (sCmpnName.Equals("maximum"))
			{
				this.SetMaximum(cmpnValue, bIsDefault);
			}
		}

		// Token: 0x0600D06E RID: 53358 RVA: 0x002E30DA File Offset: 0x002E12DA
		public virtual Property GetComponent(string sCmpnName)
		{
			if (sCmpnName.Equals("minimum"))
			{
				return this.GetMinimum();
			}
			if (sCmpnName.Equals("optimum"))
			{
				return this.GetOptimum();
			}
			if (sCmpnName.Equals("maximum"))
			{
				return this.GetMaximum();
			}
			return null;
		}

		// Token: 0x0600D06F RID: 53359 RVA: 0x002E3119 File Offset: 0x002E1319
		protected void SetMinimum(Property minimum, bool bIsDefault)
		{
			this.minimum = minimum;
			if (!bIsDefault)
			{
				this.bfSet |= 1;
			}
		}

		// Token: 0x0600D070 RID: 53360 RVA: 0x002E3133 File Offset: 0x002E1333
		protected void SetMaximum(Property max, bool bIsDefault)
		{
			this.maximum = max;
			if (!bIsDefault)
			{
				this.bfSet |= 4;
			}
		}

		// Token: 0x0600D071 RID: 53361 RVA: 0x002E314D File Offset: 0x002E134D
		protected void SetOptimum(Property opt, bool bIsDefault)
		{
			this.optimum = opt;
			if (!bIsDefault)
			{
				this.bfSet |= 2;
			}
		}

		// Token: 0x0600D072 RID: 53362 RVA: 0x002E3167 File Offset: 0x002E1367
		private void CheckConsistency()
		{
			if (this.bChecked)
			{
				return;
			}
			this.bChecked = true;
		}

		// Token: 0x0600D073 RID: 53363 RVA: 0x002E3179 File Offset: 0x002E1379
		public Property GetMinimum()
		{
			this.CheckConsistency();
			return this.minimum;
		}

		// Token: 0x0600D074 RID: 53364 RVA: 0x002E3187 File Offset: 0x002E1387
		public Property GetMaximum()
		{
			this.CheckConsistency();
			return this.maximum;
		}

		// Token: 0x0600D075 RID: 53365 RVA: 0x002E3195 File Offset: 0x002E1395
		public Property GetOptimum()
		{
			this.CheckConsistency();
			return this.optimum;
		}

		// Token: 0x040037ED RID: 14317
		private const int MINSET = 1;

		// Token: 0x040037EE RID: 14318
		private const int OPTSET = 2;

		// Token: 0x040037EF RID: 14319
		private const int MAXSET = 4;

		// Token: 0x040037F0 RID: 14320
		private Property minimum;

		// Token: 0x040037F1 RID: 14321
		private Property optimum;

		// Token: 0x040037F2 RID: 14322
		private Property maximum;

		// Token: 0x040037F3 RID: 14323
		private int bfSet;

		// Token: 0x040037F4 RID: 14324
		private bool bChecked;
	}
}
