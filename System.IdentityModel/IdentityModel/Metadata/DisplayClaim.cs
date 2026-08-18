using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000EE RID: 238
	public class DisplayClaim
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x0001A3A0 File Offset: 0x000185A0
		private static Dictionary<string, string> PopulateClaimTagMap()
		{
			return new Dictionary<string, string>
			{
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country",
					SR.GetString("CountryText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth",
					SR.GetString("DateOfBirthText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
					SR.GetString("EmailAddressText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender",
					SR.GetString("GenderText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname",
					SR.GetString("GivenNameText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone",
					SR.GetString("HomePhoneText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality",
					SR.GetString("LocalityText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone",
					SR.GetString("MobilePhoneText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
					SR.GetString("NameText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone",
					SR.GetString("OtherPhoneText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode",
					SR.GetString("PostalCodeText")
				},
				{
					ClaimTypes.PPID,
					SR.GetString("PPIDText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince",
					SR.GetString("StateOrProvinceText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress",
					SR.GetString("StreetAddressText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname",
					SR.GetString("SurnameText")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage",
					SR.GetString("WebPageText")
				},
				{
					"http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
					SR.GetString("RoleText")
				}
			};
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001A51C File Offset: 0x0001871C
		private static Dictionary<string, string> PopulateClaimDescriptionMap()
		{
			return new Dictionary<string, string>
			{
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country",
					SR.GetString("CountryDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth",
					SR.GetString("DateOfBirthDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
					SR.GetString("EmailAddressDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender",
					SR.GetString("GenderDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname",
					SR.GetString("GivenNameDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone",
					SR.GetString("HomePhoneDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality",
					SR.GetString("LocalityDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone",
					SR.GetString("MobilePhoneDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
					SR.GetString("NameDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone",
					SR.GetString("OtherPhoneDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode",
					SR.GetString("PostalCodeDescription")
				},
				{
					ClaimTypes.PPID,
					SR.GetString("PPIDDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince",
					SR.GetString("StateOrProvinceDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress",
					SR.GetString("StreetAddressDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname",
					SR.GetString("SurnameDescription")
				},
				{
					"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage",
					SR.GetString("WebPageDescription")
				},
				{
					"http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
					SR.GetString("RoleDescription")
				}
			};
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001A698 File Offset: 0x00018898
		private static string ClaimTagForClaimType(string claimType)
		{
			string result = null;
			DisplayClaim.claimTagMap.TryGetValue(claimType, out result);
			return result;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001A6B8 File Offset: 0x000188B8
		private static string ClaimDescriptionForClaimType(string claimType)
		{
			string result = null;
			DisplayClaim.claimDescriptionMap.TryGetValue(claimType, out result);
			return result;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001A6D8 File Offset: 0x000188D8
		public static DisplayClaim CreateDisplayClaimFromClaimType(string claimType)
		{
			return new DisplayClaim(claimType)
			{
				DisplayTag = DisplayClaim.ClaimTagForClaimType(claimType),
				Description = DisplayClaim.ClaimDescriptionForClaimType(claimType)
			};
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001A705 File Offset: 0x00018905
		public DisplayClaim(string claimType) : this(claimType, null, null, null)
		{
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001A711 File Offset: 0x00018911
		public DisplayClaim(string claimType, string displayTag, string description) : this(claimType, displayTag, description, null)
		{
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001A71D File Offset: 0x0001891D
		public DisplayClaim(string claimType, string displayTag, string description, string displayValue) : this(claimType, displayTag, description, displayValue, true)
		{
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001A72C File Offset: 0x0001892C
		public DisplayClaim(string claimType, string displayTag, string description, string displayValue, bool optional)
		{
			if (string.IsNullOrEmpty(claimType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claimType");
			}
			this.claimType = claimType;
			this.displayTag = displayTag;
			this.description = description;
			this.displayValue = displayValue;
			this.optional = optional;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001A77C File Offset: 0x0001897C
		public string ClaimType
		{
			get
			{
				return this.claimType;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001A784 File Offset: 0x00018984
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x0001A78C File Offset: 0x0001898C
		public string DisplayTag
		{
			get
			{
				return this.displayTag;
			}
			set
			{
				this.displayTag = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0001A795 File Offset: 0x00018995
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x0001A79D File Offset: 0x0001899D
		public string DisplayValue
		{
			get
			{
				return this.displayValue;
			}
			set
			{
				this.displayValue = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001A7A6 File Offset: 0x000189A6
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0001A7AE File Offset: 0x000189AE
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0001A7B7 File Offset: 0x000189B7
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x0001A7BF File Offset: 0x000189BF
		public bool Optional
		{
			get
			{
				return this.optional;
			}
			set
			{
				this.optional = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001A7C8 File Offset: 0x000189C8
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x0001A7D0 File Offset: 0x000189D0
		public bool WriteOptionalAttribute { get; set; }

		// Token: 0x04000A59 RID: 2649
		private static Dictionary<string, string> claimDescriptionMap = DisplayClaim.PopulateClaimDescriptionMap();

		// Token: 0x04000A5A RID: 2650
		private static Dictionary<string, string> claimTagMap = DisplayClaim.PopulateClaimTagMap();

		// Token: 0x04000A5B RID: 2651
		private string claimType;

		// Token: 0x04000A5C RID: 2652
		private string displayTag;

		// Token: 0x04000A5D RID: 2653
		private string displayValue;

		// Token: 0x04000A5E RID: 2654
		private string description;

		// Token: 0x04000A5F RID: 2655
		private bool optional;
	}
}
