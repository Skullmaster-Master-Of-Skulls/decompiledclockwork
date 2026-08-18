using System;

namespace Org.BouncyCastle.Math.EC.Abc
{
	// Token: 0x0200000D RID: 13
	internal class Tnaf
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00004488 File Offset: 0x00003488
		public static BigInteger Norm(sbyte mu, ZTauElement lambda)
		{
			BigInteger bigInteger = lambda.u.Multiply(lambda.u);
			BigInteger bigInteger2 = lambda.u.Multiply(lambda.v);
			BigInteger value = lambda.v.Multiply(lambda.v).ShiftLeft(1);
			BigInteger result;
			if (mu == 1)
			{
				result = bigInteger.Add(bigInteger2).Add(value);
			}
			else
			{
				if (mu != -1)
				{
					throw new ArgumentException("mu must be 1 or -1");
				}
				result = bigInteger.Subtract(bigInteger2).Add(value);
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004508 File Offset: 0x00003508
		public static SimpleBigDecimal Norm(sbyte mu, SimpleBigDecimal u, SimpleBigDecimal v)
		{
			SimpleBigDecimal simpleBigDecimal = u.Multiply(u);
			SimpleBigDecimal b = u.Multiply(v);
			SimpleBigDecimal b2 = v.Multiply(v).ShiftLeft(1);
			SimpleBigDecimal result;
			if (mu == 1)
			{
				result = simpleBigDecimal.Add(b).Add(b2);
			}
			else
			{
				if (mu != -1)
				{
					throw new ArgumentException("mu must be 1 or -1");
				}
				result = simpleBigDecimal.Subtract(b).Add(b2);
			}
			return result;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004568 File Offset: 0x00003568
		public static ZTauElement Round(SimpleBigDecimal lambda0, SimpleBigDecimal lambda1, sbyte mu)
		{
			int scale = lambda0.Scale;
			if (lambda1.Scale != scale)
			{
				throw new ArgumentException("lambda0 and lambda1 do not have same scale");
			}
			if (mu != 1 && mu != -1)
			{
				throw new ArgumentException("mu must be 1 or -1");
			}
			BigInteger bigInteger = lambda0.Round();
			BigInteger bigInteger2 = lambda1.Round();
			SimpleBigDecimal simpleBigDecimal = lambda0.Subtract(bigInteger);
			SimpleBigDecimal simpleBigDecimal2 = lambda1.Subtract(bigInteger2);
			SimpleBigDecimal simpleBigDecimal3 = simpleBigDecimal.Add(simpleBigDecimal);
			if (mu == 1)
			{
				simpleBigDecimal3 = simpleBigDecimal3.Add(simpleBigDecimal2);
			}
			else
			{
				simpleBigDecimal3 = simpleBigDecimal3.Subtract(simpleBigDecimal2);
			}
			SimpleBigDecimal simpleBigDecimal4 = simpleBigDecimal2.Add(simpleBigDecimal2).Add(simpleBigDecimal2);
			SimpleBigDecimal b = simpleBigDecimal4.Add(simpleBigDecimal2);
			SimpleBigDecimal simpleBigDecimal5;
			SimpleBigDecimal simpleBigDecimal6;
			if (mu == 1)
			{
				simpleBigDecimal5 = simpleBigDecimal.Subtract(simpleBigDecimal4);
				simpleBigDecimal6 = simpleBigDecimal.Add(b);
			}
			else
			{
				simpleBigDecimal5 = simpleBigDecimal.Add(simpleBigDecimal4);
				simpleBigDecimal6 = simpleBigDecimal.Subtract(b);
			}
			sbyte b2 = 0;
			sbyte b3 = 0;
			if (simpleBigDecimal3.CompareTo(BigInteger.One) >= 0)
			{
				if (simpleBigDecimal5.CompareTo(Tnaf.MinusOne) < 0)
				{
					b3 = mu;
				}
				else
				{
					b2 = 1;
				}
			}
			else if (simpleBigDecimal6.CompareTo(BigInteger.Two) >= 0)
			{
				b3 = mu;
			}
			if (simpleBigDecimal3.CompareTo(Tnaf.MinusOne) < 0)
			{
				if (simpleBigDecimal5.CompareTo(BigInteger.One) >= 0)
				{
					b3 = -mu;
				}
				else
				{
					b2 = -1;
				}
			}
			else if (simpleBigDecimal6.CompareTo(Tnaf.MinusTwo) < 0)
			{
				b3 = -mu;
			}
			BigInteger u = bigInteger.Add(BigInteger.ValueOf((long)b2));
			BigInteger v = bigInteger2.Add(BigInteger.ValueOf((long)b3));
			return new ZTauElement(u, v);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000046DC File Offset: 0x000036DC
		public static SimpleBigDecimal ApproximateDivisionByN(BigInteger k, BigInteger s, BigInteger vm, sbyte a, int m, int c)
		{
			int num = (m + 5) / 2 + c;
			BigInteger val = k.ShiftRight(m - num - 2 + (int)a);
			BigInteger bigInteger = s.Multiply(val);
			BigInteger val2 = bigInteger.ShiftRight(m);
			BigInteger value = vm.Multiply(val2);
			BigInteger bigInteger2 = bigInteger.Add(value);
			BigInteger bigInteger3 = bigInteger2.ShiftRight(num - c);
			if (bigInteger2.TestBit(num - c - 1))
			{
				bigInteger3 = bigInteger3.Add(BigInteger.One);
			}
			return new SimpleBigDecimal(bigInteger3, c);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000475C File Offset: 0x0000375C
		public static sbyte[] TauAdicNaf(sbyte mu, ZTauElement lambda)
		{
			if (mu != 1 && mu != -1)
			{
				throw new ArgumentException("mu must be 1 or -1");
			}
			BigInteger bigInteger = Tnaf.Norm(mu, lambda);
			int bitLength = bigInteger.BitLength;
			int num = (bitLength > 30) ? (bitLength + 4) : 34;
			sbyte[] array = new sbyte[num];
			int num2 = 0;
			int num3 = 0;
			BigInteger bigInteger2 = lambda.u;
			BigInteger bigInteger3 = lambda.v;
			while (!bigInteger2.Equals(BigInteger.Zero) || !bigInteger3.Equals(BigInteger.Zero))
			{
				if (bigInteger2.TestBit(0))
				{
					array[num2] = (sbyte)BigInteger.Two.Subtract(bigInteger2.Subtract(bigInteger3.ShiftLeft(1)).Mod(Tnaf.Four)).IntValue;
					if (array[num2] == 1)
					{
						bigInteger2 = bigInteger2.ClearBit(0);
					}
					else
					{
						bigInteger2 = bigInteger2.Add(BigInteger.One);
					}
					num3 = num2;
				}
				else
				{
					array[num2] = 0;
				}
				BigInteger bigInteger4 = bigInteger2;
				BigInteger bigInteger5 = bigInteger2.ShiftRight(1);
				if (mu == 1)
				{
					bigInteger2 = bigInteger3.Add(bigInteger5);
				}
				else
				{
					bigInteger2 = bigInteger3.Subtract(bigInteger5);
				}
				bigInteger3 = bigInteger4.ShiftRight(1).Negate();
				num2++;
			}
			num3++;
			sbyte[] array2 = new sbyte[num3];
			Array.Copy(array, 0, array2, 0, num3);
			return array2;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000489C File Offset: 0x0000389C
		public static F2mPoint Tau(F2mPoint p)
		{
			if (p.IsInfinity)
			{
				return p;
			}
			ECFieldElement x = p.X;
			ECFieldElement y = p.Y;
			return new F2mPoint(p.Curve, x.Square(), y.Square(), p.IsCompressed);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000048E0 File Offset: 0x000038E0
		public static sbyte GetMu(F2mCurve curve)
		{
			BigInteger bigInteger = curve.A.ToBigInteger();
			sbyte result;
			if (bigInteger.SignValue == 0)
			{
				result = -1;
			}
			else
			{
				if (!bigInteger.Equals(BigInteger.One))
				{
					throw new ArgumentException("No Koblitz curve (ABC), TNAF multiplication not possible");
				}
				result = 1;
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004924 File Offset: 0x00003924
		public static BigInteger[] GetLucas(sbyte mu, int k, bool doV)
		{
			if (mu != 1 && mu != -1)
			{
				throw new ArgumentException("mu must be 1 or -1");
			}
			BigInteger bigInteger;
			BigInteger bigInteger2;
			if (doV)
			{
				bigInteger = BigInteger.Two;
				bigInteger2 = BigInteger.ValueOf((long)mu);
			}
			else
			{
				bigInteger = BigInteger.Zero;
				bigInteger2 = BigInteger.One;
			}
			for (int i = 1; i < k; i++)
			{
				BigInteger bigInteger3;
				if (mu == 1)
				{
					bigInteger3 = bigInteger2;
				}
				else
				{
					bigInteger3 = bigInteger2.Negate();
				}
				BigInteger bigInteger4 = bigInteger3.Subtract(bigInteger.ShiftLeft(1));
				bigInteger = bigInteger2;
				bigInteger2 = bigInteger4;
			}
			return new BigInteger[]
			{
				bigInteger,
				bigInteger2
			};
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000049B0 File Offset: 0x000039B0
		public static BigInteger GetTw(sbyte mu, int w)
		{
			if (w != 4)
			{
				BigInteger[] lucas = Tnaf.GetLucas(mu, w, false);
				BigInteger m = BigInteger.Zero.SetBit(w);
				BigInteger val = lucas[1].ModInverse(m);
				return BigInteger.Two.Multiply(lucas[0]).Multiply(val).Mod(m);
			}
			if (mu == 1)
			{
				return BigInteger.ValueOf(6L);
			}
			return BigInteger.ValueOf(10L);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004A10 File Offset: 0x00003A10
		public static BigInteger[] GetSi(F2mCurve curve)
		{
			if (!curve.IsKoblitz)
			{
				throw new ArgumentException("si is defined for Koblitz curves only");
			}
			int m = curve.M;
			int intValue = curve.A.ToBigInteger().IntValue;
			sbyte mu = curve.GetMu();
			int intValue2 = curve.H.IntValue;
			int k = m + 3 - intValue;
			BigInteger[] lucas = Tnaf.GetLucas(mu, k, false);
			BigInteger bigInteger;
			BigInteger bigInteger2;
			if (mu == 1)
			{
				bigInteger = BigInteger.One.Subtract(lucas[1]);
				bigInteger2 = BigInteger.One.Subtract(lucas[0]);
			}
			else
			{
				if (mu != -1)
				{
					throw new ArgumentException("mu must be 1 or -1");
				}
				bigInteger = BigInteger.One.Add(lucas[1]);
				bigInteger2 = BigInteger.One.Add(lucas[0]);
			}
			BigInteger[] array = new BigInteger[2];
			if (intValue2 == 2)
			{
				array[0] = bigInteger.ShiftRight(1);
				array[1] = bigInteger2.ShiftRight(1).Negate();
			}
			else
			{
				if (intValue2 != 4)
				{
					throw new ArgumentException("h (Cofactor) must be 2 or 4");
				}
				array[0] = bigInteger.ShiftRight(2);
				array[1] = bigInteger2.ShiftRight(2).Negate();
			}
			return array;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004B20 File Offset: 0x00003B20
		public static ZTauElement PartModReduction(BigInteger k, int m, sbyte a, BigInteger[] s, sbyte mu, sbyte c)
		{
			BigInteger bigInteger;
			if (mu == 1)
			{
				bigInteger = s[0].Add(s[1]);
			}
			else
			{
				bigInteger = s[0].Subtract(s[1]);
			}
			BigInteger[] lucas = Tnaf.GetLucas(mu, m, true);
			BigInteger vm = lucas[1];
			SimpleBigDecimal lambda = Tnaf.ApproximateDivisionByN(k, s[0], vm, a, m, (int)c);
			SimpleBigDecimal lambda2 = Tnaf.ApproximateDivisionByN(k, s[1], vm, a, m, (int)c);
			ZTauElement ztauElement = Tnaf.Round(lambda, lambda2, mu);
			BigInteger u = k.Subtract(bigInteger.Multiply(ztauElement.u)).Subtract(BigInteger.ValueOf(2L).Multiply(s[1]).Multiply(ztauElement.v));
			BigInteger v = s[1].Multiply(ztauElement.u).Subtract(s[0].Multiply(ztauElement.v));
			return new ZTauElement(u, v);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004BE8 File Offset: 0x00003BE8
		public static F2mPoint MultiplyRTnaf(F2mPoint p, BigInteger k)
		{
			F2mCurve f2mCurve = (F2mCurve)p.Curve;
			int m = f2mCurve.M;
			sbyte a = (sbyte)f2mCurve.A.ToBigInteger().IntValue;
			sbyte mu = f2mCurve.GetMu();
			BigInteger[] si = f2mCurve.GetSi();
			ZTauElement lambda = Tnaf.PartModReduction(k, m, a, si, mu, 10);
			return Tnaf.MultiplyTnaf(p, lambda);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004C40 File Offset: 0x00003C40
		public static F2mPoint MultiplyTnaf(F2mPoint p, ZTauElement lambda)
		{
			F2mCurve f2mCurve = (F2mCurve)p.Curve;
			sbyte mu = f2mCurve.GetMu();
			sbyte[] u = Tnaf.TauAdicNaf(mu, lambda);
			return Tnaf.MultiplyFromTnaf(p, u);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004C74 File Offset: 0x00003C74
		public static F2mPoint MultiplyFromTnaf(F2mPoint p, sbyte[] u)
		{
			F2mCurve f2mCurve = (F2mCurve)p.Curve;
			F2mPoint f2mPoint = (F2mPoint)f2mCurve.Infinity;
			for (int i = u.Length - 1; i >= 0; i--)
			{
				f2mPoint = Tnaf.Tau(f2mPoint);
				if (u[i] == 1)
				{
					f2mPoint = f2mPoint.AddSimple(p);
				}
				else if (u[i] == -1)
				{
					f2mPoint = f2mPoint.SubtractSimple(p);
				}
			}
			return f2mPoint;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004CD0 File Offset: 0x00003CD0
		public static sbyte[] TauAdicWNaf(sbyte mu, ZTauElement lambda, sbyte width, BigInteger pow2w, BigInteger tw, ZTauElement[] alpha)
		{
			if (mu != 1 && mu != -1)
			{
				throw new ArgumentException("mu must be 1 or -1");
			}
			BigInteger bigInteger = Tnaf.Norm(mu, lambda);
			int bitLength = bigInteger.BitLength;
			int num = (bitLength > 30) ? (bitLength + 4 + (int)width) : ((int)(34 + width));
			sbyte[] array = new sbyte[num];
			BigInteger value = pow2w.ShiftRight(1);
			BigInteger bigInteger2 = lambda.u;
			BigInteger bigInteger3 = lambda.v;
			int num2 = 0;
			while (!bigInteger2.Equals(BigInteger.Zero) || !bigInteger3.Equals(BigInteger.Zero))
			{
				if (bigInteger2.TestBit(0))
				{
					BigInteger bigInteger4 = bigInteger2.Add(bigInteger3.Multiply(tw)).Mod(pow2w);
					sbyte b;
					if (bigInteger4.CompareTo(value) >= 0)
					{
						b = (sbyte)bigInteger4.Subtract(pow2w).IntValue;
					}
					else
					{
						b = (sbyte)bigInteger4.IntValue;
					}
					array[num2] = b;
					bool flag = true;
					if (b < 0)
					{
						flag = false;
						b = -b;
					}
					if (flag)
					{
						bigInteger2 = bigInteger2.Subtract(alpha[(int)b].u);
						bigInteger3 = bigInteger3.Subtract(alpha[(int)b].v);
					}
					else
					{
						bigInteger2 = bigInteger2.Add(alpha[(int)b].u);
						bigInteger3 = bigInteger3.Add(alpha[(int)b].v);
					}
				}
				else
				{
					array[num2] = 0;
				}
				BigInteger bigInteger5 = bigInteger2;
				if (mu == 1)
				{
					bigInteger2 = bigInteger3.Add(bigInteger2.ShiftRight(1));
				}
				else
				{
					bigInteger2 = bigInteger3.Subtract(bigInteger2.ShiftRight(1));
				}
				bigInteger3 = bigInteger5.ShiftRight(1).Negate();
				num2++;
			}
			return array;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004E5C File Offset: 0x00003E5C
		public static F2mPoint[] GetPreComp(F2mPoint p, sbyte a)
		{
			F2mPoint[] array = new F2mPoint[16];
			array[1] = p;
			sbyte[][] array2;
			if (a == 0)
			{
				array2 = Tnaf.Alpha0Tnaf;
			}
			else
			{
				array2 = Tnaf.Alpha1Tnaf;
			}
			int num = array2.Length;
			for (int i = 3; i < num; i += 2)
			{
				array[i] = Tnaf.MultiplyFromTnaf(p, array2[i]);
			}
			return array;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004EA4 File Offset: 0x00003EA4
		// Note: this type is marked as 'beforefieldinit'.
		static Tnaf()
		{
			ZTauElement[] array = new ZTauElement[9];
			array[1] = new ZTauElement(BigInteger.One, BigInteger.Zero);
			array[3] = new ZTauElement(Tnaf.MinusThree, Tnaf.MinusOne);
			array[5] = new ZTauElement(Tnaf.MinusOne, Tnaf.MinusOne);
			array[7] = new ZTauElement(BigInteger.One, Tnaf.MinusOne);
			Tnaf.Alpha0 = array;
			Tnaf.Alpha0Tnaf = new sbyte[][]
			{
				default(sbyte[]),
				new sbyte[]
				{
					1
				},
				default(sbyte[]),
				new sbyte[]
				{
					-1,
					0,
					1
				},
				default(sbyte[]),
				new sbyte[]
				{
					1,
					0,
					1
				},
				default(sbyte[]),
				new sbyte[]
				{
					-1,
					0,
					0,
					1
				}
			};
			ZTauElement[] array2 = new ZTauElement[9];
			array2[1] = new ZTauElement(BigInteger.One, BigInteger.Zero);
			array2[3] = new ZTauElement(Tnaf.MinusThree, BigInteger.One);
			array2[5] = new ZTauElement(Tnaf.MinusOne, BigInteger.One);
			array2[7] = new ZTauElement(BigInteger.One, BigInteger.One);
			Tnaf.Alpha1 = array2;
			Tnaf.Alpha1Tnaf = new sbyte[][]
			{
				default(sbyte[]),
				new sbyte[]
				{
					1
				},
				default(sbyte[]),
				new sbyte[]
				{
					-1,
					0,
					1
				},
				default(sbyte[]),
				new sbyte[]
				{
					1,
					0,
					1
				},
				default(sbyte[]),
				new sbyte[]
				{
					-1,
					0,
					0,
					-1
				}
			};
		}

		// Token: 0x04000019 RID: 25
		public const sbyte Width = 4;

		// Token: 0x0400001A RID: 26
		public const sbyte Pow2Width = 16;

		// Token: 0x0400001B RID: 27
		private static readonly BigInteger MinusOne = BigInteger.One.Negate();

		// Token: 0x0400001C RID: 28
		private static readonly BigInteger MinusTwo = BigInteger.Two.Negate();

		// Token: 0x0400001D RID: 29
		private static readonly BigInteger MinusThree = BigInteger.Three.Negate();

		// Token: 0x0400001E RID: 30
		private static readonly BigInteger Four = BigInteger.ValueOf(4L);

		// Token: 0x0400001F RID: 31
		public static readonly ZTauElement[] Alpha0;

		// Token: 0x04000020 RID: 32
		public static readonly sbyte[][] Alpha0Tnaf;

		// Token: 0x04000021 RID: 33
		public static readonly ZTauElement[] Alpha1;

		// Token: 0x04000022 RID: 34
		public static readonly sbyte[][] Alpha1Tnaf;
	}
}
