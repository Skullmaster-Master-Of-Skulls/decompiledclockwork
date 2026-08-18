using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x0200025A RID: 602
	internal abstract class SchemaDeclBase
	{
		// Token: 0x060023F2 RID: 9202 RVA: 0x000C5FA0 File Offset: 0x000C41A0
		protected SchemaDeclBase(XmlQualifiedName name, string prefix)
		{
			this.name = name;
			this.prefix = prefix;
			this.maxLength = -1L;
			this.minLength = -1L;
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000C5FD1 File Offset: 0x000C41D1
		protected SchemaDeclBase()
		{
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x000C5FE4 File Offset: 0x000C41E4
		// (set) Token: 0x060023F5 RID: 9205 RVA: 0x000C5FEC File Offset: 0x000C41EC
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x000C5FF5 File Offset: 0x000C41F5
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x000C600B File Offset: 0x000C420B
		internal string Prefix
		{
			get
			{
				if (this.prefix != null)
				{
					return this.prefix;
				}
				return string.Empty;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x000C6014 File Offset: 0x000C4214
		// (set) Token: 0x060023F9 RID: 9209 RVA: 0x000C601C File Offset: 0x000C421C
		internal bool IsDeclaredInExternal
		{
			get
			{
				return this.isDeclaredInExternal;
			}
			set
			{
				this.isDeclaredInExternal = value;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x000C6025 File Offset: 0x000C4225
		// (set) Token: 0x060023FB RID: 9211 RVA: 0x000C602D File Offset: 0x000C422D
		internal SchemaDeclBase.Use Presence
		{
			get
			{
				return this.presence;
			}
			set
			{
				this.presence = value;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x060023FC RID: 9212 RVA: 0x000C6036 File Offset: 0x000C4236
		// (set) Token: 0x060023FD RID: 9213 RVA: 0x000C603E File Offset: 0x000C423E
		internal long MaxLength
		{
			get
			{
				return this.maxLength;
			}
			set
			{
				this.maxLength = value;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x000C6047 File Offset: 0x000C4247
		// (set) Token: 0x060023FF RID: 9215 RVA: 0x000C604F File Offset: 0x000C424F
		internal long MinLength
		{
			get
			{
				return this.minLength;
			}
			set
			{
				this.minLength = value;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x000C6058 File Offset: 0x000C4258
		// (set) Token: 0x06002401 RID: 9217 RVA: 0x000C6060 File Offset: 0x000C4260
		internal XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x000C6069 File Offset: 0x000C4269
		// (set) Token: 0x06002403 RID: 9219 RVA: 0x000C6071 File Offset: 0x000C4271
		internal XmlSchemaDatatype Datatype
		{
			get
			{
				return this.datatype;
			}
			set
			{
				this.datatype = value;
			}
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000C607A File Offset: 0x000C427A
		internal void AddValue(string value)
		{
			if (this.values == null)
			{
				this.values = new List<string>();
			}
			this.values.Add(value);
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x000C609B File Offset: 0x000C429B
		// (set) Token: 0x06002406 RID: 9222 RVA: 0x000C60A3 File Offset: 0x000C42A3
		internal List<string> Values
		{
			get
			{
				return this.values;
			}
			set
			{
				this.values = value;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002407 RID: 9223 RVA: 0x000C60AC File Offset: 0x000C42AC
		// (set) Token: 0x06002408 RID: 9224 RVA: 0x000C60C2 File Offset: 0x000C42C2
		internal string DefaultValueRaw
		{
			get
			{
				if (this.defaultValueRaw == null)
				{
					return string.Empty;
				}
				return this.defaultValueRaw;
			}
			set
			{
				this.defaultValueRaw = value;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x000C60CB File Offset: 0x000C42CB
		// (set) Token: 0x0600240A RID: 9226 RVA: 0x000C60D3 File Offset: 0x000C42D3
		internal object DefaultValueTyped
		{
			get
			{
				return this.defaultValueTyped;
			}
			set
			{
				this.defaultValueTyped = value;
			}
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000C60DC File Offset: 0x000C42DC
		internal bool CheckEnumeration(object pVal)
		{
			return (this.datatype.TokenizedType != XmlTokenizedType.NOTATION && this.datatype.TokenizedType != XmlTokenizedType.ENUMERATION) || this.values.Contains(pVal.ToString());
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000C610E File Offset: 0x000C430E
		internal bool CheckValue(object pVal)
		{
			return (this.presence != SchemaDeclBase.Use.Fixed && this.presence != SchemaDeclBase.Use.RequiredFixed) || (this.defaultValueTyped != null && this.datatype.IsEqual(pVal, this.defaultValueTyped));
		}

		// Token: 0x04000F0A RID: 3850
		protected XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04000F0B RID: 3851
		protected string prefix;

		// Token: 0x04000F0C RID: 3852
		protected bool isDeclaredInExternal;

		// Token: 0x04000F0D RID: 3853
		protected SchemaDeclBase.Use presence;

		// Token: 0x04000F0E RID: 3854
		protected XmlSchemaType schemaType;

		// Token: 0x04000F0F RID: 3855
		protected XmlSchemaDatatype datatype;

		// Token: 0x04000F10 RID: 3856
		protected string defaultValueRaw;

		// Token: 0x04000F11 RID: 3857
		protected object defaultValueTyped;

		// Token: 0x04000F12 RID: 3858
		protected long maxLength;

		// Token: 0x04000F13 RID: 3859
		protected long minLength;

		// Token: 0x04000F14 RID: 3860
		protected List<string> values;

		// Token: 0x02000496 RID: 1174
		internal enum Use
		{
			// Token: 0x04001E41 RID: 7745
			Default,
			// Token: 0x04001E42 RID: 7746
			Required,
			// Token: 0x04001E43 RID: 7747
			Implied,
			// Token: 0x04001E44 RID: 7748
			Fixed,
			// Token: 0x04001E45 RID: 7749
			RequiredFixed
		}
	}
}
