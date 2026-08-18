using System;
using System.Xml.Linq;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D8C RID: 3468
	internal static class XmlaElementsExtensions
	{
		// Token: 0x0600810B RID: 33035 RVA: 0x001D7A88 File Offset: 0x001D5C88
		public static string ToXml(this IXmlaMethod method)
		{
			XElement xelement = XmlaSoapSerializer.Serialize(method);
			return xelement.ToString(SaveOptions.DisableFormatting);
		}
	}
}
