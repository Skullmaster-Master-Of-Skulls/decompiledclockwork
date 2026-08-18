using System;

namespace Telerik.Web.UI
{
	// Token: 0x020011EB RID: 4587
	internal class SpellDialogParameters
	{
		// Token: 0x0600BD72 RID: 48498 RVA: 0x0029F8C9 File Offset: 0x0029DAC9
		public SpellDialogParameters(DialogParameters parameters)
		{
			this._parameters = parameters;
		}

		// Token: 0x17003D1B RID: 15643
		// (get) Token: 0x0600BD73 RID: 48499 RVA: 0x0029F8D8 File Offset: 0x0029DAD8
		// (set) Token: 0x0600BD74 RID: 48500 RVA: 0x0029F8F8 File Offset: 0x0029DAF8
		public string AjaxUrl
		{
			get
			{
				return ((string)this._parameters["AjaxUrl"]) ?? "Telerik.Web.UI.SpellCheckHandler.axd";
			}
			set
			{
				this._parameters["AjaxUrl"] = value;
			}
		}

		// Token: 0x17003D1C RID: 15644
		// (get) Token: 0x0600BD75 RID: 48501 RVA: 0x0029F90B File Offset: 0x0029DB0B
		// (set) Token: 0x0600BD76 RID: 48502 RVA: 0x0029F92C File Offset: 0x0029DB2C
		public bool AllowAddCustom
		{
			get
			{
				return (bool)(this._parameters["AllowAddCustom"] ?? true);
			}
			set
			{
				this._parameters["AllowAddCustom"] = value;
			}
		}

		// Token: 0x17003D1D RID: 15645
		// (get) Token: 0x0600BD77 RID: 48503 RVA: 0x0029F944 File Offset: 0x0029DB44
		// (set) Token: 0x0600BD78 RID: 48504 RVA: 0x0029F964 File Offset: 0x0029DB64
		public string CustomDictionarySourceTypeName
		{
			get
			{
				return ((string)this._parameters["CustomDictionarySourceTypeName"]) ?? string.Empty;
			}
			set
			{
				this._parameters["CustomDictionarySourceTypeName"] = value;
			}
		}

		// Token: 0x17003D1E RID: 15646
		// (get) Token: 0x0600BD79 RID: 48505 RVA: 0x0029F977 File Offset: 0x0029DB77
		// (set) Token: 0x0600BD7A RID: 48506 RVA: 0x0029F997 File Offset: 0x0029DB97
		public string CustomDictionarySuffix
		{
			get
			{
				return ((string)this._parameters["CustomDictionarySuffix"]) ?? "-Custom";
			}
			set
			{
				this._parameters["CustomDictionarySuffix"] = value;
			}
		}

		// Token: 0x17003D1F RID: 15647
		// (get) Token: 0x0600BD7B RID: 48507 RVA: 0x0029F9AA File Offset: 0x0029DBAA
		// (set) Token: 0x0600BD7C RID: 48508 RVA: 0x0029F9CA File Offset: 0x0029DBCA
		public string DictionaryLanguage
		{
			get
			{
				return ((string)this._parameters["DictionaryLanguage"]) ?? "en-US";
			}
			set
			{
				this._parameters["DictionaryLanguage"] = value;
			}
		}

		// Token: 0x17003D20 RID: 15648
		// (get) Token: 0x0600BD7D RID: 48509 RVA: 0x0029F9DD File Offset: 0x0029DBDD
		// (set) Token: 0x0600BD7E RID: 48510 RVA: 0x0029F9FD File Offset: 0x0029DBFD
		public string DictionaryPath
		{
			get
			{
				return ((string)this._parameters["DictionaryPath"]) ?? "~/App_Data/RadSpell/";
			}
			set
			{
				this._parameters["DictionaryPath"] = value;
			}
		}

		// Token: 0x17003D21 RID: 15649
		// (get) Token: 0x0600BD7F RID: 48511 RVA: 0x0029FA10 File Offset: 0x0029DC10
		// (set) Token: 0x0600BD80 RID: 48512 RVA: 0x0029FA31 File Offset: 0x0029DC31
		public int EditDistance
		{
			get
			{
				return (int)(this._parameters["EditDistance"] ?? 1);
			}
			set
			{
				this._parameters["EditDistance"] = value;
			}
		}

		// Token: 0x17003D22 RID: 15650
		// (get) Token: 0x0600BD81 RID: 48513 RVA: 0x0029FA49 File Offset: 0x0029DC49
		// (set) Token: 0x0600BD82 RID: 48514 RVA: 0x0029FA6A File Offset: 0x0029DC6A
		public FragmentIgnoreOptions FragmentIgnoreOptions
		{
			get
			{
				return (FragmentIgnoreOptions)(this._parameters["FragmentIgnoreOptions"] ?? FragmentIgnoreOptions.None);
			}
			set
			{
				this._parameters["FragmentIgnoreOptions"] = value;
			}
		}

		// Token: 0x17003D23 RID: 15651
		// (get) Token: 0x0600BD83 RID: 48515 RVA: 0x0029FA82 File Offset: 0x0029DC82
		// (set) Token: 0x0600BD84 RID: 48516 RVA: 0x0029FAA2 File Offset: 0x0029DCA2
		public string SpellCheckProviderTypeName
		{
			get
			{
				return (string)(this._parameters["SpellCheckProviderTypeName"] ?? string.Empty);
			}
			set
			{
				this._parameters["SpellCheckProviderTypeName"] = value;
			}
		}

		// Token: 0x17003D24 RID: 15652
		// (get) Token: 0x0600BD85 RID: 48517 RVA: 0x0029FAB5 File Offset: 0x0029DCB5
		// (set) Token: 0x0600BD86 RID: 48518 RVA: 0x0029FAD6 File Offset: 0x0029DCD6
		public SpellCheckProvider SpellCheckProvider
		{
			get
			{
				return (SpellCheckProvider)(this._parameters["SpellCheckProvider"] ?? SpellCheckProvider.PhoneticProvider);
			}
			set
			{
				this._parameters["SpellCheckProvider"] = value;
			}
		}

		// Token: 0x17003D25 RID: 15653
		// (get) Token: 0x0600BD87 RID: 48519 RVA: 0x0029FAEE File Offset: 0x0029DCEE
		// (set) Token: 0x0600BD88 RID: 48520 RVA: 0x0029FB0F File Offset: 0x0029DD0F
		public WordIgnoreOptions WordIgnoreOptions
		{
			get
			{
				return (WordIgnoreOptions)(this._parameters["WordIgnoreOptions"] ?? WordIgnoreOptions.RepeatedWords);
			}
			set
			{
				this._parameters["WordIgnoreOptions"] = value;
			}
		}

		// Token: 0x0600BD89 RID: 48521 RVA: 0x0029FB27 File Offset: 0x0029DD27
		internal string Serialize()
		{
			return this._parameters.Serialize();
		}

		// Token: 0x040031D7 RID: 12759
		internal const string DefaultDictionaryPath = "~/App_Data/RadSpell/";

		// Token: 0x040031D8 RID: 12760
		private readonly DialogParameters _parameters;
	}
}
