using System;

namespace AutoComboBox.MyControls.MultiLineTextBox
{
	// Token: 0x020000DB RID: 219
	public class MultiLineItem : IComparable
	{
		// Token: 0x060008A5 RID: 2213 RVA: 0x000430ED File Offset: 0x000420ED
		public MultiLineItem(string text, string whoEntered, DateTime dateEntered)
		{
			this.text = text;
			this.whoEntered = whoEntered;
			this.dateEntered = dateEntered;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00043110 File Offset: 0x00042110
		public string WhoEntered
		{
			get
			{
				return this.whoEntered;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00043128 File Offset: 0x00042128
		public DateTime DateEntered
		{
			get
			{
				return this.dateEntered;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00043140 File Offset: 0x00042140
		public string DateEnteredString
		{
			get
			{
				return this.dateEntered.ToString("yyyy-MM-dd");
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00043164 File Offset: 0x00042164
		public override string ToString()
		{
			return this.text;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0004317C File Offset: 0x0004217C
		public string Header
		{
			get
			{
				return this.dateEntered.ToString("yyyy-MM-dd") + " [" + this.whoEntered + "]";
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x000431B4 File Offset: 0x000421B4
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x000431CC File Offset: 0x000421CC
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x000431D8 File Offset: 0x000421D8
		public int CompareTo(object obj)
		{
			int result;
			if (obj != null && obj is MultiLineItem)
			{
				MultiLineItem multiLineItem = (MultiLineItem)obj;
				result = this.DateEntered.CompareTo(multiLineItem.DateEntered);
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x04000641 RID: 1601
		private string text;

		// Token: 0x04000642 RID: 1602
		private string whoEntered;

		// Token: 0x04000643 RID: 1603
		private DateTime dateEntered;
	}
}
