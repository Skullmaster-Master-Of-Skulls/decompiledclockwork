using System;
using System.Globalization;
using System.Xml.Linq;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D81 RID: 3457
	internal class XmlaXmlHelper
	{
		// Token: 0x060080E9 RID: 33001 RVA: 0x001D7910 File Offset: 0x001D5B10
		public static string TryGetElementValue(XElement parent, string elementName)
		{
			XElement xelement = parent.Element(XmlaXmlHelper.GetRowsetXName(elementName));
			if (xelement != null)
			{
				return xelement.Value;
			}
			return null;
		}

		// Token: 0x060080EA RID: 33002 RVA: 0x001D7938 File Offset: 0x001D5B38
		public static int TryGetElementValueAsInt(XElement parent, string elementName)
		{
			XElement xelement = parent.Element(XmlaXmlHelper.GetRowsetXName(elementName));
			if (xelement != null)
			{
				return int.Parse(xelement.Value, CultureInfo.InvariantCulture);
			}
			return 0;
		}

		// Token: 0x060080EB RID: 33003 RVA: 0x001D7967 File Offset: 0x001D5B67
		private static XName GetRowsetXName(string name)
		{
			return XName.Get(name, "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x04002388 RID: 9096
		public const string MdDatasetNamespace = "urn:schemas-microsoft-com:xml-analysis:mddataset";

		// Token: 0x04002389 RID: 9097
		public const string RowsetNamespace = "urn:schemas-microsoft-com:xml-analysis:rowset";

		// Token: 0x0400238A RID: 9098
		public const string SoapNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
	}
}
