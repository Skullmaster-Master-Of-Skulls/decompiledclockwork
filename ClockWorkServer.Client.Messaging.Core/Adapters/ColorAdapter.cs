using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace TechnoPro.ClockWorkServer.Client.Messaging.Core.Adapters
{
	// Token: 0x02000007 RID: 7
	public static class ColorAdapter
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002710 File Offset: 0x00000910
		public static IList<Color> GetColors()
		{
			List<Color> list = new List<Color>();
			foreach (PropertyInfo propertyInfo in typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.Public))
			{
				if (propertyInfo.PropertyType == typeof(Color) && propertyInfo.Name != "Transparent")
				{
					list.Add(Color.FromName(propertyInfo.Name));
				}
			}
			return list;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002784 File Offset: 0x00000984
		public static Color GetRandomColor()
		{
			Random random = new Random();
			IList<Color> colors = ColorAdapter.GetColors();
			return colors[random.Next(0, colors.Count - 1)];
		}
	}
}
