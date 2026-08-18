using System;
using System.Linq;
using System.Xml.Linq;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D78 RID: 3448
	internal class XmlaFaultReader
	{
		// Token: 0x060080BB RID: 32955 RVA: 0x001D6F7C File Offset: 0x001D517C
		public XmlaFaultReader(string xmlaResponseString)
		{
			this.rootElement = XElement.Parse(xmlaResponseString);
			XElement xelement = this.rootElement.Descendants(XmlaFaultReader.GetXName("Fault")).FirstOrDefault<XElement>();
			if (xelement != null)
			{
				this.faultString = xelement.ToString();
			}
		}

		// Token: 0x170028D8 RID: 10456
		// (get) Token: 0x060080BC RID: 32956 RVA: 0x001D6FC5 File Offset: 0x001D51C5
		public string FaultString
		{
			get
			{
				return this.faultString;
			}
		}

		// Token: 0x060080BD RID: 32957 RVA: 0x001D6FCD File Offset: 0x001D51CD
		private static XName GetXName(string name)
		{
			return XName.Get(name, "http://schemas.xmlsoap.org/soap/envelope/");
		}

		// Token: 0x04002368 RID: 9064
		private XElement rootElement;

		// Token: 0x04002369 RID: 9065
		private string faultString;
	}
}
