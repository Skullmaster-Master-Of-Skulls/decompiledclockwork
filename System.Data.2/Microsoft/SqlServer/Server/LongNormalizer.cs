using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000067 RID: 103
	internal sealed class LongNormalizer : Normalizer
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x00047080 File Offset: 0x00046480
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((long)base.GetValue(fi, obj));
			if (!this.m_skipNormalize)
			{
				Array.Reverse(bytes);
				byte[] array = bytes;
				int num = 0;
				array[num] ^= 128;
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000470CC File Offset: 0x000464CC
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[8];
			s.Read(array, 0, array.Length);
			if (!this.m_skipNormalize)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] ^= 128;
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToInt64(array, 0));
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00047120 File Offset: 0x00046520
		internal override int Size
		{
			get
			{
				return 8;
			}
		}
	}
}
