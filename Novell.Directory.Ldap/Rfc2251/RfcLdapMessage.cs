using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000D7 RID: 215
	public class RfcLdapMessage : Asn1Sequence
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0001A14C File Offset: 0x0001914C
		public virtual int MessageID
		{
			get
			{
				return ((Asn1Integer)base.get_Renamed(0)).intValue();
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0001A170 File Offset: 0x00019170
		public virtual int Type
		{
			get
			{
				return base.get_Renamed(1).getIdentifier().Tag;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001A194 File Offset: 0x00019194
		public virtual Asn1Object Response
		{
			get
			{
				return base.get_Renamed(1);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001A1AC File Offset: 0x000191AC
		public virtual RfcControls Controls
		{
			get
			{
				RfcControls result;
				if (base.size() > 2)
				{
					result = (RfcControls)base.get_Renamed(2);
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001A1D8 File Offset: 0x000191D8
		public virtual string RequestDN
		{
			get
			{
				return ((RfcRequest)this.op).getRequestDN();
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001A1FC File Offset: 0x000191FC
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x0001A214 File Offset: 0x00019214
		public virtual LdapMessage RequestingMessage
		{
			get
			{
				return this.requestMessage;
			}
			set
			{
				this.requestMessage = value;
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001A22C File Offset: 0x0001922C
		internal RfcLdapMessage(Asn1Object[] origContent, RfcRequest origRequest, string dn, string filter, bool reference) : base(origContent, origContent.Length)
		{
			base.set_Renamed(0, new RfcMessageID());
			RfcRequest rfcRequest = (RfcRequest)origContent[1];
			RfcRequest rfcRequest2 = rfcRequest.dupRequest(dn, filter, reference);
			this.op = (Asn1Object)rfcRequest2;
			base.set_Renamed(1, (Asn1Object)rfcRequest2);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001A288 File Offset: 0x00019288
		public RfcLdapMessage(RfcRequest op) : this(op, null)
		{
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001A2A0 File Offset: 0x000192A0
		public RfcLdapMessage(RfcRequest op, RfcControls controls) : base(3)
		{
			this.op = (Asn1Object)op;
			this.controls = controls;
			base.add(new RfcMessageID());
			base.add((Asn1Object)op);
			if (controls != null)
			{
				base.add(controls);
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001A2F4 File Offset: 0x000192F4
		public RfcLdapMessage(Asn1Sequence op) : this(op, null)
		{
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001A30C File Offset: 0x0001930C
		public RfcLdapMessage(Asn1Sequence op, RfcControls controls) : base(3)
		{
			this.op = op;
			this.controls = controls;
			base.add(new RfcMessageID());
			base.add(op);
			if (controls != null)
			{
				base.add(controls);
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001A354 File Offset: 0x00019354
		[CLSCompliant(false)]
		public RfcLdapMessage(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
			Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(1);
			Asn1Identifier identifier = asn1Tagged.getIdentifier();
			sbyte[] array = ((Asn1OctetString)asn1Tagged.taggedValue()).byteValue();
			MemoryStream in_Renamed2 = new MemoryStream(SupportClass.ToByteArray(array));
			int tag = identifier.Tag;
			switch (tag)
			{
			case 1:
				base.set_Renamed(1, new RfcBindResponse(dec, in_Renamed2, array.Length));
				goto IL_1A6;
			case 2:
			case 3:
			case 6:
			case 8:
			case 10:
			case 12:
			case 14:
				break;
			case 4:
				base.set_Renamed(1, new RfcSearchResultEntry(dec, in_Renamed2, array.Length));
				goto IL_1A6;
			case 5:
				base.set_Renamed(1, new RfcSearchResultDone(dec, in_Renamed2, array.Length));
				goto IL_1A6;
			case 7:
				base.set_Renamed(1, new RfcModifyResponse(dec, in_Renamed2, array.Length));
				goto IL_1A6;
			case 9:
				base.set_Renamed(1, new RfcAddResponse(dec, in_Renamed2, array.Length));
				goto IL_1A6;
			case 11:
				base.set_Renamed(1, new RfcDelResponse(dec, in_Renamed2, array.Length));
				goto IL_1A6;
			case 13:
				base.set_Renamed(1, new RfcModifyDNResponse(dec, in_Renamed2, array.Length));
				goto IL_1A6;
			case 15:
				base.set_Renamed(1, new RfcCompareResponse(dec, in_Renamed2, array.Length));
				goto IL_1A6;
			default:
				if (tag == 19)
				{
					base.set_Renamed(1, new RfcSearchResultReference(dec, in_Renamed2, array.Length));
					goto IL_1A6;
				}
				switch (tag)
				{
				case 24:
					base.set_Renamed(1, new RfcExtendedResponse(dec, in_Renamed2, array.Length));
					goto IL_1A6;
				case 25:
					base.set_Renamed(1, new RfcIntermediateResponse(dec, in_Renamed2, array.Length));
					goto IL_1A6;
				}
				break;
			}
			throw new SystemException("RfcLdapMessage: Invalid tag: " + identifier.Tag);
			IL_1A6:
			if (base.size() > 2)
			{
				Asn1Tagged asn1Tagged2 = (Asn1Tagged)base.get_Renamed(2);
				array = ((Asn1OctetString)asn1Tagged2.taggedValue()).byteValue();
				in_Renamed2 = new MemoryStream(SupportClass.ToByteArray(array));
				base.set_Renamed(2, new RfcControls(dec, in_Renamed2, array.Length));
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001A550 File Offset: 0x00019550
		public RfcRequest getRequest()
		{
			return (RfcRequest)base.get_Renamed(1);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001A570 File Offset: 0x00019570
		public virtual bool isRequest()
		{
			return base.get_Renamed(1) is RfcRequest;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001A590 File Offset: 0x00019590
		public object dupMessage(string dn, string filter, bool reference)
		{
			if (this.op == null)
			{
				throw new LdapException("DUP_ERROR", 82, null);
			}
			return new RfcLdapMessage(base.toArray(), (RfcRequest)base.get_Renamed(1), dn, filter, reference);
		}

		// Token: 0x04000423 RID: 1059
		private Asn1Object op;

		// Token: 0x04000424 RID: 1060
		private RfcControls controls;

		// Token: 0x04000425 RID: 1061
		private LdapMessage requestMessage = null;
	}
}
