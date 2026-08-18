using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000994 RID: 2452
	internal class WizardStepJavaScriptConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06005D41 RID: 23873 RVA: 0x0011C75E File Offset: 0x0011A95E
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x0011C768 File Offset: 0x0011A968
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadWizardStep radWizardStep = obj as RadWizardStep;
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("id", radWizardStep.ClientID);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "causesValidation", radWizardStep.CausesValidation, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "allowReturn", radWizardStep.AllowReturn, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "enabled", radWizardStep.Enabled, true);
			if (radWizardStep.ResolvedDisplayCancelButton)
			{
				dictionary.Add("displayCancelButton", radWizardStep.ResolvedDisplayCancelButton);
			}
			if (!string.IsNullOrEmpty(radWizardStep.ValidationGroup))
			{
				dictionary.Add("validationGroup", radWizardStep.ValidationGroup);
			}
			if (radWizardStep.Wizard != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "stepType", radWizardStep.Wizard.GetStepType(radWizardStep), RadWizardStepType.Auto);
			}
			if (!string.IsNullOrEmpty(radWizardStep.ImageUrl))
			{
				dictionary.Add("imageUrl", radWizardStep.ResolveClientUrl(radWizardStep.ImageUrl));
			}
			if (!string.IsNullOrEmpty(radWizardStep.ActiveImageUrl))
			{
				dictionary.Add("activeImageUrl", radWizardStep.ResolveClientUrl(radWizardStep.ActiveImageUrl));
			}
			if (!string.IsNullOrEmpty(radWizardStep.HoveredImageUrl))
			{
				dictionary.Add("hoveredImageUrl", radWizardStep.ResolveClientUrl(radWizardStep.HoveredImageUrl));
			}
			if (!string.IsNullOrEmpty(radWizardStep.DisabledImageUrl))
			{
				dictionary.Add("disabledImageUrl", radWizardStep.ResolveClientUrl(radWizardStep.DisabledImageUrl));
			}
			if (!string.IsNullOrEmpty(radWizardStep.CssClass))
			{
				dictionary.Add("cssClass", radWizardStep.CssClass);
			}
			return dictionary;
		}

		// Token: 0x17001EC0 RID: 7872
		// (get) Token: 0x06005D43 RID: 23875 RVA: 0x0011C9C8 File Offset: 0x0011ABC8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RadWizardStep);
				yield break;
			}
		}
	}
}
