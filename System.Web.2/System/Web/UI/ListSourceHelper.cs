using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI
{
	// Token: 0x020002BF RID: 703
	public static class ListSourceHelper
	{
		// Token: 0x06001FDF RID: 8159 RVA: 0x00065758 File Offset: 0x00063958
		public static bool ContainsListCollection(IDataSource dataSource)
		{
			ICollection viewNames = dataSource.GetViewNames();
			return viewNames != null && viewNames.Count > 0;
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x0006577C File Offset: 0x0006397C
		public static IList GetList(IDataSource dataSource)
		{
			ICollection viewNames = dataSource.GetViewNames();
			if (viewNames != null && viewNames.Count > 0)
			{
				return new ListSourceHelper.ListSourceList(dataSource);
			}
			return null;
		}

		// Token: 0x0200096E RID: 2414
		internal sealed class ListSourceList : CollectionBase, ITypedList
		{
			// Token: 0x06006A00 RID: 27136 RVA: 0x00178D3F File Offset: 0x00176F3F
			public ListSourceList(IDataSource dataSource)
			{
				this._dataSource = dataSource;
				((IList)this).Add(new ListSourceHelper.ListSourceRow(this._dataSource));
			}

			// Token: 0x06006A01 RID: 27137 RVA: 0x00028752 File Offset: 0x00026952
			string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
			{
				return string.Empty;
			}

			// Token: 0x06006A02 RID: 27138 RVA: 0x00178D60 File Offset: 0x00176F60
			PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
			{
				if (this._dataSource != null)
				{
					ICollection viewNames = this._dataSource.GetViewNames();
					if (viewNames != null && viewNames.Count > 0)
					{
						string[] array = new string[viewNames.Count];
						viewNames.CopyTo(array, 0);
						PropertyDescriptor[] array2 = new PropertyDescriptor[viewNames.Count];
						for (int i = 0; i < array.Length; i++)
						{
							array2[i] = new ListSourceHelper.ListSourcePropertyDescriptor(array[i]);
						}
						return new PropertyDescriptorCollection(array2);
					}
				}
				return new PropertyDescriptorCollection(null);
			}

			// Token: 0x0400385F RID: 14431
			private IDataSource _dataSource;
		}

		// Token: 0x0200096F RID: 2415
		internal class ListSourceRow
		{
			// Token: 0x06006A03 RID: 27139 RVA: 0x00178DD3 File Offset: 0x00176FD3
			public ListSourceRow(IDataSource dataSource)
			{
				this._dataSource = dataSource;
			}

			// Token: 0x17001D37 RID: 7479
			// (get) Token: 0x06006A04 RID: 27140 RVA: 0x00178DE2 File Offset: 0x00176FE2
			public IDataSource DataSource
			{
				get
				{
					return this._dataSource;
				}
			}

			// Token: 0x04003860 RID: 14432
			private IDataSource _dataSource;
		}

		// Token: 0x02000970 RID: 2416
		internal class ListSourcePropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x06006A05 RID: 27141 RVA: 0x00178DEA File Offset: 0x00176FEA
			public ListSourcePropertyDescriptor(string name) : base(name, null)
			{
				this._name = name;
			}

			// Token: 0x17001D38 RID: 7480
			// (get) Token: 0x06006A06 RID: 27142 RVA: 0x00178DFB File Offset: 0x00176FFB
			public override Type ComponentType
			{
				get
				{
					return typeof(ListSourceHelper.ListSourceRow);
				}
			}

			// Token: 0x17001D39 RID: 7481
			// (get) Token: 0x06006A07 RID: 27143 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001D3A RID: 7482
			// (get) Token: 0x06006A08 RID: 27144 RVA: 0x00178E07 File Offset: 0x00177007
			public override Type PropertyType
			{
				get
				{
					return typeof(IEnumerable);
				}
			}

			// Token: 0x06006A09 RID: 27145 RVA: 0x00007722 File Offset: 0x00005922
			public override bool CanResetValue(object value)
			{
				return false;
			}

			// Token: 0x06006A0A RID: 27146 RVA: 0x00178E14 File Offset: 0x00177014
			public override object GetValue(object source)
			{
				if (source is ListSourceHelper.ListSourceRow)
				{
					ListSourceHelper.ListSourceRow listSourceRow = (ListSourceHelper.ListSourceRow)source;
					IDataSource dataSource = listSourceRow.DataSource;
					return dataSource.GetView(this._name).ExecuteSelect(DataSourceSelectArguments.Empty);
				}
				return null;
			}

			// Token: 0x06006A0B RID: 27147 RVA: 0x00010D64 File Offset: 0x0000EF64
			public override void ResetValue(object component)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06006A0C RID: 27148 RVA: 0x00010D64 File Offset: 0x0000EF64
			public override void SetValue(object component, object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06006A0D RID: 27149 RVA: 0x00007722 File Offset: 0x00005922
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x04003861 RID: 14433
			private string _name;
		}
	}
}
