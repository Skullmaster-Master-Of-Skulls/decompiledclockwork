using System;
using System.Collections;

namespace System.Configuration
{
	// Token: 0x0200008A RID: 138
	internal class SectionUpdates
	{
		// Token: 0x06000596 RID: 1430 RVA: 0x0001BD02 File Offset: 0x00019F02
		internal SectionUpdates(string name)
		{
			this._name = name;
			this._groups = new Hashtable();
			this._sections = new Hashtable();
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001BD27 File Offset: 0x00019F27
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x0001BD2F File Offset: 0x00019F2F
		internal bool IsNew
		{
			get
			{
				return this._isNew;
			}
			set
			{
				this._isNew = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001BD38 File Offset: 0x00019F38
		internal bool IsEmpty
		{
			get
			{
				return this._groups.Count == 0 && this._sections.Count == 0;
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001BD58 File Offset: 0x00019F58
		private SectionUpdates FindSectionUpdates(string configKey, bool isGroup)
		{
			string text;
			if (isGroup)
			{
				text = configKey;
			}
			else
			{
				string text2;
				BaseConfigurationRecord.SplitConfigKey(configKey, out text, out text2);
			}
			SectionUpdates sectionUpdates = this;
			if (text.Length != 0)
			{
				string[] array = text.Split(BaseConfigurationRecord.ConfigPathSeparatorParams);
				foreach (string text3 in array)
				{
					SectionUpdates sectionUpdates2 = (SectionUpdates)sectionUpdates._groups[text3];
					if (sectionUpdates2 == null)
					{
						sectionUpdates2 = new SectionUpdates(text3);
						sectionUpdates._groups[text3] = sectionUpdates2;
					}
					sectionUpdates = sectionUpdates2;
				}
			}
			return sectionUpdates;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001BDE0 File Offset: 0x00019FE0
		internal void CompleteUpdates()
		{
			bool flag = true;
			foreach (object obj in this._groups.Values)
			{
				SectionUpdates sectionUpdates = (SectionUpdates)obj;
				sectionUpdates.CompleteUpdates();
				if (!sectionUpdates.IsNew)
				{
					flag = false;
				}
			}
			this._isNew = (flag && this._cMoved == this._sections.Count);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001BE68 File Offset: 0x0001A068
		internal void AddSection(Update update)
		{
			SectionUpdates sectionUpdates = this.FindSectionUpdates(update.ConfigKey, false);
			sectionUpdates._sections.Add(update.ConfigKey, update);
			sectionUpdates._cUnretrieved++;
			if (update.Moved)
			{
				sectionUpdates._cMoved++;
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001BEBC File Offset: 0x0001A0BC
		internal void AddSectionGroup(Update update)
		{
			SectionUpdates sectionUpdates = this.FindSectionUpdates(update.ConfigKey, true);
			sectionUpdates._sectionGroupUpdate = update;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001BEE0 File Offset: 0x0001A0E0
		private Update GetUpdate(string configKey)
		{
			Update update = (Update)this._sections[configKey];
			if (update != null)
			{
				if (update.Retrieved)
				{
					update = null;
				}
				else
				{
					update.Retrieved = true;
					this._cUnretrieved--;
					if (update.Moved)
					{
						this._cMoved--;
					}
				}
			}
			return update;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001BF3A File Offset: 0x0001A13A
		internal DeclarationUpdate GetSectionGroupUpdate()
		{
			if (this._sectionGroupUpdate != null && !this._sectionGroupUpdate.Retrieved)
			{
				this._sectionGroupUpdate.Retrieved = true;
				return (DeclarationUpdate)this._sectionGroupUpdate;
			}
			return null;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001BF6A File Offset: 0x0001A16A
		internal DefinitionUpdate GetDefinitionUpdate(string configKey)
		{
			return (DefinitionUpdate)this.GetUpdate(configKey);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001BF78 File Offset: 0x0001A178
		internal DeclarationUpdate GetDeclarationUpdate(string configKey)
		{
			return (DeclarationUpdate)this.GetUpdate(configKey);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001BF86 File Offset: 0x0001A186
		internal SectionUpdates GetSectionUpdatesForGroup(string group)
		{
			return (SectionUpdates)this._groups[group];
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001BF9C File Offset: 0x0001A19C
		internal bool HasUnretrievedSections()
		{
			if (this._cUnretrieved > 0 || (this._sectionGroupUpdate != null && !this._sectionGroupUpdate.Retrieved))
			{
				return true;
			}
			foreach (object obj in this._groups.Values)
			{
				SectionUpdates sectionUpdates = (SectionUpdates)obj;
				if (sectionUpdates.HasUnretrievedSections())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001C024 File Offset: 0x0001A224
		internal void MarkAsRetrieved()
		{
			this._cUnretrieved = 0;
			foreach (object obj in this._groups.Values)
			{
				SectionUpdates sectionUpdates = (SectionUpdates)obj;
				sectionUpdates.MarkAsRetrieved();
			}
			if (this._sectionGroupUpdate != null)
			{
				this._sectionGroupUpdate.Retrieved = true;
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001C09C File Offset: 0x0001A29C
		internal void MarkGroupAsRetrieved(string groupName)
		{
			SectionUpdates sectionUpdates = this._groups[groupName] as SectionUpdates;
			if (sectionUpdates != null)
			{
				sectionUpdates.MarkAsRetrieved();
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
		internal bool HasNewSectionGroups()
		{
			foreach (object obj in this._groups.Values)
			{
				SectionUpdates sectionUpdates = (SectionUpdates)obj;
				if (sectionUpdates.IsNew)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001C12C File Offset: 0x0001A32C
		internal string[] GetUnretrievedSectionNames()
		{
			if (this._cUnretrieved == 0)
			{
				return null;
			}
			string[] array = new string[this._cUnretrieved];
			int num = 0;
			foreach (object obj in this._sections.Values)
			{
				Update update = (Update)obj;
				if (!update.Retrieved)
				{
					array[num] = update.ConfigKey;
					num++;
				}
			}
			Array.Sort<string>(array);
			return array;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001C1BC File Offset: 0x0001A3BC
		internal string[] GetMovedSectionNames()
		{
			if (this._cMoved == 0)
			{
				return null;
			}
			string[] array = new string[this._cMoved];
			int num = 0;
			foreach (object obj in this._sections.Values)
			{
				Update update = (Update)obj;
				if (update.Moved && !update.Retrieved)
				{
					array[num] = update.ConfigKey;
					num++;
				}
			}
			Array.Sort<string>(array);
			return array;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001C254 File Offset: 0x0001A454
		internal string[] GetUnretrievedGroupNames()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this._groups)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string value = (string)dictionaryEntry.Key;
				SectionUpdates sectionUpdates = (SectionUpdates)dictionaryEntry.Value;
				if (sectionUpdates.HasUnretrievedSections())
				{
					arrayList.Add(value);
				}
			}
			if (arrayList.Count == 0)
			{
				return null;
			}
			string[] array = new string[arrayList.Count];
			arrayList.CopyTo(array);
			Array.Sort<string>(array);
			return array;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001C304 File Offset: 0x0001A504
		internal string[] GetNewGroupNames()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this._groups)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string value = (string)dictionaryEntry.Key;
				SectionUpdates sectionUpdates = (SectionUpdates)dictionaryEntry.Value;
				if (sectionUpdates.IsNew && sectionUpdates.HasUnretrievedSections())
				{
					arrayList.Add(value);
				}
			}
			if (arrayList.Count == 0)
			{
				return null;
			}
			string[] array = new string[arrayList.Count];
			arrayList.CopyTo(array);
			Array.Sort<string>(array);
			return array;
		}

		// Token: 0x0400032D RID: 813
		private string _name;

		// Token: 0x0400032E RID: 814
		private Hashtable _groups;

		// Token: 0x0400032F RID: 815
		private Hashtable _sections;

		// Token: 0x04000330 RID: 816
		private int _cUnretrieved;

		// Token: 0x04000331 RID: 817
		private int _cMoved;

		// Token: 0x04000332 RID: 818
		private Update _sectionGroupUpdate;

		// Token: 0x04000333 RID: 819
		private bool _isNew;
	}
}
