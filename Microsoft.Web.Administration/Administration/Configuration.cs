using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000018 RID: 24
	public sealed class Configuration
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00004FA9 File Offset: 0x00003FA9
		internal Configuration(ConfigurationManager configurationManager, IAppHostWritableAdminManager adminManager, string configPathToEdit)
		{
			this._adminManager = adminManager;
			this._configPathToEdit = configPathToEdit;
			this._sectionTable = new Dictionary<string, IAppHostElement>();
			this._configurationManager = configurationManager;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004FD1 File Offset: 0x00003FD1
		internal Configuration(ConfigurationManager configurationManager, IAppHostAdminManager adminManager, string configPathToEdit)
		{
			this._readManager = adminManager;
			this._configPathToEdit = configPathToEdit;
			this._sectionTable = new Dictionary<string, IAppHostElement>();
			this._configurationManager = configurationManager;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004FF9 File Offset: 0x00003FF9
		private IAppHostAdminManager AdminManager
		{
			get
			{
				if (this._adminManager != null)
				{
					return this._adminManager;
				}
				return this._readManager;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005010 File Offset: 0x00004010
		internal IAppHostConfigFile ConfigFile
		{
			get
			{
				if (this._configFile == null)
				{
					IAppHostConfigManager appHostConfigManager = null;
					try
					{
						appHostConfigManager = this.AdminManager.ConfigManager;
						this._configFile = appHostConfigManager.GetConfigFile(this._configPathToEdit);
					}
					finally
					{
						if (appHostConfigManager != null)
						{
							Marshal.FinalReleaseComObject(appHostConfigManager);
						}
					}
				}
				return this._configFile;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005068 File Offset: 0x00004068
		internal string ConfigurationPathToEdit
		{
			get
			{
				return this._configPathToEdit;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005070 File Offset: 0x00004070
		public SectionGroup RootSectionGroup
		{
			get
			{
				if (this._rootSectionGroup == null)
				{
					this._rootSectionGroup = new SectionGroup(this, this.ConfigFile.RootSectionGroup);
				}
				return this._rootSectionGroup;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005097 File Offset: 0x00004097
		private static ConfigurationPermission UnrestrictedConfigPermission
		{
			get
			{
				if (Configuration.s_unrestrictedConfigPermission == null)
				{
					Configuration.s_unrestrictedConfigPermission = new ConfigurationPermission(PermissionState.Unrestricted);
				}
				return Configuration.s_unrestrictedConfigPermission;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000127 RID: 295 RVA: 0x000050B0 File Offset: 0x000040B0
		// (remove) Token: 0x06000128 RID: 296 RVA: 0x000050FE File Offset: 0x000040FE
		public event EventHandler CacheInvalidated
		{
			add
			{
				this._cacheInvalidated = (EventHandler)Delegate.Combine(this._cacheInvalidated, value);
				if (this._cacheInvalidated != null)
				{
					this._nativeChangeHandler = new Configuration.NativeConfigurationChangeHandler(this);
					this.AdminManager.SetMetadata("changeHandler", this._nativeChangeHandler);
				}
			}
			remove
			{
				this._cacheInvalidated = (EventHandler)Delegate.Remove(this._cacheInvalidated, value);
				if (this._cacheInvalidated == null)
				{
					this.ReleaseNativeChangeHandler();
				}
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005125 File Offset: 0x00004125
		internal void ClearCachedObjects()
		{
			if (this._sectionTable == null)
			{
				return;
			}
			this._configFile = null;
			this._sectionTable.Clear();
			this._isDirty = false;
			this._hasBeenCommitted = false;
			this._rootSectionGroup = null;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005157 File Offset: 0x00004157
		internal void CommitChanges()
		{
			if (this._isDirty)
			{
				this._adminManager.CommitChanges();
				this._hasBeenCommitted = true;
				this._isDirty = false;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000517C File Offset: 0x0000417C
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		private static ConfigurationSection CreateSection(Type sectionType)
		{
			if (!typeof(ConfigurationSection).IsAssignableFrom(sectionType))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Resources.InvalidType, new object[]
				{
					sectionType.ToString()
				}));
			}
			ConstructorInfo constructor = sectionType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
			if (constructor == null)
			{
				throw new InvalidOperationException(Resources.ConstructorNotFound);
			}
			return (ConfigurationSection)constructor.Invoke(null);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000051EC File Offset: 0x000041EC
		private void EnsureNotDisposed()
		{
			if (this._adminManager == null && this._readManager == null)
			{
				throw new ObjectDisposedException("Configuration");
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000520C File Offset: 0x0000420C
		private string GetConfigurationPath(string locationPath)
		{
			if (string.IsNullOrEmpty(locationPath))
			{
				return this._configPathToEdit;
			}
			string result = this._configPathToEdit;
			if (this._configPathToEdit == "MACHINE/WEBROOT")
			{
				result = ConfigurationManager.CombineConfigurationPath("MACHINE/WEBROOT/APPHOST", locationPath);
			}
			else
			{
				result = ConfigurationManager.CombineConfigurationPath(this._configPathToEdit, locationPath);
			}
			return result;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005260 File Offset: 0x00004260
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SectionGroup GetEffectiveSectionGroup()
		{
			SectionGroup sectionGroup = null;
			IAppHostConfigManager appHostConfigManager = null;
			try
			{
				appHostConfigManager = this.AdminManager.ConfigManager;
				if (appHostConfigManager == null)
				{
					return null;
				}
				int num = 0;
				while (num != -1)
				{
					string text = this._configPathToEdit;
					num = text.IndexOf('/', num + 1);
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					IAppHostConfigFile configFile = appHostConfigManager.GetConfigFile(text);
					if (configFile != null)
					{
						IAppHostSectionGroup rootSectionGroup = configFile.RootSectionGroup;
						if (sectionGroup == null)
						{
							sectionGroup = new SectionGroup(null, rootSectionGroup);
						}
						else
						{
							sectionGroup.MergeWith(rootSectionGroup);
						}
					}
				}
			}
			finally
			{
				if (appHostConfigManager != null)
				{
					Marshal.FinalReleaseComObject(appHostConfigManager);
				}
			}
			return sectionGroup;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000052FC File Offset: 0x000042FC
		public string[] GetLocationPaths()
		{
			this.EnsureNotDisposed();
			string[] array = null;
			IAppHostConfigLocationCollection locations = this.ConfigFile.Locations;
			try
			{
				uint count = locations.Count;
				array = new string[count];
				for (uint num = 0U; num < count; num += 1U)
				{
					IAppHostConfigLocation appHostConfigLocation = locations[num];
					try
					{
						array[(int)((UIntPtr)num)] = appHostConfigLocation.Path;
					}
					finally
					{
						if (appHostConfigLocation != null)
						{
							Marshal.FinalReleaseComObject(appHostConfigLocation);
						}
					}
				}
			}
			finally
			{
				if (locations != null)
				{
					Marshal.FinalReleaseComObject(locations);
				}
			}
			return array;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000538C File Offset: 0x0000438C
		public object GetMetadata(string metadataType)
		{
			this.EnsureNotDisposed();
			return this.AdminManager.GetMetadata(metadataType);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000053A0 File Offset: 0x000043A0
		public ConfigurationSection GetSection(string sectionPath)
		{
			return this.GetSectionInternal(new ConfigurationSection(), sectionPath, null);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000053AF File Offset: 0x000043AF
		public ConfigurationSection GetSection(string sectionPath, Type type)
		{
			return this.GetSection(sectionPath, type, null);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000053BA File Offset: 0x000043BA
		public ConfigurationSection GetSection(string sectionPath, string locationPath)
		{
			return this.GetSectionInternal(new ConfigurationSection(), sectionPath, locationPath);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000053C9 File Offset: 0x000043C9
		public ConfigurationSection GetSection(string sectionPath, Type type, string locationPath)
		{
			return this.GetSectionInternal(Configuration.CreateSection(type), sectionPath, locationPath);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000053DC File Offset: 0x000043DC
		private ConfigurationSection GetSectionInternal(ConfigurationSection section, string sectionPath, string locationPath)
		{
			this.EnsureNotDisposed();
			string configurationPath = this.GetConfigurationPath(locationPath);
			string key = sectionPath + '|' + configurationPath;
			IAppHostElement adminSection;
			if (!this._sectionTable.TryGetValue(key, out adminSection))
			{
				lock (this._sectionTable)
				{
					if (!this._sectionTable.TryGetValue(key, out adminSection))
					{
						adminSection = this.AdminManager.GetAdminSection(sectionPath, configurationPath);
						Configuration.CheckPermissions(adminSection);
						this._sectionTable.Add(key, adminSection);
					}
				}
			}
			if (adminSection == null)
			{
				return null;
			}
			section.SetSectionPath(sectionPath);
			section.Initialize(this, adminSection);
			return section;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005484 File Offset: 0x00004484
		internal static void CheckPermissions(IAppHostElement section)
		{
			if (section == null)
			{
				return;
			}
			IAppHostSectionDefinition appHostSectionDefinition = (IAppHostSectionDefinition)section.GetMetadata("configSectionDefinition");
			if (appHostSectionDefinition == null || appHostSectionDefinition.RequirePermission)
			{
				Configuration.UnrestrictedConfigPermission.Demand();
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000054BB File Offset: 0x000044BB
		private void OnConfigurationChanged(EventArgs e)
		{
			if (this._cacheInvalidated != null)
			{
				this._cacheInvalidated(this, e);
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000054D4 File Offset: 0x000044D4
		internal void Release()
		{
			if (this._sectionTable == null)
			{
				return;
			}
			this._configurationManager = null;
			this._cacheInvalidated = null;
			this._rootSectionGroup = null;
			this._isDirty = false;
			foreach (IAppHostElement o in this._sectionTable.Values)
			{
				Marshal.FinalReleaseComObject(o);
			}
			this._sectionTable = null;
			if (this._configFile != null)
			{
				Marshal.FinalReleaseComObject(this._configFile);
				this._configFile = null;
			}
			this._nativeChangeHandler = null;
			if (this._adminManager != null)
			{
				Marshal.FinalReleaseComObject(this._adminManager);
				this._adminManager = null;
			}
			if (this._readManager != null)
			{
				Marshal.FinalReleaseComObject(this._readManager);
				this._readManager = null;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000055B0 File Offset: 0x000045B0
		private void ReleaseNativeChangeHandler()
		{
			if (this._nativeChangeHandler != null)
			{
				this.AdminManager.SetMetadata("changeHandler", null);
				this._nativeChangeHandler = null;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000055D4 File Offset: 0x000045D4
		public void RemoveLocationPath(string locationPath)
		{
			this.EnsureNotDisposed();
			IAppHostConfigLocationCollection locations = this.ConfigFile.Locations;
			try
			{
				uint count = locations.Count;
				for (uint num = 0U; num < count; num += 1U)
				{
					IAppHostConfigLocation appHostConfigLocation = locations[num];
					try
					{
						string path = appHostConfigLocation.Path;
						if (string.Equals(path, locationPath, StringComparison.OrdinalIgnoreCase))
						{
							locations.DeleteLocation(num);
							this.SetDirty();
							break;
						}
					}
					finally
					{
						if (appHostConfigLocation != null)
						{
							Marshal.FinalReleaseComObject(appHostConfigLocation);
						}
					}
				}
			}
			finally
			{
				if (locations != null)
				{
					Marshal.FinalReleaseComObject(locations);
				}
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005670 File Offset: 0x00004670
		public void RenameLocationPath(string locationPath, string newLocationPath)
		{
			this.EnsureNotDisposed();
			IAppHostConfigLocationCollection locations = this.ConfigFile.Locations;
			try
			{
				locations.RenameLocation(locationPath, newLocationPath);
				this.SetDirty();
			}
			finally
			{
				if (locations != null)
				{
					Marshal.FinalReleaseComObject(locations);
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000056BC File Offset: 0x000046BC
		internal void SetDirty()
		{
			if (this._hasBeenCommitted || this._configurationManager.Owner.ReadOnly)
			{
				throw new InvalidOperationException(Resources.ObjectHasBeenCommited);
			}
			this._isDirty = true;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000056EA File Offset: 0x000046EA
		public void SetMetadata(string metadataType, object value)
		{
			this.EnsureNotDisposed();
			this.AdminManager.SetMetadata(metadataType, value);
		}

		// Token: 0x04000048 RID: 72
		private const BindingFlags DefaultBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000049 RID: 73
		private IAppHostWritableAdminManager _adminManager;

		// Token: 0x0400004A RID: 74
		private IAppHostConfigFile _configFile;

		// Token: 0x0400004B RID: 75
		private string _configPathToEdit;

		// Token: 0x0400004C RID: 76
		private ConfigurationManager _configurationManager;

		// Token: 0x0400004D RID: 77
		private bool _isDirty;

		// Token: 0x0400004E RID: 78
		private bool _hasBeenCommitted;

		// Token: 0x0400004F RID: 79
		private Dictionary<string, IAppHostElement> _sectionTable;

		// Token: 0x04000050 RID: 80
		private SectionGroup _rootSectionGroup;

		// Token: 0x04000051 RID: 81
		private EventHandler _cacheInvalidated;

		// Token: 0x04000052 RID: 82
		private Configuration.NativeConfigurationChangeHandler _nativeChangeHandler;

		// Token: 0x04000053 RID: 83
		private IAppHostAdminManager _readManager;

		// Token: 0x04000054 RID: 84
		private static ConfigurationPermission s_unrestrictedConfigPermission;

		// Token: 0x0200001A RID: 26
		private class NativeConfigurationChangeHandler : IAppHostChangeHandler
		{
			// Token: 0x0600013F RID: 319 RVA: 0x000056FF File Offset: 0x000046FF
			public NativeConfigurationChangeHandler(Configuration configuration)
			{
				this._configuration = configuration;
			}

			// Token: 0x06000140 RID: 320 RVA: 0x0000570E File Offset: 0x0000470E
			public void OnSectionChanges(string bstrSectionName, string bstrConfigPath)
			{
				this._configuration.OnConfigurationChanged(EventArgs.Empty);
			}

			// Token: 0x04000055 RID: 85
			private Configuration _configuration;
		}
	}
}
