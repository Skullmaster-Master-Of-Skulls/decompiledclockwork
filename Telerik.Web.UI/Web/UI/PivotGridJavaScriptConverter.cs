using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000DF7 RID: 3575
	public class PivotGridJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x060084C5 RID: 33989 RVA: 0x001E4905 File Offset: 0x001E2B05
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060084C6 RID: 33990 RVA: 0x001E490C File Offset: 0x001E2B0C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			PivotGridClientSettings pivotGridClientSettings = obj as PivotGridClientSettings;
			if (pivotGridClientSettings != null)
			{
				dictionary.Add("_scrolling", pivotGridClientSettings.Scrolling);
				dictionary.Add("_clientMessages", pivotGridClientSettings.ClientMessages);
				dictionary.Add("_resizing", pivotGridClientSettings.Resizing);
			}
			PivotGridScrolling pivotGridScrolling = obj as PivotGridScrolling;
			if (pivotGridScrolling != null)
			{
				dictionary.Add("_allowVerticalScroll", pivotGridScrolling.AllowVerticalScroll);
				if (pivotGridScrolling.AllowVerticalScroll)
				{
					dictionary.Add("_scrollHeight", pivotGridScrolling.ScrollHeight);
				}
				if (pivotGridScrolling.ShouldSerializeScrollTop)
				{
					dictionary.Add("_scrollTop", pivotGridScrolling.ScrollTop);
				}
				if (pivotGridScrolling.ShouldSerializeScrollLeft)
				{
					dictionary.Add("_scrollLeft", pivotGridScrolling.ScrollLeft);
				}
				if (pivotGridScrolling.ShouldSerializeSaveScrollPosition)
				{
					dictionary.Add("_saveScrollPosition", pivotGridScrolling.SaveScrollPosition);
				}
			}
			PivotGridClientMessages pivotGridClientMessages = obj as PivotGridClientMessages;
			if (pivotGridClientMessages != null && pivotGridClientMessages.DragToReorder != "Drag to reorder")
			{
				dictionary.Add("_dragToReorder", pivotGridClientMessages.DragToReorder);
			}
			PivotGridResizing pivotGridResizing = obj as PivotGridResizing;
			if (pivotGridResizing != null)
			{
				if (pivotGridResizing.AllowColumnResize)
				{
					dictionary.Add("_allowColumnResize", pivotGridResizing.AllowColumnResize);
				}
				if (pivotGridResizing.EnableRealTimeResize)
				{
					dictionary.Add("_enableRealTimeResize", pivotGridResizing.EnableRealTimeResize);
				}
			}
			return dictionary;
		}

		// Token: 0x170029FD RID: 10749
		// (get) Token: 0x060084C7 RID: 33991 RVA: 0x001E4BA4 File Offset: 0x001E2DA4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(PivotGridClientSettings);
				yield return typeof(PivotGridScrolling);
				yield return typeof(PivotGridClientMessages);
				yield return typeof(PivotGridResizing);
				yield break;
			}
		}
	}
}
