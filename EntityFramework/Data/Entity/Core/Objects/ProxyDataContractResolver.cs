using System;
using System.Data.Entity.Utilities;
using System.Runtime.Serialization;
using System.Xml;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005B7 RID: 1463
	public class ProxyDataContractResolver : DataContractResolver
	{
		// Token: 0x06003A9A RID: 15002 RVA: 0x00116B40 File Offset: 0x00114D40
		public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
		{
			Check.NotEmpty(typeName, "typeName");
			Check.NotEmpty(typeNamespace, "typeNamespace");
			Check.NotNull<Type>(declaredType, "declaredType");
			Check.NotNull<DataContractResolver>(knownTypeResolver, "knownTypeResolver");
			return knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, null);
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x00116B80 File Offset: 0x00114D80
		public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
		{
			Check.NotNull<Type>(type, "type");
			Check.NotNull<Type>(declaredType, "declaredType");
			Check.NotNull<DataContractResolver>(knownTypeResolver, "knownTypeResolver");
			Type objectType = ObjectContext.GetObjectType(type);
			if (objectType != type)
			{
				XmlQualifiedName schemaTypeName = this._exporter.GetSchemaTypeName(objectType);
				XmlDictionary dictionary = new XmlDictionary(2);
				typeName = new XmlDictionaryString(dictionary, schemaTypeName.Name, 0);
				typeNamespace = new XmlDictionaryString(dictionary, schemaTypeName.Namespace, 1);
				return true;
			}
			return knownTypeResolver.TryResolveType(type, declaredType, null, out typeName, out typeNamespace);
		}

		// Token: 0x04001634 RID: 5684
		private readonly XsdDataContractExporter _exporter = new XsdDataContractExporter();
	}
}
