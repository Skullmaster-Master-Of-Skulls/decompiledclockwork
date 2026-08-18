using System;
using System.ComponentModel;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x02000170 RID: 368
	public class SoapAttributes
	{
		// Token: 0x0600188C RID: 6284 RVA: 0x0006C04F File Offset: 0x0006A24F
		public SoapAttributes()
		{
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0006C058 File Offset: 0x0006A258
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

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x0006C158 File Offset: 0x0006A358
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

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0006C198 File Offset: 0x0006A398
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x0006C1A0 File Offset: 0x0006A3A0
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

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0006C1A9 File Offset: 0x0006A3A9
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x0006C1B1 File Offset: 0x0006A3B1
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

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0006C1BA File Offset: 0x0006A3BA
		// (set) Token: 0x06001894 RID: 6292 RVA: 0x0006C1C2 File Offset: 0x0006A3C2
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

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x0006C1CB File Offset: 0x0006A3CB
		// (set) Token: 0x06001896 RID: 6294 RVA: 0x0006C1D3 File Offset: 0x0006A3D3
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

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0006C1DC File Offset: 0x0006A3DC
		// (set) Token: 0x06001898 RID: 6296 RVA: 0x0006C1E4 File Offset: 0x0006A3E4
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

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x0006C1ED File Offset: 0x0006A3ED
		// (set) Token: 0x0600189A RID: 6298 RVA: 0x0006C1F5 File Offset: 0x0006A3F5
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

		// Token: 0x04000B46 RID: 2886
		private bool soapIgnore;

		// Token: 0x04000B47 RID: 2887
		private SoapTypeAttribute soapType;

		// Token: 0x04000B48 RID: 2888
		private SoapElementAttribute soapElement;

		// Token: 0x04000B49 RID: 2889
		private SoapAttributeAttribute soapAttribute;

		// Token: 0x04000B4A RID: 2890
		private SoapEnumAttribute soapEnum;

		// Token: 0x04000B4B RID: 2891
		private object soapDefaultValue;
	}
}
