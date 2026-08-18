using System;

namespace System.Windows.Forms
{
	// Token: 0x02000430 RID: 1072
	public class VScrollProperties : ScrollProperties
	{
		// Token: 0x06004A22 RID: 18978 RVA: 0x000BAF09 File Offset: 0x000B9109
		public VScrollProperties(ScrollableControl container) : base(container)
		{
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06004A23 RID: 18979 RVA: 0x0013794C File Offset: 0x00135B4C
		internal override int PageSize
		{
			get
			{
				return base.ParentControl.ClientRectangle.Height;
			}
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06004A24 RID: 18980 RVA: 0x00013062 File Offset: 0x00011262
		internal override int Orientation
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06004A25 RID: 18981 RVA: 0x0013796C File Offset: 0x00135B6C
		internal override int HorizontalDisplayPosition
		{
			get
			{
				return base.ParentControl.DisplayRectangle.X;
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06004A26 RID: 18982 RVA: 0x000BAF34 File Offset: 0x000B9134
		internal override int VerticalDisplayPosition
		{
			get
			{
				return -this.value;
			}
		}
	}
}
