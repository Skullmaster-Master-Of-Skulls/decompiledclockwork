using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005D1 RID: 1489
	public static class WebSettingsAdapter
	{
		// Token: 0x06002FDD RID: 12253 RVA: 0x0003AF20 File Offset: 0x00039120
		public static string GetWebSettingsDefinitionHtml()
		{
			Group[] array = (Group[])Enum.GetValues(typeof(Group));
			Setting[] source = (Setting[])Enum.GetValues(typeof(Setting));
			List<Setting> list = source.ToList<Setting>();
			List<WebSettingWithAttribute> source2 = list.ConvertAll<WebSettingWithAttribute>((Setting g) => new WebSettingWithAttribute(g));
			List<WebSettingGroupWithEnums> list2 = new List<WebSettingGroupWithEnums>();
			Group[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Group groupEnum = array2[i];
				GroupDataAttribute attribute = WebSettingGroupWithEnums.GetAttribute<GroupDataAttribute>(groupEnum);
				bool flag = groupEnum != Group.UNKNOWN && attribute != null && attribute.IsActive;
				if (flag)
				{
					list2.Add(new WebSettingGroupWithEnums(groupEnum, attribute, (from g in source2
					where g.IsValid && !g.IsHidden && g.Group == groupEnum
					select g).ToList<WebSettingWithAttribute>()));
				}
			}
			list2.Sort((WebSettingGroupWithEnums g1, WebSettingGroupWithEnums g2) => (g1.GroupDataAttribute.Name ?? "").CompareTo(g2.GroupDataAttribute.Name ?? ""));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (WebSettingGroupWithEnums webSettingGroupWithEnums in list2)
			{
				bool flag2 = webSettingGroupWithEnums.GroupItems.Count > 0;
				if (flag2)
				{
					string arg = webSettingGroupWithEnums.GroupDataAttribute.Name ?? "";
					stringBuilder.AppendFormat("<h1>{0}</h1>", arg);
					string description = webSettingGroupWithEnums.GroupDataAttribute.Description;
					bool flag3 = !string.IsNullOrEmpty(description);
					if (flag3)
					{
						stringBuilder.AppendFormat("<p><i>{0}</i></p><br />", description);
					}
					foreach (WebSettingWithAttribute webSettingWithAttribute in webSettingGroupWithEnums.GroupItems)
					{
						string arg2 = webSettingWithAttribute.Name ?? "";
						string text = webSettingWithAttribute.SubGroup ?? "";
						bool flag4 = text.Length > 0;
						if (flag4)
						{
							bool flag5 = text.StartsWith("_");
							if (flag5)
							{
								text = text.Substring(1);
							}
							text += ": ";
						}
						stringBuilder.AppendFormat("<h3>{0}{1}</h3>", text, arg2);
						description = webSettingWithAttribute.Description;
						bool flag6 = !string.IsNullOrEmpty(description);
						if (flag6)
						{
							stringBuilder.AppendFormat("<p><i>{0}</i></p><br />", description.Replace("<", "[").Replace(">", "]"));
						}
						else
						{
							stringBuilder.AppendLine("<br />");
						}
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
