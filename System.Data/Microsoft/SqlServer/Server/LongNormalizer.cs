using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000292 RID: 658
	internal sealed class LongNormalizer : Normalizer
	{
		// Token: 0x06002243 RID: 8771 RVA: 0x0028B718 File Offset: 0x0028AB18
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

		// Token: 0x06002244 RID: 8772 RVA: 0x0028B778 File Offset: 0x0028AB78
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

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x0028B7D8 File Offset: 0x0028ABD8
		internal override int Size
		{
			get
			{
				return 8;
			}
		}
	}
}
