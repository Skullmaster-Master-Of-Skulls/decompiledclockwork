using System;
using System.Reflection;
using System.Text;

namespace Telerik.Web.Data
{
	// Token: 0x02001B8D RID: 7053
	public abstract class DynamicClass
	{
		// Token: 0x06011167 RID: 69991 RVA: 0x003C53D4 File Offset: 0x003C35D4
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
