using System;

namespace System.ComponentModel
{
	// Token: 0x0200058C RID: 1420
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class LookupBindingPropertiesAttribute : Attribute
	{
		// Token: 0x0600345F RID: 13407 RVA: 0x000E4F45 File Offset: 0x000E3145
		public LookupBindingPropertiesAttribute()
		{
			this.dataSource = null;
			this.displayMember = null;
			this.valueMember = null;
			this.lookupMember = null;
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x000E4F69 File Offset: 0x000E3169
		public LookupBindingPropertiesAttribute(string dataSource, string displayMember, string valueMember, string lookupMember)
		{
			this.dataSource = dataSource;
			this.displayMember = displayMember;
			this.valueMember = valueMember;
			this.lookupMember = lookupMember;
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06003461 RID: 13409 RVA: 0x000E4F8E File Offset: 0x000E318E
		public string DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06003462 RID: 13410 RVA: 0x000E4F96 File Offset: 0x000E3196
		public string DisplayMember
		{
			get
			{
				return this.displayMember;
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06003463 RID: 13411 RVA: 0x000E4F9E File Offset: 0x000E319E
		public string ValueMember
		{
			get
			{
				return this.valueMember;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06003464 RID: 13412 RVA: 0x000E4FA6 File Offset: 0x000E31A6
		public string LookupMember
		{
			get
			{
				return this.lookupMember;
			}
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x000E4FB0 File Offset: 0x000E31B0
		public override bool Equals(object obj)
		{
			LookupBindingPropertiesAttribute lookupBindingPropertiesAttribute = obj as LookupBindingPropertiesAttribute;
			return lookupBindingPropertiesAttribute != null && lookupBindingPropertiesAttribute.DataSource == this.dataSource && lookupBindingPropertiesAttribute.displayMember == this.displayMember && lookupBindingPropertiesAttribute.valueMember == this.valueMember && lookupBindingPropertiesAttribute.lookupMember == this.lookupMember;
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000E5013 File Offset: 0x000E3213
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040029F3 RID: 10739
		private readonly string dataSource;

		// Token: 0x040029F4 RID: 10740
		private readonly string displayMember;

		// Token: 0x040029F5 RID: 10741
		private readonly string valueMember;

		// Token: 0x040029F6 RID: 10742
		private readonly string lookupMember;

		// Token: 0x040029F7 RID: 10743
		public static readonly LookupBindingPropertiesAttribute Default = new LookupBindingPropertiesAttribute();
	}
}
