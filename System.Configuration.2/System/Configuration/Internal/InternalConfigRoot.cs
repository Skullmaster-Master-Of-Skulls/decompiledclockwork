using System;
using System.Threading;

namespace System.Configuration.Internal
{
	// Token: 0x020000BD RID: 189
	internal sealed class InternalConfigRoot : IInternalConfigRoot
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000783 RID: 1923 RVA: 0x0001FE54 File Offset: 0x0001E054
		// (remove) Token: 0x06000784 RID: 1924 RVA: 0x0001FE8C File Offset: 0x0001E08C
		public event InternalConfigEventHandler ConfigChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000785 RID: 1925 RVA: 0x0001FEC4 File Offset: 0x0001E0C4
		// (remove) Token: 0x06000786 RID: 1926 RVA: 0x0001FEFC File Offset: 0x0001E0FC
		public event InternalConfigEventHandler ConfigRemoved;

		// Token: 0x06000787 RID: 1927 RVA: 0x000115BE File Offset: 0x0000F7BE
		internal InternalConfigRoot()
		{
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001FF31 File Offset: 0x0001E131
		internal InternalConfigRoot(Configuration currentConfiguration)
		{
			this._CurrentConfiguration = currentConfiguration;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001FF40 File Offset: 0x0001E140
		void IInternalConfigRoot.Init(IInternalConfigHost host, bool isDesignTime)
		{
			this._host = host;
			this._configBuilderHost = (host as IInternalConfigurationBuilderHost);
			this._isDesignTime = isDesignTime;
			this._hierarchyLock = new ReaderWriterLock();
			if (this._isDesignTime)
			{
				this._rootConfigRecord = MgmtConfigurationRecord.Create(this, null, string.Empty, null);
				return;
			}
			this._rootConfigRecord = (BaseConfigurationRecord)RuntimeConfigurationRecord.Create(this, null, string.Empty);
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0001FFA5 File Offset: 0x0001E1A5
		internal IInternalConfigHost Host
		{
			get
			{
				return this._host;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001FFAD File Offset: 0x0001E1AD
		internal IInternalConfigurationBuilderHost ConfigBuilderHost
		{
			get
			{
				return this._configBuilderHost;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0001FFB5 File Offset: 0x0001E1B5
		internal BaseConfigurationRecord RootConfigRecord
		{
			get
			{
				return this._rootConfigRecord;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x0001FFBD File Offset: 0x0001E1BD
		bool IInternalConfigRoot.IsDesignTime
		{
			get
			{
				return this._isDesignTime;
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001FFC5 File Offset: 0x0001E1C5
		private void AcquireHierarchyLockForRead()
		{
			if (this._hierarchyLock.IsReaderLockHeld)
			{
				throw ExceptionUtil.UnexpectedError("System.Configuration.Internal.InternalConfigRoot::AcquireHierarchyLockForRead - reader lock already held by this thread");
			}
			if (this._hierarchyLock.IsWriterLockHeld)
			{
				throw ExceptionUtil.UnexpectedError("System.Configuration.Internal.InternalConfigRoot::AcquireHierarchyLockForRead - writer lock already held by this thread");
			}
			this._hierarchyLock.AcquireReaderLock(-1);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00020003 File Offset: 0x0001E203
		private void ReleaseHierarchyLockForRead()
		{
			if (this._hierarchyLock.IsReaderLockHeld)
			{
				this._hierarchyLock.ReleaseReaderLock();
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0002001D File Offset: 0x0001E21D
		private void AcquireHierarchyLockForWrite()
		{
			if (this._hierarchyLock.IsReaderLockHeld)
			{
				throw ExceptionUtil.UnexpectedError("System.Configuration.Internal.InternalConfigRoot::AcquireHierarchyLockForWrite - reader lock already held by this thread");
			}
			if (this._hierarchyLock.IsWriterLockHeld)
			{
				throw ExceptionUtil.UnexpectedError("System.Configuration.Internal.InternalConfigRoot::AcquireHierarchyLockForWrite - writer lock already held by this thread");
			}
			this._hierarchyLock.AcquireWriterLock(-1);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0002005B File Offset: 0x0001E25B
		private void ReleaseHierarchyLockForWrite()
		{
			if (this._hierarchyLock.IsWriterLockHeld)
			{
				this._hierarchyLock.ReleaseWriterLock();
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00020078 File Offset: 0x0001E278
		private void hlFindConfigRecord(string[] parts, out int nextIndex, out BaseConfigurationRecord currentRecord)
		{
			currentRecord = this._rootConfigRecord;
			for (nextIndex = 0; nextIndex < parts.Length; nextIndex++)
			{
				BaseConfigurationRecord baseConfigurationRecord = currentRecord.hlGetChild(parts[nextIndex]);
				if (baseConfigurationRecord == null)
				{
					break;
				}
				currentRecord = baseConfigurationRecord;
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000200B4 File Offset: 0x0001E2B4
		public object GetSection(string section, string configPath)
		{
			BaseConfigurationRecord baseConfigurationRecord = (BaseConfigurationRecord)this.GetUniqueConfigRecord(configPath);
			return baseConfigurationRecord.GetSection(section);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000200D8 File Offset: 0x0001E2D8
		public string GetUniqueConfigPath(string configPath)
		{
			IInternalConfigRecord uniqueConfigRecord = this.GetUniqueConfigRecord(configPath);
			if (uniqueConfigRecord == null)
			{
				return null;
			}
			return uniqueConfigRecord.ConfigPath;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000200F8 File Offset: 0x0001E2F8
		public IInternalConfigRecord GetUniqueConfigRecord(string configPath)
		{
			BaseConfigurationRecord baseConfigurationRecord = (BaseConfigurationRecord)this.GetConfigRecord(configPath);
			while (baseConfigurationRecord.IsEmpty)
			{
				BaseConfigurationRecord parent = baseConfigurationRecord.Parent;
				if (parent.IsRootConfig)
				{
					break;
				}
				baseConfigurationRecord = parent;
			}
			return baseConfigurationRecord;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00020130 File Offset: 0x0001E330
		public IInternalConfigRecord GetConfigRecord(string configPath)
		{
			if (!ConfigPathUtility.IsValid(configPath))
			{
				throw ExceptionUtil.ParameterInvalid("configPath");
			}
			string[] parts = ConfigPathUtility.GetParts(configPath);
			try
			{
				this.AcquireHierarchyLockForRead();
				int num;
				BaseConfigurationRecord baseConfigurationRecord;
				this.hlFindConfigRecord(parts, out num, out baseConfigurationRecord);
				if (num == parts.Length || !baseConfigurationRecord.hlNeedsChildFor(parts[num]))
				{
					return baseConfigurationRecord;
				}
			}
			finally
			{
				this.ReleaseHierarchyLockForRead();
			}
			IInternalConfigRecord result;
			try
			{
				this.AcquireHierarchyLockForWrite();
				int num2;
				BaseConfigurationRecord baseConfigurationRecord2;
				this.hlFindConfigRecord(parts, out num2, out baseConfigurationRecord2);
				if (num2 == parts.Length)
				{
					result = baseConfigurationRecord2;
				}
				else
				{
					string text = string.Join("/", parts, 0, num2);
					while (num2 < parts.Length && baseConfigurationRecord2.hlNeedsChildFor(parts[num2]))
					{
						string text2 = parts[num2];
						text = ConfigPathUtility.Combine(text, text2);
						BaseConfigurationRecord baseConfigurationRecord3;
						if (this._isDesignTime)
						{
							baseConfigurationRecord3 = MgmtConfigurationRecord.Create(this, baseConfigurationRecord2, text, null);
						}
						else
						{
							baseConfigurationRecord3 = (BaseConfigurationRecord)RuntimeConfigurationRecord.Create(this, baseConfigurationRecord2, text);
						}
						baseConfigurationRecord2.hlAddChild(text2, baseConfigurationRecord3);
						num2++;
						baseConfigurationRecord2 = baseConfigurationRecord3;
					}
					result = baseConfigurationRecord2;
				}
			}
			finally
			{
				this.ReleaseHierarchyLockForWrite();
			}
			return result;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00020248 File Offset: 0x0001E448
		private void RemoveConfigImpl(string configPath, BaseConfigurationRecord configRecord)
		{
			if (!ConfigPathUtility.IsValid(configPath))
			{
				throw ExceptionUtil.ParameterInvalid("configPath");
			}
			string[] parts = ConfigPathUtility.GetParts(configPath);
			BaseConfigurationRecord baseConfigurationRecord;
			try
			{
				this.AcquireHierarchyLockForWrite();
				int num;
				this.hlFindConfigRecord(parts, out num, out baseConfigurationRecord);
				if (num != parts.Length || (configRecord != null && configRecord != baseConfigurationRecord))
				{
					return;
				}
				baseConfigurationRecord.Parent.hlRemoveChild(parts[parts.Length - 1]);
			}
			finally
			{
				this.ReleaseHierarchyLockForWrite();
			}
			this.OnConfigRemoved(new InternalConfigEventArgs(configPath));
			baseConfigurationRecord.CloseRecursive();
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x000202CC File Offset: 0x0001E4CC
		public void RemoveConfig(string configPath)
		{
			this.RemoveConfigImpl(configPath, null);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x000202D6 File Offset: 0x0001E4D6
		public void RemoveConfigRecord(BaseConfigurationRecord configRecord)
		{
			this.RemoveConfigImpl(configRecord.ConfigPath, configRecord);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000202E8 File Offset: 0x0001E4E8
		public void ClearResult(BaseConfigurationRecord configRecord, string configKey, bool forceEvaluation)
		{
			string[] parts = ConfigPathUtility.GetParts(configRecord.ConfigPath);
			try
			{
				this.AcquireHierarchyLockForWrite();
				int num;
				BaseConfigurationRecord baseConfigurationRecord;
				this.hlFindConfigRecord(parts, out num, out baseConfigurationRecord);
				if (num == parts.Length && configRecord == baseConfigurationRecord)
				{
					baseConfigurationRecord.hlClearResultRecursive(configKey, forceEvaluation);
				}
			}
			finally
			{
				this.ReleaseHierarchyLockForWrite();
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00020340 File Offset: 0x0001E540
		private void OnConfigRemoved(InternalConfigEventArgs e)
		{
			InternalConfigEventHandler configRemoved = this.ConfigRemoved;
			if (configRemoved != null)
			{
				configRemoved(this, e);
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0002035F File Offset: 0x0001E55F
		internal void FireConfigChanged(string configPath)
		{
			this.OnConfigChanged(new InternalConfigEventArgs(configPath));
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00020370 File Offset: 0x0001E570
		private void OnConfigChanged(InternalConfigEventArgs e)
		{
			InternalConfigEventHandler configChanged = this.ConfigChanged;
			if (configChanged != null)
			{
				configChanged(this, e);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0002038F File Offset: 0x0001E58F
		internal Configuration CurrentConfiguration
		{
			get
			{
				return this._CurrentConfiguration;
			}
		}

		// Token: 0x04000457 RID: 1111
		private IInternalConfigHost _host;

		// Token: 0x04000458 RID: 1112
		private IInternalConfigurationBuilderHost _configBuilderHost;

		// Token: 0x04000459 RID: 1113
		private ReaderWriterLock _hierarchyLock;

		// Token: 0x0400045A RID: 1114
		private BaseConfigurationRecord _rootConfigRecord;

		// Token: 0x0400045B RID: 1115
		private bool _isDesignTime;

		// Token: 0x0400045C RID: 1116
		private Configuration _CurrentConfiguration;
	}
}
