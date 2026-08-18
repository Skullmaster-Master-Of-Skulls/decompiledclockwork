using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Telerik.Charting.Styles.Skins
{
	// Token: 0x020017C5 RID: 6085
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class ChartSkinsCollection : DictionaryBase
	{
		// Token: 0x0600ECC6 RID: 60614 RVA: 0x00360928 File Offset: 0x0035EB28
		internal ChartSkin GetSkin(string skinName)
		{
			ChartSkin chartSkin;
			if (base.Dictionary.Contains(skinName))
			{
				chartSkin = (base.Dictionary[skinName] as ChartSkin);
			}
			else
			{
				chartSkin = this.LoadSkinFromXml(skinName);
				base.Dictionary.Add(skinName, chartSkin);
			}
			return chartSkin;
		}

		// Token: 0x0600ECC7 RID: 60615 RVA: 0x00360970 File Offset: 0x0035EB70
		private ChartSkin LoadSkinFromXml(string skinName)
		{
			ChartSkin result = null;
			try
			{
				string @string = this.resourceManager.GetString(skinName);
				if (!string.IsNullOrEmpty(@string))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(@string);
					result = new ChartSkin(xmlDocument);
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x0600ECC8 RID: 60616 RVA: 0x003609C0 File Offset: 0x0035EBC0
		public List<string> GetNames()
		{
			return this.skinNames;
		}

		// Token: 0x1700479D RID: 18333
		// (get) Token: 0x0600ECC9 RID: 60617 RVA: 0x003609C8 File Offset: 0x0035EBC8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(ChartSkinsCollection.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Telerik.Charting.Styles.ChartSkinsCollection", typeof(ChartSkinsCollection).Assembly);
					ChartSkinsCollection.resourceMan = resourceManager;
				}
				return ChartSkinsCollection.resourceMan;
			}
		}

		// Token: 0x1700479E RID: 18334
		// (get) Token: 0x0600ECCA RID: 60618 RVA: 0x00360A07 File Offset: 0x0035EC07
		// (set) Token: 0x0600ECCB RID: 60619 RVA: 0x00360A0E File Offset: 0x0035EC0E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return ChartSkinsCollection.resourceCulture;
			}
			set
			{
				ChartSkinsCollection.resourceCulture = value;
			}
		}

		// Token: 0x04004447 RID: 17479
		private readonly List<string> skinNames = new List<string>(new string[]
		{
			"BabyBlue",
			"Black",
			"Blue",
			"BlueStripes",
			"Brick",
			"Classic",
			"Colorful",
			"DeepBlue",
			"DeepGray",
			"DeepGreen",
			"DeepRed",
			"Default",
			"Default2006",
			"Desert",
			"ExcelClassic",
			"Forest",
			"Gradient",
			"Gray",
			"GrayStripes",
			"Green",
			"GreenStripes",
			"Hay",
			"Inox",
			"LightBlue",
			"LightBrown",
			"LightGreen",
			"Mac",
			"Marble",
			"Metal",
			"Office2007",
			"Outlook",
			"Pastel",
			"SkyBlue",
			"Sunset",
			"Telerik",
			"UltraGreen",
			"Vista",
			"Web",
			"Web20",
			"WebBlue",
			"Wood"
		});

		// Token: 0x04004448 RID: 17480
		private ComponentResourceManager resourceManager = new ComponentResourceManager(typeof(ChartSkinsCollection));

		// Token: 0x04004449 RID: 17481
		private static ResourceManager resourceMan;

		// Token: 0x0400444A RID: 17482
		private static CultureInfo resourceCulture;
	}
}
