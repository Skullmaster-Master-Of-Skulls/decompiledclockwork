using System;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x020002C9 RID: 713
	internal class XmlNodeConverter : XmlBaseConverter
	{
		// Token: 0x06002A42 RID: 10818 RVA: 0x000DB41E File Offset: 0x000D961E
		protected XmlNodeConverter() : base(XmlTypeCode.Node)
		{
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000DB428 File Offset: 0x000D9628
		public override object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			Type type = value.GetType();
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.XPathNavigatorType && XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XPathNavigatorType))
			{
				return (XPathNavigator)value;
			}
			if (destinationType == XmlBaseConverter.XPathItemType && XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XPathNavigatorType))
			{
				return (XPathItem)value;
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x0400121A RID: 4634
		public static readonly XmlValueConverter Node = new XmlNodeConverter();
	}
}
