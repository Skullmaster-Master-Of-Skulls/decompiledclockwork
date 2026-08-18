using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Odbc
{
	// Token: 0x020001F9 RID: 505
	[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class OdbcParameterCollection : DbParameterCollection
	{
		// Token: 0x06001C13 RID: 7187 RVA: 0x00267F88 File Offset: 0x00267388
		internal OdbcParameterCollection()
		{
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x00267FA8 File Offset: 0x002673A8
		// (set) Token: 0x06001C15 RID: 7189 RVA: 0x00267FC8 File Offset: 0x002673C8
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

		// Token: 0x170003CB RID: 971
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

		// Token: 0x170003CC RID: 972
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

		// Token: 0x06001C1A RID: 7194 RVA: 0x00268068 File Offset: 0x00267468
		public OdbcParameter Add(OdbcParameter value)
		{
			this.Add(value);
			return value;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00268088 File Offset: 0x00267488
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value).  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		public OdbcParameter Add(string parameterName, object value)
		{
			return this.Add(new OdbcParameter(parameterName, value));
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x002680A8 File Offset: 0x002674A8
		public OdbcParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new OdbcParameter(parameterName, value));
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x002680C8 File Offset: 0x002674C8
		public OdbcParameter Add(string parameterName, OdbcType odbcType)
		{
			return this.Add(new OdbcParameter(parameterName, odbcType));
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x002680E8 File Offset: 0x002674E8
		public OdbcParameter Add(string parameterName, OdbcType odbcType, int size)
		{
			return this.Add(new OdbcParameter(parameterName, odbcType, size));
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00268108 File Offset: 0x00267508
		public OdbcParameter Add(string parameterName, OdbcType odbcType, int size, string sourceColumn)
		{
			return this.Add(new OdbcParameter(parameterName, odbcType, size, sourceColumn));
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x00268128 File Offset: 0x00267528
		public void AddRange(OdbcParameter[] values)
		{
			this.AddRange(values);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00268148 File Offset: 0x00267548
		internal void Bind(OdbcCommand command, CMDWrapper cmdWrapper, CNativeBuffer parameterBuffer)
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].Bind(cmdWrapper.StatementHandle, command, checked((short)(i + 1)), parameterBuffer, true);
			}
			this._rebindCollection = false;
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x00268188 File Offset: 0x00267588
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

		// Token: 0x06001C23 RID: 7203 RVA: 0x002681E8 File Offset: 0x002675E8
		internal void ClearBindings()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this[i].ClearBinding();
			}
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00268218 File Offset: 0x00267618
		public override bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00268238 File Offset: 0x00267638
		public bool Contains(OdbcParameter value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00268258 File Offset: 0x00267658
		public void CopyTo(OdbcParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x00268278 File Offset: 0x00267678
		private void OnChange()
		{
			this._rebindCollection = true;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00268298 File Offset: 0x00267698
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

		// Token: 0x06001C29 RID: 7209 RVA: 0x002682D8 File Offset: 0x002676D8
		public int IndexOf(OdbcParameter value)
		{
			return this.IndexOf(value);
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x002682F8 File Offset: 0x002676F8
		public void Insert(int index, OdbcParameter value)
		{
			this.Insert(index, value);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x00268318 File Offset: 0x00267718
		public void Remove(OdbcParameter value)
		{
			this.Remove(value);
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001C2C RID: 7212 RVA: 0x00268338 File Offset: 0x00267738
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

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x00268368 File Offset: 0x00267768
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

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x00268398 File Offset: 0x00267798
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x002683B8 File Offset: 0x002677B8
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x002683D8 File Offset: 0x002677D8
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x002683F8 File Offset: 0x002677F8
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00268418 File Offset: 0x00267818
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((OdbcParameter)value);
			return this.Count - 1;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00268458 File Offset: 0x00267858
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

		// Token: 0x06001C34 RID: 7220 RVA: 0x00268528 File Offset: 0x00267928
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, OdbcParameterCollection.ItemType);
			}
			return num;
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00268558 File Offset: 0x00267958
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

		// Token: 0x06001C36 RID: 7222 RVA: 0x002685C8 File Offset: 0x002679C8
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x002685E8 File Offset: 0x002679E8
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x00268608 File Offset: 0x00267A08
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x00268628 File Offset: 0x00267A28
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00268648 File Offset: 0x00267A48
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw ADP.ParametersSourceIndex(parameterName, this, OdbcParameterCollection.ItemType);
			}
			return this.InnerList[num];
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00268688 File Offset: 0x00267A88
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

		// Token: 0x06001C3C RID: 7228 RVA: 0x00268778 File Offset: 0x00267B78
		public override int IndexOf(string parameterName)
		{
			return OdbcParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00268798 File Offset: 0x00267B98
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

		// Token: 0x06001C3E RID: 7230 RVA: 0x002687E8 File Offset: 0x00267BE8
		public override void Insert(int index, object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, (OdbcParameter)value);
			this.InnerList.Insert(index, (OdbcParameter)value);
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x00268828 File Offset: 0x00267C28
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ParametersMappingIndex(index, this);
			}
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x00268858 File Offset: 0x00267C58
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

		// Token: 0x06001C41 RID: 7233 RVA: 0x002688A8 File Offset: 0x00267CA8
		public override void RemoveAt(int index)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x002688D8 File Offset: 0x00267CD8
		public override void RemoveAt(string parameterName)
		{
			this.OnChange();
			int index = this.CheckName(parameterName);
			this.RemoveIndex(index);
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00268908 File Offset: 0x00267D08
		private void RemoveIndex(int index)
		{
			List<OdbcParameter> innerList = this.InnerList;
			OdbcParameter odbcParameter = innerList[index];
			innerList.RemoveAt(index);
			odbcParameter.ResetParent();
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x00268938 File Offset: 0x00267D38
		private void Replace(int index, object newValue)
		{
			List<OdbcParameter> innerList = this.InnerList;
			this.ValidateType(newValue);
			this.Validate(index, newValue);
			OdbcParameter odbcParameter = innerList[index];
			innerList[index] = (OdbcParameter)newValue;
			odbcParameter.ResetParent();
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x00268978 File Offset: 0x00267D78
		protected override void SetParameter(int index, DbParameter value)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x002689A8 File Offset: 0x00267DA8
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

		// Token: 0x06001C47 RID: 7239 RVA: 0x002689E8 File Offset: 0x00267DE8
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

		// Token: 0x06001C48 RID: 7240 RVA: 0x00268A98 File Offset: 0x00267E98
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

		// Token: 0x0400106A RID: 4202
		private bool _rebindCollection;

		// Token: 0x0400106B RID: 4203
		private static Type ItemType = typeof(OdbcParameter);

		// Token: 0x0400106C RID: 4204
		private List<OdbcParameter> _items;
	}
}
