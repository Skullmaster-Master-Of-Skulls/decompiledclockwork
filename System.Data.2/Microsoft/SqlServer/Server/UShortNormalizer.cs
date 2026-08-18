using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000064 RID: 100
	internal sealed class UShortNormalizer : Normalizer
	{
		// Token: 0x0600050C RID: 1292 RVA: 0x00046E74 File Offset: 0x00046274
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((ushort)base.GetValue(fi, obj));
			if (!this.m_skipNormalize)
			{
				Array.Reverse(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00046EB0 File Offset: 0x000462B0
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00046EF4 File Offset: 0x000462F4
		internal override int Size
		{
			get
			{
				return 2;
			}
		}
	}
}
