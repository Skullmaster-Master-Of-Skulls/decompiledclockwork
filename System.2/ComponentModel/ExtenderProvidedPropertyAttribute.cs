using System;

namespace System.ComponentModel
{
	// Token: 0x02000554 RID: 1364
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ExtenderProvidedPropertyAttribute : Attribute
	{
		// Token: 0x06003352 RID: 13138 RVA: 0x000E3EE4 File Offset: 0x000E20E4
		internal static ExtenderProvidedPropertyAttribute Create(PropertyDescriptor extenderProperty, Type receiverType, IExtenderProvider provider)
		{
			return new ExtenderProvidedPropertyAttribute
			{
				extenderProperty = extenderProperty,
				receiverType = receiverType,
				provider = provider
			};
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x000E3F15 File Offset: 0x000E2115
		public PropertyDescriptor ExtenderProperty
		{
			get
			{
				return this.extenderProperty;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x000E3F1D File Offset: 0x000E211D
		public IExtenderProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x000E3F25 File Offset: 0x000E2125
		public Type ReceiverType
		{
			get
			{
				return this.receiverType;
			}
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000E3F30 File Offset: 0x000E2130
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = obj as ExtenderProvidedPropertyAttribute;
			return extenderProvidedPropertyAttribute != null && extenderProvidedPropertyAttribute.extenderProperty.Equals(this.extenderProperty) && extenderProvidedPropertyAttribute.provider.Equals(this.provider) && extenderProvidedPropertyAttribute.receiverType.Equals(this.receiverType);
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x000E3F86 File Offset: 0x000E2186
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x000E3F8E File Offset: 0x000E218E
		public override bool IsDefaultAttribute()
		{
			return this.receiverType == null;
		}

		// Token: 0x040029BD RID: 10685
		private PropertyDescriptor extenderProperty;

		// Token: 0x040029BE RID: 10686
		private IExtenderProvider provider;

		// Token: 0x040029BF RID: 10687
		private Type receiverType;
	}
}
