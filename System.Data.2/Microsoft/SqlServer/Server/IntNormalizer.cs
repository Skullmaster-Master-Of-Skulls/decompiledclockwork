using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000065 RID: 101
	internal sealed class IntNormalizer : Normalizer
	{
		// Token: 0x06000510 RID: 1296 RVA: 0x00046F18 File Offset: 0x00046318
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((int)base.GetValue(fi, obj));
			if (!this.m_skipNormalize)
			{
				Array.Reverse(bytes);
				byte[] array = bytes;
				int num = 0;
				array[num] ^= 128;
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00046F64 File Offset: 0x00046364
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[4];
			s.Read(array, 0, array.Length);
			if (!this.m_skipNormalize)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] ^= 128;
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToInt32(array, 0));
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00046FB8 File Offset: 0x000463B8
		internal override int Size
		{
			get
			{
				return 4;
			}
		}
	}
}
