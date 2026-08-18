using System;
using System.Globalization;

namespace System.Web.Management
{
	// Token: 0x0200017E RID: 382
	internal class TemplatedMailRuntimeErrorFormatter : UnhandledErrorFormatter
	{
		// Token: 0x060014F6 RID: 5366 RVA: 0x0003FEC9 File Offset: 0x0003E0C9
		internal TemplatedMailRuntimeErrorFormatter(Exception e, int eventsRemaining, bool showDetails) : base(e)
		{
			this._eventsRemaining = eventsRemaining;
			this._showDetails = showDetails;
			this._dontShowVersion = true;
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x0003FEE8 File Offset: 0x0003E0E8
		protected override string ErrorTitle
		{
			get
			{
				if (HttpException.GetHttpCodeForException(this.Exception) == 404)
				{
					return SR.GetString("MailWebEventProvider_template_file_not_found_error", new object[]
					{
						this._eventsRemaining.ToString(CultureInfo.InstalledUICulture)
					});
				}
				return SR.GetString("MailWebEventProvider_template_runtime_error", new object[]
				{
					this._eventsRemaining.ToString(CultureInfo.InstalledUICulture)
				});
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override string ColoredSquareContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0003FF4E File Offset: 0x0003E14E
		protected override string Description
		{
			get
			{
				if (this._showDetails)
				{
					return base.Description;
				}
				return SR.GetString("MailWebEventProvider_template_error_no_details");
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x0003FF69 File Offset: 0x0003E169
		protected override string MiscSectionTitle
		{
			get
			{
				if (this._showDetails)
				{
					return base.MiscSectionTitle;
				}
				return null;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0003FF7B File Offset: 0x0003E17B
		protected override string MiscSectionContent
		{
			get
			{
				if (this._showDetails)
				{
					return base.MiscSectionContent;
				}
				return null;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0003FF8D File Offset: 0x0003E18D
		protected override string ColoredSquare2Title
		{
			get
			{
				if (this._showDetails)
				{
					return base.ColoredSquare2Title;
				}
				return null;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x0003FF9F File Offset: 0x0003E19F
		protected override string ColoredSquare2Content
		{
			get
			{
				if (this._showDetails)
				{
					return base.ColoredSquare2Content;
				}
				return null;
			}
		}

		// Token: 0x040015A3 RID: 5539
		private int _eventsRemaining;

		// Token: 0x040015A4 RID: 5540
		private bool _showDetails;
	}
}
