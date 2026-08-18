using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001ACB RID: 6859
	public class StyleSheetReference
	{
		// Token: 0x170050C8 RID: 20680
		// (get) Token: 0x060109DA RID: 68058 RVA: 0x003B5160 File Offset: 0x003B3360
		// (set) Token: 0x060109DB RID: 68059 RVA: 0x003B5168 File Offset: 0x003B3368
		public string Name { get; set; }

		// Token: 0x170050C9 RID: 20681
		// (get) Token: 0x060109DC RID: 68060 RVA: 0x003B5171 File Offset: 0x003B3371
		// (set) Token: 0x060109DD RID: 68061 RVA: 0x003B5179 File Offset: 0x003B3379
		public string Assembly { get; set; }

		// Token: 0x170050CA RID: 20682
		// (get) Token: 0x060109DE RID: 68062 RVA: 0x003B5182 File Offset: 0x003B3382
		// (set) Token: 0x060109DF RID: 68063 RVA: 0x003B518A File Offset: 0x003B338A
		public int OrderIndex { get; set; }

		// Token: 0x170050CB RID: 20683
		// (get) Token: 0x060109E0 RID: 68064 RVA: 0x003B5193 File Offset: 0x003B3393
		// (set) Token: 0x060109E1 RID: 68065 RVA: 0x003B519B File Offset: 0x003B339B
		[UrlProperty]
		public string Path { get; set; }

		// Token: 0x170050CC RID: 20684
		// (get) Token: 0x060109E2 RID: 68066 RVA: 0x003B51A4 File Offset: 0x003B33A4
		// (set) Token: 0x060109E3 RID: 68067 RVA: 0x003B51AC File Offset: 0x003B33AC
		public bool IsCommonCss { get; set; }

		// Token: 0x170050CD RID: 20685
		// (get) Token: 0x060109E4 RID: 68068 RVA: 0x003B51B5 File Offset: 0x003B33B5
		// (set) Token: 0x060109E5 RID: 68069 RVA: 0x003B51BD File Offset: 0x003B33BD
		public bool IsRequiredCss { get; set; }

		// Token: 0x060109E6 RID: 68070 RVA: 0x003B51C6 File Offset: 0x003B33C6
		public StyleSheetReference()
		{
			this.EnableSecurePathDetection = true;
		}

		// Token: 0x060109E7 RID: 68071 RVA: 0x003B51D5 File Offset: 0x003B33D5
		public StyleSheetReference(string name, string assembly) : this()
		{
			this.Name = name;
			this.Assembly = assembly;
		}

		// Token: 0x170050CE RID: 20686
		// (get) Token: 0x060109E8 RID: 68072 RVA: 0x003B51EB File Offset: 0x003B33EB
		// (set) Token: 0x060109E9 RID: 68073 RVA: 0x003B51F3 File Offset: 0x003B33F3
		internal bool EnableSecurePathDetection { get; set; }

		// Token: 0x060109EA RID: 68074 RVA: 0x003B51FC File Offset: 0x003B33FC
		internal ScriptEntry GetScriptEntry()
		{
			if (!string.IsNullOrEmpty(this.Assembly) && string.IsNullOrEmpty(this.Name))
			{
				throw new InvalidOperationException("Assembly cannot be defined without Name.");
			}
			if (string.IsNullOrEmpty(this.Assembly) && !string.IsNullOrEmpty(this.Name))
			{
				throw new InvalidOperationException("Name cannot be defined without Assembly.");
			}
			if (this._scriptEntry == null)
			{
				if (string.IsNullOrEmpty(this.Path))
				{
					this._scriptEntry = new ScriptEntry(this.Assembly, this.Name, null);
				}
				else
				{
					try
					{
						string path = this.EnableSecurePathDetection ? ExternalStyleSheetUtils.ResolveSecurePath(this.Path) : this.Path;
						this._scriptEntry = new ExternalStyleSheetEntry(path);
					}
					catch (TypeInitializationException ex)
					{
						throw ex.InnerException;
					}
				}
			}
			return this._scriptEntry;
		}

		// Token: 0x04004A3D RID: 19005
		private ScriptEntry _scriptEntry;
	}
}
