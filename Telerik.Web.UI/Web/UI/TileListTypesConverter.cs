using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200090B RID: 2315
	internal class TileListTypesConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06005789 RID: 22409 RVA: 0x0010B7DC File Offset: 0x001099DC
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			TileBadge tileBadge = obj as TileBadge;
			if (tileBadge != null)
			{
				return this.SerializeTileBadge(tileBadge);
			}
			TileTitle tileTitle = obj as TileTitle;
			if (tileTitle != null)
			{
				return this.SerializeTileTitle(tileTitle);
			}
			TilePeekTemplateSettings tilePeekTemplateSettings = obj as TilePeekTemplateSettings;
			if (tilePeekTemplateSettings != null)
			{
				return this.SerializeTilePeekTemplateSettings(tilePeekTemplateSettings);
			}
			throw new InvalidOperationException("Can serialize only TileBadge, TileTitle, and TilePeekTemplateSettings objects.");
		}

		// Token: 0x17001CF6 RID: 7414
		// (get) Token: 0x0600578A RID: 22410 RVA: 0x0010B82C File Offset: 0x00109A2C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(TileBadge),
					typeof(TileTitle),
					typeof(TilePeekTemplateSettings)
				};
			}
		}

		// Token: 0x0600578B RID: 22411 RVA: 0x0010B868 File Offset: 0x00109A68
		private IDictionary<string, object> SerializeTileBadge(TileBadge tileBadge)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (tileBadge.Value != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "value", tileBadge.Value, null);
			}
			if (tileBadge.PredefinedType != TileBadgeType.None)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "predefinedType", tileBadge.PredefinedType, TileBadgeType.None);
			}
			if (!string.IsNullOrEmpty(tileBadge.ImageUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "imageUrl", tileBadge.ImageUrl, "");
			}
			return dictionary;
		}

		// Token: 0x0600578C RID: 22412 RVA: 0x0010B8EC File Offset: 0x00109AEC
		private IDictionary<string, object> SerializeTileTitle(TileTitle tileTitle)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!string.IsNullOrEmpty(tileTitle.Text))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "text", tileTitle.Text, "");
			}
			if (!string.IsNullOrEmpty(tileTitle.ImageUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "imageUrl", tileTitle.ImageUrl, "");
			}
			return dictionary;
		}

		// Token: 0x0600578D RID: 22413 RVA: 0x0010B948 File Offset: 0x00109B48
		private IDictionary<string, object> SerializeTilePeekTemplateSettings(TilePeekTemplateSettings peekTemplateSettings)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showPeekTemplateOnMouseOver", peekTemplateSettings.ShowPeekTemplateOnMouseOver, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "hidePeekTemplateOnMouseOut", peekTemplateSettings.HidePeekTemplateOnMouseOut, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "animation", peekTemplateSettings.Animation, PeekTemplateAnimation.Fade);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "animationDuration", peekTemplateSettings.AnimationDuration, 500);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showInterval", peekTemplateSettings.ShowInterval, 10000);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "closeDelay", peekTemplateSettings.CloseDelay, 7000);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "easing", peekTemplateSettings.Easing, "");
			return dictionary;
		}
	}
}
