using System;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015CF RID: 5583
	internal abstract class Box
	{
		// Token: 0x0600D991 RID: 55697
		public abstract void render(IRenderer renderer);

		// Token: 0x04003C2C RID: 15404
		protected internal Area parent;

		// Token: 0x04003C2D RID: 15405
		protected internal AreaTree areaTree;
	}
}
