using System;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004A8 RID: 1192
	internal class GetNameItemEvent : EventArgs
	{
		// Token: 0x06004F36 RID: 20278 RVA: 0x001463F3 File Offset: 0x001445F3
		public GetNameItemEvent(object defName)
		{
			this.nameItem = defName;
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06004F37 RID: 20279 RVA: 0x00146402 File Offset: 0x00144602
		// (set) Token: 0x06004F38 RID: 20280 RVA: 0x0014640A File Offset: 0x0014460A
		public object Name
		{
			get
			{
				return this.nameItem;
			}
			set
			{
				this.nameItem = value;
			}
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06004F39 RID: 20281 RVA: 0x00146413 File Offset: 0x00144613
		public string NameString
		{
			get
			{
				if (this.nameItem != null)
				{
					return this.nameItem.ToString();
				}
				return "";
			}
		}

		// Token: 0x0400344D RID: 13389
		private object nameItem;
	}
}
