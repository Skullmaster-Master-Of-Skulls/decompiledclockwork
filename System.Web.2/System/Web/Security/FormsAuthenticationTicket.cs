using System;
using System.Runtime.Serialization;

namespace System.Web.Security
{
	// Token: 0x020005E1 RID: 1505
	[Serializable]
	public sealed class FormsAuthenticationTicket
	{
		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x06004C08 RID: 19464 RVA: 0x00103CC4 File Offset: 0x00101EC4
		public int Version
		{
			get
			{
				return this._Version;
			}
		}

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x06004C09 RID: 19465 RVA: 0x00103CCC File Offset: 0x00101ECC
		public string Name
		{
			get
			{
				return this._Name;
			}
		}

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06004C0A RID: 19466 RVA: 0x00103CD4 File Offset: 0x00101ED4
		public DateTime Expiration
		{
			get
			{
				return this._Expiration;
			}
		}

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x06004C0B RID: 19467 RVA: 0x00103CDC File Offset: 0x00101EDC
		public DateTime IssueDate
		{
			get
			{
				return this._IssueDate;
			}
		}

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06004C0C RID: 19468 RVA: 0x00103CE4 File Offset: 0x00101EE4
		public bool IsPersistent
		{
			get
			{
				return this._IsPersistent;
			}
		}

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06004C0D RID: 19469 RVA: 0x00103CEC File Offset: 0x00101EEC
		public bool Expired
		{
			get
			{
				return this.ExpirationUtc < DateTime.UtcNow;
			}
		}

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06004C0E RID: 19470 RVA: 0x00103CFE File Offset: 0x00101EFE
		public string UserData
		{
			get
			{
				return this._UserData;
			}
		}

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x06004C0F RID: 19471 RVA: 0x00103D06 File Offset: 0x00101F06
		public string CookiePath
		{
			get
			{
				return this._CookiePath;
			}
		}

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x06004C10 RID: 19472 RVA: 0x00103D10 File Offset: 0x00101F10
		internal DateTime ExpirationUtc
		{
			get
			{
				if (!this._ExpirationUtcHasValue)
				{
					return this.Expiration.ToUniversalTime();
				}
				return this._ExpirationUtc;
			}
		}

		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x06004C11 RID: 19473 RVA: 0x00103D3C File Offset: 0x00101F3C
		internal DateTime IssueDateUtc
		{
			get
			{
				if (!this._IssueDateUtcHasValue)
				{
					return this.IssueDate.ToUniversalTime();
				}
				return this._IssueDateUtc;
			}
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x00103D66 File Offset: 0x00101F66
		public FormsAuthenticationTicket(int version, string name, DateTime issueDate, DateTime expiration, bool isPersistent, string userData)
		{
			this._Version = version;
			this._Name = name;
			this._Expiration = expiration;
			this._IssueDate = issueDate;
			this._IsPersistent = isPersistent;
			this._UserData = userData;
			this._CookiePath = FormsAuthentication.FormsCookiePath;
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x00103DA6 File Offset: 0x00101FA6
		public FormsAuthenticationTicket(int version, string name, DateTime issueDate, DateTime expiration, bool isPersistent, string userData, string cookiePath)
		{
			this._Version = version;
			this._Name = name;
			this._Expiration = expiration;
			this._IssueDate = issueDate;
			this._IsPersistent = isPersistent;
			this._UserData = userData;
			this._CookiePath = cookiePath;
		}

		// Token: 0x06004C14 RID: 19476 RVA: 0x00103DE4 File Offset: 0x00101FE4
		public FormsAuthenticationTicket(string name, bool isPersistent, int timeout)
		{
			this._Version = 2;
			this._Name = name;
			this._IssueDateUtcHasValue = true;
			this._IssueDateUtc = DateTime.UtcNow;
			this._IssueDate = DateTime.Now;
			this._IsPersistent = isPersistent;
			this._UserData = "";
			this._ExpirationUtcHasValue = true;
			this._ExpirationUtc = this._IssueDateUtc.AddMinutes((double)timeout);
			this._Expiration = this._IssueDate.AddMinutes((double)timeout);
			this._CookiePath = FormsAuthentication.FormsCookiePath;
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x00103E6C File Offset: 0x0010206C
		internal static FormsAuthenticationTicket FromUtc(int version, string name, DateTime issueDateUtc, DateTime expirationUtc, bool isPersistent, string userData, string cookiePath)
		{
			return new FormsAuthenticationTicket(version, name, issueDateUtc.ToLocalTime(), expirationUtc.ToLocalTime(), isPersistent, userData, cookiePath)
			{
				_IssueDateUtcHasValue = true,
				_IssueDateUtc = issueDateUtc,
				_ExpirationUtcHasValue = true,
				_ExpirationUtc = expirationUtc
			};
		}

		// Token: 0x040028E8 RID: 10472
		private int _Version;

		// Token: 0x040028E9 RID: 10473
		private string _Name;

		// Token: 0x040028EA RID: 10474
		private DateTime _Expiration;

		// Token: 0x040028EB RID: 10475
		private DateTime _IssueDate;

		// Token: 0x040028EC RID: 10476
		private bool _IsPersistent;

		// Token: 0x040028ED RID: 10477
		private string _UserData;

		// Token: 0x040028EE RID: 10478
		private string _CookiePath;

		// Token: 0x040028EF RID: 10479
		[OptionalField(VersionAdded = 2)]
		private int _InternalVersion;

		// Token: 0x040028F0 RID: 10480
		[OptionalField(VersionAdded = 2)]
		private byte[] _InternalData;

		// Token: 0x040028F1 RID: 10481
		[NonSerialized]
		private bool _ExpirationUtcHasValue;

		// Token: 0x040028F2 RID: 10482
		[NonSerialized]
		private DateTime _ExpirationUtc;

		// Token: 0x040028F3 RID: 10483
		[NonSerialized]
		private bool _IssueDateUtcHasValue;

		// Token: 0x040028F4 RID: 10484
		[NonSerialized]
		private DateTime _IssueDateUtc;
	}
}
