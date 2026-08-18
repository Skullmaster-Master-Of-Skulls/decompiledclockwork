using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerAdvancedTemplate
{
	// Token: 0x02000815 RID: 2069
	internal interface IAdvancedTemplateView
	{
		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x06004BE0 RID: 19424
		AdvancedTemplate Owner { get; }

		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x06004BE1 RID: 19425
		// (set) Token: 0x06004BE2 RID: 19426
		Appointment Appointment { get; set; }

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x06004BE3 RID: 19427
		// (set) Token: 0x06004BE4 RID: 19428
		IDictionary<string, DataBoundControl> ResourceControls { get; set; }

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x06004BE5 RID: 19429
		// (set) Token: 0x06004BE6 RID: 19430
		IDictionary<string, WebControl> AttributeControls { get; set; }

		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x06004BE7 RID: 19431
		// (set) Token: 0x06004BE8 RID: 19432
		LinkButton CloseButton { get; set; }

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x06004BE9 RID: 19433
		// (set) Token: 0x06004BEA RID: 19434
		LinkButton CancelButton { get; set; }

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x06004BEB RID: 19435
		// (set) Token: 0x06004BEC RID: 19436
		LinkButton InsertButton { get; set; }

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x06004BED RID: 19437
		// (set) Token: 0x06004BEE RID: 19438
		LinkButton UpdateButton { get; set; }

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x06004BEF RID: 19439
		// (set) Token: 0x06004BF0 RID: 19440
		WebControl Subject { get; set; }

		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x06004BF1 RID: 19441
		// (set) Token: 0x06004BF2 RID: 19442
		WebControl Description { get; set; }

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x06004BF3 RID: 19443
		// (set) Token: 0x06004BF4 RID: 19444
		Control StartTime { get; set; }

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x06004BF5 RID: 19445
		// (set) Token: 0x06004BF6 RID: 19446
		Control StartDate { get; set; }

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x06004BF7 RID: 19447
		// (set) Token: 0x06004BF8 RID: 19448
		Control EndTime { get; set; }

		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x06004BF9 RID: 19449
		// (set) Token: 0x06004BFA RID: 19450
		Control EndDate { get; set; }

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x06004BFB RID: 19451
		// (set) Token: 0x06004BFC RID: 19452
		CheckBox AllDayEvent { get; set; }

		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x06004BFD RID: 19453
		// (set) Token: 0x06004BFE RID: 19454
		DataBoundControl Reminder { get; set; }

		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x06004BFF RID: 19455
		// (set) Token: 0x06004C00 RID: 19456
		DataBoundControl TimeZones { get; set; }

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x06004C01 RID: 19457
		// (set) Token: 0x06004C02 RID: 19458
		RadCalendar SharedCalendar { get; set; }

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x06004C03 RID: 19459
		// (set) Token: 0x06004C04 RID: 19460
		LinkButton ResetExceptions { get; set; }

		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x06004C05 RID: 19461
		DateTime StartDateValue { get; }

		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x06004C06 RID: 19462
		TimeSpan StartTimeValue { get; }

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x06004C07 RID: 19463
		DateTime EndDateValue { get; }

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x06004C08 RID: 19464
		TimeSpan EndTimeValue { get; }

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x06004C09 RID: 19465
		string SelectedTimeZone { get; }

		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x06004C0A RID: 19466
		string SelectedReminder { get; }

		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x06004C0B RID: 19467
		string SubjectText { get; }

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x06004C0C RID: 19468
		string DescriptionText { get; }

		// Token: 0x06004C0D RID: 19469
		void CreateControls();

		// Token: 0x06004C0E RID: 19470
		void ExtractAttributeValues(IDictionary target);

		// Token: 0x06004C0F RID: 19471
		void ExtractResourceValues(IDictionary target);

		// Token: 0x06004C10 RID: 19472
		void CreateInsertButtons();

		// Token: 0x06004C11 RID: 19473
		void CreateEditButtons();
	}
}
