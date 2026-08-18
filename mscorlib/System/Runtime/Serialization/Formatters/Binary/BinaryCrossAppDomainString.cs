using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007DF RID: 2015
	internal sealed class BinaryCrossAppDomainString : IStreamable
	{
		// Token: 0x0600475D RID: 18269 RVA: 0x000F4A55 File Offset: 0x000F3A55
		internal BinaryCrossAppDomainString()
		{
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x000F4A5D File Offset: 0x000F3A5D
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(19);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.value);
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x000F4A7F File Offset: 0x000F3A7F
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.value = input.ReadInt32();
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x000F4A99 File Offset: 0x000F3A99
		public void Dump()
		{
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x000F4A9B File Offset: 0x000F3A9B
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002419 RID: 9241
		internal int objectId;

		// Token: 0x0400241A RID: 9242
		internal int value;
	}
}
