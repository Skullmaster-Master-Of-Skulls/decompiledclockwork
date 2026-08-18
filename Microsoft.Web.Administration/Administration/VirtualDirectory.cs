using System;
using System.Diagnostics;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000074 RID: 116
	[DebuggerDisplay("Path = {Path}")]
	public sealed class VirtualDirectory : ConfigurationElement
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00008C3C File Offset: 0x00007C3C
		internal VirtualDirectory(Application application)
		{
			this._application = application;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00008C4B File Offset: 0x00007C4B
		private Application Application
		{
			get
			{
				return this._application;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00008C53 File Offset: 0x00007C53
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00008C65 File Offset: 0x00007C65
		public AuthenticationLogonMethod LogonMethod
		{
			get
			{
				return (AuthenticationLogonMethod)base["logonMethod"];
			}
			set
			{
				base["logonMethod"] = (int)value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00008C78 File Offset: 0x00007C78
		// (set) Token: 0x06000355 RID: 853 RVA: 0x00008C8A File Offset: 0x00007C8A
		public string Password
		{
			get
			{
				return (string)base["password"];
			}
			set
			{
				base["password"] = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00008C98 File Offset: 0x00007C98
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00008CD4 File Offset: 0x00007CD4
		public string Path
		{
			get
			{
				string text = (string)this.PathProperty.Value;
				if (text == null || !text.StartsWith("/", StringComparison.OrdinalIgnoreCase))
				{
					text = "/" + text;
				}
				return text;
			}
			set
			{
				this.PathProperty.Value = value;
				base.SetDirty();
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00008CE8 File Offset: 0x00007CE8
		private IAppHostProperty PathProperty
		{
			get
			{
				if (this._pathProperty == null)
				{
					this._pathProperty = base.AppHostElement.GetPropertyByName("path");
				}
				return this._pathProperty;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00008D0E File Offset: 0x00007D0E
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00008D20 File Offset: 0x00007D20
		public string PhysicalPath
		{
			get
			{
				return (string)base["physicalPath"];
			}
			set
			{
				base["physicalPath"] = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00008D2E File Offset: 0x00007D2E
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00008D40 File Offset: 0x00007D40
		public string UserName
		{
			get
			{
				return (string)base["userName"];
			}
			set
			{
				base["userName"] = value;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00008D4E File Offset: 0x00007D4E
		public override string ToString()
		{
			return ConfigurationManager.CombineConfigurationPath(this.Application.ToString(), this.Path);
		}

		// Token: 0x0400012A RID: 298
		private IAppHostProperty _pathProperty;

		// Token: 0x0400012B RID: 299
		private Application _application;
	}
}
