using System;

namespace MailBee.ImapMail
{
	// Token: 0x02000199 RID: 409
	public class MessageNumberCollection : MessageIndexCollection
	{
		// Token: 0x06000EA8 RID: 3752 RVA: 0x000366AB File Offset: 0x000356AB
		public MessageNumberCollection()
		{
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x000366B3 File Offset: 0x000356B3
		internal MessageNumberCollection(int A_0)
		{
			base.InnerList.Capacity = A_0;
		}

		// Token: 0x17000487 RID: 1159
		public int this[int index]
		{
			get
			{
				return (int)base.List[index];
			}
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000366DA File Offset: 0x000356DA
		public void Add(int messageNumber)
		{
			base.List.Add(messageNumber);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000366F0 File Offset: 0x000356F0
		public override void AddRange(string startIndex, string endIndex)
		{
			int num = int.Parse(startIndex);
			int num2 = int.Parse(endIndex);
			for (int i = num; i <= num2; i++)
			{
				this.Add(i);
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003671C File Offset: 0x0003571C
		public override void AddIndex(string index)
		{
			this.Add(int.Parse(index));
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003672A File Offset: 0x0003572A
		protected override bool IsPartOfRange(int index)
		{
			return (int)base.List[index - 1] == (int)base.List[index] - 1;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00036754 File Offset: 0x00035754
		public static MessageNumberCollection Parse(string messageNumberString)
		{
			if (messageNumberString == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			MessageNumberCollection messageNumberCollection = new MessageNumberCollection(messageNumberString.Length / 4);
			try
			{
				messageNumberCollection.AddSet(messageNumberString);
			}
			catch
			{
				return null;
			}
			return messageNumberCollection;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003679C File Offset: 0x0003579C
		public int[] ToArray()
		{
			return (int[])base.InnerList.ToArray(typeof(int));
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x000367B8 File Offset: 0x000357B8
		public int IndexOf(int messageNumber)
		{
			return base.List.IndexOf(messageNumber);
		}
	}
}
