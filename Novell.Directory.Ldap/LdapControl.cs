using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002A RID: 42
	public class LdapControl : ICloneable
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00009EB4 File Offset: 0x00008EB4
		public virtual string ID
		{
			get
			{
				return new StringBuilder(this.control.ControlType.stringValue()).ToString();
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00009EE0 File Offset: 0x00008EE0
		public virtual bool Critical
		{
			get
			{
				return this.control.Criticality.booleanValue();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00009F04 File Offset: 0x00008F04
		internal static RespControlVector RegisteredControls
		{
			get
			{
				return LdapControl.registeredControls;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00009F1C File Offset: 0x00008F1C
		internal virtual RfcControl Asn1Object
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009F34 File Offset: 0x00008F34
		[CLSCompliant(false)]
		public LdapControl(string oid, bool critical, sbyte[] values)
		{
			if (oid == null)
			{
				throw new ArgumentException("An OID must be specified");
			}
			if (values == null)
			{
				this.control = new RfcControl(new RfcLdapOID(oid), new Asn1Boolean(critical));
			}
			else
			{
				this.control = new RfcControl(new RfcLdapOID(oid), new Asn1Boolean(critical), new Asn1OctetString(values));
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009F90 File Offset: 0x00008F90
		protected internal LdapControl(RfcControl control)
		{
			this.control = control;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009FAC File Offset: 0x00008FAC
		public object Clone()
		{
			LdapControl ldapControl;
			try
			{
				ldapControl = (LdapControl)base.MemberwiseClone();
			}
			catch (Exception ex)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			sbyte[] value = this.getValue();
			if (value != null)
			{
				sbyte[] array = new sbyte[value.Length];
				for (int i = 0; i < value.Length; i++)
				{
					array[i] = value[i];
				}
				ldapControl.control = new RfcControl(new RfcLdapOID(this.ID), new Asn1Boolean(this.Critical), new Asn1OctetString(array));
			}
			return ldapControl;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000A04C File Offset: 0x0000904C
		[CLSCompliant(false)]
		public virtual sbyte[] getValue()
		{
			sbyte[] result = null;
			Asn1OctetString controlValue = this.control.ControlValue;
			if (controlValue != null)
			{
				result = controlValue.byteValue();
			}
			return result;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000A078 File Offset: 0x00009078
		[CLSCompliant(false)]
		protected internal virtual void setValue(sbyte[] controlValue)
		{
			this.control.ControlValue = new Asn1OctetString(controlValue);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000A098 File Offset: 0x00009098
		public static void register(string oid, Type controlClass)
		{
			LdapControl.registeredControls.registerResponseControl(oid, controlClass);
		}

		// Token: 0x040000E3 RID: 227
		private static RespControlVector registeredControls = new RespControlVector(5, 5);

		// Token: 0x040000E4 RID: 228
		private RfcControl control;
	}
}
