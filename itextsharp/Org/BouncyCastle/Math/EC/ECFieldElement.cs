using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000183 RID: 387
	public abstract class ECFieldElement
	{
		// Token: 0x06000EFF RID: 3839
		public abstract BigInteger ToBigInteger();

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000F00 RID: 3840
		public abstract string FieldName { get; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000F01 RID: 3841
		public abstract int FieldSize { get; }

		// Token: 0x06000F02 RID: 3842
		public abstract ECFieldElement Add(ECFieldElement b);

		// Token: 0x06000F03 RID: 3843
		public abstract ECFieldElement Subtract(ECFieldElement b);

		// Token: 0x06000F04 RID: 3844
		public abstract ECFieldElement Multiply(ECFieldElement b);

		// Token: 0x06000F05 RID: 3845
		public abstract ECFieldElement Divide(ECFieldElement b);

		// Token: 0x06000F06 RID: 3846
		public abstract ECFieldElement Negate();

		// Token: 0x06000F07 RID: 3847
		public abstract ECFieldElement Square();

		// Token: 0x06000F08 RID: 3848
		public abstract ECFieldElement Invert();

		// Token: 0x06000F09 RID: 3849
		public abstract ECFieldElement Sqrt();

		// Token: 0x06000F0A RID: 3850 RVA: 0x000575E4 File Offset: 0x000565E4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECFieldElement ecfieldElement = obj as ECFieldElement;
			return ecfieldElement != null && this.Equals(ecfieldElement);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0005760A File Offset: 0x0005660A
		protected bool Equals(ECFieldElement other)
		{
			return this.ToBigInteger().Equals(other.ToBigInteger());
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0005761D File Offset: 0x0005661D
		public override int GetHashCode()
		{
			return this.ToBigInteger().GetHashCode();
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0005762A File Offset: 0x0005662A
		public override string ToString()
		{
			return this.ToBigInteger().ToString(2);
		}
	}
}
