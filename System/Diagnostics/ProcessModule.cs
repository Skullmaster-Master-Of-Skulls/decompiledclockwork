using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x02000787 RID: 1927
	[Designer("System.Diagnostics.Design.ProcessModuleDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ProcessModule : Component
	{
		// Token: 0x06003B7C RID: 15228 RVA: 0x000FDE2C File Offset: 0x000FCE2C
		internal ProcessModule(ModuleInfo moduleInfo)
		{
			this.moduleInfo = moduleInfo;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x000FDE41 File Offset: 0x000FCE41
		internal void EnsureNtProcessInfo()
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06003B7E RID: 15230 RVA: 0x000FDE60 File Offset: 0x000FCE60
		[MonitoringDescription("ProcModModuleName")]
		public string ModuleName
		{
			get
			{
				return this.moduleInfo.baseName;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06003B7F RID: 15231 RVA: 0x000FDE6D File Offset: 0x000FCE6D
		[MonitoringDescription("ProcModFileName")]
		public string FileName
		{
			get
			{
				return this.moduleInfo.fileName;
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06003B80 RID: 15232 RVA: 0x000FDE7A File Offset: 0x000FCE7A
		[MonitoringDescription("ProcModBaseAddress")]
		public IntPtr BaseAddress
		{
			get
			{
				return this.moduleInfo.baseOfDll;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x000FDE87 File Offset: 0x000FCE87
		[MonitoringDescription("ProcModModuleMemorySize")]
		public int ModuleMemorySize
		{
			get
			{
				return this.moduleInfo.sizeOfImage;
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06003B82 RID: 15234 RVA: 0x000FDE94 File Offset: 0x000FCE94
		[MonitoringDescription("ProcModEntryPointAddress")]
		public IntPtr EntryPointAddress
		{
			get
			{
				this.EnsureNtProcessInfo();
				return this.moduleInfo.entryPoint;
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06003B83 RID: 15235 RVA: 0x000FDEA7 File Offset: 0x000FCEA7
		[Browsable(false)]
		public FileVersionInfo FileVersionInfo
		{
			get
			{
				if (this.fileVersionInfo == null)
				{
					this.fileVersionInfo = FileVersionInfo.GetVersionInfo(this.FileName);
				}
				return this.fileVersionInfo;
			}
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x000FDEC8 File Offset: 0x000FCEC8
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} ({1})", new object[]
			{
				base.ToString(),
				this.ModuleName
			});
		}

		// Token: 0x04003438 RID: 13368
		internal ModuleInfo moduleInfo;

		// Token: 0x04003439 RID: 13369
		private FileVersionInfo fileVersionInfo;
	}
}
