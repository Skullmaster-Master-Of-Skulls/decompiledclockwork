using System;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000013 RID: 19
	internal sealed class XPathComparerHelper : IComparer
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000030C0 File Offset: 0x000012C0
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

		// Token: 0x0600007A RID: 122 RVA: 0x00003134 File Offset: 0x00001334
		public int Compare(object x, object y)
		{
			XmlDataType xmlDataType = this.dataType;
			if (xmlDataType != XmlDataType.Text)
			{
				if (xmlDataType != XmlDataType.Number)
				{
					throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
				}
				double num = XmlConvert.ToXPathDouble(x);
				double value = XmlConvert.ToXPathDouble(y);
				int num2 = num.CompareTo(value);
				if (this.order != XmlSortOrder.Ascending)
				{
					return -num2;
				}
				return num2;
			}
			else
			{
				string strA = Convert.ToString(x, this.cinfo);
				string strB = Convert.ToString(y, this.cinfo);
				int num2 = string.Compare(strA, strB, this.caseOrder > XmlCaseOrder.None, this.cinfo);
				if (num2 != 0 || this.caseOrder == XmlCaseOrder.None)
				{
					if (this.order != XmlSortOrder.Ascending)
					{
						return -num2;
					}
					return num2;
				}
				else
				{
					num2 = string.Compare(strA, strB, false, this.cinfo);
					if (this.caseOrder != XmlCaseOrder.LowerFirst)
					{
						return -num2;
					}
					return num2;
				}
			}
		}

		// Token: 0x04000074 RID: 116
		private XmlSortOrder order;

		// Token: 0x04000075 RID: 117
		private XmlCaseOrder caseOrder;

		// Token: 0x04000076 RID: 118
		private CultureInfo cinfo;

		// Token: 0x04000077 RID: 119
		private XmlDataType dataType;
	}
}
