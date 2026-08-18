using System;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x02000295 RID: 661
	internal class XmlNodeConverter : XmlBaseConverter
	{
		// Token: 0x06001F91 RID: 8081 RVA: 0x0008EF14 File Offset: 0x0008DF14
		protected XmlNodeConverter() : base(XmlTypeCode.Node)
		{
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0008EF20 File Offset: 0x0008DF20
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

		// Token: 0x040012A9 RID: 4777
		public static readonly XmlValueConverter Node = new XmlNodeConverter();
	}
}
