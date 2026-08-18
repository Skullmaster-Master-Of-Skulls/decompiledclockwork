using System;

namespace System.Data.Design
{
	// Token: 0x02000227 RID: 551
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class DataSourceXmlAttributeAttribute : DataSourceXmlSerializationAttribute
	{
		// Token: 0x0600148B RID: 5259 RVA: 0x00076075 File Offset: 0x00074275
		internal DataSourceXmlAttributeAttribute() : this(null)
		{
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0007607E File Offset: 0x0007427E
		internal DataSourceXmlAttributeAttribute(string attributeName)
		{
			base.Name = attributeName;
		}
	}
}
