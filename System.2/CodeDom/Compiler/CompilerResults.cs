using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000679 RID: 1657
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public class CompilerResults
	{
		// Token: 0x06003D1D RID: 15645 RVA: 0x000FB8CD File Offset: 0x000F9ACD
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public CompilerResults(TempFileCollection tempFiles)
		{
			this.tempFiles = tempFiles;
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06003D1E RID: 15646 RVA: 0x000FB8F2 File Offset: 0x000F9AF2
		// (set) Token: 0x06003D1F RID: 15647 RVA: 0x000FB8FA File Offset: 0x000F9AFA
		public TempFileCollection TempFiles
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				return this.tempFiles;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				this.tempFiles = value;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06003D20 RID: 15648 RVA: 0x000FB904 File Offset: 0x000F9B04
		// (set) Token: 0x06003D21 RID: 15649 RVA: 0x000FB928 File Offset: 0x000F9B28
		[Obsolete("CAS policy is obsolete and will be removed in a future release of the .NET Framework. Please see http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public Evidence Evidence
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				Evidence result = null;
				if (this.evidence != null)
				{
					result = this.evidence.Clone();
				}
				return result;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
			set
			{
				if (value != null)
				{
					this.evidence = value.Clone();
					return;
				}
				this.evidence = null;
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x000FB944 File Offset: 0x000F9B44
		// (set) Token: 0x06003D23 RID: 15651 RVA: 0x000FB991 File Offset: 0x000F9B91
		public Assembly CompiledAssembly
		{
			[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlEvidence)]
			get
			{
				if (this.compiledAssembly == null && this.pathToAssembly != null)
				{
					this.compiledAssembly = Assembly.Load(new AssemblyName
					{
						CodeBase = this.pathToAssembly
					}, this.evidence);
				}
				return this.compiledAssembly;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				this.compiledAssembly = value;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06003D24 RID: 15652 RVA: 0x000FB99A File Offset: 0x000F9B9A
		public CompilerErrorCollection Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06003D25 RID: 15653 RVA: 0x000FB9A2 File Offset: 0x000F9BA2
		public StringCollection Output
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				return this.output;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06003D26 RID: 15654 RVA: 0x000FB9AA File Offset: 0x000F9BAA
		// (set) Token: 0x06003D27 RID: 15655 RVA: 0x000FB9B2 File Offset: 0x000F9BB2
		public string PathToAssembly
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				return this.pathToAssembly;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				this.pathToAssembly = value;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003D28 RID: 15656 RVA: 0x000FB9BB File Offset: 0x000F9BBB
		// (set) Token: 0x06003D29 RID: 15657 RVA: 0x000FB9C3 File Offset: 0x000F9BC3
		public int NativeCompilerReturnValue
		{
			get
			{
				return this.nativeCompilerReturnValue;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				this.nativeCompilerReturnValue = value;
			}
		}

		// Token: 0x04002C97 RID: 11415
		private CompilerErrorCollection errors = new CompilerErrorCollection();

		// Token: 0x04002C98 RID: 11416
		private StringCollection output = new StringCollection();

		// Token: 0x04002C99 RID: 11417
		private Assembly compiledAssembly;

		// Token: 0x04002C9A RID: 11418
		private string pathToAssembly;

		// Token: 0x04002C9B RID: 11419
		private int nativeCompilerReturnValue;

		// Token: 0x04002C9C RID: 11420
		private TempFileCollection tempFiles;

		// Token: 0x04002C9D RID: 11421
		private Evidence evidence;
	}
}
