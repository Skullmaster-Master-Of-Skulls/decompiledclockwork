using System;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.Xml.Serialization
{
	// Token: 0x020001BB RID: 443
	public class XmlSerializerFactory
	{
		// Token: 0x06001EC0 RID: 7872 RVA: 0x000A8A8F File Offset: 0x000A6C8F
		public XmlSerializer CreateSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace)
		{
			return this.CreateSerializer(type, overrides, extraTypes, root, defaultNamespace, null);
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x000A8A9F File Offset: 0x000A6C9F
		public XmlSerializer CreateSerializer(Type type, XmlRootAttribute root)
		{
			return this.CreateSerializer(type, null, new Type[0], root, null, null);
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000A8AB2 File Offset: 0x000A6CB2
		public XmlSerializer CreateSerializer(Type type, Type[] extraTypes)
		{
			return this.CreateSerializer(type, null, extraTypes, null, null, null);
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000A8AC0 File Offset: 0x000A6CC0
		public XmlSerializer CreateSerializer(Type type, XmlAttributeOverrides overrides)
		{
			return this.CreateSerializer(type, overrides, new Type[0], null, null, null);
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000A8AD4 File Offset: 0x000A6CD4
		public XmlSerializer CreateSerializer(XmlTypeMapping xmlTypeMapping)
		{
			TempAssembly tempAssembly = XmlSerializer.GenerateTempAssembly(xmlTypeMapping);
			return (XmlSerializer)tempAssembly.Contract.TypedSerializers[xmlTypeMapping.Key];
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000A8B03 File Offset: 0x000A6D03
		public XmlSerializer CreateSerializer(Type type)
		{
			return this.CreateSerializer(type, null);
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000A8B10 File Offset: 0x000A6D10
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
				TempAssemblyCache obj = XmlSerializerFactory.cache;
				lock (obj)
				{
					tempAssembly = XmlSerializerFactory.cache[defaultNamespace, type];
					if (tempAssembly == null)
					{
						XmlSerializerImplementation contract;
						Assembly left = TempAssembly.LoadGeneratedAssembly(type, defaultNamespace, out contract);
						if (left == null)
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

		// Token: 0x06001EC7 RID: 7879 RVA: 0x000A8BE0 File Offset: 0x000A6DE0
		public XmlSerializer CreateSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location)
		{
			return this.CreateSerializer(type, overrides, extraTypes, root, defaultNamespace, location, null);
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x000A8BF4 File Offset: 0x000A6DF4
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateSerializer which does not take an Evidence parameter. See http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public XmlSerializer CreateSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location, Evidence evidence)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (location != null || evidence != null)
			{
				this.DemandForUserLocationOrEvidence();
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

		// Token: 0x06001EC9 RID: 7881 RVA: 0x000A8C78 File Offset: 0x000A6E78
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		private void DemandForUserLocationOrEvidence()
		{
		}

		// Token: 0x04000CE8 RID: 3304
		private static TempAssemblyCache cache = new TempAssemblyCache();
	}
}
