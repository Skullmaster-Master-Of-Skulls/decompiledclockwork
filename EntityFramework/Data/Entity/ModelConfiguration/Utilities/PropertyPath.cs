using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Data.Entity.ModelConfiguration.Utilities
{
	// Token: 0x0200082E RID: 2094
	internal class PropertyPath : IEnumerable<PropertyInfo>, IEnumerable
	{
		// Token: 0x06005DC9 RID: 24009 RVA: 0x0019584C File Offset: 0x00193A4C
		public PropertyPath(IEnumerable<PropertyInfo> components)
		{
			this._components.AddRange(components);
		}

		// Token: 0x06005DCA RID: 24010 RVA: 0x0019586B File Offset: 0x00193A6B
		public PropertyPath(PropertyInfo component)
		{
			this._components.Add(component);
		}

		// Token: 0x06005DCB RID: 24011 RVA: 0x0019588A File Offset: 0x00193A8A
		private PropertyPath()
		{
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06005DCC RID: 24012 RVA: 0x0019589D File Offset: 0x00193A9D
		public int Count
		{
			get
			{
				return this._components.Count;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06005DCD RID: 24013 RVA: 0x001958AA File Offset: 0x00193AAA
		public static PropertyPath Empty
		{
			get
			{
				return PropertyPath._empty;
			}
		}

		// Token: 0x17000FE5 RID: 4069
		public PropertyInfo this[int index]
		{
			get
			{
				return this._components[index];
			}
		}

		// Token: 0x06005DCF RID: 24015 RVA: 0x001958EC File Offset: 0x00193AEC
		public override string ToString()
		{
			StringBuilder propertyPathName = new StringBuilder();
			this._components.Each(delegate(PropertyInfo pi)
			{
				propertyPathName.Append(pi.Name);
				propertyPathName.Append('.');
			});
			return propertyPathName.ToString(0, propertyPathName.Length - 1);
		}

		// Token: 0x06005DD0 RID: 24016 RVA: 0x00195944 File Offset: 0x00193B44
		public bool Equals(PropertyPath other)
		{
			if (object.ReferenceEquals(null, other))
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			return this._components.SequenceEqual(other._components, (PropertyInfo p1, PropertyInfo p2) => p1.IsSameAs(p2));
		}

		// Token: 0x06005DD1 RID: 24017 RVA: 0x00195995 File Offset: 0x00193B95
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != typeof(PropertyPath)) && this.Equals((PropertyPath)obj)));
		}

		// Token: 0x06005DD2 RID: 24018 RVA: 0x001959F3 File Offset: 0x00193BF3
		public override int GetHashCode()
		{
			return this._components.Aggregate(0, (int t, PropertyInfo n) => t ^ n.DeclaringType.GetHashCode() * n.Name.GetHashCode() * 397);
		}

		// Token: 0x06005DD3 RID: 24019 RVA: 0x00195A1E File Offset: 0x00193C1E
		public static bool operator ==(PropertyPath left, PropertyPath right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06005DD4 RID: 24020 RVA: 0x00195A27 File Offset: 0x00193C27
		public static bool operator !=(PropertyPath left, PropertyPath right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06005DD5 RID: 24021 RVA: 0x00195A33 File Offset: 0x00193C33
		IEnumerator<PropertyInfo> IEnumerable<PropertyInfo>.GetEnumerator()
		{
			return this._components.GetEnumerator();
		}

		// Token: 0x06005DD6 RID: 24022 RVA: 0x00195A45 File Offset: 0x00193C45
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._components.GetEnumerator();
		}

		// Token: 0x0400250B RID: 9483
		private static readonly PropertyPath _empty = new PropertyPath();

		// Token: 0x0400250C RID: 9484
		private readonly List<PropertyInfo> _components = new List<PropertyInfo>();
	}
}
