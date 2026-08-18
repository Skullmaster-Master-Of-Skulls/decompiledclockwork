using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.OleDb
{
	// Token: 0x0200025A RID: 602
	[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class OleDbParameterCollection : DbParameterCollection
	{
		// Token: 0x06002612 RID: 9746 RVA: 0x00102E4C File Offset: 0x0010224C
		internal OleDbParameterCollection()
		{
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x00102E60 File Offset: 0x00102260
		internal int ChangeID
		{
			get
			{
				return this._changeID;
			}
		}

		// Token: 0x17000632 RID: 1586
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x17000633 RID: 1587
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x06002618 RID: 9752 RVA: 0x00102EDC File Offset: 0x001022DC
		public OleDbParameter Add(OleDbParameter value)
		{
			this.Add(value);
			return value;
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x00102EF4 File Offset: 0x001022F4
		[Obsolete("Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value).  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public OleDbParameter Add(string parameterName, object value)
		{
			return this.Add(new OleDbParameter(parameterName, value));
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x00102F10 File Offset: 0x00102310
		public OleDbParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new OleDbParameter(parameterName, value));
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x00102F2C File Offset: 0x0010232C
		public OleDbParameter Add(string parameterName, OleDbType oleDbType)
		{
			return this.Add(new OleDbParameter(parameterName, oleDbType));
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x00102F48 File Offset: 0x00102348
		public OleDbParameter Add(string parameterName, OleDbType oleDbType, int size)
		{
			return this.Add(new OleDbParameter(parameterName, oleDbType, size));
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x00102F64 File Offset: 0x00102364
		public OleDbParameter Add(string parameterName, OleDbType oleDbType, int size, string sourceColumn)
		{
			return this.Add(new OleDbParameter(parameterName, oleDbType, size, sourceColumn));
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x00102F84 File Offset: 0x00102384
		public void AddRange(OleDbParameter[] values)
		{
			this.AddRange(values);
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x00102F98 File Offset: 0x00102398
		public override bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x00102FB4 File Offset: 0x001023B4
		public bool Contains(OleDbParameter value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x00102FD0 File Offset: 0x001023D0
		public void CopyTo(OleDbParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x00102FE8 File Offset: 0x001023E8
		public int IndexOf(OleDbParameter value)
		{
			return this.IndexOf(value);
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x00102FFC File Offset: 0x001023FC
		public void Insert(int index, OleDbParameter value)
		{
			this.Insert(index, value);
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x00103014 File Offset: 0x00102414
		private void OnChange()
		{
			this._changeID++;
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x00103030 File Offset: 0x00102430
		public void Remove(OleDbParameter value)
		{
			this.Remove(value);
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x00103044 File Offset: 0x00102444
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

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x00103068 File Offset: 0x00102468
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

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x00103090 File Offset: 0x00102490
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x001030A8 File Offset: 0x001024A8
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x001030C0 File Offset: 0x001024C0
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x001030D8 File Offset: 0x001024D8
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x001030F0 File Offset: 0x001024F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((OleDbParameter)value);
			return this.Count - 1;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x0010312C File Offset: 0x0010252C
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

		// Token: 0x0600262E RID: 9774 RVA: 0x001031FC File Offset: 0x001025FC
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, OleDbParameterCollection.ItemType);
			}
			return num;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x00103224 File Offset: 0x00102624
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

		// Token: 0x06002630 RID: 9776 RVA: 0x00103294 File Offset: 0x00102694
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x001032B0 File Offset: 0x001026B0
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x001032CC File Offset: 0x001026CC
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x001032E4 File Offset: 0x001026E4
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x00103304 File Offset: 0x00102704
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, OleDbParameterCollection.ItemType);
			}
			return this.InnerList[num];
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x00103338 File Offset: 0x00102738
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

		// Token: 0x06002636 RID: 9782 RVA: 0x0010341C File Offset: 0x0010281C
		public override int IndexOf(string parameterName)
		{
			return OleDbParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00103438 File Offset: 0x00102838
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

		// Token: 0x06002638 RID: 9784 RVA: 0x0010347C File Offset: 0x0010287C
		public override void Insert(int index, object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, (OleDbParameter)value);
			this.InnerList.Insert(index, (OleDbParameter)value);
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x001034B8 File Offset: 0x001028B8
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ParametersMappingIndex(index, this);
			}
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x001034DC File Offset: 0x001028DC
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

		// Token: 0x0600263B RID: 9787 RVA: 0x00103528 File Offset: 0x00102928
		public override void RemoveAt(int index)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x0010354C File Offset: 0x0010294C
		public override void RemoveAt(string parameterName)
		{
			this.OnChange();
			int index = this.CheckName(parameterName);
			this.RemoveIndex(index);
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x00103570 File Offset: 0x00102970
		private void RemoveIndex(int index)
		{
			List<OleDbParameter> innerList = this.InnerList;
			OleDbParameter oleDbParameter = innerList[index];
			innerList.RemoveAt(index);
			oleDbParameter.ResetParent();
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x0010359C File Offset: 0x0010299C
		private void Replace(int index, object newValue)
		{
			List<OleDbParameter> innerList = this.InnerList;
			this.ValidateType(newValue);
			this.Validate(index, newValue);
			OleDbParameter oleDbParameter = innerList[index];
			innerList[index] = (OleDbParameter)newValue;
			oleDbParameter.ResetParent();
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x001035DC File Offset: 0x001029DC
		protected override void SetParameter(int index, DbParameter value)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x00103600 File Offset: 0x00102A00
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

		// Token: 0x06002641 RID: 9793 RVA: 0x00103634 File Offset: 0x00102A34
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

		// Token: 0x06002642 RID: 9794 RVA: 0x001036D8 File Offset: 0x00102AD8
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

		// Token: 0x04001768 RID: 5992
		private int _changeID;

		// Token: 0x04001769 RID: 5993
		private static Type ItemType = typeof(OleDbParameter);

		// Token: 0x0400176A RID: 5994
		private List<OleDbParameter> _items;
	}
}
