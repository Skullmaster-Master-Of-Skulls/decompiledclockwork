using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001B3F RID: 6975
	internal class RadMenuItemGroupSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06010DCB RID: 69067 RVA: 0x003BDB00 File Offset: 0x003BBD00
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadMenuItemGroupSettings radMenuItemGroupSettings = obj as RadMenuItemGroupSettings;
			if (radMenuItemGroupSettings == null)
			{
				throw new InvalidOperationException("Can serialize only RadMenuItemGroupSettings objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (radMenuItemGroupSettings.IsFlowSet)
			{
				dictionary.Add("flow", radMenuItemGroupSettings.Flow);
			}
			if (radMenuItemGroupSettings.IsExpandDirectionSet)
			{
				dictionary.Add("expandDirection", radMenuItemGroupSettings.ExpandDirection);
			}
			if (radMenuItemGroupSettings.IsOffsetXSet)
			{
				dictionary.Add("offsetX", radMenuItemGroupSettings.OffsetX);
			}
			if (radMenuItemGroupSettings.IsOffsetYSet)
			{
				dictionary.Add("offsetY", radMenuItemGroupSettings.OffsetY);
			}
			if (radMenuItemGroupSettings.IsWidthSet)
			{
				dictionary.Add("width", radMenuItemGroupSettings.Width.ToString());
			}
			if (radMenuItemGroupSettings.IsHeightSet)
			{
				dictionary.Add("height", radMenuItemGroupSettings.Height.ToString());
			}
			if (radMenuItemGroupSettings.IsRepeatColumnsSet)
			{
				dictionary.Add("repeatColumns", radMenuItemGroupSettings.RepeatColumns);
			}
			if (radMenuItemGroupSettings.IsRepeatDirectionSet)
			{
				dictionary.Add("repeatDirection", radMenuItemGroupSettings.RepeatDirection);
			}
			return dictionary;
		}

		// Token: 0x17005233 RID: 21043
		// (get) Token: 0x06010DCC RID: 69068 RVA: 0x003BDC2C File Offset: 0x003BBE2C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadMenuItemGroupSettings)
				};
			}
		}
	}
}
