using System;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200003A RID: 58
	public class ConfigurationSection : ConfigurationElement
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x0000716A File Offset: 0x0000616A
		protected internal ConfigurationSection()
		{
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00007172 File Offset: 0x00006172
		public bool IsLocked
		{
			get
			{
				return (bool)base.AppHostElement.GetMetadata("isLocked");
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000718C File Offset: 0x0000618C
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x000071C5 File Offset: 0x000061C5
		public OverrideMode OverrideMode
		{
			get
			{
				string value = (string)base.AppHostElement.GetMetadata("overrideMode");
				return (OverrideMode)Enum.Parse(typeof(OverrideMode), value, true);
			}
			set
			{
				base.SetDirty();
				base.AppHostElement.SetMetadata("overrideMode", Enum.GetName(typeof(OverrideMode), value));
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000071F4 File Offset: 0x000061F4
		public OverrideMode OverrideModeEffective
		{
			get
			{
				string value = (string)base.AppHostElement.GetMetadata("effectiveOverrideMode");
				return (OverrideMode)Enum.Parse(typeof(OverrideMode), value, true);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000722D File Offset: 0x0000622D
		public string SectionPath
		{
			get
			{
				return this._sectionPath;
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007235 File Offset: 0x00006235
		public void RevertToParent()
		{
			base.SetDirty();
			base.AppHostElement.Clear();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007248 File Offset: 0x00006248
		internal void SetSectionPath(string sectionPath)
		{
			this._sectionPath = sectionPath;
		}

		// Token: 0x040000A4 RID: 164
		private string _sectionPath;
	}
}
