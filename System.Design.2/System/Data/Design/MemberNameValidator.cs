using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data.Design
{
	// Token: 0x0200024F RID: 591
	internal sealed class MemberNameValidator
	{
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x0007CE27 File Offset: 0x0007B027
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x0007CE2F File Offset: 0x0007B02F
		internal bool UseSuffix
		{
			get
			{
				return this.useSuffix;
			}
			set
			{
				this.useSuffix = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0007CE38 File Offset: 0x0007B038
		private static Dictionary<string, string[]> InvalidEverettIdentifiers
		{
			get
			{
				if (MemberNameValidator.invalidEverettIdentifiers == null)
				{
					MemberNameValidator.invalidEverettIdentifiers = new Dictionary<string, string[]>();
					MemberNameValidator.invalidEverettIdentifiers.Add(".vb", MemberNameValidator.invalidEverettIdentifiersVb);
				}
				return MemberNameValidator.invalidEverettIdentifiers;
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0007CE64 File Offset: 0x0007B064
		internal MemberNameValidator(ICollection initialNameSet, CodeDomProvider codeProvider, bool languageCaseInsensitive)
		{
			this.codeProvider = codeProvider;
			this.languageCaseInsensitive = languageCaseInsensitive;
			if (initialNameSet != null)
			{
				this.bookedMemberNames = new ArrayList(initialNameSet.Count);
				using (IEnumerator enumerator = initialNameSet.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						string name = (string)obj;
						this.AddNameToList(name);
					}
					return;
				}
			}
			this.bookedMemberNames = new ArrayList();
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0007CEEC File Offset: 0x0007B0EC
		internal string GetCandidateMemberName(string originalName)
		{
			if (originalName == null)
			{
				throw new InternalException("Member name cannot be null.");
			}
			string text = this.GenerateIdName(originalName);
			string str = text;
			int num = 0;
			while (this.ListContains(text))
			{
				num++;
				text = str + num.ToString(CultureInfo.CurrentCulture);
				if (!this.codeProvider.IsValidIdentifier(text))
				{
					throw new InternalException(string.Format(CultureInfo.CurrentCulture, "Unable to generate valid identifier from name: {0}.", new object[]
					{
						originalName
					}));
				}
				if (num > 200)
				{
					throw new InternalException(string.Format(CultureInfo.CurrentCulture, "Unable to generate unique identifier from name: {0}. Too many attempts.", new object[]
					{
						originalName
					}));
				}
			}
			return text;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0007CF8C File Offset: 0x0007B18C
		internal string GetNewMemberName(string originalName)
		{
			string candidateMemberName = this.GetCandidateMemberName(originalName);
			this.AddNameToList(candidateMemberName);
			return candidateMemberName;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0007CFA9 File Offset: 0x0007B1A9
		internal string GenerateIdName(string name)
		{
			return MemberNameValidator.GenerateIdName(name, this.codeProvider, this.UseSuffix);
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0007CFBD File Offset: 0x0007B1BD
		internal static string GenerateIdName(string name, CodeDomProvider codeProvider, bool useSuffix)
		{
			return MemberNameValidator.GenerateIdName(name, codeProvider, useSuffix, 100);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0007CFCC File Offset: 0x0007B1CC
		internal static string GenerateIdName(string name, CodeDomProvider codeProvider, bool useSuffix, int additionalCharsToTruncate)
		{
			if (!useSuffix)
			{
				name = MemberNameValidator.GetBackwardCompatibleIdentifier(name, codeProvider);
			}
			if (codeProvider.IsValidIdentifier(name))
			{
				return name;
			}
			string text = name.Replace(' ', '_');
			if (!codeProvider.IsValidIdentifier(text))
			{
				if (!useSuffix)
				{
					text = "_" + text;
				}
				for (int i = 0; i < text.Length; i++)
				{
					UnicodeCategory unicodeCategory = char.GetUnicodeCategory(text[i]);
					if (unicodeCategory != UnicodeCategory.UppercaseLetter && UnicodeCategory.LowercaseLetter != unicodeCategory && UnicodeCategory.TitlecaseLetter != unicodeCategory && UnicodeCategory.ModifierLetter != unicodeCategory && UnicodeCategory.OtherLetter != unicodeCategory && UnicodeCategory.NonSpacingMark != unicodeCategory && UnicodeCategory.SpacingCombiningMark != unicodeCategory && UnicodeCategory.DecimalDigitNumber != unicodeCategory && UnicodeCategory.ConnectorPunctuation != unicodeCategory)
					{
						text = text.Replace(text[i], '_');
					}
				}
			}
			int num = 0;
			string text2 = text;
			while (!codeProvider.IsValidIdentifier(text) && num < 200)
			{
				num++;
				text = "_" + text;
			}
			if (num >= 200)
			{
				text = text2;
				while (!codeProvider.IsValidIdentifier(text) && text.Length > 0)
				{
					text = text.Remove(text.Length - 1);
				}
				if (text.Length == 0)
				{
					return text2;
				}
				if (additionalCharsToTruncate > 0 && text.Length > additionalCharsToTruncate && codeProvider.IsValidIdentifier(text.Remove(text.Length - additionalCharsToTruncate)))
				{
					text = text.Remove(text.Length - additionalCharsToTruncate);
				}
			}
			return text;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0007D0FC File Offset: 0x0007B2FC
		private void AddNameToList(string name)
		{
			if (this.languageCaseInsensitive)
			{
				this.bookedMemberNames.Add(name.ToUpperInvariant());
				return;
			}
			this.bookedMemberNames.Add(name);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0007D126 File Offset: 0x0007B326
		private bool ListContains(string name)
		{
			if (this.languageCaseInsensitive)
			{
				return this.bookedMemberNames.Contains(name.ToUpperInvariant());
			}
			return this.bookedMemberNames.Contains(name);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0007D150 File Offset: 0x0007B350
		private static string GetBackwardCompatibleIdentifier(string identifier, CodeDomProvider provider)
		{
			string text = "." + provider.FileExtension;
			if (text.StartsWith("..", StringComparison.Ordinal))
			{
				text = text.Substring(1);
			}
			if (MemberNameValidator.InvalidEverettIdentifiers.ContainsKey(text))
			{
				string[] array = MemberNameValidator.InvalidEverettIdentifiers[text];
				if (array != null)
				{
					bool caseInsensitive = (provider.LanguageOptions & LanguageOptions.CaseInsensitive) > LanguageOptions.None;
					for (int i = 0; i < array.Length; i++)
					{
						if (StringUtil.EqualValue(identifier, array[i], caseInsensitive))
						{
							return "_" + identifier;
						}
					}
				}
			}
			return identifier;
		}

		// Token: 0x04000B99 RID: 2969
		private const int maxGenerationAttempts = 200;

		// Token: 0x04000B9A RID: 2970
		private const int additionalTruncationChars = 100;

		// Token: 0x04000B9B RID: 2971
		private ArrayList bookedMemberNames;

		// Token: 0x04000B9C RID: 2972
		private CodeDomProvider codeProvider;

		// Token: 0x04000B9D RID: 2973
		private bool languageCaseInsensitive;

		// Token: 0x04000B9E RID: 2974
		private bool useSuffix;

		// Token: 0x04000B9F RID: 2975
		private static string[] invalidEverettIdentifiersVb = new string[]
		{
			"region",
			"externalsource"
		};

		// Token: 0x04000BA0 RID: 2976
		private static Dictionary<string, string[]> invalidEverettIdentifiers = null;
	}
}
