using System;
using System.Collections;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001E RID: 30
	public abstract class LdapSchemaElement : LdapAttribute
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00006E64 File Offset: 0x00005E64
		private void InitBlock()
		{
			this.hashQualifier = new Hashtable();
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006E7C File Offset: 0x00005E7C
		public virtual string[] Names
		{
			get
			{
				string[] result;
				if (this.names == null)
				{
					result = null;
				}
				else
				{
					string[] array = new string[this.names.Length];
					this.names.CopyTo(array, 0);
					result = array;
				}
				return result;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00006EB8 File Offset: 0x00005EB8
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006ED0 File Offset: 0x00005ED0
		public virtual string ID
		{
			get
			{
				return this.oid;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006EE8 File Offset: 0x00005EE8
		public virtual IEnumerator QualifierNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.hashQualifier.Keys).GetEnumerator());
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006F14 File Offset: 0x00005F14
		public virtual bool Obsolete
		{
			get
			{
				return this.obsolete;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006F2C File Offset: 0x00005F2C
		protected internal LdapSchemaElement(string attrName) : base(attrName)
		{
			this.InitBlock();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006F90 File Offset: 0x00005F90
		public virtual string[] getQualifier(string name)
		{
			AttributeQualifier attributeQualifier = (AttributeQualifier)this.hashQualifier[name];
			string[] result;
			if (attributeQualifier != null)
			{
				result = attributeQualifier.Values;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006FC0 File Offset: 0x00005FC0
		public override string ToString()
		{
			return this.formatString();
		}

		// Token: 0x06000136 RID: 310
		protected internal abstract string formatString();

		// Token: 0x06000137 RID: 311 RVA: 0x00006FD8 File Offset: 0x00005FD8
		public virtual void setQualifier(string name, string[] values)
		{
			AttributeQualifier newValue = new AttributeQualifier(name, values);
			SupportClass.PutElement(this.hashQualifier, name, newValue);
			base.Value = this.formatString();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000700C File Offset: 0x0000600C
		public override void addValue(string value_Renamed)
		{
			throw new NotSupportedException("addValue is not supported by LdapSchemaElement");
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007024 File Offset: 0x00006024
		public virtual void addValue(byte[] value_Renamed)
		{
			throw new NotSupportedException("addValue is not supported by LdapSchemaElement");
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000703C File Offset: 0x0000603C
		public override void removeValue(string value_Renamed)
		{
			throw new NotSupportedException("removeValue is not supported by LdapSchemaElement");
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007054 File Offset: 0x00006054
		public virtual void removeValue(byte[] value_Renamed)
		{
			throw new NotSupportedException("removeValue is not supported by LdapSchemaElement");
		}

		// Token: 0x040000AC RID: 172
		[CLSCompliant(false)]
		protected internal string[] names = new string[]
		{
			""
		};

		// Token: 0x040000AD RID: 173
		protected internal string oid = "";

		// Token: 0x040000AE RID: 174
		[CLSCompliant(false)]
		protected internal string description = "";

		// Token: 0x040000AF RID: 175
		[CLSCompliant(false)]
		protected internal bool obsolete = false;

		// Token: 0x040000B0 RID: 176
		protected internal string[] qualifier = new string[]
		{
			""
		};

		// Token: 0x040000B1 RID: 177
		protected internal Hashtable hashQualifier;
	}
}
