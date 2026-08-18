using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000118 RID: 280
	internal static class DEREncoding
	{
		// Token: 0x060007A7 RID: 1959 RVA: 0x000204D4 File Offset: 0x0001E6D4
		private static bool BufferIsEqual(byte[] arrayOne, int offsetOne, byte[] arrayTwo, int offsetTwo, int length)
		{
			if (length > arrayOne.Length - offsetOne)
			{
				return false;
			}
			if (length > arrayTwo.Length - offsetTwo)
			{
				return false;
			}
			for (int i = 0; i < length; i++)
			{
				if (arrayOne[offsetOne + i] != arrayTwo[offsetTwo + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00020513 File Offset: 0x0001E713
		public static int LengthSize(int length)
		{
			if (length < 128)
			{
				return 1;
			}
			if (length < 256)
			{
				return 2;
			}
			if (length < 65536)
			{
				return 3;
			}
			if (length < 16777216)
			{
				return 4;
			}
			return 5;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00020540 File Offset: 0x0001E740
		public static void MakeTokenHeader(int bodySize, byte[] buffer, ref int offset, ref int len)
		{
			int num = offset;
			offset = num + 1;
			buffer[num] = 96;
			len--;
			DEREncoding.WriteLength(buffer, ref offset, ref len, 1 + DEREncoding.LengthSize(DEREncoding.mech.Length) + DEREncoding.mech.Length + DEREncoding.type.Length + bodySize);
			num = offset;
			offset = num + 1;
			buffer[num] = 6;
			len--;
			DEREncoding.WriteLength(buffer, ref offset, ref len, DEREncoding.mech.Length);
			Buffer.BlockCopy(DEREncoding.mech, 0, buffer, offset, DEREncoding.mech.Length);
			offset += DEREncoding.mech.Length;
			len -= DEREncoding.mech.Length;
			Buffer.BlockCopy(DEREncoding.type, 0, buffer, offset, DEREncoding.type.Length);
			offset += DEREncoding.type.Length;
			len -= DEREncoding.type.Length;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00020604 File Offset: 0x0001E804
		public static int ReadLength(byte[] buffer, ref int offset, ref int length)
		{
			int num = 0;
			if (length < 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SystemException());
			}
			int num2 = offset;
			offset = num2 + 1;
			int num3 = (int)buffer[num2];
			length--;
			if ((num3 & 128) != 0)
			{
				if ((num3 &= 127) > length - 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SystemException());
				}
				if (num3 > 4)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SystemException());
				}
				while (num3 != 0)
				{
					int num4 = num << 8;
					num2 = offset;
					offset = num2 + 1;
					num = num4 + (int)buffer[num2];
					length--;
					num3--;
				}
			}
			else
			{
				num = num3;
			}
			return num;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00020695 File Offset: 0x0001E895
		public static int TokenSize(int bodySize)
		{
			bodySize += 2 + DEREncoding.mech.Length + DEREncoding.LengthSize(DEREncoding.mech.Length) + 1;
			return 1 + DEREncoding.LengthSize(bodySize) + bodySize;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000206C0 File Offset: 0x0001E8C0
		public static void VerifyTokenHeader(byte[] buffer, ref int offset, ref int len)
		{
			if (--len < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SystemException());
			}
			int num = offset;
			offset = num + 1;
			if (buffer[num] != 96)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SystemException());
			}
			int num2 = DEREncoding.ReadLength(buffer, ref offset, ref len);
			if (num2 != len)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SystemException());
			}
			if (--len < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SystemException());
			}
			num = offset;
			offset = num + 1;
			if (buffer[num] != 6)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SystemException());
			}
			int num3 = DEREncoding.ReadLength(buffer, ref offset, ref len);
			if ((num3 & 2147483647) != DEREncoding.mech.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SystemException());
			}
			if ((len -= num3) < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SystemException());
			}
			if (!DEREncoding.BufferIsEqual(DEREncoding.mech, 0, buffer, offset, DEREncoding.mech.Length))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SystemException());
			}
			offset += num3;
			if ((len -= DEREncoding.type.Length) < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SystemException());
			}
			if (!DEREncoding.BufferIsEqual(DEREncoding.type, 0, buffer, offset, DEREncoding.type.Length))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SystemException());
			}
			offset += DEREncoding.type.Length;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00020828 File Offset: 0x0001EA28
		public static void WriteLength(byte[] buffer, ref int offset, ref int bufferLength, int length)
		{
			int num;
			if (length < 128)
			{
				num = offset;
				offset = num + 1;
				buffer[num] = (byte)length;
				bufferLength--;
				return;
			}
			num = offset;
			offset = num + 1;
			buffer[num] = (byte)(DEREncoding.LengthSize(length) + 127);
			if (length >= 16777216)
			{
				num = offset;
				offset = num + 1;
				buffer[num] = (byte)(length >> 24);
				bufferLength--;
			}
			if (length >= 65536)
			{
				num = offset;
				offset = num + 1;
				buffer[num] = (byte)(length >> 16 & 255);
				bufferLength--;
			}
			if (length >= 256)
			{
				num = offset;
				offset = num + 1;
				buffer[num] = (byte)(length >> 8 & 255);
				bufferLength--;
			}
			num = offset;
			offset = num + 1;
			buffer[num] = (byte)(length & 255);
			bufferLength--;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000208E4 File Offset: 0x0001EAE4
		// Note: this type is marked as 'beforefieldinit'.
		static DEREncoding()
		{
			byte[] array = new byte[2];
			array[0] = 1;
			DEREncoding.type = array;
		}

		// Token: 0x04000AD3 RID: 2771
		private static byte[] mech = new byte[]
		{
			42,
			134,
			72,
			134,
			247,
			18,
			1,
			2,
			2
		};

		// Token: 0x04000AD4 RID: 2772
		private static byte[] type;
	}
}
