using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200028F RID: 655
	internal sealed class UShortNormalizer : Normalizer
	{
		// Token: 0x06002237 RID: 8759 RVA: 0x0028B4A8 File Offset: 0x0028A8A8
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((ushort)base.GetValue(fi, obj));
			if (!this.m_skipNormalize)
			{
				Array.Reverse(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0028B4E8 File Offset: 0x0028A8E8
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[2];
			s.Read(array, 0, array.Length);
			if (!this.m_skipNormalize)
			{
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToUInt16(array, 0));
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x0028B538 File Offset: 0x0028A938
		internal override int Size
		{
			get
			{
				return 2;
			}
		}
	}
}
