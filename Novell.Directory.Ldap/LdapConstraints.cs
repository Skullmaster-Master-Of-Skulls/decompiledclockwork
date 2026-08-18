using System;
using System.Collections;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000029 RID: 41
	public class LdapConstraints : ICloneable
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00009B5C File Offset: 0x00008B5C
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00009B74 File Offset: 0x00008B74
		public virtual int HopLimit
		{
			get
			{
				return this.hopLimit;
			}
			set
			{
				this.hopLimit = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00009B8C File Offset: 0x00008B8C
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00009BA4 File Offset: 0x00008BA4
		internal virtual Hashtable Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = (Hashtable)value.Clone();
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00009BC4 File Offset: 0x00008BC4
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00009BDC File Offset: 0x00008BDC
		public virtual bool ReferralFollowing
		{
			get
			{
				return this.doReferrals;
			}
			set
			{
				this.doReferrals = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00009BF4 File Offset: 0x00008BF4
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00009C0C File Offset: 0x00008C0C
		public virtual int TimeLimit
		{
			get
			{
				return this.msLimit;
			}
			set
			{
				this.msLimit = value;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00009C24 File Offset: 0x00008C24
		public LdapConstraints()
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00009C64 File Offset: 0x00008C64
		public LdapConstraints(int msLimit, bool doReferrals, LdapReferralHandler handler, int hop_limit)
		{
			this.msLimit = msLimit;
			this.doReferrals = doReferrals;
			this.refHandler = handler;
			this.hopLimit = hop_limit;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009CC4 File Offset: 0x00008CC4
		public virtual LdapControl[] getControls()
		{
			return this.controls;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009CDC File Offset: 0x00008CDC
		public virtual object getProperty(string name)
		{
			object result;
			if (this.properties == null)
			{
				result = null;
			}
			else
			{
				result = this.properties[name];
			}
			return result;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009D08 File Offset: 0x00008D08
		internal virtual LdapReferralHandler getReferralHandler()
		{
			return this.refHandler;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00009D20 File Offset: 0x00008D20
		public virtual void setControls(LdapControl control)
		{
			if (control == null)
			{
				this.controls = null;
			}
			else
			{
				this.controls = new LdapControl[1];
				this.controls[0] = (LdapControl)control.Clone();
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009D5C File Offset: 0x00008D5C
		public virtual void setControls(LdapControl[] controls)
		{
			if (controls == null || controls.Length == 0)
			{
				this.controls = null;
			}
			else
			{
				this.controls = new LdapControl[controls.Length];
				for (int i = 0; i < controls.Length; i++)
				{
					this.controls[i] = (LdapControl)controls[i].Clone();
				}
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009DB0 File Offset: 0x00008DB0
		public virtual void setProperty(string name, object value_Renamed)
		{
			if (this.properties == null)
			{
				this.properties = new Hashtable();
			}
			SupportClass.PutElement(this.properties, name, value_Renamed);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009DE0 File Offset: 0x00008DE0
		public virtual void setReferralHandler(LdapReferralHandler handler)
		{
			this.refHandler = handler;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009DF8 File Offset: 0x00008DF8
		public object Clone()
		{
			object result;
			try
			{
				object obj = base.MemberwiseClone();
				if (this.controls != null)
				{
					((LdapConstraints)obj).controls = new LdapControl[this.controls.Length];
					this.controls.CopyTo(((LdapConstraints)obj).controls, 0);
				}
				if (this.properties != null)
				{
					((LdapConstraints)obj).properties = (Hashtable)this.properties.Clone();
				}
				result = obj;
			}
			catch (Exception ex)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return result;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00009E94 File Offset: 0x00008E94
		static LdapConstraints()
		{
			LdapConstraints.nameLock = new object();
		}

		// Token: 0x040000DA RID: 218
		private int msLimit = 0;

		// Token: 0x040000DB RID: 219
		private int hopLimit = 10;

		// Token: 0x040000DC RID: 220
		private bool doReferrals = false;

		// Token: 0x040000DD RID: 221
		private LdapReferralHandler refHandler = null;

		// Token: 0x040000DE RID: 222
		private LdapControl[] controls = null;

		// Token: 0x040000DF RID: 223
		private static object nameLock;

		// Token: 0x040000E0 RID: 224
		private static int lConsNum = 0;

		// Token: 0x040000E1 RID: 225
		private string name;

		// Token: 0x040000E2 RID: 226
		private Hashtable properties = null;
	}
}
