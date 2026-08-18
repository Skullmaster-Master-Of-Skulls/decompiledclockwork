using System;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015E6 RID: 5606
	internal class DisplaySpace : Space
	{
		// Token: 0x0600DA76 RID: 55926 RVA: 0x002FD9E7 File Offset: 0x002FBBE7
		public DisplaySpace(int size)
		{
			this.size = size;
		}

		// Token: 0x0600DA77 RID: 55927 RVA: 0x002FD9F6 File Offset: 0x002FBBF6
		public int getSize()
		{
			return this.size;
		}

		// Token: 0x0600DA78 RID: 55928 RVA: 0x002FD9FE File Offset: 0x002FBBFE
		public override void render(IRenderer renderer)
		{
			renderer.RenderDisplaySpace(this);
		}

		// Token: 0x04003CCE RID: 15566
		private int size;
	}
}
