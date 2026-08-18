using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000987 RID: 2439
	internal class WindowWaiAriaSettingsConverter : WaiAriaSettingsConverter
	{
		// Token: 0x06005D25 RID: 23845 RVA: 0x0011C630 File Offset: 0x0011A830
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			WindowWaiAriaSettings windowWaiAriaSettings = obj as WindowWaiAriaSettings;
			if (windowWaiAriaSettings == null)
			{
				throw new InvalidOperationException("Can serialize only WindowWaiAriaSettings objects.");
			}
			Dictionary<string, object> dictionary = (Dictionary<string, object>)base.Serialize(obj, serializer);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "aria-labelledby", windowWaiAriaSettings.LabelledBy, string.Empty);
			return dictionary;
		}

		// Token: 0x17001EB8 RID: 7864
		// (get) Token: 0x06005D26 RID: 23846 RVA: 0x0011C678 File Offset: 0x0011A878
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(WindowWaiAriaSettings)
				};
			}
		}
	}
}
