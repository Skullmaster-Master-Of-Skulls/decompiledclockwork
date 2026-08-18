using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001A18 RID: 6680
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public interface IRecurrenceEditorStrings
	{
		// Token: 0x17004E04 RID: 19972
		// (get) Token: 0x06010277 RID: 66167
		// (set) Token: 0x06010278 RID: 66168
		string Recurrence { get; set; }

		// Token: 0x17004E05 RID: 19973
		// (get) Token: 0x06010279 RID: 66169
		// (set) Token: 0x0601027A RID: 66170
		string RepeatAppointment { get; set; }

		// Token: 0x17004E06 RID: 19974
		// (get) Token: 0x0601027B RID: 66171
		// (set) Token: 0x0601027C RID: 66172
		string Repeat { get; set; }

		// Token: 0x17004E07 RID: 19975
		// (get) Token: 0x0601027D RID: 66173
		// (set) Token: 0x0601027E RID: 66174
		string RepeatOn { get; set; }

		// Token: 0x17004E08 RID: 19976
		// (get) Token: 0x0601027F RID: 66175
		// (set) Token: 0x06010280 RID: 66176
		string RepeatEnd { get; set; }

		// Token: 0x17004E09 RID: 19977
		// (get) Token: 0x06010281 RID: 66177
		// (set) Token: 0x06010282 RID: 66178
		string Never { get; set; }

		// Token: 0x17004E0A RID: 19978
		// (get) Token: 0x06010283 RID: 66179
		// (set) Token: 0x06010284 RID: 66180
		string After { get; set; }

		// Token: 0x17004E0B RID: 19979
		// (get) Token: 0x06010285 RID: 66181
		// (set) Token: 0x06010286 RID: 66182
		string On { get; set; }

		// Token: 0x17004E0C RID: 19980
		// (get) Token: 0x06010287 RID: 66183
		// (set) Token: 0x06010288 RID: 66184
		string DayOfMonth { get; set; }

		// Token: 0x17004E0D RID: 19981
		// (get) Token: 0x06010289 RID: 66185
		// (set) Token: 0x0601028A RID: 66186
		string DayOfWeek { get; set; }

		// Token: 0x17004E0E RID: 19982
		// (get) Token: 0x0601028B RID: 66187
		// (set) Token: 0x0601028C RID: 66188
		string Hourly { get; set; }

		// Token: 0x17004E0F RID: 19983
		// (get) Token: 0x0601028D RID: 66189
		// (set) Token: 0x0601028E RID: 66190
		string Daily { get; set; }

		// Token: 0x17004E10 RID: 19984
		// (get) Token: 0x0601028F RID: 66191
		// (set) Token: 0x06010290 RID: 66192
		string Weekly { get; set; }

		// Token: 0x17004E11 RID: 19985
		// (get) Token: 0x06010291 RID: 66193
		// (set) Token: 0x06010292 RID: 66194
		string Monthly { get; set; }

		// Token: 0x17004E12 RID: 19986
		// (get) Token: 0x06010293 RID: 66195
		// (set) Token: 0x06010294 RID: 66196
		string Yearly { get; set; }

		// Token: 0x17004E13 RID: 19987
		// (get) Token: 0x06010295 RID: 66197
		// (set) Token: 0x06010296 RID: 66198
		string Every { get; set; }

		// Token: 0x17004E14 RID: 19988
		// (get) Token: 0x06010297 RID: 66199
		// (set) Token: 0x06010298 RID: 66200
		string Hours { get; set; }

		// Token: 0x17004E15 RID: 19989
		// (get) Token: 0x06010299 RID: 66201
		// (set) Token: 0x0601029A RID: 66202
		string Days { get; set; }

		// Token: 0x17004E16 RID: 19990
		// (get) Token: 0x0601029B RID: 66203
		// (set) Token: 0x0601029C RID: 66204
		string Weeks { get; set; }

		// Token: 0x17004E17 RID: 19991
		// (get) Token: 0x0601029D RID: 66205
		// (set) Token: 0x0601029E RID: 66206
		string Months { get; set; }

		// Token: 0x17004E18 RID: 19992
		// (get) Token: 0x0601029F RID: 66207
		// (set) Token: 0x060102A0 RID: 66208
		string Years { get; set; }

		// Token: 0x17004E19 RID: 19993
		// (get) Token: 0x060102A1 RID: 66209
		// (set) Token: 0x060102A2 RID: 66210
		string EveryWeekday { get; set; }

		// Token: 0x17004E1A RID: 19994
		// (get) Token: 0x060102A3 RID: 66211
		// (set) Token: 0x060102A4 RID: 66212
		string EveryWorkingDay { get; set; }

		// Token: 0x17004E1B RID: 19995
		// (get) Token: 0x060102A5 RID: 66213
		// (set) Token: 0x060102A6 RID: 66214
		string RecurEvery { get; set; }

		// Token: 0x17004E1C RID: 19996
		// (get) Token: 0x060102A7 RID: 66215
		// (set) Token: 0x060102A8 RID: 66216
		string Day { get; set; }

		// Token: 0x17004E1D RID: 19997
		// (get) Token: 0x060102A9 RID: 66217
		// (set) Token: 0x060102AA RID: 66218
		string OfEvery { get; set; }

		// Token: 0x17004E1E RID: 19998
		// (get) Token: 0x060102AB RID: 66219
		// (set) Token: 0x060102AC RID: 66220
		string First { get; set; }

		// Token: 0x17004E1F RID: 19999
		// (get) Token: 0x060102AD RID: 66221
		// (set) Token: 0x060102AE RID: 66222
		string Second { get; set; }

		// Token: 0x17004E20 RID: 20000
		// (get) Token: 0x060102AF RID: 66223
		// (set) Token: 0x060102B0 RID: 66224
		string Third { get; set; }

		// Token: 0x17004E21 RID: 20001
		// (get) Token: 0x060102B1 RID: 66225
		// (set) Token: 0x060102B2 RID: 66226
		string Fourth { get; set; }

		// Token: 0x17004E22 RID: 20002
		// (get) Token: 0x060102B3 RID: 66227
		// (set) Token: 0x060102B4 RID: 66228
		string Last { get; set; }

		// Token: 0x17004E23 RID: 20003
		// (get) Token: 0x060102B5 RID: 66229
		// (set) Token: 0x060102B6 RID: 66230
		string MaskDay { get; set; }

		// Token: 0x17004E24 RID: 20004
		// (get) Token: 0x060102B7 RID: 66231
		// (set) Token: 0x060102B8 RID: 66232
		string MaskWeekday { get; set; }

		// Token: 0x17004E25 RID: 20005
		// (get) Token: 0x060102B9 RID: 66233
		// (set) Token: 0x060102BA RID: 66234
		string MaskWeekendDay { get; set; }

		// Token: 0x17004E26 RID: 20006
		// (get) Token: 0x060102BB RID: 66235
		// (set) Token: 0x060102BC RID: 66236
		string The { get; set; }

		// Token: 0x17004E27 RID: 20007
		// (get) Token: 0x060102BD RID: 66237
		// (set) Token: 0x060102BE RID: 66238
		string Of { get; set; }

		// Token: 0x17004E28 RID: 20008
		// (get) Token: 0x060102BF RID: 66239
		// (set) Token: 0x060102C0 RID: 66240
		string NoEndDate { get; set; }

		// Token: 0x17004E29 RID: 20009
		// (get) Token: 0x060102C1 RID: 66241
		// (set) Token: 0x060102C2 RID: 66242
		string EndAfter { get; set; }

		// Token: 0x17004E2A RID: 20010
		// (get) Token: 0x060102C3 RID: 66243
		// (set) Token: 0x060102C4 RID: 66244
		string EndByThisDate { get; set; }

		// Token: 0x17004E2B RID: 20011
		// (get) Token: 0x060102C5 RID: 66245
		// (set) Token: 0x060102C6 RID: 66246
		string Occurrences { get; set; }

		// Token: 0x17004E2C RID: 20012
		// (get) Token: 0x060102C7 RID: 66247
		// (set) Token: 0x060102C8 RID: 66248
		string CalendarOK { get; set; }

		// Token: 0x17004E2D RID: 20013
		// (get) Token: 0x060102C9 RID: 66249
		// (set) Token: 0x060102CA RID: 66250
		string CalendarCancel { get; set; }

		// Token: 0x17004E2E RID: 20014
		// (get) Token: 0x060102CB RID: 66251
		// (set) Token: 0x060102CC RID: 66252
		string CalendarToday { get; set; }

		// Token: 0x17004E2F RID: 20015
		// (get) Token: 0x060102CD RID: 66253
		// (set) Token: 0x060102CE RID: 66254
		string Save { get; set; }

		// Token: 0x17004E30 RID: 20016
		// (get) Token: 0x060102CF RID: 66255
		// (set) Token: 0x060102D0 RID: 66256
		string Cancel { get; set; }
	}
}
