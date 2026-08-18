using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000EC RID: 236
	public class ContactPerson
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x0001A307 File Offset: 0x00018507
		public ContactPerson()
		{
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001A325 File Offset: 0x00018525
		public ContactPerson(ContactType contactType)
		{
			this._type = contactType;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0001A34A File Offset: 0x0001854A
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x0001A352 File Offset: 0x00018552
		public string Company
		{
			get
			{
				return this._company;
			}
			set
			{
				this._company = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001A35B File Offset: 0x0001855B
		public ICollection<string> EmailAddresses
		{
			get
			{
				return this._emailAddresses;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0001A363 File Offset: 0x00018563
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x0001A36B File Offset: 0x0001856B
		public string GivenName
		{
			get
			{
				return this._givenName;
			}
			set
			{
				this._givenName = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001A374 File Offset: 0x00018574
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x0001A37C File Offset: 0x0001857C
		public string Surname
		{
			get
			{
				return this._surname;
			}
			set
			{
				this._surname = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001A385 File Offset: 0x00018585
		public ICollection<string> TelephoneNumbers
		{
			get
			{
				return this._telephoneNumbers;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001A38D File Offset: 0x0001858D
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0001A395 File Offset: 0x00018595
		public ContactType Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x04000A4C RID: 2636
		private ContactType _type;

		// Token: 0x04000A4D RID: 2637
		private string _company;

		// Token: 0x04000A4E RID: 2638
		private string _givenName;

		// Token: 0x04000A4F RID: 2639
		private string _surname;

		// Token: 0x04000A50 RID: 2640
		private Collection<string> _emailAddresses = new Collection<string>();

		// Token: 0x04000A51 RID: 2641
		private Collection<string> _telephoneNumbers = new Collection<string>();
	}
}
