using System;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000134 RID: 308
	internal sealed class XPathComparerHelper : IComparer
	{
		// Token: 0x060011DA RID: 4570 RVA: 0x0004E8F8 File Offset: 0x0004D8F8
		public XPathComparerHelper(XmlSortOrder order, XmlCaseOrder caseOrder, string lang, XmlDataType dataType)
		{
			if (lang == null)
			{
				this.cinfo = Thread.CurrentThread.CurrentCulture;
			}
			else
			{
				try
				{
					this.cinfo = new CultureInfo(lang);
				}
				catch (ArgumentException)
				{
					throw;
				}
			}
			if (order == XmlSortOrder.Descending)
			{
				if (caseOrder == XmlCaseOrder.LowerFirst)
				{
					caseOrder = XmlCaseOrder.UpperFirst;
				}
				else if (caseOrder == XmlCaseOrder.UpperFirst)
				{
					caseOrder = XmlCaseOrder.LowerFirst;
				}
			}
			this.order = order;
			this.caseOrder = caseOrder;
			this.dataType = dataType;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0004E96C File Offset: 0x0004D96C
		public int Compare(object x, object y)
		{
			int num = (this.order == XmlSortOrder.Ascending) ? 1 : -1;
			switch (this.dataType)
			{
			case XmlDataType.Text:
			{
				string strA = Convert.ToString(x, this.cinfo);
				string strB = Convert.ToString(y, this.cinfo);
				int num2 = string.Compare(strA, strB, this.caseOrder != XmlCaseOrder.None, this.cinfo);
				if (num2 != 0 || this.caseOrder == XmlCaseOrder.None)
				{
					return num * num2;
				}
				int num3 = (this.caseOrder == XmlCaseOrder.LowerFirst) ? 1 : -1;
				num2 = string.Compare(strA, strB, false, this.cinfo);
				return num3 * num2;
			}
			case XmlDataType.Number:
			{
				double num4 = XmlConvert.ToXPathDouble(x);
				double num5 = XmlConvert.ToXPathDouble(y);
				if (num4 > num5)
				{
					return num;
				}
				if (num4 < num5)
				{
					return -1 * num;
				}
				if (num4 == num5)
				{
					return 0;
				}
				if (!double.IsNaN(num4))
				{
					return num;
				}
				if (double.IsNaN(num5))
				{
					return 0;
				}
				return -1 * num;
			}
			default:
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
		}

		// Token: 0x04000B4F RID: 2895
		private XmlSortOrder order;

		// Token: 0x04000B50 RID: 2896
		private XmlCaseOrder caseOrder;

		// Token: 0x04000B51 RID: 2897
		private CultureInfo cinfo;

		// Token: 0x04000B52 RID: 2898
		private XmlDataType dataType;
	}
}
