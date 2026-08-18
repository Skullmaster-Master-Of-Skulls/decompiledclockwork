using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Web.Compilation;

namespace System.Web.Configuration
{
	// Token: 0x020006C4 RID: 1732
	public sealed class Compiler : ConfigurationElement
	{
		// Token: 0x060053AB RID: 21419 RVA: 0x001262F4 File Offset: 0x001244F4
		static Compiler()
		{
			Compiler._properties = new ConfigurationPropertyCollection();
			Compiler._properties.Add(Compiler._propLanguage);
			Compiler._properties.Add(Compiler._propExtension);
			Compiler._properties.Add(Compiler._propType);
			Compiler._properties.Add(Compiler._propWarningLevel);
			Compiler._properties.Add(Compiler._propCompilerOptions);
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x00117E9E File Offset: 0x0011609E
		internal Compiler()
		{
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x001263FC File Offset: 0x001245FC
		public Compiler(string compilerOptions, string extension, string language, string type, int warningLevel) : this()
		{
			base[Compiler._propCompilerOptions] = compilerOptions;
			base[Compiler._propExtension] = extension;
			base[Compiler._propLanguage] = language;
			base[Compiler._propType] = type;
			base[Compiler._propWarningLevel] = warningLevel;
		}

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x060053AE RID: 21422 RVA: 0x00126452 File Offset: 0x00124652
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Compiler._properties;
			}
		}

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x060053AF RID: 21423 RVA: 0x00126459 File Offset: 0x00124659
		[ConfigurationProperty("language", DefaultValue = "", IsRequired = true, IsKey = true)]
		public string Language
		{
			get
			{
				return (string)base[Compiler._propLanguage];
			}
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x060053B0 RID: 21424 RVA: 0x0012646B File Offset: 0x0012466B
		[ConfigurationProperty("extension", DefaultValue = "")]
		public string Extension
		{
			get
			{
				return (string)base[Compiler._propExtension];
			}
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x060053B1 RID: 21425 RVA: 0x0012647D File Offset: 0x0012467D
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		public string Type
		{
			get
			{
				return (string)base[Compiler._propType];
			}
		}

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x060053B2 RID: 21426 RVA: 0x00126490 File Offset: 0x00124690
		internal CompilerType CompilerTypeInternal
		{
			get
			{
				if (this._compilerType == null)
				{
					lock (this)
					{
						if (this._compilerType == null)
						{
							Type codeDomProviderType = CompilationUtil.LoadTypeWithChecks(this.Type, typeof(CodeDomProvider), null, this, "type");
							CompilerParameters compilerParameters = new CompilerParameters();
							compilerParameters.WarningLevel = this.WarningLevel;
							compilerParameters.TreatWarningsAsErrors = (this.WarningLevel > 0);
							string compilerOptions = this.CompilerOptions;
							CompilationUtil.CheckCompilerOptionsAllowed(compilerOptions, true, base.ElementInformation.Properties["compilerOptions"].Source, base.ElementInformation.Properties["compilerOptions"].LineNumber);
							compilerParameters.CompilerOptions = compilerOptions;
							this._compilerType = new CompilerType(codeDomProviderType, compilerParameters);
						}
					}
				}
				return this._compilerType;
			}
		}

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x060053B3 RID: 21427 RVA: 0x00126578 File Offset: 0x00124778
		[ConfigurationProperty("warningLevel", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0, MaxValue = 4)]
		public int WarningLevel
		{
			get
			{
				return (int)base[Compiler._propWarningLevel];
			}
		}

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x060053B4 RID: 21428 RVA: 0x0012658A File Offset: 0x0012478A
		[ConfigurationProperty("compilerOptions", DefaultValue = "")]
		public string CompilerOptions
		{
			get
			{
				return (string)base[Compiler._propCompilerOptions];
			}
		}

		// Token: 0x04002C0D RID: 11277
		private const string compilerOptionsAttribName = "compilerOptions";

		// Token: 0x04002C0E RID: 11278
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002C0F RID: 11279
		private static readonly ConfigurationProperty _propLanguage = new ConfigurationProperty("language", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002C10 RID: 11280
		private static readonly ConfigurationProperty _propExtension = new ConfigurationProperty("extension", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002C11 RID: 11281
		private static readonly ConfigurationProperty _propType = new ConfigurationProperty("type", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x04002C12 RID: 11282
		private static readonly ConfigurationProperty _propWarningLevel = new ConfigurationProperty("warningLevel", typeof(int), 0, null, new IntegerValidator(0, 4), ConfigurationPropertyOptions.None);

		// Token: 0x04002C13 RID: 11283
		private static readonly ConfigurationProperty _propCompilerOptions = new ConfigurationProperty("compilerOptions", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002C14 RID: 11284
		private CompilerType _compilerType;
	}
}
