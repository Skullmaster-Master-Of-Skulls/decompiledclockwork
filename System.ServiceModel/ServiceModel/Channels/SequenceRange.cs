using System;
using System.Globalization;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200095A RID: 2394
	internal struct SequenceRange
	{
		// Token: 0x06005CC7 RID: 23751 RVA: 0x00156A06 File Offset: 0x00154C06
		public SequenceRange(long number)
		{
			this = new SequenceRange(number, number);
		}

		// Token: 0x06005CC8 RID: 23752 RVA: 0x00156A10 File Offset: 0x00154C10
		public SequenceRange(long lower, long upper)
		{
			if (lower < 0L)
			{
				throw Fx.AssertAndThrow("Argument lower cannot be negative.");
			}
			if (lower > upper)
			{
				throw Fx.AssertAndThrow("Argument upper cannot be less than argument lower.");
			}
			this.lower = lower;
			this.upper = upper;
		}

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x06005CC9 RID: 23753 RVA: 0x00156A3F File Offset: 0x00154C3F
		public long Lower
		{
			get
			{
				return this.lower;
			}
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x06005CCA RID: 23754 RVA: 0x00156A47 File Offset: 0x00154C47
		public long Upper
		{
			get
			{
				return this.upper;
			}
		}

		// Token: 0x06005CCB RID: 23755 RVA: 0x00156A4F File Offset: 0x00154C4F
		public static bool operator ==(SequenceRange a, SequenceRange b)
		{
			return a.lower == b.lower && a.upper == b.upper;
		}

		// Token: 0x06005CCC RID: 23756 RVA: 0x00156A6F File Offset: 0x00154C6F
		public static bool operator !=(SequenceRange a, SequenceRange b)
		{
			return !(a == b);
		}

		// Token: 0x06005CCD RID: 23757 RVA: 0x00156A7B File Offset: 0x00154C7B
		public bool Contains(long number)
		{
			return number >= this.lower && number <= this.upper;
		}

		// Token: 0x06005CCE RID: 23758 RVA: 0x00156A94 File Offset: 0x00154C94
		public bool Contains(SequenceRange range)
		{
			return range.Lower >= this.lower && range.Upper <= this.upper;
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x00156AB9 File Offset: 0x00154CB9
		public override bool Equals(object obj)
		{
			return obj != null && obj is SequenceRange && this == (SequenceRange)obj;
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x00156ADC File Offset: 0x00154CDC
		public override int GetHashCode()
		{
			long num = this.upper ^ this.upper - this.lower;
			return (int)(num << 32 ^ num >> 32);
		}

		// Token: 0x06005CD1 RID: 23761 RVA: 0x00156B08 File Offset: 0x00154D08
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", new object[]
			{
				this.lower,
				this.upper
			});
		}

		// Token: 0x04003752 RID: 14162
		private long lower;

		// Token: 0x04003753 RID: 14163
		private long upper;
	}
}
