using System;
using System.IO;

namespace log4net.ObjectRenderer
{
	// Token: 0x020000B6 RID: 182
	public interface IObjectRenderer
	{
		// Token: 0x0600052F RID: 1327
		void RenderObject(RendererMap rendererMap, object obj, TextWriter writer);
	}
}
