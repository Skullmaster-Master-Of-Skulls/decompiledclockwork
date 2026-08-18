using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Schema;

namespace System.Web.Optimization
{
	// Token: 0x0200000E RID: 14
	public sealed class BundleManifest
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000037B5 File Offset: 0x000019B5
		private BundleManifest()
		{
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000037BD File Offset: 0x000019BD
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000037C5 File Offset: 0x000019C5
		public IList<BundleDefinition> StyleBundles { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000037CE File Offset: 0x000019CE
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000037D6 File Offset: 0x000019D6
		public IList<BundleDefinition> ScriptBundles { get; private set; }

		// Token: 0x0600008A RID: 138 RVA: 0x000037E0 File Offset: 0x000019E0
		public static BundleManifest ReadBundleManifest(Stream bundleStream)
		{
			XmlDocument xmlDocument = BundleManifest.GetXmlDocument(bundleStream);
			return new BundleManifest
			{
				StyleBundles = xmlDocument.SelectNodes("bundles/styleBundle").Cast<XmlElement>().Select(new Func<XmlElement, BundleDefinition>(BundleManifest.ReadBundle)).ToList<BundleDefinition>(),
				ScriptBundles = xmlDocument.SelectNodes("bundles/scriptBundle").Cast<XmlElement>().Select(new Func<XmlElement, BundleDefinition>(BundleManifest.ReadBundle)).ToList<BundleDefinition>()
			};
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003853 File Offset: 0x00001A53
		public static string BundleManifestPath
		{
			get
			{
				return "~/bundle.config";
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000385A File Offset: 0x00001A5A
		public static BundleManifest ReadBundleManifest()
		{
			return BundleManifest.ReadBundleManifest(BundleTable.VirtualPathProvider);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003868 File Offset: 0x00001A68
		internal static BundleManifest ReadBundleManifest(VirtualPathProvider vpp)
		{
			if (vpp == null)
			{
				return null;
			}
			if (!vpp.FileExists(BundleManifest.BundleManifestPath))
			{
				return null;
			}
			VirtualFile file = vpp.GetFile(BundleManifest.BundleManifestPath);
			BundleManifest result;
			using (Stream stream = file.Open())
			{
				result = BundleManifest.ReadBundleManifest(stream);
			}
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000038D8 File Offset: 0x00001AD8
		private static XmlDocument GetXmlDocument(Stream bundleStream)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (Stream manifestResourceStream = typeof(BundleManifest).Assembly.GetManifestResourceStream("System.Web.Optimization.BundleManifestSchema.xsd"))
			{
				using (XmlReader xmlReader = XmlReader.Create(manifestResourceStream))
				{
					xmlDocument.Schemas.Add(null, xmlReader);
				}
			}
			xmlDocument.Load(bundleStream);
			xmlDocument.Validate(delegate(object sender, ValidationEventArgs e)
			{
				if (e.Severity == XmlSeverityType.Error)
				{
					throw new InvalidOperationException(e.Message);
				}
			});
			return xmlDocument;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000398C File Offset: 0x00001B8C
		private static BundleDefinition ReadBundle(XmlElement element)
		{
			BundleDefinition bundleDefinition = new BundleDefinition();
			bundleDefinition.Path = element.GetAttribute("path");
			bundleDefinition.CdnPath = element.GetAttribute("cdnPath");
			bundleDefinition.CdnFallbackExpression = element.GetAttribute("cdnFallbackExpression");
			bundleDefinition.Includes = (from XmlElement s in element.GetElementsByTagName("include")
			select s.GetAttribute("path")).ToList<string>();
			return bundleDefinition;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003A10 File Offset: 0x00001C10
		internal void Register(BundleCollection collection)
		{
			foreach (BundleDefinition bundleDefinition in this.StyleBundles)
			{
				StyleBundle styleBundle = new StyleBundle(bundleDefinition.Path);
				styleBundle.Include(bundleDefinition.Includes.ToArray<string>());
				collection.Add(styleBundle);
			}
			foreach (BundleDefinition bundleDefinition2 in this.ScriptBundles)
			{
				ScriptBundle scriptBundle = new ScriptBundle(bundleDefinition2.Path);
				scriptBundle.Include(bundleDefinition2.Includes.ToArray<string>());
				collection.Add(scriptBundle);
			}
		}

		// Token: 0x0400002A RID: 42
		private const string XsdResourceName = "System.Web.Optimization.BundleManifestSchema.xsd";

		// Token: 0x0400002B RID: 43
		private const string DefaultBundlePath = "~/bundle.config";
	}
}
