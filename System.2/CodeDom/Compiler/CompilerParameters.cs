using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Policy;
using Microsoft.Win32.SafeHandles;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000678 RID: 1656
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public class CompilerParameters
	{
		// Token: 0x06003CFB RID: 15611 RVA: 0x000FB6E0 File Offset: 0x000F98E0
		public CompilerParameters() : this(null, null)
		{
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x000FB6EA File Offset: 0x000F98EA
		public CompilerParameters(string[] assemblyNames) : this(assemblyNames, null, false)
		{
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x000FB6F5 File Offset: 0x000F98F5
		public CompilerParameters(string[] assemblyNames, string outputName) : this(assemblyNames, outputName, false)
		{
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x000FB700 File Offset: 0x000F9900
		public CompilerParameters(string[] assemblyNames, string outputName, bool includeDebugInformation)
		{
			if (assemblyNames != null)
			{
				this.ReferencedAssemblies.AddRange(assemblyNames);
			}
			this.outputName = outputName;
			this.includeDebugInformation = includeDebugInformation;
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06003CFF RID: 15615 RVA: 0x000FB763 File Offset: 0x000F9963
		// (set) Token: 0x06003D00 RID: 15616 RVA: 0x000FB76B File Offset: 0x000F996B
		public string CoreAssemblyFileName
		{
			get
			{
				return this.coreAssemblyFileName;
			}
			set
			{
				this.coreAssemblyFileName = value;
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x000FB774 File Offset: 0x000F9974
		// (set) Token: 0x06003D02 RID: 15618 RVA: 0x000FB77C File Offset: 0x000F997C
		public bool GenerateExecutable
		{
			get
			{
				return this.generateExecutable;
			}
			set
			{
				this.generateExecutable = value;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003D03 RID: 15619 RVA: 0x000FB785 File Offset: 0x000F9985
		// (set) Token: 0x06003D04 RID: 15620 RVA: 0x000FB78D File Offset: 0x000F998D
		public bool GenerateInMemory
		{
			get
			{
				return this.generateInMemory;
			}
			set
			{
				this.generateInMemory = value;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003D05 RID: 15621 RVA: 0x000FB796 File Offset: 0x000F9996
		public StringCollection ReferencedAssemblies
		{
			get
			{
				return this.assemblyNames;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06003D06 RID: 15622 RVA: 0x000FB79E File Offset: 0x000F999E
		// (set) Token: 0x06003D07 RID: 15623 RVA: 0x000FB7A6 File Offset: 0x000F99A6
		public string MainClass
		{
			get
			{
				return this.mainClass;
			}
			set
			{
				this.mainClass = value;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06003D08 RID: 15624 RVA: 0x000FB7AF File Offset: 0x000F99AF
		// (set) Token: 0x06003D09 RID: 15625 RVA: 0x000FB7B7 File Offset: 0x000F99B7
		public string OutputAssembly
		{
			get
			{
				return this.outputName;
			}
			set
			{
				this.outputName = value;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x000FB7C0 File Offset: 0x000F99C0
		// (set) Token: 0x06003D0B RID: 15627 RVA: 0x000FB7DB File Offset: 0x000F99DB
		public TempFileCollection TempFiles
		{
			get
			{
				if (this.tempFiles == null)
				{
					this.tempFiles = new TempFileCollection();
				}
				return this.tempFiles;
			}
			set
			{
				this.tempFiles = value;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x000FB7E4 File Offset: 0x000F99E4
		// (set) Token: 0x06003D0D RID: 15629 RVA: 0x000FB7EC File Offset: 0x000F99EC
		public bool IncludeDebugInformation
		{
			get
			{
				return this.includeDebugInformation;
			}
			set
			{
				this.includeDebugInformation = value;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003D0E RID: 15630 RVA: 0x000FB7F5 File Offset: 0x000F99F5
		// (set) Token: 0x06003D0F RID: 15631 RVA: 0x000FB7FD File Offset: 0x000F99FD
		public bool TreatWarningsAsErrors
		{
			get
			{
				return this.treatWarningsAsErrors;
			}
			set
			{
				this.treatWarningsAsErrors = value;
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06003D10 RID: 15632 RVA: 0x000FB806 File Offset: 0x000F9A06
		// (set) Token: 0x06003D11 RID: 15633 RVA: 0x000FB80E File Offset: 0x000F9A0E
		public int WarningLevel
		{
			get
			{
				return this.warningLevel;
			}
			set
			{
				this.warningLevel = value;
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06003D12 RID: 15634 RVA: 0x000FB817 File Offset: 0x000F9A17
		// (set) Token: 0x06003D13 RID: 15635 RVA: 0x000FB81F File Offset: 0x000F9A1F
		public string CompilerOptions
		{
			get
			{
				return this.compilerOptions;
			}
			set
			{
				this.compilerOptions = value;
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06003D14 RID: 15636 RVA: 0x000FB828 File Offset: 0x000F9A28
		// (set) Token: 0x06003D15 RID: 15637 RVA: 0x000FB830 File Offset: 0x000F9A30
		public string Win32Resource
		{
			get
			{
				return this.win32Resource;
			}
			set
			{
				this.win32Resource = value;
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06003D16 RID: 15638 RVA: 0x000FB839 File Offset: 0x000F9A39
		[ComVisible(false)]
		public StringCollection EmbeddedResources
		{
			get
			{
				return this.embeddedResources;
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06003D17 RID: 15639 RVA: 0x000FB841 File Offset: 0x000F9A41
		[ComVisible(false)]
		public StringCollection LinkedResources
		{
			get
			{
				return this.linkedResources;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06003D18 RID: 15640 RVA: 0x000FB849 File Offset: 0x000F9A49
		// (set) Token: 0x06003D19 RID: 15641 RVA: 0x000FB864 File Offset: 0x000F9A64
		public IntPtr UserToken
		{
			get
			{
				if (this.userToken != null)
				{
					return this.userToken.DangerousGetHandle();
				}
				return IntPtr.Zero;
			}
			set
			{
				if (this.userToken != null)
				{
					this.userToken.Close();
				}
				this.userToken = new SafeUserTokenHandle(value, false);
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06003D1A RID: 15642 RVA: 0x000FB886 File Offset: 0x000F9A86
		internal SafeUserTokenHandle SafeUserToken
		{
			get
			{
				return this.userToken;
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06003D1B RID: 15643 RVA: 0x000FB890 File Offset: 0x000F9A90
		// (set) Token: 0x06003D1C RID: 15644 RVA: 0x000FB8B4 File Offset: 0x000F9AB4
		[Obsolete("CAS policy is obsolete and will be removed in a future release of the .NET Framework. Please see http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public Evidence Evidence
		{
			get
			{
				Evidence result = null;
				if (this.evidence != null)
				{
					result = this.evidence.Clone();
				}
				return result;
			}
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

		// Token: 0x04002C87 RID: 11399
		[OptionalField]
		private string coreAssemblyFileName = string.Empty;

		// Token: 0x04002C88 RID: 11400
		private StringCollection assemblyNames = new StringCollection();

		// Token: 0x04002C89 RID: 11401
		[OptionalField]
		private StringCollection embeddedResources = new StringCollection();

		// Token: 0x04002C8A RID: 11402
		[OptionalField]
		private StringCollection linkedResources = new StringCollection();

		// Token: 0x04002C8B RID: 11403
		private string outputName;

		// Token: 0x04002C8C RID: 11404
		private string mainClass;

		// Token: 0x04002C8D RID: 11405
		private bool generateInMemory;

		// Token: 0x04002C8E RID: 11406
		private bool includeDebugInformation;

		// Token: 0x04002C8F RID: 11407
		private int warningLevel = -1;

		// Token: 0x04002C90 RID: 11408
		private string compilerOptions;

		// Token: 0x04002C91 RID: 11409
		private string win32Resource;

		// Token: 0x04002C92 RID: 11410
		private bool treatWarningsAsErrors;

		// Token: 0x04002C93 RID: 11411
		private bool generateExecutable;

		// Token: 0x04002C94 RID: 11412
		private TempFileCollection tempFiles;

		// Token: 0x04002C95 RID: 11413
		[NonSerialized]
		private SafeUserTokenHandle userToken;

		// Token: 0x04002C96 RID: 11414
		private Evidence evidence;
	}
}
