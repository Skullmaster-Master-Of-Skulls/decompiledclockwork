using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x02000337 RID: 823
	[SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	public sealed class EntityParameterCollection : DbParameterCollection
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x0008B5E5 File Offset: 0x000897E5
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

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x0008B5FC File Offset: 0x000897FC
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

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x0008B621 File Offset: 0x00089821
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0008B62E File Offset: 0x0008982E
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0008B63B File Offset: 0x0008983B
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0008B648 File Offset: 0x00089848
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x0008B655 File Offset: 0x00089855
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			this.OnChange();
			Check.NotNull<object>(value, "value");
			EntityParameterCollection.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Add((EntityParameter)value);
			return this.Count - 1;
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0008B690 File Offset: 0x00089890
		public override void AddRange(Array values)
		{
			this.OnChange();
			Check.NotNull<Array>(values, "values");
			foreach (object value in values)
			{
				EntityParameterCollection.ValidateType(value);
			}
			foreach (object obj in values)
			{
				EntityParameter entityParameter = (EntityParameter)obj;
				this.Validate(-1, entityParameter);
				this.InnerList.Add(entityParameter);
			}
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0008B748 File Offset: 0x00089948
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		private int CheckName(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw new IndexOutOfRangeException(Strings.EntityParameterCollectionInvalidParameterName(parameterName));
			}
			return num;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0008B770 File Offset: 0x00089970
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

		// Token: 0x06001C87 RID: 7303 RVA: 0x0008B7D4 File Offset: 0x000899D4
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x0008B7E3 File Offset: 0x000899E3
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0008B7F2 File Offset: 0x000899F2
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x0008B7FF File Offset: 0x000899FF
		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented")]
		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1604:ElementDocumentationMustHaveSummary")]
		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:ElementReturnValueMustBeDocumented")]
		protected override DbParameter GetParameter(int index)
		{
			this.RangeCheck(index);
			return this.InnerList[index];
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0008B814 File Offset: 0x00089A14
		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1604:ElementDocumentationMustHaveSummary")]
		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:ElementReturnValueMustBeDocumented")]
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented")]
		protected override DbParameter GetParameter(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw new IndexOutOfRangeException(Strings.EntityParameterCollectionInvalidParameterName(parameterName));
			}
			return this.InnerList[num];
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0008B848 File Offset: 0x00089A48
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

		// Token: 0x06001C8D RID: 7309 RVA: 0x0008B918 File Offset: 0x00089B18
		public override int IndexOf(string parameterName)
		{
			return EntityParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0008B928 File Offset: 0x00089B28
		public override int IndexOf(object value)
		{
			if (value != null)
			{
				EntityParameterCollection.ValidateType(value);
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

		// Token: 0x06001C8F RID: 7311 RVA: 0x0008B968 File Offset: 0x00089B68
		public override void Insert(int index, object value)
		{
			this.OnChange();
			Check.NotNull<object>(value, "value");
			EntityParameterCollection.ValidateType(value);
			this.Validate(-1, value);
			this.InnerList.Insert(index, (EntityParameter)value);
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0008B99C File Offset: 0x00089B9C
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw new IndexOutOfRangeException(Strings.EntityParameterCollectionInvalidIndex(index.ToString(CultureInfo.InvariantCulture), this.Count.ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0008B9E0 File Offset: 0x00089BE0
		public override void Remove(object value)
		{
			this.OnChange();
			Check.NotNull<object>(value, "value");
			EntityParameterCollection.ValidateType(value);
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			if (this != ((EntityParameter)value).CompareExchangeParent(null, this))
			{
				throw new ArgumentException(Strings.EntityParameterCollectionRemoveInvalidObject);
			}
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x0008BA34 File Offset: 0x00089C34
		public override void RemoveAt(int index)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x0008BA4C File Offset: 0x00089C4C
		public override void RemoveAt(string parameterName)
		{
			this.OnChange();
			int index = this.CheckName(parameterName);
			this.RemoveIndex(index);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x0008BA70 File Offset: 0x00089C70
		private void RemoveIndex(int index)
		{
			List<EntityParameter> innerList = this.InnerList;
			EntityParameter entityParameter = innerList[index];
			innerList.RemoveAt(index);
			entityParameter.ResetParent();
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x0008BA9C File Offset: 0x00089C9C
		private void Replace(int index, object newValue)
		{
			List<EntityParameter> innerList = this.InnerList;
			EntityParameterCollection.ValidateType(newValue);
			this.Validate(index, newValue);
			EntityParameter entityParameter = innerList[index];
			innerList[index] = (EntityParameter)newValue;
			entityParameter.ResetParent();
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x0008BAD9 File Offset: 0x00089CD9
		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented")]
		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1604:ElementDocumentationMustHaveSummary")]
		protected override void SetParameter(int index, DbParameter value)
		{
			this.OnChange();
			this.RangeCheck(index);
			this.Replace(index, value);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x0008BAF0 File Offset: 0x00089CF0
		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented")]
		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1604:ElementDocumentationMustHaveSummary")]
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.OnChange();
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw new IndexOutOfRangeException(Strings.EntityParameterCollectionInvalidParameterName(parameterName));
			}
			this.Replace(num, value);
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x0008BB24 File Offset: 0x00089D24
		private void Validate(int index, object value)
		{
			Check.NotNull<object>(value, "value");
			EntityParameter entityParameter = (EntityParameter)value;
			object obj = entityParameter.CompareExchangeParent(this, null);
			if (obj != null)
			{
				if (this != obj)
				{
					throw new ArgumentException(Strings.EntityParameterContainedByAnotherCollection);
				}
				if (index != this.IndexOf(value))
				{
					throw new ArgumentException(Strings.EntityParameterContainedByAnotherCollection);
				}
			}
			string text = entityParameter.ParameterName;
			if (text.Length == 0)
			{
				index = 1;
				do
				{
					text = "Parameter" + index.ToString(CultureInfo.CurrentCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				entityParameter.ParameterName = text;
			}
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x0008BBB3 File Offset: 0x00089DB3
		private static void ValidateType(object value)
		{
			Check.NotNull<object>(value, "value");
			if (!EntityParameterCollection._itemType.IsInstanceOfType(value))
			{
				throw new InvalidCastException(Strings.InvalidEntityParameterType(value.GetType().Name));
			}
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x0008BBE4 File Offset: 0x00089DE4
		internal EntityParameterCollection()
		{
		}

		// Token: 0x17000320 RID: 800
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

		// Token: 0x17000321 RID: 801
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

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x0008BC1C File Offset: 0x00089E1C
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

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0008BC84 File Offset: 0x00089E84
		public EntityParameter Add(EntityParameter value)
		{
			this.Add(value);
			return value;
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0008BC90 File Offset: 0x00089E90
		public EntityParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new EntityParameter
			{
				ParameterName = parameterName,
				Value = value
			});
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0008BCB8 File Offset: 0x00089EB8
		public EntityParameter Add(string parameterName, DbType dbType)
		{
			return this.Add(new EntityParameter(parameterName, dbType));
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0008BCC7 File Offset: 0x00089EC7
		public EntityParameter Add(string parameterName, DbType dbType, int size)
		{
			return this.Add(new EntityParameter(parameterName, dbType, size));
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0008BCD7 File Offset: 0x00089ED7
		public void AddRange(EntityParameter[] values)
		{
			this.AddRange(values);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0008BCE0 File Offset: 0x00089EE0
		public override bool Contains(string parameterName)
		{
			return this.IndexOf(parameterName) != -1;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0008BCEF File Offset: 0x00089EEF
		public void CopyTo(EntityParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0008BCF9 File Offset: 0x00089EF9
		public int IndexOf(EntityParameter value)
		{
			return this.IndexOf(value);
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0008BD02 File Offset: 0x00089F02
		public void Insert(int index, EntityParameter value)
		{
			this.Insert(index, value);
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0008BD0C File Offset: 0x00089F0C
		private void OnChange()
		{
			this._isDirty = true;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0008BD15 File Offset: 0x00089F15
		public void Remove(EntityParameter value)
		{
			this.Remove(value);
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0008BD20 File Offset: 0x00089F20
		internal void ResetIsDirty()
		{
			this._isDirty = false;
			foreach (object obj in this)
			{
				EntityParameter entityParameter = (EntityParameter)obj;
				entityParameter.ResetIsDirty();
			}
		}

		// Token: 0x040009D3 RID: 2515
		private List<EntityParameter> _items;

		// Token: 0x040009D4 RID: 2516
		private static readonly Type _itemType = typeof(EntityParameter);

		// Token: 0x040009D5 RID: 2517
		private bool _isDirty;
	}
}
