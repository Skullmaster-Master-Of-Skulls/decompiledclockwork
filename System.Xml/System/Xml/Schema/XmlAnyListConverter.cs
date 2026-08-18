using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000297 RID: 663
	internal class XmlAnyListConverter : XmlListConverter
	{
		// Token: 0x06001FAB RID: 8107 RVA: 0x0008F95F File Offset: 0x0008E95F
		protected XmlAnyListConverter(XmlBaseConverter atomicConverter) : base(atomicConverter)
		{
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x0008F968 File Offset: 0x0008E968
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

		// Token: 0x040012AC RID: 4780
		public static readonly XmlValueConverter ItemList = new XmlAnyListConverter((XmlBaseConverter)XmlAnyConverter.Item);

		// Token: 0x040012AD RID: 4781
		public static readonly XmlValueConverter AnyAtomicList = new XmlAnyListConverter((XmlBaseConverter)XmlAnyConverter.AnyAtomic);
	}
}
