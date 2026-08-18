using System;
using System.Collections;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013BA RID: 5050
	internal class Numeric
	{
		// Token: 0x0600D158 RID: 53592 RVA: 0x002E48B4 File Offset: 0x002E2AB4
		protected Numeric(int valType, double absValue, double pcValue, double tcolValue, int dim, IPercentBase pcBase)
		{
			this.valType = valType;
			this.absValue = absValue;
			this.pcValue = pcValue;
			this.tcolValue = tcolValue;
			this.dim = dim;
			this.pcBase = pcBase;
		}

		// Token: 0x0600D159 RID: 53593 RVA: 0x002E48E9 File Offset: 0x002E2AE9
		public Numeric(decimal num) : this(1, (double)num, 0.0, 0.0, 0, null)
		{
		}

		// Token: 0x0600D15A RID: 53594 RVA: 0x002E490D File Offset: 0x002E2B0D
		public Numeric(FixedLength l) : this(1, (double)l.MValue(), 0.0, 0.0, 1, null)
		{
		}

		// Token: 0x0600D15B RID: 53595 RVA: 0x002E4931 File Offset: 0x002E2B31
		public Numeric(PercentLength pclen) : this(2, 0.0, pclen.value(), 0.0, 1, pclen.BaseLength)
		{
		}

		// Token: 0x0600D15C RID: 53596 RVA: 0x002E4959 File Offset: 0x002E2B59
		public Numeric(TableColLength tclen) : this(4, 0.0, 0.0, tclen.GetTableUnits(), 1, null)
		{
		}

		// Token: 0x0600D15D RID: 53597 RVA: 0x002E497C File Offset: 0x002E2B7C
		public Length asLength()
		{
			if (this.dim != 1)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList(3);
			if ((this.valType & 1) != 0)
			{
				arrayList.Add(new FixedLength((int)this.absValue));
			}
			if ((this.valType & 2) != 0)
			{
				arrayList.Add(new PercentLength(this.pcValue, this.pcBase));
			}
			if ((this.valType & 4) != 0)
			{
				arrayList.Add(new TableColLength(this.tcolValue));
			}
			if (arrayList.Count == 1)
			{
				return (Length)arrayList[0];
			}
			return new MixedLength(arrayList);
		}

		// Token: 0x0600D15E RID: 53598 RVA: 0x002E4A12 File Offset: 0x002E2C12
		public Number asNumber()
		{
			return new Number(this.asDouble());
		}

		// Token: 0x0600D15F RID: 53599 RVA: 0x002E4A1F File Offset: 0x002E2C1F
		public double asDouble()
		{
			if (this.dim == 0 && this.valType == 1)
			{
				return this.absValue;
			}
			throw new Exception("cannot make number if dimension != 0");
		}

		// Token: 0x0600D160 RID: 53600 RVA: 0x002E4A44 File Offset: 0x002E2C44
		private bool isMixedType()
		{
			int num = 0;
			for (int num2 = this.valType; num2 != 0; num2 >>= 1)
			{
				if ((num2 & 1) != 0)
				{
					num++;
				}
			}
			return num > 1;
		}

		// Token: 0x0600D161 RID: 53601 RVA: 0x002E4A70 File Offset: 0x002E2C70
		public Numeric subtract(Numeric op)
		{
			if (this.dim == op.dim)
			{
				IPercentBase percentBase = ((this.valType & 2) != 0) ? this.pcBase : op.pcBase;
				return new Numeric(this.valType | op.valType, this.absValue - op.absValue, this.pcValue - op.pcValue, this.tcolValue - op.tcolValue, this.dim, percentBase);
			}
			throw new PropertyException("Can't add Numerics of different dimensions");
		}

		// Token: 0x0600D162 RID: 53602 RVA: 0x002E4AF0 File Offset: 0x002E2CF0
		public Numeric add(Numeric op)
		{
			if (this.dim == op.dim)
			{
				IPercentBase percentBase = ((this.valType & 2) != 0) ? this.pcBase : op.pcBase;
				return new Numeric(this.valType | op.valType, this.absValue + op.absValue, this.pcValue + op.pcValue, this.tcolValue + op.tcolValue, this.dim, percentBase);
			}
			throw new PropertyException("Can't add Numerics of different dimensions");
		}

		// Token: 0x0600D163 RID: 53603 RVA: 0x002E4B70 File Offset: 0x002E2D70
		public Numeric multiply(Numeric op)
		{
			if (this.dim == 0)
			{
				return new Numeric(op.valType, this.absValue * op.absValue, this.absValue * op.pcValue, this.absValue * op.tcolValue, op.dim, op.pcBase);
			}
			if (op.dim == 0)
			{
				double num = op.absValue;
				return new Numeric(this.valType, num * this.absValue, num * this.pcValue, num * this.tcolValue, this.dim, this.pcBase);
			}
			if (this.valType == op.valType && !this.isMixedType())
			{
				IPercentBase percentBase = ((this.valType & 2) != 0) ? this.pcBase : op.pcBase;
				return new Numeric(this.valType, this.absValue * op.absValue, this.pcValue * op.pcValue, this.tcolValue * op.tcolValue, this.dim + op.dim, percentBase);
			}
			throw new PropertyException("Can't multiply mixed Numerics");
		}

		// Token: 0x0600D164 RID: 53604 RVA: 0x002E4C80 File Offset: 0x002E2E80
		public Numeric divide(Numeric op)
		{
			if (this.dim == 0)
			{
				return new Numeric(op.valType, this.absValue / op.absValue, this.absValue / op.pcValue, this.absValue / op.tcolValue, -op.dim, op.pcBase);
			}
			if (op.dim == 0)
			{
				double num = op.absValue;
				return new Numeric(this.valType, this.absValue / num, this.pcValue / num, this.tcolValue / num, this.dim, this.pcBase);
			}
			if (this.valType == op.valType && !this.isMixedType())
			{
				IPercentBase percentBase = ((this.valType & 2) != 0) ? this.pcBase : op.pcBase;
				return new Numeric(this.valType, (this.valType == 1) ? (this.absValue / op.absValue) : 0.0, (this.valType == 2) ? (this.pcValue / op.pcValue) : 0.0, (this.valType == 4) ? (this.tcolValue / op.tcolValue) : 0.0, this.dim - op.dim, percentBase);
			}
			throw new PropertyException("Can't divide mixed Numerics.");
		}

		// Token: 0x0600D165 RID: 53605 RVA: 0x002E4DD0 File Offset: 0x002E2FD0
		public Numeric abs()
		{
			return new Numeric(this.valType, Math.Abs(this.absValue), Math.Abs(this.pcValue), Math.Abs(this.tcolValue), this.dim, this.pcBase);
		}

		// Token: 0x0600D166 RID: 53606 RVA: 0x002E4E0C File Offset: 0x002E300C
		public Numeric max(Numeric op)
		{
			double num = 0.0;
			if (this.dim != op.dim || this.valType != op.valType || this.isMixedType())
			{
				throw new PropertyException("Arguments to max() must have same dimension and value type.");
			}
			if (this.valType == 1)
			{
				num = this.absValue - op.absValue;
			}
			else if (this.valType == 2)
			{
				num = this.pcValue - op.pcValue;
			}
			else if (this.valType == 4)
			{
				num = this.tcolValue - op.tcolValue;
			}
			if (num > 0.0)
			{
				return this;
			}
			return op;
		}

		// Token: 0x0600D167 RID: 53607 RVA: 0x002E4EAC File Offset: 0x002E30AC
		public Numeric min(Numeric op)
		{
			double num = 0.0;
			if (this.dim != op.dim || this.valType != op.valType || this.isMixedType())
			{
				throw new PropertyException("Arguments to min() must have same dimension and value type.");
			}
			if (this.valType == 1)
			{
				num = this.absValue - op.absValue;
			}
			else if (this.valType == 2)
			{
				num = this.pcValue - op.pcValue;
			}
			else if (this.valType == 4)
			{
				num = this.tcolValue - op.tcolValue;
			}
			if (num > 0.0)
			{
				return op;
			}
			return this;
		}

		// Token: 0x04003821 RID: 14369
		public const int ABS_LENGTH = 1;

		// Token: 0x04003822 RID: 14370
		public const int PC_LENGTH = 2;

		// Token: 0x04003823 RID: 14371
		public const int TCOL_LENGTH = 4;

		// Token: 0x04003824 RID: 14372
		private int valType;

		// Token: 0x04003825 RID: 14373
		private double absValue;

		// Token: 0x04003826 RID: 14374
		private double pcValue;

		// Token: 0x04003827 RID: 14375
		private IPercentBase pcBase;

		// Token: 0x04003828 RID: 14376
		private double tcolValue;

		// Token: 0x04003829 RID: 14377
		private int dim;
	}
}
