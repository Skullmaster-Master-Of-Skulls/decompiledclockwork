using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006C2 RID: 1730
	public sealed class CodeSubDirectory : ConfigurationElement
	{
		// Token: 0x06005360 RID: 21344 RVA: 0x00124E1C File Offset: 0x0012301C
		static CodeSubDirectory()
		{
			CodeSubDirectory._properties = new ConfigurationPropertyCollection();
			CodeSubDirectory._properties.Add(CodeSubDirectory._propDirectoryName);
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x00117E9E File Offset: 0x0011609E
		internal CodeSubDirectory()
		{
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x00124E5C File Offset: 0x0012305C
		public CodeSubDirectory(string directoryName)
		{
			this.DirectoryName = directoryName;
		}

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x06005363 RID: 21347 RVA: 0x00124E6B File Offset: 0x0012306B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CodeSubDirectory._properties;
			}
		}

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x06005364 RID: 21348 RVA: 0x00124E72 File Offset: 0x00123072
		// (set) Token: 0x06005365 RID: 21349 RVA: 0x00124E84 File Offset: 0x00123084
		[ConfigurationProperty("directoryName", IsRequired = true, IsKey = true, DefaultValue = "")]
		[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
		public string DirectoryName
		{
			get
			{
				return (string)base[CodeSubDirectory._propDirectoryName];
			}
			set
			{
				base[CodeSubDirectory._propDirectoryName] = value;
			}
		}

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x06005366 RID: 21350 RVA: 0x00124E92 File Offset: 0x00123092
		internal string AssemblyName
		{
			get
			{
				return this.DirectoryName;
			}
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x00124E9C File Offset: 0x0012309C
		internal void DoRuntimeValidation()
		{
			string directoryName = this.DirectoryName;
			if (BuildManager.IsPrecompiledApp)
			{
				return;
			}
			if (!Util.IsValidFileName(directoryName))
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_CodeSubDirectory", new object[]
				{
					directoryName
				}), base.ElementInformation.Properties["directoryName"].Source, base.ElementInformation.Properties["directoryName"].LineNumber);
			}
			VirtualPath virtualPath = HttpRuntime.CodeDirectoryVirtualPath.SimpleCombineWithDir(directoryName);
			if (!VirtualPathProvider.DirectoryExistsNoThrow(virtualPath))
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_CodeSubDirectory_Not_Exist", new object[]
				{
					virtualPath
				}), base.ElementInformation.Properties["directoryName"].Source, base.ElementInformation.Properties["directoryName"].LineNumber);
			}
			string path = virtualPath.MapPathInternal();
			FindFileData findFileData;
			FindFileData.FindFile(path, out findFileData);
			if (!StringUtil.EqualsIgnoreCase(directoryName, findFileData.FileNameLong))
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_CodeSubDirectory", new object[]
				{
					directoryName
				}), base.ElementInformation.Properties["directoryName"].Source, base.ElementInformation.Properties["directoryName"].LineNumber);
			}
			if (BuildManager.IsReservedAssemblyName(directoryName))
			{
				throw new ConfigurationErrorsException(SR.GetString("Reserved_AssemblyName", new object[]
				{
					directoryName
				}), base.ElementInformation.Properties["directoryName"].Source, base.ElementInformation.Properties["directoryName"].LineNumber);
			}
		}

		// Token: 0x04002BE4 RID: 11236
		private const string dirNameAttribName = "directoryName";

		// Token: 0x04002BE5 RID: 11237
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002BE6 RID: 11238
		private static readonly ConfigurationProperty _propDirectoryName = new ConfigurationProperty("directoryName", typeof(string), null, StdValidatorsAndConverters.WhiteSpaceTrimStringConverter, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
