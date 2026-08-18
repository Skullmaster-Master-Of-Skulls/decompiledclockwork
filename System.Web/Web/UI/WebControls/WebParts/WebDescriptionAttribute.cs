using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000702 RID: 1794
	[AttributeUsage(AttributeTargets.Property)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WebDescriptionAttribute : Attribute
	{
		// Token: 0x0600577B RID: 22395 RVA: 0x0016101D File Offset: 0x0016001D
		public WebDescriptionAttribute() : this(string.Empty)
		{
		}

		// Token: 0x0600577C RID: 22396 RVA: 0x0016102A File Offset: 0x0016002A
		public WebDescriptionAttribute(string description)
		{
			this._description = description;
		}

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x0600577D RID: 22397 RVA: 0x00161039 File Offset: 0x00160039
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x0600577E RID: 22398 RVA: 0x00161041 File Offset: 0x00160041
		// (set) Token: 0x0600577F RID: 22399 RVA: 0x00161049 File Offset: 0x00160049
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

		// Token: 0x06005780 RID: 22400 RVA: 0x00161054 File Offset: 0x00160054
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			WebDescriptionAttribute webDescriptionAttribute = obj as WebDescriptionAttribute;
			return webDescriptionAttribute != null && webDescriptionAttribute.Description == this.Description;
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x00161084 File Offset: 0x00160084
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x00161091 File Offset: 0x00160091
		public override bool IsDefaultAttribute()
		{
			return this.Equals(WebDescriptionAttribute.Default);
		}

		// Token: 0x04002FA3 RID: 12195
		public static readonly WebDescriptionAttribute Default = new WebDescriptionAttribute();

		// Token: 0x04002FA4 RID: 12196
		private string _description;
	}
}
