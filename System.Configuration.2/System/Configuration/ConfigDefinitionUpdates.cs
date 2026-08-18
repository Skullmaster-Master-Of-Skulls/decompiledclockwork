using System;
using System.Collections;

namespace System.Configuration
{
	// Token: 0x02000019 RID: 25
	internal class ConfigDefinitionUpdates
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00008E24 File Offset: 0x00007024
		internal ConfigDefinitionUpdates()
		{
			this._locationUpdatesList = new ArrayList();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00008E38 File Offset: 0x00007038
		internal LocationUpdates FindLocationUpdates(OverrideModeSetting overrideMode, bool inheritInChildApps)
		{
			foreach (object obj in this._locationUpdatesList)
			{
				LocationUpdates locationUpdates = (LocationUpdates)obj;
				if (OverrideModeSetting.CanUseSameLocationTag(locationUpdates.OverrideMode, overrideMode) && locationUpdates.InheritInChildApps == inheritInChildApps)
				{
					return locationUpdates;
				}
			}
			return null;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00008EA8 File Offset: 0x000070A8
		internal DefinitionUpdate AddUpdate(OverrideModeSetting overrideMode, bool inheritInChildApps, bool moved, string updatedXml, SectionRecord sectionRecord)
		{
			LocationUpdates locationUpdates = this.FindLocationUpdates(overrideMode, inheritInChildApps);
			if (locationUpdates == null)
			{
				locationUpdates = new LocationUpdates(overrideMode, inheritInChildApps);
				this._locationUpdatesList.Add(locationUpdates);
			}
			DefinitionUpdate definitionUpdate = new DefinitionUpdate(sectionRecord.ConfigKey, moved, updatedXml, sectionRecord);
			locationUpdates.SectionUpdates.AddSection(definitionUpdate);
			return definitionUpdate;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008EF8 File Offset: 0x000070F8
		internal void CompleteUpdates()
		{
			foreach (object obj in this._locationUpdatesList)
			{
				LocationUpdates locationUpdates = (LocationUpdates)obj;
				locationUpdates.CompleteUpdates();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00008F50 File Offset: 0x00007150
		internal ArrayList LocationUpdatesList
		{
			get
			{
				return this._locationUpdatesList;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00008F58 File Offset: 0x00007158
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00008F60 File Offset: 0x00007160
		internal bool RequireLocation
		{
			get
			{
				return this._requireLocationWritten;
			}
			set
			{
				this._requireLocationWritten = value;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00008F69 File Offset: 0x00007169
		internal void FlagLocationWritten()
		{
			this._requireLocationWritten = false;
		}

		// Token: 0x0400016D RID: 365
		private ArrayList _locationUpdatesList;

		// Token: 0x0400016E RID: 366
		private bool _requireLocationWritten;
	}
}
