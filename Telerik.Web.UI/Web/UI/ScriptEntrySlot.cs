using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02001849 RID: 6217
	internal class ScriptEntrySlot
	{
		// Token: 0x0600F167 RID: 61799 RVA: 0x0036DD95 File Offset: 0x0036BF95
		public ScriptEntrySlot() : this(CryptoService.GetService(""), false)
		{
		}

		// Token: 0x0600F168 RID: 61800 RVA: 0x0036DDA8 File Offset: 0x0036BFA8
		public ScriptEntrySlot(string urlBase, bool shoudlEncrypt = false) : this(CryptoService.GetService(""), shoudlEncrypt)
		{
			this._urlBase = urlBase;
			this._path = this._urlBase;
		}

		// Token: 0x0600F169 RID: 61801 RVA: 0x0036DDCE File Offset: 0x0036BFCE
		public ScriptEntrySlot(ICryptoService service, bool shoudlEncrypt = false)
		{
			this._service = service;
			this.Encrypt = shoudlEncrypt;
		}

		// Token: 0x170048EA RID: 18666
		// (get) Token: 0x0600F16A RID: 61802 RVA: 0x0036DE05 File Offset: 0x0036C005
		public ICryptoService EncryptionService
		{
			get
			{
				return this._service;
			}
		}

		// Token: 0x170048EB RID: 18667
		// (get) Token: 0x0600F16B RID: 61803 RVA: 0x0036DE0D File Offset: 0x0036C00D
		// (set) Token: 0x0600F16C RID: 61804 RVA: 0x0036DE15 File Offset: 0x0036C015
		public bool Encrypt { get; private set; }

		// Token: 0x0600F16D RID: 61805 RVA: 0x0036DE1E File Offset: 0x0036C01E
		public void SetUrlBase(string newUrlBase)
		{
			this._urlBase = newUrlBase;
			this._path = this._urlBase + this._stringBuilder.ToString();
			this.ResetPaths(this._path);
		}

		// Token: 0x0600F16E RID: 61806 RVA: 0x0036DE4F File Offset: 0x0036C04F
		public string GetUrl()
		{
			if (this._externalEntryLastModifiedTicks > 0L)
			{
				this.UpdateLastExternalStyleSheetEntryBlockTimeStamp();
			}
			if (!(this._path == this._urlBase))
			{
				return this._path;
			}
			return null;
		}

		// Token: 0x0600F16F RID: 61807 RVA: 0x0036DE7C File Offset: 0x0036C07C
		public bool CanStore(ScriptEntry scriptEntry, string serializedScriptEntry)
		{
			return this._urlBase.Length + this._stringBuilder.Length + serializedScriptEntry.Length <= 2000 || scriptEntry.HasInitialPath;
		}

		// Token: 0x0600F170 RID: 61808 RVA: 0x0036DEAB File Offset: 0x0036C0AB
		public bool HasScriptEntry(ScriptEntry searchedScriptEntry)
		{
			return this.ContainsScriptEntry(searchedScriptEntry, this._scriptEntries) || this.ContainsScriptEntry(searchedScriptEntry, this._disabledScriptEntries);
		}

		// Token: 0x0600F171 RID: 61809 RVA: 0x0036DECC File Offset: 0x0036C0CC
		public void UpdateLastExternalStyleSheetEntryBlockTimeStamp()
		{
			this._stringBuilder.Replace("_timestamp_", this._externalEntryLastModifiedTicks.ToString());
			this._externalEntryLastModifiedTicks = 0L;
			this._path = this._urlBase + this._stringBuilder.ToString();
		}

		// Token: 0x0600F172 RID: 61810 RVA: 0x0036DF1C File Offset: 0x0036C11C
		public void Add(ScriptEntry newScriptEntry, string serializedScriptEntry)
		{
			if (!newScriptEntry.HasInitialPath && !(newScriptEntry is ExternalScriptEntry))
			{
				bool flag = this.IsNewAssembly(newScriptEntry);
				if (flag)
				{
					if (newScriptEntry.IsExternal)
					{
						string text = HttpUtility.UrlEncode(newScriptEntry.GetSerializedAssemblyInfo());
						string newValue = text + "_timestamp_";
						serializedScriptEntry = serializedScriptEntry.Replace(text, newValue);
					}
					else if (this._externalEntryLastModifiedTicks > 0L)
					{
						this._stringBuilder.Replace("_timestamp_", this._externalEntryLastModifiedTicks.ToString());
						this._externalEntryLastModifiedTicks = 0L;
					}
				}
				if (newScriptEntry.IsExternal)
				{
					long lastModified = (newScriptEntry as ExternalStyleSheetEntry).LastModified;
					if (lastModified > this._externalEntryLastModifiedTicks)
					{
						this._externalEntryLastModifiedTicks = lastModified;
					}
				}
			}
			this._scriptEntries.Add(newScriptEntry);
			if (!newScriptEntry.HasInitialPath)
			{
				this._stringBuilder.Append(serializedScriptEntry);
				newScriptEntry.ResetReference();
				string text2 = this._stringBuilder.ToString();
				if (this.Encrypt)
				{
					text2 = this.EncryptionService.EncryptWithMachineKey(text2);
				}
				this._path = this._urlBase + text2;
				this.ResetPaths(this._path);
			}
		}

		// Token: 0x0600F173 RID: 61811 RVA: 0x0036E032 File Offset: 0x0036C232
		public void AddDisabled(ScriptEntry scriptEntry)
		{
			this._disabledScriptEntries.Add(scriptEntry);
			scriptEntry.ResetReference();
			scriptEntry.SetPath(this._path);
		}

		// Token: 0x0600F174 RID: 61812 RVA: 0x0036E054 File Offset: 0x0036C254
		public string SerializeScriptEntry(ScriptEntry scriptEntry)
		{
			string str = string.Empty;
			if (this.IsNewAssembly(scriptEntry))
			{
				str = this.GetSerializedAssemblyInfo(scriptEntry) + this.GetSerializedScriptEntryInfo(scriptEntry);
			}
			else
			{
				str = this.GetSerializedScriptEntryInfo(scriptEntry);
			}
			return HttpUtility.UrlEncode(str);
		}

		// Token: 0x0600F175 RID: 61813 RVA: 0x0036E094 File Offset: 0x0036C294
		internal string GetSerializedAssemblyInfo(ScriptEntry scriptEntry)
		{
			return scriptEntry.GetSerializedAssemblyInfo();
		}

		// Token: 0x0600F176 RID: 61814 RVA: 0x0036E09C File Offset: 0x0036C29C
		internal string GetSerializedScriptEntryInfo(ScriptEntry scriptEntry)
		{
			return scriptEntry.GetSerializedScriptEntryInfo();
		}

		// Token: 0x0600F177 RID: 61815 RVA: 0x0036E0A4 File Offset: 0x0036C2A4
		internal bool IsNewAssembly(ScriptEntry newScriptEntry)
		{
			if (this._scriptEntries.Count == 0)
			{
				return true;
			}
			int num = this._scriptEntries.Count;
			ScriptEntry scriptEntry;
			do
			{
				scriptEntry = this.GetScriptEntry(--num);
			}
			while (scriptEntry != null && scriptEntry.HasInitialPath);
			return scriptEntry == null || !ScriptEntry.AssembliesEqual(scriptEntry, newScriptEntry);
		}

		// Token: 0x0600F178 RID: 61816 RVA: 0x0036E0F4 File Offset: 0x0036C2F4
		private bool ContainsScriptEntry(ScriptEntry searchedScriptEntry, List<ScriptEntry> scriptEntries)
		{
			foreach (ScriptEntry scriptEntry in scriptEntries)
			{
				if (scriptEntry.Equals(searchedScriptEntry))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600F179 RID: 61817 RVA: 0x0036E14C File Offset: 0x0036C34C
		private void ResetPaths(string path)
		{
			foreach (ScriptEntry scriptEntry in this._scriptEntries)
			{
				if (!scriptEntry.HasInitialPath)
				{
					scriptEntry.SetPath(path);
				}
			}
			foreach (ScriptEntry scriptEntry2 in this._disabledScriptEntries)
			{
				scriptEntry2.SetPath(path);
			}
		}

		// Token: 0x0600F17A RID: 61818 RVA: 0x0036E1EC File Offset: 0x0036C3EC
		private ScriptEntry GetScriptEntry(int currentIndex)
		{
			if (currentIndex >= 0 && this._scriptEntries.Count > currentIndex)
			{
				return this._scriptEntries[currentIndex];
			}
			return null;
		}

		// Token: 0x170048EC RID: 18668
		// (get) Token: 0x0600F17B RID: 61819 RVA: 0x0036E20E File Offset: 0x0036C40E
		internal List<ScriptEntry> DisabledScriptEntries
		{
			get
			{
				return this._disabledScriptEntries;
			}
		}

		// Token: 0x170048ED RID: 18669
		// (get) Token: 0x0600F17C RID: 61820 RVA: 0x0036E216 File Offset: 0x0036C416
		internal List<ScriptEntry> ActiveScriptEntries
		{
			get
			{
				return this._scriptEntries;
			}
		}

		// Token: 0x04004575 RID: 17781
		private const int MaxUrlLength = 2000;

		// Token: 0x04004576 RID: 17782
		private const string TimeStampPlaceHolder = "_timestamp_";

		// Token: 0x04004577 RID: 17783
		private readonly StringBuilder _stringBuilder = new StringBuilder();

		// Token: 0x04004578 RID: 17784
		private readonly List<ScriptEntry> _scriptEntries = new List<ScriptEntry>();

		// Token: 0x04004579 RID: 17785
		private readonly List<ScriptEntry> _disabledScriptEntries = new List<ScriptEntry>();

		// Token: 0x0400457A RID: 17786
		private readonly ICryptoService _service;

		// Token: 0x0400457B RID: 17787
		private string _urlBase;

		// Token: 0x0400457C RID: 17788
		private string _path;

		// Token: 0x0400457D RID: 17789
		private long _externalEntryLastModifiedTicks;
	}
}
