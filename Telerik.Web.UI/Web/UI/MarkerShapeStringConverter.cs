using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x02000449 RID: 1097
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	internal class MarkerShapeStringConverter : StringConverter
	{
		// Token: 0x0600278F RID: 10127 RVA: 0x00080586 File Offset: 0x0007E786
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				this.values = new TypeConverter.StandardValuesCollection(MarkerShapeStringConverter.shapes);
			}
			return this.values;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000805A6 File Offset: 0x0007E7A6
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000805A9 File Offset: 0x0007E7A9
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04000A15 RID: 2581
		private static string[] shapes = new string[]
		{
			"pin",
			"pinTarget"
		};

		// Token: 0x04000A16 RID: 2582
		private TypeConverter.StandardValuesCollection values;
	}
}
