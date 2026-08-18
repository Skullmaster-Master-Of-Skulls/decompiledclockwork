using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI
{
	// Token: 0x0200028B RID: 651
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class FilterableAttribute : Attribute
	{
		// Token: 0x06001EA4 RID: 7844 RVA: 0x00062285 File Offset: 0x00060485
		public FilterableAttribute(bool filterable)
		{
			this._filterable = filterable;
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x00062294 File Offset: 0x00060494
		public bool Filterable
		{
			get
			{
				return this._filterable;
			}
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0006229C File Offset: 0x0006049C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			FilterableAttribute filterableAttribute = obj as FilterableAttribute;
			return filterableAttribute != null && filterableAttribute.Filterable == this._filterable;
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000622C9 File Offset: 0x000604C9
		public override int GetHashCode()
		{
			return this._filterable.GetHashCode();
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000622D6 File Offset: 0x000604D6
		public override bool IsDefaultAttribute()
		{
			return this.Equals(FilterableAttribute.Default);
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000622E3 File Offset: 0x000604E3
		public static bool IsObjectFilterable(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return FilterableAttribute.IsTypeFilterable(instance.GetType());
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x00062300 File Offset: 0x00060500
		public static bool IsPropertyFilterable(PropertyDescriptor propertyDescriptor)
		{
			FilterableAttribute filterableAttribute = (FilterableAttribute)propertyDescriptor.Attributes[typeof(FilterableAttribute)];
			return filterableAttribute == null || filterableAttribute.Filterable;
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00062334 File Offset: 0x00060534
		public static bool IsTypeFilterable(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj = FilterableAttribute._filterableTypes[type];
			if (obj != null)
			{
				return (bool)obj;
			}
			AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
			FilterableAttribute filterableAttribute = (FilterableAttribute)attributes[typeof(FilterableAttribute)];
			obj = (filterableAttribute != null && filterableAttribute.Filterable);
			FilterableAttribute._filterableTypes[type] = obj;
			return (bool)obj;
		}

		// Token: 0x040019A4 RID: 6564
		public static readonly FilterableAttribute Yes = new FilterableAttribute(true);

		// Token: 0x040019A5 RID: 6565
		public static readonly FilterableAttribute No = new FilterableAttribute(false);

		// Token: 0x040019A6 RID: 6566
		public static readonly FilterableAttribute Default = FilterableAttribute.Yes;

		// Token: 0x040019A7 RID: 6567
		private bool _filterable;

		// Token: 0x040019A8 RID: 6568
		private static Hashtable _filterableTypes = Hashtable.Synchronized(new Hashtable());
	}
}
