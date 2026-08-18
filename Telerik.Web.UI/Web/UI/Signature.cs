using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI
{
	// Token: 0x02000374 RID: 884
	internal class Signature : IEquatable<Signature>
	{
		// Token: 0x06001E41 RID: 7745 RVA: 0x0005E470 File Offset: 0x0005C670
		public Signature(IEnumerable<DynamicProperty> properties)
		{
			this.properties = properties.ToArray<DynamicProperty>();
			this.hashCode = 0;
			foreach (DynamicProperty dynamicProperty in properties)
			{
				this.hashCode ^= (dynamicProperty.Name.GetHashCode() ^ dynamicProperty.Type.GetHashCode());
			}
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0005E4F0 File Offset: 0x0005C6F0
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x0005E4F8 File Offset: 0x0005C6F8
		public override bool Equals(object obj)
		{
			Signature signature = obj as Signature;
			return signature != null && this.Equals(signature);
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0005E518 File Offset: 0x0005C718
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

		// Token: 0x0400077F RID: 1919
		public DynamicProperty[] properties;

		// Token: 0x04000780 RID: 1920
		public int hashCode;
	}
}
