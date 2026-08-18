using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000062 RID: 98
	internal sealed class ByteNormalizer : Normalizer
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x00046D44 File Offset: 0x00046144
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte value = (byte)base.GetValue(fi, obj);
			s.WriteByte(value);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00046D68 File Offset: 0x00046168
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte b = (byte)s.ReadByte();
			base.SetValue(fi, recvr, b);
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00046D8C File Offset: 0x0004618C
		internal override int Size
		{
			get
			{
				return 1;
			}
		}
	}
}
