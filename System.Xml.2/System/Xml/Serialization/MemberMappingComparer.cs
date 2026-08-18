using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000155 RID: 341
	internal class MemberMappingComparer : IComparer
	{
		// Token: 0x060017BA RID: 6074 RVA: 0x0006801C File Offset: 0x0006621C
		public int Compare(object o1, object o2)
		{
			MemberMapping memberMapping = (MemberMapping)o1;
			MemberMapping memberMapping2 = (MemberMapping)o2;
			bool isText = memberMapping.IsText;
			if (isText)
			{
				if (memberMapping2.IsText)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (memberMapping2.IsText)
				{
					return -1;
				}
				if (memberMapping.SequenceId < 0 && memberMapping2.SequenceId < 0)
				{
					return 0;
				}
				if (memberMapping.SequenceId < 0)
				{
					return 1;
				}
				if (memberMapping2.SequenceId < 0)
				{
					return -1;
				}
				if (memberMapping.SequenceId < memberMapping2.SequenceId)
				{
					return -1;
				}
				if (memberMapping.SequenceId > memberMapping2.SequenceId)
				{
					return 1;
				}
				return 0;
			}
		}
	}
}
