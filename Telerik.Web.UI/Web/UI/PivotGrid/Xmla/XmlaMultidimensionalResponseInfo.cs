using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D7F RID: 3455
	internal class XmlaMultidimensionalResponseInfo
	{
		// Token: 0x060080D7 RID: 32983 RVA: 0x001D7650 File Offset: 0x001D5850
		public XmlaMultidimensionalResponseInfo(string responseString)
		{
			this.rootElement = XElement.Parse(responseString);
		}

		// Token: 0x170028E0 RID: 10464
		// (get) Token: 0x060080D8 RID: 32984 RVA: 0x001D767C File Offset: 0x001D587C
		public IEnumerable<XElement> ColumnTupleElements
		{
			get
			{
				if (this.columnTupleElements == null)
				{
					XElement xelement = (from de in this.rootElement.Descendants(XName.Get("Axis", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
					where de.FirstAttribute.Value == "Axis0"
					select de).FirstOrDefault<XElement>();
					if (xelement != null)
					{
						this.columnTupleElements = xelement.Descendants(XName.Get("Tuple", "urn:schemas-microsoft-com:xml-analysis:mddataset")).ToList<XElement>();
					}
					else
					{
						this.columnTupleElements = new List<XElement>();
					}
				}
				return this.columnTupleElements;
			}
		}

		// Token: 0x170028E1 RID: 10465
		// (get) Token: 0x060080D9 RID: 32985 RVA: 0x001D7720 File Offset: 0x001D5920
		public IEnumerable<XElement> RowTupleElements
		{
			get
			{
				if (this.rowTupleElements == null)
				{
					XElement xelement = (from de in this.rootElement.Descendants(XName.Get("Axis", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
					where de.FirstAttribute.Value == "Axis1"
					select de).FirstOrDefault<XElement>();
					if (xelement != null)
					{
						this.rowTupleElements = xelement.Descendants(XName.Get("Tuple", "urn:schemas-microsoft-com:xml-analysis:mddataset")).ToList<XElement>();
					}
					else
					{
						this.rowTupleElements = new List<XElement>();
					}
				}
				return this.rowTupleElements;
			}
		}

		// Token: 0x170028E2 RID: 10466
		// (get) Token: 0x060080DA RID: 32986 RVA: 0x001D77AD File Offset: 0x001D59AD
		public IEnumerable<XElement> DataCellElements
		{
			get
			{
				if (this.dataCells == null)
				{
					this.dataCells = this.rootElement.Descendants(XName.Get("Cell", "urn:schemas-microsoft-com:xml-analysis:mddataset")).ToList<XElement>();
				}
				return this.dataCells;
			}
		}

		// Token: 0x0400237B RID: 9083
		private XElement rootElement;

		// Token: 0x0400237C RID: 9084
		private IList<XElement> columnTupleElements;

		// Token: 0x0400237D RID: 9085
		private IList<XElement> rowTupleElements;

		// Token: 0x0400237E RID: 9086
		private IList<XElement> dataCells;
	}
}
