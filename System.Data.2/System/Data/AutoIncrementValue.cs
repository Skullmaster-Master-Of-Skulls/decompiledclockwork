using System;

namespace System.Data
{
	// Token: 0x020000A5 RID: 165
	internal abstract class AutoIncrementValue
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0005A600 File Offset: 0x00059A00
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0005A614 File Offset: 0x00059A14
		internal bool Auto
		{
			get
			{
				return this.auto;
			}
			set
			{
				this.auto = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060008B2 RID: 2226
		// (set) Token: 0x060008B3 RID: 2227
		internal abstract object Current { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060008B4 RID: 2228
		// (set) Token: 0x060008B5 RID: 2229
		internal abstract long Seed { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060008B6 RID: 2230
		// (set) Token: 0x060008B7 RID: 2231
		internal abstract long Step { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060008B8 RID: 2232
		internal abstract Type DataType { get; }

		// Token: 0x060008B9 RID: 2233
		internal abstract void SetCurrent(object value, IFormatProvider formatProvider);

		// Token: 0x060008BA RID: 2234
		internal abstract void SetCurrentAndIncrement(object value);

		// Token: 0x060008BB RID: 2235
		internal abstract void MoveAfter();

		// Token: 0x060008BC RID: 2236 RVA: 0x0005A628 File Offset: 0x00059A28
		internal AutoIncrementValue Clone()
		{
			AutoIncrementValue autoIncrementValue = (this is AutoIncrementInt64) ? new AutoIncrementInt64() : new AutoIncrementBigInteger();
			autoIncrementValue.Auto = this.Auto;
			autoIncrementValue.Seed = this.Seed;
			autoIncrementValue.Step = this.Step;
			autoIncrementValue.Current = this.Current;
			return autoIncrementValue;
		}

		// Token: 0x04000310 RID: 784
		private bool auto;
	}
}
