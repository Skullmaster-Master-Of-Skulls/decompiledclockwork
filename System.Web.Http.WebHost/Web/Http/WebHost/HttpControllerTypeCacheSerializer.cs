using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http.WebHost.Properties;
using System.Xml;

namespace System.Web.Http.WebHost
{
	// Token: 0x02000015 RID: 21
	internal sealed class HttpControllerTypeCacheSerializer
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000038F0 File Offset: 0x00001AF0
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000391A File Offset: 0x00001B1A
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003922 File Offset: 0x00001B22
		internal DateTime? CurrentDateOverride { get; set; }

		// Token: 0x0600008A RID: 138 RVA: 0x0000392C File Offset: 0x00001B2C
		public ICollection<Type> DeserializeTypes(TextReader input)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(input);
			XmlElement documentElement = xmlDocument.DocumentElement;
			Guid a = new Guid(documentElement.Attributes["mvcVersionId"].Value);
			if (a != HttpControllerTypeCacheSerializer._mvcVersionId)
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

		// Token: 0x0600008B RID: 139 RVA: 0x00003B2C File Offset: 0x00001D2C
		public void SerializeTypes(IEnumerable<Type> types, TextWriter output)
		{
			IEnumerable<IGrouping<Assembly, IGrouping<Module, Type>>> enumerable = from type in types
			group type by type.Module into groupedByModule
			group groupedByModule by groupedByModule.Key.Assembly;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(xmlDocument.CreateComment(SRResources.TypeCache_DoNotModify));
			XmlElement xmlElement = xmlDocument.CreateElement("typeCache");
			xmlDocument.AppendChild(xmlElement);
			xmlElement.SetAttribute("lastModified", this.CurrentDate.ToString());
			xmlElement.SetAttribute("mvcVersionId", HttpControllerTypeCacheSerializer._mvcVersionId.ToString());
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

		// Token: 0x04000020 RID: 32
		private static readonly Guid _mvcVersionId = typeof(HttpControllerTypeCacheSerializer).Module.ModuleVersionId;
	}
}
