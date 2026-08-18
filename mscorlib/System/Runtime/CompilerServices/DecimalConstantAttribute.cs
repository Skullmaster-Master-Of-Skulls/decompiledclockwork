using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005E3 RID: 1507
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DecimalConstantAttribute : Attribute
	{
		// Token: 0x060037E1 RID: 14305 RVA: 0x000BBC05 File Offset: 0x000BAC05
		[CLSCompliant(false)]
		public DecimalConstantAttribute(byte scale, byte sign, uint hi, uint mid, uint low)
		{
			this.dec = new decimal((int)low, (int)mid, (int)hi, sign != 0, scale);
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x000BBC25 File Offset: 0x000BAC25
		public DecimalConstantAttribute(byte scale, byte sign, int hi, int mid, int low)
		{
			this.dec = new decimal(low, mid, hi, sign != 0, scale);
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x060037E3 RID: 14307 RVA: 0x000BBC45 File Offset: 0x000BAC45
		public decimal Value
		{
			get
			{
				return this.dec;
			}
		}

		// Token: 0x04001CE8 RID: 7400
		private decimal dec;
	}
}
