using System;

namespace MailBee.ImapMail
{
	// Token: 0x02000198 RID: 408
	public class UidCollection : MessageIndexCollection
	{
		// Token: 0x06000E9E RID: 3742 RVA: 0x00036588 File Offset: 0x00035588
		public UidCollection()
		{
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00036590 File Offset: 0x00035590
		internal UidCollection(int A_0)
		{
			base.InnerList.Capacity = A_0;
		}

		// Token: 0x17000486 RID: 1158
		public long this[int index]
		{
			get
			{
				return (long)base.List[index];
			}
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x000365B7 File Offset: 0x000355B7
		public void Add(long uid)
		{
			base.List.Add(uid);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x000365CC File Offset: 0x000355CC
		public override void AddRange(string startIndex, string endIndex)
		{
			long num = long.Parse(startIndex);
			long num2 = long.Parse(endIndex);
			for (long num3 = num; num3 <= num2; num3 += 1L)
			{
				this.Add(num3);
			}
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000365F9 File Offset: 0x000355F9
		public override void AddIndex(string index)
		{
			this.Add(long.Parse(index));
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00036607 File Offset: 0x00035607
		protected override bool IsPartOfRange(int index)
		{
			return (long)base.List[index - 1] == (long)base.List[index] - 1L;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00036634 File Offset: 0x00035634
		public static UidCollection Parse(string uidString)
		{
			if (uidString == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			UidCollection uidCollection = new UidCollection(uidString.Length / 4);
			try
			{
				uidCollection.AddSet(uidString);
			}
			catch
			{
				return null;
			}
			return uidCollection;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003667C File Offset: 0x0003567C
		public long[] ToArray()
		{
			return (long[])base.InnerList.ToArray(typeof(long));
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00036698 File Offset: 0x00035698
		public int IndexOf(long uid)
		{
			return base.List.IndexOf(uid);
		}
	}
}
