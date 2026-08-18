using System;
using System.Globalization;

namespace System.Web.Management
{
	// Token: 0x0200017D RID: 381
	internal class TemplatedMailCompileErrorFormatter : DynamicCompileErrorFormatter
	{
		// Token: 0x060014F1 RID: 5361 RVA: 0x0003FE40 File Offset: 0x0003E040
		internal TemplatedMailCompileErrorFormatter(HttpCompileException e, int eventsRemaining, bool showDetails) : base(e)
		{
			this._eventsRemaining = eventsRemaining;
			this._showDetails = showDetails;
			this._hideDetailedCompilerOutput = true;
			this._dontShowVersion = true;
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0003FE65 File Offset: 0x0003E065
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("MailWebEventProvider_template_compile_error", new object[]
				{
					this._eventsRemaining.ToString(CultureInfo.InstalledUICulture)
				});
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x0003FE8A File Offset: 0x0003E08A
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

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x0003FEA5 File Offset: 0x0003E0A5
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

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x0003FEB7 File Offset: 0x0003E0B7
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

		// Token: 0x040015A1 RID: 5537
		private int _eventsRemaining;

		// Token: 0x040015A2 RID: 5538
		private bool _showDetails;
	}
}
