using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Serialization;

namespace AjaxControlToolkit.Bundling
{
	// Token: 0x02000059 RID: 89
	public class BundleResolver
	{
		// Token: 0x06000315 RID: 789 RVA: 0x00009E94 File Offset: 0x00008094
		public BundleResolver(ICache cache)
		{
			this._cache = cache;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00009EB4 File Offset: 0x000080B4
		public virtual List<Type> GetControlTypesInBundles(string[] bundles, string fileName)
		{
			List<Type> list = new List<Type>();
			List<string> list2 = new List<string>();
			if (!File.Exists(fileName))
			{
				if (bundles != null && bundles.Length > 0)
				{
					throw new Exception("Can not resolve requested control bundle since AjaxControlToolkit.config file is not defined.");
				}
				list.AddRange(ControlDependencyMap.Maps.SelectMany((KeyValuePair<string, ControlDependencyMap> m) => m.Value.Dependecies));
			}
			else
			{
				Settings settings = this.ParseConfiguration(this.ReadConfiguration(fileName));
				if (settings.ControlBundleSections != null && settings.ControlBundleSections.Length > 0)
				{
					foreach (ControlBundleSection controlBundleSection in settings.ControlBundleSections)
					{
						if (controlBundleSection != null && controlBundleSection.ControlBundles != null && controlBundleSection.ControlBundles.Length > 0)
						{
							foreach (ControlBundle controlBundle in controlBundleSection.ControlBundles)
							{
								if (controlBundle.Controls != null && controlBundle.Controls.Length > 0 && ((string.IsNullOrEmpty(controlBundle.Name) && (bundles == null || bundles.Length == 0)) || (bundles != null && bundles.Contains(controlBundle.Name))))
								{
									foreach (Control control in controlBundle.Controls)
									{
										if (string.IsNullOrEmpty(control.Assembly) || control.Assembly == "AjaxControlToolkit")
										{
											string key = "AjaxControlToolkit." + control.Name;
											if (!ControlDependencyMap.Maps.ContainsKey(key))
											{
												throw new Exception(string.Format("Could not find control '{0}'. Please make sure you entered the correct control name in AjaxControlToolkit.config file.", control.Name));
											}
											list.AddRange(ControlDependencyMap.Maps[key].Dependecies);
										}
										else
										{
											list.Add(BundleResolver.GetAssembly(control.Assembly).GetType(control.Assembly + "." + control.Name));
										}
									}
									list2.Add(controlBundle.Name);
								}
							}
						}
					}
				}
				if (bundles != null)
				{
					foreach (string text in bundles)
					{
						if (!list2.Contains(text))
						{
							throw new Exception(string.Format("Could not resolve bundle {0}.", text));
						}
					}
				}
			}
			return list.Distinct<Type>().ToList<Type>();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000A118 File Offset: 0x00008318
		private string ReadConfiguration(string fileName)
		{
			string text = this._cache.Get<string>("e3e5a62a67434f0aa62901759726f470");
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			text = File.ReadAllText(fileName);
			this._cache.Set("e3e5a62a67434f0aa62901759726f470", text, fileName);
			return text;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000A15C File Offset: 0x0000835C
		private Settings ParseConfiguration(string text)
		{
			Settings result;
			using (StringReader stringReader = new StringReader(text))
			{
				result = (new XmlSerializer(typeof(Settings)).Deserialize(stringReader) as Settings);
			}
			return result;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A1A8 File Offset: 0x000083A8
		private static Assembly GetAssembly(string name)
		{
			if (!BundleResolver.LoadedAssemblies.ContainsKey(name))
			{
				BundleResolver.LoadedAssemblies.Add(name, Assembly.Load(name));
			}
			return BundleResolver.LoadedAssemblies[name];
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000A430 File Offset: 0x00008630
		public IEnumerable<string> GetControlBundles()
		{
			string fileName = Path.Combine(HttpRuntime.AppDomainAppPath, "AjaxControlToolkit.config");
			if (File.Exists(fileName))
			{
				Settings settings = this.ParseConfiguration(this.ReadConfiguration(fileName));
				foreach (ControlBundleSection section in settings.ControlBundleSections)
				{
					foreach (ControlBundle bundle in section.ControlBundles)
					{
						yield return bundle.Name;
					}
				}
			}
			yield break;
		}

		// Token: 0x04000106 RID: 262
		public const string ConfigFileVirtualPath = "AjaxControlToolkit.config";

		// Token: 0x04000107 RID: 263
		private const string ConfigCacheKey = "e3e5a62a67434f0aa62901759726f470";

		// Token: 0x04000108 RID: 264
		private readonly ICache _cache;

		// Token: 0x04000109 RID: 265
		private static readonly Dictionary<string, Assembly> LoadedAssemblies = new Dictionary<string, Assembly>();
	}
}
