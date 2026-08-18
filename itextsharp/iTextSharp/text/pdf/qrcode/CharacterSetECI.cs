using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x02000456 RID: 1110
	public class CharacterSetECI
	{
		// Token: 0x0600257E RID: 9598 RVA: 0x000E36AC File Offset: 0x000E26AC
		private static void Initialize()
		{
			Dictionary<string, CharacterSetECI> dictionary = new Dictionary<string, CharacterSetECI>();
			CharacterSetECI.AddCharacterSet(0, "Cp437", dictionary);
			CharacterSetECI.AddCharacterSet(1, new string[]
			{
				"ISO8859_1",
				"ISO-8859-1"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(2, "Cp437", dictionary);
			CharacterSetECI.AddCharacterSet(3, new string[]
			{
				"ISO8859_1",
				"ISO-8859-1"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(4, new string[]
			{
				"ISO8859_2",
				"ISO-8859-2"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(5, new string[]
			{
				"ISO8859_3",
				"ISO-8859-3"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(6, new string[]
			{
				"ISO8859_4",
				"ISO-8859-4"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(7, new string[]
			{
				"ISO8859_5",
				"ISO-8859-5"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(8, new string[]
			{
				"ISO8859_6",
				"ISO-8859-6"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(9, new string[]
			{
				"ISO8859_7",
				"ISO-8859-7"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(10, new string[]
			{
				"ISO8859_8",
				"ISO-8859-8"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(11, new string[]
			{
				"ISO8859_9",
				"ISO-8859-9"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(12, new string[]
			{
				"ISO8859_10",
				"ISO-8859-10"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(13, new string[]
			{
				"ISO8859_11",
				"ISO-8859-11"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(15, new string[]
			{
				"ISO8859_13",
				"ISO-8859-13"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(16, new string[]
			{
				"ISO8859_14",
				"ISO-8859-14"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(17, new string[]
			{
				"ISO8859_15",
				"ISO-8859-15"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(18, new string[]
			{
				"ISO8859_16",
				"ISO-8859-16"
			}, dictionary);
			CharacterSetECI.AddCharacterSet(20, new string[]
			{
				"SJIS",
				"Shift_JIS"
			}, dictionary);
			CharacterSetECI.NAME_TO_ECI = dictionary;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000E392E File Offset: 0x000E292E
		private CharacterSetECI(int value, string encodingName)
		{
			this.value = value;
			this.encodingName = encodingName;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000E3944 File Offset: 0x000E2944
		public string GetEncodingName()
		{
			return this.encodingName;
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000E394C File Offset: 0x000E294C
		public int GetValue()
		{
			return this.value;
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000E3954 File Offset: 0x000E2954
		private static void AddCharacterSet(int value, string encodingName, Dictionary<string, CharacterSetECI> n)
		{
			CharacterSetECI characterSetECI = new CharacterSetECI(value, encodingName);
			n[encodingName] = characterSetECI;
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000E3974 File Offset: 0x000E2974
		private static void AddCharacterSet(int value, string[] encodingNames, Dictionary<string, CharacterSetECI> n)
		{
			CharacterSetECI characterSetECI = new CharacterSetECI(value, encodingNames[0]);
			for (int i = 0; i < encodingNames.Length; i++)
			{
				n[encodingNames[i]] = characterSetECI;
			}
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000E39A4 File Offset: 0x000E29A4
		public static CharacterSetECI GetCharacterSetECIByName(string name)
		{
			if (CharacterSetECI.NAME_TO_ECI == null)
			{
				CharacterSetECI.Initialize();
			}
			CharacterSetECI result;
			CharacterSetECI.NAME_TO_ECI.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x04001A2A RID: 6698
		private static Dictionary<string, CharacterSetECI> NAME_TO_ECI;

		// Token: 0x04001A2B RID: 6699
		private string encodingName;

		// Token: 0x04001A2C RID: 6700
		private int value;
	}
}
