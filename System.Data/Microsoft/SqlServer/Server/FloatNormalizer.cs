using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000294 RID: 660
	internal sealed class FloatNormalizer : Normalizer
	{
		// Token: 0x0600224B RID: 8779 RVA: 0x0028B8C8 File Offset: 0x0028ACC8
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

		// Token: 0x0600224C RID: 8780 RVA: 0x0028B938 File Offset: 0x0028AD38
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

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x0028B9A8 File Offset: 0x0028ADA8
		internal override int Size
		{
			get
			{
				return 4;
			}
		}
	}
}
