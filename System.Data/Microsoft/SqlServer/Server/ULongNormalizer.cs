using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000293 RID: 659
	internal sealed class ULongNormalizer : Normalizer
	{
		// Token: 0x06002247 RID: 8775 RVA: 0x0028B808 File Offset: 0x0028AC08
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((ulong)base.GetValue(fi, obj));
			if (!this.m_skipNormalize)
			{
				Array.Reverse(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0028B848 File Offset: 0x0028AC48
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[8];
			s.Read(array, 0, array.Length);
			if (!this.m_skipNormalize)
			{
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToUInt64(array, 0));
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x0028B898 File Offset: 0x0028AC98
		internal override int Size
		{
			get
			{
				return 8;
			}
		}
	}
}
