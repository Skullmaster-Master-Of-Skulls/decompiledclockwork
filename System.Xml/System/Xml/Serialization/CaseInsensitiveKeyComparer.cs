using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Serialization
{
	// Token: 0x020002B0 RID: 688
	internal class CaseInsensitiveKeyComparer : CaseInsensitiveComparer, IEqualityComparer
	{
		// Token: 0x0600210E RID: 8462 RVA: 0x0009C63F File Offset: 0x0009B63F
		public CaseInsensitiveKeyComparer() : base(CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x0009C64C File Offset: 0x0009B64C
		bool IEqualityComparer.Equals(object x, object y)
		{
			return base.Compare(x, y) == 0;
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x0009C65C File Offset: 0x0009B65C
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
