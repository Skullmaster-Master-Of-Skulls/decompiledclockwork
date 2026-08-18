using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000066 RID: 102
	internal sealed class UIntNormalizer : Normalizer
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x00046FDC File Offset: 0x000463DC
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((uint)base.GetValue(fi, obj));
			if (!this.m_skipNormalize)
			{
				Array.Reverse(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00047018 File Offset: 0x00046418
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0004705C File Offset: 0x0004645C
		internal override int Size
		{
			get
			{
				return 4;
			}
		}
	}
}
