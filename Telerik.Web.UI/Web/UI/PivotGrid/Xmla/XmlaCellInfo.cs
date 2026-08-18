using System;
using System.Globalization;
using System.Xml.Linq;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D75 RID: 3445
	internal class XmlaCellInfo : IOlapCell
	{
		// Token: 0x170028CD RID: 10445
		// (get) Token: 0x0600809F RID: 32927 RVA: 0x001D6BBE File Offset: 0x001D4DBE
		// (set) Token: 0x060080A0 RID: 32928 RVA: 0x001D6BC6 File Offset: 0x001D4DC6
		public string FormattedValue { get; private set; }

		// Token: 0x170028CE RID: 10446
		// (get) Token: 0x060080A1 RID: 32929 RVA: 0x001D6BCF File Offset: 0x001D4DCF
		// (set) Token: 0x060080A2 RID: 32930 RVA: 0x001D6BD7 File Offset: 0x001D4DD7
		public object Value { get; private set; }

		// Token: 0x170028CF RID: 10447
		// (get) Token: 0x060080A3 RID: 32931 RVA: 0x001D6BE0 File Offset: 0x001D4DE0
		// (set) Token: 0x060080A4 RID: 32932 RVA: 0x001D6BE8 File Offset: 0x001D4DE8
		public int Ordinal { get; private set; }

		// Token: 0x060080A5 RID: 32933 RVA: 0x001D6BF4 File Offset: 0x001D4DF4
		public static XmlaCellInfo FromXElement(XElement cellElement)
		{
			XmlaCellInfo xmlaCellInfo = new XmlaCellInfo();
			xmlaCellInfo.Ordinal = int.Parse(cellElement.Attribute(XName.Get("CellOrdinal")).Value, CultureInfo.InvariantCulture);
			xmlaCellInfo.FormattedValue = cellElement.Element(XName.Get("FmtValue", "urn:schemas-microsoft-com:xml-analysis:mddataset")).Value;
			XElement xelement = cellElement.Element(XName.Get("Value", "urn:schemas-microsoft-com:xml-analysis:mddataset"));
			if (xelement != null)
			{
				double num = 0.0;
				if (double.TryParse(xelement.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
				{
					xmlaCellInfo.Value = num;
				}
				else
				{
					xmlaCellInfo.Value = xelement.Value;
				}
			}
			return xmlaCellInfo;
		}

		// Token: 0x060080A6 RID: 32934 RVA: 0x001D6CA4 File Offset: 0x001D4EA4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Ordinal: {0} | Value: {1}", new object[]
			{
				this.Ordinal,
				this.Value
			});
		}
	}
}
