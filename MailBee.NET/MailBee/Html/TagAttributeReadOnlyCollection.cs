using System;
using System.Collections;

namespace MailBee.Html
{
	// Token: 0x02000012 RID: 18
	public class TagAttributeReadOnlyCollection : CollectionBase
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00006608 File Offset: 0x00005608
		internal TagAttributeReadOnlyCollection(TagAttributeCollection A_0, string A_1)
		{
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			string b = A_1.ToLower();
			foreach (object obj in A_0)
			{
				TagAttribute tagAttribute = (TagAttribute)obj;
				if (tagAttribute.Name.ToLower() == b)
				{
					base.InnerList.Add(tagAttribute);
				}
			}
		}

		// Token: 0x1700003A RID: 58
		public TagAttribute this[int index]
		{
			get
			{
				return (TagAttribute)base.InnerList[index];
			}
		}
	}
}
