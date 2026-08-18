using System;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200000E RID: 14
	internal class ContractTypeNameCollection : NonNullItemCollection<XmlQualifiedName>
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00003875 File Offset: 0x00001A75
		protected override void InsertItem(int index, XmlQualifiedName item)
		{
			if (item != null && item.Name == string.Empty)
			{
				throw FxTrace.Exception.Argument("item", SR.DiscoveryArgumentEmptyContractTypeName);
			}
			base.InsertItem(index, item);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000038AF File Offset: 0x00001AAF
		protected override void SetItem(int index, XmlQualifiedName item)
		{
			if (item != null && item.Name == string.Empty)
			{
				throw FxTrace.Exception.Argument("item", SR.DiscoveryArgumentEmptyContractTypeName);
			}
			base.SetItem(index, item);
		}
	}
}
