using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200028C RID: 652
	internal sealed class SByteNormalizer : Normalizer
	{
		// Token: 0x0600222B RID: 8747 RVA: 0x0028B278 File Offset: 0x0028A678
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

		// Token: 0x0600222C RID: 8748 RVA: 0x0028B2B8 File Offset: 0x0028A6B8
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

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x0028B2F8 File Offset: 0x0028A6F8
		internal override int Size
		{
			get
			{
				return 1;
			}
		}
	}
}
