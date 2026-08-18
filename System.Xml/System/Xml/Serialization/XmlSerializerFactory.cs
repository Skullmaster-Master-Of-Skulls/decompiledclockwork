using System;
using System.Security.Policy;

namespace System.Xml.Serialization
{
	// Token: 0x0200033A RID: 826
	public class XmlSerializerFactory
	{
		// Token: 0x06002877 RID: 10359 RVA: 0x000D187C File Offset: 0x000D087C
		public XmlSerializer CreateSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace)
		{
			return this.CreateSerializer(type, overrides, extraTypes, root, defaultNamespace, null, null);
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000D188D File Offset: 0x000D088D
		public XmlSerializer CreateSerializer(Type type, XmlRootAttribute root)
		{
			return this.CreateSerializer(type, null, new Type[0], root, null, null, null);
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000D18A1 File Offset: 0x000D08A1
		public XmlSerializer CreateSerializer(Type type, Type[] extraTypes)
		{
			return this.CreateSerializer(type, null, extraTypes, null, null, null, null);
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000D18B0 File Offset: 0x000D08B0
		public XmlSerializer CreateSerializer(Type type, XmlAttributeOverrides overrides)
		{
			return this.CreateSerializer(type, overrides, new Type[0], null, null, null, null);
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000D18C4 File Offset: 0x000D08C4
		public XmlSerializer CreateSerializer(XmlTypeMapping xmlTypeMapping)
		{
			TempAssembly tempAssembly = XmlSerializer.GenerateTempAssembly(xmlTypeMapping);
			return (XmlSerializer)tempAssembly.Contract.TypedSerializers[xmlTypeMapping.Key];
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000D18F3 File Offset: 0x000D08F3
		public XmlSerializer CreateSerializer(Type type)
		{
			return this.CreateSerializer(type, null);
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000D1900 File Offset: 0x000D0900
		public XmlSerializer CreateSerializer(Type type, string defaultNamespace)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			TempAssembly tempAssembly = XmlSerializerFactory.cache[defaultNamespace, type];
			XmlTypeMapping xmlTypeMapping = null;
			if (tempAssembly == null)
			{
				lock (XmlSerializerFactory.cache)
				{
					tempAssembly = XmlSerializerFactory.cache[defaultNamespace, type];
					if (tempAssembly == null)
					{
						XmlSerializerImplementation contract;
						if (TempAssembly.LoadGeneratedAssembly(type, defaultNamespace, out contract) == null)
						{
							XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter(defaultNamespace);
							xmlTypeMapping = xmlReflectionImporter.ImportTypeMapping(type, null, defaultNamespace);
							tempAssembly = XmlSerializer.GenerateTempAssembly(xmlTypeMapping, type, defaultNamespace);
						}
						else
						{
							tempAssembly = new TempAssembly(contract);
						}
						XmlSerializerFactory.cache.Add(defaultNamespace, type, tempAssembly);
					}
				}
			}
			if (xmlTypeMapping == null)
			{
				xmlTypeMapping = XmlReflectionImporter.GetTopLevelMapping(type, defaultNamespace);
			}
			return tempAssembly.Contract.GetSerializer(type);
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000D19BC File Offset: 0x000D09BC
		public XmlSerializer CreateSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location, Evidence evidence)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter(overrides, defaultNamespace);
			for (int i = 0; i < extraTypes.Length; i++)
			{
				xmlReflectionImporter.IncludeType(extraTypes[i]);
			}
			XmlTypeMapping xmlTypeMapping = xmlReflectionImporter.ImportTypeMapping(type, root, defaultNamespace);
			TempAssembly tempAssembly = XmlSerializer.GenerateTempAssembly(xmlTypeMapping, type, defaultNamespace, location, evidence);
			return (XmlSerializer)tempAssembly.Contract.TypedSerializers[xmlTypeMapping.Key];
		}

		// Token: 0x04001681 RID: 5761
		private static TempAssemblyCache cache = new TempAssemblyCache();
	}
}
