using System;

namespace System.IdentityModel.Claims
{
	// Token: 0x020001DB RID: 475
	public static class ClaimTypes
	{
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00044A80 File Offset: 0x00042C80
		public static string Anonymous
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous";
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00044A87 File Offset: 0x00042C87
		public static string DenyOnlySid
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid";
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00044A8E File Offset: 0x00042C8E
		public static string Dns
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns";
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00044A95 File Offset: 0x00042C95
		public static string Email
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00044A9C File Offset: 0x00042C9C
		public static string Hash
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash";
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00044AA3 File Offset: 0x00042CA3
		public static string Name
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00044AAA File Offset: 0x00042CAA
		public static string Rsa
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00044AB1 File Offset: 0x00042CB1
		public static string Sid
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid";
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00044AB8 File Offset: 0x00042CB8
		public static string Spn
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn";
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x00044ABF File Offset: 0x00042CBF
		public static string System
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system";
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00044AC6 File Offset: 0x00042CC6
		public static string Thumbprint
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint";
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00044ACD File Offset: 0x00042CCD
		public static string Upn
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00044AD4 File Offset: 0x00042CD4
		public static string Uri
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri";
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x00044ADB File Offset: 0x00042CDB
		public static string X500DistinguishedName
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname";
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00044AE2 File Offset: 0x00042CE2
		public static string NameIdentifier
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00044AE9 File Offset: 0x00042CE9
		public static string Authentication
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication";
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00044AF0 File Offset: 0x00042CF0
		public static string AuthorizationDecision
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision";
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00044AF7 File Offset: 0x00042CF7
		public static string GivenName
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00044AFE File Offset: 0x00042CFE
		public static string Surname
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00044B05 File Offset: 0x00042D05
		public static string StreetAddress
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress";
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00044B0C File Offset: 0x00042D0C
		public static string Locality
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality";
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x00044B13 File Offset: 0x00042D13
		public static string StateOrProvince
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince";
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x00044B1A File Offset: 0x00042D1A
		public static string PostalCode
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode";
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00044B21 File Offset: 0x00042D21
		public static string Country
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country";
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00044B28 File Offset: 0x00042D28
		public static string HomePhone
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone";
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00044B2F File Offset: 0x00042D2F
		public static string OtherPhone
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone";
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00044B36 File Offset: 0x00042D36
		public static string MobilePhone
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone";
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00044B3D File Offset: 0x00042D3D
		public static string DateOfBirth
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth";
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00044B44 File Offset: 0x00042D44
		public static string Gender
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender";
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00044B4B File Offset: 0x00042D4B
		public static string PPID
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/privatepersonalidentifier";
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00044B52 File Offset: 0x00042D52
		public static string Webpage
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage";
			}
		}

		// Token: 0x04000DA5 RID: 3493
		private const string claimTypeNamespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";

		// Token: 0x04000DA6 RID: 3494
		private const string anonymous = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous";

		// Token: 0x04000DA7 RID: 3495
		private const string dns = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns";

		// Token: 0x04000DA8 RID: 3496
		private const string email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

		// Token: 0x04000DA9 RID: 3497
		private const string hash = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash";

		// Token: 0x04000DAA RID: 3498
		private const string name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

		// Token: 0x04000DAB RID: 3499
		private const string rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";

		// Token: 0x04000DAC RID: 3500
		private const string sid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid";

		// Token: 0x04000DAD RID: 3501
		private const string denyOnlySid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid";

		// Token: 0x04000DAE RID: 3502
		private const string spn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn";

		// Token: 0x04000DAF RID: 3503
		private const string system = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system";

		// Token: 0x04000DB0 RID: 3504
		private const string thumbprint = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint";

		// Token: 0x04000DB1 RID: 3505
		private const string upn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";

		// Token: 0x04000DB2 RID: 3506
		private const string uri = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri";

		// Token: 0x04000DB3 RID: 3507
		private const string x500DistinguishedName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname";

		// Token: 0x04000DB4 RID: 3508
		private const string givenname = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";

		// Token: 0x04000DB5 RID: 3509
		private const string surname = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";

		// Token: 0x04000DB6 RID: 3510
		private const string streetaddress = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress";

		// Token: 0x04000DB7 RID: 3511
		private const string locality = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality";

		// Token: 0x04000DB8 RID: 3512
		private const string stateorprovince = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince";

		// Token: 0x04000DB9 RID: 3513
		private const string postalcode = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode";

		// Token: 0x04000DBA RID: 3514
		private const string country = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country";

		// Token: 0x04000DBB RID: 3515
		private const string homephone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone";

		// Token: 0x04000DBC RID: 3516
		private const string otherphone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone";

		// Token: 0x04000DBD RID: 3517
		private const string mobilephone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone";

		// Token: 0x04000DBE RID: 3518
		private const string dateofbirth = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth";

		// Token: 0x04000DBF RID: 3519
		private const string gender = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender";

		// Token: 0x04000DC0 RID: 3520
		private const string ppid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/privatepersonalidentifier";

		// Token: 0x04000DC1 RID: 3521
		private const string webpage = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage";

		// Token: 0x04000DC2 RID: 3522
		private const string nameidentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

		// Token: 0x04000DC3 RID: 3523
		private const string authentication = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication";

		// Token: 0x04000DC4 RID: 3524
		private const string authorizationdecision = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision";
	}
}
