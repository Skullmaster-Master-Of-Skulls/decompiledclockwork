using System;
using System.Collections;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AE3 RID: 2787
	internal sealed class SSTHelper
	{
		// Token: 0x060068D1 RID: 26833 RVA: 0x00189208 File Offset: 0x00187408
		public void AddLastIndexRecord()
		{
			if (this.listOfIndexes == null)
			{
				this.listOfIndexes = new ArrayList();
			}
			int index = 0;
			if (this.stringList != null)
			{
				index = this.stringList.Count;
			}
			this.listOfIndexes.Add(new RecordSize(index, this.currentLength, this.currentCharIndex));
		}

		// Token: 0x060068D2 RID: 26834 RVA: 0x0018925C File Offset: 0x0018745C
		public int AddString(string labelSST, Hashtable stringRecords)
		{
			this.totalStringCount += 1U;
			if (stringRecords != null && stringRecords.ContainsKey(labelSST))
			{
				return (int)stringRecords[labelSST];
			}
			int num = labelSST.Length * 2 + 3;
			if (this.stringList == null)
			{
				this.stringList = new ArrayList();
			}
			int num2 = this.stringList.Add(labelSST);
			this.totalStringLength += num;
			int num3;
			if (this.sstRecord)
			{
				num3 = 12;
			}
			else
			{
				num3 = 4;
			}
			if (this.currentLength + num3 + num > 8227)
			{
				if (this.listOfIndexes == null)
				{
					this.listOfIndexes = new ArrayList();
				}
				this.listOfIndexes.Add(new RecordSize(num2, this.currentLength, this.currentCharIndex));
				if (this.sstRecord)
				{
					this.sstStringLength = this.currentLength;
				}
				this.currentLength = 0;
				this.sstRecord = false;
				this.currentCharIndex = 0;
			}
			int num4 = 0;
			while (num + 4 > 8227)
			{
				int num5;
				if (num4 == 0)
				{
					num5 = 4110;
					this.listOfIndexes.Add(new RecordSize(num2 + 1, num5 * 2 + 3, num4));
					num -= num5 * 2 + 3;
				}
				else
				{
					num5 = 4111;
					this.listOfIndexes.Add(new RecordSize(num2 + 1, num5 * 2 + 1, num4));
					num -= num5 * 2;
					this.totalStringLength++;
				}
				num4 += num5;
			}
			this.currentLength += num;
			if (num4 > 0)
			{
				this.currentCharIndex = num4;
				this.currentLength++;
				this.totalStringLength++;
			}
			if (stringRecords != null)
			{
				stringRecords.Add(labelSST, num2);
			}
			return num2;
		}

		// Token: 0x1700225A RID: 8794
		// (get) Token: 0x060068D3 RID: 26835 RVA: 0x0018940A File Offset: 0x0018760A
		public ArrayList ListOfIndexes
		{
			get
			{
				return this.listOfIndexes;
			}
		}

		// Token: 0x1700225B RID: 8795
		// (get) Token: 0x060068D4 RID: 26836 RVA: 0x00189412 File Offset: 0x00187612
		public int SSTStringLength
		{
			get
			{
				return this.sstStringLength;
			}
		}

		// Token: 0x1700225C RID: 8796
		// (get) Token: 0x060068D5 RID: 26837 RVA: 0x0018941A File Offset: 0x0018761A
		public ArrayList StringList
		{
			get
			{
				return this.stringList;
			}
		}

		// Token: 0x1700225D RID: 8797
		// (get) Token: 0x060068D6 RID: 26838 RVA: 0x00189422 File Offset: 0x00187622
		public uint TotalStringCount
		{
			get
			{
				return this.totalStringCount;
			}
		}

		// Token: 0x1700225E RID: 8798
		// (get) Token: 0x060068D7 RID: 26839 RVA: 0x0018942A File Offset: 0x0018762A
		public int TotalStringLength
		{
			get
			{
				return this.totalStringLength;
			}
		}

		// Token: 0x04001C0A RID: 7178
		private int currentCharIndex;

		// Token: 0x04001C0B RID: 7179
		private int currentLength;

		// Token: 0x04001C0C RID: 7180
		private ArrayList listOfIndexes;

		// Token: 0x04001C0D RID: 7181
		private bool sstRecord = true;

		// Token: 0x04001C0E RID: 7182
		private int sstStringLength;

		// Token: 0x04001C0F RID: 7183
		private ArrayList stringList;

		// Token: 0x04001C10 RID: 7184
		private uint totalStringCount;

		// Token: 0x04001C11 RID: 7185
		private int totalStringLength;
	}
}
