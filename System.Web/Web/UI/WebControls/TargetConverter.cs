using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000660 RID: 1632
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TargetConverter : StringConverter
	{
		// Token: 0x06004FC5 RID: 20421 RVA: 0x001404C8 File Offset: 0x0013F4C8
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				this.values = new TypeConverter.StandardValuesCollection(TargetConverter.targetValues);
			}
			return this.values;
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x001404E8 File Offset: 0x0013F4E8
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x001404EB File Offset: 0x0013F4EB
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04002CF8 RID: 11512
		private static string[] targetValues = new string[]
		{
			"_blank",
			"_parent",
			"_search",
			"_self",
			"_top"
		};

		// Token: 0x04002CF9 RID: 11513
		private TypeConverter.StandardValuesCollection values;
	}
}
