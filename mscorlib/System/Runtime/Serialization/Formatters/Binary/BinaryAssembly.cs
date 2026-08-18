using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007D9 RID: 2009
	internal sealed class BinaryAssembly : IStreamable
	{
		// Token: 0x06004737 RID: 18231 RVA: 0x000F3CB5 File Offset: 0x000F2CB5
		internal BinaryAssembly()
		{
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x000F3CBD File Offset: 0x000F2CBD
		internal void Set(int assemId, string assemblyString)
		{
			this.assemId = assemId;
			this.assemblyString = assemblyString;
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x000F3CCD File Offset: 0x000F2CCD
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(12);
			sout.WriteInt32(this.assemId);
			sout.WriteString(this.assemblyString);
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x000F3CEF File Offset: 0x000F2CEF
		public void Read(__BinaryParser input)
		{
			this.assemId = input.ReadInt32();
			this.assemblyString = input.ReadString();
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x000F3D09 File Offset: 0x000F2D09
		public void Dump()
		{
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x000F3D0B File Offset: 0x000F2D0B
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040023F8 RID: 9208
		internal int assemId;

		// Token: 0x040023F9 RID: 9209
		internal string assemblyString;
	}
}
