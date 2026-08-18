using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x02000185 RID: 389
	public class F2mFieldElement : ECFieldElement
	{
		// Token: 0x06000F20 RID: 3872 RVA: 0x00057BA4 File Offset: 0x00056BA4
		public F2mFieldElement(int m, int k1, int k2, int k3, BigInteger x)
		{
			this.t = m + 31 >> 5;
			this.x = new IntArray(x, this.t);
			if (k2 == 0 && k3 == 0)
			{
				this.representation = 2;
			}
			else
			{
				if (k2 >= k3)
				{
					throw new ArgumentException("k2 must be smaller than k3");
				}
				if (k2 <= 0)
				{
					throw new ArgumentException("k2 must be larger than 0");
				}
				this.representation = 3;
			}
			if (x.SignValue < 0)
			{
				throw new ArgumentException("x value cannot be negative");
			}
			this.m = m;
			this.k1 = k1;
			this.k2 = k2;
			this.k3 = k3;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00057C3E File Offset: 0x00056C3E
		public F2mFieldElement(int m, int k, BigInteger x) : this(m, k, 0, 0, x)
		{
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00057C4C File Offset: 0x00056C4C
		private F2mFieldElement(int m, int k1, int k2, int k3, IntArray x)
		{
			this.t = m + 31 >> 5;
			this.x = x;
			this.m = m;
			this.k1 = k1;
			this.k2 = k2;
			this.k3 = k3;
			if (k2 == 0 && k3 == 0)
			{
				this.representation = 2;
				return;
			}
			this.representation = 3;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00057CA6 File Offset: 0x00056CA6
		public override BigInteger ToBigInteger()
		{
			return this.x.ToBigInteger();
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00057CB3 File Offset: 0x00056CB3
		public override string FieldName
		{
			get
			{
				return "F2m";
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00057CBA File Offset: 0x00056CBA
		public override int FieldSize
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00057CC4 File Offset: 0x00056CC4
		public static void CheckFieldElements(ECFieldElement a, ECFieldElement b)
		{
			if (!(a is F2mFieldElement) || !(b is F2mFieldElement))
			{
				throw new ArgumentException("Field elements are not both instances of F2mFieldElement");
			}
			F2mFieldElement f2mFieldElement = (F2mFieldElement)a;
			F2mFieldElement f2mFieldElement2 = (F2mFieldElement)b;
			if (f2mFieldElement.m != f2mFieldElement2.m || f2mFieldElement.k1 != f2mFieldElement2.k1 || f2mFieldElement.k2 != f2mFieldElement2.k2 || f2mFieldElement.k3 != f2mFieldElement2.k3)
			{
				throw new ArgumentException("Field elements are not elements of the same field F2m");
			}
			if (f2mFieldElement.representation != f2mFieldElement2.representation)
			{
				throw new ArgumentException("One of the field elements are not elements has incorrect representation");
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00057D58 File Offset: 0x00056D58
		public override ECFieldElement Add(ECFieldElement b)
		{
			IntArray intArray = (IntArray)this.x.Clone();
			F2mFieldElement f2mFieldElement = (F2mFieldElement)b;
			intArray.AddShifted(f2mFieldElement.x, 0);
			return new F2mFieldElement(this.m, this.k1, this.k2, this.k3, intArray);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00057DA8 File Offset: 0x00056DA8
		public override ECFieldElement Subtract(ECFieldElement b)
		{
			return this.Add(b);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00057DB4 File Offset: 0x00056DB4
		public override ECFieldElement Multiply(ECFieldElement b)
		{
			F2mFieldElement f2mFieldElement = (F2mFieldElement)b;
			IntArray intArray = this.x.Multiply(f2mFieldElement.x, this.m);
			intArray.Reduce(this.m, new int[]
			{
				this.k1,
				this.k2,
				this.k3
			});
			return new F2mFieldElement(this.m, this.k1, this.k2, this.k3, intArray);
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00057E30 File Offset: 0x00056E30
		public override ECFieldElement Divide(ECFieldElement b)
		{
			ECFieldElement b2 = b.Invert();
			return this.Multiply(b2);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00057E4B File Offset: 0x00056E4B
		public override ECFieldElement Negate()
		{
			return this;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00057E50 File Offset: 0x00056E50
		public override ECFieldElement Square()
		{
			IntArray intArray = this.x.Square(this.m);
			intArray.Reduce(this.m, new int[]
			{
				this.k1,
				this.k2,
				this.k3
			});
			return new F2mFieldElement(this.m, this.k1, this.k2, this.k3, intArray);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00057EBC File Offset: 0x00056EBC
		public override ECFieldElement Invert()
		{
			IntArray intArray = (IntArray)this.x.Clone();
			IntArray intArray2 = new IntArray(this.t);
			intArray2.SetBit(this.m);
			intArray2.SetBit(0);
			intArray2.SetBit(this.k1);
			if (this.representation == 3)
			{
				intArray2.SetBit(this.k2);
				intArray2.SetBit(this.k3);
			}
			IntArray intArray3 = new IntArray(this.t);
			intArray3.SetBit(0);
			IntArray intArray4 = new IntArray(this.t);
			while (intArray.GetUsedLength() > 0)
			{
				int num = intArray.BitLength - intArray2.BitLength;
				if (num < 0)
				{
					IntArray intArray5 = intArray;
					intArray = intArray2;
					intArray2 = intArray5;
					IntArray intArray6 = intArray3;
					intArray3 = intArray4;
					intArray4 = intArray6;
					num = -num;
				}
				int shift = num >> 5;
				int n = num & 31;
				IntArray other = intArray2.ShiftLeft(n);
				intArray.AddShifted(other, shift);
				IntArray other2 = intArray4.ShiftLeft(n);
				intArray3.AddShifted(other2, shift);
			}
			return new F2mFieldElement(this.m, this.k1, this.k2, this.k3, intArray4);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00057FCC File Offset: 0x00056FCC
		public override ECFieldElement Sqrt()
		{
			throw new ArithmeticException("Not implemented");
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00057FD8 File Offset: 0x00056FD8
		public int Representation
		{
			get
			{
				return this.representation;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00057FE0 File Offset: 0x00056FE0
		public int M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00057FE8 File Offset: 0x00056FE8
		public int K1
		{
			get
			{
				return this.k1;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00057FF0 File Offset: 0x00056FF0
		public int K2
		{
			get
			{
				return this.k2;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x00057FF8 File Offset: 0x00056FF8
		public int K3
		{
			get
			{
				return this.k3;
			}
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00058000 File Offset: 0x00057000
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			F2mFieldElement f2mFieldElement = obj as F2mFieldElement;
			return f2mFieldElement != null && this.Equals(f2mFieldElement);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00058028 File Offset: 0x00057028
		protected bool Equals(F2mFieldElement other)
		{
			return this.m == other.m && this.k1 == other.k1 && this.k2 == other.k2 && this.k3 == other.k3 && this.representation == other.representation && base.Equals(other);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00058084 File Offset: 0x00057084
		public override int GetHashCode()
		{
			return this.m.GetHashCode() ^ this.k1.GetHashCode() ^ this.k2.GetHashCode() ^ this.k3.GetHashCode() ^ this.representation.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04000B0F RID: 2831
		public const int Gnb = 1;

		// Token: 0x04000B10 RID: 2832
		public const int Tpb = 2;

		// Token: 0x04000B11 RID: 2833
		public const int Ppb = 3;

		// Token: 0x04000B12 RID: 2834
		private int representation;

		// Token: 0x04000B13 RID: 2835
		private int m;

		// Token: 0x04000B14 RID: 2836
		private int k1;

		// Token: 0x04000B15 RID: 2837
		private int k2;

		// Token: 0x04000B16 RID: 2838
		private int k3;

		// Token: 0x04000B17 RID: 2839
		private IntArray x;

		// Token: 0x04000B18 RID: 2840
		private readonly int t;
	}
}
