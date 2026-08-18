using System;

namespace System.Windows.Forms
{
	// Token: 0x02000428 RID: 1064
	public class UICuesEventArgs : EventArgs
	{
		// Token: 0x060049EC RID: 18924 RVA: 0x00137581 File Offset: 0x00135781
		public UICuesEventArgs(UICues uicues)
		{
			this.uicues = uicues;
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x060049ED RID: 18925 RVA: 0x00137590 File Offset: 0x00135790
		public bool ShowFocus
		{
			get
			{
				return (this.uicues & UICues.ShowFocus) > UICues.None;
			}
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x060049EE RID: 18926 RVA: 0x0013759D File Offset: 0x0013579D
		public bool ShowKeyboard
		{
			get
			{
				return (this.uicues & UICues.ShowKeyboard) > UICues.None;
			}
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x060049EF RID: 18927 RVA: 0x001375AA File Offset: 0x001357AA
		public bool ChangeFocus
		{
			get
			{
				return (this.uicues & UICues.ChangeFocus) > UICues.None;
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x060049F0 RID: 18928 RVA: 0x001375B7 File Offset: 0x001357B7
		public bool ChangeKeyboard
		{
			get
			{
				return (this.uicues & UICues.ChangeKeyboard) > UICues.None;
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x060049F1 RID: 18929 RVA: 0x001375C4 File Offset: 0x001357C4
		public UICues Changed
		{
			get
			{
				return this.uicues & UICues.Changed;
			}
		}

		// Token: 0x040027C7 RID: 10183
		private readonly UICues uicues;
	}
}
