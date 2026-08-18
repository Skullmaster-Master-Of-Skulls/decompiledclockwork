using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Query.Dynamic
{
	// Token: 0x0200003C RID: 60
	internal class Signature : IEquatable<Signature>
	{
		// Token: 0x06000237 RID: 567 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		public Signature(IEnumerable<DynamicProperty> properties)
		{
			this.properties = properties.ToArray<DynamicProperty>();
			this.hashCode = 0;
			foreach (DynamicProperty dynamicProperty in properties)
			{
				this.hashCode ^= (dynamicProperty.Name.GetHashCode() ^ dynamicProperty.Type.GetHashCode());
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000DB74 File Offset: 0x0000BD74
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		public override bool Equals(object obj)
		{
			return obj is Signature && this.Equals((Signature)obj);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000DB94 File Offset: 0x0000BD94
		public bool Equals(Signature other)
		{
			if (this.properties.Length != other.properties.Length)
			{
				return false;
			}
			for (int i = 0; i < this.properties.Length; i++)
			{
				if (this.properties[i].Name != other.properties[i].Name || this.properties[i].Type != other.properties[i].Type)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040000DF RID: 223
		public DynamicProperty[] properties;

		// Token: 0x040000E0 RID: 224
		public int hashCode;
	}
}
