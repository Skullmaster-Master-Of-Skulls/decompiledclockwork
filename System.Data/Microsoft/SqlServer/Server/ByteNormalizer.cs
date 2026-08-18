using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200028D RID: 653
	internal sealed class ByteNormalizer : Normalizer
	{
		// Token: 0x0600222F RID: 8751 RVA: 0x0028B328 File Offset: 0x0028A728
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte value = (byte)base.GetValue(fi, obj);
			s.WriteByte(value);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0028B358 File Offset: 0x0028A758
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte b = (byte)s.ReadByte();
			base.SetValue(fi, recvr, b);
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x0028B388 File Offset: 0x0028A788
		internal override int Size
		{
			get
			{
				return 1;
			}
		}
	}
}
