using System;
using System.Collections;
using System.Text;
using Telerik.Web.Apoc.Fo.Expr;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x02001388 RID: 5000
	internal class MixedLength : Length
	{
		// Token: 0x0600D07A RID: 53370 RVA: 0x002E324D File Offset: 0x002E144D
		public MixedLength(ArrayList lengths)
		{
			this.lengths = lengths;
		}

		// Token: 0x0600D07B RID: 53371 RVA: 0x002E325C File Offset: 0x002E145C
		public override void ComputeValue()
		{
			int num = 0;
			bool bSetComputed = true;
			foreach (object obj in this.lengths)
			{
				Length length = (Length)obj;
				num += length.MValue();
				if (!length.IsComputed())
				{
					bSetComputed = false;
				}
			}
			base.SetComputedValue(num, bSetComputed);
		}

		// Token: 0x0600D07C RID: 53372 RVA: 0x002E32D0 File Offset: 0x002E14D0
		public override double GetTableUnits()
		{
			double num = 0.0;
			foreach (object obj in this.lengths)
			{
				Length length = (Length)obj;
				num += length.GetTableUnits();
			}
			return num;
		}

		// Token: 0x0600D07D RID: 53373 RVA: 0x002E3338 File Offset: 0x002E1538
		public override void ResolveTableUnit(double dTableUnit)
		{
			foreach (object obj in this.lengths)
			{
				Length length = (Length)obj;
				length.ResolveTableUnit(dTableUnit);
			}
		}

		// Token: 0x0600D07E RID: 53374 RVA: 0x002E3394 File Offset: 0x002E1594
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this.lengths)
			{
				Length length = (Length)obj;
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append('+');
				}
				stringBuilder.Append(length.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600D07F RID: 53375 RVA: 0x002E3414 File Offset: 0x002E1614
		public override Numeric AsNumeric()
		{
			Numeric numeric = null;
			foreach (object obj in this.lengths)
			{
				Length length = (Length)obj;
				if (numeric == null)
				{
					numeric = length.AsNumeric();
				}
				else
				{
					try
					{
						Numeric numeric2 = numeric.add(length.AsNumeric());
						numeric = numeric2;
					}
					catch (PropertyException arg)
					{
						Console.Error.WriteLine("Can't convert MixedLength to Numeric: " + arg);
					}
				}
			}
			return numeric;
		}

		// Token: 0x040037F7 RID: 14327
		private ArrayList lengths;
	}
}
