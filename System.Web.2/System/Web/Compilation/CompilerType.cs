using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Web.Configuration;

namespace System.Web.Compilation
{
	// Token: 0x02000835 RID: 2101
	public sealed class CompilerType
	{
		// Token: 0x17001C3F RID: 7231
		// (get) Token: 0x06006438 RID: 25656 RVA: 0x0015FDA8 File Offset: 0x0015DFA8
		public Type CodeDomProviderType
		{
			get
			{
				return this._codeDomProviderType;
			}
		}

		// Token: 0x17001C40 RID: 7232
		// (get) Token: 0x06006439 RID: 25657 RVA: 0x0015FDB0 File Offset: 0x0015DFB0
		public CompilerParameters CompilerParameters
		{
			get
			{
				return this._compilParams;
			}
		}

		// Token: 0x0600643A RID: 25658 RVA: 0x0015FDB8 File Offset: 0x0015DFB8
		internal CompilerType(Type codeDomProviderType, CompilerParameters compilParams)
		{
			this._codeDomProviderType = codeDomProviderType;
			if (compilParams == null)
			{
				this._compilParams = new CompilerParameters();
				return;
			}
			this._compilParams = compilParams;
		}

		// Token: 0x0600643B RID: 25659 RVA: 0x0015FDDD File Offset: 0x0015DFDD
		internal CompilerType Clone()
		{
			return new CompilerType(this._codeDomProviderType, this.CloneCompilerParameters());
		}

		// Token: 0x0600643C RID: 25660 RVA: 0x0015FDF0 File Offset: 0x0015DFF0
		private CompilerParameters CloneCompilerParameters()
		{
			return new CompilerParameters
			{
				IncludeDebugInformation = this._compilParams.IncludeDebugInformation,
				TreatWarningsAsErrors = this._compilParams.TreatWarningsAsErrors,
				WarningLevel = this._compilParams.WarningLevel,
				CompilerOptions = this._compilParams.CompilerOptions
			};
		}

		// Token: 0x0600643D RID: 25661 RVA: 0x0015FE48 File Offset: 0x0015E048
		public override int GetHashCode()
		{
			return this._codeDomProviderType.GetHashCode();
		}

		// Token: 0x0600643E RID: 25662 RVA: 0x0015FE58 File Offset: 0x0015E058
		public override bool Equals(object o)
		{
			CompilerType compilerType = o as CompilerType;
			return o != null && (this._codeDomProviderType == compilerType._codeDomProviderType && this._compilParams.WarningLevel == compilerType._compilParams.WarningLevel && this._compilParams.IncludeDebugInformation == compilerType._compilParams.IncludeDebugInformation) && this._compilParams.CompilerOptions == compilerType._compilParams.CompilerOptions;
		}

		// Token: 0x0600643F RID: 25663 RVA: 0x0015FED1 File Offset: 0x0015E0D1
		internal AssemblyBuilder CreateAssemblyBuilder(CompilationSection compConfig, ICollection referencedAssemblies)
		{
			return this.CreateAssemblyBuilder(compConfig, referencedAssemblies, null, null);
		}

		// Token: 0x06006440 RID: 25664 RVA: 0x0015FEDD File Offset: 0x0015E0DD
		internal AssemblyBuilder CreateAssemblyBuilder(CompilationSection compConfig, ICollection referencedAssemblies, string generatedFilesDir, string outputAssemblyName)
		{
			if (generatedFilesDir != null)
			{
				return new CbmCodeGeneratorBuildProviderHost(compConfig, referencedAssemblies, this, generatedFilesDir, outputAssemblyName);
			}
			return new AssemblyBuilder(compConfig, referencedAssemblies, this, outputAssemblyName);
		}

		// Token: 0x06006441 RID: 25665 RVA: 0x0015FEF8 File Offset: 0x0015E0F8
		private static CompilerType GetDefaultCompilerTypeWithParams(CompilationSection compConfig, VirtualPath configPath)
		{
			return CompilationUtil.GetCSharpCompilerInfo(compConfig, configPath);
		}

		// Token: 0x06006442 RID: 25666 RVA: 0x0015FF01 File Offset: 0x0015E101
		internal static AssemblyBuilder GetDefaultAssemblyBuilder(CompilationSection compConfig, ICollection referencedAssemblies, VirtualPath configPath, string outputAssemblyName)
		{
			return CompilerType.GetDefaultAssemblyBuilder(compConfig, referencedAssemblies, configPath, null, outputAssemblyName);
		}

		// Token: 0x06006443 RID: 25667 RVA: 0x0015FF10 File Offset: 0x0015E110
		internal static AssemblyBuilder GetDefaultAssemblyBuilder(CompilationSection compConfig, ICollection referencedAssemblies, VirtualPath configPath, string generatedFilesDir, string outputAssemblyName)
		{
			CompilerType defaultCompilerTypeWithParams = CompilerType.GetDefaultCompilerTypeWithParams(compConfig, configPath);
			return defaultCompilerTypeWithParams.CreateAssemblyBuilder(compConfig, referencedAssemblies, generatedFilesDir, outputAssemblyName);
		}

		// Token: 0x040033DA RID: 13274
		private Type _codeDomProviderType;

		// Token: 0x040033DB RID: 13275
		private CompilerParameters _compilParams;
	}
}
