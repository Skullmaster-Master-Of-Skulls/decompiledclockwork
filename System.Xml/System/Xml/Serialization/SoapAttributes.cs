using System;
using System.ComponentModel;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x020002EA RID: 746
	public class SoapAttributes
	{
		// Token: 0x060022D5 RID: 8917 RVA: 0x000A3BEF File Offset: 0x000A2BEF
		public SoapAttributes()
		{
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000A3BF8 File Offset: 0x000A2BF8
		public SoapAttributes(ICustomAttributeProvider provider)
		{
			object[] customAttributes = provider.GetCustomAttributes(false);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (customAttributes[i] is SoapIgnoreAttribute || customAttributes[i] is ObsoleteAttribute)
				{
					this.soapIgnore = true;
					break;
				}
				if (customAttributes[i] is SoapElementAttribute)
				{
					this.soapElement = (SoapElementAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is SoapAttributeAttribute)
				{
					this.soapAttribute = (SoapAttributeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is SoapTypeAttribute)
				{
					this.soapType = (SoapTypeAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is SoapEnumAttribute)
				{
					this.soapEnum = (SoapEnumAttribute)customAttributes[i];
				}
				else if (customAttributes[i] is DefaultValueAttribute)
				{
					this.soapDefaultValue = ((DefaultValueAttribute)customAttributes[i]).Value;
				}
			}
			if (this.soapIgnore)
			{
				this.soapElement = null;
				this.soapAttribute = null;
				this.soapType = null;
				this.soapEnum = null;
				this.soapDefaultValue = null;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x000A3CF8 File Offset: 0x000A2CF8
		internal SoapAttributeFlags SoapFlags
		{
			get
			{
				SoapAttributeFlags soapAttributeFlags = (SoapAttributeFlags)0;
				if (this.soapElement != null)
				{
					soapAttributeFlags |= SoapAttributeFlags.Element;
				}
				if (this.soapAttribute != null)
				{
					soapAttributeFlags |= SoapAttributeFlags.Attribute;
				}
				if (this.soapEnum != null)
				{
					soapAttributeFlags |= SoapAttributeFlags.Enum;
				}
				if (this.soapType != null)
				{
					soapAttributeFlags |= SoapAttributeFlags.Type;
				}
				return soapAttributeFlags;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x000A3D38 File Offset: 0x000A2D38
		// (set) Token: 0x060022D9 RID: 8921 RVA: 0x000A3D40 File Offset: 0x000A2D40
		public SoapTypeAttribute SoapType
		{
			get
			{
				return this.soapType;
			}
			set
			{
				this.soapType = value;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x000A3D49 File Offset: 0x000A2D49
		// (set) Token: 0x060022DB RID: 8923 RVA: 0x000A3D51 File Offset: 0x000A2D51
		public SoapEnumAttribute SoapEnum
		{
			get
			{
				return this.soapEnum;
			}
			set
			{
				this.soapEnum = value;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x000A3D5A File Offset: 0x000A2D5A
		// (set) Token: 0x060022DD RID: 8925 RVA: 0x000A3D62 File Offset: 0x000A2D62
		public bool SoapIgnore
		{
			get
			{
				return this.soapIgnore;
			}
			set
			{
				this.soapIgnore = value;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060022DE RID: 8926 RVA: 0x000A3D6B File Offset: 0x000A2D6B
		// (set) Token: 0x060022DF RID: 8927 RVA: 0x000A3D73 File Offset: 0x000A2D73
		public SoapElementAttribute SoapElement
		{
			get
			{
				return this.soapElement;
			}
			set
			{
				this.soapElement = value;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x000A3D7C File Offset: 0x000A2D7C
		// (set) Token: 0x060022E1 RID: 8929 RVA: 0x000A3D84 File Offset: 0x000A2D84
		public SoapAttributeAttribute SoapAttribute
		{
			get
			{
				return this.soapAttribute;
			}
			set
			{
				this.soapAttribute = value;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x000A3D8D File Offset: 0x000A2D8D
		// (set) Token: 0x060022E3 RID: 8931 RVA: 0x000A3D95 File Offset: 0x000A2D95
		public object SoapDefaultValue
		{
			get
			{
				return this.soapDefaultValue;
			}
			set
			{
				this.soapDefaultValue = value;
			}
		}

		// Token: 0x040014DA RID: 5338
		private bool soapIgnore;

		// Token: 0x040014DB RID: 5339
		private SoapTypeAttribute soapType;

		// Token: 0x040014DC RID: 5340
		private SoapElementAttribute soapElement;

		// Token: 0x040014DD RID: 5341
		private SoapAttributeAttribute soapAttribute;

		// Token: 0x040014DE RID: 5342
		private SoapEnumAttribute soapEnum;

		// Token: 0x040014DF RID: 5343
		private object soapDefaultValue;
	}
}
