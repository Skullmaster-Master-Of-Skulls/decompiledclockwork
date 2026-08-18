using System;
using System.Reflection;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x02000370 RID: 880
	public abstract class DynamicClass
	{
		// Token: 0x06001E35 RID: 7733 RVA: 0x0005E2DD File Offset: 0x0005C4DD
		public DynamicClass()
		{
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0005E2E8 File Offset: 0x0005C4E8
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
