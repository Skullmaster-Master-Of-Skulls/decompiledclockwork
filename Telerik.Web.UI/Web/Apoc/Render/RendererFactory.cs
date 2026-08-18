using System;
using System.IO;
using Telerik.Web.Apoc.Render.Pdf;
using Telerik.Web.Apoc.Render.Xml;

namespace Telerik.Web.Apoc.Render
{
	// Token: 0x020016A2 RID: 5794
	internal abstract class RendererFactory
	{
		// Token: 0x0600DFC3 RID: 57283 RVA: 0x0031C678 File Offset: 0x0031A878
		internal static IRenderer Make(RendererEngine renderer, Stream stream)
		{
			IRenderer result = null;
			if (renderer == RendererEngine.PDF)
			{
				result = new PdfRenderer(stream);
			}
			else if (renderer == RendererEngine.XML)
			{
				result = new XMLRenderer(stream);
			}
			return result;
		}
	}
}
