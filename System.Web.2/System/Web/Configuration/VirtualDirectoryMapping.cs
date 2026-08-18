using System;
using System.IO;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x0200076F RID: 1903
	public sealed class VirtualDirectoryMapping
	{
		// Token: 0x06005BA3 RID: 23459 RVA: 0x0013D93B File Offset: 0x0013BB3B
		public VirtualDirectoryMapping(string physicalDirectory, bool isAppRoot) : this(null, physicalDirectory, isAppRoot, "web.config")
		{
		}

		// Token: 0x06005BA4 RID: 23460 RVA: 0x0013D94B File Offset: 0x0013BB4B
		public VirtualDirectoryMapping(string physicalDirectory, bool isAppRoot, string configFileBaseName) : this(null, physicalDirectory, isAppRoot, configFileBaseName)
		{
		}

		// Token: 0x06005BA5 RID: 23461 RVA: 0x0013D957 File Offset: 0x0013BB57
		private VirtualDirectoryMapping(VirtualPath virtualDirectory, string physicalDirectory, bool isAppRoot, string configFileBaseName)
		{
			this._virtualDirectory = virtualDirectory;
			this._isAppRoot = isAppRoot;
			this.PhysicalDirectory = physicalDirectory;
			this.ConfigFileBaseName = configFileBaseName;
		}

		// Token: 0x06005BA6 RID: 23462 RVA: 0x0013D97C File Offset: 0x0013BB7C
		internal VirtualDirectoryMapping Clone()
		{
			return new VirtualDirectoryMapping(this._virtualDirectory, this._physicalDirectory, this._isAppRoot, this._configFileBaseName);
		}

		// Token: 0x17001ADD RID: 6877
		// (get) Token: 0x06005BA7 RID: 23463 RVA: 0x0013D99B File Offset: 0x0013BB9B
		public string VirtualDirectory
		{
			get
			{
				if (!(this._virtualDirectory != null))
				{
					return string.Empty;
				}
				return this._virtualDirectory.VirtualPathString;
			}
		}

		// Token: 0x17001ADE RID: 6878
		// (get) Token: 0x06005BA8 RID: 23464 RVA: 0x0013D9BC File Offset: 0x0013BBBC
		internal VirtualPath VirtualDirectoryObject
		{
			get
			{
				return this._virtualDirectory;
			}
		}

		// Token: 0x06005BA9 RID: 23465 RVA: 0x0013D9C4 File Offset: 0x0013BBC4
		internal void SetVirtualDirectory(VirtualPath virtualDirectory)
		{
			this._virtualDirectory = virtualDirectory;
		}

		// Token: 0x17001ADF RID: 6879
		// (get) Token: 0x06005BAA RID: 23466 RVA: 0x0013D9CD File Offset: 0x0013BBCD
		// (set) Token: 0x06005BAB RID: 23467 RVA: 0x0013D9D8 File Offset: 0x0013BBD8
		public string PhysicalDirectory
		{
			get
			{
				return this._physicalDirectory;
			}
			set
			{
				string text = value;
				if (string.IsNullOrEmpty(text))
				{
					text = null;
				}
				else
				{
					if (UrlPath.PathEndsWithExtraSlash(text))
					{
						text = text.Substring(0, text.Length - 1);
					}
					if (FileUtil.IsSuspiciousPhysicalPath(text))
					{
						throw ExceptionUtil.ParameterInvalid("PhysicalDirectory");
					}
				}
				this._physicalDirectory = text;
			}
		}

		// Token: 0x17001AE0 RID: 6880
		// (get) Token: 0x06005BAC RID: 23468 RVA: 0x0013DA25 File Offset: 0x0013BC25
		// (set) Token: 0x06005BAD RID: 23469 RVA: 0x0013DA2D File Offset: 0x0013BC2D
		public bool IsAppRoot
		{
			get
			{
				return this._isAppRoot;
			}
			set
			{
				this._isAppRoot = value;
			}
		}

		// Token: 0x17001AE1 RID: 6881
		// (get) Token: 0x06005BAE RID: 23470 RVA: 0x0013DA36 File Offset: 0x0013BC36
		// (set) Token: 0x06005BAF RID: 23471 RVA: 0x0013DA3E File Offset: 0x0013BC3E
		public string ConfigFileBaseName
		{
			get
			{
				return this._configFileBaseName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw ExceptionUtil.PropertyInvalid("ConfigFileBaseName");
				}
				this._configFileBaseName = value;
			}
		}

		// Token: 0x06005BB0 RID: 23472 RVA: 0x0013DA5C File Offset: 0x0013BC5C
		internal void Validate()
		{
			if (this._physicalDirectory != null)
			{
				string text = Path.Combine(this._physicalDirectory, this._configFileBaseName);
				string fullPath = Path.GetFullPath(text);
				if (Path.GetDirectoryName(fullPath) != this._physicalDirectory || Path.GetFileName(fullPath) != this._configFileBaseName || FileUtil.IsSuspiciousPhysicalPath(text))
				{
					throw ExceptionUtil.ParameterInvalid("configFileBaseName");
				}
			}
		}

		// Token: 0x04003047 RID: 12359
		private VirtualPath _virtualDirectory;

		// Token: 0x04003048 RID: 12360
		private string _physicalDirectory;

		// Token: 0x04003049 RID: 12361
		private string _configFileBaseName;

		// Token: 0x0400304A RID: 12362
		private bool _isAppRoot;

		// Token: 0x0400304B RID: 12363
		private const string DEFAULT_BASE_NAME = "web.config";
	}
}
