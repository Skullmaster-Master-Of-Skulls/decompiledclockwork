using System;
using System.Collections.Generic;
using System.Data;
using UnivOleDb;

namespace ClockWorkAPI.ServiceProviders
{
	// Token: 0x02000069 RID: 105
	public class ServiceProvider
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001D05C File Offset: 0x0001C05C
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x0001D073 File Offset: 0x0001C073
		public string Email { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001D07C File Offset: 0x0001C07C
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0001D093 File Offset: 0x0001C093
		public string AlternateEmail { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001D09C File Offset: 0x0001C09C
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0001D0B3 File Offset: 0x0001C0B3
		public string Phone { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0001D0BC File Offset: 0x0001C0BC
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0001D0D3 File Offset: 0x0001C0D3
		public string Cell { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001D0DC File Offset: 0x0001C0DC
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x0001D0F3 File Offset: 0x0001C0F3
		public string Specialization { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0001D0FC File Offset: 0x0001C0FC
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x0001D113 File Offset: 0x0001C113
		public string FirstName { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001D11C File Offset: 0x0001C11C
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x0001D133 File Offset: 0x0001C133
		public string LastName { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001D13C File Offset: 0x0001C13C
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x0001D153 File Offset: 0x0001C153
		public string Student_no { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001D15C File Offset: 0x0001C15C
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0001D173 File Offset: 0x0001C173
		public string MiddleName { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001D17C File Offset: 0x0001C17C
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0001D193 File Offset: 0x0001C193
		public int ServiceProviderId { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001D19C File Offset: 0x0001C19C
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0001D1B3 File Offset: 0x0001C1B3
		public string AltId { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0001D1BC File Offset: 0x0001C1BC
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0001D1D3 File Offset: 0x0001C1D3
		public string Address { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001D1DC File Offset: 0x0001C1DC
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x0001D1F3 File Offset: 0x0001C1F3
		public string PermanentAddress { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001D1FC File Offset: 0x0001C1FC
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x0001D213 File Offset: 0x0001C213
		public string Phone2 { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001D21C File Offset: 0x0001C21C
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0001D234 File Offset: 0x0001C234
		public eMatchingMethod MatchingMethod
		{
			get
			{
				return this.matchingMethod;
			}
			set
			{
				this.matchingMethod = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0001D240 File Offset: 0x0001C240
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x0001D258 File Offset: 0x0001C258
		public bool ServiceAppliesToIndividualCourses
		{
			get
			{
				return this.serviceAppliesToIndividualCourses;
			}
			set
			{
				this.serviceAppliesToIndividualCourses = value;
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001D262 File Offset: 0x0001C262
		public ServiceProvider()
		{
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001D274 File Offset: 0x0001C274
		public ServiceProvider(eServiceProviderType serviceProviderType, string serviceTypeDescriptions)
		{
			this.serviceTypeDescriptions = serviceTypeDescriptions.Split(new char[]
			{
				','
			});
			this.serviceProviderType = serviceProviderType;
			this.displayName = this.GetServiceTypeDisplayName(serviceProviderType);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001D2C0 File Offset: 0x0001C2C0
		public string GetServiceTypeDisplayName(eServiceProviderType type)
		{
			if (this.eTypes == null)
			{
				this.eTypes = ServiceProvider.GetETypes();
			}
			return ServiceProvider.GetServiceTypeDisplayName(type, this.serviceTypeDescriptions, this.eTypes);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001D304 File Offset: 0x0001C304
		private static List<eServiceProviderType> GetETypes()
		{
			return new List<eServiceProviderType>
			{
				eServiceProviderType.Unknown,
				eServiceProviderType.Interpreter,
				eServiceProviderType.Teamer,
				eServiceProviderType.Professional_notetaker,
				eServiceProviderType.Coach,
				eServiceProviderType.Specialized_tutor,
				eServiceProviderType.Real_time_captioner,
				eServiceProviderType.Peer_assistant,
				eServiceProviderType.Peer_notetaker,
				eServiceProviderType.Peer_tutor,
				eServiceProviderType.Custom1,
				eServiceProviderType.Custom2,
				eServiceProviderType.Custom3,
				eServiceProviderType.Custom4,
				eServiceProviderType.Custom5
			};
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001D3B4 File Offset: 0x0001C3B4
		public static string GetServiceTypeDisplayName(eServiceProviderType type, string[] serviceTypeDescriptions, List<eServiceProviderType> eTypes)
		{
			int num = eTypes.IndexOf(type);
			string result;
			if (num > 0)
			{
				foreach (string text in serviceTypeDescriptions)
				{
					int num2 = text.IndexOf('=');
					int num3 = int.Parse(text.Substring(0, num2));
					if (num3 == num)
					{
						return text.Substring(num2 + 1);
					}
				}
				result = "";
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001D444 File Offset: 0x0001C444
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001D45C File Offset: 0x0001C45C
		public eServiceProviderType ServiceProviderType
		{
			get
			{
				return this.serviceProviderType;
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001D474 File Offset: 0x0001C474
		public static List<ServiceProvider> LoadServiceProviders(UnivDataAdapter da, string serviceTypeDescriptions)
		{
			string[] array = serviceTypeDescriptions.Split(new char[]
			{
				','
			});
			List<ServiceProvider> list = new List<ServiceProvider>();
			List<eServiceProviderType> etypes = ServiceProvider.GetETypes();
			Array values = Enum.GetValues(typeof(eServiceProviderType));
			foreach (object obj in values)
			{
				eServiceProviderType eServiceProviderType = (eServiceProviderType)obj;
				string serviceTypeDisplayName = ServiceProvider.GetServiceTypeDisplayName(eServiceProviderType, array, etypes);
				if (serviceTypeDisplayName.Length > 0 && serviceTypeDisplayName.CompareTo(".") != 0)
				{
					ServiceProvider serviceProvider = new ServiceProvider(eServiceProviderType, serviceTypeDescriptions);
					list.Add(serviceProvider);
					if (eServiceProviderType == eServiceProviderType.Peer_notetaker || eServiceProviderType == eServiceProviderType.Interpreter || eServiceProviderType == eServiceProviderType.Professional_notetaker)
					{
						serviceProvider.ServiceAppliesToIndividualCourses = true;
					}
				}
			}
			return list;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001D584 File Offset: 0x0001C584
		public ServiceProvider(DataRow dr)
		{
			if (dr != null)
			{
				this.ServiceProviderId = ((dr["serviceproviderid"] == DBNull.Value) ? 0 : ((int)dr["serviceproviderid"]));
				string str = dr.Table.Columns.Contains("providerfirstname") ? "provider" : "";
				this.FirstName = dr[str + "firstname"].ToString();
				this.LastName = dr[str + "lastname"].ToString();
				this.Student_no = dr[str + "student_no"].ToString();
				this.MiddleName = dr[str + "middlename"].ToString();
				this.Email = dr[str + "email"].ToString();
				this.AlternateEmail = dr[str + "alternateemail"].ToString();
				this.Phone = dr[str + "phone"].ToString();
				this.Cell = dr[str + "cell"].ToString();
				this.Specialization = dr[str + "specialization"].ToString();
				if (dr.Table.Columns.Contains("serviceprovidertype"))
				{
					int num = (dr["serviceprovidertype"] == DBNull.Value) ? 0 : ((int)dr["serviceprovidertype"]);
					if (num > 0)
					{
						this.serviceProviderType = (eServiceProviderType)num;
					}
					else
					{
						this.serviceProviderType = eServiceProviderType.Unknown;
					}
				}
				else
				{
					this.serviceProviderType = eServiceProviderType.Unknown;
				}
			}
		}

		// Token: 0x04000240 RID: 576
		private eServiceProviderType serviceProviderType;

		// Token: 0x04000241 RID: 577
		private eHowServiceProviderEnteredIntoClockWork howServiceProviderEnteredIntoClockWork;

		// Token: 0x04000242 RID: 578
		private eMatchingMethod matchingMethod;

		// Token: 0x04000243 RID: 579
		private ePaymentMethod paymentMethod;

		// Token: 0x04000244 RID: 580
		private bool serviceAppliesToIndividualCourses;

		// Token: 0x04000245 RID: 581
		private int screenNumProviderApplication;

		// Token: 0x04000246 RID: 582
		private int screenNumStudentForm;

		// Token: 0x04000247 RID: 583
		private string displayName;

		// Token: 0x04000248 RID: 584
		private string[] serviceTypeDescriptions;

		// Token: 0x04000249 RID: 585
		private List<eServiceProviderType> eTypes = null;
	}
}
