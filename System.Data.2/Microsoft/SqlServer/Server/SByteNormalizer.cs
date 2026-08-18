using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000061 RID: 97
	internal sealed class SByteNormalizer : Normalizer
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x00046CB0 File Offset: 0x000460B0
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			sbyte b = (sbyte)base.GetValue(fi, obj);
			byte b2 = (byte)b;
			if (!this.m_skipNormalize)
			{
				b2 ^= 128;
			}
			s.WriteByte(b2);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00046CE8 File Offset: 0x000460E8
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte b = (byte)s.ReadByte();
			if (!this.m_skipNormalize)
			{
				b ^= 128;
			}
			sbyte b2 = (sbyte)b;
			base.SetValue(fi, recvr, b2);
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00046D20 File Offset: 0x00046120
		internal override int Size
		{
			get
			{
				return 1;
			}
		}
	}
}
