using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200013E RID: 318
	public sealed class X9IntegerConverter
	{
		// Token: 0x06000B8C RID: 2956 RVA: 0x000409CF File Offset: 0x0003F9CF
		private X9IntegerConverter()
		{
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000409D7 File Offset: 0x0003F9D7
		public static int GetByteLength(ECFieldElement fe)
		{
			return (fe.FieldSize + 7) / 8;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x000409E3 File Offset: 0x0003F9E3
		public static int GetByteLength(ECCurve c)
		{
			return (c.FieldSize + 7) / 8;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000409F0 File Offset: 0x0003F9F0
		public static byte[] IntegerToBytes(BigInteger s, int qLength)
		{
			byte[] array = s.ToByteArrayUnsigned();
			if (qLength < array.Length)
			{
				byte[] array2 = new byte[qLength];
				Array.Copy(array, array.Length - array2.Length, array2, 0, array2.Length);
				return array2;
			}
			if (qLength > array.Length)
			{
				byte[] array3 = new byte[qLength];
				Array.Copy(array, 0, array3, array3.Length - array.Length, array.Length);
				return array3;
			}
			return array;
		}
	}
}
