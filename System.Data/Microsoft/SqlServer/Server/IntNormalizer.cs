using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000290 RID: 656
	internal sealed class IntNormalizer : Normalizer
	{
		// Token: 0x0600223B RID: 8763 RVA: 0x0028B568 File Offset: 0x0028A968
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

		// Token: 0x0600223C RID: 8764 RVA: 0x0028B5C8 File Offset: 0x0028A9C8
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

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x0028B628 File Offset: 0x0028AA28
		internal override int Size
		{
			get
			{
				return 4;
			}
		}
	}
}
