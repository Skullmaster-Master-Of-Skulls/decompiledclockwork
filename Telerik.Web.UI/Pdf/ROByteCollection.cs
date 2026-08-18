using System;
using System.Collections;

namespace Telerik.Pdf
{
	// Token: 0x02001675 RID: 5749
	public class ROByteCollection : ReadOnlyCollectionBase
	{
		// Token: 0x0600DE41 RID: 56897 RVA: 0x0030911B File Offset: 0x0030731B
		public ROByteCollection(IList sourceList)
		{
			base.InnerList.AddRange(sourceList);
		}

		// Token: 0x17004400 RID: 17408
		public byte this[int index]
		{
			get
			{
				return (byte)base.InnerList[index];
			}
		}

		// Token: 0x0600DE43 RID: 56899 RVA: 0x00309142 File Offset: 0x00307342
		public int IndexOf(byte value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x0600DE44 RID: 56900 RVA: 0x00309155 File Offset: 0x00307355
		public bool Contains(byte value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x0600DE45 RID: 56901 RVA: 0x00309168 File Offset: 0x00307368
		public byte[] ToArray()
		{
			return (byte[])base.InnerList.ToArray(typeof(byte));
		}
	}
}
