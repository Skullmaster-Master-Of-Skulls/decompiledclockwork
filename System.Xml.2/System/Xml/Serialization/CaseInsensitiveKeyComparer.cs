using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Serialization
{
	// Token: 0x02000139 RID: 313
	internal class CaseInsensitiveKeyComparer : CaseInsensitiveComparer, IEqualityComparer
	{
		// Token: 0x060016BA RID: 5818 RVA: 0x00064483 File Offset: 0x00062683
		public CaseInsensitiveKeyComparer() : base(CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00064490 File Offset: 0x00062690
		bool IEqualityComparer.Equals(object x, object y)
		{
			return base.Compare(x, y) == 0;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000644A0 File Offset: 0x000626A0
		int IEqualityComparer.GetHashCode(object obj)
		{
			string text = obj as string;
			if (text == null)
			{
				throw new ArgumentException(null, "obj");
			}
			return text.ToUpper(CultureInfo.CurrentCulture).GetHashCode();
		}
	}
}
