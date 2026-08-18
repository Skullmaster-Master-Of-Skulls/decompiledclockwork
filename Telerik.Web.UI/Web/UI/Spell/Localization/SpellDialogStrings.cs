using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Spell.Localization
{
	// Token: 0x02001B4F RID: 6991
	public class SpellDialogStrings : LocalizationStrings
	{
		// Token: 0x170052A4 RID: 21156
		// (get) Token: 0x06010ED2 RID: 69330 RVA: 0x003BFBCC File Offset: 0x003BDDCC
		// (set) Token: 0x06010ED3 RID: 69331 RVA: 0x003BFBD9 File Offset: 0x003BDDD9
		[DefaultValue("Add Custom")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string AddCustom
		{
			get
			{
				return this.GetString("AddCustom");
			}
			set
			{
				this.SetString("AddCustom", value);
			}
		}

		// Token: 0x170052A5 RID: 21157
		// (get) Token: 0x06010ED4 RID: 69332 RVA: 0x003BFBE7 File Offset: 0x003BDDE7
		// (set) Token: 0x06010ED5 RID: 69333 RVA: 0x003BFBF4 File Offset: 0x003BDDF4
		[DefaultValue("Spell Check")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Title
		{
			get
			{
				return this.GetString("Title");
			}
			set
			{
				this.SetString("Title", value);
			}
		}

		// Token: 0x170052A6 RID: 21158
		// (get) Token: 0x06010ED6 RID: 69334 RVA: 0x003BFC02 File Offset: 0x003BDE02
		// (set) Token: 0x06010ED7 RID: 69335 RVA: 0x003BFC0F File Offset: 0x003BDE0F
		[DefaultValue("You don't have permission to access this page or your cookie has expired!\nPlease, refresh the page!")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string NoPermission
		{
			get
			{
				return this.GetString("NoPermission");
			}
			set
			{
				this.SetString("NoPermission", value);
			}
		}

		// Token: 0x170052A7 RID: 21159
		// (get) Token: 0x06010ED8 RID: 69336 RVA: 0x003BFC1D File Offset: 0x003BDE1D
		// (set) Token: 0x06010ED9 RID: 69337 RVA: 0x003BFC2A File Offset: 0x003BDE2A
		[Localizable(true)]
		[DefaultValue("Spell Checking in progress.....")]
		[NotifyParentProperty(true)]
		public string ProgressMessage
		{
			get
			{
				return this.GetString("ProgressMessage");
			}
			set
			{
				this.SetString("ProgressMessage", value);
			}
		}

		// Token: 0x170052A8 RID: 21160
		// (get) Token: 0x06010EDA RID: 69338 RVA: 0x003BFC38 File Offset: 0x003BDE38
		// (set) Token: 0x06010EDB RID: 69339 RVA: 0x003BFC45 File Offset: 0x003BDE45
		[NotifyParentProperty(true)]
		[DefaultValue("Do you want to apply or cancel the changes to the text so far?")]
		[Localizable(true)]
		public string Confirm
		{
			get
			{
				return this.GetString("Confirm");
			}
			set
			{
				this.SetString("Confirm", value);
			}
		}

		// Token: 0x170052A9 RID: 21161
		// (get) Token: 0x06010EDC RID: 69340 RVA: 0x003BFC53 File Offset: 0x003BDE53
		// (set) Token: 0x06010EDD RID: 69341 RVA: 0x003BFC60 File Offset: 0x003BDE60
		[DefaultValue("You have made changes to the text.")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ChangesMade
		{
			get
			{
				return this.GetString("ChangesMade");
			}
			set
			{
				this.SetString("ChangesMade", value);
			}
		}

		// Token: 0x170052AA RID: 21162
		// (get) Token: 0x06010EDE RID: 69342 RVA: 0x003BFC6E File Offset: 0x003BDE6E
		// (set) Token: 0x06010EDF RID: 69343 RVA: 0x003BFC7B File Offset: 0x003BDE7B
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("The Spell Check is complete!")]
		public string SpellCheckComplete
		{
			get
			{
				return this.GetString("SpellCheckComplete");
			}
			set
			{
				this.SetString("SpellCheckComplete", value);
			}
		}

		// Token: 0x170052AB RID: 21163
		// (get) Token: 0x06010EE0 RID: 69344 RVA: 0x003BFC89 File Offset: 0x003BDE89
		// (set) Token: 0x06010EE1 RID: 69345 RVA: 0x003BFC96 File Offset: 0x003BDE96
		[DefaultValue("No suggestions")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Nosuggestions
		{
			get
			{
				return this.GetString("Nosuggestions");
			}
			set
			{
				this.SetString("Nosuggestions", value);
			}
		}

		// Token: 0x170052AC RID: 21164
		// (get) Token: 0x06010EE2 RID: 69346 RVA: 0x003BFCA4 File Offset: 0x003BDEA4
		// (set) Token: 0x06010EE3 RID: 69347 RVA: 0x003BFCB1 File Offset: 0x003BDEB1
		[Localizable(true)]
		[DefaultValue("Undo")]
		[NotifyParentProperty(true)]
		public string Undo
		{
			get
			{
				return this.GetString("Undo");
			}
			set
			{
				this.SetString("Undo", value);
			}
		}

		// Token: 0x170052AD RID: 21165
		// (get) Token: 0x06010EE4 RID: 69348 RVA: 0x003BFCBF File Offset: 0x003BDEBF
		// (set) Token: 0x06010EE5 RID: 69349 RVA: 0x003BFCCC File Offset: 0x003BDECC
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Undo Edit")]
		public string UndoEdit
		{
			get
			{
				return this.GetString("UndoEdit");
			}
			set
			{
				this.SetString("UndoEdit", value);
			}
		}

		// Token: 0x170052AE RID: 21166
		// (get) Token: 0x06010EE6 RID: 69350 RVA: 0x003BFCDA File Offset: 0x003BDEDA
		// (set) Token: 0x06010EE7 RID: 69351 RVA: 0x003BFCE7 File Offset: 0x003BDEE7
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Ignore")]
		public string Ignore
		{
			get
			{
				return this.GetString("Ignore");
			}
			set
			{
				this.SetString("Ignore", value);
			}
		}

		// Token: 0x170052AF RID: 21167
		// (get) Token: 0x06010EE8 RID: 69352 RVA: 0x003BFCF5 File Offset: 0x003BDEF5
		// (set) Token: 0x06010EE9 RID: 69353 RVA: 0x003BFD02 File Offset: 0x003BDF02
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Ignore All")]
		public string IgnoreAll
		{
			get
			{
				return this.GetString("IgnoreAll");
			}
			set
			{
				this.SetString("IgnoreAll", value);
			}
		}

		// Token: 0x170052B0 RID: 21168
		// (get) Token: 0x06010EEA RID: 69354 RVA: 0x003BFD10 File Offset: 0x003BDF10
		// (set) Token: 0x06010EEB RID: 69355 RVA: 0x003BFD1D File Offset: 0x003BDF1D
		[DefaultValue("Close")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string Cancel
		{
			get
			{
				return this.GetString("Cancel");
			}
			set
			{
				this.SetString("Cancel", value);
			}
		}

		// Token: 0x170052B1 RID: 21169
		// (get) Token: 0x06010EEC RID: 69356 RVA: 0x003BFD2B File Offset: 0x003BDF2B
		// (set) Token: 0x06010EED RID: 69357 RVA: 0x003BFD38 File Offset: 0x003BDF38
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Change")]
		public string Change
		{
			get
			{
				return this.GetString("Change");
			}
			set
			{
				this.SetString("Change", value);
			}
		}

		// Token: 0x170052B2 RID: 21170
		// (get) Token: 0x06010EEE RID: 69358 RVA: 0x003BFD46 File Offset: 0x003BDF46
		// (set) Token: 0x06010EEF RID: 69359 RVA: 0x003BFD53 File Offset: 0x003BDF53
		[DefaultValue("Change All")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string ChangeAll
		{
			get
			{
				return this.GetString("ChangeAll");
			}
			set
			{
				this.SetString("ChangeAll", value);
			}
		}

		// Token: 0x170052B3 RID: 21171
		// (get) Token: 0x06010EF0 RID: 69360 RVA: 0x003BFD61 File Offset: 0x003BDF61
		// (set) Token: 0x06010EF1 RID: 69361 RVA: 0x003BFD6E File Offset: 0x003BDF6E
		[DefaultValue("Help")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Help
		{
			get
			{
				return this.GetString("Help");
			}
			set
			{
				this.SetString("Help", value);
			}
		}

		// Token: 0x170052B4 RID: 21172
		// (get) Token: 0x06010EF2 RID: 69362 RVA: 0x003BFD7C File Offset: 0x003BDF7C
		// (set) Token: 0x06010EF3 RID: 69363 RVA: 0x003BFD89 File Offset: 0x003BDF89
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Not in Dictionary:")]
		public string NotInDictionary
		{
			get
			{
				return this.GetString("NotInDictionary");
			}
			set
			{
				this.SetString("NotInDictionary", value);
			}
		}

		// Token: 0x170052B5 RID: 21173
		// (get) Token: 0x06010EF4 RID: 69364 RVA: 0x003BFD97 File Offset: 0x003BDF97
		// (set) Token: 0x06010EF5 RID: 69365 RVA: 0x003BFDA4 File Offset: 0x003BDFA4
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Suggestions:")]
		public string Suggestions
		{
			get
			{
				return this.GetString("Suggestions");
			}
			set
			{
				this.SetString("Suggestions", value);
			}
		}

		// Token: 0x170052B6 RID: 21174
		// (get) Token: 0x06010EF6 RID: 69366 RVA: 0x003BFDB2 File Offset: 0x003BDFB2
		// (set) Token: 0x06010EF7 RID: 69367 RVA: 0x003BFDBF File Offset: 0x003BDFBF
		[DefaultValue("Are you sure you want to add '")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string AddWord1
		{
			get
			{
				return this.GetString("AddWord1");
			}
			set
			{
				this.SetString("AddWord1", value);
			}
		}

		// Token: 0x170052B7 RID: 21175
		// (get) Token: 0x06010EF8 RID: 69368 RVA: 0x003BFDCD File Offset: 0x003BDFCD
		// (set) Token: 0x06010EF9 RID: 69369 RVA: 0x003BFDDA File Offset: 0x003BDFDA
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("' to the custom dictionary?")]
		public string AddWord2
		{
			get
			{
				return this.GetString("AddWord2");
			}
			set
			{
				this.SetString("AddWord2", value);
			}
		}

		// Token: 0x06010EFA RID: 69370 RVA: 0x003BFDE8 File Offset: 0x003BDFE8
		internal SpellDialogStrings(LocalizationProvider provider) : base(provider)
		{
		}
	}
}
