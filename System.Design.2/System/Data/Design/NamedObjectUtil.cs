using System;
using System.Collections;

namespace System.Data.Design
{
	// Token: 0x02000255 RID: 597
	internal class NamedObjectUtil
	{
		// Token: 0x060016EB RID: 5867 RVA: 0x0000362F File Offset: 0x0000182F
		private NamedObjectUtil()
		{
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0007D9EF File Offset: 0x0007BBEF
		public static INamedObject Find(INamedObjectCollection coll, string name)
		{
			return NamedObjectUtil.Find(coll, name, false);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0007D9FC File Offset: 0x0007BBFC
		private static INamedObject Find(ICollection coll, string name, bool ignoreCase)
		{
			foreach (object obj in coll)
			{
				INamedObject namedObject = obj as INamedObject;
				if (namedObject == null)
				{
					throw new InternalException("Named object collection holds something that is not a named object", 2);
				}
				if (StringUtil.EqualValue(namedObject.Name, name, ignoreCase))
				{
					return namedObject;
				}
			}
			return null;
		}
	}
}
