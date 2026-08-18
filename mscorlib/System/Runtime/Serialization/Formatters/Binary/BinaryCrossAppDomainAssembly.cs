using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007DA RID: 2010
	internal sealed class BinaryCrossAppDomainAssembly : IStreamable
	{
		// Token: 0x0600473D RID: 18237 RVA: 0x000F3D18 File Offset: 0x000F2D18
		internal BinaryCrossAppDomainAssembly()
		{
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x000F3D20 File Offset: 0x000F2D20
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(20);
			sout.WriteInt32(this.assemId);
			sout.WriteInt32(this.assemblyIndex);
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x000F3D42 File Offset: 0x000F2D42
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyIndex = input.ReadInt32();
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x000F3D5C File Offset: 0x000F2D5C
		public void Dump()
		{
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x000F3D5E File Offset: 0x000F2D5E
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040023FA RID: 9210
		internal int assemId;

		// Token: 0x040023FB RID: 9211
		internal int assemblyIndex;
	}
}
