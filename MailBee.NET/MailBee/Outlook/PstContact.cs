using System;
using System.Collections;
using a.b;
using MailBee.Mime;

namespace MailBee.Outlook
{
	// Token: 0x020005AF RID: 1455
	public class PstContact : PstItem
	{
		// Token: 0x060030F0 RID: 12528 RVA: 0x000E5874 File Offset: 0x000E4874
		internal PstContact(fo A_0) : base(A_0)
		{
			this.c = "X-Contact-";
			this.b["Account"] = A_0.t();
			this.b["CallbackTelephoneNumber"] = A_0.a8();
			this.b["Generation"] = A_0.aj();
			this.b["GivenName"] = A_0.bw();
			this.b["GovernmentIdNumber"] = A_0.az();
			this.b["BusinessTelephoneNumber"] = A_0.s();
			this.b["HomeTelephoneNumber"] = A_0.bm();
			this.b["Initials"] = A_0.k();
			this.b["Keyword"] = A_0.a6();
			this.b["Language"] = A_0.ap();
			this.b["Location"] = A_0.a();
			this.b["MhsCommonName"] = A_0.o();
			this.b["OrganizationalIdNumber"] = A_0.ai();
			this.b["Surname"] = A_0.l();
			this.b["OriginalDisplayName"] = A_0.b1();
			this.b["PostalAddress"] = A_0.v();
			this.b["CompanyName"] = A_0.a4();
			this.b["Title"] = A_0.b3();
			this.b["DepartmentName"] = A_0.ax();
			this.b["OfficeLocation"] = A_0.af();
			this.b["PrimaryTelephoneNumber"] = A_0.bk();
			this.b["Business2TelephoneNumber"] = A_0.av();
			this.b["MobileTelephoneNumber"] = A_0.cb();
			this.b["RadioTelephoneNumber"] = A_0.ci();
			this.b["CarTelephoneNumber"] = A_0.by();
			this.b["OtherTelephoneNumber"] = A_0.a2();
			this.b["TransmittableDisplayName"] = A_0.y();
			this.b["PagerTelephoneNumber"] = A_0.g();
			this.b["PrimaryFaxNumber"] = A_0.w();
			this.b["BusinessFaxNumber"] = A_0.i();
			this.b["HomeFaxNumber"] = A_0.u();
			this.b["BusinessAddressCountry"] = A_0.bt();
			this.b["BusinessAddressCity"] = A_0.ag();
			this.b["BusinessAddressStateOrProvince"] = A_0.b5();
			this.b["BusinessAddressStreet"] = A_0.al();
			this.b["BusinessPostalCode"] = A_0.ar();
			this.b["BusinessPoBox"] = A_0.an();
			this.b["TelexNumber"] = A_0.cc();
			this.b["IsdnNumber"] = A_0.m();
			this.b["AssistantTelephoneNumber"] = A_0.r();
			this.b["Home2TelephoneNumber"] = A_0.at();
			this.b["Assistant"] = A_0.b();
			this.b["Hobbies"] = A_0.bo();
			this.b["MiddleName"] = A_0.a5();
			this.b["DisplayNamePrefix"] = A_0.bx();
			this.b["Profession"] = A_0.au();
			this.b["PreferredByName"] = A_0.n();
			this.b["SpouseName"] = A_0.bf();
			this.b["ComputerNetworkName"] = A_0.@as();
			this.b["CustomerId"] = A_0.cl();
			this.b["TtytddPhoneNumber"] = A_0.bb();
			this.b["FtpSite"] = A_0.b2();
			this.b["ManagerName"] = A_0.ah();
			this.b["Nickname"] = A_0.aw();
			this.b["PersonalHomePage"] = A_0.be();
			this.b["BusinessHomePage"] = A_0.ae();
			this.b["CompanyMainPhoneNumber"] = A_0.bg();
			this.b["ChildrensNames"] = A_0.ab();
			this.b["HomeAddressCity"] = A_0.ak();
			this.b["HomeAddressCountry"] = A_0.bd();
			this.b["HomeAddressPostalCode"] = A_0.aq();
			this.b["HomeAddressStateOrProvince"] = A_0.a7();
			this.b["HomeAddressStreet"] = A_0.aa();
			this.b["HomeAddressPostOfficeBox"] = A_0.bl();
			this.b["OtherAddressCity"] = A_0.a1();
			this.b["OtherAddressCountry"] = A_0.bp();
			this.b["OtherAddressPostalCode"] = A_0.bi();
			this.b["OtherAddressStateOrProvince"] = A_0.b8();
			this.b["OtherAddressStreet"] = A_0.cj();
			this.b["OtherAddressPostOfficeBox"] = A_0.b0();
			this.b["FileUnder"] = A_0.h();
			this.b["HomeAddress"] = A_0.e();
			this.b["WorkAddress"] = A_0.bz();
			this.b["OtherAddress"] = A_0.ck();
			this.b["PostalAddressId"] = A_0.a0();
			this.b["Html"] = A_0.ao();
			this.b["WorkAddressStreet"] = A_0.b4();
			this.b["WorkAddressState"] = A_0.p();
			this.b["WorkAddressPostalCode"] = A_0.bs();
			this.b["WorkAddressCountry"] = A_0.ch();
			this.b["InstantMessagingAddress"] = A_0.f();
			this.b["Email1DisplayName"] = A_0.ba();
			this.b["Email1AddressType"] = A_0.ac();
			this.b["Email1EmailAddress"] = A_0.ay();
			this.b["Email1OriginalDisplayName"] = A_0.d();
			this.b["Email1EmailType"] = A_0.b7();
			this.b["Email2DisplayName"] = A_0.cd();
			this.b["Email2AddressType"] = A_0.x();
			this.b["Email2EmailAddress"] = A_0.bn();
			this.b["Email2OriginalDisplayName"] = A_0.bq();
			this.b["Email3DisplayName"] = A_0.c();
			this.b["Email3AddressType"] = A_0.cf();
			this.b["Email3EmailAddress"] = A_0.b9();
			this.b["Email3OriginalDisplayName"] = A_0.am();
			this.b["Fax1AddressType"] = A_0.bj();
			this.b["Fax1EmailAddress"] = A_0.q();
			this.b["Fax1OriginalDisplayName"] = A_0.br();
			this.b["Fax2AddressType"] = A_0.ad();
			this.b["Fax2EmailAddress"] = A_0.bu();
			this.b["Fax2OriginalDisplayName"] = A_0.z();
			this.b["Fax3AddressType"] = A_0.ca();
			this.b["Fax3EmailAddress"] = A_0.j();
			this.b["Fax3OriginalDisplayName"] = A_0.a3();
			this.b["Note"] = A_0.bh();
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x000E6188 File Offset: 0x000E5188
		public override PstItemType PstType
		{
			get
			{
				return base.PstType;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x000E6190 File Offset: 0x000E5190
		public override Hashtable PstFields
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000E6198 File Offset: 0x000E5198
		public override MailMessage GetAsMailMessage()
		{
			MailMessage a_ = new MailMessage();
			return base.a(a_);
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060030F4 RID: 12532 RVA: 0x000E61B2 File Offset: 0x000E51B2
		public override int PstID
		{
			get
			{
				return base.PstID;
			}
		}
	}
}
