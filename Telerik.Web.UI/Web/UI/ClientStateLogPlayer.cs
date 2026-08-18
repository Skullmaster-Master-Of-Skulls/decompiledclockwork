using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001AF5 RID: 6901
	internal class ClientStateLogPlayer<T> where T : ControlItem
	{
		// Token: 0x17005143 RID: 20803
		// (get) Token: 0x06010B2F RID: 68399 RVA: 0x003B7C55 File Offset: 0x003B5E55
		// (set) Token: 0x06010B30 RID: 68400 RVA: 0x003B7C5D File Offset: 0x003B5E5D
		private ControlItemContainer ItemContainer { get; set; }

		// Token: 0x06010B31 RID: 68401 RVA: 0x003B7C66 File Offset: 0x003B5E66
		public ClientStateLogPlayer(ControlItemContainer itemContainer)
		{
			this.ItemContainer = itemContainer;
		}

		// Token: 0x06010B32 RID: 68402 RVA: 0x003B7C78 File Offset: 0x003B5E78
		public IList<ClientOperation<T>> Play(IEnumerable<ClientStateLogEntry> clientStateLogEntry)
		{
			List<ClientOperation<T>> list = new List<ClientOperation<T>>();
			foreach (ClientStateLogEntry entry in clientStateLogEntry)
			{
				ClientOperation<T> clientOperation = this.Play(entry);
				if (clientOperation != null)
				{
					list.Add(clientOperation);
				}
			}
			return list;
		}

		// Token: 0x06010B33 RID: 68403 RVA: 0x003B7CD4 File Offset: 0x003B5ED4
		public ClientOperation<T> Play(ClientStateLogEntry entry)
		{
			if (entry.Type == ClientStateLogEntryType.Invalid)
			{
				return null;
			}
			if (entry.Type == ClientStateLogEntryType.Clear)
			{
				return this.ClearCommand(entry);
			}
			ControlItemCollection itemCollectionToUpdate = this.GetItemCollectionToUpdate(entry);
			if (itemCollectionToUpdate == null)
			{
				return null;
			}
			int index = ClientStateLogPlayer<T>.AdjustVisibleIndex(itemCollectionToUpdate, ClientStateLogPlayer<T>.ExtractChildIndex(entry.Index));
			ClientOperation<T> result = null;
			switch (entry.Type)
			{
			case ClientStateLogEntryType.Insert:
				result = this.Insert(entry, itemCollectionToUpdate, index);
				break;
			case ClientStateLogEntryType.Remove:
				result = ClientStateLogPlayer<T>.Remove(itemCollectionToUpdate, index);
				break;
			case ClientStateLogEntryType.Update:
				result = ClientStateLogPlayer<T>.Update(entry, itemCollectionToUpdate, index);
				break;
			case ClientStateLogEntryType.Reorder:
				result = ClientStateLogPlayer<T>.Reorder(entry, itemCollectionToUpdate, index);
				break;
			}
			return result;
		}

		// Token: 0x06010B34 RID: 68404 RVA: 0x003B7D70 File Offset: 0x003B5F70
		internal static string ExtractParentIndex(string hierarchicalIndex)
		{
			int num = hierarchicalIndex.LastIndexOf(":");
			if (num < 0)
			{
				return null;
			}
			return hierarchicalIndex.Substring(0, num);
		}

		// Token: 0x06010B35 RID: 68405 RVA: 0x003B7D98 File Offset: 0x003B5F98
		internal static int ExtractChildIndex(string hierarchicalIndex)
		{
			string[] array = hierarchicalIndex.Split(new char[]
			{
				':'
			});
			if (array.Length == 1)
			{
				return Convert.ToInt32(hierarchicalIndex);
			}
			return Convert.ToInt32(array[array.Length - 1]);
		}

		// Token: 0x06010B36 RID: 68406 RVA: 0x003B7DD2 File Offset: 0x003B5FD2
		internal ControlItem FindParentItem(string parentIndex)
		{
			return this.ItemContainer.FindItemByHierarchicalIndex(parentIndex);
		}

		// Token: 0x06010B37 RID: 68407 RVA: 0x003B7DE0 File Offset: 0x003B5FE0
		private static ReorderClientOperation<T> Reorder(ClientStateLogEntry entry, ControlItemCollection items, int index)
		{
			if (index < 0 || index > items.Count)
			{
				return null;
			}
			int num = ClientStateLogPlayer<T>.AdjustVisibleIndex(items, ClientStateLogPlayer<T>.ExtractChildIndex((string)entry.Data["NewIndex"]));
			ControlItem controlItem = items[index];
			items.Remove(controlItem);
			items.Insert(num, controlItem);
			return new ReorderClientOperation<T>
			{
				Item = (T)((object)controlItem),
				Type = ClientOperationType.Reorder,
				NewIndex = num,
				OldIndex = index
			};
		}

		// Token: 0x06010B38 RID: 68408 RVA: 0x003B7E5C File Offset: 0x003B605C
		private ClientOperation<T> ClearCommand(ClientStateLogEntry entry)
		{
			ClientOperation<T> clientOperation = new ClientOperation<T>
			{
				Type = ClientOperationType.Clear
			};
			if (string.IsNullOrEmpty(entry.Index))
			{
				this.ItemContainer.Children.Clear();
				return clientOperation;
			}
			ControlItem controlItem = this.ItemContainer.FindItemByHierarchicalIndex(entry.Index);
			if (controlItem == null)
			{
				return null;
			}
			controlItem.Children.Clear();
			clientOperation.Item = (T)((object)controlItem);
			return clientOperation;
		}

		// Token: 0x06010B39 RID: 68409 RVA: 0x003B7EC8 File Offset: 0x003B60C8
		private static ClientOperation<T> Remove(ControlItemCollection items, int index)
		{
			if (index < 0 || index >= items.Count)
			{
				return null;
			}
			ControlItem controlItem = items[index];
			items.RemoveAt(index);
			return new ClientOperation<T>
			{
				Item = (T)((object)controlItem),
				Type = ClientOperationType.Remove
			};
		}

		// Token: 0x06010B3A RID: 68410 RVA: 0x003B7F10 File Offset: 0x003B6110
		private static ClientOperation<T> Update(ClientStateLogEntry entry, ControlItemCollection items, int index)
		{
			if (index < 0 || index >= items.Count)
			{
				return null;
			}
			ControlItem controlItem = items[index];
			controlItem.LoadFromDictionary(entry.Data);
			UpdateClientOperation<T> updateClientOperation = new UpdateClientOperation<T>
			{
				Item = (T)((object)controlItem),
				Type = ClientOperationType.Update
			};
			using (IEnumerator<string> enumerator = entry.Data.Keys.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					string text = enumerator.Current;
					updateClientOperation.PropertyName = text[0].ToString().ToUpper() + text.Substring(1);
				}
			}
			return updateClientOperation;
		}

		// Token: 0x06010B3B RID: 68411 RVA: 0x003B7FC8 File Offset: 0x003B61C8
		private ClientOperation<T> Insert(ClientStateLogEntry entry, ControlItemCollection items, int index)
		{
			if (index < 0 || index > items.Count)
			{
				return null;
			}
			ControlItem controlItem = this.ItemContainer.CreateItem(entry) ?? this.ItemContainer.CreateItem();
			items.Insert(index, controlItem);
			controlItem.LoadFromDictionary(entry.Data);
			return new ClientOperation<T>
			{
				Item = (T)((object)controlItem),
				Type = ClientOperationType.Insert
			};
		}

		// Token: 0x06010B3C RID: 68412 RVA: 0x003B8030 File Offset: 0x003B6230
		private ControlItemCollection GetItemCollectionToUpdate(ClientStateLogEntry entry)
		{
			if (string.IsNullOrEmpty(entry.Index))
			{
				throw new InvalidOperationException("Index should be specified");
			}
			ControlItemCollection result = this.ItemContainer.Children;
			string text = ClientStateLogPlayer<T>.ExtractParentIndex(entry.Index);
			if (text != null)
			{
				ControlItem controlItem = this.FindParentItem(text);
				result = ((controlItem != null) ? controlItem.Children : null);
			}
			return result;
		}

		// Token: 0x06010B3D RID: 68413 RVA: 0x003B8088 File Offset: 0x003B6288
		private static int AdjustVisibleIndex(ControlItemCollection items, int initialIndex)
		{
			if (items.Count == items.VisibleItems.Count)
			{
				return initialIndex;
			}
			int num = 0;
			while (num <= initialIndex && num < items.Count)
			{
				if (!items[num].Visible)
				{
					initialIndex++;
				}
				num++;
			}
			return initialIndex;
		}
	}
}
