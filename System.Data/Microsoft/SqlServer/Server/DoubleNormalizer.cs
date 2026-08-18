using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000295 RID: 661
	internal sealed class DoubleNormalizer : Normalizer
	{
		// Token: 0x0600224F RID: 8783 RVA: 0x0028B9D8 File Offset: 0x0028ADD8
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

		// Token: 0x06002250 RID: 8784 RVA: 0x0028BA58 File Offset: 0x0028AE58
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

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x0028BAC8 File Offset: 0x0028AEC8
		internal override int Size
		{
			get
			{
				return 8;
			}
		}
	}
}
