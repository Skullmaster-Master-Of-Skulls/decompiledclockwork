using System;
using System.Reflection;
using System.Text;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CE3 RID: 3299
	public abstract class PivotDynamicClass
	{
		// Token: 0x06007B40 RID: 31552 RVA: 0x001C4D4C File Offset: 0x001C2F4C
		public override string ToString()
		{
			PropertyInfo[] properties = base.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			for (int i = 0; i < properties.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(properties[i].Name);
				stringBuilder.Append("=");
				stringBuilder.Append(properties[i].GetValue(this, null));
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
