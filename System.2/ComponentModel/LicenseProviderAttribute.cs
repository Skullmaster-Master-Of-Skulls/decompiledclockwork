using System;

namespace System.ComponentModel
{
	// Token: 0x02000581 RID: 1409
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class LicenseProviderAttribute : Attribute
	{
		// Token: 0x06003421 RID: 13345 RVA: 0x000E4A0A File Offset: 0x000E2C0A
		public LicenseProviderAttribute() : this(null)
		{
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000E4A13 File Offset: 0x000E2C13
		public LicenseProviderAttribute(string typeName)
		{
			this.licenseProviderName = typeName;
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000E4A22 File Offset: 0x000E2C22
		public LicenseProviderAttribute(Type type)
		{
			this.licenseProviderType = type;
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x000E4A31 File Offset: 0x000E2C31
		public Type LicenseProvider
		{
			get
			{
				if (this.licenseProviderType == null && this.licenseProviderName != null)
				{
					this.licenseProviderType = Type.GetType(this.licenseProviderName);
				}
				return this.licenseProviderType;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x000E4A60 File Offset: 0x000E2C60
		public override object TypeId
		{
			get
			{
				string fullName = this.licenseProviderName;
				if (fullName == null && this.licenseProviderType != null)
				{
					fullName = this.licenseProviderType.FullName;
				}
				return base.GetType().FullName + fullName;
			}
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000E4AA4 File Offset: 0x000E2CA4
		public override bool Equals(object value)
		{
			if (value is LicenseProviderAttribute && value != null)
			{
				Type licenseProvider = ((LicenseProviderAttribute)value).LicenseProvider;
				if (licenseProvider == this.LicenseProvider)
				{
					return true;
				}
				if (licenseProvider != null && licenseProvider.Equals(this.LicenseProvider))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000E4AF2 File Offset: 0x000E2CF2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040029D1 RID: 10705
		public static readonly LicenseProviderAttribute Default = new LicenseProviderAttribute();

		// Token: 0x040029D2 RID: 10706
		private Type licenseProviderType;

		// Token: 0x040029D3 RID: 10707
		private string licenseProviderName;
	}
}
