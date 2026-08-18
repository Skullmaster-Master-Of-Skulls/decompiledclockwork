using System;
using System.Collections;
using System.Configuration;
using System.Web.UI;

namespace System.Web.Configuration
{
	// Token: 0x0200071B RID: 1819
	[ConfigurationCollection(typeof(NamespaceInfo))]
	public sealed class NamespaceCollection : ConfigurationElementCollection
	{
		// Token: 0x0600578F RID: 22415 RVA: 0x00132F5E File Offset: 0x0013115E
		static NamespaceCollection()
		{
			NamespaceCollection._properties = new ConfigurationPropertyCollection();
			NamespaceCollection._properties.Add(NamespaceCollection._propAutoImportVBNamespace);
		}

		// Token: 0x17001941 RID: 6465
		// (get) Token: 0x06005790 RID: 22416 RVA: 0x00132F99 File Offset: 0x00131199
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return NamespaceCollection._properties;
			}
		}

		// Token: 0x17001942 RID: 6466
		// (get) Token: 0x06005791 RID: 22417 RVA: 0x00132FA0 File Offset: 0x001311A0
		// (set) Token: 0x06005792 RID: 22418 RVA: 0x00132FB2 File Offset: 0x001311B2
		[ConfigurationProperty("autoImportVBNamespace", DefaultValue = true)]
		public bool AutoImportVBNamespace
		{
			get
			{
				return (bool)base[NamespaceCollection._propAutoImportVBNamespace];
			}
			set
			{
				base[NamespaceCollection._propAutoImportVBNamespace] = value;
			}
		}

		// Token: 0x17001943 RID: 6467
		public NamespaceInfo this[int index]
		{
			get
			{
				return (NamespaceInfo)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
				this._namespaceEntries = null;
			}
		}

		// Token: 0x06005795 RID: 22421 RVA: 0x00132FF4 File Offset: 0x001311F4
		public void Add(NamespaceInfo namespaceInformation)
		{
			this.BaseAdd(namespaceInformation);
			this._namespaceEntries = null;
		}

		// Token: 0x06005796 RID: 22422 RVA: 0x00133004 File Offset: 0x00131204
		public void Remove(string s)
		{
			base.BaseRemove(s);
			this._namespaceEntries = null;
		}

		// Token: 0x06005797 RID: 22423 RVA: 0x00133014 File Offset: 0x00131214
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
			this._namespaceEntries = null;
		}

		// Token: 0x06005798 RID: 22424 RVA: 0x00133024 File Offset: 0x00131224
		protected override ConfigurationElement CreateNewElement()
		{
			return new NamespaceInfo();
		}

		// Token: 0x06005799 RID: 22425 RVA: 0x0013302B File Offset: 0x0013122B
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((NamespaceInfo)element).Namespace;
		}

		// Token: 0x0600579A RID: 22426 RVA: 0x00133038 File Offset: 0x00131238
		public void Clear()
		{
			base.BaseClear();
			this._namespaceEntries = null;
		}

		// Token: 0x17001944 RID: 6468
		// (get) Token: 0x0600579B RID: 22427 RVA: 0x00133048 File Offset: 0x00131248
		internal Hashtable NamespaceEntries
		{
			get
			{
				if (this._namespaceEntries == null)
				{
					lock (this)
					{
						if (this._namespaceEntries == null)
						{
							this._namespaceEntries = new Hashtable(StringComparer.OrdinalIgnoreCase);
							foreach (object obj in this)
							{
								NamespaceInfo namespaceInfo = (NamespaceInfo)obj;
								NamespaceEntry namespaceEntry = new NamespaceEntry();
								namespaceEntry.Namespace = namespaceInfo.Namespace;
								namespaceEntry.Line = namespaceInfo.ElementInformation.Properties["namespace"].LineNumber;
								namespaceEntry.VirtualPath = namespaceInfo.ElementInformation.Properties["namespace"].Source;
								if (namespaceEntry.Line == 0)
								{
									namespaceEntry.Line = 1;
								}
								this._namespaceEntries[namespaceInfo.Namespace] = namespaceEntry;
							}
						}
					}
				}
				return this._namespaceEntries;
			}
		}

		// Token: 0x04002E93 RID: 11923
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002E94 RID: 11924
		private static readonly ConfigurationProperty _propAutoImportVBNamespace = new ConfigurationProperty("autoImportVBNamespace", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002E95 RID: 11925
		private Hashtable _namespaceEntries;
	}
}
