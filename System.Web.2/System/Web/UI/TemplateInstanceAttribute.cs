using System;

namespace System.Web.UI
{
	// Token: 0x02000312 RID: 786
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class TemplateInstanceAttribute : Attribute
	{
		// Token: 0x06002464 RID: 9316 RVA: 0x00076EA2 File Offset: 0x000750A2
		public TemplateInstanceAttribute(TemplateInstance instances)
		{
			this._instances = instances;
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x00076EB1 File Offset: 0x000750B1
		public TemplateInstance Instances
		{
			get
			{
				return this._instances;
			}
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x00076EBC File Offset: 0x000750BC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			TemplateInstanceAttribute templateInstanceAttribute = obj as TemplateInstanceAttribute;
			return templateInstanceAttribute != null && templateInstanceAttribute.Instances == this.Instances;
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x00076EE9 File Offset: 0x000750E9
		public override int GetHashCode()
		{
			return this._instances.GetHashCode();
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x00076EFC File Offset: 0x000750FC
		public override bool IsDefaultAttribute()
		{
			return this.Equals(TemplateInstanceAttribute.Default);
		}

		// Token: 0x04001D08 RID: 7432
		public static readonly TemplateInstanceAttribute Multiple = new TemplateInstanceAttribute(TemplateInstance.Multiple);

		// Token: 0x04001D09 RID: 7433
		public static readonly TemplateInstanceAttribute Single = new TemplateInstanceAttribute(TemplateInstance.Single);

		// Token: 0x04001D0A RID: 7434
		public static readonly TemplateInstanceAttribute Default = TemplateInstanceAttribute.Multiple;

		// Token: 0x04001D0B RID: 7435
		private TemplateInstance _instances;
	}
}
