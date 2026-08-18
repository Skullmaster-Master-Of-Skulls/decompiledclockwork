using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020004AF RID: 1199
	public class GridBatchEditingCommand
	{
		// Token: 0x06002AB3 RID: 10931 RVA: 0x0008A0DF File Offset: 0x000882DF
		public GridBatchEditingCommand(GridTableView ownerTableView, GridDataItem dataItem, GridBatchEditingCommandType type)
		{
			this.ownerTableView = ownerTableView;
			this.dataSourceView = ownerTableView.GetDataSourceView();
			this.item = dataItem;
			this.type = type;
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x0008A108 File Offset: 0x00088308
		internal GridBatchEditingCommand(GridTableView ownerTableView, DataSourceView dataSourceView, string command)
		{
			this.canceled = false;
			this.ownerTableView = ownerTableView;
			this.dataSourceView = dataSourceView;
			int num = command.IndexOf('(') + 1;
			int num2 = command.LastIndexOf(')');
			string argumentsAsString = command.Substring(num, num2 - num);
			this.FillArgumnetsArray(argumentsAsString);
			string a;
			if ((a = command.Substring(0, num - 1)) != null)
			{
				if (!(a == "a"))
				{
					if (!(a == "u"))
					{
						if (a == "d")
						{
							this.type = GridBatchEditingCommandType.Delete;
						}
					}
					else
					{
						this.type = GridBatchEditingCommandType.Update;
					}
				}
				else
				{
					this.type = GridBatchEditingCommandType.Insert;
				}
			}
			this.item = (this.OwnerTableView.Items.FindByHierarchyIndex(this.arguments[0].ToString()) as GridDataItem);
			this.oldValues = new Hashtable();
			if (this.item != null)
			{
				GridItemType itemType = this.item.ItemType;
				this.OwnerTableView.ExtractValuesFromItem(this.oldValues, this.item);
				this.OwnerTableView.FillDataKeys(this.oldValues, this.item);
				this.PrepareOldValues(this.oldValues);
				this.FillNewValues();
				return;
			}
			if (this.type == GridBatchEditingCommandType.Insert)
			{
				if (this.OwnerTableView.Items.Count > 0)
				{
					this.item = this.OwnerTableView.Items[0];
				}
				this.FillEmptyOldValues();
				this.FillNewValues();
			}
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x0008A270 File Offset: 0x00088470
		private void PrepareOldValues(Hashtable values)
		{
			List<string> list = values.Keys.Cast<string>().ToList<string>();
			Dictionary<string, GridColumn> dictionary = new Dictionary<string, GridColumn>();
			foreach (GridColumn gridColumn in this.OwnerTableView.RenderColumns)
			{
				if (gridColumn.Visible && gridColumn.IsEditable)
				{
					string dataFieldFromColumnUniqueName = this.GetDataFieldFromColumnUniqueName(gridColumn.UniqueName);
					if (!dictionary.ContainsKey(dataFieldFromColumnUniqueName))
					{
						dictionary.Add(this.GetDataFieldFromColumnUniqueName(gridColumn.UniqueName), gridColumn);
					}
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				string key = list[j];
				if (dictionary.ContainsKey(key))
				{
					values[key] = this.PrepareValue(dictionary[key].DataType, values[key]);
				}
			}
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x0008A340 File Offset: 0x00088540
		private object PrepareValue(Type type, object value)
		{
			if (value == null || string.IsNullOrEmpty(value.ToString()))
			{
				return value;
			}
			if (type == typeof(int) || type == typeof(double) || type == typeof(decimal) || type == typeof(short) || type == typeof(int) || type == typeof(long) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong) || type == typeof(uint))
			{
				string text = Regex.Replace(value.ToString(), "[^-+.,0-9']", "");
				int num = text.IndexOf('.') - text.IndexOf(',');
				if (num > 0)
				{
					value = text.Replace(",", "");
				}
				else if (num == 0)
				{
					value = text;
				}
				else
				{
					value = text.Replace(".", "");
				}
			}
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			object result;
			try
			{
				object obj = converter.ConvertFromString(value.ToString());
				if (!string.IsNullOrEmpty(this.OwnerTableView.TimeZoneID) && type == typeof(DateTime) && obj != null)
				{
					DateTime local = (DateTime)obj;
					result = this.OwnerTableView.TimeZoneProvider.LocalToUtc(local);
				}
				else
				{
					result = (converter.ConvertFromString(value.ToString()) ?? value);
				}
			}
			catch
			{
				result = value;
			}
			return result;
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x0008A500 File Offset: 0x00088700
		protected virtual List<object> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x0008A508 File Offset: 0x00088708
		// (set) Token: 0x06002AB9 RID: 10937 RVA: 0x0008A510 File Offset: 0x00088710
		public bool Canceled
		{
			get
			{
				return this.canceled;
			}
			set
			{
				this.canceled = value;
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x0008A519 File Offset: 0x00088719
		public GridTableView OwnerTableView
		{
			get
			{
				return this.ownerTableView;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06002ABB RID: 10939 RVA: 0x0008A521 File Offset: 0x00088721
		public GridDataItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x0008A529 File Offset: 0x00088729
		public Hashtable OldValues
		{
			get
			{
				return this.oldValues;
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x0008A531 File Offset: 0x00088731
		public Hashtable NewValues
		{
			get
			{
				return this.newValues;
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x0008A539 File Offset: 0x00088739
		public virtual GridBatchEditingCommandType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x0008A544 File Offset: 0x00088744
		public virtual void Execute()
		{
			if (this.Canceled)
			{
				return;
			}
			GridCommandEventArgs gridCommandEventArgs = this.CreateGridComamndEventArgs();
			this.OwnerTableView.OwnerGrid.CallOnItemCommand(gridCommandEventArgs);
			switch (this.Type)
			{
			case GridBatchEditingCommandType.Insert:
				this.OwnerTableView.OwnerGrid.CallOnInsertCommand(gridCommandEventArgs);
				break;
			case GridBatchEditingCommandType.Update:
				this.OwnerTableView.OwnerGrid.CallOnUpdateCommand(gridCommandEventArgs);
				break;
			case GridBatchEditingCommandType.Delete:
				this.OwnerTableView.OwnerGrid.CallOnDeleteCommand(gridCommandEventArgs);
				break;
			}
			if (gridCommandEventArgs.Canceled)
			{
				return;
			}
			bool isUsingModelBinding = this.OwnerTableView.IsUsingModelBinding;
			switch (this.Type)
			{
			case GridBatchEditingCommandType.Insert:
				if (this.OwnerTableView.AllowAutomaticInserts || isUsingModelBinding)
				{
					this.PerformInsert();
					return;
				}
				break;
			case GridBatchEditingCommandType.Update:
				if (this.OwnerTableView.AllowAutomaticUpdates || isUsingModelBinding)
				{
					this.PerformUpdate(this.Item);
					return;
				}
				break;
			case GridBatchEditingCommandType.Delete:
				if (this.OwnerTableView.AllowAutomaticDeletes || isUsingModelBinding)
				{
					this.PerformDelete(this.Item);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x0008A648 File Offset: 0x00088848
		private GridCommandEventArgs CreateGridComamndEventArgs()
		{
			string name = null;
			GridItem gridItem = this.Item;
			switch (this.Type)
			{
			case GridBatchEditingCommandType.Insert:
			{
				name = "InitInsert";
				GridItem[] items = this.OwnerTableView.GetItems(new GridItemType[]
				{
					GridItemType.Header
				});
				if (items.Length > 0)
				{
					gridItem = items[0];
				}
				break;
			}
			case GridBatchEditingCommandType.Update:
				name = "Update";
				break;
			case GridBatchEditingCommandType.Delete:
				name = "Delete";
				break;
			}
			GridBatchEditingEventArgument argument = new GridBatchEditingEventArgument
			{
				NewValues = this.NewValues,
				OldValues = this.OldValues,
				OwnerTableView = this.OwnerTableView
			};
			return new GridCommandEventArgs(gridItem, null, name, argument);
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x0008A747 File Offset: 0x00088947
		private void FillEmptyOldValues()
		{
			GridBatchEditingHelper.CreateFakeEditableItem(this.OwnerTableView, delegate(GridEditableItem dataItem)
			{
				foreach (GridColumn gridColumn in this.OwnerTableView.RenderColumns)
				{
					IGridEditableColumn gridEditableColumn = gridColumn as IGridEditableColumn;
					if (gridEditableColumn != null && gridColumn.Visible && gridEditableColumn.ShouldExtractValues(dataItem))
					{
						gridEditableColumn.FillValues(this.OldValues, dataItem);
					}
				}
			});
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x0008A760 File Offset: 0x00088960
		private string GetDataFieldFromColumnUniqueName(string uniqueName)
		{
			GridColumn column = this.OwnerTableView.GetColumn(uniqueName);
			GridImageColumn gridImageColumn = column as GridImageColumn;
			if (gridImageColumn != null)
			{
				return gridImageColumn.DataImageUrlFields[0];
			}
			IGridDataColumn gridDataColumn = column as IGridDataColumn;
			if (gridDataColumn != null)
			{
				return gridDataColumn.GetActiveDataField();
			}
			return uniqueName;
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x0008A7A0 File Offset: 0x000889A0
		private object GetColumnValue(string uniqueName, object value)
		{
			GridColumn column = this.OwnerTableView.GetColumn(uniqueName);
			GridAttachmentColumn gridAttachmentColumn = column as GridAttachmentColumn;
			if (gridAttachmentColumn != null)
			{
				RadAsyncUpload asyncUpload = this.OwnerTableView.BatchEditingHelper.GetAsyncUpload(this.OwnerTableView.ClientID + column.UniqueName);
				using (IEnumerator enumerator = asyncUpload.UploadedFiles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						UploadedFile uploadedFile = (UploadedFile)obj;
						if (uploadedFile.FileName == value.ToString())
						{
							Stream inputStream = uploadedFile.InputStream;
							byte[] array = new byte[inputStream.Length];
							inputStream.Read(array, 0, (int)inputStream.Length);
							return array;
						}
					}
					return value;
				}
			}
			if (value != null && !string.IsNullOrEmpty(value.ToString()))
			{
				return this.PrepareValue(column.DataType, value);
			}
			return value;
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x0008A8A0 File Offset: 0x00088AA0
		private void FillNewValues()
		{
			this.newValues = new Hashtable(this.OldValues);
			for (int i = 1; i < this.arguments.Count; i += 2)
			{
				string text = this.arguments[i].ToString();
				if (!string.IsNullOrEmpty(text))
				{
					object columnValue = this.GetColumnValue(text, this.arguments[i + 1]);
					string text2 = this.GetDataFieldFromColumnUniqueName(text);
					if (string.IsNullOrEmpty(text2))
					{
						text2 = text;
					}
					this.newValues[text2] = columnValue;
					GridColumn column = this.OwnerTableView.GetColumn(text);
					if (column is GridCheckBoxColumn)
					{
						this.oldValues[text2] = !Convert.ToBoolean(columnValue);
					}
				}
			}
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x0008A95C File Offset: 0x00088B5C
		private void PerformInsert()
		{
			this.OwnerTableView.UpdateModelBindingProperties(this.dataSourceView, this.OwnerTableView.InsertMethod, "Inserting is not supported unless the InsertMethod is specified.");
			if (this.dataSourceView.CanInsert)
			{
				this.OwnerTableView.AddParentKeyRelationValues(this.NewValues);
				this.dataSourceView.Insert(this.NewValues, new DataSourceViewOperationCallback(new GridTableView.ItemInsertCallback(this.OwnerTableView, this.Item, true).HandleCallback));
				this.OwnerTableView.Validate();
			}
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x0008A9E4 File Offset: 0x00088BE4
		private void PerformUpdate(GridEditableItem editedItem)
		{
			this.OwnerTableView.UpdateModelBindingProperties(this.dataSourceView, this.OwnerTableView.UpdateMethod, "Updating is not supported unless the UpdateMethod is specified.");
			if (this.dataSourceView.CanUpdate)
			{
				Hashtable keys = new Hashtable();
				this.OwnerTableView.FillDataKeys(keys, editedItem);
				this.dataSourceView.Update(keys, this.NewValues, this.OldValues, new DataSourceViewOperationCallback(new GridTableView.ItemUpdateCallback(editedItem, true).HandleCallback));
			}
			this.OwnerTableView.Validate();
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x0008AA6C File Offset: 0x00088C6C
		private void PerformDelete(GridEditableItem editedItem)
		{
			this.OwnerTableView.UpdateModelBindingProperties(this.dataSourceView, this.OwnerTableView.DeleteMethod, "Deleting is not supported unless the DeleteMethod is specified.");
			if (this.dataSourceView.CanDelete)
			{
				Hashtable keys = new Hashtable();
				this.OwnerTableView.FillDataKeys(keys, editedItem);
				this.dataSourceView.Delete(keys, this.OldValues, new DataSourceViewOperationCallback(new GridTableView.ItemDeleteCallback(editedItem, true).HandleCallback));
			}
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x0008AAE0 File Offset: 0x00088CE0
		private void FillArgumnetsArray(string argumentsAsString)
		{
			this.arguments = new List<object>();
			string[] array = argumentsAsString.Split(new string[]
			{
				",.,"
			}, StringSplitOptions.None);
			foreach (string text in array)
			{
				string text2 = text.Trim();
				text2 = text2.Replace("&quot;", "\"");
				text2 = text2.Replace("&apos;", "'");
				text2 = text2.Replace("&#92;&#110;", Environment.NewLine);
				text2 = text2.Replace("&#92;", "\\");
				this.arguments.Add(text2);
			}
		}

		// Token: 0x04000B20 RID: 2848
		private bool canceled;

		// Token: 0x04000B21 RID: 2849
		private GridTableView ownerTableView;

		// Token: 0x04000B22 RID: 2850
		private GridBatchEditingCommandType type;

		// Token: 0x04000B23 RID: 2851
		private List<object> arguments;

		// Token: 0x04000B24 RID: 2852
		private DataSourceView dataSourceView;

		// Token: 0x04000B25 RID: 2853
		private GridDataItem item;

		// Token: 0x04000B26 RID: 2854
		private Hashtable oldValues;

		// Token: 0x04000B27 RID: 2855
		private Hashtable newValues;
	}
}
