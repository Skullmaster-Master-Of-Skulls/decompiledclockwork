using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Odbc
{
	// Token: 0x020002A7 RID: 679
	[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class OdbcParameterCollection : DbParameterCollection
	{
		// Token: 0x06002965 RID: 10597 RVA: 0x00113FA8 File Offset: 0x001133A8
		internal OdbcParameterCollection()
		{
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x00113FBC File Offset: 0x001133BC
		// (set) Token: 0x06002967 RID: 10599 RVA: 0x00113FD0 File Offset: 0x001133D0
		internal bool RebindCollection
		{
			get
			{
				return this._rebindCollection;
			}
			set
			{
				this._rebindCollection = value;
			}
		}

		// Token: 0x170006C6 RID: 1734
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public OdbcParameter this[int index]
		{
			get
			{
				return (OdbcParameter)this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		// Token: 0x170006C7 RID: 1735
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public OdbcParameter this[string parameterName]
		{
			get
			{
				return (OdbcParameter)this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0011404C File Offset: 0x0011344C
		public OdbcParameter Add(OdbcParameter value)
		{
			this.Add(value);
			return value;
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x00114064 File Offset: 0x00113464
		[Obsolete("Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value).  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public OdbcParameter Add(string parameterName, object value)
		{
			return this.Add(new OdbcParameter(parameterName, value));
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x00114080 File Offset: 0x00113480
		public OdbcParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new OdbcParameter(parameterName, value));
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x0011409C File Offset: 0x0011349C
		public OdbcParameter Add(string parameterName, OdbcType odbcType)
		{
			return this.Add(new OdbcParameter(parameterName, odbcType));
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x001140B8 File Offset: 0x001134B8
		public OdbcParameter Add(string parameterName, OdbcType odbcType, int size)
		{
			return this.Add(new OdbcParameter(parameterName, odbcType, size));
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x001140D4 File Offset: 0x001134D4
		public OdbcParameter Add(string parameterName, OdbcType odbcType, int size, string sourceColumn)
		{
			return this.Add(new OdbcParameter(parameterName, odbcType, size, sourceColumn));
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x001140F4 File Offset: 0x001134F4
		public void AddRange(OdbcParameter[] values)
		{
			this.AddRange(values);
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x00114108 File Offset: 0x00113508
		internal void Bind(OdbcCommand command, CMDWrapper cmdWrapper, CNativeBuffer parameterBuffer)
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].Bind(cmdWrapper.StatementHandle, command, checked((short)(i + 1)), parameterBuffer, true);
			}
			this._rebindCollection = false;
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x00114148 File Offset: 0x00113548
		internal int CalcParameterBufferSize(OdbcCommand command)
		{
			int num = 0;
			for (int i = 0; i < this.Count; i++)
			{
				if (this._rebindCollection)
				{
					this[i].HasChanged = true;
				}
				this[i].PrepareForBind(command, (short)(i + 1), ref num);
				num = (num + (IntPtr.Size - 1) & ~(IntPtr.Size - 1));
			}
			return num;
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x001141A4 File Offset: 0x001135A4
		internal void ClearBindings()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].ClearBinding();
			}
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x001141D0 File Offset: 0x001135D0
		public override bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x001141EC File Offset: 0x001135EC
		public bool Contains(OdbcParameter value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x00114208 File Offset: 0x00113608
		public void CopyTo(OdbcParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x00114220 File Offset: 0x00113620
		private void OnChange()
		{
			this._rebindCollection = true;
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x00114234 File Offset: 0x00113634
		internal void GetOutputValues(CMDWrapper cmdWrapper)
		{
			if (!this._rebindCollection)
			{
				CNativeBuffer nativeParameterBuffer = cmdWrapper._nativeParameterBuffer;
				for (int i = 0; i < this.Count; i++)
				{
					this[i].GetOutputValue(nativeParameterBuffer);
				}
			}
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x00114270 File Offset: 0x00113670
		public int IndexOf(OdbcParameter value)
		{
			return this.IndexOf(value);
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x00114284 File Offset: 0x00113684
		public void Insert(int index, OdbcParameter value)
		{
			this.Insert(index, value);
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x0011429C File Offset: 0x0011369C
		public void Remove(OdbcParameter value)
		{
			this.Remove(value);
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x001142B0 File Offset: 0x001136B0
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

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x001142D4 File Offset: 0x001136D4
		private List<OdbcParameter> InnerList
		{
			get
			{
				List<OdbcParameter> list = this._items;
				if (list == null)
				{
					list = new List<OdbcParameter>();
					this._items = list;
				}
				return list;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x001142FC File Offset: 0x001136FC
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x00114314 File Offset: 0x00113714
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x0011432C File Offset: 0x0011372C
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x00114344 File Offset: 0x00113744
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x0011435C File Offset: 0x0011375C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((OdbcParameter)value);
			return this.Count - 1;
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x00114398 File Offset: 0x00113798
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
				OdbcParameter odbcParameter = (OdbcParameter)obj;
				this.Validate(-1, odbcParameter);
				this.InnerList.Add(odbcParameter);
			}
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x00114468 File Offset: 0x00113868
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, OdbcParameterCollection.ItemType);
			}
			return num;
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x00114490 File Offset: 0x00113890
		public override void Clear()
		{
			this.OnChange();
			List<OdbcParameter> innerList = this.InnerList;
			if (innerList != null)
			{
				foreach (OdbcParameter odbcParameter in innerList)
				{
					odbcParameter.ResetParent();
				}
				innerList.Clear();
			}
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x00114500 File Offset: 0x00113900
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x0011451C File Offset: 0x0011391C
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x00114538 File Offset: 0x00113938
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x00114550 File Offset: 0x00113950
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x00114570 File Offset: 0x00113970
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, OdbcParameterCollection.ItemType);
			}
			return this.InnerList[num];
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x001145A4 File Offset: 0x001139A4
		private static int IndexOf(IEnumerable items, string parameterName)
		{
			if (items != null)
			{
				int num = 0;
				foreach (object obj in items)
				{
					OdbcParameter odbcParameter = (OdbcParameter)obj;
					if (ADP.SrcCompare(parameterName, odbcParameter.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				num = 0;
				foreach (object obj2 in items)
				{
					OdbcParameter odbcParameter2 = (OdbcParameter)obj2;
					if (ADP.DstCompare(parameterName, odbcParameter2.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x00114688 File Offset: 0x00113A88
		public override int IndexOf(string parameterName)
		{
			return OdbcParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x001146A4 File Offset: 0x00113AA4
		public override int IndexOf(object value)
		{
			if (value != null)
			{
				this.ValidateType(value);
				List<OdbcParameter> innerList = this.InnerList;
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

		// Token: 0x06002990 RID: 10640 RVA: 0x001146E8 File Offset: 0x00113AE8
		public override void Insert(int index, object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, (OdbcParameter)value);
			this.InnerList.Insert(index, (OdbcParameter)value);
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x00114724 File Offset: 0x00113B24
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ParametersMappingIndex(index, this);
			}
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x00114748 File Offset: 0x00113B48
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
			if (this != ((OdbcParameter)value).CompareExchangeParent(null, this))
			{
				throw ADP.CollectionRemoveInvalidObject(OdbcParameterCollection.ItemType, this);
			}
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x00114794 File Offset: 0x00113B94
		public override void RemoveAt(int index)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x001147B8 File Offset: 0x00113BB8
		public override void RemoveAt(string parameterName)
		{
			this.OnChange();
			int index = this.CheckName(parameterName);
			this.RemoveIndex(index);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x001147DC File Offset: 0x00113BDC
		private void RemoveIndex(int index)
		{
			List<OdbcParameter> innerList = this.InnerList;
			OdbcParameter odbcParameter = innerList[index];
			innerList.RemoveAt(index);
			odbcParameter.ResetParent();
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x00114808 File Offset: 0x00113C08
		private void Replace(int index, object newValue)
		{
			List<OdbcParameter> innerList = this.InnerList;
			this.ValidateType(newValue);
			this.Validate(index, newValue);
			OdbcParameter odbcParameter = innerList[index];
			innerList[index] = (OdbcParameter)newValue;
			odbcParameter.ResetParent();
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x00114848 File Offset: 0x00113C48
		protected override void SetParameter(int index, DbParameter value)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x0011486C File Offset: 0x00113C6C
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.OnChange();
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, OdbcParameterCollection.ItemType);
			}
			this.Replace(num, value);
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x001148A0 File Offset: 0x00113CA0
		private void Validate(int index, object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, OdbcParameterCollection.ItemType);
			}
			object obj = ((OdbcParameter)value).CompareExchangeParent(this, null);
			if (obj != null)
			{
				if (this != obj)
				{
					throw ADP.ParametersIsNotParent(OdbcParameterCollection.ItemType, this);
				}
				if (index != this.IndexOf(value))
				{
					throw ADP.ParametersIsParent(OdbcParameterCollection.ItemType, this);
				}
			}
			string text = ((OdbcParameter)value).ParameterName;
			if (text.Length == 0)
			{
				index = 1;
				do
				{
					text = "Parameter" + index.ToString(CultureInfo.CurrentCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				((OdbcParameter)value).ParameterName = text;
			}
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x00114944 File Offset: 0x00113D44
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw ADP.ParameterNull("value", this, OdbcParameterCollection.ItemType);
			}
			if (!OdbcParameterCollection.ItemType.IsInstanceOfType(value))
			{
				throw ADP.InvalidParameterType(this, OdbcParameterCollection.ItemType, value);
			}
		}

		// Token: 0x04001AE4 RID: 6884
		private bool _rebindCollection;

		// Token: 0x04001AE5 RID: 6885
		private static Type ItemType = typeof(OdbcParameter);

		// Token: 0x04001AE6 RID: 6886
		private List<OdbcParameter> _items;
	}
}
