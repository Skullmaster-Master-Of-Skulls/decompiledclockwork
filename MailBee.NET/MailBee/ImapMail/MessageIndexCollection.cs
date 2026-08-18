using System;
using System.Collections;
using System.Text;

namespace MailBee.ImapMail
{
	// Token: 0x02000197 RID: 407
	public abstract class MessageIndexCollection : CollectionBase
	{
		// Token: 0x06000E98 RID: 3736 RVA: 0x00036444 File Offset: 0x00035444
		public void AddSet(string messageIndexSet)
		{
			foreach (string text in messageIndexSet.Split(new char[]
			{
				','
			}))
			{
				if (text.IndexOf(':') > -1)
				{
					string[] array2 = text.Split(new char[]
					{
						':'
					});
					this.AddRange(array2[0], array2[1]);
				}
				else
				{
					this.AddIndex(text);
				}
			}
		}

		// Token: 0x06000E99 RID: 3737
		public abstract void AddRange(string startIndex, string endIndex);

		// Token: 0x06000E9A RID: 3738
		public abstract void AddIndex(string index);

		// Token: 0x06000E9B RID: 3739
		protected abstract bool IsPartOfRange(int index);

		// Token: 0x06000E9C RID: 3740 RVA: 0x000364A8 File Offset: 0x000354A8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.List.Count * 4);
			int index = 0;
			bool flag = false;
			for (int i = 0; i < base.List.Count; i++)
			{
				if (i < base.List.Count - 1 && this.IsPartOfRange(i + 1))
				{
					if (!flag)
					{
						index = i;
						flag = true;
					}
				}
				else
				{
					if (flag)
					{
						stringBuilder.Append(base.List[index].ToString() + ":" + base.List[i].ToString());
						flag = false;
					}
					else
					{
						stringBuilder.Append(base.List[i].ToString());
					}
					if (i < base.List.Count - 1)
					{
						stringBuilder.Append(",");
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
