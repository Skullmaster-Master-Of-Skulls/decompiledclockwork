using System;
using System.Data.Common;
using System.Globalization;
using System.Numerics;

namespace System.Data
{
	// Token: 0x020000A6 RID: 166
	internal sealed class AutoIncrementInt64 : AutoIncrementValue
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0005A690 File Offset: 0x00059A90
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x0005A6A8 File Offset: 0x00059AA8
		internal override object Current
		{
			get
			{
				return this.current;
			}
			set
			{
				this.current = (long)value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0005A6C4 File Offset: 0x00059AC4
		internal override Type DataType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0005A6DC File Offset: 0x00059ADC
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x0005A6F0 File Offset: 0x00059AF0
		internal override long Seed
		{
			get
			{
				return this.seed;
			}
			set
			{
				if (this.current == this.seed || this.BoundaryCheck(value))
				{
					this.current = value;
				}
				this.seed = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0005A728 File Offset: 0x00059B28
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0005A73C File Offset: 0x00059B3C
		internal override long Step
		{
			get
			{
				return this.step;
			}
			set
			{
				if (value == 0L)
				{
					throw ExceptionBuilder.AutoIncrementSeed();
				}
				if (this.step != value)
				{
					if (this.current != this.Seed)
					{
						this.current = this.current - this.step + value;
					}
					this.step = value;
				}
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0005A788 File Offset: 0x00059B88
		internal override void MoveAfter()
		{
			this.current += this.step;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0005A7A8 File Offset: 0x00059BA8
		internal override void SetCurrent(object value, IFormatProvider formatProvider)
		{
			this.current = Convert.ToInt64(value, formatProvider);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0005A7C4 File Offset: 0x00059BC4
		internal override void SetCurrentAndIncrement(object value)
		{
			long num = (long)SqlConvert.ChangeType2(value, StorageType.Int64, typeof(long), CultureInfo.InvariantCulture);
			if (this.BoundaryCheck(num))
			{
				this.current = num + this.step;
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0005A80C File Offset: 0x00059C0C
		private bool BoundaryCheck(BigInteger value)
		{
			return (this.step < 0L && value <= this.current) || (0L < this.step && this.current <= value);
		}

		// Token: 0x04000311 RID: 785
		private long current;

		// Token: 0x04000312 RID: 786
		private long seed;

		// Token: 0x04000313 RID: 787
		private long step = 1L;
	}
}
