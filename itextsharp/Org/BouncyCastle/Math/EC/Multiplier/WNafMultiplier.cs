using System;

namespace Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000082 RID: 130
	internal class WNafMultiplier : ECMultiplier
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x000160C8 File Offset: 0x000150C8
		public sbyte[] WindowNaf(sbyte width, BigInteger k)
		{
			sbyte[] array = new sbyte[k.BitLength + 1];
			short num = (short)(1 << (int)width);
			BigInteger m = BigInteger.ValueOf((long)num);
			int num2 = 0;
			int num3 = 0;
			while (k.SignValue > 0)
			{
				if (k.TestBit(0))
				{
					BigInteger bigInteger = k.Mod(m);
					if (bigInteger.TestBit((int)(width - 1)))
					{
						array[num2] = (sbyte)(bigInteger.IntValue - (int)num);
					}
					else
					{
						array[num2] = (sbyte)bigInteger.IntValue;
					}
					k = k.Subtract(BigInteger.ValueOf((long)array[num2]));
					num3 = num2;
				}
				else
				{
					array[num2] = 0;
				}
				k = k.ShiftRight(1);
				num2++;
			}
			num3++;
			sbyte[] array2 = new sbyte[num3];
			Array.Copy(array, 0, array2, 0, num3);
			return array2;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00016180 File Offset: 0x00015180
		public ECPoint Multiply(ECPoint p, BigInteger k, PreCompInfo preCompInfo)
		{
			WNafPreCompInfo wnafPreCompInfo;
			if (preCompInfo != null && preCompInfo is WNafPreCompInfo)
			{
				wnafPreCompInfo = (WNafPreCompInfo)preCompInfo;
			}
			else
			{
				wnafPreCompInfo = new WNafPreCompInfo();
			}
			int bitLength = k.BitLength;
			sbyte width;
			int num;
			if (bitLength < 13)
			{
				width = 2;
				num = 1;
			}
			else if (bitLength < 41)
			{
				width = 3;
				num = 2;
			}
			else if (bitLength < 121)
			{
				width = 4;
				num = 4;
			}
			else if (bitLength < 337)
			{
				width = 5;
				num = 8;
			}
			else if (bitLength < 897)
			{
				width = 6;
				num = 16;
			}
			else if (bitLength < 2305)
			{
				width = 7;
				num = 32;
			}
			else
			{
				width = 8;
				num = 127;
			}
			int num2 = 1;
			ECPoint[] array = wnafPreCompInfo.GetPreComp();
			ECPoint ecpoint = wnafPreCompInfo.GetTwiceP();
			if (array == null)
			{
				array = new ECPoint[]
				{
					p
				};
			}
			else
			{
				num2 = array.Length;
			}
			if (ecpoint == null)
			{
				ecpoint = p.Twice();
			}
			if (num2 < num)
			{
				ECPoint[] sourceArray = array;
				array = new ECPoint[num];
				Array.Copy(sourceArray, 0, array, 0, num2);
				for (int i = num2; i < num; i++)
				{
					array[i] = ecpoint.Add(array[i - 1]);
				}
			}
			sbyte[] array2 = this.WindowNaf(width, k);
			int num3 = array2.Length;
			ECPoint ecpoint2 = p.Curve.Infinity;
			for (int j = num3 - 1; j >= 0; j--)
			{
				ecpoint2 = ecpoint2.Twice();
				if (array2[j] != 0)
				{
					if (array2[j] > 0)
					{
						ecpoint2 = ecpoint2.Add(array[(int)((array2[j] - 1) / 2)]);
					}
					else
					{
						ecpoint2 = ecpoint2.Subtract(array[(int)((-array2[j] - 1) / 2)]);
					}
				}
			}
			wnafPreCompInfo.SetPreComp(array);
			wnafPreCompInfo.SetTwiceP(ecpoint);
			p.SetPreCompInfo(wnafPreCompInfo);
			return ecpoint2;
		}
	}
}
