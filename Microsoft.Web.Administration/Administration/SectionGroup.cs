using System;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000068 RID: 104
	public sealed class SectionGroup
	{
		// Token: 0x060002AB RID: 683 RVA: 0x000073E5 File Offset: 0x000063E5
		internal SectionGroup(Configuration configuration, IAppHostSectionGroup sectionGroup)
		{
			this._sectionGroup = sectionGroup;
			this._configuration = configuration;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000073FB File Offset: 0x000063FB
		internal Configuration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00007403 File Offset: 0x00006403
		public string Name
		{
			get
			{
				return this._sectionGroup.Name;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00007410 File Offset: 0x00006410
		public SectionGroupCollection SectionGroups
		{
			get
			{
				if (this._sectionGroups == null)
				{
					this._sectionGroups = new SectionGroupCollection(this, this._sectionGroup);
				}
				return this._sectionGroups;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00007432 File Offset: 0x00006432
		public SectionDefinitionCollection Sections
		{
			get
			{
				if (this._sections == null)
				{
					this._sections = new SectionDefinitionCollection(this, this._sectionGroup.Sections);
				}
				return this._sections;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00007459 File Offset: 0x00006459
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x00007466 File Offset: 0x00006466
		public string Type
		{
			get
			{
				return this._sectionGroup.Type;
			}
			set
			{
				this.SetDirty();
				this._sectionGroup.Type = value;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000747A File Offset: 0x0000647A
		internal void SetDirty()
		{
			if (this._configuration == null)
			{
				throw new InvalidOperationException(Resources.ConfigurationReadOnly);
			}
			this._configuration.SetDirty();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000749C File Offset: 0x0000649C
		internal void MergeWith(IAppHostSectionGroup sectionGroup)
		{
			int num = 0;
			while ((long)num < (long)((ulong)sectionGroup.Count))
			{
				IAppHostSectionGroup appHostSectionGroup = sectionGroup[num];
				SectionGroup sectionGroup2 = this.SectionGroups[appHostSectionGroup.Name];
				if (sectionGroup2 != null)
				{
					sectionGroup2.MergeWith(appHostSectionGroup);
				}
				else
				{
					this._sectionGroups.AddInternal(appHostSectionGroup);
				}
				num++;
			}
			this.Sections.AddSections(sectionGroup.Sections);
		}

		// Token: 0x040000FC RID: 252
		private IAppHostSectionGroup _sectionGroup;

		// Token: 0x040000FD RID: 253
		private SectionDefinitionCollection _sections;

		// Token: 0x040000FE RID: 254
		private SectionGroupCollection _sectionGroups;

		// Token: 0x040000FF RID: 255
		private Configuration _configuration;
	}
}
