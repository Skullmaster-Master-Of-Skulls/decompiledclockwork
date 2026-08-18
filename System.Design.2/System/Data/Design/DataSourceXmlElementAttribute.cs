using System;

namespace System.Data.Design
{
	// Token: 0x02000229 RID: 553
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class DataSourceXmlElementAttribute : DataSourceXmlSerializationAttribute
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x000760AD File Offset: 0x000742AD
		internal DataSourceXmlElementAttribute() : this(null)
		{
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0007607E File Offset: 0x0007427E
		internal DataSourceXmlElementAttribute(string elementName)
		{
			base.Name = elementName;
		}
	}
}
