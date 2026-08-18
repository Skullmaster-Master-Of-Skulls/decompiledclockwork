using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000575 RID: 1397
	[AttributeUsage(AttributeTargets.Property)]
	public class WebDescriptionAttribute : Attribute
	{
		// Token: 0x060046CC RID: 18124 RVA: 0x000E9FF5 File Offset: 0x000E81F5
		public WebDescriptionAttribute() : this(string.Empty)
		{
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x000EA002 File Offset: 0x000E8202
		public WebDescriptionAttribute(string description)
		{
			this._description = description;
		}

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x060046CE RID: 18126 RVA: 0x000EA011 File Offset: 0x000E8211
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x060046CF RID: 18127 RVA: 0x000EA019 File Offset: 0x000E8219
		// (set) Token: 0x060046D0 RID: 18128 RVA: 0x000EA021 File Offset: 0x000E8221
		protected string DescriptionValue
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x000EA02C File Offset: 0x000E822C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			WebDescriptionAttribute webDescriptionAttribute = obj as WebDescriptionAttribute;
			return webDescriptionAttribute != null && webDescriptionAttribute.Description == this.Description;
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x000EA05C File Offset: 0x000E825C
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x000EA069 File Offset: 0x000E8269
		public override bool IsDefaultAttribute()
		{
			return this.Equals(WebDescriptionAttribute.Default);
		}

		// Token: 0x040026C2 RID: 9922
		public static readonly WebDescriptionAttribute Default = new WebDescriptionAttribute();

		// Token: 0x040026C3 RID: 9923
		private string _description;
	}
}
