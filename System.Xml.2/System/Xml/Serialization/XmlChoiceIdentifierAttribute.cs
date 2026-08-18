using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x0200018E RID: 398
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public class XmlChoiceIdentifierAttribute : Attribute
	{
		// Token: 0x06001A0D RID: 6669 RVA: 0x00073A91 File Offset: 0x00071C91
		[__DynamicallyInvokable]
		public XmlChoiceIdentifierAttribute()
		{
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00073A99 File Offset: 0x00071C99
		[__DynamicallyInvokable]
		public XmlChoiceIdentifierAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x00073AA8 File Offset: 0x00071CA8
		// (set) Token: 0x06001A10 RID: 6672 RVA: 0x00073ABE File Offset: 0x00071CBE
		[__DynamicallyInvokable]
		public string MemberName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			[__DynamicallyInvokable]
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x00073AC7 File Offset: 0x00071CC7
		// (set) Token: 0x06001A12 RID: 6674 RVA: 0x00073ACF File Offset: 0x00071CCF
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
			set
			{
				this.memberInfo = value;
			}
		}

		// Token: 0x04000BE4 RID: 3044
		private string name;

		// Token: 0x04000BE5 RID: 3045
		private MemberInfo memberInfo;
	}
}
