using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WebGrease.Extensions;

namespace WebGrease.Configuration
{
	// Token: 0x02000026 RID: 38
	internal abstract class FileSetBase : IFileSet
	{
		// Token: 0x060002DD RID: 733 RVA: 0x00006EA0 File Offset: 0x000050A0
		protected FileSetBase()
		{
			this.ResourcePivots = new ResourcePivotGroupCollection();
			this.AutoNaming = new Dictionary<string, AutoNameConfig>(StringComparer.OrdinalIgnoreCase);
			this.InputSpecs = new List<InputSpec>();
			this.Bundling = new Dictionary<string, BundlingConfig>(StringComparer.OrdinalIgnoreCase);
			this.Preprocessing = new Dictionary<string, PreprocessingConfig>(StringComparer.OrdinalIgnoreCase);
			this.LoadedConfigurationFiles = new List<string>();
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00006F0F File Offset: 0x0000510F
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00006F17 File Offset: 0x00005117
		public ResourcePivotGroupCollection ResourcePivots { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00006F20 File Offset: 0x00005120
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00006F28 File Offset: 0x00005128
		public IList<string> LoadedConfigurationFiles { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00006F3E File Offset: 0x0000513E
		public IList<string> Locales
		{
			get
			{
				return this.ResourcePivots["locales"].NullSafeAction((ResourcePivotGroup l) => l.Keys.ToArray<string>()) ?? new string[0];
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00006F89 File Offset: 0x00005189
		public IList<string> Themes
		{
			get
			{
				return this.ResourcePivots["themes"].NullSafeAction((ResourcePivotGroup l) => l.Keys.ToArray<string>()) ?? new string[0];
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00006FC7 File Offset: 0x000051C7
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00006FCF File Offset: 0x000051CF
		public IDictionary<string, PreprocessingConfig> Preprocessing { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00006FD8 File Offset: 0x000051D8
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00006FE0 File Offset: 0x000051E0
		public IDictionary<string, BundlingConfig> Bundling { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00006FE9 File Offset: 0x000051E9
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00006FF1 File Offset: 0x000051F1
		public string Output { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00006FFA File Offset: 0x000051FA
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00007002 File Offset: 0x00005202
		public string OutputPathFormat { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000700B File Offset: 0x0000520B
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00007013 File Offset: 0x00005213
		public IList<InputSpec> InputSpecs { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000701C File Offset: 0x0000521C
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00007024 File Offset: 0x00005224
		public IDictionary<string, AutoNameConfig> AutoNaming { get; private set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000702D File Offset: 0x0000522D
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00007035 File Offset: 0x00005235
		internal GlobalConfig GlobalConfig { get; private set; }

		// Token: 0x060002F2 RID: 754 RVA: 0x00007058 File Offset: 0x00005258
		protected virtual void Load(IEnumerable<XElement> fileSetElements, string sourceDirectory)
		{
			foreach (XElement xelement in fileSetElements)
			{
				string text = xelement.Name.ToString();
				string value = xelement.Value;
				string key;
				switch (key = text)
				{
				case "OutputPathFormat":
					this.OutputPathFormat = value;
					break;
				case "Inputs":
					this.InputSpecs.AddInputSpecs(sourceDirectory, xelement);
					break;
				case "Preprocessing":
					this.Preprocessing.AddNamedConfig(new PreprocessingConfig(xelement));
					break;
				case "Bundling":
					this.Bundling.AddNamedConfig(new BundlingConfig(xelement));
					break;
				case "Autoname":
					this.AutoNaming.AddNamedConfig(new AutoNameConfig(xelement));
					break;
				case "Locales":
					if (!this.usingLocalResourcePivot.Contains("locales"))
					{
						this.usingLocalResourcePivot.Add("locales");
						this.ResourcePivots.Clear("locales");
					}
					this.ResourcePivots.Set("locales", new ResourcePivotApplyMode?(ResourcePivotApplyMode.ApplyAsStringReplace), value.NullSafeAction((string sv) => sv.SafeSplitSemiColonSeperatedValue()));
					break;
				case "Themes":
					if (!this.usingLocalResourcePivot.Contains("themes"))
					{
						this.usingLocalResourcePivot.Add("themes");
						this.ResourcePivots.Clear("themes");
					}
					this.ResourcePivots.Set("themes", new ResourcePivotApplyMode?(ResourcePivotApplyMode.ApplyAsStringReplace), value.NullSafeAction((string sv) => sv.SafeSplitSemiColonSeperatedValue()));
					break;
				case "ResourcePivot":
					this.ResourcePivots.Set((string)xelement.Attribute("key"), new ResourcePivotApplyMode?(((string)xelement.Attribute("applyMode")).TryParseToEnum(null) ?? ResourcePivotApplyMode.ApplyAsStringReplace), ((string)xelement).NullSafeAction((string sv) => sv.SafeSplitSemiColonSeperatedValue()));
					break;
				}
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00007370 File Offset: 0x00005570
		protected IEnumerable<XElement> Initialize(XElement fileSetElement, GlobalConfig globalConfig, string configurationFile)
		{
			XAttribute attribute = fileSetElement.Attribute("output");
			this.Output = (((string)attribute) ?? string.Empty);
			this.GlobalConfig = globalConfig;
			List<XElement> fileSetElements = fileSetElement.Descendants().ToList<XElement>();
			WebGreaseConfiguration.ForEachConfigSourceElement(fileSetElement, configurationFile, delegate(XElement element, string s)
			{
				this.LoadedConfigurationFiles.Add(s);
				fileSetElements.AddRange(element.Descendants());
			});
			return fileSetElements;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000073E4 File Offset: 0x000055E4
		protected void InitializeDefaults(ResourcePivotGroupCollection defaultResourcePivots, IDictionary<string, PreprocessingConfig> defaultPreprocessing, IDictionary<string, BundlingConfig> defaultBundling, string defaultOutputPathFormat)
		{
			if (!string.IsNullOrWhiteSpace(defaultOutputPathFormat))
			{
				this.OutputPathFormat = defaultOutputPathFormat;
			}
			if (defaultResourcePivots != null && defaultResourcePivots.Count<ResourcePivotGroup>() > 0)
			{
				foreach (ResourcePivotGroup resourcePivotGroup in defaultResourcePivots)
				{
					this.ResourcePivots.Set(resourcePivotGroup.Key, new ResourcePivotApplyMode?(resourcePivotGroup.ApplyMode), resourcePivotGroup.Keys);
				}
			}
			if (defaultPreprocessing != null && defaultPreprocessing.Count > 0)
			{
				foreach (string key in defaultPreprocessing.Keys)
				{
					this.Preprocessing[key] = defaultPreprocessing[key];
				}
			}
			if (defaultBundling != null && defaultBundling.Count > 0)
			{
				foreach (string key2 in defaultBundling.Keys)
				{
					this.Bundling[key2] = defaultBundling[key2];
				}
			}
		}

		// Token: 0x04000083 RID: 131
		private readonly IList<string> usingLocalResourcePivot = new List<string>();
	}
}
