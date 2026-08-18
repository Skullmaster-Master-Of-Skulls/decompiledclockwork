using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc.Properties;
using System.Xml;

namespace System.Web.Mvc
{
	// Token: 0x0200011B RID: 283
	internal sealed class TypeCacheSerializer
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00013FCC File Offset: 0x000121CC
		private DateTime CurrentDate
		{
			get
			{
				DateTime? currentDateOverride = this.CurrentDateOverride;
				if (currentDateOverride == null)
				{
					return DateTime.Now;
				}
				return currentDateOverride.GetValueOrDefault();
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00013FF6 File Offset: 0x000121F6
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x00013FFE File Offset: 0x000121FE
		internal DateTime? CurrentDateOverride { get; set; }

		// Token: 0x0600076B RID: 1899 RVA: 0x00014008 File Offset: 0x00012208
		public List<Type> DeserializeTypes(TextReader input)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(input);
			XmlElement documentElement = xmlDocument.DocumentElement;
			Guid a = new Guid(documentElement.Attributes["mvcVersionId"].Value);
			if (a != TypeCacheSerializer._mvcVersionId)
			{
				return null;
			}
			List<Type> list = new List<Type>();
			foreach (object obj in documentElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string value = xmlNode.Attributes["name"].Value;
				Assembly assembly = Assembly.Load(value);
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					Guid b = new Guid(xmlNode2.Attributes["versionId"].Value);
					foreach (object obj3 in xmlNode2.ChildNodes)
					{
						XmlNode xmlNode3 = (XmlNode)obj3;
						string innerText = xmlNode3.InnerText;
						Type type = assembly.GetType(innerText);
						if (type == null || type.Module.ModuleVersionId != b)
						{
							return null;
						}
						list.Add(type);
					}
				}
			}
			return list;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00014208 File Offset: 0x00012408
		public void SerializeTypes(IEnumerable<Type> types, TextWriter output)
		{
			IEnumerable<IGrouping<Assembly, IGrouping<Module, Type>>> enumerable = from type in types
			group type by type.Module into groupedByModule
			group groupedByModule by groupedByModule.Key.Assembly;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(xmlDocument.CreateComment(MvcResources.TypeCache_DoNotModify));
			XmlElement xmlElement = xmlDocument.CreateElement("typeCache");
			xmlDocument.AppendChild(xmlElement);
			xmlElement.SetAttribute("lastModified", this.CurrentDate.ToString());
			xmlElement.SetAttribute("mvcVersionId", TypeCacheSerializer._mvcVersionId.ToString());
			foreach (IGrouping<Assembly, IGrouping<Module, Type>> grouping in enumerable)
			{
				XmlElement xmlElement2 = xmlDocument.CreateElement("assembly");
				xmlElement.AppendChild(xmlElement2);
				xmlElement2.SetAttribute("name", grouping.Key.FullName);
				foreach (IGrouping<Module, Type> grouping2 in grouping)
				{
					XmlElement xmlElement3 = xmlDocument.CreateElement("module");
					xmlElement2.AppendChild(xmlElement3);
					xmlElement3.SetAttribute("versionId", grouping2.Key.ModuleVersionId.ToString());
					foreach (Type type2 in grouping2)
					{
						XmlElement xmlElement4 = xmlDocument.CreateElement("type");
						xmlElement3.AppendChild(xmlElement4);
						xmlElement4.AppendChild(xmlDocument.CreateTextNode(type2.FullName));
					}
				}
			}
			xmlDocument.Save(output);
		}

		// Token: 0x0400020F RID: 527
		private static readonly Guid _mvcVersionId = typeof(TypeCacheSerializer).Module.ModuleVersionId;
	}
}
