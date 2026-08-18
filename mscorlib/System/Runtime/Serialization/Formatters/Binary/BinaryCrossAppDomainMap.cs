using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007E0 RID: 2016
	internal sealed class BinaryCrossAppDomainMap : IStreamable
	{
		// Token: 0x06004762 RID: 18274 RVA: 0x000F4AA8 File Offset: 0x000F3AA8
		internal BinaryCrossAppDomainMap()
		{
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x000F4AB0 File Offset: 0x000F3AB0
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(18);
			sout.WriteInt32(this.crossAppDomainArrayIndex);
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x000F4AC6 File Offset: 0x000F3AC6
		public void Read(__BinaryParser input)
		{
			this.crossAppDomainArrayIndex = input.ReadInt32();
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x000F4AD4 File Offset: 0x000F3AD4
		public void Dump()
		{
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x000F4AD6 File Offset: 0x000F3AD6
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x0400241B RID: 9243
		internal int crossAppDomainArrayIndex;
	}
}
