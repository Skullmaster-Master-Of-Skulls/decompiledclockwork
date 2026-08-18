using System;
using TechnoPro.Common.Public.Entities.Academic;
using TechnoPro.Common.Public.Entities.General;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x02000102 RID: 258
	public class VetsBenefitApplication : BusinessBase<Guid>, ICloneable<VetsBenefitApplication>, ICloneable
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x0000EDF5 File Offset: 0x0000CFF5
		public VetsBenefitApplication()
		{
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000EDFF File Offset: 0x0000CFFF
		public VetsBenefitApplication(VetsBenefitApplication app)
		{
			this.Copy(app, this);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000EE14 File Offset: 0x0000D014
		public void Copy(VetsBenefitApplication appSource, VetsBenefitApplication appDest)
		{
			appDest.BenefitApplicationId = appSource.BenefitApplicationId;
			appDest.Student = appSource.Student.Clone();
			appDest.Semester = appSource.Semester;
			appDest.PerSemesterId = appSource.PerSemesterId;
			appDest.Chapter = appSource.Chapter;
			appDest.StudentAgreed = appSource.StudentAgreed;
			appDest.BenAppCompleted = appSource.BenAppCompleted;
			appDest.PreferredStep = appSource.PreferredStep;
			appDest.FinalStatus = appSource.FinalStatus;
			appDest.MinPageAllow = appSource.MinPageAllow;
			appDest.MaxPageAllow = appSource.MaxPageAllow;
			appDest.ScreenerPersonId = appSource.ScreenerPersonId;
			appDest.CertifierPersonId = appSource.CertifierPersonId;
			appDest.CurrentProgressStepId = appSource.CurrentProgressStepId;
			appDest.ModificationHistoryItem = appSource.ModificationHistoryItem;
			appDest.RegistrationCompleted = appSource.RegistrationCompleted;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		public T Clone<T>() where T : VetsBenefitApplication
		{
			T t = Activator.CreateInstance<T>();
			t.Copy(this, t);
			return t;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000EF24 File Offset: 0x0000D124
		public VetsBenefitApplication Clone()
		{
			return new VetsBenefitApplication(this);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000EF3C File Offset: 0x0000D13C
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000EF54 File Offset: 0x0000D154
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public virtual Guid BenefitApplicationId
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

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000EF6C File Offset: 0x0000D16C
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0000EF74 File Offset: 0x0000D174
		public PersonBase Student { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000EF7D File Offset: 0x0000D17D
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x0000EF85 File Offset: 0x0000D185
		public virtual Semester Semester { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000EF8E File Offset: 0x0000D18E
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0000EF96 File Offset: 0x0000D196
		public virtual int PerSemesterId { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000EF9F File Offset: 0x0000D19F
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x0000EFA7 File Offset: 0x0000D1A7
		public virtual VetsChapter Chapter { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x0000EFB8 File Offset: 0x0000D1B8
		public bool StudentAgreed { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000EFC1 File Offset: 0x0000D1C1
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0000EFC9 File Offset: 0x0000D1C9
		public bool BenAppCompleted { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0000EFD2 File Offset: 0x0000D1D2
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0000EFDA File Offset: 0x0000D1DA
		public bool RegistrationCompleted { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000EFE3 File Offset: 0x0000D1E3
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0000EFEB File Offset: 0x0000D1EB
		public eVetsBenefitApplicationStep? PreferredStep { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		public eVetsRequestStatus FinalStatus { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000F005 File Offset: 0x0000D205
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0000F00D File Offset: 0x0000D20D
		public eVetsBenefitApplicationStep MinPageAllow { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000F016 File Offset: 0x0000D216
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x0000F01E File Offset: 0x0000D21E
		public eVetsBenefitApplicationStep MaxPageAllow { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0000F027 File Offset: 0x0000D227
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x0000F02F File Offset: 0x0000D22F
		public int ScreenerPersonId { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0000F038 File Offset: 0x0000D238
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x0000F040 File Offset: 0x0000D240
		public int CertifierPersonId { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0000F049 File Offset: 0x0000D249
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x0000F051 File Offset: 0x0000D251
		public Guid CurrentProgressStepId { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000F05A File Offset: 0x0000D25A
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x0000F062 File Offset: 0x0000D262
		public ModificationHistoryItemBase ModificationHistoryItem { get; set; }
	}
}
