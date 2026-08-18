using System;
using System.Collections;
using System.Text;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x020002E6 RID: 742
	public sealed class CollectionUtilities
	{
		// Token: 0x06001B7F RID: 7039 RVA: 0x000A548D File Offset: 0x000A448D
		private CollectionUtilities()
		{
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x000A5498 File Offset: 0x000A4498
		public static bool CheckElementsAreOfType(IEnumerable e, Type t)
		{
			foreach (object o in e)
			{
				if (!t.IsInstanceOfType(o))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x000A54F0 File Offset: 0x000A44F0
		public static string ToString(IEnumerable c)
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			IEnumerator enumerator = c.GetEnumerator();
			if (enumerator.MoveNext())
			{
				stringBuilder.Append(enumerator.Current.ToString());
				while (enumerator.MoveNext())
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(enumerator.Current.ToString());
				}
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}
	}
}
