using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Telerik.Web.Data
{
	// Token: 0x02001B90 RID: 7056
	internal class Signature : IEquatable<Signature>
	{
		// Token: 0x0601116E RID: 69998 RVA: 0x003C54E4 File Offset: 0x003C36E4
		[SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily")]
		public Signature(IEnumerable<DynamicProperty> properties)
		{
			this._properties = properties.ToArray<DynamicProperty>();
			this.hashCode = 0;
			foreach (DynamicProperty dynamicProperty in properties)
			{
				this.hashCode ^= (dynamicProperty.Name.GetHashCode() ^ dynamicProperty.Type.GetHashCode());
			}
		}

		// Token: 0x17005372 RID: 21362
		// (get) Token: 0x0601116F RID: 69999 RVA: 0x003C5564 File Offset: 0x003C3764
		// (set) Token: 0x06011170 RID: 70000 RVA: 0x003C556C File Offset: 0x003C376C
		public DynamicProperty[] properties
		{
			get
			{
				return this._properties;
			}
			set
			{
				this._properties = value;
			}
		}

		// Token: 0x06011171 RID: 70001 RVA: 0x003C5575 File Offset: 0x003C3775
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06011172 RID: 70002 RVA: 0x003C5580 File Offset: 0x003C3780
		public override bool Equals(object obj)
		{
			Signature signature = obj as Signature;
			return signature != null && this.Equals(signature);
		}

		// Token: 0x06011173 RID: 70003 RVA: 0x003C55A0 File Offset: 0x003C37A0
		public bool Equals(Signature other)
		{
			if (this._properties.Length != other.properties.Length)
			{
				return false;
			}
			for (int i = 0; i < this._properties.Length; i++)
			{
				if (this._properties[i].Name != other.properties[i].Name || this._properties[i].Type != other.properties[i].Type)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04004C7B RID: 19579
		private DynamicProperty[] _properties;

		// Token: 0x04004C7C RID: 19580
		private int hashCode;
	}
}
