using System;

namespace System.Reflection.Internal
{
	// Token: 0x02000081 RID: 129
	internal struct StreamConstraints
	{
		// Token: 0x06000330 RID: 816 RVA: 0x00007EEA File Offset: 0x000060EA
		public StreamConstraints(object guardOpt, long startPosition, int imageSize)
		{
			this.GuardOpt = guardOpt;
			this.ImageStart = startPosition;
			this.ImageSize = imageSize;
		}

		// Token: 0x04000485 RID: 1157
		public readonly object GuardOpt;

		// Token: 0x04000486 RID: 1158
		public readonly long ImageStart;

		// Token: 0x04000487 RID: 1159
		public readonly int ImageSize;
	}
}
