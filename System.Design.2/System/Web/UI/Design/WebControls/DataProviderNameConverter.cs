using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000C0 RID: 192
	public class DataProviderNameConverter : StringConverter
	{
		// Token: 0x0600061F RID: 1567 RVA: 0x00020F20 File Offset: 0x0001F120
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			DataTable factoryClasses = DbProviderFactories.GetFactoryClasses();
			DataRowCollection rows = factoryClasses.Rows;
			string[] array = new string[rows.Count];
			for (int i = 0; i < rows.Count; i++)
			{
				array[i] = (string)rows[i]["InvariantName"];
			}
			return new TypeConverter.StandardValuesCollection(array);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
