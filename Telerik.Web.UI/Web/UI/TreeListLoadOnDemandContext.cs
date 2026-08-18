using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001235 RID: 4661
	internal class TreeListLoadOnDemandContext
	{
		// Token: 0x17003E10 RID: 15888
		// (get) Token: 0x0600C03A RID: 49210 RVA: 0x002AAA69 File Offset: 0x002A8C69
		// (set) Token: 0x0600C03B RID: 49211 RVA: 0x002AAA71 File Offset: 0x002A8C71
		public HashSet<TreeListSourceItem> ExpandedItems { get; set; }

		// Token: 0x17003E11 RID: 15889
		// (get) Token: 0x0600C03C RID: 49212 RVA: 0x002AAA7A File Offset: 0x002A8C7A
		// (set) Token: 0x0600C03D RID: 49213 RVA: 0x002AAA82 File Offset: 0x002A8C82
		public List<TreeListSourceItem> SelectedItems { get; set; }

		// Token: 0x17003E12 RID: 15890
		// (get) Token: 0x0600C03E RID: 49214 RVA: 0x002AAA8B File Offset: 0x002A8C8B
		// (set) Token: 0x0600C03F RID: 49215 RVA: 0x002AAA93 File Offset: 0x002A8C93
		public RadTreeList OwnerTreeList { get; set; }

		// Token: 0x17003E13 RID: 15891
		// (get) Token: 0x0600C040 RID: 49216 RVA: 0x002AAA9C File Offset: 0x002A8C9C
		// (set) Token: 0x0600C041 RID: 49217 RVA: 0x002AAAA4 File Offset: 0x002A8CA4
		internal int ExpansionDepth { get; set; }

		// Token: 0x17003E14 RID: 15892
		// (get) Token: 0x0600C042 RID: 49218 RVA: 0x002AAAB0 File Offset: 0x002A8CB0
		public List<Hashtable> SelectedItemsDataKeyValues
		{
			get
			{
				if (this._selectedItems == null)
				{
					this._selectedItems = (List<Hashtable>)this.OwnerTreeList.ControlState["_selectedItems"];
					if (this._selectedItems == null)
					{
						this._selectedItems = new List<Hashtable>();
						this.OwnerTreeList.ControlState["_selectedItems"] = this._selectedItems;
					}
				}
				return this._selectedItems;
			}
		}

		// Token: 0x17003E15 RID: 15893
		// (get) Token: 0x0600C043 RID: 49219 RVA: 0x002AAB1C File Offset: 0x002A8D1C
		public HashSet<Hashtable> ExpandedItemsDataKeyValues
		{
			get
			{
				if (this._expandedItems == null)
				{
					List<Hashtable> list = this.OwnerTreeList.ControlState["_expandedItems"] as List<Hashtable>;
					if (list == null)
					{
						this._expandedItems = new HashSet<Hashtable>(new TreeListEnumerableHelper.ExpandedItemsDataKeyValuesHashEqualityComparer());
					}
					else
					{
						this._expandedItems = new HashSet<Hashtable>(list, new TreeListEnumerableHelper.ExpandedItemsDataKeyValuesHashEqualityComparer());
					}
				}
				return this._expandedItems;
			}
		}

		// Token: 0x0600C044 RID: 49220 RVA: 0x002AAB78 File Offset: 0x002A8D78
		public TreeListLoadOnDemandContext(RadTreeList treeList)
		{
			this.OwnerTreeList = treeList;
			this.ExpandedItems = new HashSet<TreeListSourceItem>();
			this.SelectedItems = new List<TreeListSourceItem>();
		}

		// Token: 0x0600C045 RID: 49221 RVA: 0x002AABA8 File Offset: 0x002A8DA8
		public void InsertExpandedDataKeyValue(TreeListDataItem item)
		{
			Hashtable hashtable = new Hashtable();
			foreach (string text in item.OwnerTreeList.DataKeyNames)
			{
				hashtable.Add(text, item.GetDataKeyValue(text));
			}
			this.ExpandedItemsDataKeyValues.Add(hashtable);
		}

		// Token: 0x0600C046 RID: 49222 RVA: 0x002AABF4 File Offset: 0x002A8DF4
		public void InsertExpandedDataKeyValue(TreeListSourceItem item)
		{
			Hashtable hashtable = new Hashtable();
			foreach (string text in this.OwnerTreeList.DataKeyNames)
			{
				DataKey dataKey = new DataKey();
				dataKey[text] = this.OwnerTreeList.ExtractDataKeyValue(item.OriginalDataItem, text);
				hashtable[text] = dataKey[text];
			}
			this.ExpandedItemsDataKeyValues.Add(hashtable);
		}

		// Token: 0x0600C047 RID: 49223 RVA: 0x002AAC64 File Offset: 0x002A8E64
		public void InsertSelectedDataKeyValue(TreeListDataItem item)
		{
			Hashtable hashtable = new Hashtable();
			foreach (string text in item.OwnerTreeList.DataKeyNames)
			{
				hashtable.Add(text, item.GetDataKeyValue(text));
			}
			this.SelectedItemsDataKeyValues.Add(hashtable);
		}

		// Token: 0x0600C048 RID: 49224 RVA: 0x002AACB0 File Offset: 0x002A8EB0
		public void RemoveExpandedDataKeyValue(TreeListDataItem item)
		{
			Hashtable hashtable = new Hashtable();
			foreach (string text in item.OwnerTreeList.DataKeyNames)
			{
				hashtable.Add(text, item.GetDataKeyValue(text));
			}
			this.ExpandedItemsDataKeyValues.Remove(hashtable);
		}

		// Token: 0x0600C049 RID: 49225 RVA: 0x002AACFC File Offset: 0x002A8EFC
		public void RemoveExpandedDataKeyValue(TreeListSourceItem item)
		{
			Hashtable hashtable = new Hashtable();
			foreach (string text in this.OwnerTreeList.DataKeyNames)
			{
				DataKey dataKey = new DataKey();
				dataKey[text] = this.OwnerTreeList.ExtractDataKeyValue(item.OriginalDataItem, text);
				hashtable[text] = dataKey[text];
			}
			this.ExpandedItemsDataKeyValues.Remove(hashtable);
		}

		// Token: 0x0600C04A RID: 49226 RVA: 0x002AAD6C File Offset: 0x002A8F6C
		public void RemoveSelectedDataKeyValue(TreeListDataItem item)
		{
			for (int i = 0; i < this.SelectedItemsDataKeyValues.Count; i++)
			{
				bool flag = false;
				foreach (string text in item.OwnerTreeList.DataKeyNames)
				{
					flag = this.SelectedItemsDataKeyValues[i][text].Equals(item.GetDataKeyValue(text));
				}
				if (flag)
				{
					this.SelectedItemsDataKeyValues.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600C04B RID: 49227 RVA: 0x002AADEC File Offset: 0x002A8FEC
		public bool ItemNeedsToBeExpanded(Hashtable dataKyesArr, TreeListSourceItem sourceItem)
		{
			if (this.ExpandedItemsDataKeyValues.Count == 0)
			{
				return false;
			}
			foreach (Hashtable hashtable in this.ExpandedItemsDataKeyValues)
			{
				bool flag = false;
				foreach (object key in dataKyesArr.Keys)
				{
					flag = hashtable[key].Equals(dataKyesArr[key]);
				}
				if (flag)
				{
					this.ExpandedItems.Add(sourceItem);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600C04C RID: 49228 RVA: 0x002AAEC0 File Offset: 0x002A90C0
		public bool ItemNeedsToBeSelected(Hashtable dataKyesArr)
		{
			if (this.SelectedItemsDataKeyValues.Count == 0)
			{
				return false;
			}
			foreach (Hashtable hashtable in this.SelectedItemsDataKeyValues)
			{
				bool flag = false;
				foreach (object key in dataKyesArr.Keys)
				{
					flag = hashtable[key].Equals(dataKyesArr[key]);
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04003293 RID: 12947
		private HashSet<Hashtable> _expandedItems;

		// Token: 0x04003294 RID: 12948
		private List<Hashtable> _selectedItems;

		// Token: 0x04003295 RID: 12949
		internal HashSet<TreeListHierarchyIndex> ExpandedOnDemandIndexes = new HashSet<TreeListHierarchyIndex>();
	}
}
