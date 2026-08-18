using System;

namespace Telerik.Web
{
	// Token: 0x02000A47 RID: 2631
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class EmbeddedSkinAttribute : Attribute
	{
		// Token: 0x060065A6 RID: 26022 RVA: 0x0017CDB9 File Offset: 0x0017AFB9
		public EmbeddedSkinAttribute(string shortControlName, Type type) : this(shortControlName, null, type)
		{
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x0017CDC4 File Offset: 0x0017AFC4
		public EmbeddedSkinAttribute(string shortControlName) : this(shortControlName, null, typeof(EmbeddedSkinAttribute))
		{
		}

		// Token: 0x060065A8 RID: 26024 RVA: 0x0017CDD8 File Offset: 0x0017AFD8
		public EmbeddedSkinAttribute(string shortControlName, string skin) : this(shortControlName, skin, typeof(EmbeddedSkinAttribute))
		{
		}

		// Token: 0x060065A9 RID: 26025 RVA: 0x0017CDEC File Offset: 0x0017AFEC
		public EmbeddedSkinAttribute(string shortControlName, string skin, Type type)
		{
			this._shortControlName = shortControlName;
			this._skin = skin;
			this._type = type;
		}

		// Token: 0x17002174 RID: 8564
		// (get) Token: 0x060065AA RID: 26026 RVA: 0x0017CE09 File Offset: 0x0017B009
		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17002175 RID: 8565
		// (get) Token: 0x060065AB RID: 26027 RVA: 0x0017CE11 File Offset: 0x0017B011
		public string Skin
		{
			get
			{
				return this._skin;
			}
		}

		// Token: 0x17002176 RID: 8566
		// (get) Token: 0x060065AC RID: 26028 RVA: 0x0017CE19 File Offset: 0x0017B019
		public string ShortControlName
		{
			get
			{
				return this._shortControlName;
			}
		}

		// Token: 0x17002177 RID: 8567
		// (get) Token: 0x060065AD RID: 26029 RVA: 0x0017CE21 File Offset: 0x0017B021
		public bool IsCommonCss
		{
			get
			{
				return this._skin == null;
			}
		}

		// Token: 0x17002178 RID: 8568
		// (get) Token: 0x060065AE RID: 26030 RVA: 0x0017CE2C File Offset: 0x0017B02C
		public string CssResourceName
		{
			get
			{
				string format;
				string format2;
				if (this.AssemblyName == "Telerik.Web.UI")
				{
					format = "{0}.Skins.{1}{2}.css";
					format2 = "{0}.Skins.{1}{2}.{3}.{1}.css";
				}
				else
				{
					format = "{0}.{1}{2}.css";
					format2 = "{0}.{1}{2}.{3}.{1}.css";
				}
				if (this.IsCommonCss)
				{
					return string.Format(format, this.AssemblyName, this.ShortControlName, this.Suffix);
				}
				return string.Format(format2, new object[]
				{
					this.AssemblyName,
					this.Skin,
					this.Suffix,
					this.ShortControlName
				});
			}
		}

		// Token: 0x17002179 RID: 8569
		// (get) Token: 0x060065AF RID: 26031 RVA: 0x0017CEB8 File Offset: 0x0017B0B8
		private string AssemblyName
		{
			get
			{
				if (string.IsNullOrEmpty(this._assemblyName))
				{
					this._assemblyName = this.Type.Assembly.GetName().Name;
					if (this._assemblyName == "Telerik.Web.UI.Skins")
					{
						this._assemblyName = "Telerik.Web.UI";
					}
				}
				return this._assemblyName;
			}
		}

		// Token: 0x1700217A RID: 8570
		// (get) Token: 0x060065B0 RID: 26032 RVA: 0x0017CF10 File Offset: 0x0017B110
		// (set) Token: 0x060065B1 RID: 26033 RVA: 0x0017CF18 File Offset: 0x0017B118
		internal string Suffix { get; set; }

		// Token: 0x0400188C RID: 6284
		private string _assemblyName;

		// Token: 0x0400188D RID: 6285
		private readonly string _skin;

		// Token: 0x0400188E RID: 6286
		private readonly string _shortControlName;

		// Token: 0x0400188F RID: 6287
		private readonly Type _type;
	}
}
