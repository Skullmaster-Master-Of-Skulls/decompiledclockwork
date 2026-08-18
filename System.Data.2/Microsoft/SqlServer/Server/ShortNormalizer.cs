using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000063 RID: 99
	internal sealed class ShortNormalizer : Normalizer
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x00046DB0 File Offset: 0x000461B0
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

		// Token: 0x06000509 RID: 1289 RVA: 0x00046DFC File Offset: 0x000461FC
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

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00046E50 File Offset: 0x00046250
		internal override int Size
		{
			get
			{
				return 2;
			}
		}
	}
}
