using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.ServiceModel.Description
{
	// Token: 0x02000426 RID: 1062
	internal class UniqueCodeIdentifierScope
	{
		// Token: 0x0600290D RID: 10509 RVA: 0x0009C29F File Offset: 0x0009A49F
		protected virtual void AddIdentifier(string identifier)
		{
			if (this.names == null)
			{
				this.names = new SortedList<string, string>(StringComparer.OrdinalIgnoreCase);
			}
			this.names.Add(identifier, identifier);
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x0009C2C6 File Offset: 0x0009A4C6
		public void AddReserved(string identifier)
		{
			this.AddIdentifier(identifier);
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x0009C2D0 File Offset: 0x0009A4D0
		public string AddUnique(string name, string defaultName)
		{
			string text = UniqueCodeIdentifierScope.MakeValid(name, defaultName);
			string text2 = text;
			int num = 1;
			while (!this.IsUnique(text2))
			{
				text2 = text + num++.ToString(CultureInfo.InvariantCulture);
			}
			this.AddIdentifier(text2);
			return text2;
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x0009C315 File Offset: 0x0009A515
		public virtual bool IsUnique(string identifier)
		{
			return this.names == null || !this.names.ContainsKey(identifier);
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x0009C330 File Offset: 0x0009A530
		private static bool IsValidStart(char c)
		{
			return char.GetUnicodeCategory(c) != UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x0009C340 File Offset: 0x0009A540
		private static bool IsValid(char c)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
			return unicodeCategory <= UnicodeCategory.SpacingCombiningMark || unicodeCategory == UnicodeCategory.DecimalDigitNumber || unicodeCategory == UnicodeCategory.ConnectorPunctuation;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x0009C364 File Offset: 0x0009A564
		public static string MakeValid(string identifier, string defaultIdentifier)
		{
			if (string.IsNullOrEmpty(identifier))
			{
				return defaultIdentifier;
			}
			if (identifier.Length <= 511 && CodeGenerator.IsValidLanguageIndependentIdentifier(identifier))
			{
				return identifier;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (num < identifier.Length && stringBuilder.Length < 511)
			{
				char c = identifier[num];
				if (UniqueCodeIdentifierScope.IsValid(c))
				{
					if (stringBuilder.Length == 0 && !UniqueCodeIdentifierScope.IsValidStart(c))
					{
						stringBuilder.Append('_');
					}
					stringBuilder.Append(c);
				}
				num++;
			}
			if (stringBuilder.Length == 0)
			{
				return defaultIdentifier;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002268 RID: 8808
		private const int MaxIdentifierLength = 511;

		// Token: 0x04002269 RID: 8809
		private SortedList<string, string> names;
	}
}
