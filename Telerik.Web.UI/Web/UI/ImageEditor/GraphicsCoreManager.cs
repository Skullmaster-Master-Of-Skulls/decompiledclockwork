using System;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EA5 RID: 3749
	public class GraphicsCoreManager
	{
		// Token: 0x06008F0D RID: 36621 RVA: 0x002036D8 File Offset: 0x002018D8
		public static IGraphicsCore GetGraphicsCoreByType(GraphicsCoreTypes type)
		{
			if (type == GraphicsCoreTypes.GDI)
			{
				return new GDIGraphicsCore();
			}
			throw new NotImplementedException();
		}

		// Token: 0x040027B6 RID: 10166
		internal static readonly IGraphicsCore Default = GraphicsCoreManager.GetGraphicsCoreByType(GraphicsCoreTypes.GDI);
	}
}
