using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestExamBrowser
{
	// Token: 0x02000519 RID: 1305
	public class TestExamRow : BusinessBase<int>
	{
		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x00029D0C File Offset: 0x00027F0C
		// (set) Token: 0x060027F4 RID: 10228 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AppointmentId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x00029D24 File Offset: 0x00027F24
		// (set) Token: 0x060027F6 RID: 10230 RVA: 0x00029D2C File Offset: 0x00027F2C
		public int ExamId { get; set; }

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x00029D35 File Offset: 0x00027F35
		// (set) Token: 0x060027F8 RID: 10232 RVA: 0x00029D3D File Offset: 0x00027F3D
		public int PersonId { get; set; }

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x00029D46 File Offset: 0x00027F46
		// (set) Token: 0x060027FA RID: 10234 RVA: 0x00029D4E File Offset: 0x00027F4E
		public int AppTypeId { get; set; }

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x00029D57 File Offset: 0x00027F57
		// (set) Token: 0x060027FC RID: 10236 RVA: 0x00029D5F File Offset: 0x00027F5F
		public int LuCourseId { get; set; }

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x00029D68 File Offset: 0x00027F68
		// (set) Token: 0x060027FE RID: 10238 RVA: 0x00029D70 File Offset: 0x00027F70
		public int InvigilatorPid { get; set; }

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x060027FF RID: 10239 RVA: 0x00029D79 File Offset: 0x00027F79
		// (set) Token: 0x06002800 RID: 10240 RVA: 0x00029D81 File Offset: 0x00027F81
		public int RoomPid { get; set; }

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06002801 RID: 10241 RVA: 0x00029D8A File Offset: 0x00027F8A
		// (set) Token: 0x06002802 RID: 10242 RVA: 0x00029D92 File Offset: 0x00027F92
		public int SittingId { get; set; }

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06002803 RID: 10243 RVA: 0x00029D9B File Offset: 0x00027F9B
		// (set) Token: 0x06002804 RID: 10244 RVA: 0x00029DA3 File Offset: 0x00027FA3
		public int AppCode { get; set; }

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06002805 RID: 10245 RVA: 0x00029DAC File Offset: 0x00027FAC
		// (set) Token: 0x06002806 RID: 10246 RVA: 0x00029DB4 File Offset: 0x00027FB4
		public int AlternateContactId { get; set; }

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x06002807 RID: 10247 RVA: 0x00029DBD File Offset: 0x00027FBD
		// (set) Token: 0x06002808 RID: 10248 RVA: 0x00029DC5 File Offset: 0x00027FC5
		public int ExamStatusLookupId { get; set; }

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x00029DCE File Offset: 0x00027FCE
		// (set) Token: 0x0600280A RID: 10250 RVA: 0x00029DD6 File Offset: 0x00027FD6
		public string Status { get; set; }

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x0600280B RID: 10251 RVA: 0x00029DDF File Offset: 0x00027FDF
		// (set) Token: 0x0600280C RID: 10252 RVA: 0x00029DE7 File Offset: 0x00027FE7
		public string FirstName { get; set; }

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x0600280D RID: 10253 RVA: 0x00029DF0 File Offset: 0x00027FF0
		// (set) Token: 0x0600280E RID: 10254 RVA: 0x00029DF8 File Offset: 0x00027FF8
		public string LastName { get; set; }

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x00029E01 File Offset: 0x00028001
		// (set) Token: 0x06002810 RID: 10256 RVA: 0x00029E09 File Offset: 0x00028009
		public string Student_no { get; set; }

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x00029E12 File Offset: 0x00028012
		// (set) Token: 0x06002812 RID: 10258 RVA: 0x00029E1A File Offset: 0x0002801A
		public DateTime ScheduledStartTime { get; set; }

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06002813 RID: 10259 RVA: 0x00029E23 File Offset: 0x00028023
		// (set) Token: 0x06002814 RID: 10260 RVA: 0x00029E2B File Offset: 0x0002802B
		public DateTime ScheduledEndTime { get; set; }

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06002815 RID: 10261 RVA: 0x00029E34 File Offset: 0x00028034
		// (set) Token: 0x06002816 RID: 10262 RVA: 0x00029E3C File Offset: 0x0002803C
		public string Description { get; set; }

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x00029E45 File Offset: 0x00028045
		// (set) Token: 0x06002818 RID: 10264 RVA: 0x00029E4D File Offset: 0x0002804D
		public string Room { get; set; }

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06002819 RID: 10265 RVA: 0x00029E56 File Offset: 0x00028056
		// (set) Token: 0x0600281A RID: 10266 RVA: 0x00029E5E File Offset: 0x0002805E
		public string Location { get; set; }

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x00029E67 File Offset: 0x00028067
		// (set) Token: 0x0600281C RID: 10268 RVA: 0x00029E6F File Offset: 0x0002806F
		public string Memo { get; set; }

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x0600281D RID: 10269 RVA: 0x00029E78 File Offset: 0x00028078
		// (set) Token: 0x0600281E RID: 10270 RVA: 0x00029E80 File Offset: 0x00028080
		public DateTime ClassStartTime { get; set; }

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x0600281F RID: 10271 RVA: 0x00029E89 File Offset: 0x00028089
		// (set) Token: 0x06002820 RID: 10272 RVA: 0x00029E91 File Offset: 0x00028091
		public DateTime ClassEndTime { get; set; }

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06002821 RID: 10273 RVA: 0x00029E9A File Offset: 0x0002809A
		// (set) Token: 0x06002822 RID: 10274 RVA: 0x00029EA2 File Offset: 0x000280A2
		public bool Cancelled { get; set; }

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x00029EAB File Offset: 0x000280AB
		// (set) Token: 0x06002824 RID: 10276 RVA: 0x00029EB3 File Offset: 0x000280B3
		public bool NoShow { get; set; }

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x00029EBC File Offset: 0x000280BC
		// (set) Token: 0x06002826 RID: 10278 RVA: 0x00029EC4 File Offset: 0x000280C4
		public bool Tentative { get; set; }

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06002827 RID: 10279 RVA: 0x00029ECD File Offset: 0x000280CD
		// (set) Token: 0x06002828 RID: 10280 RVA: 0x00029ED5 File Offset: 0x000280D5
		public bool InstructorSubmitted { get; set; }

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x00029EDE File Offset: 0x000280DE
		// (set) Token: 0x0600282A RID: 10282 RVA: 0x00029EE6 File Offset: 0x000280E6
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x0600282B RID: 10283 RVA: 0x00029EEF File Offset: 0x000280EF
		// (set) Token: 0x0600282C RID: 10284 RVA: 0x00029EF7 File Offset: 0x000280F7
		public DateTime CourseStartDate { get; set; }

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x00029F00 File Offset: 0x00028100
		// (set) Token: 0x0600282E RID: 10286 RVA: 0x00029F08 File Offset: 0x00028108
		public DateTime CourseEndDate { get; set; }

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x00029F11 File Offset: 0x00028111
		// (set) Token: 0x06002830 RID: 10288 RVA: 0x00029F19 File Offset: 0x00028119
		public string Department { get; set; }

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06002831 RID: 10289 RVA: 0x00029F22 File Offset: 0x00028122
		// (set) Token: 0x06002832 RID: 10290 RVA: 0x00029F2A File Offset: 0x0002812A
		public string DepartmentEmail { get; set; }

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x06002833 RID: 10291 RVA: 0x00029F33 File Offset: 0x00028133
		// (set) Token: 0x06002834 RID: 10292 RVA: 0x00029F3B File Offset: 0x0002813B
		public string DepartmentCode { get; set; }

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x00029F44 File Offset: 0x00028144
		// (set) Token: 0x06002836 RID: 10294 RVA: 0x00029F4C File Offset: 0x0002814C
		public string Term { get; set; }

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06002837 RID: 10295 RVA: 0x00029F55 File Offset: 0x00028155
		// (set) Token: 0x06002838 RID: 10296 RVA: 0x00029F5D File Offset: 0x0002815D
		public string Duration { get; set; }

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x00029F66 File Offset: 0x00028166
		// (set) Token: 0x0600283A RID: 10298 RVA: 0x00029F6E File Offset: 0x0002816E
		public string Subject { get; set; }

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x00029F77 File Offset: 0x00028177
		// (set) Token: 0x0600283C RID: 10300 RVA: 0x00029F7F File Offset: 0x0002817F
		public string Course { get; set; }

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x0600283D RID: 10301 RVA: 0x00029F88 File Offset: 0x00028188
		// (set) Token: 0x0600283E RID: 10302 RVA: 0x00029F90 File Offset: 0x00028190
		public string Section { get; set; }

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x0600283F RID: 10303 RVA: 0x00029F99 File Offset: 0x00028199
		// (set) Token: 0x06002840 RID: 10304 RVA: 0x00029FA1 File Offset: 0x000281A1
		public string TimeOfDay { get; set; }

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06002841 RID: 10305 RVA: 0x00029FAA File Offset: 0x000281AA
		// (set) Token: 0x06002842 RID: 10306 RVA: 0x00029FB2 File Offset: 0x000281B2
		public string ClassRoom { get; set; }

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x00029FBB File Offset: 0x000281BB
		// (set) Token: 0x06002844 RID: 10308 RVA: 0x00029FC3 File Offset: 0x000281C3
		public string Campus { get; set; }

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x00029FCC File Offset: 0x000281CC
		// (set) Token: 0x06002846 RID: 10310 RVA: 0x00029FD4 File Offset: 0x000281D4
		public string PrimaryInstructor { get; set; }

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x00029FDD File Offset: 0x000281DD
		// (set) Token: 0x06002848 RID: 10312 RVA: 0x00029FE5 File Offset: 0x000281E5
		public string PrimaryInstructorEmail { get; set; }

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x06002849 RID: 10313 RVA: 0x00029FEE File Offset: 0x000281EE
		// (set) Token: 0x0600284A RID: 10314 RVA: 0x00029FF6 File Offset: 0x000281F6
		public string PrimaryInstructorPhone { get; set; }

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x00029FFF File Offset: 0x000281FF
		// (set) Token: 0x0600284C RID: 10316 RVA: 0x0002A007 File Offset: 0x00028207
		public string ExamAccommodations { get; set; }

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x0002A010 File Offset: 0x00028210
		// (set) Token: 0x0600284E RID: 10318 RVA: 0x0002A018 File Offset: 0x00028218
		public string AccommodationGroups { get; set; }

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x0002A021 File Offset: 0x00028221
		// (set) Token: 0x06002850 RID: 10320 RVA: 0x0002A029 File Offset: 0x00028229
		public int TotalBreakMinutes { get; set; }

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x0002A032 File Offset: 0x00028232
		// (set) Token: 0x06002852 RID: 10322 RVA: 0x0002A03A File Offset: 0x0002823A
		public string AssignedAdvisorFirstName { get; set; }

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x0002A043 File Offset: 0x00028243
		// (set) Token: 0x06002854 RID: 10324 RVA: 0x0002A04B File Offset: 0x0002824B
		public string AssingedAdvisorLastName { get; set; }

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x0002A054 File Offset: 0x00028254
		// (set) Token: 0x06002856 RID: 10326 RVA: 0x0002A05C File Offset: 0x0002825C
		public int AssignedAdvisorPersonId { get; set; }

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x0002A065 File Offset: 0x00028265
		// (set) Token: 0x06002858 RID: 10328 RVA: 0x0002A06D File Offset: 0x0002826D
		public string Invigilator { get; set; }

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x0002A076 File Offset: 0x00028276
		// (set) Token: 0x0600285A RID: 10330 RVA: 0x0002A07E File Offset: 0x0002827E
		public DateTime DateAdded { get; set; }

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x0002A087 File Offset: 0x00028287
		// (set) Token: 0x0600285C RID: 10332 RVA: 0x0002A08F File Offset: 0x0002828F
		public string WhoBooked { get; set; }

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x0002A098 File Offset: 0x00028298
		// (set) Token: 0x0600285E RID: 10334 RVA: 0x0002A0A0 File Offset: 0x000282A0
		public int WhoBookedPersonId { get; set; }

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x0002A0A9 File Offset: 0x000282A9
		// (set) Token: 0x06002860 RID: 10336 RVA: 0x0002A0B1 File Offset: 0x000282B1
		public DateTime? ActualStartTime { get; set; }

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x0002A0BA File Offset: 0x000282BA
		// (set) Token: 0x06002862 RID: 10338 RVA: 0x0002A0C2 File Offset: 0x000282C2
		public DateTime? ActualEndTime { get; set; }

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06002863 RID: 10339 RVA: 0x0002A0CB File Offset: 0x000282CB
		// (set) Token: 0x06002864 RID: 10340 RVA: 0x0002A0D3 File Offset: 0x000282D3
		public string TestDelivered { get; set; }

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x0002A0DC File Offset: 0x000282DC
		// (set) Token: 0x06002866 RID: 10342 RVA: 0x0002A0E4 File Offset: 0x000282E4
		public DateTime? StudentReportedClassStartTime { get; set; }

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06002867 RID: 10343 RVA: 0x0002A0ED File Offset: 0x000282ED
		// (set) Token: 0x06002868 RID: 10344 RVA: 0x0002A0F5 File Offset: 0x000282F5
		public DateTime? StudentReportedClassEndTime { get; set; }

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x0002A0FE File Offset: 0x000282FE
		// (set) Token: 0x0600286A RID: 10346 RVA: 0x0002A106 File Offset: 0x00028306
		public string AlternateContact { get; set; }

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x0002A10F File Offset: 0x0002830F
		// (set) Token: 0x0600286C RID: 10348 RVA: 0x0002A117 File Offset: 0x00028317
		public string AlternateContactEmail { get; set; }

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x0002A120 File Offset: 0x00028320
		// (set) Token: 0x0600286E RID: 10350 RVA: 0x0002A128 File Offset: 0x00028328
		public string AlternateContactPhone { get; set; }

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x0600286F RID: 10351 RVA: 0x0002A131 File Offset: 0x00028331
		// (set) Token: 0x06002870 RID: 10352 RVA: 0x0002A139 File Offset: 0x00028339
		public string AlternateContactUsername { get; set; }

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06002871 RID: 10353 RVA: 0x0002A142 File Offset: 0x00028342
		// (set) Token: 0x06002872 RID: 10354 RVA: 0x0002A14A File Offset: 0x0002834A
		public int AlternateContactPermissionLevel { get; set; }

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06002873 RID: 10355 RVA: 0x0002A153 File Offset: 0x00028353
		// (set) Token: 0x06002874 RID: 10356 RVA: 0x0002A15B File Offset: 0x0002835B
		public string InstructorAcknowledged { get; set; }

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x0002A164 File Offset: 0x00028364
		// (set) Token: 0x06002876 RID: 10358 RVA: 0x0002A16C File Offset: 0x0002836C
		public string InstructorAcknowledgedOnline { get; set; }

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x06002877 RID: 10359 RVA: 0x0002A175 File Offset: 0x00028375
		// (set) Token: 0x06002878 RID: 10360 RVA: 0x0002A17D File Offset: 0x0002837D
		public DateTime? InstructorAcknolwedgedDate { get; set; }

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x06002879 RID: 10361 RVA: 0x0002A186 File Offset: 0x00028386
		// (set) Token: 0x0600287A RID: 10362 RVA: 0x0002A18E File Offset: 0x0002838E
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x0600287B RID: 10363 RVA: 0x0002A197 File Offset: 0x00028397
		// (set) Token: 0x0600287C RID: 10364 RVA: 0x0002A19F File Offset: 0x0002839F
		public string InstructorContactedNote { get; set; }

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x0002A1A8 File Offset: 0x000283A8
		// (set) Token: 0x0600287E RID: 10366 RVA: 0x0002A1B0 File Offset: 0x000283B0
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x0002A1B9 File Offset: 0x000283B9
		// (set) Token: 0x06002880 RID: 10368 RVA: 0x0002A1C1 File Offset: 0x000283C1
		public string TestPickedUpNote { get; set; }

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06002881 RID: 10369 RVA: 0x0002A1CA File Offset: 0x000283CA
		// (set) Token: 0x06002882 RID: 10370 RVA: 0x0002A1D2 File Offset: 0x000283D2
		public string PrivateNote2 { get; set; }

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06002883 RID: 10371 RVA: 0x0002A1DB File Offset: 0x000283DB
		// (set) Token: 0x06002884 RID: 10372 RVA: 0x0002A1E3 File Offset: 0x000283E3
		public string ExamStatus { get; set; }

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x0002A1EC File Offset: 0x000283EC
		// (set) Token: 0x06002886 RID: 10374 RVA: 0x0002A1F4 File Offset: 0x000283F4
		public int ColourArgB { get; set; }

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06002887 RID: 10375 RVA: 0x0002A1FD File Offset: 0x000283FD
		// (set) Token: 0x06002888 RID: 10376 RVA: 0x0002A205 File Offset: 0x00028405
		public string Sitting { get; set; }

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06002889 RID: 10377 RVA: 0x0002A20E File Offset: 0x0002840E
		// (set) Token: 0x0600288A RID: 10378 RVA: 0x0002A216 File Offset: 0x00028416
		public string SittingRoom { get; set; }

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x0600288B RID: 10379 RVA: 0x0002A21F File Offset: 0x0002841F
		// (set) Token: 0x0600288C RID: 10380 RVA: 0x0002A227 File Offset: 0x00028427
		public string SittingLocation { get; set; }

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x0002A230 File Offset: 0x00028430
		// (set) Token: 0x0600288E RID: 10382 RVA: 0x0002A238 File Offset: 0x00028438
		public string SittingInvigilator { get; set; }
	}
}
