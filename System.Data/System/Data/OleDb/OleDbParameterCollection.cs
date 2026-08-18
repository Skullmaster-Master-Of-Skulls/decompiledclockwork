using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.OleDb
{
	// Token: 0x02000234 RID: 564
	[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class OleDbParameterCollection : DbParameterCollection
	{
		// Token: 0x06002000 RID: 8192 RVA: 0x0027E2B8 File Offset: 0x0027D6B8
		internal OleDbParameterCollection()
		{
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x0027E2D8 File Offset: 0x0027D6D8
		internal int ChangeID
		{
			get
			{
				return this._changeID;
			}
		}

		// Token: 0x17000469 RID: 1129
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public OleDbParameter this[int index]
		{
			get
			{
				return (OleDbParameter)this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		// Token: 0x1700046A RID: 1130
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public OleDbParameter this[string parameterName]
		{
			get
			{
				return (OleDbParameter)this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x0027E378 File Offset: 0x0027D778
		public OleDbParameter Add(OleDbParameter value)
		{
			this.Add(value);
			return value;
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x0027E398 File Offset: 0x0027D798
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value).  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		public OleDbParameter Add(string parameterName, object value)
		{
			return this.Add(new OleDbParameter(parameterName, value));
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x0027E3B8 File Offset: 0x0027D7B8
		public OleDbParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new OleDbParameter(parameterName, value));
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x0027E3D8 File Offset: 0x0027D7D8
		public OleDbParameter Add(string parameterName, OleDbType oleDbType)
		{
			return this.Add(new OleDbParameter(parameterName, oleDbType));
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0027E3F8 File Offset: 0x0027D7F8
		public OleDbParameter Add(string parameterName, OleDbType oleDbType, int size)
		{
			return this.Add(new OleDbParameter(parameterName, oleDbType, size));
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x0027E418 File Offset: 0x0027D818
		public OleDbParameter Add(string parameterName, OleDbType oleDbType, int size, string sourceColumn)
		{
			return this.Add(new OleDbParameter(parameterName, oleDbType, size, sourceColumn));
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x0027E438 File Offset: 0x0027D838
		public void AddRange(OleDbParameter[] values)
		{
			this.AddRange(values);
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x0027E458 File Offset: 0x0027D858
		public override bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x0027E478 File Offset: 0x0027D878
		public bool Contains(OleDbParameter value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x0027E498 File Offset: 0x0027D898
		public void CopyTo(OleDbParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x0027E4B8 File Offset: 0x0027D8B8
		public int IndexOf(OleDbParameter value)
		{
			return this.IndexOf(value);
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0027E4D8 File Offset: 0x0027D8D8
		public void Insert(int index, OleDbParameter value)
		{
			this.Insert(index, value);
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0027E4F8 File Offset: 0x0027D8F8
		private void OnChange()
		{
			this._changeID++;
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x0027E518 File Offset: 0x0027D918
		public void Remove(OleDbParameter value)
		{
			this.Remove(value);
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x0027E538 File Offset: 0x0027D938
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

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x0027E568 File Offset: 0x0027D968
		private List<OleDbParameter> InnerList
		{
			get
			{
				List<OleDbParameter> list = this._items;
				if (list == null)
				{
					list = new List<OleDbParameter>();
					this._items = list;
				}
				return list;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x0027E598 File Offset: 0x0027D998
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x0027E5B8 File Offset: 0x0027D9B8
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x0027E5D8 File Offset: 0x0027D9D8
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x0027E5F8 File Offset: 0x0027D9F8
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x0027E618 File Offset: 0x0027DA18
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((OleDbParameter)value);
			return this.Count - 1;
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x0027E658 File Offset: 0x0027DA58
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
				OleDbParameter oleDbParameter = (OleDbParameter)obj;
				this.Validate(-1, oleDbParameter);
				this.InnerList.Add(oleDbParameter);
			}
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0027E728 File Offset: 0x0027DB28
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, OleDbParameterCollection.ItemType);
			}
			return num;
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x0027E758 File Offset: 0x0027DB58
		public override void Clear()
		{
			this.OnChange();
			List<OleDbParameter> innerList = this.InnerList;
			if (innerList != null)
			{
				foreach (OleDbParameter oleDbParameter in innerList)
				{
					oleDbParameter.ResetParent();
				}
				innerList.Clear();
			}
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x0027E7C8 File Offset: 0x0027DBC8
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x0027E7E8 File Offset: 0x0027DBE8
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x0027E808 File Offset: 0x0027DC08
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0027E828 File Offset: 0x0027DC28
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x0027E848 File Offset: 0x0027DC48
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, OleDbParameterCollection.ItemType);
			}
			return this.InnerList[num];
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x0027E888 File Offset: 0x0027DC88
		private static int IndexOf(IEnumerable items, string parameterName)
		{
			if (items != null)
			{
				int num = 0;
				foreach (object obj in items)
				{
					OleDbParameter oleDbParameter = (OleDbParameter)obj;
					if (ADP.SrcCompare(parameterName, oleDbParameter.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				num = 0;
				foreach (object obj2 in items)
				{
					OleDbParameter oleDbParameter2 = (OleDbParameter)obj2;
					if (ADP.DstCompare(parameterName, oleDbParameter2.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x0027E978 File Offset: 0x0027DD78
		public override int IndexOf(string parameterName)
		{
			return OleDbParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x0027E998 File Offset: 0x0027DD98
		public override int IndexOf(object value)
		{
			if (value != null)
			{
				this.ValidateType(value);
				List<OleDbParameter> innerList = this.InnerList;
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

		// Token: 0x06002026 RID: 8230 RVA: 0x0027E9E8 File Offset: 0x0027DDE8
		public override void Insert(int index, object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, (OleDbParameter)value);
			this.InnerList.Insert(index, (OleDbParameter)value);
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x0027EA28 File Offset: 0x0027DE28
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ParametersMappingIndex(index, this);
			}
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x0027EA58 File Offset: 0x0027DE58
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
			if (this != ((OleDbParameter)value).CompareExchangeParent(null, this))
			{
				throw ADP.CollectionRemoveInvalidObject(OleDbParameterCollection.ItemType, this);
			}
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x0027EAA8 File Offset: 0x0027DEA8
		public override void RemoveAt(int index)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x0027EAD8 File Offset: 0x0027DED8
		public override void RemoveAt(string parameterName)
		{
			this.OnChange();
			int index = this.CheckName(parameterName);
			this.RemoveIndex(index);
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x0027EB08 File Offset: 0x0027DF08
		private void RemoveIndex(int index)
		{
			List<OleDbParameter> innerList = this.InnerList;
			OleDbParameter oleDbParameter = innerList[index];
			innerList.RemoveAt(index);
			oleDbParameter.ResetParent();
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x0027EB38 File Offset: 0x0027DF38
		private void Replace(int index, object newValue)
		{
			List<OleDbParameter> innerList = this.InnerList;
			this.ValidateType(newValue);
			this.Validate(index, newValue);
			OleDbParameter oleDbParameter = innerList[index];
			innerList[index] = (OleDbParameter)newValue;
			oleDbParameter.ResetParent();
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x0027EB78 File Offset: 0x0027DF78
		protected override void SetParameter(int index, DbParameter value)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x0027EBA8 File Offset: 0x0027DFA8
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.OnChange();
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, OleDbParameterCollection.ItemType);
			}
			this.Replace(num, value);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x0027EBE8 File Offset: 0x0027DFE8
		private void Validate(int index, object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, OleDbParameterCollection.ItemType);
			}
			object obj = ((OleDbParameter)value).CompareExchangeParent(this, null);
			if (obj != null)
			{
				if (this != obj)
				{
					throw ADP.ParametersIsNotParent(OleDbParameterCollection.ItemType, this);
				}
				if (index != this.IndexOf(value))
				{
					throw ADP.ParametersIsParent(OleDbParameterCollection.ItemType, this);
				}
			}
			string text = ((OleDbParameter)value).ParameterName;
			if (text.Length == 0)
			{
				index = 1;
				do
				{
					text = "Parameter" + index.ToString(CultureInfo.CurrentCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				((OleDbParameter)value).ParameterName = text;
			}
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x0027EC98 File Offset: 0x0027E098
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, OleDbParameterCollection.ItemType);
			}
			if (!OleDbParameterCollection.ItemType.IsInstanceOfType(value))
			{
				throw ADP.InvalidParameterType(this, OleDbParameterCollection.ItemType, value);
			}
		}

		// Token: 0x04001455 RID: 5205
		private int _changeID;

		// Token: 0x04001456 RID: 5206
		private static Type ItemType = typeof(OleDbParameter);

		// Token: 0x04001457 RID: 5207
		private List<OleDbParameter> _items;
	}
}
