using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000484 RID: 1156
	internal class ColorConverter : JavaScriptConverter
	{
		// Token: 0x06002945 RID: 10565 RVA: 0x00085330 File Offset: 0x00083530
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			if (dictionary == null)
			{
				throw new PersistenceFrameworkArgumentNullException("dictionary");
			}
			if (object.ReferenceEquals(type, typeof(Color)) && dictionary.Count > 0)
			{
				Color color = Color.FromArgb((int)dictionary["alpha"], (int)dictionary["red"], (int)dictionary["green"], (int)dictionary["blue"]);
				return color;
			}
			return null;
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000853B4 File Offset: 0x000835B4
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Color color = (Color)obj;
			return new Dictionary<string, object>
			{
				{
					"alpha",
					color.A
				},
				{
					"red",
					color.R
				},
				{
					"green",
					color.G
				},
				{
					"blue",
					color.B
				}
			};
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x0008542C File Offset: 0x0008362C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new ReadOnlyCollection<Type>(new List<Type>(new Type[]
				{
					typeof(Color)
				}));
			}
		}
	}
}
