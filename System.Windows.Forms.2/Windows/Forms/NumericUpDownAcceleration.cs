using System;

namespace System.Windows.Forms
{
	// Token: 0x0200030F RID: 783
	public class NumericUpDownAcceleration
	{
		// Token: 0x060031F7 RID: 12791 RVA: 0x000E121C File Offset: 0x000DF41C
		public NumericUpDownAcceleration(int seconds, decimal increment)
		{
			if (seconds < 0)
			{
				throw new ArgumentOutOfRangeException("seconds", seconds, SR.GetString("NumericUpDownLessThanZeroError"));
			}
			if (increment < 0m)
			{
				throw new ArgumentOutOfRangeException("increment", increment, SR.GetString("NumericUpDownLessThanZeroError"));
			}
			this.seconds = seconds;
			this.increment = increment;
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x000E1284 File Offset: 0x000DF484
		// (set) Token: 0x060031F9 RID: 12793 RVA: 0x000E128C File Offset: 0x000DF48C
		public int Seconds
		{
			get
			{
				return this.seconds;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("seconds", value, SR.GetString("NumericUpDownLessThanZeroError"));
				}
				this.seconds = value;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x000E12B4 File Offset: 0x000DF4B4
		// (set) Token: 0x060031FB RID: 12795 RVA: 0x000E12BC File Offset: 0x000DF4BC
		public decimal Increment
		{
			get
			{
				return this.increment;
			}
			set
			{
				if (value < 0m)
				{
					throw new ArgumentOutOfRangeException("increment", value, SR.GetString("NumericUpDownLessThanZeroError"));
				}
				this.increment = value;
			}
		}

		// Token: 0x04001E61 RID: 7777
		private int seconds;

		// Token: 0x04001E62 RID: 7778
		private decimal increment;
	}
}
