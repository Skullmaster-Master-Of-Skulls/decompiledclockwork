using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013C1 RID: 5057
	internal class RGBColorFunction : FunctionBase
	{
		// Token: 0x170042EE RID: 17134
		// (get) Token: 0x0600D197 RID: 53655 RVA: 0x002E5E95 File Offset: 0x002E4095
		public override int NumArgs
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x0600D198 RID: 53656 RVA: 0x002E5E98 File Offset: 0x002E4098
		public override IPercentBase GetPercentBase()
		{
			return new RGBColorFunction.RGBPercentBase();
		}

		// Token: 0x0600D199 RID: 53657 RVA: 0x002E5EA0 File Offset: 0x002E40A0
		public override Property Eval(Property[] args, PropertyInfo pInfo)
		{
			float[] array = new float[3];
			for (int i = 0; i < 3; i++)
			{
				Number number = args[i].GetNumber();
				if (number == null)
				{
					throw new PropertyException("Argument to rgb() must be a Number");
				}
				float num = number.FloatValue() / 255f;
				if ((double)num < 0.0 || (double)num > 255.0)
				{
					ApocDriver.ActiveDriver.FireApocWarning(string.Format("Normalising colour value {0} to 0", number.FloatValue()));
					num = 0f;
				}
				array[i] = num;
			}
			return new ColorTypeProperty(new ColorType(array[0], array[1], array[2]));
		}

		// Token: 0x020013C2 RID: 5058
		internal class RGBPercentBase : IPercentBase
		{
			// Token: 0x0600D19B RID: 53659 RVA: 0x002E5F42 File Offset: 0x002E4142
			public int GetDimension()
			{
				return 0;
			}

			// Token: 0x0600D19C RID: 53660 RVA: 0x002E5F45 File Offset: 0x002E4145
			public double GetBaseValue()
			{
				return 255.0;
			}

			// Token: 0x0600D19D RID: 53661 RVA: 0x002E5F50 File Offset: 0x002E4150
			public int GetBaseLength()
			{
				return 0;
			}
		}
	}
}
