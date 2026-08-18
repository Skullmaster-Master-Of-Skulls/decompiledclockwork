using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02000F78 RID: 3960
	public class CellProtectionElement : ElementBase
	{
		// Token: 0x060097B8 RID: 38840 RVA: 0x002201C3 File Offset: 0x0021E3C3
		public CellProtectionElement()
		{
			this.IsProtected = true;
		}

		// Token: 0x17002FEA RID: 12266
		// (get) Token: 0x060097B9 RID: 38841 RVA: 0x002201D2 File Offset: 0x0021E3D2
		// (set) Token: 0x060097BA RID: 38842 RVA: 0x002201DA File Offset: 0x0021E3DA
		public bool IsProtected { get; set; }

		// Token: 0x17002FEB RID: 12267
		// (get) Token: 0x060097BB RID: 38843 RVA: 0x002201E3 File Offset: 0x0021E3E3
		protected override string EndTag
		{
			get
			{
				return "</Protection>";
			}
		}

		// Token: 0x17002FEC RID: 12268
		// (get) Token: 0x060097BC RID: 38844 RVA: 0x002201EA File Offset: 0x0021E3EA
		protected override string StartTag
		{
			get
			{
				return "<Protection{0}>";
			}
		}

		// Token: 0x060097BD RID: 38845 RVA: 0x002201F1 File Offset: 0x0021E3F1
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (!this.IsProtected)
			{
				base.Attributes.Add("ss:Protected", "0");
			}
			base.AppendAttributes(sb);
		}
	}
}
