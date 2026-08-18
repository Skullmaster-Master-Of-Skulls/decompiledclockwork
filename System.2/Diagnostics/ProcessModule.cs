using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004FC RID: 1276
	[Designer("System.Diagnostics.Design.ProcessModuleDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ProcessModule : Component
	{
		// Token: 0x06003061 RID: 12385 RVA: 0x000DBA28 File Offset: 0x000D9C28
		internal ProcessModule(ModuleInfo moduleInfo)
		{
			this.moduleInfo = moduleInfo;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000DBA3D File Offset: 0x000D9C3D
		internal void EnsureNtProcessInfo()
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x000DBA5C File Offset: 0x000D9C5C
		[MonitoringDescription("ProcModModuleName")]
		public string ModuleName
		{
			get
			{
				return this.moduleInfo.baseName;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x000DBA69 File Offset: 0x000D9C69
		[MonitoringDescription("ProcModFileName")]
		public string FileName
		{
			get
			{
				return this.moduleInfo.fileName;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x000DBA76 File Offset: 0x000D9C76
		[MonitoringDescription("ProcModBaseAddress")]
		public IntPtr BaseAddress
		{
			get
			{
				return this.moduleInfo.baseOfDll;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x000DBA83 File Offset: 0x000D9C83
		[MonitoringDescription("ProcModModuleMemorySize")]
		public int ModuleMemorySize
		{
			get
			{
				return this.moduleInfo.sizeOfImage;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06003067 RID: 12391 RVA: 0x000DBA90 File Offset: 0x000D9C90
		[MonitoringDescription("ProcModEntryPointAddress")]
		public IntPtr EntryPointAddress
		{
			get
			{
				this.EnsureNtProcessInfo();
				return this.moduleInfo.entryPoint;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x000DBAA3 File Offset: 0x000D9CA3
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

		// Token: 0x06003069 RID: 12393 RVA: 0x000DBAC4 File Offset: 0x000D9CC4
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} ({1})", new object[]
			{
				base.ToString(),
				this.ModuleName
			});
		}

		// Token: 0x04002896 RID: 10390
		internal ModuleInfo moduleInfo;

		// Token: 0x04002897 RID: 10391
		private FileVersionInfo fileVersionInfo;
	}
}
