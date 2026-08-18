using System;
using System.Collections;

namespace System.Web.Configuration
{
	// Token: 0x02000772 RID: 1906
	internal class WebBaseEventKeyComparer : IEqualityComparer
	{
		// Token: 0x06005BBF RID: 23487 RVA: 0x0013DC14 File Offset: 0x0013BE14
		public bool Equals(object x, object y)
		{
			CustomWebEventKey customWebEventKey = (CustomWebEventKey)x;
			CustomWebEventKey customWebEventKey2 = (CustomWebEventKey)y;
			return customWebEventKey._eventCode == customWebEventKey2._eventCode && customWebEventKey._type.Equals(customWebEventKey2._type);
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x0013DC53 File Offset: 0x0013BE53
		public int GetHashCode(object obj)
		{
			return ((CustomWebEventKey)obj)._eventCode ^ ((CustomWebEventKey)obj)._type.GetHashCode();
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x0013DC74 File Offset: 0x0013BE74
		public int Compare(object x, object y)
		{
			CustomWebEventKey customWebEventKey = (CustomWebEventKey)x;
			CustomWebEventKey customWebEventKey2 = (CustomWebEventKey)y;
			int eventCode = customWebEventKey._eventCode;
			int eventCode2 = customWebEventKey2._eventCode;
			if (eventCode == eventCode2)
			{
				Type type = customWebEventKey._type;
				Type type2 = customWebEventKey2._type;
				if (type.Equals(type2))
				{
					return 0;
				}
				return Comparer.Default.Compare(type.ToString(), type2.ToString());
			}
			else
			{
				if (eventCode > eventCode2)
				{
					return 1;
				}
				return -1;
			}
		}
	}
}
