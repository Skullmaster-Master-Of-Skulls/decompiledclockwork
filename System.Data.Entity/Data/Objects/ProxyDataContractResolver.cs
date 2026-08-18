using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.Data.Objects
{
	// Token: 0x02000139 RID: 313
	public class ProxyDataContractResolver : DataContractResolver
	{
		// Token: 0x060016B5 RID: 5813 RVA: 0x0004C2F5 File Offset: 0x0004A4F5
		public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
		{
			EntityUtil.CheckStringArgument(typeName, "typeName");
			EntityUtil.CheckStringArgument(typeNamespace, "typeNamespace");
			EntityUtil.CheckArgumentNull<Type>(declaredType, "declaredType");
			EntityUtil.CheckArgumentNull<DataContractResolver>(knownTypeResolver, "knownTypeResolver");
			return knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, null);
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0004C334 File Offset: 0x0004A534
		public override bool TryResolveType(Type dataContractType, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
		{
			EntityUtil.CheckArgumentNull<Type>(dataContractType, "dataContractType");
			EntityUtil.CheckArgumentNull<Type>(declaredType, "declaredType");
			EntityUtil.CheckArgumentNull<DataContractResolver>(knownTypeResolver, "knownTypeResolver");
			Type objectType = ObjectContext.GetObjectType(dataContractType);
			if (objectType != dataContractType)
			{
				XmlQualifiedName schemaTypeName = this._exporter.GetSchemaTypeName(objectType);
				XmlDictionary dictionary = new XmlDictionary(2);
				typeName = new XmlDictionaryString(dictionary, schemaTypeName.Name, 0);
				typeNamespace = new XmlDictionaryString(dictionary, schemaTypeName.Namespace, 1);
				return true;
			}
			return knownTypeResolver.TryResolveType(dataContractType, declaredType, null, out typeName, out typeNamespace);
		}

		// Token: 0x04000A60 RID: 2656
		private XsdDataContractExporter _exporter = new XsdDataContractExporter();
	}
}
