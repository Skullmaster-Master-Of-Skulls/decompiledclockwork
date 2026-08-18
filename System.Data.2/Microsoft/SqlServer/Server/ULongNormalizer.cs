using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000068 RID: 104
	internal sealed class ULongNormalizer : Normalizer
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x00047144 File Offset: 0x00046544
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((ulong)base.GetValue(fi, obj));
			if (!this.m_skipNormalize)
			{
				Array.Reverse(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00047180 File Offset: 0x00046580
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

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000471C4 File Offset: 0x000465C4
		internal override int Size
		{
			get
			{
				return 8;
			}
		}
	}
}
