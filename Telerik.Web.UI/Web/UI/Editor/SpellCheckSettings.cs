using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02001090 RID: 4240
	public class SpellCheckSettings : StateManager
	{
		// Token: 0x170037B9 RID: 14265
		// (get) Token: 0x0600AC67 RID: 44135 RVA: 0x002504CD File Offset: 0x0024E6CD
		// (set) Token: 0x0600AC68 RID: 44136 RVA: 0x002504FC File Offset: 0x0024E6FC
		[DefaultValue("-Custom")]
		[Description("The appendix for the custom dictionary files (filenames are Language + CustomDictionarySuffix + '.txt').")]
		[NotifyParentProperty(true)]
		public string CustomDictionarySuffix
		{
			get
			{
				if (base.ViewState["CustomDictionarySuffix"] != null)
				{
					return (string)base.ViewState["CustomDictionarySuffix"];
				}
				return "-Custom";
			}
			set
			{
				base.ViewState["CustomDictionarySuffix"] = value;
			}
		}

		// Token: 0x170037BA RID: 14266
		// (get) Token: 0x0600AC69 RID: 44137 RVA: 0x0025050F File Offset: 0x0024E70F
		// (set) Token: 0x0600AC6A RID: 44138 RVA: 0x0025053E File Offset: 0x0024E73E
		[Description("The path for the dictionary files. The default is ~/App_Data/Spell/")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string DictionaryPath
		{
			get
			{
				if (base.ViewState["DictionaryPath"] != null)
				{
					return (string)base.ViewState["DictionaryPath"];
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DictionaryPath"] = value;
			}
		}

		// Token: 0x170037BB RID: 14267
		// (get) Token: 0x0600AC6B RID: 44139 RVA: 0x00250551 File Offset: 0x0024E751
		// (set) Token: 0x0600AC6C RID: 44140 RVA: 0x0025057C File Offset: 0x0024E77C
		[Description("Specifies the edit distance. If you increase the value, the checking speed decreases but more suggestions are presented.")]
		[DefaultValue(1)]
		[NotifyParentProperty(true)]
		public int EditDistance
		{
			get
			{
				if (base.ViewState["EditDistance"] != null)
				{
					return (int)base.ViewState["EditDistance"];
				}
				return 1;
			}
			set
			{
				base.ViewState["EditDistance"] = value;
			}
		}

		// Token: 0x170037BC RID: 14268
		// (get) Token: 0x0600AC6D RID: 44141 RVA: 0x00250594 File Offset: 0x0024E794
		// (set) Token: 0x0600AC6E RID: 44142 RVA: 0x002505BF File Offset: 0x0024E7BF
		[Description("Gets or sets the value indicating whether the spell will allow adding custom words.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool AllowAddCustom
		{
			get
			{
				return base.ViewState["AllowAddCustom"] == null || (bool)base.ViewState["AllowAddCustom"];
			}
			set
			{
				base.ViewState["AllowAddCustom"] = value;
			}
		}

		// Token: 0x170037BD RID: 14269
		// (get) Token: 0x0600AC6F RID: 44143 RVA: 0x002505D7 File Offset: 0x0024E7D7
		// (set) Token: 0x0600AC70 RID: 44144 RVA: 0x00250606 File Offset: 0x0024E806
		[NotifyParentProperty(true)]
		[DefaultValue("en-US")]
		[Description("Gets or sets the spellcheck language if different than the Language property.")]
		public string DictionaryLanguage
		{
			get
			{
				if (base.ViewState["DictionaryLanguage"] != null)
				{
					return (string)base.ViewState["DictionaryLanguage"];
				}
				return "en-US";
			}
			set
			{
				base.ViewState["DictionaryLanguage"] = value;
			}
		}

		// Token: 0x170037BE RID: 14270
		// (get) Token: 0x0600AC71 RID: 44145 RVA: 0x00250619 File Offset: 0x0024E819
		// (set) Token: 0x0600AC72 RID: 44146 RVA: 0x00250644 File Offset: 0x0024E844
		[DefaultValue(FragmentIgnoreOptions.All)]
		[NotifyParentProperty(true)]
		[Description("Ignore selectd text fragments: file names, URL's, email addresses.")]
		public FragmentIgnoreOptions FragmentIgnoreOptions
		{
			get
			{
				if (base.ViewState["FragmentIgnoreOptions"] != null)
				{
					return (FragmentIgnoreOptions)base.ViewState["FragmentIgnoreOptions"];
				}
				return FragmentIgnoreOptions.All;
			}
			set
			{
				base.ViewState["FragmentIgnoreOptions"] = value;
			}
		}

		// Token: 0x170037BF RID: 14271
		// (get) Token: 0x0600AC73 RID: 44147 RVA: 0x0025065C File Offset: 0x0024E85C
		// (set) Token: 0x0600AC74 RID: 44148 RVA: 0x0025067C File Offset: 0x0024E87C
		[NotifyParentProperty(true)]
		[Description("Specifies the type name for a custom spell check provider.")]
		[DefaultValue("")]
		public string SpellCheckProviderTypeName
		{
			get
			{
				return ((string)base.ViewState["SpellCheckProviderTypeName"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["SpellCheckProviderTypeName"] = value;
			}
		}

		// Token: 0x170037C0 RID: 14272
		// (get) Token: 0x0600AC75 RID: 44149 RVA: 0x0025068F File Offset: 0x0024E88F
		// (set) Token: 0x0600AC76 RID: 44150 RVA: 0x002506BA File Offset: 0x0024E8BA
		[DefaultValue(SpellCheckProvider.PhoneticProvider)]
		[Description("Specifies whether RadSpell should use the internal spellchecking algorithm or try to use Microsoft Word.")]
		[NotifyParentProperty(true)]
		public SpellCheckProvider SpellCheckProvider
		{
			get
			{
				if (base.ViewState["SpellCheckProvider"] != null)
				{
					return (SpellCheckProvider)base.ViewState["SpellCheckProvider"];
				}
				return SpellCheckProvider.PhoneticProvider;
			}
			set
			{
				base.ViewState["SpellCheckProvider"] = value;
			}
		}

		// Token: 0x170037C1 RID: 14273
		// (get) Token: 0x0600AC77 RID: 44151 RVA: 0x002506D2 File Offset: 0x0024E8D2
		// (set) Token: 0x0600AC78 RID: 44152 RVA: 0x002506FD File Offset: 0x0024E8FD
		[NotifyParentProperty(true)]
		[DefaultValue(WordIgnoreOptions.RepeatedWords)]
		[Description("Gets or sets the value used to configure the spellchecker engine to ignore words containing: UPPERCASE, someCaPitaL letters, numbers; or to ignore repeated words (very very).")]
		public WordIgnoreOptions WordIgnoreOptions
		{
			get
			{
				if (base.ViewState["WordIgnoreOptions"] != null)
				{
					return (WordIgnoreOptions)base.ViewState["WordIgnoreOptions"];
				}
				return WordIgnoreOptions.RepeatedWords;
			}
			set
			{
				base.ViewState["WordIgnoreOptions"] = value;
			}
		}

		// Token: 0x170037C2 RID: 14274
		// (get) Token: 0x0600AC79 RID: 44153 RVA: 0x00250715 File Offset: 0x0024E915
		// (set) Token: 0x0600AC7A RID: 44154 RVA: 0x00250735 File Offset: 0x0024E935
		[Description("Gets or sets the URL, to which the spellchecker engine AJAX call will be made. Check the help for more information.")]
		[NotifyParentProperty(true)]
		[DefaultValue("Telerik.Web.UI.SpellCheckHandler.axd")]
		public string AjaxUrl
		{
			get
			{
				return ((string)base.ViewState["AjaxUrl"]) ?? "Telerik.Web.UI.SpellCheckHandler.axd";
			}
			set
			{
				base.ViewState["AjaxUrl"] = value;
			}
		}

		// Token: 0x170037C3 RID: 14275
		// (get) Token: 0x0600AC7B RID: 44155 RVA: 0x00250748 File Offset: 0x0024E948
		// (set) Token: 0x0600AC7C RID: 44156 RVA: 0x00250768 File Offset: 0x0024E968
		[Description("Gets or sets the type for the spell custom dictionary provider.")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string CustomDictionarySourceTypeName
		{
			get
			{
				return ((string)base.ViewState["CustomDictionarySourceTypeName"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["CustomDictionarySourceTypeName"] = value;
			}
		}

		// Token: 0x170037C4 RID: 14276
		// (get) Token: 0x0600AC7D RID: 44157 RVA: 0x0025077C File Offset: 0x0024E97C
		internal DialogParameters DialogParameters
		{
			get
			{
				DialogParameters dialogParameters = new DialogParameters();
				foreach (object obj in base.ViewState.Keys)
				{
					string key = (string)obj;
					dialogParameters[key] = base.ViewState[key];
				}
				return dialogParameters;
			}
		}
	}
}
