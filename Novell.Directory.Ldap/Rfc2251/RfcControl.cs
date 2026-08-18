using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000CC RID: 204
	public class RfcControl : Asn1Sequence
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00018014 File Offset: 0x00017014
		public virtual Asn1OctetString ControlType
		{
			get
			{
				return (Asn1OctetString)base.get_Renamed(0);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00018034 File Offset: 0x00017034
		public virtual Asn1Boolean Criticality
		{
			get
			{
				if (base.size() > 1)
				{
					Asn1Object asn1Object = base.get_Renamed(1);
					if (asn1Object is Asn1Boolean)
					{
						return (Asn1Boolean)asn1Object;
					}
				}
				return new Asn1Boolean(false);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00018070 File Offset: 0x00017070
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x000180BC File Offset: 0x000170BC
		public virtual Asn1OctetString ControlValue
		{
			get
			{
				Asn1OctetString result;
				if (base.size() > 2)
				{
					result = (Asn1OctetString)base.get_Renamed(2);
				}
				else
				{
					if (base.size() > 1)
					{
						Asn1Object asn1Object = base.get_Renamed(1);
						if (asn1Object is Asn1OctetString)
						{
							return (Asn1OctetString)asn1Object;
						}
					}
					result = null;
				}
				return result;
			}
			set
			{
				if (value != null)
				{
					if (base.size() == 3)
					{
						base.set_Renamed(2, value);
					}
					else if (base.size() == 2)
					{
						Asn1Object asn1Object = base.get_Renamed(1);
						if (asn1Object is Asn1OctetString)
						{
							base.set_Renamed(1, value);
						}
						else
						{
							base.add(value);
						}
					}
				}
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00018110 File Offset: 0x00017110
		public RfcControl(RfcLdapOID controlType) : this(controlType, new Asn1Boolean(false), null)
		{
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001812C File Offset: 0x0001712C
		public RfcControl(RfcLdapOID controlType, Asn1Boolean criticality) : this(controlType, criticality, null)
		{
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00018144 File Offset: 0x00017144
		public RfcControl(RfcLdapOID controlType, Asn1Boolean criticality, Asn1OctetString controlValue) : base(3)
		{
			base.add(controlType);
			if (criticality.booleanValue())
			{
				base.add(criticality);
			}
			if (controlValue != null)
			{
				base.add(controlValue);
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00018178 File Offset: 0x00017178
		[CLSCompliant(false)]
		public RfcControl(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00018190 File Offset: 0x00017190
		public RfcControl(Asn1Sequence seqObj) : base(3)
		{
			int num = seqObj.size();
			for (int i = 0; i < num; i++)
			{
				base.add(seqObj.get_Renamed(i));
			}
		}
	}
}
