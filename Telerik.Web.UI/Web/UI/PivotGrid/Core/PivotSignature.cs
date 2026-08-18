using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CE6 RID: 3302
	internal class PivotSignature : IEquatable<PivotSignature>
	{
		// Token: 0x06007B4B RID: 31563 RVA: 0x001C4E88 File Offset: 0x001C3088
		[SuppressMessage("Microsoft.Performance", "CA1805:DoNotInitializeUnnecessarily", Justification = "Design choice.")]
		public PivotSignature(IEnumerable<PivotDynamicProperty> properties)
		{
			this.Properties = properties.ToArray<PivotDynamicProperty>();
			this.HashCode = 0;
			foreach (PivotDynamicProperty pivotDynamicProperty in properties)
			{
				this.HashCode ^= (pivotDynamicProperty.Name.GetHashCode() ^ pivotDynamicProperty.Type.GetHashCode());
			}
		}

		// Token: 0x17002771 RID: 10097
		// (get) Token: 0x06007B4C RID: 31564 RVA: 0x001C4F08 File Offset: 0x001C3108
		// (set) Token: 0x06007B4D RID: 31565 RVA: 0x001C4F10 File Offset: 0x001C3110
		public PivotDynamicProperty[] Properties { get; private set; }

		// Token: 0x17002772 RID: 10098
		// (get) Token: 0x06007B4E RID: 31566 RVA: 0x001C4F19 File Offset: 0x001C3119
		// (set) Token: 0x06007B4F RID: 31567 RVA: 0x001C4F21 File Offset: 0x001C3121
		public int HashCode { get; private set; }

		// Token: 0x06007B50 RID: 31568 RVA: 0x001C4F2A File Offset: 0x001C312A
		public override int GetHashCode()
		{
			return this.HashCode;
		}

		// Token: 0x06007B51 RID: 31569 RVA: 0x001C4F34 File Offset: 0x001C3134
		public override bool Equals(object obj)
		{
			PivotSignature pivotSignature = obj as PivotSignature;
			return pivotSignature != null && this.Equals(pivotSignature);
		}

		// Token: 0x06007B52 RID: 31570 RVA: 0x001C4F54 File Offset: 0x001C3154
		public bool Equals(PivotSignature other)
		{
			if (this.Properties.Length != other.Properties.Length)
			{
				return false;
			}
			for (int i = 0; i < this.Properties.Length; i++)
			{
				if (this.Properties[i].Name != other.Properties[i].Name || this.Properties[i].Type != other.Properties[i].Type)
				{
					return false;
				}
			}
			return true;
		}
	}
}
