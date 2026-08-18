using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007E6 RID: 2022
	internal sealed class MemberReference : IStreamable
	{
		// Token: 0x06004785 RID: 18309 RVA: 0x000F5394 File Offset: 0x000F4394
		internal MemberReference()
		{
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x000F539C File Offset: 0x000F439C
		internal void Set(int idRef)
		{
			this.idRef = idRef;
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x000F53A5 File Offset: 0x000F43A5
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(9);
			sout.WriteInt32(this.idRef);
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x000F53BB File Offset: 0x000F43BB
		public void Read(__BinaryParser input)
		{
			this.idRef = input.ReadInt32();
		}

		// Token: 0x06004789 RID: 18313 RVA: 0x000F53C9 File Offset: 0x000F43C9
		public void Dump()
		{
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x000F53CB File Offset: 0x000F43CB
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002438 RID: 9272
		internal int idRef;
	}
}
