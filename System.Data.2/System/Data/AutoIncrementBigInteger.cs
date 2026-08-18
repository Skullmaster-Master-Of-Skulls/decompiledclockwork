using System;
using System.Data.Common;
using System.Numerics;

namespace System.Data
{
	// Token: 0x020000A7 RID: 167
	internal sealed class AutoIncrementBigInteger : AutoIncrementValue
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0005A868 File Offset: 0x00059C68
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x0005A880 File Offset: 0x00059C80
		internal override object Current
		{
			get
			{
				return this.current;
			}
			set
			{
				this.current = (BigInteger)value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0005A89C File Offset: 0x00059C9C
		internal override Type DataType
		{
			get
			{
				return typeof(BigInteger);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0005A8B4 File Offset: 0x00059CB4
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x0005A8C8 File Offset: 0x00059CC8
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

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0005A90C File Offset: 0x00059D0C
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0005A924 File Offset: 0x00059D24
		internal override long Step
		{
			get
			{
				return (long)this.step;
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

		// Token: 0x060008D1 RID: 2257 RVA: 0x0005A98C File Offset: 0x00059D8C
		internal override void MoveAfter()
		{
			this.current += this.step;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0005A9B0 File Offset: 0x00059DB0
		internal override void SetCurrent(object value, IFormatProvider formatProvider)
		{
			this.current = BigIntegerStorage.ConvertToBigInteger(value, formatProvider);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0005A9CC File Offset: 0x00059DCC
		internal override void SetCurrentAndIncrement(object value)
		{
			BigInteger bigInteger = (BigInteger)value;
			if (this.BoundaryCheck(bigInteger))
			{
				this.current = bigInteger + this.step;
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0005A9FC File Offset: 0x00059DFC
		private bool BoundaryCheck(BigInteger value)
		{
			return (this.step < 0L && value <= this.current) || (0L < this.step && this.current <= value);
		}

		// Token: 0x04000314 RID: 788
		private BigInteger current;

		// Token: 0x04000315 RID: 789
		private long seed;

		// Token: 0x04000316 RID: 790
		private BigInteger step = 1;
	}
}
