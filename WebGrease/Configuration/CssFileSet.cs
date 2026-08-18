using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WebGrease.Css.Extensions;
using WebGrease.Extensions;

namespace WebGrease.Configuration
{
	// Token: 0x020000F1 RID: 241
	internal sealed class CssFileSet : FileSetBase
	{
		// Token: 0x06000F66 RID: 3942 RVA: 0x00046F54 File Offset: 0x00045154
		internal CssFileSet()
		{
			this.Minification = new Dictionary<string, CssMinificationConfig>(StringComparer.OrdinalIgnoreCase);
			this.ImageSpriting = new Dictionary<string, CssSpritingConfig>(StringComparer.OrdinalIgnoreCase);
			this.Autonaming = new Dictionary<string, AutoNameConfig>(StringComparer.OrdinalIgnoreCase);
			this.Dpi = new HashSet<float>();
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00046FB0 File Offset: 0x000451B0
		internal CssFileSet(XElement cssFileSetElement, string sourceDirectory, IDictionary<string, CssMinificationConfig> defaultMinification, IDictionary<string, CssSpritingConfig> defaultSpriting, IDictionary<string, PreprocessingConfig> defaultPreprocessing, IDictionary<string, BundlingConfig> defaultBundling, ResourcePivotGroupCollection defaultResourcePivots, GlobalConfig globalConfig, string defaultOutputPathFormat, IDictionary<string, HashSet<float>> defaultDpi, string configurationFile) : this()
		{
			base.InitializeDefaults(defaultResourcePivots, defaultPreprocessing, defaultBundling, defaultOutputPathFormat);
			this.InitializeDefaults(defaultMinification, defaultSpriting, defaultDpi);
			IEnumerable<XElement> fileSetElements = base.Initialize(cssFileSetElement, globalConfig, configurationFile);
			this.Load(fileSetElements, sourceDirectory);
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x00046FF0 File Offset: 0x000451F0
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x00046FF8 File Offset: 0x000451F8
		public IDictionary<string, AutoNameConfig> Autonaming { get; private set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00047001 File Offset: 0x00045201
		// (set) Token: 0x06000F6B RID: 3947 RVA: 0x00047009 File Offset: 0x00045209
		internal IDictionary<string, CssMinificationConfig> Minification { get; private set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x00047012 File Offset: 0x00045212
		// (set) Token: 0x06000F6D RID: 3949 RVA: 0x0004701A File Offset: 0x0004521A
		internal HashSet<float> Dpi { get; private set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x00047023 File Offset: 0x00045223
		// (set) Token: 0x06000F6F RID: 3951 RVA: 0x0004702B File Offset: 0x0004522B
		internal IDictionary<string, CssSpritingConfig> ImageSpriting { get; private set; }

		// Token: 0x06000F70 RID: 3952 RVA: 0x00047034 File Offset: 0x00045234
		public override string ToString()
		{
			return "[CssFileSet:{0}]".InvariantFormat(new object[]
			{
				base.Output
			});
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00047098 File Offset: 0x00045298
		protected override void Load(IEnumerable<XElement> fileSetElements, string sourceDirectory)
		{
			base.Load(fileSetElements, sourceDirectory);
			foreach (XElement xelement in fileSetElements)
			{
				string text = xelement.Name.ToString();
				string obj = (string)xelement;
				string a;
				if ((a = text) != null)
				{
					if (!(a == "Dpi"))
					{
						if (!(a == "Minification"))
						{
							if (!(a == "Spriting"))
							{
								if (a == "Autoname")
								{
									this.Autonaming.AddNamedConfig(new AutoNameConfig(xelement));
								}
							}
							else
							{
								this.ImageSpriting.AddNamedConfig(new CssSpritingConfig(xelement));
							}
						}
						else
						{
							this.Minification.AddNamedConfig(new CssMinificationConfig(xelement));
						}
					}
					else
					{
						if (!this.localDpiUsed)
						{
							this.localDpiUsed = true;
							this.allDpi.Clear();
						}
						IEnumerable<float> collection = from d in obj.NullSafeAction(new Func<string, IEnumerable<string>>(StringExtensions.SafeSplitSemiColonSeperatedValue))
						select d.TryParseFloat() into d
						where d != null
						select d.Value;
						string value = (string)xelement.Attribute("output");
						this.allDpi[value.AsNullIfWhiteSpace() ?? string.Empty] = new HashSet<float>(collection);
					}
				}
			}
			string key = this.allDpi.Keys.FirstOrDefault((string k) => !k.IsNullOrWhitespace() && base.Output.IndexOf(k, StringComparison.OrdinalIgnoreCase) != -1) ?? string.Empty;
			HashSet<float> hashSet;
			if (!this.allDpi.TryGetValue(key, out hashSet))
			{
				this.allDpi.TryGetValue(string.Empty, out hashSet);
			}
			HashSet<float> dpi;
			if ((dpi = hashSet) == null)
			{
				dpi = new HashSet<float>
				{
					1f
				};
			}
			this.Dpi = dpi;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x000472E8 File Offset: 0x000454E8
		private void InitializeDefaults(IDictionary<string, CssMinificationConfig> defaultMinification, IDictionary<string, CssSpritingConfig> defaultSpriting, IDictionary<string, HashSet<float>> defaultDpi)
		{
			if (defaultDpi != null && defaultDpi.Any<KeyValuePair<string, HashSet<float>>>())
			{
				defaultDpi.ForEach(delegate(KeyValuePair<string, HashSet<float>> dd)
				{
					this.allDpi[dd.Key] = dd.Value;
				});
			}
			if (defaultMinification != null && defaultMinification.Count > 0)
			{
				foreach (string key in defaultMinification.Keys)
				{
					this.Minification[key] = defaultMinification[key];
				}
			}
			if (defaultSpriting != null && defaultSpriting.Count > 0)
			{
				foreach (string key2 in defaultSpriting.Keys)
				{
					this.ImageSpriting[key2] = defaultSpriting[key2];
				}
			}
		}

		// Token: 0x040005F6 RID: 1526
		private bool localDpiUsed;

		// Token: 0x040005F7 RID: 1527
		private IDictionary<string, HashSet<float>> allDpi = new Dictionary<string, HashSet<float>>();
	}
}
