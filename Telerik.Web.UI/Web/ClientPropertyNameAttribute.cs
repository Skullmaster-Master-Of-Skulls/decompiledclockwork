using System;

namespace Telerik.Web
{
	// Token: 0x02000F5A RID: 3930
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ClientPropertyNameAttribute : Attribute
	{
		// Token: 0x060095F1 RID: 38385 RVA: 0x002184F8 File Offset: 0x002166F8
		public ClientPropertyNameAttribute()
		{
		}

		// Token: 0x060095F2 RID: 38386 RVA: 0x00218500 File Offset: 0x00216700
		public ClientPropertyNameAttribute(string propertyName)
		{
			this._propertyName = propertyName;
		}

		// Token: 0x17002F61 RID: 12129
		// (get) Token: 0x060095F3 RID: 38387 RVA: 0x0021850F File Offset: 0x0021670F
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x060095F4 RID: 38388 RVA: 0x00218517 File Offset: 0x00216717
		public override bool IsDefaultAttribute()
		{
			return string.IsNullOrEmpty(this.PropertyName);
		}

		// Token: 0x04002ADB RID: 10971
		private readonly string _propertyName;
	}
}
