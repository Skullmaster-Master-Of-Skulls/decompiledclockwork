using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002FE RID: 766
	internal class ColumnConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001A35 RID: 6709 RVA: 0x0005524A File Offset: 0x0005344A
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00055254 File Offset: 0x00053454
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			GanttBoundColumn ganttBoundColumn = (GanttBoundColumn)obj;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			string text = "javascript:";
			if (!string.IsNullOrEmpty(ganttBoundColumn.HeaderText))
			{
				dictionary["title"] = ganttBoundColumn.HeaderText;
			}
			if (!ganttBoundColumn.AllowSorting)
			{
				dictionary["sortable"] = false;
			}
			if (!ganttBoundColumn.AllowEdit)
			{
				dictionary["editable"] = false;
			}
			if (ganttBoundColumn.Width != 150)
			{
				dictionary["width"] = ganttBoundColumn.Width.Value;
			}
			if (!string.IsNullOrEmpty(ganttBoundColumn.DataFormatString))
			{
				dictionary["format"] = string.Format("{{0:{0}}}", ganttBoundColumn.DataFormatString);
			}
			if (ganttBoundColumn.ClientTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(dictionary, "template", ganttBoundColumn.ClientTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "template", ganttBoundColumn.ClientTemplate, "");
			}
			if (ganttBoundColumn.ClientHeaderTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(dictionary, "headerTemplate", ganttBoundColumn.ClientHeaderTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "headerTemplate", ganttBoundColumn.ClientHeaderTemplate, "");
			}
			dictionary["field"] = StringHelpers.ToCamelCase(ganttBoundColumn.DataField);
			return dictionary;
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x000553D8 File Offset: 0x000535D8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(GanttBoundColumn)
				};
			}
		}
	}
}
