using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200006F RID: 111
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class Pkcs9AttributeObject : AsnEncodedData
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x00016AAB File Offset: 0x00014CAB
		internal Pkcs9AttributeObject(Oid oid)
		{
			base.Oid = oid;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00016ABA File Offset: 0x00014CBA
		public Pkcs9AttributeObject()
		{
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00016AC2 File Offset: 0x00014CC2
		public Pkcs9AttributeObject(string oid, byte[] encodedData) : this(new AsnEncodedData(oid, encodedData))
		{
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00016AD1 File Offset: 0x00014CD1
		public Pkcs9AttributeObject(Oid oid, byte[] encodedData) : this(new AsnEncodedData(oid, encodedData))
		{
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00016AE0 File Offset: 0x00014CE0
		public Pkcs9AttributeObject(AsnEncodedData asnEncodedData) : base(asnEncodedData)
		{
			if (asnEncodedData.Oid == null)
			{
				throw new ArgumentNullException("asnEncodedData.Oid");
			}
			string value = base.Oid.Value;
			if (value == null)
			{
				throw new ArgumentNullException("oid.Value");
			}
			if (value.Length == 0)
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Arg_EmptyOrNullString"), "oid.Value");
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00016B3E File Offset: 0x00014D3E
		public new Oid Oid
		{
			get
			{
				return base.Oid;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00016B48 File Offset: 0x00014D48
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			if (!(asnEncodedData is Pkcs9AttributeObject))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Pkcs9_AttributeMismatch"));
			}
			base.CopyFrom(asnEncodedData);
		}
	}
}
