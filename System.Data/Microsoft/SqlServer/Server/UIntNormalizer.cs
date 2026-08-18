using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000291 RID: 657
	internal sealed class UIntNormalizer : Normalizer
	{
		// Token: 0x0600223F RID: 8767 RVA: 0x0028B658 File Offset: 0x0028AA58
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((uint)base.GetValue(fi, obj));
			if (!this.m_skipNormalize)
			{
				Array.Reverse(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x0028B698 File Offset: 0x0028AA98
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[4];
			s.Read(array, 0, array.Length);
			if (!this.m_skipNormalize)
			{
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToUInt32(array, 0));
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x0028B6E8 File Offset: 0x0028AAE8
		internal override int Size
		{
			get
			{
				return 4;
			}
		}
	}
}
