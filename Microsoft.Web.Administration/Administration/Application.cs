using System;
using System.Diagnostics;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000004 RID: 4
	[DebuggerDisplay("Path = {Path}")]
	public sealed class Application : ConfigurationElement
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002AC7 File Offset: 0x00001AC7
		internal Application(ServerManager owner, Site site)
		{
			this._site = site;
			this._owner = owner;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002ADD File Offset: 0x00001ADD
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002AEF File Offset: 0x00001AEF
		public string ApplicationPoolName
		{
			get
			{
				return (string)base["applicationPool"];
			}
			set
			{
				base["applicationPool"] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002AFD File Offset: 0x00001AFD
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002B0F File Offset: 0x00001B0F
		public string EnabledProtocols
		{
			get
			{
				return (string)base["enabledProtocols"];
			}
			set
			{
				base["enabledProtocols"] = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002B20 File Offset: 0x00001B20
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002B5C File Offset: 0x00001B5C
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002B70 File Offset: 0x00001B70
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002B96 File Offset: 0x00001B96
		internal Site Site
		{
			get
			{
				return this._site;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002B9E File Offset: 0x00001B9E
		public VirtualDirectoryCollection VirtualDirectories
		{
			get
			{
				if (this._virtualDirectories == null)
				{
					this._virtualDirectories = (VirtualDirectoryCollection)base.GetCollection(typeof(VirtualDirectoryCollection));
					this._virtualDirectories.SetParentApplication(this);
				}
				return this._virtualDirectories;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002BD8 File Offset: 0x00001BD8
		public VirtualDirectoryDefaults VirtualDirectoryDefaults
		{
			get
			{
				if (this._virtualDirectoryDefaults == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("virtualDirectoryDefaults");
					this._virtualDirectoryDefaults = new VirtualDirectoryDefaults(this._site.VirtualDirectoryDefaults);
					this._virtualDirectoryDefaults.Initialize(base.Configuration, elementByName);
				}
				return this._virtualDirectoryDefaults;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C2C File Offset: 0x00001C2C
		public Configuration GetWebConfiguration()
		{
			return this._owner.GetWebConfiguration(this.Site.Name, this.Path);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C4A File Offset: 0x00001C4A
		public override string ToString()
		{
			return ConfigurationManager.CombineConfigurationPath(this.Site.Name, this.Path);
		}

		// Token: 0x0400000C RID: 12
		private VirtualDirectoryDefaults _virtualDirectoryDefaults;

		// Token: 0x0400000D RID: 13
		private VirtualDirectoryCollection _virtualDirectories;

		// Token: 0x0400000E RID: 14
		private Site _site;

		// Token: 0x0400000F RID: 15
		private ServerManager _owner;

		// Token: 0x04000010 RID: 16
		private IAppHostProperty _pathProperty;
	}
}
