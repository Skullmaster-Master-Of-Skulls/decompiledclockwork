using System;
using System.Collections.Generic;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000310 RID: 784
	internal class MaskDescriptorComparer : IComparer<MaskDescriptor>
	{
		// Token: 0x06001EF4 RID: 7924 RVA: 0x000B8F46 File Offset: 0x000B7146
		public MaskDescriptorComparer(MaskDescriptorComparer.SortType sortType, SortOrder sortOrder)
		{
			this.sortType = sortType;
			this.sortOrder = sortOrder;
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x000B8F5C File Offset: 0x000B715C
		public int Compare(MaskDescriptor maskDescriptorA, MaskDescriptor maskDescriptorB)
		{
			if (maskDescriptorA == null || maskDescriptorB == null)
			{
				return 0;
			}
			string strA;
			string strB;
			switch (this.sortType)
			{
			default:
				strA = maskDescriptorA.Name;
				strB = maskDescriptorB.Name;
				break;
			case MaskDescriptorComparer.SortType.BySample:
				strA = maskDescriptorA.Sample;
				strB = maskDescriptorB.Sample;
				break;
			case MaskDescriptorComparer.SortType.ByValidatingTypeName:
				strA = ((maskDescriptorA.ValidatingType == null) ? SR.GetString("MaskDescriptorValidatingTypeNone") : maskDescriptorA.ValidatingType.Name);
				strB = ((maskDescriptorB.ValidatingType == null) ? SR.GetString("MaskDescriptorValidatingTypeNone") : maskDescriptorB.ValidatingType.Name);
				break;
			}
			int num = string.Compare(strA, strB);
			if (this.sortOrder != SortOrder.Descending)
			{
				return num;
			}
			return -num;
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x000B900B File Offset: 0x000B720B
		public int GetHashCode(MaskDescriptor maskDescriptor)
		{
			if (maskDescriptor != null)
			{
				return maskDescriptor.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x000B9018 File Offset: 0x000B7218
		public bool Equals(MaskDescriptor maskDescriptorA, MaskDescriptor maskDescriptorB)
		{
			if (!MaskDescriptor.IsValidMaskDescriptor(maskDescriptorA) || !MaskDescriptor.IsValidMaskDescriptor(maskDescriptorB))
			{
				return maskDescriptorA == maskDescriptorB;
			}
			return maskDescriptorA.Equals(maskDescriptorB);
		}

		// Token: 0x040017E2 RID: 6114
		private SortOrder sortOrder;

		// Token: 0x040017E3 RID: 6115
		private MaskDescriptorComparer.SortType sortType;

		// Token: 0x02000581 RID: 1409
		public enum SortType
		{
			// Token: 0x0400219E RID: 8606
			ByName,
			// Token: 0x0400219F RID: 8607
			BySample,
			// Token: 0x040021A0 RID: 8608
			ByValidatingTypeName
		}
	}
}
