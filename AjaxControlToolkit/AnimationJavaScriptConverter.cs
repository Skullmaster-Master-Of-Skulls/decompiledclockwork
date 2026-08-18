using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

namespace AjaxControlToolkit
{
	// Token: 0x02000034 RID: 52
	internal class AnimationJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00006C58 File Offset: 0x00004E58
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new ReadOnlyCollection<Type>(new List<Type>(new Type[]
				{
					typeof(Animation)
				}));
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006C84 File Offset: 0x00004E84
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Animation animation = obj as Animation;
			if (animation != null)
			{
				return AnimationJavaScriptConverter.Serialize(animation);
			}
			return new Dictionary<string, object>();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006CA8 File Offset: 0x00004EA8
		private static IDictionary<string, object> Serialize(Animation animation)
		{
			if (animation == null)
			{
				throw new ArgumentNullException("animation");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["AnimationName"] = animation.Name;
			foreach (KeyValuePair<string, string> keyValuePair in animation.Properties)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
			List<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
			foreach (Animation animation2 in animation.Children)
			{
				list.Add(AnimationJavaScriptConverter.Serialize(animation2));
			}
			dictionary["AnimationChildren"] = list.ToArray();
			return dictionary;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006D8C File Offset: 0x00004F8C
		public override object Deserialize(IDictionary<string, object> dictionary, Type t, JavaScriptSerializer serializer)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (t == typeof(Animation) || t.IsSubclassOf(typeof(Animation)))
			{
				return AnimationJavaScriptConverter.Deserialize(dictionary);
			}
			return null;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006DC8 File Offset: 0x00004FC8
		private static Animation Deserialize(IDictionary<string, object> obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Animation animation = new Animation();
			if (!obj.ContainsKey("AnimationName"))
			{
				throw new InvalidOperationException("Cannot deserialize an Animation without an AnimationName property");
			}
			animation.Name = (obj["AnimationName"] as string);
			foreach (KeyValuePair<string, object> keyValuePair in obj)
			{
				if (string.Compare(keyValuePair.Key, "AnimationName", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(keyValuePair.Key, "AnimationChildren", StringComparison.OrdinalIgnoreCase) != 0)
				{
					animation.Properties.Add(keyValuePair.Key, (keyValuePair.Value != null) ? keyValuePair.Value.ToString() : null);
				}
			}
			if (obj.ContainsKey("AnimationChildren"))
			{
				ArrayList arrayList = obj["AnimationChildren"] as ArrayList;
				if (arrayList != null)
				{
					foreach (object obj2 in arrayList)
					{
						IDictionary<string, object> dictionary = obj2 as IDictionary<string, object>;
						if (dictionary != null)
						{
							animation.Children.Add(AnimationJavaScriptConverter.Deserialize(dictionary));
						}
					}
				}
			}
			return animation;
		}
	}
}
