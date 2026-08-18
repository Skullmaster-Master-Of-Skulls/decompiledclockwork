using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000069 RID: 105
	internal sealed class FloatNormalizer : Normalizer
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x000471E8 File Offset: 0x000465E8
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			float num = (float)base.GetValue(fi, obj);
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
				else if (num < 0f)
				{
					base.FlipAllBits(bytes);
				}
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00047250 File Offset: 0x00046650
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[4];
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
			base.SetValue(fi, recvr, BitConverter.ToSingle(array, 0));
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x000472B8 File Offset: 0x000466B8
		internal override int Size
		{
			get
			{
				return 4;
			}
		}
	}
}
