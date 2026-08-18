using System;

namespace System.Windows.Forms
{
	// Token: 0x02000279 RID: 633
	public class HScrollProperties : ScrollProperties
	{
		// Token: 0x06002838 RID: 10296 RVA: 0x000BAF09 File Offset: 0x000B9109
		public HScrollProperties(ScrollableControl container) : base(container)
		{
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x000BAF14 File Offset: 0x000B9114
		internal override int PageSize
		{
			get
			{
				return base.ParentControl.ClientRectangle.Width;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal override int Orientation
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x000BAF34 File Offset: 0x000B9134
		internal override int HorizontalDisplayPosition
		{
			get
			{
				return -this.value;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x0600283C RID: 10300 RVA: 0x000BAF40 File Offset: 0x000B9140
		internal override int VerticalDisplayPosition
		{
			get
			{
				return base.ParentControl.DisplayRectangle.Y;
			}
		}
	}
}
