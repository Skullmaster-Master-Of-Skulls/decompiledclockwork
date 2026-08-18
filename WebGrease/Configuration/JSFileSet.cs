using System;
using System.Collections.Generic;
using System.Xml.Linq;
using WebGrease.Extensions;

namespace WebGrease.Configuration
{
	// Token: 0x020000F5 RID: 245
	internal sealed class JSFileSet : FileSetBase
	{
		// Token: 0x06000FB7 RID: 4023 RVA: 0x00047C86 File Offset: 0x00045E86
		internal JSFileSet()
		{
			this.Minification = new Dictionary<string, JsMinificationConfig>(StringComparer.OrdinalIgnoreCase);
			this.Validation = new Dictionary<string, JSValidationConfig>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00047CB0 File Offset: 0x00045EB0
		public override string ToString()
		{
			return "[JsFileSet:{0}]".InvariantFormat(new object[]
			{
				base.Output
			});
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00047CD8 File Offset: 0x00045ED8
		internal JSFileSet(XElement jsFileSetElement, string sourceDirectory, IDictionary<string, JsMinificationConfig> defaultMinification, IDictionary<string, PreprocessingConfig> defaultPreProcessing, IDictionary<string, BundlingConfig> defaultBundling, ResourcePivotGroupCollection defaultResourcePivots, GlobalConfig globalConfig, string defaultOutputPathFormat, string configurationFile) : this()
		{
			base.InitializeDefaults(defaultResourcePivots, defaultPreProcessing, defaultBundling, defaultOutputPathFormat);
			this.InitializeDefaults(defaultMinification);
			IEnumerable<XElement> fileSetElements = base.Initialize(jsFileSetElement, globalConfig, configurationFile);
			this.Load(fileSetElements, sourceDirectory);
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00047D14 File Offset: 0x00045F14
		// (set) Token: 0x06000FBB RID: 4027 RVA: 0x00047D1C File Offset: 0x00045F1C
		internal IDictionary<string, JSValidationConfig> Validation { get; private set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00047D25 File Offset: 0x00045F25
		// (set) Token: 0x06000FBD RID: 4029 RVA: 0x00047D2D File Offset: 0x00045F2D
		internal IDictionary<string, JsMinificationConfig> Minification { get; private set; }

		// Token: 0x06000FBE RID: 4030 RVA: 0x00047D38 File Offset: 0x00045F38
		protected override void Load(IEnumerable<XElement> fileSetElements, string sourceDirectory)
		{
			base.Load(fileSetElements, sourceDirectory);
			foreach (XElement xelement in fileSetElements)
			{
				string text = xelement.Name.ToString();
				string a;
				if ((a = text) != null)
				{
					if (!(a == "Minification"))
					{
						if (a == "Validation")
						{
							this.Validation.AddNamedConfig(new JSValidationConfig(xelement));
						}
					}
					else
					{
						this.Minification.AddNamedConfig(new JsMinificationConfig(xelement));
					}
				}
			}
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00047DD4 File Offset: 0x00045FD4
		private void InitializeDefaults(IDictionary<string, JsMinificationConfig> defaultMinification)
		{
			if (defaultMinification != null && defaultMinification.Count > 0)
			{
				foreach (string key in defaultMinification.Keys)
				{
					this.Minification[key] = defaultMinification[key];
				}
			}
		}
	}
}
