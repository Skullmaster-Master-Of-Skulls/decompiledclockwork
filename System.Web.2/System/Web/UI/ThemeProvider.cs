using System;
using System.Collections;
using System.ComponentModel.Design;

namespace System.Web.UI
{
	// Token: 0x020002FE RID: 766
	public sealed class ThemeProvider
	{
		// Token: 0x0600235C RID: 9052 RVA: 0x00073698 File Offset: 0x00071898
		public ThemeProvider(IDesignerHost host, string name, string themeDefinition, string[] cssFiles, string themePath)
		{
			this._themeName = name;
			this._themePath = themePath;
			this._cssFiles = cssFiles;
			this._host = host;
			ControlBuilder controlBuilder = DesignTimeTemplateParser.ParseTheme(host, themeDefinition, themePath);
			this._contentHashCode = themeDefinition.GetHashCode();
			ArrayList subBuilders = controlBuilder.SubBuilders;
			this._skinBuilders = new Hashtable();
			for (int i = 0; i < subBuilders.Count; i++)
			{
				ControlBuilder controlBuilder2 = subBuilders[i] as ControlBuilder;
				if (controlBuilder2 != null)
				{
					IDictionary dictionary = this._skinBuilders[controlBuilder2.ControlType] as IDictionary;
					if (dictionary == null)
					{
						dictionary = new SortedList(StringComparer.OrdinalIgnoreCase);
						this._skinBuilders[controlBuilder2.ControlType] = dictionary;
					}
					Control control = controlBuilder2.BuildObject() as Control;
					if (control != null)
					{
						dictionary[control.SkinID] = controlBuilder2;
					}
				}
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x0007376D File Offset: 0x0007196D
		public int ContentHashCode
		{
			get
			{
				return this._contentHashCode;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x00073775 File Offset: 0x00071975
		public ICollection CssFiles
		{
			get
			{
				return this._cssFiles;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x0007377D File Offset: 0x0007197D
		public IDesignerHost DesignerHost
		{
			get
			{
				return this._host;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x00073785 File Offset: 0x00071985
		public string ThemeName
		{
			get
			{
				return this._themeName;
			}
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x00073790 File Offset: 0x00071990
		public ICollection GetSkinsForControl(Type type)
		{
			IDictionary dictionary = this._skinBuilders[type] as IDictionary;
			if (dictionary == null)
			{
				return new ArrayList();
			}
			return dictionary.Keys;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000737C0 File Offset: 0x000719C0
		public SkinBuilder GetSkinBuilder(Control control)
		{
			IDictionary dictionary = this._skinBuilders[control.GetType()] as IDictionary;
			if (dictionary == null)
			{
				return null;
			}
			ControlBuilder controlBuilder = dictionary[control.SkinID] as ControlBuilder;
			if (controlBuilder == null)
			{
				return null;
			}
			return new SkinBuilder(this, control, controlBuilder, this._themePath);
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x00073810 File Offset: 0x00071A10
		public IDictionary GetSkinControlBuildersForControlType(Type type)
		{
			return this._skinBuilders[type] as IDictionary;
		}

		// Token: 0x04001CB9 RID: 7353
		private IDictionary _skinBuilders;

		// Token: 0x04001CBA RID: 7354
		private string[] _cssFiles;

		// Token: 0x04001CBB RID: 7355
		private string _themeName;

		// Token: 0x04001CBC RID: 7356
		private string _themePath;

		// Token: 0x04001CBD RID: 7357
		private int _contentHashCode;

		// Token: 0x04001CBE RID: 7358
		private IDesignerHost _host;
	}
}
