using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.EntityClient
{
	// Token: 0x0200011B RID: 283
	public sealed class EntityParameterCollection : DbParameterCollection
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0003E186 File Offset: 0x0003C386
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

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0003E1A0 File Offset: 0x0003C3A0
		private List<EntityParameter> InnerList
		{
			get
			{
				List<EntityParameter> list = this._items;
				if (list == null)
				{
					list = new List<EntityParameter>();
					this._items = list;
				}
				return list;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0003E1C5 File Offset: 0x0003C3C5
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0003E1D2 File Offset: 0x0003C3D2
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0003E1DF File Offset: 0x0003C3DF
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0003E1EC File Offset: 0x0003C3EC
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0003E1F9 File Offset: 0x0003C3F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((EntityParameter)value);
			return this.Count - 1;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003E22C File Offset: 0x0003C42C
		public override void AddRange(Array values)
		{
			this.OnChange();
			if (values == null)
			{
				throw EntityUtil.ArgumentNull("values");
			}
			foreach (object value in values)
			{
				this.ValidateType(value);
			}
			foreach (object obj in values)
			{
				EntityParameter entityParameter = (EntityParameter)obj;
				this.Validate(-1, entityParameter);
				this.InnerList.Add(entityParameter);
			}
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw EntityUtil.EntityParameterCollectionInvalidParameterName(parameterName);
			}
			return num;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003E308 File Offset: 0x0003C508
		public override void Clear()
		{
			this.OnChange();
			List<EntityParameter> innerList = this.InnerList;
			if (innerList != null)
			{
				foreach (EntityParameter entityParameter in innerList)
				{
					entityParameter.ResetParent();
				}
				innerList.Clear();
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0003E36C File Offset: 0x0003C56C
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0003E37B File Offset: 0x0003C57B
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0003E38A File Offset: 0x0003C58A
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003E397 File Offset: 0x0003C597
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003E3AC File Offset: 0x0003C5AC
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw EntityUtil.EntityParameterCollectionInvalidParameterName(parameterName);
			}
			return this.InnerList[num];
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003E3D8 File Offset: 0x0003C5D8
		private static int IndexOf(IEnumerable items, string parameterName)
		{
			if (items != null)
			{
				int num = 0;
				foreach (object obj in items)
				{
					EntityParameter entityParameter = (EntityParameter)obj;
					if (EntityUtil.SrcCompare(parameterName, entityParameter.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				num = 0;
				foreach (object obj2 in items)
				{
					EntityParameter entityParameter2 = (EntityParameter)obj2;
					if (EntityUtil.DstCompare(parameterName, entityParameter2.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003E4A8 File Offset: 0x0003C6A8
		public override int IndexOf(string parameterName)
		{
			return EntityParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0003E4B8 File Offset: 0x0003C6B8
		public override int IndexOf(object value)
		{
			if (value != null)
			{
				this.ValidateType(value);
				List<EntityParameter> innerList = this.InnerList;
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

		// Token: 0x06000E8D RID: 3725 RVA: 0x0003E4F9 File Offset: 0x0003C6F9
		public override void Insert(int index, object value)
		{
			this.OnChange();
			this.ValidateType(value);
			this.Validate(-1, (EntityParameter)value);
			this.InnerList.Insert(index, (EntityParameter)value);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003E527 File Offset: 0x0003C727
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw EntityUtil.EntityParameterCollectionInvalidIndex(index, this.Count);
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003E544 File Offset: 0x0003C744
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
			if (this != ((EntityParameter)value).CompareExchangeParent(null, this))
			{
				throw EntityUtil.EntityParameterCollectionRemoveInvalidObject();
			}
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003E588 File Offset: 0x0003C788
		public override void RemoveAt(int index)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003E5A0 File Offset: 0x0003C7A0
		public override void RemoveAt(string parameterName)
		{
			this.OnChange();
			int index = this.CheckName(parameterName);
			this.RemoveIndex(index);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0003E5C4 File Offset: 0x0003C7C4
		private void RemoveIndex(int index)
		{
			List<EntityParameter> innerList = this.InnerList;
			EntityParameter entityParameter = innerList[index];
			innerList.RemoveAt(index);
			entityParameter.ResetParent();
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003E5F0 File Offset: 0x0003C7F0
		private void Replace(int index, object newValue)
		{
			List<EntityParameter> innerList = this.InnerList;
			this.ValidateType(newValue);
			this.Validate(index, newValue);
			EntityParameter entityParameter = innerList[index];
			innerList[index] = (EntityParameter)newValue;
			entityParameter.ResetParent();
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003E62E File Offset: 0x0003C82E
		protected override void SetParameter(int index, DbParameter value)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003E648 File Offset: 0x0003C848
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.OnChange();
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw EntityUtil.EntityParameterCollectionInvalidParameterName(parameterName);
			}
			this.Replace(num, value);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003E678 File Offset: 0x0003C878
		private void Validate(int index, object value)
		{
			if (value == null)
			{
				throw EntityUtil.EntityParameterNull("value");
			}
			object obj = ((EntityParameter)value).CompareExchangeParent(this, null);
			if (obj != null)
			{
				if (this != obj)
				{
					throw EntityUtil.EntityParameterContainedByAnotherCollection();
				}
				if (index != this.IndexOf(value))
				{
					throw EntityUtil.EntityParameterContainedByAnotherCollection();
				}
			}
			string text = ((EntityParameter)value).ParameterName;
			if (text.Length == 0)
			{
				index = 1;
				do
				{
					text = "Parameter" + index.ToString(CultureInfo.CurrentCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				((EntityParameter)value).ParameterName = text;
			}
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003E707 File Offset: 0x0003C907
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw EntityUtil.EntityParameterNull("value");
			}
			if (!EntityParameterCollection.ItemType.IsInstanceOfType(value))
			{
				throw EntityUtil.InvalidEntityParameterType(value);
			}
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0003E72B File Offset: 0x0003C92B
		internal EntityParameterCollection()
		{
		}

		// Token: 0x170001B5 RID: 437
		public EntityParameter this[int index]
		{
			get
			{
				return (EntityParameter)this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		// Token: 0x170001B6 RID: 438
		public EntityParameter this[string parameterName]
		{
			get
			{
				return (EntityParameter)this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x0003E764 File Offset: 0x0003C964
		internal bool IsDirty
		{
			get
			{
				if (this._isDirty)
				{
					return true;
				}
				foreach (object obj in this)
				{
					EntityParameter entityParameter = (EntityParameter)obj;
					if (entityParameter.IsDirty)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003E7CC File Offset: 0x0003C9CC
		public EntityParameter Add(EntityParameter value)
		{
			this.Add(value);
			return value;
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003E7D8 File Offset: 0x0003C9D8
		public EntityParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new EntityParameter
			{
				ParameterName = parameterName,
				Value = value
			});
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003E800 File Offset: 0x0003CA00
		public EntityParameter Add(string parameterName, DbType dbType)
		{
			return this.Add(new EntityParameter(parameterName, dbType));
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003E80F File Offset: 0x0003CA0F
		public EntityParameter Add(string parameterName, DbType dbType, int size)
		{
			return this.Add(new EntityParameter(parameterName, dbType, size));
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003E81F File Offset: 0x0003CA1F
		public void AddRange(EntityParameter[] values)
		{
			this.AddRange(values);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0003E828 File Offset: 0x0003CA28
		public override bool Contains(string parameterName)
		{
			return this.IndexOf(parameterName) != -1;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0003E837 File Offset: 0x0003CA37
		public void CopyTo(EntityParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003E841 File Offset: 0x0003CA41
		public int IndexOf(EntityParameter value)
		{
			return this.IndexOf(value);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003E84A File Offset: 0x0003CA4A
		public void Insert(int index, EntityParameter value)
		{
			this.Insert(index, value);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003E854 File Offset: 0x0003CA54
		private void OnChange()
		{
			this._isDirty = true;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003E85D File Offset: 0x0003CA5D
		public void Remove(EntityParameter value)
		{
			this.Remove(value);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003E868 File Offset: 0x0003CA68
		internal void ResetIsDirty()
		{
			this._isDirty = false;
			foreach (object obj in this)
			{
				EntityParameter entityParameter = (EntityParameter)obj;
				entityParameter.ResetIsDirty();
			}
		}

		// Token: 0x040009E2 RID: 2530
		private List<EntityParameter> _items;

		// Token: 0x040009E3 RID: 2531
		private static Type ItemType = typeof(EntityParameter);

		// Token: 0x040009E4 RID: 2532
		private bool _isDirty;
	}
}
