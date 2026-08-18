using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005AC RID: 1452
	internal static class DataContractSerializerDefaults
	{
		// Token: 0x060038A5 RID: 14501 RVA: 0x000DA5B4 File Offset: 0x000D87B4
		internal static DataContractSerializer CreateSerializer(Type type, int maxItems)
		{
			return DataContractSerializerDefaults.CreateSerializer(type, null, maxItems);
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000DA5BE File Offset: 0x000D87BE
		internal static DataContractSerializer CreateSerializer(Type type, IList<Type> knownTypes, int maxItems)
		{
			return new DataContractSerializer(type, knownTypes, maxItems, false, false, null);
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000DA5CB File Offset: 0x000D87CB
		internal static DataContractSerializer CreateSerializer(Type type, string rootName, string rootNs, int maxItems)
		{
			return DataContractSerializerDefaults.CreateSerializer(type, null, rootName, rootNs, maxItems);
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x000DA5D7 File Offset: 0x000D87D7
		internal static DataContractSerializer CreateSerializer(Type type, IList<Type> knownTypes, string rootName, string rootNs, int maxItems)
		{
			return new DataContractSerializer(type, rootName, rootNs, knownTypes, maxItems, false, false, null);
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000DA5E7 File Offset: 0x000D87E7
		internal static DataContractSerializer CreateSerializer(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNs, int maxItems)
		{
			return DataContractSerializerDefaults.CreateSerializer(type, null, rootName, rootNs, maxItems);
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000DA5F3 File Offset: 0x000D87F3
		internal static DataContractSerializer CreateSerializer(Type type, IList<Type> knownTypes, XmlDictionaryString rootName, XmlDictionaryString rootNs, int maxItems)
		{
			return new DataContractSerializer(type, rootName, rootNs, knownTypes, maxItems, false, false, null);
		}

		// Token: 0x040029A7 RID: 10663
		internal const bool IgnoreExtensionDataObject = false;

		// Token: 0x040029A8 RID: 10664
		internal const int MaxItemsInObjectGraph = 2147483647;
	}
}
