using System;

namespace System.Drawing.Drawing2D
{
	// Token: 0x020000C2 RID: 194
	public sealed class GraphicsState : MarshalByRefObject
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x00027C10 File Offset: 0x00025E10
		internal GraphicsState(int nativeState)
		{
			this.nativeState = nativeState;
		}

		// Token: 0x04000993 RID: 2451
		internal int nativeState;
	}
}
