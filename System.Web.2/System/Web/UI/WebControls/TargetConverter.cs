using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004F1 RID: 1265
	public class TargetConverter : StringConverter
	{
		// Token: 0x06003F05 RID: 16133 RVA: 0x000CAAD8 File Offset: 0x000C8CD8
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				this.values = new TypeConverter.StandardValuesCollection(TargetConverter.targetValues);
			}
			return this.values;
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x00007722 File Offset: 0x00005922
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04002427 RID: 9255
		private static string[] targetValues = new string[]
		{
			"_blank",
			"_parent",
			"_search",
			"_self",
			"_top"
		};

		// Token: 0x04002428 RID: 9256
		private TypeConverter.StandardValuesCollection values;
	}
}
