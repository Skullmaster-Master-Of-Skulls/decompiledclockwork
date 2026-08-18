using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Utilities;
using System.Reflection;
using System.Xml.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000796 RID: 1942
	internal class SortableBindingList<T> : BindingList<T>
	{
		// Token: 0x0600580A RID: 22538 RVA: 0x0017AC12 File Offset: 0x00178E12
		public SortableBindingList(List<T> list) : base(list)
		{
		}

		// Token: 0x0600580B RID: 22539 RVA: 0x0017AC1C File Offset: 0x00178E1C
		protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
		{
			if (SortableBindingList<T>.PropertyComparer.CanSort(prop.PropertyType))
			{
				((List<T>)base.Items).Sort(new SortableBindingList<T>.PropertyComparer(prop, direction));
				this._sortDirection = direction;
				this._sortProperty = prop;
				this._isSorted = true;
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
			}
		}

		// Token: 0x0600580C RID: 22540 RVA: 0x0017AC6F File Offset: 0x00178E6F
		protected override void RemoveSortCore()
		{
			this._isSorted = false;
			this._sortProperty = null;
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x0600580D RID: 22541 RVA: 0x0017AC7F File Offset: 0x00178E7F
		protected override bool IsSortedCore
		{
			get
			{
				return this._isSorted;
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x0600580E RID: 22542 RVA: 0x0017AC87 File Offset: 0x00178E87
		protected override ListSortDirection SortDirectionCore
		{
			get
			{
				return this._sortDirection;
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x0600580F RID: 22543 RVA: 0x0017AC8F File Offset: 0x00178E8F
		protected override PropertyDescriptor SortPropertyCore
		{
			get
			{
				return this._sortProperty;
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06005810 RID: 22544 RVA: 0x0017AC97 File Offset: 0x00178E97
		protected override bool SupportsSortingCore
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04002351 RID: 9041
		private bool _isSorted;

		// Token: 0x04002352 RID: 9042
		private ListSortDirection _sortDirection;

		// Token: 0x04002353 RID: 9043
		private PropertyDescriptor _sortProperty;

		// Token: 0x02000797 RID: 1943
		internal class PropertyComparer : Comparer<T>
		{
			// Token: 0x06005811 RID: 22545 RVA: 0x0017AC9C File Offset: 0x00178E9C
			public PropertyComparer(PropertyDescriptor prop, ListSortDirection direction)
			{
				if (!prop.ComponentType.IsAssignableFrom(typeof(T)))
				{
					throw new MissingMemberException(typeof(T).Name, prop.Name);
				}
				this._prop = prop;
				this._direction = direction;
				if (SortableBindingList<T>.PropertyComparer.CanSortWithIComparable(prop.PropertyType))
				{
					PropertyInfo declaredProperty = typeof(Comparer<>).MakeGenericType(new Type[]
					{
						prop.PropertyType
					}).GetDeclaredProperty("Default");
					this._comparer = (IComparer)declaredProperty.GetValue(null, null);
					this._useToString = false;
					return;
				}
				this._comparer = StringComparer.CurrentCultureIgnoreCase;
				this._useToString = true;
			}

			// Token: 0x06005812 RID: 22546 RVA: 0x0017AD54 File Offset: 0x00178F54
			public override int Compare(T left, T right)
			{
				object obj = this._prop.GetValue(left);
				object obj2 = this._prop.GetValue(right);
				if (this._useToString)
				{
					obj = ((obj != null) ? obj.ToString() : null);
					obj2 = ((obj2 != null) ? obj2.ToString() : null);
				}
				if (this._direction != ListSortDirection.Ascending)
				{
					return this._comparer.Compare(obj2, obj);
				}
				return this._comparer.Compare(obj, obj2);
			}

			// Token: 0x06005813 RID: 22547 RVA: 0x0017ADCA File Offset: 0x00178FCA
			public static bool CanSort(Type type)
			{
				return SortableBindingList<T>.PropertyComparer.CanSortWithToString(type) || SortableBindingList<T>.PropertyComparer.CanSortWithIComparable(type);
			}

			// Token: 0x06005814 RID: 22548 RVA: 0x0017ADDC File Offset: 0x00178FDC
			private static bool CanSortWithIComparable(Type type)
			{
				return type.GetInterface("IComparable") != null || (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Nullable<>));
			}

			// Token: 0x06005815 RID: 22549 RVA: 0x0017AE12 File Offset: 0x00179012
			private static bool CanSortWithToString(Type type)
			{
				return type.Equals(typeof(XNode)) || type.IsSubclassOf(typeof(XNode));
			}

			// Token: 0x04002354 RID: 9044
			private readonly IComparer _comparer;

			// Token: 0x04002355 RID: 9045
			private readonly ListSortDirection _direction;

			// Token: 0x04002356 RID: 9046
			private readonly PropertyDescriptor _prop;

			// Token: 0x04002357 RID: 9047
			private readonly bool _useToString;
		}
	}
}
