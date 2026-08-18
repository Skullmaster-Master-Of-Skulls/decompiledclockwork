using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200028E RID: 654
	internal sealed class ShortNormalizer : Normalizer
	{
		// Token: 0x06002233 RID: 8755 RVA: 0x0028B3B8 File Offset: 0x0028A7B8
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((short)base.GetValue(fi, obj));
			if (!this.m_skipNormalize)
			{
				Array.Reverse(bytes);
				byte[] array = bytes;
				int num = 0;
				array[num] ^= 128;
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x0028B418 File Offset: 0x0028A818
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[2];
			s.Read(array, 0, array.Length);
			if (!this.m_skipNormalize)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] ^= 128;
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToInt16(array, 0));
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x0028B478 File Offset: 0x0028A878
		internal override int Size
		{
			get
			{
				return 2;
			}
		}
	}
}
