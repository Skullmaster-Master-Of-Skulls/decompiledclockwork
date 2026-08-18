using System;
using System.Collections;
using System.Globalization;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011D8 RID: 4568
	internal class WordComparer : IComparer
	{
		// Token: 0x0600BCB2 RID: 48306 RVA: 0x0029DC84 File Offset: 0x0029BE84
		public int Compare(object first, object second)
		{
			string text = first as string;
			string text2 = second as string;
			if (text == null || text2 == null)
			{
				throw new ArgumentException("I can compare strings only!");
			}
			int length = text.Length;
			int length2 = text2.Length;
			if (length != length2)
			{
				return (length < length2) ? -1 : 1;
			}
			return string.Compare(text, text2, false, CultureInfo.InvariantCulture);
		}
	}
}
