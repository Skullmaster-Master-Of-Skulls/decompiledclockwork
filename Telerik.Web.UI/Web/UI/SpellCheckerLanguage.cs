using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001081 RID: 4225
	public class SpellCheckerLanguage : EditorNameValueItem
	{
		// Token: 0x0600A9EC RID: 43500 RVA: 0x0024DEAB File Offset: 0x0024C0AB
		public SpellCheckerLanguage()
		{
		}

		// Token: 0x0600A9ED RID: 43501 RVA: 0x0024DEB3 File Offset: 0x0024C0B3
		public SpellCheckerLanguage(string code, string title)
		{
			this.Code = code;
			this.Title = title;
		}

		// Token: 0x1700368D RID: 13965
		// (get) Token: 0x0600A9EE RID: 43502 RVA: 0x0024DEC9 File Offset: 0x0024C0C9
		// (set) Token: 0x0600A9EF RID: 43503 RVA: 0x0024DED1 File Offset: 0x0024C0D1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x1700368E RID: 13966
		// (get) Token: 0x0600A9F0 RID: 43504 RVA: 0x0024DEDA File Offset: 0x0024C0DA
		// (set) Token: 0x0600A9F1 RID: 43505 RVA: 0x0024DEE2 File Offset: 0x0024C0E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x1700368F RID: 13967
		// (get) Token: 0x0600A9F2 RID: 43506 RVA: 0x0024DEEB File Offset: 0x0024C0EB
		// (set) Token: 0x0600A9F3 RID: 43507 RVA: 0x0024DEF3 File Offset: 0x0024C0F3
		public string Code
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x17003690 RID: 13968
		// (get) Token: 0x0600A9F4 RID: 43508 RVA: 0x0024DEFC File Offset: 0x0024C0FC
		// (set) Token: 0x0600A9F5 RID: 43509 RVA: 0x0024DF04 File Offset: 0x0024C104
		public string Title
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}
	}
}
