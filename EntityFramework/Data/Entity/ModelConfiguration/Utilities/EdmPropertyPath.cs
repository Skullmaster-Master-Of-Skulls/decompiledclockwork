using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;

namespace System.Data.Entity.ModelConfiguration.Utilities
{
	// Token: 0x020007AE RID: 1966
	internal class EdmPropertyPath : IEnumerable<EdmProperty>, IEnumerable
	{
		// Token: 0x060058CB RID: 22731 RVA: 0x0017D380 File Offset: 0x0017B580
		public EdmPropertyPath(IEnumerable<EdmProperty> components)
		{
			this._components.AddRange(components);
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x0017D39F File Offset: 0x0017B59F
		public EdmPropertyPath(EdmProperty component)
		{
			this._components.Add(component);
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x0017D3BE File Offset: 0x0017B5BE
		private EdmPropertyPath()
		{
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x060058CE RID: 22734 RVA: 0x0017D3D1 File Offset: 0x0017B5D1
		public static EdmPropertyPath Empty
		{
			get
			{
				return EdmPropertyPath._empty;
			}
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x0017D404 File Offset: 0x0017B604
		public override string ToString()
		{
			StringBuilder propertyPathName = new StringBuilder();
			this._components.Each(delegate(EdmProperty pi)
			{
				propertyPathName.Append(pi.Name);
				propertyPathName.Append('.');
			});
			return propertyPathName.ToString(0, propertyPathName.Length - 1);
		}

		// Token: 0x060058D0 RID: 22736 RVA: 0x0017D458 File Offset: 0x0017B658
		public bool Equals(EdmPropertyPath other)
		{
			if (object.ReferenceEquals(null, other))
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			return this._components.SequenceEqual(other._components, (EdmProperty p1, EdmProperty p2) => p1 == p2);
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x0017D4A9 File Offset: 0x0017B6A9
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != typeof(EdmPropertyPath)) && this.Equals((EdmPropertyPath)obj)));
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x0017D4F0 File Offset: 0x0017B6F0
		public override int GetHashCode()
		{
			return this._components.Aggregate(0, (int t, EdmProperty n) => t + n.GetHashCode());
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x0017D51B File Offset: 0x0017B71B
		public static bool operator ==(EdmPropertyPath left, EdmPropertyPath right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x0017D524 File Offset: 0x0017B724
		public static bool operator !=(EdmPropertyPath left, EdmPropertyPath right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x0017D530 File Offset: 0x0017B730
		IEnumerator<EdmProperty> IEnumerable<EdmProperty>.GetEnumerator()
		{
			return this._components.GetEnumerator();
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x0017D542 File Offset: 0x0017B742
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._components.GetEnumerator();
		}

		// Token: 0x04002392 RID: 9106
		private static readonly EdmPropertyPath _empty = new EdmPropertyPath();

		// Token: 0x04002393 RID: 9107
		private readonly List<EdmProperty> _components = new List<EdmProperty>();
	}
}
