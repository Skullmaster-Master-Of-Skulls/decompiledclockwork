using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200007B RID: 123
	internal class RadButtonConfirmSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadButtonConfirmSettings radButtonConfirmSettings = obj as RadButtonConfirmSettings;
			if (radButtonConfirmSettings == null)
			{
				throw new InvalidOperationException("Can serialize only RadButtonConfirmSettings objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!string.IsNullOrEmpty(radButtonConfirmSettings.ConfirmText))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "confirmText", radButtonConfirmSettings.ConfirmText, string.Empty);
				ExplicitJavaScriptConverter.AddProperty(dictionary, "confirmTitle", radButtonConfirmSettings.Title, string.Empty);
				if (!radButtonConfirmSettings.UseRadConfirm)
				{
					ExplicitJavaScriptConverter.AddProperty(dictionary, "useRadConfirm", radButtonConfirmSettings.UseRadConfirm, true);
				}
				if (radButtonConfirmSettings.Width > 0)
				{
					ExplicitJavaScriptConverter.AddProperty(dictionary, "width", radButtonConfirmSettings.Width, null);
				}
				if (radButtonConfirmSettings.Height > 0)
				{
					ExplicitJavaScriptConverter.AddProperty(dictionary, "height", radButtonConfirmSettings.Height, null);
				}
			}
			return dictionary;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000CA70 File Offset: 0x0000AC70
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadButtonConfirmSettings)
				};
			}
		}
	}
}
