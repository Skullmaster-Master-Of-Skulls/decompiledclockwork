using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000677 RID: 1655
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class CompilerInfo
	{
		// Token: 0x06003CEA RID: 15594 RVA: 0x000FB333 File Offset: 0x000F9533
		private CompilerInfo()
		{
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x000FB33B File Offset: 0x000F953B
		public string[] GetLanguages()
		{
			return this.CloneCompilerLanguages();
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x000FB343 File Offset: 0x000F9543
		public string[] GetExtensions()
		{
			return this.CloneCompilerExtensions();
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06003CED RID: 15597 RVA: 0x000FB34C File Offset: 0x000F954C
		public Type CodeDomProviderType
		{
			get
			{
				if (this.type == null)
				{
					lock (this)
					{
						if (this.type == null)
						{
							this.type = Type.GetType(this._codeDomProviderTypeName);
							if (this.type == null)
							{
								if (this.configFileName == null)
								{
									throw new ConfigurationErrorsException(SR.GetString("Unable_To_Locate_Type", new object[]
									{
										this._codeDomProviderTypeName,
										string.Empty,
										0
									}));
								}
								throw new ConfigurationErrorsException(SR.GetString("Unable_To_Locate_Type", new object[]
								{
									this._codeDomProviderTypeName
								}), this.configFileName, this.configFileLineNumber);
							}
						}
					}
				}
				return this.type;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06003CEE RID: 15598 RVA: 0x000FB42C File Offset: 0x000F962C
		public bool IsCodeDomProviderTypeValid
		{
			get
			{
				Type left = Type.GetType(this._codeDomProviderTypeName);
				return left != null;
			}
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x000FB44C File Offset: 0x000F964C
		public CodeDomProvider CreateProvider()
		{
			if (this._providerOptions.Count > 0)
			{
				ConstructorInfo constructor = this.CodeDomProviderType.GetConstructor(new Type[]
				{
					typeof(IDictionary<string, string>)
				});
				if (constructor != null)
				{
					return (CodeDomProvider)constructor.Invoke(new object[]
					{
						this._providerOptions
					});
				}
			}
			return (CodeDomProvider)Activator.CreateInstance(this.CodeDomProviderType);
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x000FB4BC File Offset: 0x000F96BC
		public CodeDomProvider CreateProvider(IDictionary<string, string> providerOptions)
		{
			if (providerOptions == null)
			{
				throw new ArgumentNullException("providerOptions");
			}
			ConstructorInfo constructor = this.CodeDomProviderType.GetConstructor(new Type[]
			{
				typeof(IDictionary<string, string>)
			});
			if (constructor != null)
			{
				return (CodeDomProvider)constructor.Invoke(new object[]
				{
					providerOptions
				});
			}
			throw new InvalidOperationException(SR.GetString("Provider_does_not_support_options", new object[]
			{
				this.CodeDomProviderType.ToString()
			}));
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x000FB538 File Offset: 0x000F9738
		public CompilerParameters CreateDefaultCompilerParameters()
		{
			return this.CloneCompilerParameters();
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x000FB540 File Offset: 0x000F9740
		internal CompilerInfo(CompilerParameters compilerParams, string codeDomProviderTypeName, string[] compilerLanguages, string[] compilerExtensions)
		{
			this._compilerLanguages = compilerLanguages;
			this._compilerExtensions = compilerExtensions;
			this._codeDomProviderTypeName = codeDomProviderTypeName;
			if (compilerParams == null)
			{
				compilerParams = new CompilerParameters();
			}
			this._compilerParams = compilerParams;
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000FB56F File Offset: 0x000F976F
		internal CompilerInfo(CompilerParameters compilerParams, string codeDomProviderTypeName)
		{
			this._codeDomProviderTypeName = codeDomProviderTypeName;
			if (compilerParams == null)
			{
				compilerParams = new CompilerParameters();
			}
			this._compilerParams = compilerParams;
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x000FB58F File Offset: 0x000F978F
		public override int GetHashCode()
		{
			return this._codeDomProviderTypeName.GetHashCode();
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x000FB59C File Offset: 0x000F979C
		public override bool Equals(object o)
		{
			CompilerInfo compilerInfo = o as CompilerInfo;
			return o != null && (this.CodeDomProviderType == compilerInfo.CodeDomProviderType && this.CompilerParams.WarningLevel == compilerInfo.CompilerParams.WarningLevel && this.CompilerParams.IncludeDebugInformation == compilerInfo.CompilerParams.IncludeDebugInformation) && this.CompilerParams.CompilerOptions == compilerInfo.CompilerParams.CompilerOptions;
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x000FB618 File Offset: 0x000F9818
		private CompilerParameters CloneCompilerParameters()
		{
			return new CompilerParameters
			{
				IncludeDebugInformation = this._compilerParams.IncludeDebugInformation,
				TreatWarningsAsErrors = this._compilerParams.TreatWarningsAsErrors,
				WarningLevel = this._compilerParams.WarningLevel,
				CompilerOptions = this._compilerParams.CompilerOptions
			};
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x000FB670 File Offset: 0x000F9870
		private string[] CloneCompilerLanguages()
		{
			string[] array = new string[this._compilerLanguages.Length];
			Array.Copy(this._compilerLanguages, array, this._compilerLanguages.Length);
			return array;
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x000FB6A0 File Offset: 0x000F98A0
		private string[] CloneCompilerExtensions()
		{
			string[] array = new string[this._compilerExtensions.Length];
			Array.Copy(this._compilerExtensions, array, this._compilerExtensions.Length);
			return array;
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003CF9 RID: 15609 RVA: 0x000FB6D0 File Offset: 0x000F98D0
		internal CompilerParameters CompilerParams
		{
			get
			{
				return this._compilerParams;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x000FB6D8 File Offset: 0x000F98D8
		internal IDictionary<string, string> ProviderOptions
		{
			get
			{
				return this._providerOptions;
			}
		}

		// Token: 0x04002C7E RID: 11390
		internal string _codeDomProviderTypeName;

		// Token: 0x04002C7F RID: 11391
		internal CompilerParameters _compilerParams;

		// Token: 0x04002C80 RID: 11392
		internal string[] _compilerLanguages;

		// Token: 0x04002C81 RID: 11393
		internal string[] _compilerExtensions;

		// Token: 0x04002C82 RID: 11394
		internal string configFileName;

		// Token: 0x04002C83 RID: 11395
		internal IDictionary<string, string> _providerOptions;

		// Token: 0x04002C84 RID: 11396
		internal int configFileLineNumber;

		// Token: 0x04002C85 RID: 11397
		internal bool _mapped;

		// Token: 0x04002C86 RID: 11398
		private Type type;
	}
}
