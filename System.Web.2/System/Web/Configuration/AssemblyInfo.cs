using System;
using System.Configuration;
using System.Reflection;

namespace System.Web.Configuration
{
	// Token: 0x0200069B RID: 1691
	public sealed class AssemblyInfo : ConfigurationElement
	{
		// Token: 0x0600515B RID: 20827 RVA: 0x00117E58 File Offset: 0x00116058
		internal void SetCompilationReference(CompilationSection compSection)
		{
			this._compilationSection = compSection;
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x00117E61 File Offset: 0x00116061
		static AssemblyInfo()
		{
			AssemblyInfo._properties = new ConfigurationPropertyCollection();
			AssemblyInfo._properties.Add(AssemblyInfo._propAssembly);
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x00117E9E File Offset: 0x0011609E
		internal AssemblyInfo()
		{
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x00117EA6 File Offset: 0x001160A6
		public AssemblyInfo(string assemblyName)
		{
			this.Assembly = assemblyName;
		}

		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x0600515F RID: 20831 RVA: 0x00117EB5 File Offset: 0x001160B5
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AssemblyInfo._properties;
			}
		}

		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x06005160 RID: 20832 RVA: 0x00117EBC File Offset: 0x001160BC
		// (set) Token: 0x06005161 RID: 20833 RVA: 0x00117ECE File Offset: 0x001160CE
		[ConfigurationProperty("assembly", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Assembly
		{
			get
			{
				return (string)base[AssemblyInfo._propAssembly];
			}
			set
			{
				base[AssemblyInfo._propAssembly] = value;
			}
		}

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x06005162 RID: 20834 RVA: 0x00117EDC File Offset: 0x001160DC
		// (set) Token: 0x06005163 RID: 20835 RVA: 0x00117EFE File Offset: 0x001160FE
		internal Assembly[] AssemblyInternal
		{
			get
			{
				if (this._assembly == null)
				{
					this._assembly = this._compilationSection.LoadAssembly(this);
				}
				return this._assembly;
			}
			set
			{
				this._assembly = value;
			}
		}

		// Token: 0x04002AFB RID: 11003
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002AFC RID: 11004
		private static readonly ConfigurationProperty _propAssembly = new ConfigurationProperty("assembly", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsAssemblyStringTransformationRequired);

		// Token: 0x04002AFD RID: 11005
		private Assembly[] _assembly;

		// Token: 0x04002AFE RID: 11006
		private CompilationSection _compilationSection;
	}
}
