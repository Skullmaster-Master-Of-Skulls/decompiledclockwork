using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x020019E2 RID: 6626
	internal class ScriptEntryUrlBuilder
	{
		// Token: 0x06010072 RID: 65650 RVA: 0x00398984 File Offset: 0x00396B84
		public ScriptEntryUrlBuilder(string urlBase, string key) : this(urlBase, key, false)
		{
		}

		// Token: 0x06010073 RID: 65651 RVA: 0x0039898F File Offset: 0x00396B8F
		public ScriptEntryUrlBuilder(string urlBase, string key, bool registerStyleSheets)
		{
			this._urlBase = urlBase;
			this._key = key;
			this._resourcesAreStyleSheets = registerStyleSheets;
			this.AddSlot();
		}

		// Token: 0x17004D66 RID: 19814
		// (get) Token: 0x06010074 RID: 65652 RVA: 0x003989C8 File Offset: 0x00396BC8
		private ScriptManagerConfigurationSettings Config
		{
			get
			{
				return this._settings;
			}
		}

		// Token: 0x17004D67 RID: 19815
		// (get) Token: 0x06010075 RID: 65653 RVA: 0x003989D0 File Offset: 0x00396BD0
		private ScriptEntrySlot CurrentSlot
		{
			get
			{
				return this._scriptEntrySlots[this._scriptEntrySlots.Count - 1];
			}
		}

		// Token: 0x06010076 RID: 65654 RVA: 0x003989EC File Offset: 0x00396BEC
		private void AddSlot()
		{
			ScriptEntrySlot item = new ScriptEntrySlot(this._urlBase, this.Config.EnableHandlerEncryption);
			this._scriptEntrySlots.Add(item);
			if (this._resourcesAreStyleSheets)
			{
				this._currentSlotSelectorCount = 0;
			}
		}

		// Token: 0x06010077 RID: 65655 RVA: 0x00398A2B File Offset: 0x00396C2B
		public void StartNewSlot()
		{
			this.AddSlot();
		}

		// Token: 0x06010078 RID: 65656 RVA: 0x00398A34 File Offset: 0x00396C34
		public void UpdateBaseUrl(string newBaseUrl)
		{
			this._urlBase = newBaseUrl;
			foreach (ScriptEntrySlot scriptEntrySlot in this.ScriptEntrySlots)
			{
				scriptEntrySlot.SetUrlBase(this._urlBase);
			}
		}

		// Token: 0x17004D68 RID: 19816
		// (get) Token: 0x06010079 RID: 65657 RVA: 0x00398A94 File Offset: 0x00396C94
		private List<ScriptEntry> ScriptEntries
		{
			get
			{
				if (HttpContext.Current.Items[this._key] == null)
				{
					HttpContext.Current.Items[this._key] = new List<ScriptEntry>();
				}
				return (List<ScriptEntry>)HttpContext.Current.Items[this._key];
			}
		}

		// Token: 0x0601007A RID: 65658 RVA: 0x00398B04 File Offset: 0x00396D04
		public bool IsScriptEntryRegistered(ScriptEntry scriptEntry)
		{
			foreach (ScriptEntrySlot scriptEntrySlot in this._scriptEntrySlots)
			{
				if (scriptEntrySlot.HasScriptEntry(scriptEntry))
				{
					return true;
				}
			}
			return this.ScriptEntries.Find((ScriptEntry currentEntry) => currentEntry.Equals(scriptEntry)) != null;
		}

		// Token: 0x0601007B RID: 65659 RVA: 0x00398B90 File Offset: 0x00396D90
		public void RegisterDisabledScriptEntry(ScriptEntry scriptEntry)
		{
			this.ScriptEntries.Add(scriptEntry);
			this.CurrentSlot.AddDisabled(scriptEntry);
		}

		// Token: 0x0601007C RID: 65660 RVA: 0x00398BAC File Offset: 0x00396DAC
		public void RegisterScriptEntry(ScriptEntry scriptEntry)
		{
			if (scriptEntry.HasInitialPath)
			{
				string name = string.IsNullOrEmpty(scriptEntry.Name) ? scriptEntry.Path : scriptEntry.Name;
				ScriptEntry scriptEntry2 = new ScriptEntry(scriptEntry.Assembly, name, scriptEntry.Culture);
				this.RegisterDisabledScriptEntry(scriptEntry2);
				this.AddSlot();
				this.ScriptEntries.Add(scriptEntry2);
				return;
			}
			this.ScriptEntries.Add(scriptEntry);
			if (scriptEntry.LoadSeparately)
			{
				this.AddSlot();
				this.TryAddScriptEntry(scriptEntry);
				return;
			}
			if (!this.TryAddScriptEntry(scriptEntry))
			{
				this.AddSlot();
				this.TryAddScriptEntry(scriptEntry);
			}
		}

		// Token: 0x0601007D RID: 65661 RVA: 0x00398C44 File Offset: 0x00396E44
		private bool TryAddScriptEntry(ScriptEntry scriptEntry)
		{
			string serializedScriptEntry = this.CurrentSlot.SerializeScriptEntry(scriptEntry);
			bool flag = this.CurrentSlot.CanStore(scriptEntry, serializedScriptEntry);
			if (this._resourcesAreStyleSheets)
			{
				flag &= this.CanStoreStyleSheet(scriptEntry);
			}
			if (flag)
			{
				this.CurrentSlot.Add(scriptEntry, serializedScriptEntry);
				if (this._resourcesAreStyleSheets)
				{
					this._currentSlotSelectorCount += this._currentStyleSheetSelectorCount;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0601007E RID: 65662 RVA: 0x00398CAC File Offset: 0x00396EAC
		public void RegisterScriptEntryToSeparateSlot(ScriptEntry scriptEntry)
		{
			this.CurrentSlot.Add(scriptEntry, this.CurrentSlot.SerializeScriptEntry(scriptEntry));
			this.AddSlot();
		}

		// Token: 0x0601007F RID: 65663 RVA: 0x00398CCC File Offset: 0x00396ECC
		public List<string> GetUrls()
		{
			List<string> list = new List<string>();
			foreach (ScriptEntrySlot scriptEntrySlot in this._scriptEntrySlots)
			{
				if (!string.IsNullOrEmpty(scriptEntrySlot.GetUrl()))
				{
					list.Add(scriptEntrySlot.GetUrl());
				}
			}
			return list;
		}

		// Token: 0x17004D69 RID: 19817
		// (get) Token: 0x06010080 RID: 65664 RVA: 0x00398D38 File Offset: 0x00396F38
		internal List<ScriptEntrySlot> ScriptEntrySlots
		{
			get
			{
				return this._scriptEntrySlots;
			}
		}

		// Token: 0x06010081 RID: 65665 RVA: 0x00398D40 File Offset: 0x00396F40
		private Regex GetStyleSheetCommentRegex()
		{
			if (this._styleSheetCommentRegex == null)
			{
				this._styleSheetCommentRegex = new Regex("\\/\\*.*?(\\*\\/|$)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			}
			return this._styleSheetCommentRegex;
		}

		// Token: 0x06010082 RID: 65666 RVA: 0x00398D62 File Offset: 0x00396F62
		private Regex GetStyleSheetSelectorRegex()
		{
			if (this._styleSheetSelectorRegex == null)
			{
				this._styleSheetSelectorRegex = new Regex("[\\{\\,]", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			}
			return this._styleSheetSelectorRegex;
		}

		// Token: 0x06010083 RID: 65667 RVA: 0x00398D84 File Offset: 0x00396F84
		internal int GetSelectorCount(ScriptEntry styleSheetEntry)
		{
			string script = styleSheetEntry.GetScript();
			string input = this.GetStyleSheetCommentRegex().Replace(script, string.Empty);
			int num = 0;
			Match match = this.GetStyleSheetSelectorRegex().Match(input);
			while (match.Success)
			{
				num++;
				match = match.NextMatch();
			}
			return num;
		}

		// Token: 0x06010084 RID: 65668 RVA: 0x00398DCF File Offset: 0x00396FCF
		private bool CanStoreStyleSheet(ScriptEntry styleSheet)
		{
			if (styleSheet.EnableSelectorLimitCheck)
			{
				this._currentStyleSheetSelectorCount = this.GetSelectorCount(styleSheet);
				return this._currentSlotSelectorCount + this._currentStyleSheetSelectorCount <= 4095;
			}
			return true;
		}

		// Token: 0x0400489C RID: 18588
		private const int _styleSheetSelectorLimit = 4095;

		// Token: 0x0400489D RID: 18589
		private string _urlBase;

		// Token: 0x0400489E RID: 18590
		private string _key;

		// Token: 0x0400489F RID: 18591
		private List<ScriptEntrySlot> _scriptEntrySlots = new List<ScriptEntrySlot>();

		// Token: 0x040048A0 RID: 18592
		private bool _resourcesAreStyleSheets;

		// Token: 0x040048A1 RID: 18593
		private int _currentSlotSelectorCount;

		// Token: 0x040048A2 RID: 18594
		private int _currentStyleSheetSelectorCount;

		// Token: 0x040048A3 RID: 18595
		private Regex _styleSheetCommentRegex;

		// Token: 0x040048A4 RID: 18596
		private Regex _styleSheetSelectorRegex;

		// Token: 0x040048A5 RID: 18597
		private readonly ScriptManagerConfigurationSettings _settings = ScriptManagerConfigurationSettings.GetConfiguration();
	}
}
