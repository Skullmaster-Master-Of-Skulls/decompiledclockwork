using System;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000055 RID: 85
	public interface IBinarySerialize
	{
		// Token: 0x06000466 RID: 1126
		void Read(BinaryReader r);

		// Token: 0x06000467 RID: 1127
		void Write(BinaryWriter w);
	}
}
