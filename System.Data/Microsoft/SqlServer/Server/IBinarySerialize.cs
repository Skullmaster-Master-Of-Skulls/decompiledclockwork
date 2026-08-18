using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200027E RID: 638
	public interface IBinarySerialize
	{
		// Token: 0x06002188 RID: 8584
		void Read(BinaryReader r);

		// Token: 0x06002189 RID: 8585
		void Write(BinaryWriter w);
	}
}
