using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000520 RID: 1312
	internal static class XPathCharTypes
	{
		// Token: 0x060031EE RID: 12782 RVA: 0x000BFD00 File Offset: 0x000BDF00
		static XPathCharTypes()
		{
			if (XPathCharTypes.charProperties != null)
			{
				return;
			}
			XPathCharTypes.charProperties = new byte[65535];
			XPathCharTypes.SetProperties("AZazÀÖØöøÿĀıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂぁゔァヺㄅㄬ가힣", 1);
			XPathCharTypes.SetProperties("一龥〇〇〡〩", 1);
			XPathCharTypes.SetProperties("ֹֻֽֿֿׁׂًْٰٰ֑֣̀҃҆֡ׄׄۖۜ͠͡ͅ۝۪ۭ۟۠ۤۧۨँः़़ाौ््॑॔ॢॣঁঃ়়াািিীৄেৈো্ৗৗৢৣਂਂ਼਼ਾਾਿਿੀੂੇੈੋ੍ੰੱઁઃ઼઼ાૅેૉો્ଁଃ଼଼ାୃେୈୋ୍ୖୗஂஃாூெைொ்ௗௗఁఃాౄెైొ్ౕౖಂಃಾೄೆೈೊ್ೕೖംഃാൃെൈൊ്ൗൗััิฺ็๎ັັິູົຼ່ໍ༹༹༘༙༵༵༷༷༾༾༿༿྄ཱ྆ྋྐྕྗྗྙྭྱྷྐྵྐྵ゙゙゚゚〪〯⃐⃜⃡⃡", 2);
			XPathCharTypes.SetProperties("09٠٩۰۹०९০৯੦੯૦૯୦୯௧௯౦౯೦೯൦൯๐๙໐໙༠༩", 4);
			XPathCharTypes.SetProperties("··ːːˑˑ··ــๆๆໆໆ々々〱〵ゝゞーヾ", 8);
			XPathCharTypes.SetProperties("  \t\t\r\r\n\n", 16);
			XPathCharTypes.SetProperties("AZazÀÖØöøÿĀıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂぁゔァヺㄅㄬ가힣", 64);
			XPathCharTypes.SetProperties("一龥〇〇〡〩", 64);
			XPathCharTypes.SetProperties("__", 64);
			XPathCharTypes.SetProperties("AZazÀÖØöøÿĀıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂぁゔァヺㄅㄬ가힣", 32);
			XPathCharTypes.SetProperties("一龥〇〇〡〩", 32);
			XPathCharTypes.SetProperties("09٠٩۰۹०९০৯੦੯૦૯୦୯௧௯౦౯೦೯൦൯๐๙໐໙༠༩", 32);
			XPathCharTypes.SetProperties("ֹֻֽֿֿׁׂًْٰٰ֑֣̀҃҆֡ׄׄۖۜ͠͡ͅ۝۪ۭ۟۠ۤۧۨँः़़ाौ््॑॔ॢॣঁঃ়়াািিীৄেৈো্ৗৗৢৣਂਂ਼਼ਾਾਿਿੀੂੇੈੋ੍ੰੱઁઃ઼઼ાૅેૉો્ଁଃ଼଼ାୃେୈୋ୍ୖୗஂஃாூெைொ்ௗௗఁఃాౄెైొ్ౕౖಂಃಾೄೆೈೊ್ೕೖംഃാൃെൈൊ്ൗൗััิฺ็๎ັັິູົຼ່ໍ༹༹༘༙༵༵༷༷༾༾༿༿྄ཱ྆ྋྐྕྗྗྙྭྱྷྐྵྐྵ゙゙゚゚〪〯⃐⃜⃡⃡", 32);
			XPathCharTypes.SetProperties("··ːːˑˑ··ــๆๆໆໆ々々〱〵ゝゞーヾ", 32);
			XPathCharTypes.SetProperties("..--__", 32);
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000BFDD4 File Offset: 0x000BDFD4
		private static void SetProperties(string ranges, byte value)
		{
			for (int i = 0; i < ranges.Length; i += 2)
			{
				int j = (int)ranges[i];
				int num = (int)ranges[i + 1];
				while (j <= num)
				{
					byte[] array = XPathCharTypes.charProperties;
					int num2 = j;
					array[num2] |= value;
					j++;
				}
			}
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000BFE1F File Offset: 0x000BE01F
		private static byte GetCode(char c)
		{
			return XPathCharTypes.charProperties[(int)c];
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000BFE28 File Offset: 0x000BE028
		internal static bool IsDigit(char c)
		{
			return (XPathCharTypes.GetCode(c) & 4) > 0;
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000BFE35 File Offset: 0x000BE035
		internal static bool IsWhitespace(char c)
		{
			return (XPathCharTypes.GetCode(c) & 16) > 0;
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000BFE43 File Offset: 0x000BE043
		internal static bool IsNCName(char c)
		{
			return (XPathCharTypes.GetCode(c) & 32) > 0;
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000BFE51 File Offset: 0x000BE051
		internal static bool IsNCNameStart(char c)
		{
			return (XPathCharTypes.GetCode(c) & 64) > 0;
		}

		// Token: 0x04002681 RID: 9857
		private static byte[] charProperties;

		// Token: 0x04002682 RID: 9858
		private const byte None = 0;

		// Token: 0x04002683 RID: 9859
		private const byte Letter = 1;

		// Token: 0x04002684 RID: 9860
		private const byte Combining = 2;

		// Token: 0x04002685 RID: 9861
		private const byte Digit = 4;

		// Token: 0x04002686 RID: 9862
		private const byte Extender = 8;

		// Token: 0x04002687 RID: 9863
		private const byte Whitespace = 16;

		// Token: 0x04002688 RID: 9864
		private const byte NCName = 32;

		// Token: 0x04002689 RID: 9865
		private const byte NCNameStart = 64;

		// Token: 0x0400268A RID: 9866
		private const string BaseChars = "AZazÀÖØöøÿĀıĴľŁňŊžƀǃǍǰǴǵǺȗɐʨʻˁΆΆΈΊΌΌΎΡΣώϐϖϚϚϜϜϞϞϠϠϢϳЁЌЎяёќўҁҐӄӇӈӋӌӐӫӮӵӸӹԱՖՙՙաֆאתװײءغفيٱڷںھۀێېۓەەۥۦअहऽऽक़ॡঅঌএঐওনপরললশহড়ঢ়য়ৡৰৱਅਊਏਐਓਨਪਰਲਲ਼ਵਸ਼ਸਹਖ਼ੜਫ਼ਫ਼ੲੴઅઋઍઍએઑઓનપરલળવહઽઽૠૠଅଌଏଐଓନପରଲଳଶହଽଽଡ଼ଢ଼ୟୡஅஊஎஐஒகஙசஜஜஞடணதநபமவஷஹఅఌఎఐఒనపళవహౠౡಅಌಎಐಒನಪಳವಹೞೞೠೡഅഌഎഐഒനപഹൠൡกฮะะาำเๅກຂຄຄງຈຊຊຍຍດທນຟມຣລລວວສຫອຮະະາຳຽຽເໄཀཇཉཀྵႠჅაჶᄀᄀᄂᄃᄅᄇᄉᄉᄋᄌᄎᄒᄼᄼᄾᄾᅀᅀᅌᅌᅎᅎᅐᅐᅔᅕᅙᅙᅟᅡᅣᅣᅥᅥᅧᅧᅩᅩᅭᅮᅲᅳᅵᅵᆞᆞᆨᆨᆫᆫᆮᆯᆷᆸᆺᆺᆼᇂᇫᇫᇰᇰᇹᇹḀẛẠỹἀἕἘἝἠὅὈὍὐὗὙὙὛὛὝὝὟώᾀᾴᾶᾼιιῂῄῆῌῐΐῖΊῠῬῲῴῶῼΩΩKÅ℮℮ↀↂぁゔァヺㄅㄬ가힣";

		// Token: 0x0400268B RID: 9867
		private const string IdeogramicChars = "一龥〇〇〡〩";

		// Token: 0x0400268C RID: 9868
		private const string CombiningChars = "ֹֻֽֿֿׁׂًْٰٰ֑֣̀҃҆֡ׄׄۖۜ͠͡ͅ۝۪ۭ۟۠ۤۧۨँः़़ाौ््॑॔ॢॣঁঃ়়াািিীৄেৈো্ৗৗৢৣਂਂ਼਼ਾਾਿਿੀੂੇੈੋ੍ੰੱઁઃ઼઼ાૅેૉો્ଁଃ଼଼ାୃେୈୋ୍ୖୗஂஃாூெைொ்ௗௗఁఃాౄెైొ్ౕౖಂಃಾೄೆೈೊ್ೕೖംഃാൃെൈൊ്ൗൗััิฺ็๎ັັິູົຼ່ໍ༹༹༘༙༵༵༷༷༾༾༿༿྄ཱ྆ྋྐྕྗྗྙྭྱྷྐྵྐྵ゙゙゚゚〪〯⃐⃜⃡⃡";

		// Token: 0x0400268D RID: 9869
		private const string DigitChars = "09٠٩۰۹०९০৯੦੯૦૯୦୯௧௯౦౯೦೯൦൯๐๙໐໙༠༩";

		// Token: 0x0400268E RID: 9870
		private const string ExtenderChars = "··ːːˑˑ··ــๆๆໆໆ々々〱〵ゝゞーヾ";

		// Token: 0x0400268F RID: 9871
		private const string WhitespaceChars = "  \t\t\r\r\n\n";

		// Token: 0x04002690 RID: 9872
		private const string OtherNCNameStartChars = "__";

		// Token: 0x04002691 RID: 9873
		private const string OtherNCNameChars = "..--__";
	}
}
