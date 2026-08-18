using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Compilation;

namespace System.Web.Configuration
{
	// Token: 0x020006DC RID: 1756
	[ConfigurationCollection(typeof(FolderLevelBuildProvider))]
	public sealed class FolderLevelBuildProviderCollection : ConfigurationElementCollection
	{
		// Token: 0x06005476 RID: 21622 RVA: 0x001240D1 File Offset: 0x001222D1
		public FolderLevelBuildProviderCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x06005477 RID: 21623 RVA: 0x00127E84 File Offset: 0x00126084
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FolderLevelBuildProviderCollection._properties;
			}
		}

		// Token: 0x17001813 RID: 6163
		public BuildProvider this[string name]
		{
			get
			{
				return (BuildProvider)base.BaseGet(name);
			}
		}

		// Token: 0x17001814 RID: 6164
		public FolderLevelBuildProvider this[int index]
		{
			get
			{
				return (FolderLevelBuildProvider)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x0600547B RID: 21627 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(FolderLevelBuildProvider buildProvider)
		{
			this.BaseAdd(buildProvider);
		}

		// Token: 0x0600547C RID: 21628 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x0600547F RID: 21631 RVA: 0x00127E99 File Offset: 0x00126099
		protected override ConfigurationElement CreateNewElement()
		{
			return new FolderLevelBuildProvider();
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x00127EA0 File Offset: 0x001260A0
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((FolderLevelBuildProvider)element).Name;
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x00127EB0 File Offset: 0x001260B0
		private void AddMapping(FolderLevelBuildProviderAppliesTo appliesTo, Type buildProviderType)
		{
			if (this._buildProviderMappings == null)
			{
				this._buildProviderMappings = new Dictionary<FolderLevelBuildProviderAppliesTo, List<Type>>();
			}
			if (this._buildProviderTypes == null)
			{
				this._buildProviderTypes = new HashSet<Type>();
			}
			List<Type> list = null;
			if (!this._buildProviderMappings.TryGetValue(appliesTo, out list))
			{
				list = new List<Type>();
				this._buildProviderMappings.Add(appliesTo, list);
			}
			list.Add(buildProviderType);
			this._buildProviderTypes.Add(buildProviderType);
		}

		// Token: 0x06005482 RID: 21634 RVA: 0x00127F1C File Offset: 0x0012611C
		internal List<Type> GetBuildProviderTypes(FolderLevelBuildProviderAppliesTo appliesTo)
		{
			this.EnsureFolderLevelBuildProvidersInitialized();
			List<Type> list = new List<Type>();
			if (this._buildProviderMappings != null)
			{
				foreach (KeyValuePair<FolderLevelBuildProviderAppliesTo, List<Type>> keyValuePair in this._buildProviderMappings)
				{
					if ((keyValuePair.Key & appliesTo) != FolderLevelBuildProviderAppliesTo.None)
					{
						list.AddRange(keyValuePair.Value);
					}
				}
			}
			return list;
		}

		// Token: 0x06005483 RID: 21635 RVA: 0x00127F98 File Offset: 0x00126198
		internal bool IsFolderLevelBuildProvider(Type t)
		{
			this.EnsureFolderLevelBuildProvidersInitialized();
			return this._buildProviderTypes != null && this._buildProviderTypes.Contains(t);
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x00127FB8 File Offset: 0x001261B8
		private void EnsureFolderLevelBuildProvidersInitialized()
		{
			if (!this._folderLevelBuildProviderTypesSet)
			{
				lock (this)
				{
					if (!this._folderLevelBuildProviderTypesSet)
					{
						foreach (object obj in this)
						{
							FolderLevelBuildProvider folderLevelBuildProvider = (FolderLevelBuildProvider)obj;
							this.AddMapping(folderLevelBuildProvider.AppliesToInternal, folderLevelBuildProvider.TypeInternal);
						}
						this._folderLevelBuildProviderTypesSet = true;
					}
				}
			}
		}

		// Token: 0x04002C56 RID: 11350
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04002C57 RID: 11351
		private Dictionary<FolderLevelBuildProviderAppliesTo, List<Type>> _buildProviderMappings;

		// Token: 0x04002C58 RID: 11352
		private HashSet<Type> _buildProviderTypes;

		// Token: 0x04002C59 RID: 11353
		private bool _folderLevelBuildProviderTypesSet;
	}
}
