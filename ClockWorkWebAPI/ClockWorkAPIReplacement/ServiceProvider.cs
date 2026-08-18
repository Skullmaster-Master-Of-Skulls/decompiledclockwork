using System;
using System.Collections.Generic;
using System.Data;
using UnivOleDb;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x0200006B RID: 107
	public class ServiceProvider
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00022730 File Offset: 0x00020930
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x00022738 File Offset: 0x00020938
		public string Email { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00022741 File Offset: 0x00020941
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x00022749 File Offset: 0x00020949
		public string AlternateEmail { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00022752 File Offset: 0x00020952
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x0002275A File Offset: 0x0002095A
		public string Phone { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00022763 File Offset: 0x00020963
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0002276B File Offset: 0x0002096B
		public string Cell { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00022774 File Offset: 0x00020974
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x0002277C File Offset: 0x0002097C
		public string Specialization { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00022785 File Offset: 0x00020985
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x0002278D File Offset: 0x0002098D
		public string FirstName { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00022796 File Offset: 0x00020996
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x0002279E File Offset: 0x0002099E
		public string LastName { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000227A7 File Offset: 0x000209A7
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x000227AF File Offset: 0x000209AF
		public string Student_no { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000227B8 File Offset: 0x000209B8
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x000227C0 File Offset: 0x000209C0
		public string MiddleName { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x000227C9 File Offset: 0x000209C9
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x000227D1 File Offset: 0x000209D1
		public int ServiceProviderId { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000227DA File Offset: 0x000209DA
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x000227E2 File Offset: 0x000209E2
		public string AltId { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x000227EB File Offset: 0x000209EB
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x000227F3 File Offset: 0x000209F3
		public string Address { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x000227FC File Offset: 0x000209FC
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00022804 File Offset: 0x00020A04
		public string PermanentAddress { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0002280D File Offset: 0x00020A0D
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00022815 File Offset: 0x00020A15
		public string Phone2 { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00022820 File Offset: 0x00020A20
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00022838 File Offset: 0x00020A38
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

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00022844 File Offset: 0x00020A44
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0002285C File Offset: 0x00020A5C
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

		// Token: 0x06000531 RID: 1329 RVA: 0x00022866 File Offset: 0x00020A66
		public ServiceProvider()
		{
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00022877 File Offset: 0x00020A77
		public ServiceProvider(eServiceProviderType serviceProviderType, string serviceTypeDescriptions)
		{
			this.serviceTypeDescriptions = serviceTypeDescriptions.Split(new char[]
			{
				','
			});
			this.serviceProviderType = serviceProviderType;
			this.displayName = this.GetServiceTypeDisplayName(serviceProviderType);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000228B4 File Offset: 0x00020AB4
		public string GetServiceTypeDisplayName(eServiceProviderType type)
		{
			bool flag = this.eTypes == null;
			if (flag)
			{
				this.eTypes = ServiceProvider.GetETypes();
			}
			return ServiceProvider.GetServiceTypeDisplayName(type, this.serviceTypeDescriptions, this.eTypes);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000228F4 File Offset: 0x00020AF4
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

		// Token: 0x06000535 RID: 1333 RVA: 0x000229A4 File Offset: 0x00020BA4
		public static string GetServiceTypeDisplayName(eServiceProviderType type, string[] serviceTypeDescriptions, List<eServiceProviderType> eTypes)
		{
			int num = eTypes.IndexOf(type);
			bool flag = num > 0;
			string result;
			if (flag)
			{
				foreach (string text in serviceTypeDescriptions)
				{
					int num2 = text.IndexOf('=');
					int num3 = int.Parse(text.Substring(0, num2));
					bool flag2 = num3 == num;
					if (flag2)
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

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00022A28 File Offset: 0x00020C28
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00022A40 File Offset: 0x00020C40
		public eServiceProviderType ServiceProviderType
		{
			get
			{
				return this.serviceProviderType;
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00022A58 File Offset: 0x00020C58
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
				bool flag = serviceTypeDisplayName.Length > 0 && serviceTypeDisplayName.CompareTo(".") != 0;
				if (flag)
				{
					ServiceProvider serviceProvider = new ServiceProvider(eServiceProviderType, serviceTypeDescriptions);
					list.Add(serviceProvider);
					bool flag2 = eServiceProviderType == eServiceProviderType.Peer_notetaker || eServiceProviderType == eServiceProviderType.Interpreter || eServiceProviderType == eServiceProviderType.Professional_notetaker;
					if (flag2)
					{
						serviceProvider.ServiceAppliesToIndividualCourses = true;
					}
				}
			}
			return list;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00022B54 File Offset: 0x00020D54
		public ServiceProvider(DataRow dr)
		{
			bool flag = dr != null;
			if (flag)
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
				bool flag2 = dr.Table.Columns.Contains("serviceprovidertype");
				if (flag2)
				{
					int num = (dr["serviceprovidertype"] == DBNull.Value) ? 0 : ((int)dr["serviceprovidertype"]);
					bool flag3 = num > 0;
					if (flag3)
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

		// Token: 0x040002BA RID: 698
		private eServiceProviderType serviceProviderType;

		// Token: 0x040002BB RID: 699
		private eHowServiceProviderEnteredIntoClockWork howServiceProviderEnteredIntoClockWork;

		// Token: 0x040002BC RID: 700
		private eMatchingMethod matchingMethod;

		// Token: 0x040002BD RID: 701
		private ePaymentMethod paymentMethod;

		// Token: 0x040002BE RID: 702
		private bool serviceAppliesToIndividualCourses;

		// Token: 0x040002BF RID: 703
		private int screenNumProviderApplication;

		// Token: 0x040002C0 RID: 704
		private int screenNumStudentForm;

		// Token: 0x040002C1 RID: 705
		private string displayName;

		// Token: 0x040002C2 RID: 706
		private string[] serviceTypeDescriptions;

		// Token: 0x040002D1 RID: 721
		private List<eServiceProviderType> eTypes = null;
	}
}
