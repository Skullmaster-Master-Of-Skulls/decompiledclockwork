using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x0200081C RID: 2076
	internal class AdjustmentRuleConverter : JavaScriptConverter
	{
		// Token: 0x06004CB1 RID: 19633 RVA: 0x000F1096 File Offset: 0x000EF296
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x000F10A0 File Offset: 0x000EF2A0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			TimeZoneInfo.AdjustmentRule adjustmentRule = obj as TimeZoneInfo.AdjustmentRule;
			if (adjustmentRule == null)
			{
				throw new ArgumentException("Can serialize only AdjustmentRule objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["start"] = adjustmentRule.DateStart;
			dictionary["end"] = adjustmentRule.DateEnd;
			dictionary["daylightDelta"] = adjustmentRule.DaylightDelta.TotalMilliseconds;
			dictionary["transitionStart"] = adjustmentRule.DaylightTransitionStart;
			dictionary["transitionEnd"] = adjustmentRule.DaylightTransitionEnd;
			return dictionary;
		}

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x06004CB3 RID: 19635 RVA: 0x000F1140 File Offset: 0x000EF340
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(TimeZoneInfo.AdjustmentRule)
				};
			}
		}

		// Token: 0x0400133E RID: 4926
		public const string JavaScriptDateFormat = "yyyy/MM/dd HH:mm";
	}
}
