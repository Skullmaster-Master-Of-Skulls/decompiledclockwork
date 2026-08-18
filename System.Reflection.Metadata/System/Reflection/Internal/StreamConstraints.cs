using System;

namespace System.Reflection.Internal
{
	// Token: 0x0200015A RID: 346
	internal struct StreamConstraints
	{
		// Token: 0x06000ACF RID: 2767 RVA: 0x0001EB9F File Offset: 0x0001CD9F
		public StreamConstraints(object guardOpt, long startPosition, int imageSize)
		{
			this.GuardOpt = guardOpt;
			this.ImageStart = startPosition;
			this.ImageSize = imageSize;
		}

		// Token: 0x040008FD RID: 2301
		public readonly object GuardOpt;

		// Token: 0x040008FE RID: 2302
		public readonly long ImageStart;

		// Token: 0x040008FF RID: 2303
		public readonly int ImageSize;
	}
}
