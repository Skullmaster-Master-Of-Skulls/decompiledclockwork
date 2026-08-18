using System;
using System.Drawing;

namespace System.Resources
{
	// Token: 0x020000EC RID: 236
	internal class DataNodeInfo
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000A028 File Offset: 0x00008228
		internal DataNodeInfo Clone()
		{
			return new DataNodeInfo
			{
				Name = this.Name,
				Comment = this.Comment,
				TypeName = this.TypeName,
				MimeType = this.MimeType,
				ValueData = this.ValueData,
				ReaderPosition = new Point(this.ReaderPosition.X, this.ReaderPosition.Y)
			};
		}

		// Token: 0x040003C6 RID: 966
		internal string Name;

		// Token: 0x040003C7 RID: 967
		internal string Comment;

		// Token: 0x040003C8 RID: 968
		internal string TypeName;

		// Token: 0x040003C9 RID: 969
		internal string MimeType;

		// Token: 0x040003CA RID: 970
		internal string ValueData;

		// Token: 0x040003CB RID: 971
		internal Point ReaderPosition;
	}
}
