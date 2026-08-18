using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200056D RID: 1389
	internal class LightBoxJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x060031D7 RID: 12759 RVA: 0x000A366D File Offset: 0x000A186D
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000A3674 File Offset: 0x000A1874
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			LightBoxClientSettings lightBoxClientSettings = obj as LightBoxClientSettings;
			if (lightBoxClientSettings != null)
			{
				LightBoxAnimationSettings animationSettings = lightBoxClientSettings.AnimationSettings;
				if (animationSettings != null)
				{
					if (animationSettings.ShowAnimation != LightBoxAnimationType.None)
					{
						dictionary.Add("_showAnimation", animationSettings.ShowAnimation);
					}
					if (animationSettings.ShowAnimationSettings != null)
					{
						if (animationSettings.ShowAnimationSettings.Easing != LightBoxEasingType.Linear)
						{
							dictionary.Add("_showAnimationEasing", animationSettings.ShowAnimationSettings.Easing);
						}
						if (animationSettings.ShowAnimationSettings.Speed != 400)
						{
							dictionary.Add("_showAnimationSpeed", animationSettings.ShowAnimationSettings.Speed);
						}
					}
					if (animationSettings.HideAnimation != LightBoxAnimationType.None)
					{
						dictionary.Add("_hideAnimation", animationSettings.HideAnimation);
					}
					if (animationSettings.HideAnimationSettings != null)
					{
						if (animationSettings.HideAnimationSettings.Easing != LightBoxEasingType.Linear)
						{
							dictionary.Add("_hideAnimationEasing", animationSettings.HideAnimationSettings.Easing);
						}
						if (animationSettings.HideAnimationSettings.Speed != 400)
						{
							dictionary.Add("_hideAnimationSpeed", animationSettings.HideAnimationSettings.Speed);
						}
					}
					if (animationSettings.PrevAnimation != LightBoxAnimationType.None)
					{
						dictionary.Add("_prevAnimation", animationSettings.PrevAnimation);
					}
					if (animationSettings.PrevAnimationSettings != null)
					{
						if (animationSettings.PrevAnimationSettings.Easing != LightBoxEasingType.Linear)
						{
							dictionary.Add("_prevAnimationEasing", animationSettings.PrevAnimationSettings.Easing);
						}
						if (animationSettings.PrevAnimationSettings.Speed != 400)
						{
							dictionary.Add("_prevAnimationSpeed", animationSettings.PrevAnimationSettings.Speed);
						}
					}
					if (animationSettings.NextAnimation != LightBoxAnimationType.None)
					{
						dictionary.Add("_nextAnimation", animationSettings.NextAnimation);
					}
					if (animationSettings.NextAnimationSettings != null)
					{
						if (animationSettings.NextAnimationSettings.Easing != LightBoxEasingType.Linear)
						{
							dictionary.Add("_nextAnimationEasing", animationSettings.NextAnimationSettings.Easing);
						}
						if (animationSettings.NextAnimationSettings.Speed != 400)
						{
							dictionary.Add("_nextAnimationSpeed", animationSettings.NextAnimationSettings.Speed);
						}
					}
				}
				if (!string.IsNullOrEmpty(lightBoxClientSettings.DataBinding.ItemTemplate))
				{
					dictionary.Add("_itemTemplate", lightBoxClientSettings.DataBinding.ItemTemplate);
				}
				if (!string.IsNullOrEmpty(lightBoxClientSettings.DataBinding.DescriptionTemplate))
				{
					dictionary.Add("_descriptionTemplate", lightBoxClientSettings.DataBinding.DescriptionTemplate);
				}
				if (lightBoxClientSettings.AllowKeyboardNavigation)
				{
					dictionary.Add("_allowKeyboardNavigation", lightBoxClientSettings.AllowKeyboardNavigation);
				}
				if (lightBoxClientSettings.PreventOverlayClose)
				{
					dictionary.Add("_preventOverlayClose", lightBoxClientSettings.PreventOverlayClose);
				}
				if (!lightBoxClientSettings.ShowItemsCounter)
				{
					dictionary.Add("_showItemsCounter", lightBoxClientSettings.ShowItemsCounter);
				}
				if (!lightBoxClientSettings.AutoResize)
				{
					dictionary.Add("_autoResize", lightBoxClientSettings.AutoResize);
				}
				if (lightBoxClientSettings.ContentResizeMode != LightBoxContentResizeMode.Fit)
				{
					dictionary.Add("_contentResizeMode", lightBoxClientSettings.ContentResizeMode);
				}
				if (lightBoxClientSettings.NavigationMode != LightBoxNavigationMode.Button)
				{
					dictionary.Add("_navigationMode", lightBoxClientSettings.NavigationMode);
				}
				if (lightBoxClientSettings.FullscreenMode != LightBoxFullscreenMode.Emulation)
				{
					dictionary.Add("_fullscreenMode", lightBoxClientSettings.FullscreenMode);
				}
			}
			return dictionary;
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x060031D9 RID: 12761 RVA: 0x000A3AC4 File Offset: 0x000A1CC4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(LightBoxClientSettings);
				yield return typeof(LightBoxClientEvents);
				yield return typeof(LightBoxAnimationSettings);
				yield break;
			}
		}
	}
}
