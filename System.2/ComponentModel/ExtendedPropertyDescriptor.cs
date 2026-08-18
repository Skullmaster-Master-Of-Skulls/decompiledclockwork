using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000553 RID: 1363
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class ExtendedPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06003347 RID: 13127 RVA: 0x000E3D04 File Offset: 0x000E1F04
		public ExtendedPropertyDescriptor(ReflectPropertyDescriptor extenderInfo, Type receiverType, IExtenderProvider provider, Attribute[] attributes) : base(extenderInfo, attributes)
		{
			ArrayList arrayList = new ArrayList(this.AttributeArray);
			arrayList.Add(ExtenderProvidedPropertyAttribute.Create(extenderInfo, receiverType, provider));
			if (extenderInfo.IsReadOnly)
			{
				arrayList.Add(ReadOnlyAttribute.Yes);
			}
			Attribute[] array = new Attribute[arrayList.Count];
			arrayList.CopyTo(array, 0);
			this.AttributeArray = array;
			this.extenderInfo = extenderInfo;
			this.provider = provider;
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000E3D74 File Offset: 0x000E1F74
		public ExtendedPropertyDescriptor(PropertyDescriptor extender, Attribute[] attributes) : base(extender, attributes)
		{
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = extender.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;
			ReflectPropertyDescriptor reflectPropertyDescriptor = extenderProvidedPropertyAttribute.ExtenderProperty as ReflectPropertyDescriptor;
			this.extenderInfo = reflectPropertyDescriptor;
			this.provider = extenderProvidedPropertyAttribute.Provider;
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000E3DC3 File Offset: 0x000E1FC3
		public override bool CanResetValue(object comp)
		{
			return this.extenderInfo.ExtenderCanResetValue(this.provider, comp);
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x000E3DD7 File Offset: 0x000E1FD7
		public override Type ComponentType
		{
			get
			{
				return this.extenderInfo.ComponentType;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x0600334B RID: 13131 RVA: 0x000E3DE4 File Offset: 0x000E1FE4
		public override bool IsReadOnly
		{
			get
			{
				return this.Attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x000E3E05 File Offset: 0x000E2005
		public override Type PropertyType
		{
			get
			{
				return this.extenderInfo.ExtenderGetType(this.provider);
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x000E3E18 File Offset: 0x000E2018
		public override string DisplayName
		{
			get
			{
				string text = base.DisplayName;
				DisplayNameAttribute displayNameAttribute = this.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
				if (displayNameAttribute == null || displayNameAttribute.IsDefaultAttribute())
				{
					ISite site = MemberDescriptor.GetSite(this.provider);
					if (site != null)
					{
						string name = site.Name;
						if (name != null && name.Length > 0)
						{
							text = SR.GetString("MetaExtenderName", new object[]
							{
								text,
								name
							});
						}
					}
				}
				return text;
			}
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000E3E8E File Offset: 0x000E208E
		public override object GetValue(object comp)
		{
			return this.extenderInfo.ExtenderGetValue(this.provider, comp);
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000E3EA2 File Offset: 0x000E20A2
		public override void ResetValue(object comp)
		{
			this.extenderInfo.ExtenderResetValue(this.provider, comp, this);
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x000E3EB7 File Offset: 0x000E20B7
		public override void SetValue(object component, object value)
		{
			this.extenderInfo.ExtenderSetValue(this.provider, component, value, this);
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000E3ECD File Offset: 0x000E20CD
		public override bool ShouldSerializeValue(object comp)
		{
			return this.extenderInfo.ExtenderShouldSerializeValue(this.provider, comp);
		}

		// Token: 0x040029BB RID: 10683
		private readonly ReflectPropertyDescriptor extenderInfo;

		// Token: 0x040029BC RID: 10684
		private readonly IExtenderProvider provider;
	}
}
