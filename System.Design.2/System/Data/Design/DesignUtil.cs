using System;
using System.Collections;

namespace System.Data.Design
{
	// Token: 0x02000240 RID: 576
	internal sealed class DesignUtil
	{
		// Token: 0x06001686 RID: 5766 RVA: 0x0000362F File Offset: 0x0000182F
		private DesignUtil()
		{
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0007BF7C File Offset: 0x0007A17C
		internal static IDictionary CloneDictionary(IDictionary source)
		{
			if (source == null)
			{
				return null;
			}
			if (source is ICloneable)
			{
				return (IDictionary)((ICloneable)source).Clone();
			}
			IDictionary dictionary = (IDictionary)Activator.CreateInstance(source.GetType());
			IDictionaryEnumerator enumerator = source.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ICloneable cloneable = enumerator.Key as ICloneable;
				ICloneable cloneable2 = enumerator.Value as ICloneable;
				if (cloneable != null && cloneable2 != null)
				{
					dictionary.Add(cloneable.Clone(), cloneable2.Clone());
				}
			}
			return dictionary;
		}
	}
}
