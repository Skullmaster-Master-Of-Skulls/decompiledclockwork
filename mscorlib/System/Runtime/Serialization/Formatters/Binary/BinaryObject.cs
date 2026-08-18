using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007DB RID: 2011
	internal sealed class BinaryObject : IStreamable
	{
		// Token: 0x06004742 RID: 18242 RVA: 0x000F3D6B File Offset: 0x000F2D6B
		internal BinaryObject()
		{
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x000F3D73 File Offset: 0x000F2D73
		internal void Set(int objectId, int mapId)
		{
			this.objectId = objectId;
			this.mapId = mapId;
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x000F3D83 File Offset: 0x000F2D83
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(1);
			sout.WriteInt32(this.objectId);
			sout.WriteInt32(this.mapId);
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x000F3DA4 File Offset: 0x000F2DA4
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.mapId = input.ReadInt32();
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x000F3DBE File Offset: 0x000F2DBE
		public void Dump()
		{
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x000F3DC0 File Offset: 0x000F2DC0
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040023FC RID: 9212
		internal int objectId;

		// Token: 0x040023FD RID: 9213
		internal int mapId;
	}
}
