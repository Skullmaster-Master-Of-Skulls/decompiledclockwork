using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200006A RID: 106
	internal sealed class DoubleNormalizer : Normalizer
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x000472DC File Offset: 0x000466DC
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			double num = (double)base.GetValue(fi, obj);
			byte[] bytes = BitConverter.GetBytes(num);
			if (!this.m_skipNormalize)
			{
				Array.Reverse(bytes);
				if ((bytes[0] & 128) == 0)
				{
					byte[] array = bytes;
					int num2 = 0;
					array[num2] ^= 128;
				}
				else if (num < 0.0)
				{
					base.FlipAllBits(bytes);
				}
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00047348 File Offset: 0x00046748
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[8];
			s.Read(array, 0, array.Length);
			if (!this.m_skipNormalize)
			{
				if ((array[0] & 128) > 0)
				{
					byte[] array2 = array;
					int num = 0;
					array2[num] ^= 128;
				}
				else
				{
					base.FlipAllBits(array);
				}
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToDouble(array, 0));
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x000473B0 File Offset: 0x000467B0
		internal override int Size
		{
			get
			{
				return 8;
			}
		}
	}
}
