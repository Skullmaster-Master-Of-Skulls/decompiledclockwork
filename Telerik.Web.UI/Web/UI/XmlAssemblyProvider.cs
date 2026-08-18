using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Telerik.Web.UI
{
	// Token: 0x0200085F RID: 2143
	public class XmlAssemblyProvider : AssemblyProviderBase
	{
		// Token: 0x170019C5 RID: 6597
		// (get) Token: 0x06004ED8 RID: 20184 RVA: 0x000F7334 File Offset: 0x000F5534
		protected string FilePath
		{
			get
			{
				return this._dataFileName;
			}
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x000F733C File Offset: 0x000F553C
		public XmlAssemblyProvider()
		{
			this._dataFileName = this._defaultDataFilePath;
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x000F736A File Offset: 0x000F556A
		public XmlAssemblyProvider(string filePath)
		{
			this._dataFileName = filePath;
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x000F7394 File Offset: 0x000F5594
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "AppDataAssemblyProvider";
			}
			base.Initialize(name, config);
			this._dataFileName = HttpContext.Current.Server.MapPath(config["fileName"]);
			if (string.IsNullOrEmpty(this._dataFileName))
			{
				this._dataFileName = this._defaultDataFilePath;
			}
			if (!File.Exists(this._dataFileName))
			{
				throw new ProviderException(string.Format("Missing XML data file - {0}. Please specify it with the fileName property.", this._dataFileName));
			}
		}

		// Token: 0x06004EDC RID: 20188 RVA: 0x000F7424 File Offset: 0x000F5624
		public override Collection<AssemblyReference> GetAssembliesList()
		{
			XElement[] array = this.ReadFile(this.FilePath);
			Collection<AssemblyReference> collection = new Collection<AssemblyReference>();
			foreach (XElement xelement in array)
			{
				collection.Add(new AssemblyReference(xelement.Value));
			}
			return collection;
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x000F7470 File Offset: 0x000F5670
		protected virtual XElement[] ReadFile(string filePath)
		{
			XElement[] result;
			try
			{
				XDocument xdocument = XDocument.Load(filePath);
				IEnumerable<XElement> source = xdocument.Root.Elements("Assembly");
				result = source.ToArray<XElement>();
			}
			catch (Exception innerException)
			{
				throw new InvalidDataException(string.Format("Failed to read {0}. Please verify that the file contains <assemblies> element with <assembly name='full assembly name'> as child nodes", filePath), innerException);
			}
			return result;
		}

		// Token: 0x0400139C RID: 5020
		private string _dataFileName;

		// Token: 0x0400139D RID: 5021
		private readonly string _defaultDataFilePath = HttpContext.Current.Server.MapPath("~/App_Data/WhiteList.xml");
	}
}
