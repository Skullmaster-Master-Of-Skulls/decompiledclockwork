using System;
using System.Collections.Generic;
using System.Text;
using NLog.Config;
using NLog.LayoutRenderers.Wrappers;

namespace NLog.Layouts
{
	// Token: 0x02000116 RID: 278
	[ThreadAgnostic]
	[Layout("JsonLayout")]
	[AppDomainFixedOutput]
	public class JsonLayout : Layout
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x00010D6A File Offset: 0x0000EF6A
		public JsonLayout()
		{
			this.Attributes = new List<JsonAttribute>();
			this.RenderEmptyObject = true;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00010D84 File Offset: 0x0000EF84
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x00010D8C File Offset: 0x0000EF8C
		[ArrayParameter(typeof(JsonAttribute), "attribute")]
		public IList<JsonAttribute> Attributes { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00010D95 File Offset: 0x0000EF95
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x00010D9D File Offset: 0x0000EF9D
		public bool SuppressSpaces { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00010DA6 File Offset: 0x0000EFA6
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x00010DAE File Offset: 0x0000EFAE
		public bool RenderEmptyObject { get; set; }

		// Token: 0x060007BB RID: 1979 RVA: 0x00010DB8 File Offset: 0x0000EFB8
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			JsonEncodeLayoutRendererWrapper jsonEncodeLayoutRendererWrapper = new JsonEncodeLayoutRendererWrapper();
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			bool flag2 = false;
			for (int i = 0; i < this.Attributes.Count; i++)
			{
				JsonAttribute jsonAttribute = this.Attributes[i];
				jsonEncodeLayoutRendererWrapper.Inner = jsonAttribute.Layout;
				jsonEncodeLayoutRendererWrapper.JsonEncode = jsonAttribute.Encode;
				string text = jsonEncodeLayoutRendererWrapper.Render(logEvent);
				if (!string.IsNullOrEmpty(text))
				{
					if (!flag)
					{
						stringBuilder.Append(",");
						JsonLayout.AppendIf<string>(!this.SuppressSpaces, stringBuilder, " ");
					}
					flag = false;
					string format;
					if (jsonAttribute.Encode)
					{
						format = "\"{0}\":{1}\"{2}\"";
					}
					else
					{
						format = "\"{0}\":{1}{2}";
					}
					stringBuilder.AppendFormat(format, jsonAttribute.Name, (!this.SuppressSpaces) ? " " : "", text);
					flag2 = true;
				}
			}
			string str = stringBuilder.ToString();
			if (!flag2 && !this.RenderEmptyObject)
			{
				return string.Empty;
			}
			if (this.SuppressSpaces)
			{
				return "{" + str + "}";
			}
			return "{ " + str + " }";
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00010EDA File Offset: 0x0000F0DA
		private static void AppendIf<T>(bool condition, StringBuilder stringBuilder, T objectToAppend)
		{
			if (condition)
			{
				stringBuilder.Append(objectToAppend);
			}
		}
	}
}
