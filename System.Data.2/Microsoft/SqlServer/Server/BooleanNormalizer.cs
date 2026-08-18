using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000060 RID: 96
	internal sealed class BooleanNormalizer : Normalizer
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x00046C38 File Offset: 0x00046038
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			s.WriteByte(((bool)base.GetValue(fi, obj)) ? 1 : 0);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00046C64 File Offset: 0x00046064
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte b = (byte)s.ReadByte();
			base.SetValue(fi, recvr, b == 1);
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00046C8C File Offset: 0x0004608C
		internal override int Size
		{
			get
			{
				return 1;
			}
		}
	}
}
