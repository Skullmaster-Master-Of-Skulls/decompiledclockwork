using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.SqlClient
{
	// Token: 0x02000307 RID: 775
	[ListBindable(false)]
	[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class SqlParameterCollection : DbParameterCollection
	{
		// Token: 0x06002877 RID: 10359 RVA: 0x002B1058 File Offset: 0x002B0458
		internal SqlParameterCollection()
		{
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x002B1078 File Offset: 0x002B0478
		// (set) Token: 0x06002879 RID: 10361 RVA: 0x002B1098 File Offset: 0x002B0498
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

		// Token: 0x170006AD RID: 1709
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

		// Token: 0x170006AE RID: 1710
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

		// Token: 0x0600287E RID: 10366 RVA: 0x002B1138 File Offset: 0x002B0538
		public SqlParameter Add(SqlParameter value)
		{
			this.Add(value);
			return value;
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x002B1158 File Offset: 0x002B0558
		[Obsolete("Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value).  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public SqlParameter Add(string parameterName, object value)
		{
			return this.Add(new SqlParameter(parameterName, value));
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x002B1178 File Offset: 0x002B0578
		public SqlParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new SqlParameter(parameterName, value));
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x002B1198 File Offset: 0x002B0598
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType));
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x002B11B8 File Offset: 0x002B05B8
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType, size));
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x002B11D8 File Offset: 0x002B05D8
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size, string sourceColumn)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType, size, sourceColumn));
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x002B11F8 File Offset: 0x002B05F8
		public void AddRange(SqlParameter[] values)
		{
			this.AddRange(values);
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x002B1218 File Offset: 0x002B0618
		public override bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x002B1238 File Offset: 0x002B0638
		public bool Contains(SqlParameter value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x002B1258 File Offset: 0x002B0658
		public void CopyTo(SqlParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x002B1278 File Offset: 0x002B0678
		public int IndexOf(SqlParameter value)
		{
			return this.IndexOf(value);
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x002B1298 File Offset: 0x002B0698
		public void Insert(int index, SqlParameter value)
		{
			this.Insert(index, value);
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x002B12B8 File Offset: 0x002B06B8
		private void OnChange()
		{
			this.IsDirty = true;
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x002B12D8 File Offset: 0x002B06D8
		public void Remove(SqlParameter value)
		{
			this.Remove(value);
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600288C RID: 10380 RVA: 0x002B12F8 File Offset: 0x002B06F8
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

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x002B1328 File Offset: 0x002B0728
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

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600288E RID: 10382 RVA: 0x002B1358 File Offset: 0x002B0758
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x002B1378 File Offset: 0x002B0778
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x002B1398 File Offset: 0x002B0798
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x002B13B8 File Offset: 0x002B07B8
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x002B13D8 File Offset: 0x002B07D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((SqlParameter)value);
			return this.Count - 1;
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x002B1418 File Offset: 0x002B0818
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

		// Token: 0x06002894 RID: 10388 RVA: 0x002B14E8 File Offset: 0x002B08E8
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, SqlParameterCollection.ItemType);
			}
			return num;
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x002B1518 File Offset: 0x002B0918
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

		// Token: 0x06002896 RID: 10390 RVA: 0x002B1588 File Offset: 0x002B0988
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x002B15A8 File Offset: 0x002B09A8
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x002B15C8 File Offset: 0x002B09C8
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x002B15E8 File Offset: 0x002B09E8
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x002B1608 File Offset: 0x002B0A08
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, SqlParameterCollection.ItemType);
			}
			return this.InnerList[num];
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x002B1648 File Offset: 0x002B0A48
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

		// Token: 0x0600289C RID: 10396 RVA: 0x002B1738 File Offset: 0x002B0B38
		public override int IndexOf(string parameterName)
		{
			return SqlParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x002B1758 File Offset: 0x002B0B58
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

		// Token: 0x0600289E RID: 10398 RVA: 0x002B17A8 File Offset: 0x002B0BA8
		public override void Insert(int index, object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, (SqlParameter)value);
			this.InnerList.Insert(index, (SqlParameter)value);
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x002B17E8 File Offset: 0x002B0BE8
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ParametersMappingIndex(index, this);
			}
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x002B1818 File Offset: 0x002B0C18
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

		// Token: 0x060028A1 RID: 10401 RVA: 0x002B1868 File Offset: 0x002B0C68
		public override void RemoveAt(int index)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x002B1898 File Offset: 0x002B0C98
		public override void RemoveAt(string parameterName)
		{
			this.OnChange();
			int index = this.CheckName(parameterName);
			this.RemoveIndex(index);
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x002B18C8 File Offset: 0x002B0CC8
		private void RemoveIndex(int index)
		{
			List<SqlParameter> innerList = this.InnerList;
			SqlParameter sqlParameter = innerList[index];
			innerList.RemoveAt(index);
			sqlParameter.ResetParent();
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x002B18F8 File Offset: 0x002B0CF8
		private void Replace(int index, object newValue)
		{
			List<SqlParameter> innerList = this.InnerList;
			this.ValidateType(newValue);
			this.Validate(index, newValue);
			SqlParameter sqlParameter = innerList[index];
			innerList[index] = (SqlParameter)newValue;
			sqlParameter.ResetParent();
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x002B1938 File Offset: 0x002B0D38
		protected override void SetParameter(int index, DbParameter value)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x002B1968 File Offset: 0x002B0D68
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

		// Token: 0x060028A7 RID: 10407 RVA: 0x002B19A8 File Offset: 0x002B0DA8
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

		// Token: 0x060028A8 RID: 10408 RVA: 0x002B1A58 File Offset: 0x002B0E58
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

		// Token: 0x0400197A RID: 6522
		private bool _isDirty;

		// Token: 0x0400197B RID: 6523
		private static Type ItemType = typeof(SqlParameter);

		// Token: 0x0400197C RID: 6524
		private List<SqlParameter> _items;
	}
}
