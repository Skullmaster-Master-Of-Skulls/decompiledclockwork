using System;
using System.Collections;

namespace MailBee.Html
{
	// Token: 0x02000007 RID: 7
	public class ElementReadOnlyCollection : CollectionBase
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000051A8 File Offset: 0x000041A8
		internal ElementReadOnlyCollection(ElementCollection A_0)
		{
			foreach (object obj in A_0)
			{
				Element value = (Element)obj;
				base.InnerList.Add(value);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005208 File Offset: 0x00004208
		internal ElementReadOnlyCollection(ElementCollection A_0, string A_1)
		{
			string b = (A_1 != null) ? A_1.ToLower() : null;
			foreach (object obj in A_0)
			{
				Element element = (Element)obj;
				if ((element.TagName != null && A_1 != null && element.TagName.ToLower() == b) || (element.TagName == null && A_1 == element.TagName))
				{
					base.InnerList.Add(element);
				}
			}
		}

		// Token: 0x17000020 RID: 32
		public Element this[int index]
		{
			get
			{
				return (Element)base.InnerList[index];
			}
		}
	}
}
