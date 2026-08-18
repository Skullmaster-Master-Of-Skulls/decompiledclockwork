using System;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200006A RID: 106
	public sealed class SectionDefinition
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x000076F5 File Offset: 0x000066F5
		internal SectionDefinition(SectionGroup parentSectionGroup, IAppHostSectionDefinition sectionDefinition)
		{
			this._sectionDefinition = sectionDefinition;
			this._parentSectionGroup = parentSectionGroup;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000770B File Offset: 0x0000670B
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00007718 File Offset: 0x00006718
		public string AllowDefinition
		{
			get
			{
				return this._sectionDefinition.AllowDefinition;
			}
			set
			{
				this._parentSectionGroup.SetDirty();
				this._sectionDefinition.AllowDefinition = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00007731 File Offset: 0x00006731
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000773E File Offset: 0x0000673E
		public string AllowLocation
		{
			get
			{
				return this._sectionDefinition.AllowLocation;
			}
			set
			{
				this._parentSectionGroup.SetDirty();
				this._sectionDefinition.AllowLocation = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00007757 File Offset: 0x00006757
		public string Name
		{
			get
			{
				return this._sectionDefinition.Name;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00007764 File Offset: 0x00006764
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00007771 File Offset: 0x00006771
		public string OverrideModeDefault
		{
			get
			{
				return this._sectionDefinition.OverrideModeDefault;
			}
			set
			{
				this._parentSectionGroup.SetDirty();
				this._sectionDefinition.OverrideModeDefault = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000778A File Offset: 0x0000678A
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00007797 File Offset: 0x00006797
		public bool RequirePermission
		{
			get
			{
				return this._sectionDefinition.RequirePermission;
			}
			set
			{
				this._parentSectionGroup.SetDirty();
				this._sectionDefinition.RequirePermission = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060002CB RID: 715 RVA: 0x000077B0 File Offset: 0x000067B0
		// (set) Token: 0x060002CC RID: 716 RVA: 0x000077BD File Offset: 0x000067BD
		public string Type
		{
			get
			{
				return this._sectionDefinition.Type;
			}
			set
			{
				this._parentSectionGroup.SetDirty();
				this._sectionDefinition.Type = value;
			}
		}

		// Token: 0x04000103 RID: 259
		private IAppHostSectionDefinition _sectionDefinition;

		// Token: 0x04000104 RID: 260
		private SectionGroup _parentSectionGroup;
	}
}
