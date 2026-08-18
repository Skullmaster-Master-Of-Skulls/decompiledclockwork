using System;

namespace System.Drawing.Drawing2D
{
	// Token: 0x020000BF RID: 191
	public sealed class GraphicsContainer : MarshalByRefObject
	{
		// Token: 0x06000A63 RID: 2659 RVA: 0x00025EE2 File Offset: 0x000240E2
		internal GraphicsContainer(int graphicsContainer)
		{
			this.nativeGraphicsContainer = graphicsContainer;
		}

		// Token: 0x04000990 RID: 2448
		internal int nativeGraphicsContainer;
	}
}
