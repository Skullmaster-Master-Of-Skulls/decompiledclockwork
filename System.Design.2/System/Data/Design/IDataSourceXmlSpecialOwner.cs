using System;
using System.Xml;

namespace System.Data.Design
{
	// Token: 0x0200024B RID: 587
	internal interface IDataSourceXmlSpecialOwner
	{
		// Token: 0x060016AC RID: 5804
		void ReadSpecialItem(string propertyName, XmlNode xmlNode, DataSourceXmlSerializer serializer);

		// Token: 0x060016AD RID: 5805
		void WriteSpecialItem(string propertyName, XmlWriter writer, DataSourceXmlSerializer serializer);
	}
}
