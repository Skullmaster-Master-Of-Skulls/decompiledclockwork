using System;
using System.Collections;
using System.Diagnostics;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000025 RID: 37
	[DebuggerDisplay("Count = {Count}")]
	public abstract class ConfigurationElementCollection : ConfigurationElement, ICollection, IEnumerable
	{
		// Token: 0x0600019F RID: 415 RVA: 0x0000D0D3 File Offset: 0x0000B2D3
		protected ConfigurationElementCollection()
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000D114 File Offset: 0x0000B314
		protected ConfigurationElementCollection(IComparer comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			this._comparer = comparer;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000D173 File Offset: 0x0000B373
		private ArrayList Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000D17B File Offset: 0x0000B37B
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000D183 File Offset: 0x0000B383
		protected internal string AddElementName
		{
			get
			{
				return this._addElement;
			}
			set
			{
				this._addElement = value;
				if (BaseConfigurationRecord.IsReservedAttributeName(value))
				{
					throw new ArgumentException(SR.GetString("Item_name_reserved", new object[]
					{
						"add",
						value
					}));
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000D1B6 File Offset: 0x0000B3B6
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000D1BE File Offset: 0x0000B3BE
		protected internal string RemoveElementName
		{
			get
			{
				return this._removeElement;
			}
			set
			{
				if (BaseConfigurationRecord.IsReservedAttributeName(value))
				{
					throw new ArgumentException(SR.GetString("Item_name_reserved", new object[]
					{
						"remove",
						value
					}));
				}
				this._removeElement = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000D1F1 File Offset: 0x0000B3F1
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000D1F9 File Offset: 0x0000B3F9
		protected internal string ClearElementName
		{
			get
			{
				return this._clearElement;
			}
			set
			{
				if (BaseConfigurationRecord.IsReservedAttributeName(value))
				{
					throw new ArgumentException(SR.GetString("Item_name_reserved", new object[]
					{
						"clear",
						value
					}));
				}
				this._clearElement = value;
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000D22C File Offset: 0x0000B42C
		internal override void AssociateContext(BaseConfigurationRecord configRecord)
		{
			base.AssociateContext(configRecord);
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				if (entry._value != null)
				{
					entry._value.AssociateContext(configRecord);
				}
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000D29C File Offset: 0x0000B49C
		protected internal override bool IsModified()
		{
			if (this.bModified)
			{
				return true;
			}
			if (base.IsModified())
			{
				return true;
			}
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				if (entry._entryType != ConfigurationElementCollection.EntryType.Removed)
				{
					ConfigurationElement value = entry._value;
					if (value.IsModified())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000D324 File Offset: 0x0000B524
		protected internal override void ResetModified()
		{
			this.bModified = false;
			base.ResetModified();
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				if (entry._entryType != ConfigurationElementCollection.EntryType.Removed)
				{
					ConfigurationElement value = entry._value;
					value.ResetModified();
				}
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000D39C File Offset: 0x0000B59C
		public override bool IsReadOnly()
		{
			return this.bReadOnly;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		protected internal override void SetReadOnly()
		{
			this.bReadOnly = true;
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				if (entry._entryType != ConfigurationElementCollection.EntryType.Removed)
				{
					ConfigurationElement value = entry._value;
					value.SetReadOnly();
				}
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000D414 File Offset: 0x0000B614
		internal virtual IEnumerator GetEnumeratorImpl()
		{
			return new ConfigurationElementCollection.Enumerator(this._items, this);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000D414 File Offset: 0x0000B614
		internal IEnumerator GetElementsEnumerator()
		{
			return new ConfigurationElementCollection.Enumerator(this._items, this);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000D424 File Offset: 0x0000B624
		public override bool Equals(object compareTo)
		{
			if (!(compareTo.GetType() == base.GetType()))
			{
				return false;
			}
			ConfigurationElementCollection configurationElementCollection = (ConfigurationElementCollection)compareTo;
			if (this.Count != configurationElementCollection.Count)
			{
				return false;
			}
			foreach (object obj in this.Items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				bool flag = false;
				foreach (object obj2 in configurationElementCollection.Items)
				{
					ConfigurationElementCollection.Entry entry2 = (ConfigurationElementCollection.Entry)obj2;
					if (object.Equals(entry._value, entry2._value))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000D518 File Offset: 0x0000B718
		public override int GetHashCode()
		{
			int num = 0;
			foreach (object obj in this.Items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				ConfigurationElement value = entry._value;
				num ^= value.GetHashCode();
			}
			return num;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000D580 File Offset: 0x0000B780
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			if (sourceElement != null)
			{
				ConfigurationElementCollection configurationElementCollection = parentElement as ConfigurationElementCollection;
				ConfigurationElementCollection configurationElementCollection2 = sourceElement as ConfigurationElementCollection;
				Hashtable hashtable = new Hashtable();
				this._lockedAllExceptAttributesList = sourceElement._lockedAllExceptAttributesList;
				this._lockedAllExceptElementsList = sourceElement._lockedAllExceptElementsList;
				this._fItemLocked = sourceElement._fItemLocked;
				this._lockedAttributesList = sourceElement._lockedAttributesList;
				this._lockedElementsList = sourceElement._lockedElementsList;
				this.AssociateContext(sourceElement._configRecord);
				if (parentElement != null)
				{
					if (parentElement._lockedAttributesList != null)
					{
						this._lockedAttributesList = base.UnMergeLockList(sourceElement._lockedAttributesList, parentElement._lockedAttributesList, saveMode);
					}
					if (parentElement._lockedElementsList != null)
					{
						this._lockedElementsList = base.UnMergeLockList(sourceElement._lockedElementsList, parentElement._lockedElementsList, saveMode);
					}
					if (parentElement._lockedAllExceptAttributesList != null)
					{
						this._lockedAllExceptAttributesList = base.UnMergeLockList(sourceElement._lockedAllExceptAttributesList, parentElement._lockedAllExceptAttributesList, saveMode);
					}
					if (parentElement._lockedAllExceptElementsList != null)
					{
						this._lockedAllExceptElementsList = base.UnMergeLockList(sourceElement._lockedAllExceptElementsList, parentElement._lockedAllExceptElementsList, saveMode);
					}
				}
				if (this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMap || this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
				{
					this.bCollectionCleared = configurationElementCollection2.bCollectionCleared;
					this.EmitClear = ((saveMode == ConfigurationSaveMode.Full && this._clearElement.Length != 0) || (saveMode == ConfigurationSaveMode.Modified && this.bCollectionCleared) || configurationElementCollection2.EmitClear);
					if (configurationElementCollection != null && !this.EmitClear)
					{
						foreach (object obj in configurationElementCollection.Items)
						{
							ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
							if (entry._entryType != ConfigurationElementCollection.EntryType.Removed)
							{
								hashtable[entry.GetKey(this)] = ConfigurationElementCollection.InheritedType.inParent;
							}
						}
					}
					foreach (object obj2 in configurationElementCollection2.Items)
					{
						ConfigurationElementCollection.Entry entry2 = (ConfigurationElementCollection.Entry)obj2;
						if (entry2._entryType != ConfigurationElementCollection.EntryType.Removed)
						{
							if (hashtable.Contains(entry2.GetKey(this)))
							{
								ConfigurationElementCollection.Entry entry3 = (ConfigurationElementCollection.Entry)configurationElementCollection.Items[configurationElementCollection.RealIndexOf(entry2._value)];
								ConfigurationElement value = entry2._value;
								if (value.Equals(entry3._value))
								{
									hashtable[entry2.GetKey(this)] = ConfigurationElementCollection.InheritedType.inBothSame;
									if (saveMode == ConfigurationSaveMode.Modified)
									{
										if (value.IsModified())
										{
											hashtable[entry2.GetKey(this)] = ConfigurationElementCollection.InheritedType.inBothDiff;
										}
										else if (value.ElementPresent)
										{
											hashtable[entry2.GetKey(this)] = ConfigurationElementCollection.InheritedType.inBothCopyNoRemove;
										}
									}
								}
								else
								{
									hashtable[entry2.GetKey(this)] = ConfigurationElementCollection.InheritedType.inBothDiff;
									if (this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate && entry2._entryType == ConfigurationElementCollection.EntryType.Added)
									{
										hashtable[entry2.GetKey(this)] = ConfigurationElementCollection.InheritedType.inBothCopyNoRemove;
									}
								}
							}
							else
							{
								hashtable[entry2.GetKey(this)] = ConfigurationElementCollection.InheritedType.inSelf;
							}
						}
					}
					if (configurationElementCollection != null && !this.EmitClear)
					{
						foreach (object obj3 in configurationElementCollection.Items)
						{
							ConfigurationElementCollection.Entry entry4 = (ConfigurationElementCollection.Entry)obj3;
							if (entry4._entryType != ConfigurationElementCollection.EntryType.Removed)
							{
								ConfigurationElementCollection.InheritedType inheritedType = (ConfigurationElementCollection.InheritedType)hashtable[entry4.GetKey(this)];
								if (inheritedType == ConfigurationElementCollection.InheritedType.inParent || inheritedType == ConfigurationElementCollection.InheritedType.inBothDiff)
								{
									ConfigurationElement configurationElement = this.CallCreateNewElement(entry4.GetKey(this).ToString());
									configurationElement.Reset(entry4._value);
									this.BaseAdd(configurationElement, this.ThrowOnDuplicate, true);
									this.BaseRemove(entry4.GetKey(this), false);
								}
							}
						}
					}
					using (IEnumerator enumerator4 = configurationElementCollection2.Items.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							object obj4 = enumerator4.Current;
							ConfigurationElementCollection.Entry entry5 = (ConfigurationElementCollection.Entry)obj4;
							if (entry5._entryType != ConfigurationElementCollection.EntryType.Removed)
							{
								ConfigurationElementCollection.InheritedType inheritedType2 = (ConfigurationElementCollection.InheritedType)hashtable[entry5.GetKey(this)];
								if (inheritedType2 == ConfigurationElementCollection.InheritedType.inSelf || inheritedType2 == ConfigurationElementCollection.InheritedType.inBothDiff || inheritedType2 == ConfigurationElementCollection.InheritedType.inBothCopyNoRemove)
								{
									ConfigurationElement configurationElement2 = this.CallCreateNewElement(entry5.GetKey(this).ToString());
									configurationElement2.Unmerge(entry5._value, null, saveMode);
									if (inheritedType2 == ConfigurationElementCollection.InheritedType.inSelf)
									{
										configurationElement2.RemoveAllInheritedLocks();
									}
									this.BaseAdd(configurationElement2, this.ThrowOnDuplicate, true);
								}
							}
						}
						return;
					}
				}
				if (this.CollectionType == ConfigurationElementCollectionType.BasicMap || this.CollectionType == ConfigurationElementCollectionType.BasicMapAlternate)
				{
					foreach (object obj5 in configurationElementCollection2.Items)
					{
						ConfigurationElementCollection.Entry entry6 = (ConfigurationElementCollection.Entry)obj5;
						bool flag = false;
						ConfigurationElementCollection.Entry entry7 = null;
						if (entry6._entryType == ConfigurationElementCollection.EntryType.Added || entry6._entryType == ConfigurationElementCollection.EntryType.Replaced)
						{
							bool flag2 = false;
							if (configurationElementCollection != null)
							{
								foreach (object obj6 in configurationElementCollection.Items)
								{
									ConfigurationElementCollection.Entry entry8 = (ConfigurationElementCollection.Entry)obj6;
									if (object.Equals(entry6.GetKey(this), entry8.GetKey(this)) && !this.IsElementName(entry6.GetKey(this).ToString()))
									{
										flag = true;
										entry7 = entry8;
									}
									if (object.Equals(entry6._value, entry8._value))
									{
										flag = true;
										flag2 = true;
										entry7 = entry8;
										break;
									}
								}
							}
							ConfigurationElement configurationElement3 = this.CallCreateNewElement(entry6.GetKey(this).ToString());
							if (!flag)
							{
								configurationElement3.Unmerge(entry6._value, null, saveMode);
								this.BaseAdd(-1, configurationElement3, true);
							}
							else
							{
								ConfigurationElement value2 = entry6._value;
								if (!flag2 || (saveMode == ConfigurationSaveMode.Modified && value2.IsModified()) || saveMode == ConfigurationSaveMode.Full)
								{
									configurationElement3.Unmerge(entry6._value, entry7._value, saveMode);
									this.BaseAdd(-1, configurationElement3, true);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			ConfigurationElementCollection configurationElementCollection = parentElement as ConfigurationElementCollection;
			base.ResetLockLists(parentElement);
			if (configurationElementCollection != null)
			{
				foreach (object obj in configurationElementCollection.Items)
				{
					ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
					ConfigurationElement configurationElement = this.CallCreateNewElement(entry.GetKey(this).ToString());
					configurationElement.Reset(entry._value);
					if ((this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMap || this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate) && (entry._entryType == ConfigurationElementCollection.EntryType.Added || entry._entryType == ConfigurationElementCollection.EntryType.Replaced))
					{
						this.BaseAdd(configurationElement, true, true);
					}
					else if (this.CollectionType == ConfigurationElementCollectionType.BasicMap || this.CollectionType == ConfigurationElementCollectionType.BasicMapAlternate)
					{
						this.BaseAdd(-1, configurationElement, true);
					}
				}
				this._inheritedCount = this.Count;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		public int Count
		{
			get
			{
				return this._items.Count - this._removedItemCount;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		public bool EmitClear
		{
			get
			{
				return this.bEmitClearTag;
			}
			set
			{
				if (this.IsReadOnly())
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_base_read_only"));
				}
				if (value)
				{
					base.CheckLockedElement(this._clearElement, null);
					base.CheckLockedElement(this._removeElement, null);
				}
				this.bModified = true;
				this.bEmitClearTag = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00008751 File Offset: 0x00006951
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000088C2 File Offset: 0x00006AC2
		public object SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public void CopyTo(ConfigurationElement[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		void ICollection.CopyTo(Array arr, int index)
		{
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				if (entry._entryType != ConfigurationElementCollection.EntryType.Removed)
				{
					arr.SetValue(entry._value, index++);
				}
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		public IEnumerator GetEnumerator()
		{
			return this.GetEnumeratorImpl();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000DDC4 File Offset: 0x0000BFC4
		protected virtual void BaseAdd(ConfigurationElement element)
		{
			this.BaseAdd(element, this.ThrowOnDuplicate);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000DDD3 File Offset: 0x0000BFD3
		protected internal void BaseAdd(ConfigurationElement element, bool throwIfExists)
		{
			this.BaseAdd(element, throwIfExists, false);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		private void BaseAdd(ConfigurationElement element, bool throwIfExists, bool ignoreLocks)
		{
			bool flagAsReplaced = false;
			bool flag = this.internalAddToEnd;
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_read_only"));
			}
			if (base.LockItem && !ignoreLocks)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_element_locked", new object[]
				{
					this._addElement
				}));
			}
			object elementKeyInternal = this.GetElementKeyInternal(element);
			int num = -1;
			int i = 0;
			while (i < this._items.Count)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)this._items[i];
				if (this.CompareKeys(elementKeyInternal, entry.GetKey(this)))
				{
					if (entry._value != null && entry._value.LockItem && !ignoreLocks)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_base_collection_item_locked"));
					}
					if (entry._entryType != ConfigurationElementCollection.EntryType.Removed && throwIfExists)
					{
						if (!element.Equals(entry._value))
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_base_collection_entry_already_exists", new object[]
							{
								elementKeyInternal
							}), element.PropertyFileName(""), element.PropertyLineNumber(""));
						}
						entry._value = element;
						return;
					}
					else
					{
						if (entry._entryType != ConfigurationElementCollection.EntryType.Added)
						{
							if ((this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMap || this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate) && entry._entryType == ConfigurationElementCollection.EntryType.Removed && this._removedItemCount > 0)
							{
								this._removedItemCount--;
							}
							entry._entryType = ConfigurationElementCollection.EntryType.Replaced;
							flagAsReplaced = true;
						}
						if (!flag && this.CollectionType != ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
						{
							if (!ignoreLocks)
							{
								element.HandleLockedAttributes(entry._value);
								element.MergeLocks(entry._value);
							}
							entry._value = element;
							this.bModified = true;
							return;
						}
						num = i;
						if (entry._entryType == ConfigurationElementCollection.EntryType.Added)
						{
							flag = true;
							break;
						}
						break;
					}
				}
				else
				{
					i++;
				}
			}
			if (num >= 0)
			{
				this._items.RemoveAt(num);
				if (this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate && num > this.Count + this._removedItemCount - this._inheritedCount)
				{
					this._inheritedCount--;
				}
			}
			this.BaseAddInternal(flag ? -1 : num, element, flagAsReplaced, ignoreLocks);
			this.bModified = true;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000DFF4 File Offset: 0x0000C1F4
		protected int BaseIndexOf(ConfigurationElement element)
		{
			int num = 0;
			object elementKeyInternal = this.GetElementKeyInternal(element);
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				if (entry._entryType != ConfigurationElementCollection.EntryType.Removed)
				{
					if (this.CompareKeys(elementKeyInternal, entry.GetKey(this)))
					{
						return num;
					}
					num++;
				}
			}
			return -1;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000E07C File Offset: 0x0000C27C
		internal int RealIndexOf(ConfigurationElement element)
		{
			int num = 0;
			object elementKeyInternal = this.GetElementKeyInternal(element);
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				if (this.CompareKeys(elementKeyInternal, entry.GetKey(this)))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000E0F8 File Offset: 0x0000C2F8
		private void BaseAddInternal(int index, ConfigurationElement element, bool flagAsReplaced, bool ignoreLocks)
		{
			element.AssociateContext(this._configRecord);
			if (element != null)
			{
				element.CallInit();
			}
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_read_only"));
			}
			if (!ignoreLocks)
			{
				if (this.CollectionType == ConfigurationElementCollectionType.BasicMap || this.CollectionType == ConfigurationElementCollectionType.BasicMapAlternate)
				{
					if (BaseConfigurationRecord.IsReservedAttributeName(this.ElementName))
					{
						throw new ArgumentException(SR.GetString("Basicmap_item_name_reserved", new object[]
						{
							this.ElementName
						}));
					}
					base.CheckLockedElement(this.ElementName, null);
				}
				if (this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMap || this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
				{
					base.CheckLockedElement(this._addElement, null);
				}
			}
			if (this.CollectionType == ConfigurationElementCollectionType.BasicMapAlternate || this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
			{
				if (index == -1)
				{
					index = this.Count + this._removedItemCount - this._inheritedCount;
				}
				else if (index > this.Count + this._removedItemCount - this._inheritedCount && !flagAsReplaced)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_base_cannot_add_items_below_inherited_items"));
				}
			}
			if (this.CollectionType == ConfigurationElementCollectionType.BasicMap && index >= 0 && index < this._inheritedCount)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_cannot_add_items_above_inherited_items"));
			}
			ConfigurationElementCollection.EntryType type = (!flagAsReplaced) ? ConfigurationElementCollection.EntryType.Added : ConfigurationElementCollection.EntryType.Replaced;
			object elementKeyInternal = this.GetElementKeyInternal(element);
			if (index >= 0)
			{
				if (index > this._items.Count)
				{
					throw new ConfigurationErrorsException(SR.GetString("IndexOutOfRange", new object[]
					{
						index
					}));
				}
				this._items.Insert(index, new ConfigurationElementCollection.Entry(type, elementKeyInternal, element));
			}
			else
			{
				this._items.Add(new ConfigurationElementCollection.Entry(type, elementKeyInternal, element));
			}
			this.bModified = true;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000E28E File Offset: 0x0000C48E
		protected virtual void BaseAdd(int index, ConfigurationElement element)
		{
			this.BaseAdd(index, element, false);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000E29C File Offset: 0x0000C49C
		private void BaseAdd(int index, ConfigurationElement element, bool ignoreLocks)
		{
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_read_only"));
			}
			if (index < -1)
			{
				throw new ConfigurationErrorsException(SR.GetString("IndexOutOfRange", new object[]
				{
					index
				}));
			}
			if (index != -1 && (this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMap || this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate))
			{
				int num = 0;
				if (index > 0)
				{
					foreach (object obj in this._items)
					{
						ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
						if (entry._entryType != ConfigurationElementCollection.EntryType.Removed)
						{
							index--;
						}
						if (index == 0)
						{
							break;
						}
						num++;
					}
					num = (index = num + 1);
				}
				object elementKeyInternal = this.GetElementKeyInternal(element);
				foreach (object obj2 in this._items)
				{
					ConfigurationElementCollection.Entry entry2 = (ConfigurationElementCollection.Entry)obj2;
					if (this.CompareKeys(elementKeyInternal, entry2.GetKey(this)) && entry2._entryType != ConfigurationElementCollection.EntryType.Removed)
					{
						if (!element.Equals(entry2._value))
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_base_collection_entry_already_exists", new object[]
							{
								elementKeyInternal
							}), element.PropertyFileName(""), element.PropertyLineNumber(""));
						}
						return;
					}
				}
			}
			this.BaseAddInternal(index, element, false, ignoreLocks);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000E424 File Offset: 0x0000C624
		protected internal void BaseRemove(object key)
		{
			this.BaseRemove(key, false);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000E430 File Offset: 0x0000C630
		private void BaseRemove(object key, bool throwIfMissing)
		{
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_read_only"));
			}
			int num = 0;
			bool flag = false;
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				if (this.CompareKeys(key, entry.GetKey(this)))
				{
					flag = true;
					if (entry._value == null)
					{
						if (throwIfMissing)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_base_collection_entry_not_found", new object[]
							{
								key
							}));
						}
						return;
					}
					else
					{
						if (entry._value.LockItem)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_base_attribute_locked", new object[]
							{
								key
							}));
						}
						if (!entry._value.ElementPresent)
						{
							base.CheckLockedElement(this._removeElement, null);
						}
						ConfigurationElementCollection.EntryType entryType = entry._entryType;
						if (entryType != ConfigurationElementCollection.EntryType.Removed)
						{
							if (entryType == ConfigurationElementCollection.EntryType.Added)
							{
								if (this.CollectionType != ConfigurationElementCollectionType.AddRemoveClearMap && this.CollectionType != ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
								{
									if (this.CollectionType == ConfigurationElementCollectionType.BasicMapAlternate && num >= this.Count - this._inheritedCount)
									{
										throw new ConfigurationErrorsException(SR.GetString("Config_base_cannot_remove_inherited_items"));
									}
									if (this.CollectionType == ConfigurationElementCollectionType.BasicMap && num < this._inheritedCount)
									{
										throw new ConfigurationErrorsException(SR.GetString("Config_base_cannot_remove_inherited_items"));
									}
									this._items.RemoveAt(num);
								}
								else
								{
									entry._entryType = ConfigurationElementCollection.EntryType.Removed;
									this._removedItemCount++;
								}
							}
							else
							{
								if (this.CollectionType != ConfigurationElementCollectionType.AddRemoveClearMap && this.CollectionType != ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
								{
									throw new ConfigurationErrorsException(SR.GetString("Config_base_collection_elements_may_not_be_removed"));
								}
								entry._entryType = ConfigurationElementCollection.EntryType.Removed;
								this._removedItemCount++;
							}
						}
						else if (throwIfMissing)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_base_collection_entry_already_removed"));
						}
						this.bModified = true;
						return;
					}
				}
				else
				{
					num++;
				}
			}
			if (!flag)
			{
				if (throwIfMissing)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_base_collection_entry_not_found", new object[]
					{
						key
					}));
				}
				if (this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMap || this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
				{
					if (this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
					{
						this._items.Insert(this.Count + this._removedItemCount - this._inheritedCount, new ConfigurationElementCollection.Entry(ConfigurationElementCollection.EntryType.Removed, key, null));
					}
					else
					{
						this._items.Add(new ConfigurationElementCollection.Entry(ConfigurationElementCollection.EntryType.Removed, key, null));
					}
					this._removedItemCount++;
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		protected internal ConfigurationElement BaseGet(object key)
		{
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				if (entry._entryType != ConfigurationElementCollection.EntryType.Removed && this.CompareKeys(key, entry.GetKey(this)))
				{
					return entry._value;
				}
			}
			return null;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000E728 File Offset: 0x0000C928
		protected internal bool BaseIsRemoved(object key)
		{
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				if (this.CompareKeys(key, entry.GetKey(this)))
				{
					if (entry._entryType == ConfigurationElementCollection.EntryType.Removed)
					{
						return true;
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		protected internal ConfigurationElement BaseGet(int index)
		{
			if (index < 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("IndexOutOfRange", new object[]
				{
					index
				}));
			}
			int num = 0;
			ConfigurationElementCollection.Entry entry = null;
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry2 = (ConfigurationElementCollection.Entry)obj;
				if (num == index && entry2._entryType != ConfigurationElementCollection.EntryType.Removed)
				{
					entry = entry2;
					break;
				}
				if (entry2._entryType != ConfigurationElementCollection.EntryType.Removed)
				{
					num++;
				}
			}
			if (entry != null)
			{
				return entry._value;
			}
			throw new ConfigurationErrorsException(SR.GetString("IndexOutOfRange", new object[]
			{
				index
			}));
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000E864 File Offset: 0x0000CA64
		protected internal object[] BaseGetAllKeys()
		{
			object[] array = new object[this.Count];
			int num = 0;
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				if (entry._entryType != ConfigurationElementCollection.EntryType.Removed)
				{
					array[num] = entry.GetKey(this);
					num++;
				}
			}
			return array;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000E8E0 File Offset: 0x0000CAE0
		protected internal object BaseGetKey(int index)
		{
			int num = 0;
			ConfigurationElementCollection.Entry entry = null;
			if (index < 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("IndexOutOfRange", new object[]
				{
					index
				}));
			}
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry2 = (ConfigurationElementCollection.Entry)obj;
				if (num == index && entry2._entryType != ConfigurationElementCollection.EntryType.Removed)
				{
					entry = entry2;
					break;
				}
				if (entry2._entryType != ConfigurationElementCollection.EntryType.Removed)
				{
					num++;
				}
			}
			if (entry != null)
			{
				return entry.GetKey(this);
			}
			throw new ConfigurationErrorsException(SR.GetString("IndexOutOfRange", new object[]
			{
				index
			}));
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000E9A8 File Offset: 0x0000CBA8
		protected internal void BaseClear()
		{
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_read_only"));
			}
			base.CheckLockedElement(this._clearElement, null);
			base.CheckLockedElement(this._removeElement, null);
			this.bModified = true;
			this.bCollectionCleared = true;
			if ((this.CollectionType == ConfigurationElementCollectionType.BasicMap || this.CollectionType == ConfigurationElementCollectionType.BasicMapAlternate) && this._inheritedCount > 0)
			{
				int index = 0;
				if (this.CollectionType == ConfigurationElementCollectionType.BasicMapAlternate)
				{
					index = 0;
				}
				if (this.CollectionType == ConfigurationElementCollectionType.BasicMap)
				{
					index = this._inheritedCount;
				}
				while (this.Count - this._inheritedCount > 0)
				{
					this._items.RemoveAt(index);
				}
				return;
			}
			int num = 0;
			int num2 = 0;
			int count = this.Count;
			for (int i = 0; i < this._items.Count; i++)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)this._items[i];
				if (entry._value != null && entry._value.LockItem)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_base_collection_item_locked_cannot_clear"));
				}
			}
			for (int j = this._items.Count - 1; j >= 0; j--)
			{
				ConfigurationElementCollection.Entry entry2 = (ConfigurationElementCollection.Entry)this._items[j];
				if ((this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMap && j < this._inheritedCount) || (this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate && j >= count - this._inheritedCount))
				{
					num++;
				}
				if (entry2._entryType == ConfigurationElementCollection.EntryType.Removed)
				{
					num2++;
				}
				this._items.RemoveAt(j);
			}
			this._inheritedCount -= num;
			this._removedItemCount -= num2;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000EB40 File Offset: 0x0000CD40
		protected internal void BaseRemoveAt(int index)
		{
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_read_only"));
			}
			int num = 0;
			ConfigurationElementCollection.Entry entry = null;
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry2 = (ConfigurationElementCollection.Entry)obj;
				if (num == index && entry2._entryType != ConfigurationElementCollection.EntryType.Removed)
				{
					entry = entry2;
					break;
				}
				if (entry2._entryType != ConfigurationElementCollection.EntryType.Removed)
				{
					num++;
				}
			}
			if (entry == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("IndexOutOfRange", new object[]
				{
					index
				}));
			}
			if (entry._value.LockItem)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_attribute_locked", new object[]
				{
					entry.GetKey(this)
				}));
			}
			if (!entry._value.ElementPresent)
			{
				base.CheckLockedElement(this._removeElement, null);
			}
			ConfigurationElementCollection.EntryType entryType = entry._entryType;
			if (entryType != ConfigurationElementCollection.EntryType.Removed)
			{
				if (entryType == ConfigurationElementCollection.EntryType.Added)
				{
					if (this.CollectionType != ConfigurationElementCollectionType.AddRemoveClearMap && this.CollectionType != ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
					{
						if (this.CollectionType == ConfigurationElementCollectionType.BasicMapAlternate && index >= this.Count - this._inheritedCount)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_base_cannot_remove_inherited_items"));
						}
						if (this.CollectionType == ConfigurationElementCollectionType.BasicMap && index < this._inheritedCount)
						{
							throw new ConfigurationErrorsException(SR.GetString("Config_base_cannot_remove_inherited_items"));
						}
						this._items.RemoveAt(index);
					}
					else
					{
						if (!entry._value.ElementPresent)
						{
							base.CheckLockedElement(this._removeElement, null);
						}
						entry._entryType = ConfigurationElementCollection.EntryType.Removed;
						this._removedItemCount++;
					}
				}
				else
				{
					if (this.CollectionType != ConfigurationElementCollectionType.AddRemoveClearMap && this.CollectionType != ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_base_collection_elements_may_not_be_removed"));
					}
					entry._entryType = ConfigurationElementCollection.EntryType.Removed;
					this._removedItemCount++;
				}
				this.bModified = true;
				return;
			}
			throw new ConfigurationErrorsException(SR.GetString("Config_base_collection_entry_already_removed"));
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		protected internal override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			ConfigurationElementCollectionType collectionType = this.CollectionType;
			bool flag = false;
			flag |= base.SerializeElement(writer, serializeCollectionKey);
			if ((collectionType == ConfigurationElementCollectionType.AddRemoveClearMap || collectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate) && this.bEmitClearTag && this._clearElement.Length != 0)
			{
				if (writer != null)
				{
					writer.WriteStartElement(this._clearElement);
					writer.WriteEndElement();
				}
				flag = true;
			}
			foreach (object obj in this._items)
			{
				ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)obj;
				if (collectionType == ConfigurationElementCollectionType.BasicMap || collectionType == ConfigurationElementCollectionType.BasicMapAlternate)
				{
					if (entry._entryType == ConfigurationElementCollection.EntryType.Added || entry._entryType == ConfigurationElementCollection.EntryType.Replaced)
					{
						if (this.ElementName != null && this.ElementName.Length != 0)
						{
							if (BaseConfigurationRecord.IsReservedAttributeName(this.ElementName))
							{
								throw new ArgumentException(SR.GetString("Basicmap_item_name_reserved", new object[]
								{
									this.ElementName
								}));
							}
							flag |= entry._value.SerializeToXmlElement(writer, this.ElementName);
						}
						else
						{
							flag |= entry._value.SerializeElement(writer, false);
						}
					}
				}
				else if (collectionType == ConfigurationElementCollectionType.AddRemoveClearMap || collectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
				{
					if ((entry._entryType == ConfigurationElementCollection.EntryType.Removed || entry._entryType == ConfigurationElementCollection.EntryType.Replaced) && entry._value != null)
					{
						if (writer != null)
						{
							writer.WriteStartElement(this._removeElement);
						}
						flag |= entry._value.SerializeElement(writer, true);
						if (writer != null)
						{
							writer.WriteEndElement();
						}
						flag = true;
					}
					if (entry._entryType == ConfigurationElementCollection.EntryType.Added || entry._entryType == ConfigurationElementCollection.EntryType.Replaced)
					{
						flag |= entry._value.SerializeToXmlElement(writer, this._addElement);
					}
				}
			}
			return flag;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000EEEC File Offset: 0x0000D0EC
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			bool result = false;
			if (this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMap || this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
			{
				if (elementName == this._addElement)
				{
					ConfigurationElement configurationElement = this.CallCreateNewElement();
					configurationElement.ResetLockLists(this);
					configurationElement.DeserializeElement(reader, false);
					this.BaseAdd(configurationElement);
					result = true;
				}
				else if (elementName == this._removeElement)
				{
					ConfigurationElement configurationElement2 = this.CallCreateNewElement();
					configurationElement2.ResetLockLists(this);
					configurationElement2.DeserializeElement(reader, true);
					if (this.IsElementRemovable(configurationElement2))
					{
						this.BaseRemove(this.GetElementKeyInternal(configurationElement2), false);
					}
					result = true;
				}
				else if (elementName == this._clearElement)
				{
					if (reader.AttributeCount > 0 && reader.MoveToNextAttribute())
					{
						string name = reader.Name;
						throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_attribute", new object[]
						{
							name
						}), reader);
					}
					base.CheckLockedElement(elementName, reader);
					reader.MoveToElement();
					this.BaseClear();
					this.bEmitClearTag = true;
					result = true;
				}
			}
			else if (elementName == this.ElementName)
			{
				if (BaseConfigurationRecord.IsReservedAttributeName(elementName))
				{
					throw new ArgumentException(SR.GetString("Basicmap_item_name_reserved", new object[]
					{
						elementName
					}));
				}
				ConfigurationElement configurationElement3 = this.CallCreateNewElement();
				configurationElement3.ResetLockLists(this);
				configurationElement3.DeserializeElement(reader, false);
				this.BaseAdd(configurationElement3);
				result = true;
			}
			else if (this.IsElementName(elementName))
			{
				if (BaseConfigurationRecord.IsReservedAttributeName(elementName))
				{
					throw new ArgumentException(SR.GetString("Basicmap_item_name_reserved", new object[]
					{
						elementName
					}));
				}
				ConfigurationElement configurationElement4 = this.CallCreateNewElement(elementName);
				configurationElement4.ResetLockLists(this);
				configurationElement4.DeserializeElement(reader, false);
				this.BaseAdd(-1, configurationElement4);
				result = true;
			}
			return result;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000F094 File Offset: 0x0000D294
		private ConfigurationElement CallCreateNewElement(string elementName)
		{
			ConfigurationElement configurationElement = this.CreateNewElement(elementName);
			configurationElement.AssociateContext(this._configRecord);
			configurationElement.CallInit();
			return configurationElement;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000F0BC File Offset: 0x0000D2BC
		private ConfigurationElement CallCreateNewElement()
		{
			ConfigurationElement configurationElement = this.CreateNewElement();
			configurationElement.AssociateContext(this._configRecord);
			configurationElement.CallInit();
			return configurationElement;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000F0E3 File Offset: 0x0000D2E3
		protected virtual ConfigurationElement CreateNewElement(string elementName)
		{
			return this.CreateNewElement();
		}

		// Token: 0x060001D1 RID: 465
		protected abstract ConfigurationElement CreateNewElement();

		// Token: 0x060001D2 RID: 466
		protected abstract object GetElementKey(ConfigurationElement element);

		// Token: 0x060001D3 RID: 467 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		internal object GetElementKeyInternal(ConfigurationElement element)
		{
			object elementKey = this.GetElementKey(element);
			if (elementKey == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_invalid_element_key"));
			}
			return elementKey;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000874E File Offset: 0x0000694E
		protected virtual bool IsElementRemovable(ConfigurationElement element)
		{
			return true;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000F115 File Offset: 0x0000D315
		private bool CompareKeys(object key1, object key2)
		{
			if (this._comparer != null)
			{
				return this._comparer.Compare(key1, key2) == 0;
			}
			return key1.Equals(key2);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000F137 File Offset: 0x0000D337
		protected virtual string ElementName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008751 File Offset: 0x00006951
		protected virtual bool IsElementName(string elementName)
		{
			return false;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000F140 File Offset: 0x0000D340
		internal bool IsLockableElement(string elementName)
		{
			if (this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMap || this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
			{
				return elementName == this.AddElementName || elementName == this.RemoveElementName || elementName == this.ClearElementName;
			}
			return elementName == this.ElementName || this.IsElementName(elementName);
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000F1A4 File Offset: 0x0000D3A4
		internal string LockableElements
		{
			get
			{
				if (this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMap || this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate)
				{
					string text = "'" + this.AddElementName + "'";
					if (this.RemoveElementName.Length != 0)
					{
						text = text + ", '" + this.RemoveElementName + "'";
					}
					if (this.ClearElementName.Length != 0)
					{
						text = text + ", '" + this.ClearElementName + "'";
					}
					return text;
				}
				if (!string.IsNullOrEmpty(this.ElementName))
				{
					return "'" + this.ElementName + "'";
				}
				return string.Empty;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000F24B File Offset: 0x0000D44B
		protected virtual bool ThrowOnDuplicate
		{
			get
			{
				return this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMap || this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000874E File Offset: 0x0000694E
		public virtual ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x040001AD RID: 429
		internal const string DefaultAddItemName = "add";

		// Token: 0x040001AE RID: 430
		internal const string DefaultRemoveItemName = "remove";

		// Token: 0x040001AF RID: 431
		internal const string DefaultClearItemsName = "clear";

		// Token: 0x040001B0 RID: 432
		private int _removedItemCount;

		// Token: 0x040001B1 RID: 433
		private int _inheritedCount;

		// Token: 0x040001B2 RID: 434
		private ArrayList _items = new ArrayList();

		// Token: 0x040001B3 RID: 435
		private string _addElement = "add";

		// Token: 0x040001B4 RID: 436
		private string _removeElement = "remove";

		// Token: 0x040001B5 RID: 437
		private string _clearElement = "clear";

		// Token: 0x040001B6 RID: 438
		private bool bEmitClearTag;

		// Token: 0x040001B7 RID: 439
		private bool bCollectionCleared;

		// Token: 0x040001B8 RID: 440
		private bool bModified;

		// Token: 0x040001B9 RID: 441
		private bool bReadOnly;

		// Token: 0x040001BA RID: 442
		private IComparer _comparer;

		// Token: 0x040001BB RID: 443
		internal bool internalAddToEnd;

		// Token: 0x040001BC RID: 444
		internal string internalElementTagName = string.Empty;

		// Token: 0x020000C9 RID: 201
		private enum InheritedType
		{
			// Token: 0x0400047F RID: 1151
			inNeither,
			// Token: 0x04000480 RID: 1152
			inParent,
			// Token: 0x04000481 RID: 1153
			inSelf,
			// Token: 0x04000482 RID: 1154
			inBothSame,
			// Token: 0x04000483 RID: 1155
			inBothDiff,
			// Token: 0x04000484 RID: 1156
			inBothCopyNoRemove
		}

		// Token: 0x020000CA RID: 202
		private enum EntryType
		{
			// Token: 0x04000486 RID: 1158
			Inherited,
			// Token: 0x04000487 RID: 1159
			Replaced,
			// Token: 0x04000488 RID: 1160
			Removed,
			// Token: 0x04000489 RID: 1161
			Added
		}

		// Token: 0x020000CB RID: 203
		private class Entry
		{
			// Token: 0x060007DA RID: 2010 RVA: 0x00020A20 File Offset: 0x0001EC20
			internal object GetKey(ConfigurationElementCollection ThisCollection)
			{
				if (this._value != null)
				{
					return ThisCollection.GetElementKeyInternal(this._value);
				}
				return this._key;
			}

			// Token: 0x060007DB RID: 2011 RVA: 0x00020A3D File Offset: 0x0001EC3D
			internal Entry(ConfigurationElementCollection.EntryType type, object key, ConfigurationElement value)
			{
				this._entryType = type;
				this._key = key;
				this._value = value;
			}

			// Token: 0x0400048A RID: 1162
			internal ConfigurationElementCollection.EntryType _entryType;

			// Token: 0x0400048B RID: 1163
			internal object _key;

			// Token: 0x0400048C RID: 1164
			internal ConfigurationElement _value;
		}

		// Token: 0x020000CC RID: 204
		private class Enumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060007DC RID: 2012 RVA: 0x00020A5A File Offset: 0x0001EC5A
			internal Enumerator(ArrayList items, ConfigurationElementCollection collection)
			{
				this._itemsEnumerator = items.GetEnumerator();
				this.ThisCollection = collection;
			}

			// Token: 0x060007DD RID: 2013 RVA: 0x00020A78 File Offset: 0x0001EC78
			bool IEnumerator.MoveNext()
			{
				while (this._itemsEnumerator.MoveNext())
				{
					ConfigurationElementCollection.Entry entry = (ConfigurationElementCollection.Entry)this._itemsEnumerator.Current;
					if (entry._entryType != ConfigurationElementCollection.EntryType.Removed)
					{
						this._current.Key = ((entry.GetKey(this.ThisCollection) != null) ? entry.GetKey(this.ThisCollection) : "key");
						this._current.Value = entry._value;
						return true;
					}
				}
				return false;
			}

			// Token: 0x060007DE RID: 2014 RVA: 0x00020AEE File Offset: 0x0001ECEE
			void IEnumerator.Reset()
			{
				this._itemsEnumerator.Reset();
			}

			// Token: 0x17000248 RID: 584
			// (get) Token: 0x060007DF RID: 2015 RVA: 0x00020AFB File Offset: 0x0001ECFB
			object IEnumerator.Current
			{
				get
				{
					return this._current.Value;
				}
			}

			// Token: 0x17000249 RID: 585
			// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00020B08 File Offset: 0x0001ED08
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00020B10 File Offset: 0x0001ED10
			object IDictionaryEnumerator.Key
			{
				get
				{
					return this._current.Key;
				}
			}

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00020AFB File Offset: 0x0001ECFB
			object IDictionaryEnumerator.Value
			{
				get
				{
					return this._current.Value;
				}
			}

			// Token: 0x0400048D RID: 1165
			private IEnumerator _itemsEnumerator;

			// Token: 0x0400048E RID: 1166
			private DictionaryEntry _current;

			// Token: 0x0400048F RID: 1167
			private ConfigurationElementCollection ThisCollection;
		}
	}
}
