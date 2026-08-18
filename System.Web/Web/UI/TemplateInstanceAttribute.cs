using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x02000473 RID: 1139
	[AttributeUsage(AttributeTargets.Property)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TemplateInstanceAttribute : Attribute
	{
		// Token: 0x060035B0 RID: 13744 RVA: 0x000E7ED3 File Offset: 0x000E6ED3
		public TemplateInstanceAttribute(TemplateInstance instances)
		{
			this._instances = instances;
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x060035B1 RID: 13745 RVA: 0x000E7EE2 File Offset: 0x000E6EE2
		public TemplateInstance Instances
		{
			get
			{
				return this._instances;
			}
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000E7EEC File Offset: 0x000E6EEC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			TemplateInstanceAttribute templateInstanceAttribute = obj as TemplateInstanceAttribute;
			return templateInstanceAttribute != null && templateInstanceAttribute.Instances == this.Instances;
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000E7F19 File Offset: 0x000E6F19
		public override int GetHashCode()
		{
			return this._instances.GetHashCode();
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000E7F2B File Offset: 0x000E6F2B
		public override bool IsDefaultAttribute()
		{
			return this.Equals(TemplateInstanceAttribute.Default);
		}

		// Token: 0x04002548 RID: 9544
		public static readonly TemplateInstanceAttribute Multiple = new TemplateInstanceAttribute(TemplateInstance.Multiple);

		// Token: 0x04002549 RID: 9545
		public static readonly TemplateInstanceAttribute Single = new TemplateInstanceAttribute(TemplateInstance.Single);

		// Token: 0x0400254A RID: 9546
		public static readonly TemplateInstanceAttribute Default = TemplateInstanceAttribute.Multiple;

		// Token: 0x0400254B RID: 9547
		private TemplateInstance _instances;
	}
}
