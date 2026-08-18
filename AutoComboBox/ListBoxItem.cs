using System;

namespace AutoComboBox
{
	// Token: 0x02000069 RID: 105
	internal class ListBoxItem
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0001F297 File Offset: 0x0001E297
		public ListBoxItem(string name, int id)
		{
			this.id = id;
			this.name = name;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001F2B0 File Offset: 0x0001E2B0
		public ListBoxItem(int id, string name)
		{
			this.id = id;
			this.name = name;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0001F2CC File Offset: 0x0001E2CC
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001F2E4 File Offset: 0x0001E2E4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000393 RID: 915
		private int id;

		// Token: 0x04000394 RID: 916
		private string name;
	}
}
