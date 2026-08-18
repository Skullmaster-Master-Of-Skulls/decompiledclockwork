using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020002CB RID: 715
	internal class XmlAnyListConverter : XmlListConverter
	{
		// Token: 0x06002A5C RID: 10844 RVA: 0x000DC01D File Offset: 0x000DA21D
		protected XmlAnyListConverter(XmlBaseConverter atomicConverter) : base(atomicConverter)
		{
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x000DC028 File Offset: 0x000DA228
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
			if (!(value is IEnumerable) || value.GetType() == XmlBaseConverter.StringType || value.GetType() == XmlBaseConverter.ByteArrayType)
			{
				value = new object[]
				{
					value
				};
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x0400121D RID: 4637
		public static readonly XmlValueConverter ItemList = new XmlAnyListConverter((XmlBaseConverter)XmlAnyConverter.Item);

		// Token: 0x0400121E RID: 4638
		public static readonly XmlValueConverter AnyAtomicList = new XmlAnyListConverter((XmlBaseConverter)XmlAnyConverter.AnyAtomic);
	}
}
