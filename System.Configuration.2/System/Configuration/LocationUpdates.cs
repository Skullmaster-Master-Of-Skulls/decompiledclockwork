using System;

namespace System.Configuration
{
	// Token: 0x0200006C RID: 108
	internal class LocationUpdates
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x0001450D File Offset: 0x0001270D
		internal LocationUpdates(OverrideModeSetting overrideMode, bool inheritInChildApps)
		{
			this._overrideMode = overrideMode;
			this._inheritInChildApps = inheritInChildApps;
			this._sectionUpdates = new SectionUpdates(string.Empty);
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00014533 File Offset: 0x00012733
		internal OverrideModeSetting OverrideMode
		{
			get
			{
				return this._overrideMode;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0001453B File Offset: 0x0001273B
		internal bool InheritInChildApps
		{
			get
			{
				return this._inheritInChildApps;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00014543 File Offset: 0x00012743
		internal SectionUpdates SectionUpdates
		{
			get
			{
				return this._sectionUpdates;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0001454B File Offset: 0x0001274B
		internal bool IsDefault
		{
			get
			{
				return this._overrideMode.IsDefaultForLocationTag && this._inheritInChildApps;
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00014562 File Offset: 0x00012762
		internal void CompleteUpdates()
		{
			this._sectionUpdates.CompleteUpdates();
		}

		// Token: 0x04000299 RID: 665
		private OverrideModeSetting _overrideMode;

		// Token: 0x0400029A RID: 666
		private bool _inheritInChildApps;

		// Token: 0x0400029B RID: 667
		private SectionUpdates _sectionUpdates;
	}
}
