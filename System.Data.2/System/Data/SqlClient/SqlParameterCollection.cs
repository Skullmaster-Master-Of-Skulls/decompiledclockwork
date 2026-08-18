using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.SqlClient
{
	// Token: 0x020001EF RID: 495
	[ListBindable(false)]
	[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class SqlParameterCollection : DbParameterCollection
	{
		// Token: 0x06001ED2 RID: 7890 RVA: 0x000D7390 File Offset: 0x000D6790
		internal SqlParameterCollection()
		{
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x000D73A4 File Offset: 0x000D67A4
		// (set) Token: 0x06001ED4 RID: 7892 RVA: 0x000D73B8 File Offset: 0x000D67B8
		internal bool IsDirty
		{
			get
			{
				return this._isDirty;
			}
			set
			{
				this._isDirty = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SqlParameter this[int index]
		{
			get
			{
				return (SqlParameter)this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		// Token: 0x17000512 RID: 1298
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SqlParameter this[string parameterName]
		{
			get
			{
				return (SqlParameter)this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x000D7434 File Offset: 0x000D6834
		public SqlParameter Add(SqlParameter value)
		{
			this.Add(value);
			return value;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x000D744C File Offset: 0x000D684C
		[Obsolete("Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value).  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public SqlParameter Add(string parameterName, object value)
		{
			return this.Add(new SqlParameter(parameterName, value));
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x000D7468 File Offset: 0x000D6868
		public SqlParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new SqlParameter(parameterName, value));
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x000D7484 File Offset: 0x000D6884
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType));
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x000D74A0 File Offset: 0x000D68A0
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType, size));
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x000D74BC File Offset: 0x000D68BC
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size, string sourceColumn)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType, size, sourceColumn));
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x000D74DC File Offset: 0x000D68DC
		public void AddRange(SqlParameter[] values)
		{
			this.AddRange(values);
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x000D74F0 File Offset: 0x000D68F0
		public override bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x000D750C File Offset: 0x000D690C
		public bool Contains(SqlParameter value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x000D7528 File Offset: 0x000D6928
		public void CopyTo(SqlParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x000D7540 File Offset: 0x000D6940
		public int IndexOf(SqlParameter value)
		{
			return this.IndexOf(value);
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x000D7554 File Offset: 0x000D6954
		public void Insert(int index, SqlParameter value)
		{
			this.Insert(index, value);
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000D756C File Offset: 0x000D696C
		private void OnChange()
		{
			this.IsDirty = true;
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x000D7580 File Offset: 0x000D6980
		public void Remove(SqlParameter value)
		{
			this.Remove(value);
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x000D7594 File Offset: 0x000D6994
		public override int Count
		{
			get
			{
				if (this._items == null)
				{
					return 0;
				}
				return this._items.Count;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x000D75B8 File Offset: 0x000D69B8
		private List<SqlParameter> InnerList
		{
			get
			{
				List<SqlParameter> list = this._items;
				if (list == null)
				{
					list = new List<SqlParameter>();
					this._items = list;
				}
				return list;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x000D75E0 File Offset: 0x000D69E0
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x000D75F8 File Offset: 0x000D69F8
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x000D7610 File Offset: 0x000D6A10
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x000D7628 File Offset: 0x000D6A28
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000D7640 File Offset: 0x000D6A40
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((SqlParameter)value);
			return this.Count - 1;
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x000D767C File Offset: 0x000D6A7C
		public override void AddRange(Array values)
		{
			this.OnChange();
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			foreach (object value in values)
			{
				this.ValidateType(value);
			}
			foreach (object obj in values)
			{
				SqlParameter sqlParameter = (SqlParameter)obj;
				this.Validate(-1, sqlParameter);
				this.InnerList.Add(sqlParameter);
			}
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x000D774C File Offset: 0x000D6B4C
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, SqlParameterCollection.ItemType);
			}
			return num;
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x000D7774 File Offset: 0x000D6B74
		public override void Clear()
		{
			this.OnChange();
			List<SqlParameter> innerList = this.InnerList;
			if (innerList != null)
			{
				foreach (SqlParameter sqlParameter in innerList)
				{
					sqlParameter.ResetParent();
				}
				innerList.Clear();
			}
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x000D77E4 File Offset: 0x000D6BE4
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x000D7800 File Offset: 0x000D6C00
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x000D781C File Offset: 0x000D6C1C
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000D7834 File Offset: 0x000D6C34
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x000D7854 File Offset: 0x000D6C54
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, SqlParameterCollection.ItemType);
			}
			return this.InnerList[num];
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x000D7888 File Offset: 0x000D6C88
		private static int IndexOf(IEnumerable items, string parameterName)
		{
			if (items != null)
			{
				int num = 0;
				foreach (object obj in items)
				{
					SqlParameter sqlParameter = (SqlParameter)obj;
					if (ADP.SrcCompare(parameterName, sqlParameter.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				num = 0;
				foreach (object obj2 in items)
				{
					SqlParameter sqlParameter2 = (SqlParameter)obj2;
					if (ADP.DstCompare(parameterName, sqlParameter2.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x000D796C File Offset: 0x000D6D6C
		public override int IndexOf(string parameterName)
		{
			return SqlParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x000D7988 File Offset: 0x000D6D88
		public override int IndexOf(object value)
		{
			if (value != null)
			{
				this.ValidateType(value);
				List<SqlParameter> innerList = this.InnerList;
				if (innerList != null)
				{
					int count = innerList.Count;
					for (int i = 0; i < count; i++)
					{
						if (value == innerList[i])
						{
							return i;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x000D79CC File Offset: 0x000D6DCC
		public override void Insert(int index, object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, (SqlParameter)value);
			this.InnerList.Insert(index, (SqlParameter)value);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x000D7A08 File Offset: 0x000D6E08
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ParametersMappingIndex(index, this);
			}
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x000D7A2C File Offset: 0x000D6E2C
		public override void Remove(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			if (this != ((SqlParameter)value).CompareExchangeParent(null, this))
			{
				throw ADP.CollectionRemoveInvalidObject(SqlParameterCollection.ItemType, this);
			}
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x000D7A78 File Offset: 0x000D6E78
		public override void RemoveAt(int index)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000D7A9C File Offset: 0x000D6E9C
		public override void RemoveAt(string parameterName)
		{
			this.OnChange();
			int index = this.CheckName(parameterName);
			this.RemoveIndex(index);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x000D7AC0 File Offset: 0x000D6EC0
		private void RemoveIndex(int index)
		{
			List<SqlParameter> innerList = this.InnerList;
			SqlParameter sqlParameter = innerList[index];
			innerList.RemoveAt(index);
			sqlParameter.ResetParent();
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000D7AEC File Offset: 0x000D6EEC
		private void Replace(int index, object newValue)
		{
			List<SqlParameter> innerList = this.InnerList;
			this.ValidateType(newValue);
			this.Validate(index, newValue);
			SqlParameter sqlParameter = innerList[index];
			innerList[index] = (SqlParameter)newValue;
			sqlParameter.ResetParent();
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000D7B2C File Offset: 0x000D6F2C
		protected override void SetParameter(int index, DbParameter value)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000D7B50 File Offset: 0x000D6F50
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.OnChange();
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, SqlParameterCollection.ItemType);
			}
			this.Replace(num, value);
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x000D7B84 File Offset: 0x000D6F84
		private void Validate(int index, object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, SqlParameterCollection.ItemType);
			}
			object obj = ((SqlParameter)value).CompareExchangeParent(this, null);
			if (obj != null)
			{
				if (this != obj)
				{
					throw ADP.ParametersIsNotParent(SqlParameterCollection.ItemType, this);
				}
				if (index != this.IndexOf(value))
				{
					throw ADP.ParametersIsParent(SqlParameterCollection.ItemType, this);
				}
			}
			string text = ((SqlParameter)value).ParameterName;
			if (text.Length == 0)
			{
				index = 1;
				do
				{
					text = "Parameter" + index.ToString(CultureInfo.CurrentCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				((SqlParameter)value).ParameterName = text;
			}
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000D7C28 File Offset: 0x000D7028
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, SqlParameterCollection.ItemType);
			}
			if (!SqlParameterCollection.ItemType.IsInstanceOfType(value))
			{
				throw ADP.InvalidParameterType(this, SqlParameterCollection.ItemType, value);
			}
		}

		// Token: 0x04001197 RID: 4503
		private bool _isDirty;

		// Token: 0x04001198 RID: 4504
		private static Type ItemType = typeof(SqlParameter);

		// Token: 0x04001199 RID: 4505
		private List<SqlParameter> _items;
	}
}
