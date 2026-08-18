using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;

namespace TechnoPro.Common.Core.MailMerging.Output
{
	// Token: 0x020000CD RID: 205
	public class HtmlMailMergeOutputManager : TextMailMergeOutputManager
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x00036242 File Offset: 0x00034442
		public HtmlMailMergeOutputManager(MailMergeOutputOperationContext opContext) : base(opContext)
		{
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0003624D File Offset: 0x0003444D
		protected override void OutputImage(MailMergeCode code, byte[] imageData, MailMergeValueFormat valueFormat)
		{
			base.OutputImage(code, imageData, valueFormat);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0003625C File Offset: 0x0003445C
		protected override void OutputStringList(MailMergeCode code, IList<string> list, TempCache tempCache, MailMergeValueFormat valueFormat = null)
		{
			bool flag = valueFormat == null;
			if (flag)
			{
				valueFormat = MailMergeValueFormat.DefaultMailMergeValueFormat;
			}
			TempCacheObject tempCacheObject = null;
			string itemStart;
			string itemEnd;
			string str;
			string str2;
			string separator;
			switch (valueFormat.ValueFormatType)
			{
			case eValueFormatType.BulletedList:
				str = "<ul> ";
				str2 = " </ul>";
				itemStart = "<li>";
				itemEnd = "</li>";
				separator = "";
				goto IL_13A;
			case eValueFormatType.NumberedList:
			{
				bool flag2 = !string.IsNullOrEmpty(valueFormat.CustomFormat);
				if (flag2)
				{
					str = "";
					str2 = "";
					itemStart = "{ctr}. ";
					itemEnd = "";
					separator = "<br />";
					tempCacheObject = (tempCache.ContainsKey(valueFormat.CustomFormat) ? tempCache[valueFormat.CustomFormat] : tempCache.AddLocalItem(valueFormat.CustomFormat, 1));
				}
				else
				{
					str = "<ol> ";
					str2 = " </ol>";
					itemStart = "<li> ";
					itemEnd = " </li>";
					separator = "";
				}
				goto IL_13A;
			}
			}
			str = "";
			str2 = "";
			itemStart = "";
			itemEnd = "";
			separator = ",";
			IL_13A:
			int ctr = ((int?)((tempCacheObject != null) ? tempCacheObject.Object : null)) ?? 1;
			string text = str + string.Join(separator, list.Select(delegate(string g)
			{
				string itemStart = itemStart;
				string oldValue = "{ctr}";
				int ctr = ctr;
				ctr++;
				return itemStart.Replace(oldValue, ctr.ToString()) + (g ?? "") + itemEnd;
			}).ToArray<string>()) + str2;
			bool flag3 = tempCacheObject != null;
			if (flag3)
			{
				tempCacheObject.Object = ctr;
			}
			this.OutputString(code, text, null);
		}
	}
}
