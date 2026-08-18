using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200068F RID: 1679
	internal sealed class RegexCharClass
	{
		// Token: 0x06003E09 RID: 15881 RVA: 0x000FE230 File Offset: 0x000FC430
		static RegexCharClass()
		{
			string[,] array = new string[112, 2];
			array[0, 0] = "IsAlphabeticPresentationForms";
			array[0, 1] = "ﬀﭐ";
			array[1, 0] = "IsArabic";
			array[1, 1] = "؀܀";
			array[2, 0] = "IsArabicPresentationForms-A";
			array[2, 1] = "ﭐ︀";
			array[3, 0] = "IsArabicPresentationForms-B";
			array[3, 1] = "ﹰ＀";
			array[4, 0] = "IsArmenian";
			array[4, 1] = "԰֐";
			array[5, 0] = "IsArrows";
			array[5, 1] = "←∀";
			array[6, 0] = "IsBasicLatin";
			array[6, 1] = "\0\u0080";
			array[7, 0] = "IsBengali";
			array[7, 1] = "ঀ਀";
			array[8, 0] = "IsBlockElements";
			array[8, 1] = "▀■";
			array[9, 0] = "IsBopomofo";
			array[9, 1] = "㄀㄰";
			array[10, 0] = "IsBopomofoExtended";
			array[10, 1] = "ㆠ㇀";
			array[11, 0] = "IsBoxDrawing";
			array[11, 1] = "─▀";
			array[12, 0] = "IsBraillePatterns";
			array[12, 1] = "⠀⤀";
			array[13, 0] = "IsBuhid";
			array[13, 1] = "ᝀᝠ";
			array[14, 0] = "IsCJKCompatibility";
			array[14, 1] = "㌀㐀";
			array[15, 0] = "IsCJKCompatibilityForms";
			array[15, 1] = "︰﹐";
			array[16, 0] = "IsCJKCompatibilityIdeographs";
			array[16, 1] = "豈ﬀ";
			array[17, 0] = "IsCJKRadicalsSupplement";
			array[17, 1] = "⺀⼀";
			array[18, 0] = "IsCJKSymbolsandPunctuation";
			array[18, 1] = "\u3000぀";
			array[19, 0] = "IsCJKUnifiedIdeographs";
			array[19, 1] = "一ꀀ";
			array[20, 0] = "IsCJKUnifiedIdeographsExtensionA";
			array[20, 1] = "㐀䷀";
			array[21, 0] = "IsCherokee";
			array[21, 1] = "Ꭰ᐀";
			array[22, 0] = "IsCombiningDiacriticalMarks";
			array[22, 1] = "̀Ͱ";
			array[23, 0] = "IsCombiningDiacriticalMarksforSymbols";
			array[23, 1] = "⃐℀";
			array[24, 0] = "IsCombiningHalfMarks";
			array[24, 1] = "︠︰";
			array[25, 0] = "IsCombiningMarksforSymbols";
			array[25, 1] = "⃐℀";
			array[26, 0] = "IsControlPictures";
			array[26, 1] = "␀⑀";
			array[27, 0] = "IsCurrencySymbols";
			array[27, 1] = "₠⃐";
			array[28, 0] = "IsCyrillic";
			array[28, 1] = "ЀԀ";
			array[29, 0] = "IsCyrillicSupplement";
			array[29, 1] = "Ԁ԰";
			array[30, 0] = "IsDevanagari";
			array[30, 1] = "ऀঀ";
			array[31, 0] = "IsDingbats";
			array[31, 1] = "✀⟀";
			array[32, 0] = "IsEnclosedAlphanumerics";
			array[32, 1] = "①─";
			array[33, 0] = "IsEnclosedCJKLettersandMonths";
			array[33, 1] = "㈀㌀";
			array[34, 0] = "IsEthiopic";
			array[34, 1] = "ሀᎀ";
			array[35, 0] = "IsGeneralPunctuation";
			array[35, 1] = "\u2000⁰";
			array[36, 0] = "IsGeometricShapes";
			array[36, 1] = "■☀";
			array[37, 0] = "IsGeorgian";
			array[37, 1] = "Ⴀᄀ";
			array[38, 0] = "IsGreek";
			array[38, 1] = "ͰЀ";
			array[39, 0] = "IsGreekExtended";
			array[39, 1] = "ἀ\u2000";
			array[40, 0] = "IsGreekandCoptic";
			array[40, 1] = "ͰЀ";
			array[41, 0] = "IsGujarati";
			array[41, 1] = "઀଀";
			array[42, 0] = "IsGurmukhi";
			array[42, 1] = "਀઀";
			array[43, 0] = "IsHalfwidthandFullwidthForms";
			array[43, 1] = "＀￰";
			array[44, 0] = "IsHangulCompatibilityJamo";
			array[44, 1] = "㄰㆐";
			array[45, 0] = "IsHangulJamo";
			array[45, 1] = "ᄀሀ";
			array[46, 0] = "IsHangulSyllables";
			array[46, 1] = "가ힰ";
			array[47, 0] = "IsHanunoo";
			array[47, 1] = "ᜠᝀ";
			array[48, 0] = "IsHebrew";
			array[48, 1] = "֐؀";
			array[49, 0] = "IsHighPrivateUseSurrogates";
			array[49, 1] = "\udb80\udc00";
			array[50, 0] = "IsHighSurrogates";
			array[50, 1] = "\ud800\udb80";
			array[51, 0] = "IsHiragana";
			array[51, 1] = "぀゠";
			array[52, 0] = "IsIPAExtensions";
			array[52, 1] = "ɐʰ";
			array[53, 0] = "IsIdeographicDescriptionCharacters";
			array[53, 1] = "⿰\u3000";
			array[54, 0] = "IsKanbun";
			array[54, 1] = "㆐ㆠ";
			array[55, 0] = "IsKangxiRadicals";
			array[55, 1] = "⼀⿠";
			array[56, 0] = "IsKannada";
			array[56, 1] = "ಀഀ";
			array[57, 0] = "IsKatakana";
			array[57, 1] = "゠㄀";
			array[58, 0] = "IsKatakanaPhoneticExtensions";
			array[58, 1] = "ㇰ㈀";
			array[59, 0] = "IsKhmer";
			array[59, 1] = "ក᠀";
			array[60, 0] = "IsKhmerSymbols";
			array[60, 1] = "᧠ᨀ";
			array[61, 0] = "IsLao";
			array[61, 1] = "຀ༀ";
			array[62, 0] = "IsLatin-1Supplement";
			array[62, 1] = "\u0080Ā";
			array[63, 0] = "IsLatinExtended-A";
			array[63, 1] = "Āƀ";
			array[64, 0] = "IsLatinExtended-B";
			array[64, 1] = "ƀɐ";
			array[65, 0] = "IsLatinExtendedAdditional";
			array[65, 1] = "Ḁἀ";
			array[66, 0] = "IsLetterlikeSymbols";
			array[66, 1] = "℀⅐";
			array[67, 0] = "IsLimbu";
			array[67, 1] = "ᤀᥐ";
			array[68, 0] = "IsLowSurrogates";
			array[68, 1] = "\udc00";
			array[69, 0] = "IsMalayalam";
			array[69, 1] = "ഀ඀";
			array[70, 0] = "IsMathematicalOperators";
			array[70, 1] = "∀⌀";
			array[71, 0] = "IsMiscellaneousMathematicalSymbols-A";
			array[71, 1] = "⟀⟰";
			array[72, 0] = "IsMiscellaneousMathematicalSymbols-B";
			array[72, 1] = "⦀⨀";
			array[73, 0] = "IsMiscellaneousSymbols";
			array[73, 1] = "☀✀";
			array[74, 0] = "IsMiscellaneousSymbolsandArrows";
			array[74, 1] = "⬀Ⰰ";
			array[75, 0] = "IsMiscellaneousTechnical";
			array[75, 1] = "⌀␀";
			array[76, 0] = "IsMongolian";
			array[76, 1] = "᠀ᢰ";
			array[77, 0] = "IsMyanmar";
			array[77, 1] = "ကႠ";
			array[78, 0] = "IsNumberForms";
			array[78, 1] = "⅐←";
			array[79, 0] = "IsOgham";
			array[79, 1] = "\u1680ᚠ";
			array[80, 0] = "IsOpticalCharacterRecognition";
			array[80, 1] = "⑀①";
			array[81, 0] = "IsOriya";
			array[81, 1] = "଀஀";
			array[82, 0] = "IsPhoneticExtensions";
			array[82, 1] = "ᴀᶀ";
			array[83, 0] = "IsPrivateUse";
			array[83, 1] = "豈";
			array[84, 0] = "IsPrivateUseArea";
			array[84, 1] = "豈";
			array[85, 0] = "IsRunic";
			array[85, 1] = "ᚠᜀ";
			array[86, 0] = "IsSinhala";
			array[86, 1] = "඀฀";
			array[87, 0] = "IsSmallFormVariants";
			array[87, 1] = "﹐ﹰ";
			array[88, 0] = "IsSpacingModifierLetters";
			array[88, 1] = "ʰ̀";
			array[89, 0] = "IsSpecials";
			array[89, 1] = "￰";
			array[90, 0] = "IsSuperscriptsandSubscripts";
			array[90, 1] = "⁰₠";
			array[91, 0] = "IsSupplementalArrows-A";
			array[91, 1] = "⟰⠀";
			array[92, 0] = "IsSupplementalArrows-B";
			array[92, 1] = "⤀⦀";
			array[93, 0] = "IsSupplementalMathematicalOperators";
			array[93, 1] = "⨀⬀";
			array[94, 0] = "IsSyriac";
			array[94, 1] = "܀ݐ";
			array[95, 0] = "IsTagalog";
			array[95, 1] = "ᜀᜠ";
			array[96, 0] = "IsTagbanwa";
			array[96, 1] = "ᝠក";
			array[97, 0] = "IsTaiLe";
			array[97, 1] = "ᥐᦀ";
			array[98, 0] = "IsTamil";
			array[98, 1] = "஀ఀ";
			array[99, 0] = "IsTelugu";
			array[99, 1] = "ఀಀ";
			array[100, 0] = "IsThaana";
			array[100, 1] = "ހ߀";
			array[101, 0] = "IsThai";
			array[101, 1] = "฀຀";
			array[102, 0] = "IsTibetan";
			array[102, 1] = "ༀက";
			array[103, 0] = "IsUnifiedCanadianAboriginalSyllabics";
			array[103, 1] = "᐀\u1680";
			array[104, 0] = "IsVariationSelectors";
			array[104, 1] = "︀︐";
			array[105, 0] = "IsYiRadicals";
			array[105, 1] = "꒐ꓐ";
			array[106, 0] = "IsYiSyllables";
			array[106, 1] = "ꀀ꒐";
			array[107, 0] = "IsYijingHexagramSymbols";
			array[107, 1] = "䷀一";
			array[108, 0] = "_xmlC";
			array[108, 1] = "-/0;A[_`a{·¸À×Ø÷øĲĴĿŁŉŊſƀǄǍǱǴǶǺȘɐʩʻ˂ː˒̀͆͢͠Ά΋Ό΍Ύ΢ΣϏϐϗϚϛϜϝϞϟϠϡϢϴЁЍЎѐёѝў҂҃҇ҐӅӇӉӋӍӐӬӮӶӸӺԱ՗ՙ՚աևֺֻ֑֢֣־ֿ׀ׁ׃ׅׄא׫װ׳ءػـٓ٠٪ٰڸںڿۀۏې۔ە۩۪ۮ۰ۺँऄअऺ़ॎ॑ॕक़।०॰ঁ঄অ঍এ঑ও঩প঱ল঳শ঺়ঽা৅ে৉োৎৗ৘ড়৞য়৤০৲ਂਃਅ਋ਏ਑ਓ਩ਪ਱ਲ਴ਵ਷ਸ਺਼਽ਾ੃ੇ੉ੋ੎ਖ਼੝ਫ਼੟੦ੵઁ઄અઌઍ઎એ઒ઓ઩પ઱લ઴વ઺઼૆ે૊ો૎ૠૡ૦૰ଁ଄ଅ଍ଏ଑ଓ଩ପ଱ଲ଴ଶ଺଼ୄେ୉ୋ୎ୖ୘ଡ଼୞ୟୢ୦୰ஂ஄அ஋எ஑ஒ஖ங஛ஜ஝ஞ஠ண஥ந஫மஶஷ஺ா௃ெ௉ொ௎ௗ௘௧௰ఁఄఅ఍ఎ఑ఒ఩పఴవ఺ా౅ె౉ొ౎ౕ౗ౠౢ౦౰ಂ಄ಅ಍ಎ಑ಒ಩ಪ಴ವ಺ಾ೅ೆ೉ೊ೎ೕ೗ೞ೟ೠೢ೦೰ംഄഅ഍എ഑ഒഩപഺാൄെ൉ൊൎൗ൘ൠൢ൦൰กฯะ฻เ๏๐๚ກ຃ຄ຅ງຉຊ຋ຍຎດຘນຠມ຤ລ຦ວຨສຬອຯະ຺ົ຾ເ໅ໆ໇່໎໐໚༘༚༠༪༵༶༷༸༹༺༾཈ཉཪཱ྅྆ྌྐྖྗ྘ྙྮྱྸྐྵྺႠ჆აჷᄀᄁᄂᄄᄅᄈᄉᄊᄋᄍᄎᄓᄼᄽᄾᄿᅀᅁᅌᅍᅎᅏᅐᅑᅔᅖᅙᅚᅟᅢᅣᅤᅥᅦᅧᅨᅩᅪᅭᅯᅲᅴᅵᅶᆞᆟᆨᆩᆫᆬᆮᆰᆷᆹᆺᆻᆼᇃᇫᇬᇰᇱᇹᇺḀẜẠỺἀ἖Ἐ἞ἠ὆Ὀ὎ὐ὘Ὑ὚Ὓ὜Ὕ὞Ὗ὾ᾀ᾵ᾶ᾽ι᾿ῂ῅ῆ῍ῐ῔ῖ῜ῠ῭ῲ῵ῶ´⃐⃝⃡⃢Ω℧Kℬ℮ℯↀↃ々〆〇〈〡〰〱〶ぁゕ゙゛ゝゟァ・ーヿㄅㄭ一龦가힤";
			array[109, 0] = "_xmlD";
			array[109, 1] = "0:٠٪۰ۺ०॰০ৰ੦ੰ૦૰୦୰௧௰౦౰೦೰൦൰๐๚໐໚༠༪၀၊፩፲០៪᠐᠚０：";
			array[110, 0] = "_xmlI";
			array[110, 1] = ":;A[_`a{À×Ø÷øĲĴĿŁŉŊſƀǄǍǱǴǶǺȘɐʩʻ˂Ά·Έ΋Ό΍Ύ΢ΣϏϐϗϚϛϜϝϞϟϠϡϢϴЁЍЎѐёѝў҂ҐӅӇӉӋӍӐӬӮӶӸӺԱ՗ՙ՚աևא׫װ׳ءػفًٱڸںڿۀۏې۔ەۖۥۧअऺऽाक़ॢঅ঍এ঑ও঩প঱ল঳শ঺ড়৞য়ৢৰ৲ਅ਋ਏ਑ਓ਩ਪ਱ਲ਴ਵ਷ਸ਺ਖ਼੝ਫ਼੟ੲੵઅઌઍ઎એ઒ઓ઩પ઱લ઴વ઺ઽાૠૡଅ଍ଏ଑ଓ଩ପ଱ଲ଴ଶ଺ଽାଡ଼୞ୟୢஅ஋எ஑ஒ஖ங஛ஜ஝ஞ஠ண஥ந஫மஶஷ஺అ఍ఎ఑ఒ఩పఴవ఺ౠౢಅ಍ಎ಑ಒ಩ಪ಴ವ಺ೞ೟ೠೢഅ഍എ഑ഒഩപഺൠൢกฯะัาิเๆກ຃ຄ຅ງຉຊ຋ຍຎດຘນຠມ຤ລ຦ວຨສຬອຯະັາິຽ຾ເ໅ཀ཈ཉཪႠ჆აჷᄀᄁᄂᄄᄅᄈᄉᄊᄋᄍᄎᄓᄼᄽᄾᄿᅀᅁᅌᅍᅎᅏᅐᅑᅔᅖᅙᅚᅟᅢᅣᅤᅥᅦᅧᅨᅩᅪᅭᅯᅲᅴᅵᅶᆞᆟᆨᆩᆫᆬᆮᆰᆷᆹᆺᆻᆼᇃᇫᇬᇰᇱᇹᇺḀẜẠỺἀ἖Ἐ἞ἠ὆Ὀ὎ὐ὘Ὑ὚Ὓ὜Ὕ὞Ὗ὾ᾀ᾵ᾶ᾽ι᾿ῂ῅ῆ῍ῐ῔ῖ῜ῠ῭ῲ῵ῶ´Ω℧Kℬ℮ℯↀↃ〇〈〡〪ぁゕァ・ㄅㄭ一龦가힤";
			array[111, 0] = "_xmlW";
			array[111, 1] = "$%+,0:<?A[^_`{|}~\u007f¢«¬­®·¸»¼¿ÀȡȢȴɐʮʰ˯̀͐͠ͰʹͶͺͻ΄·Έ΋Ό΍Ύ΢ΣϏϐϷЀ҇҈ӏӐӶӸӺԀԐԱ՗ՙ՚աֈֺֻ֑֢֣־ֿ׀ׁ׃ׅׄא׫װ׳ءػـٖ٠٪ٮ۔ە۝۞ۮ۰ۿܐܭܰ݋ހ޲ँऄअऺ़ॎॐॕक़।०॰ঁ঄অ঍এ঑ও঩প঱ল঳শ঺়ঽা৅ে৉োৎৗ৘ড়৞য়৤০৻ਂਃਅ਋ਏ਑ਓ਩ਪ਱ਲ਴ਵ਷ਸ਺਼਽ਾ੃ੇ੉ੋ੎ਖ਼੝ਫ਼੟੦ੵઁ઄અઌઍ઎એ઒ઓ઩પ઱લ઴વ઺઼૆ે૊ો૎ૐ૑ૠૡ૦૰ଁ଄ଅ଍ଏ଑ଓ଩ପ଱ଲ଴ଶ଺଼ୄେ୉ୋ୎ୖ୘ଡ଼୞ୟୢ୦ୱஂ஄அ஋எ஑ஒ஖ங஛ஜ஝ஞ஠ண஥ந஫மஶஷ஺ா௃ெ௉ொ௎ௗ௘௧௳ఁఄఅ఍ఎ఑ఒ఩పఴవ఺ా౅ె౉ొ౎ౕ౗ౠౢ౦౰ಂ಄ಅ಍ಎ಑ಒ಩ಪ಴ವ಺ಾ೅ೆ೉ೊ೎ೕ೗ೞ೟ೠೢ೦೰ംഄഅ഍എ഑ഒഩപഺാൄെ൉ൊൎൗ൘ൠൢ൦൰ං඄අ඗ක඲ඳ඼ල඾ව෇්෋ා෕ූ෗ෘ෠ෲ෴ก฻฿๏๐๚ກ຃ຄ຅ງຉຊ຋ຍຎດຘນຠມ຤ລ຦ວຨສຬອ຺ົ຾ເ໅ໆ໇່໎໐໚ໜໞༀ༄༓༺༾཈ཉཫཱ྅྆ྌྐ྘ྙ྽྾࿍࿏࿐ကဢဣဨဩါာဳံ်၀၊ၐၚႠ჆აჹᄀᅚᅟᆣᆨᇺሀሇለቇቈ቉ቊ቎ቐ቗ቘ቙ቚ቞በኇኈ኉ኊ኎ነኯኰ኱ኲ኶ኸ኿ዀ዁ዂ዆ወዏዐ዗ዘዯደጏጐ጑ጒ጖ጘጟጠፇፈ፛፩፽ᎠᏵᐁ᙭ᙯᙷᚁ᚛ᚠ᛫ᛮᛱᜀᜍᜎ᜕ᜠ᜵ᝀ᝔ᝠ᝭ᝮ᝱ᝲ᝴ក។ៗ៘៛៝០៪᠋᠎᠐᠚ᠠᡸᢀᢪḀẜẠỺἀ἖Ἐ἞ἠ὆Ὀ὎ὐ὘Ὑ὚Ὓ὜Ὕ὞Ὗ὾ᾀ᾵ᾶ῅ῆ῔ῖ῜῝῰ῲ῵ῶ῿⁄⁅⁒⁓⁰⁲⁴⁽ⁿ₍₠₲⃫⃐℀℻ℽ⅌⅓ↄ←〈⌫⎴⎷⏏␀␧⑀⑋①⓿─☔☖☘☙♾⚀⚊✁✅✆✊✌✨✩❌❍❎❏❓❖❗❘❟❡❨❶➕➘➰➱➿⟐⟦⟰⦃⦙⧘⧜⧼⧾⬀⺀⺚⺛⻴⼀⿖⿰⿼〄〈〒〔〠〰〱〽〾぀ぁ゗゙゠ァ・ー㄀ㄅㄭㄱ㆏㆐ㆸㇰ㈝㈠㉄㉑㉼㉿㋌㋐㋿㌀㍷㍻㏞㏠㏿㐀䶶一龦ꀀ꒍꒐꓇가힤豈郞侮恵ﬀ﬇ﬓ﬘יִ﬷טּ﬽מּ﬿נּ﭂ףּ﭅צּ﮲ﯓ﴾ﵐ﶐ﶒ﷈ﷰ﷽︀︐︠︤﹢﹣﹤﹧﹩﹪ﹰ﹵ﹶ﻽＄％＋，０：＜？Ａ［＾＿｀｛｜｝～｟ｦ﾿ￂ￈ￊ￐ￒ￘ￚ￝￠￧￨￯￼￾";
			RegexCharClass._propTable = array;
			RegexCharClass._lcTable = new RegexCharClass.LowerCaseMapping[]
			{
				new RegexCharClass.LowerCaseMapping('A', 'Z', 1, 32),
				new RegexCharClass.LowerCaseMapping('À', 'Þ', 1, 32),
				new RegexCharClass.LowerCaseMapping('Ā', 'Į', 2, 0),
				new RegexCharClass.LowerCaseMapping('İ', 'İ', 0, 105),
				new RegexCharClass.LowerCaseMapping('Ĳ', 'Ķ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ĺ', 'Ň', 3, 0),
				new RegexCharClass.LowerCaseMapping('Ŋ', 'Ŷ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ÿ', 'Ÿ', 0, 255),
				new RegexCharClass.LowerCaseMapping('Ź', 'Ž', 3, 0),
				new RegexCharClass.LowerCaseMapping('Ɓ', 'Ɓ', 0, 595),
				new RegexCharClass.LowerCaseMapping('Ƃ', 'Ƅ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ɔ', 'Ɔ', 0, 596),
				new RegexCharClass.LowerCaseMapping('Ƈ', 'Ƈ', 0, 392),
				new RegexCharClass.LowerCaseMapping('Ɖ', 'Ɗ', 1, 205),
				new RegexCharClass.LowerCaseMapping('Ƌ', 'Ƌ', 0, 396),
				new RegexCharClass.LowerCaseMapping('Ǝ', 'Ǝ', 0, 477),
				new RegexCharClass.LowerCaseMapping('Ə', 'Ə', 0, 601),
				new RegexCharClass.LowerCaseMapping('Ɛ', 'Ɛ', 0, 603),
				new RegexCharClass.LowerCaseMapping('Ƒ', 'Ƒ', 0, 402),
				new RegexCharClass.LowerCaseMapping('Ɠ', 'Ɠ', 0, 608),
				new RegexCharClass.LowerCaseMapping('Ɣ', 'Ɣ', 0, 611),
				new RegexCharClass.LowerCaseMapping('Ɩ', 'Ɩ', 0, 617),
				new RegexCharClass.LowerCaseMapping('Ɨ', 'Ɨ', 0, 616),
				new RegexCharClass.LowerCaseMapping('Ƙ', 'Ƙ', 0, 409),
				new RegexCharClass.LowerCaseMapping('Ɯ', 'Ɯ', 0, 623),
				new RegexCharClass.LowerCaseMapping('Ɲ', 'Ɲ', 0, 626),
				new RegexCharClass.LowerCaseMapping('Ɵ', 'Ɵ', 0, 629),
				new RegexCharClass.LowerCaseMapping('Ơ', 'Ƥ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ƨ', 'Ƨ', 0, 424),
				new RegexCharClass.LowerCaseMapping('Ʃ', 'Ʃ', 0, 643),
				new RegexCharClass.LowerCaseMapping('Ƭ', 'Ƭ', 0, 429),
				new RegexCharClass.LowerCaseMapping('Ʈ', 'Ʈ', 0, 648),
				new RegexCharClass.LowerCaseMapping('Ư', 'Ư', 0, 432),
				new RegexCharClass.LowerCaseMapping('Ʊ', 'Ʋ', 1, 217),
				new RegexCharClass.LowerCaseMapping('Ƴ', 'Ƶ', 3, 0),
				new RegexCharClass.LowerCaseMapping('Ʒ', 'Ʒ', 0, 658),
				new RegexCharClass.LowerCaseMapping('Ƹ', 'Ƹ', 0, 441),
				new RegexCharClass.LowerCaseMapping('Ƽ', 'Ƽ', 0, 445),
				new RegexCharClass.LowerCaseMapping('Ǆ', 'ǅ', 0, 454),
				new RegexCharClass.LowerCaseMapping('Ǉ', 'ǈ', 0, 457),
				new RegexCharClass.LowerCaseMapping('Ǌ', 'ǋ', 0, 460),
				new RegexCharClass.LowerCaseMapping('Ǎ', 'Ǜ', 3, 0),
				new RegexCharClass.LowerCaseMapping('Ǟ', 'Ǯ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ǳ', 'ǲ', 0, 499),
				new RegexCharClass.LowerCaseMapping('Ǵ', 'Ǵ', 0, 501),
				new RegexCharClass.LowerCaseMapping('Ǻ', 'Ȗ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ά', 'Ά', 0, 940),
				new RegexCharClass.LowerCaseMapping('Έ', 'Ί', 1, 37),
				new RegexCharClass.LowerCaseMapping('Ό', 'Ό', 0, 972),
				new RegexCharClass.LowerCaseMapping('Ύ', 'Ώ', 1, 63),
				new RegexCharClass.LowerCaseMapping('Α', 'Ϋ', 1, 32),
				new RegexCharClass.LowerCaseMapping('Ϣ', 'Ϯ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ё', 'Џ', 1, 80),
				new RegexCharClass.LowerCaseMapping('А', 'Я', 1, 32),
				new RegexCharClass.LowerCaseMapping('Ѡ', 'Ҁ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ґ', 'Ҿ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ӂ', 'Ӄ', 3, 0),
				new RegexCharClass.LowerCaseMapping('Ӈ', 'Ӈ', 0, 1224),
				new RegexCharClass.LowerCaseMapping('Ӌ', 'Ӌ', 0, 1228),
				new RegexCharClass.LowerCaseMapping('Ӑ', 'Ӫ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ӯ', 'Ӵ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ӹ', 'Ӹ', 0, 1273),
				new RegexCharClass.LowerCaseMapping('Ա', 'Ֆ', 1, 48),
				new RegexCharClass.LowerCaseMapping('Ⴀ', 'Ⴥ', 1, 48),
				new RegexCharClass.LowerCaseMapping('Ḁ', 'Ỹ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ἀ', 'Ἇ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ἐ', '἟', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ἠ', 'Ἧ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ἰ', 'Ἷ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ὀ', 'Ὅ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ὑ', 'Ὑ', 0, 8017),
				new RegexCharClass.LowerCaseMapping('Ὓ', 'Ὓ', 0, 8019),
				new RegexCharClass.LowerCaseMapping('Ὕ', 'Ὕ', 0, 8021),
				new RegexCharClass.LowerCaseMapping('Ὗ', 'Ὗ', 0, 8023),
				new RegexCharClass.LowerCaseMapping('Ὠ', 'Ὧ', 1, -8),
				new RegexCharClass.LowerCaseMapping('ᾈ', 'ᾏ', 1, -8),
				new RegexCharClass.LowerCaseMapping('ᾘ', 'ᾟ', 1, -8),
				new RegexCharClass.LowerCaseMapping('ᾨ', 'ᾯ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ᾰ', 'Ᾱ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ὰ', 'Ά', 1, -74),
				new RegexCharClass.LowerCaseMapping('ᾼ', 'ᾼ', 0, 8115),
				new RegexCharClass.LowerCaseMapping('Ὲ', 'Ή', 1, -86),
				new RegexCharClass.LowerCaseMapping('ῌ', 'ῌ', 0, 8131),
				new RegexCharClass.LowerCaseMapping('Ῐ', 'Ῑ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ὶ', 'Ί', 1, -100),
				new RegexCharClass.LowerCaseMapping('Ῠ', 'Ῡ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ὺ', 'Ύ', 1, -112),
				new RegexCharClass.LowerCaseMapping('Ῥ', 'Ῥ', 0, 8165),
				new RegexCharClass.LowerCaseMapping('Ὸ', 'Ό', 1, -128),
				new RegexCharClass.LowerCaseMapping('Ὼ', 'Ώ', 1, -126),
				new RegexCharClass.LowerCaseMapping('ῼ', 'ῼ', 0, 8179),
				new RegexCharClass.LowerCaseMapping('Ⅰ', 'Ⅿ', 1, 16),
				new RegexCharClass.LowerCaseMapping('Ⓐ', 'ⓐ', 1, 26),
				new RegexCharClass.LowerCaseMapping('Ａ', 'Ｚ', 1, 32)
			};
			Dictionary<string, string> dictionary = new Dictionary<string, string>(32);
			char[] array2 = new char[9];
			StringBuilder stringBuilder = new StringBuilder(11);
			stringBuilder.Append('\0');
			array2[0] = '\0';
			array2[1] = '\u000f';
			dictionary["Cc"] = array2[1].ToString();
			array2[2] = '\u0010';
			dictionary["Cf"] = array2[2].ToString();
			array2[3] = '\u001e';
			dictionary["Cn"] = array2[3].ToString();
			array2[4] = '\u0012';
			dictionary["Co"] = array2[4].ToString();
			array2[5] = '\u0011';
			dictionary["Cs"] = array2[5].ToString();
			array2[6] = '\0';
			dictionary["C"] = new string(array2, 0, 7);
			array2[1] = '\u0002';
			dictionary["Ll"] = array2[1].ToString();
			array2[2] = '\u0004';
			dictionary["Lm"] = array2[2].ToString();
			array2[3] = '\u0005';
			dictionary["Lo"] = array2[3].ToString();
			array2[4] = '\u0003';
			dictionary["Lt"] = array2[4].ToString();
			array2[5] = '\u0001';
			dictionary["Lu"] = array2[5].ToString();
			dictionary["L"] = new string(array2, 0, 7);
			stringBuilder.Append(new string(array2, 1, 5));
			dictionary[RegexCharClass.InternalRegexIgnoreCase] = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}", new object[]
			{
				'\0',
				array2[1],
				array2[4],
				array2[5],
				array2[6]
			});
			array2[1] = '\a';
			dictionary["Mc"] = array2[1].ToString();
			array2[2] = '\b';
			dictionary["Me"] = array2[2].ToString();
			array2[3] = '\u0006';
			dictionary["Mn"] = array2[3].ToString();
			array2[4] = '\0';
			dictionary["M"] = new string(array2, 0, 5);
			stringBuilder.Append(array2[3]);
			array2[1] = '\t';
			dictionary["Nd"] = array2[1].ToString();
			array2[2] = '\n';
			dictionary["Nl"] = array2[2].ToString();
			array2[3] = '\v';
			dictionary["No"] = array2[3].ToString();
			dictionary["N"] = new string(array2, 0, 5);
			stringBuilder.Append(array2[1]);
			array2[1] = '\u0013';
			dictionary["Pc"] = array2[1].ToString();
			array2[2] = '\u0014';
			dictionary["Pd"] = array2[2].ToString();
			array2[3] = '\u0016';
			dictionary["Pe"] = array2[3].ToString();
			array2[4] = '\u0019';
			dictionary["Po"] = array2[4].ToString();
			array2[5] = '\u0015';
			dictionary["Ps"] = array2[5].ToString();
			array2[6] = '\u0018';
			dictionary["Pf"] = array2[6].ToString();
			array2[7] = '\u0017';
			dictionary["Pi"] = array2[7].ToString();
			array2[8] = '\0';
			dictionary["P"] = new string(array2, 0, 9);
			stringBuilder.Append(array2[1]);
			array2[1] = '\u001b';
			dictionary["Sc"] = array2[1].ToString();
			array2[2] = '\u001c';
			dictionary["Sk"] = array2[2].ToString();
			array2[3] = '\u001a';
			dictionary["Sm"] = array2[3].ToString();
			array2[4] = '\u001d';
			dictionary["So"] = array2[4].ToString();
			array2[5] = '\0';
			dictionary["S"] = new string(array2, 0, 6);
			array2[1] = '\r';
			dictionary["Zl"] = array2[1].ToString();
			array2[2] = '\u000e';
			dictionary["Zp"] = array2[2].ToString();
			array2[3] = '\f';
			dictionary["Zs"] = array2[3].ToString();
			array2[4] = '\0';
			dictionary["Z"] = new string(array2, 0, 5);
			stringBuilder.Append('\0');
			RegexCharClass.Word = stringBuilder.ToString();
			RegexCharClass.NotWord = RegexCharClass.NegateCategory(RegexCharClass.Word);
			RegexCharClass.SpaceClass = "\0\0\u0001" + RegexCharClass.Space;
			RegexCharClass.NotSpaceClass = "\u0001\0\u0001" + RegexCharClass.Space;
			RegexCharClass.WordClass = "\0\0" + ((char)RegexCharClass.Word.Length).ToString() + RegexCharClass.Word;
			RegexCharClass.NotWordClass = "\u0001\0" + ((char)RegexCharClass.Word.Length).ToString() + RegexCharClass.Word;
			RegexCharClass.DigitClass = "\0\0\u0001\t";
			RegexCharClass.NotDigitClass = "\0\0\u0001￷";
			RegexCharClass._definedCategories = dictionary;
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x000FFDCB File Offset: 0x000FDFCB
		internal RegexCharClass()
		{
			this._rangelist = new List<RegexCharClass.SingleRange>(6);
			this._canonical = true;
			this._categories = new StringBuilder();
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x000FFDF1 File Offset: 0x000FDFF1
		private RegexCharClass(bool negate, List<RegexCharClass.SingleRange> ranges, StringBuilder categories, RegexCharClass subtraction)
		{
			this._rangelist = ranges;
			this._categories = categories;
			this._canonical = true;
			this._negate = negate;
			this._subtractor = subtraction;
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06003E0C RID: 15884 RVA: 0x000FFE1D File Offset: 0x000FE01D
		internal bool CanMerge
		{
			get
			{
				return !this._negate && this._subtractor == null;
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (set) Token: 0x06003E0D RID: 15885 RVA: 0x000FFE32 File Offset: 0x000FE032
		internal bool Negate
		{
			set
			{
				this._negate = value;
			}
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x000FFE3B File Offset: 0x000FE03B
		internal void AddChar(char c)
		{
			this.AddRange(c, c);
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x000FFE48 File Offset: 0x000FE048
		internal void AddCharClass(RegexCharClass cc)
		{
			if (!cc._canonical)
			{
				this._canonical = false;
			}
			else if (this._canonical && this.RangeCount() > 0 && cc.RangeCount() > 0 && cc.GetRangeAt(0)._first <= this.GetRangeAt(this.RangeCount() - 1)._last)
			{
				this._canonical = false;
			}
			for (int i = 0; i < cc.RangeCount(); i++)
			{
				this._rangelist.Add(cc.GetRangeAt(i));
			}
			this._categories.Append(cc._categories.ToString());
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x000FFEE4 File Offset: 0x000FE0E4
		private void AddSet(string set)
		{
			if (this._canonical && this.RangeCount() > 0 && set.Length > 0 && set[0] <= this.GetRangeAt(this.RangeCount() - 1)._last)
			{
				this._canonical = false;
			}
			int i;
			for (i = 0; i < set.Length - 1; i += 2)
			{
				this._rangelist.Add(new RegexCharClass.SingleRange(set[i], set[i + 1] - '\u0001'));
			}
			if (i < set.Length)
			{
				this._rangelist.Add(new RegexCharClass.SingleRange(set[i], char.MaxValue));
			}
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x000FFF89 File Offset: 0x000FE189
		internal void AddSubtraction(RegexCharClass sub)
		{
			this._subtractor = sub;
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x000FFF94 File Offset: 0x000FE194
		internal void AddRange(char first, char last)
		{
			this._rangelist.Add(new RegexCharClass.SingleRange(first, last));
			if (this._canonical && this._rangelist.Count > 0 && first <= this._rangelist[this._rangelist.Count - 1]._last)
			{
				this._canonical = false;
			}
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x000FFFF0 File Offset: 0x000FE1F0
		internal void AddCategoryFromName(string categoryName, bool invert, bool caseInsensitive, string pattern)
		{
			string text;
			RegexCharClass._definedCategories.TryGetValue(categoryName, out text);
			if (text != null && !categoryName.Equals(RegexCharClass.InternalRegexIgnoreCase))
			{
				string text2 = text;
				if (caseInsensitive && (categoryName.Equals("Ll") || categoryName.Equals("Lu") || categoryName.Equals("Lt")))
				{
					text2 = RegexCharClass._definedCategories[RegexCharClass.InternalRegexIgnoreCase];
				}
				if (invert)
				{
					text2 = RegexCharClass.NegateCategory(text2);
				}
				this._categories.Append(text2);
				return;
			}
			this.AddSet(RegexCharClass.SetFromProperty(categoryName, invert, pattern));
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x0010007E File Offset: 0x000FE27E
		private void AddCategory(string category)
		{
			this._categories.Append(category);
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x00100090 File Offset: 0x000FE290
		internal void AddLowercase(CultureInfo culture)
		{
			this._canonical = false;
			int i = 0;
			int count = this._rangelist.Count;
			while (i < count)
			{
				RegexCharClass.SingleRange singleRange = this._rangelist[i];
				if (singleRange._first == singleRange._last)
				{
					singleRange._first = (singleRange._last = char.ToLower(singleRange._first, culture));
				}
				else
				{
					this.AddLowercaseRange(singleRange._first, singleRange._last, culture);
				}
				i++;
			}
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x00100108 File Offset: 0x000FE308
		private void AddLowercaseRange(char chMin, char chMax, CultureInfo culture)
		{
			int i = 0;
			int num = RegexCharClass._lcTable.Length;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				if (RegexCharClass._lcTable[num2]._chMax < chMin)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2;
				}
			}
			if (i >= RegexCharClass._lcTable.Length)
			{
				return;
			}
			RegexCharClass.LowerCaseMapping lowerCaseMapping;
			while (i < RegexCharClass._lcTable.Length && (lowerCaseMapping = RegexCharClass._lcTable[i])._chMin <= chMax)
			{
				char c;
				if ((c = lowerCaseMapping._chMin) < chMin)
				{
					c = chMin;
				}
				char c2;
				if ((c2 = lowerCaseMapping._chMax) > chMax)
				{
					c2 = chMax;
				}
				switch (lowerCaseMapping._lcOp)
				{
				case 0:
					c = (char)lowerCaseMapping._data;
					c2 = (char)lowerCaseMapping._data;
					break;
				case 1:
					c += (char)lowerCaseMapping._data;
					c2 += (char)lowerCaseMapping._data;
					break;
				case 2:
					c |= '\u0001';
					c2 |= '\u0001';
					break;
				case 3:
					c += (c & '\u0001');
					c2 += (c2 & '\u0001');
					break;
				}
				if (c < chMin || c2 > chMax)
				{
					this.AddRange(c, c2);
				}
				i++;
			}
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x0010021F File Offset: 0x000FE41F
		internal void AddWord(bool ecma, bool negate)
		{
			if (negate)
			{
				if (ecma)
				{
					this.AddSet("\00:A[_`a{İı");
					return;
				}
				this.AddCategory(RegexCharClass.NotWord);
				return;
			}
			else
			{
				if (ecma)
				{
					this.AddSet("0:A[_`a{İı");
					return;
				}
				this.AddCategory(RegexCharClass.Word);
				return;
			}
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x00100259 File Offset: 0x000FE459
		internal void AddSpace(bool ecma, bool negate)
		{
			if (negate)
			{
				if (ecma)
				{
					this.AddSet("\0\t\u000e !");
					return;
				}
				this.AddCategory(RegexCharClass.NotSpace);
				return;
			}
			else
			{
				if (ecma)
				{
					this.AddSet("\t\u000e !");
					return;
				}
				this.AddCategory(RegexCharClass.Space);
				return;
			}
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x00100293 File Offset: 0x000FE493
		internal void AddDigit(bool ecma, bool negate, string pattern)
		{
			if (!ecma)
			{
				this.AddCategoryFromName("Nd", negate, false, pattern);
				return;
			}
			if (negate)
			{
				this.AddSet("\00:");
				return;
			}
			this.AddSet("0:");
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x001002C4 File Offset: 0x000FE4C4
		internal static string ConvertOldStringsToClass(string set, string category)
		{
			StringBuilder stringBuilder = new StringBuilder(set.Length + category.Length + 3);
			if (set.Length >= 2 && set[0] == '\0' && set[1] == '\0')
			{
				stringBuilder.Append('\u0001');
				stringBuilder.Append((char)(set.Length - 2));
				stringBuilder.Append((char)category.Length);
				stringBuilder.Append(set.Substring(2));
			}
			else
			{
				stringBuilder.Append('\0');
				stringBuilder.Append((char)set.Length);
				stringBuilder.Append((char)category.Length);
				stringBuilder.Append(set);
			}
			stringBuilder.Append(category);
			return stringBuilder.ToString();
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x00100371 File Offset: 0x000FE571
		internal static char SingletonChar(string set)
		{
			return set[3];
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x0010037A File Offset: 0x000FE57A
		internal static bool IsMergeable(string charClass)
		{
			return !RegexCharClass.IsNegated(charClass) && !RegexCharClass.IsSubtraction(charClass);
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x0010038F File Offset: 0x000FE58F
		internal static bool IsEmpty(string charClass)
		{
			return charClass[2] == '\0' && charClass[0] == '\0' && charClass[1] == '\0' && !RegexCharClass.IsSubtraction(charClass);
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x001003B8 File Offset: 0x000FE5B8
		internal static bool IsSingleton(string set)
		{
			return set[0] == '\0' && set[2] == '\0' && set[1] == '\u0002' && !RegexCharClass.IsSubtraction(set) && (set[3] == char.MaxValue || set[3] + '\u0001' == set[4]);
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x0010040C File Offset: 0x000FE60C
		internal static bool IsSingletonInverse(string set)
		{
			return set[0] == '\u0001' && set[2] == '\0' && set[1] == '\u0002' && !RegexCharClass.IsSubtraction(set) && (set[3] == char.MaxValue || set[3] + '\u0001' == set[4]);
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x00100461 File Offset: 0x000FE661
		private static bool IsSubtraction(string charClass)
		{
			return charClass.Length > (int)('\u0003' + charClass[1] + charClass[2]);
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x0010047C File Offset: 0x000FE67C
		internal static bool IsNegated(string set)
		{
			return set != null && set[0] == '\u0001';
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x0010048D File Offset: 0x000FE68D
		internal static bool IsECMAWordChar(char ch)
		{
			return RegexCharClass.CharInClass(ch, "\0\n\00:A[_`a{İı");
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x0010049A File Offset: 0x000FE69A
		internal static bool IsWordChar(char ch)
		{
			return RegexCharClass.CharInClass(ch, RegexCharClass.WordClass) || ch == '‍' || ch == '‌';
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x001004BB File Offset: 0x000FE6BB
		internal static bool CharInClass(char ch, string set)
		{
			return RegexCharClass.CharInClassRecursive(ch, set, 0);
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x001004C8 File Offset: 0x000FE6C8
		internal static bool CharInClassRecursive(char ch, string set, int start)
		{
			int num = (int)set[start + 1];
			int num2 = (int)set[start + 2];
			int num3 = start + 3 + num + num2;
			bool flag = false;
			if (set.Length > num3)
			{
				flag = RegexCharClass.CharInClassRecursive(ch, set, num3);
			}
			bool flag2 = RegexCharClass.CharInClassInternal(ch, set, start, num, num2);
			if (set[start] == '\u0001')
			{
				flag2 = !flag2;
			}
			return flag2 && !flag;
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x0010052C File Offset: 0x000FE72C
		private static bool CharInClassInternal(char ch, string set, int start, int mySetLength, int myCategoryLength)
		{
			int num = start + 3;
			int num2 = num + mySetLength;
			while (num != num2)
			{
				int num3 = (num + num2) / 2;
				if (ch < set[num3])
				{
					num2 = num3;
				}
				else
				{
					num = num3 + 1;
				}
			}
			return (num & 1) == (start & 1) || (myCategoryLength != 0 && RegexCharClass.CharInCategory(ch, set, start, mySetLength, myCategoryLength));
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x0010057C File Offset: 0x000FE77C
		private static bool CharInCategory(char ch, string set, int start, int mySetLength, int myCategoryLength)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(ch);
			int i = start + 3 + mySetLength;
			int num = i + myCategoryLength;
			while (i < num)
			{
				int num2 = (int)((short)set[i]);
				if (num2 == 0)
				{
					if (RegexCharClass.CharInCategoryGroup(ch, unicodeCategory, set, ref i))
					{
						return true;
					}
				}
				else if (num2 > 0)
				{
					if (num2 == 100)
					{
						if (char.IsWhiteSpace(ch))
						{
							return true;
						}
						i++;
						continue;
					}
					else
					{
						num2--;
						if (unicodeCategory == (UnicodeCategory)num2)
						{
							return true;
						}
					}
				}
				else if (num2 == -100)
				{
					if (!char.IsWhiteSpace(ch))
					{
						return true;
					}
					i++;
					continue;
				}
				else
				{
					num2 = -1 - num2;
					if (unicodeCategory != (UnicodeCategory)num2)
					{
						return true;
					}
				}
				i++;
			}
			return false;
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x00100604 File Offset: 0x000FE804
		private static bool CharInCategoryGroup(char ch, UnicodeCategory chcategory, string category, ref int i)
		{
			i++;
			int num = (int)((short)category[i]);
			if (num > 0)
			{
				bool flag = false;
				while (num != 0)
				{
					if (!flag)
					{
						num--;
						if (chcategory == (UnicodeCategory)num)
						{
							flag = true;
						}
					}
					i++;
					num = (int)((short)category[i]);
				}
				return flag;
			}
			bool flag2 = true;
			while (num != 0)
			{
				if (flag2)
				{
					num = -1 - num;
					if (chcategory == (UnicodeCategory)num)
					{
						flag2 = false;
					}
				}
				i++;
				num = (int)((short)category[i]);
			}
			return flag2;
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x00100670 File Offset: 0x000FE870
		private static string NegateCategory(string category)
		{
			if (category == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(category.Length);
			foreach (short num in category)
			{
				stringBuilder.Append((char)(-(char)num));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x001006B8 File Offset: 0x000FE8B8
		internal static RegexCharClass Parse(string charClass)
		{
			return RegexCharClass.ParseRecursive(charClass, 0);
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x001006C4 File Offset: 0x000FE8C4
		private static RegexCharClass ParseRecursive(string charClass, int start)
		{
			int num = (int)charClass[start + 1];
			int num2 = (int)charClass[start + 2];
			int num3 = start + 3 + num + num2;
			List<RegexCharClass.SingleRange> list = new List<RegexCharClass.SingleRange>(num);
			int i = start + 3;
			int num4 = i + num;
			while (i < num4)
			{
				char first = charClass[i];
				i++;
				char last;
				if (i < num4)
				{
					last = charClass[i] - '\u0001';
				}
				else
				{
					last = char.MaxValue;
				}
				i++;
				list.Add(new RegexCharClass.SingleRange(first, last));
			}
			RegexCharClass subtraction = null;
			if (charClass.Length > num3)
			{
				subtraction = RegexCharClass.ParseRecursive(charClass, num3);
			}
			return new RegexCharClass(charClass[start] == '\u0001', list, new StringBuilder(charClass.Substring(num4, num2)), subtraction);
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x0010077D File Offset: 0x000FE97D
		private int RangeCount()
		{
			return this._rangelist.Count;
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x0010078C File Offset: 0x000FE98C
		internal string ToStringClass()
		{
			if (!this._canonical)
			{
				this.Canonicalize();
			}
			int num = this._rangelist.Count * 2;
			StringBuilder stringBuilder = new StringBuilder(num + this._categories.Length + 3);
			int num2;
			if (this._negate)
			{
				num2 = 1;
			}
			else
			{
				num2 = 0;
			}
			stringBuilder.Append((char)num2);
			stringBuilder.Append((char)num);
			stringBuilder.Append((char)this._categories.Length);
			for (int i = 0; i < this._rangelist.Count; i++)
			{
				RegexCharClass.SingleRange singleRange = this._rangelist[i];
				stringBuilder.Append(singleRange._first);
				if (singleRange._last != '￿')
				{
					stringBuilder.Append(singleRange._last + '\u0001');
				}
			}
			stringBuilder[1] = (char)(stringBuilder.Length - 3);
			stringBuilder.Append(this._categories);
			if (this._subtractor != null)
			{
				stringBuilder.Append(this._subtractor.ToStringClass());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x0010088B File Offset: 0x000FEA8B
		private RegexCharClass.SingleRange GetRangeAt(int i)
		{
			return this._rangelist[i];
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x0010089C File Offset: 0x000FEA9C
		private void Canonicalize()
		{
			this._canonical = true;
			this._rangelist.Sort(0, this._rangelist.Count, new RegexCharClass.SingleRangeComparer());
			if (this._rangelist.Count > 1)
			{
				bool flag = false;
				int num = 1;
				int num2 = 0;
				for (;;)
				{
					IL_3B:
					char last = this._rangelist[num2]._last;
					while (num != this._rangelist.Count && last != '￿')
					{
						RegexCharClass.SingleRange singleRange;
						if ((singleRange = this._rangelist[num])._first <= last + '\u0001')
						{
							if (last < singleRange._last)
							{
								last = singleRange._last;
							}
							num++;
						}
						else
						{
							IL_96:
							this._rangelist[num2]._last = last;
							num2++;
							if (!flag)
							{
								if (num2 < num)
								{
									this._rangelist[num2] = this._rangelist[num];
								}
								num++;
								goto IL_3B;
							}
							goto IL_D5;
						}
					}
					flag = true;
					goto IL_96;
				}
				IL_D5:
				this._rangelist.RemoveRange(num2, this._rangelist.Count - num2);
			}
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x00100998 File Offset: 0x000FEB98
		private static string SetFromProperty(string capname, bool invert, string pattern)
		{
			int num = 0;
			int num2 = RegexCharClass._propTable.GetLength(0);
			while (num != num2)
			{
				int num3 = (num + num2) / 2;
				int num4 = string.Compare(capname, RegexCharClass._propTable[num3, 0], StringComparison.Ordinal);
				if (num4 < 0)
				{
					num2 = num3;
				}
				else if (num4 > 0)
				{
					num = num3 + 1;
				}
				else
				{
					string text = RegexCharClass._propTable[num3, 1];
					if (!invert)
					{
						return text;
					}
					if (text[0] == '\0')
					{
						return text.Substring(1);
					}
					return "\0" + text;
				}
			}
			throw new ArgumentException(SR.GetString("MakeException", new object[]
			{
				pattern,
				SR.GetString("UnknownProperty", new object[]
				{
					capname
				})
			}));
		}

		// Token: 0x04002D0A RID: 11530
		private List<RegexCharClass.SingleRange> _rangelist;

		// Token: 0x04002D0B RID: 11531
		private StringBuilder _categories;

		// Token: 0x04002D0C RID: 11532
		private bool _canonical;

		// Token: 0x04002D0D RID: 11533
		private bool _negate;

		// Token: 0x04002D0E RID: 11534
		private RegexCharClass _subtractor;

		// Token: 0x04002D0F RID: 11535
		private const int FLAGS = 0;

		// Token: 0x04002D10 RID: 11536
		private const int SETLENGTH = 1;

		// Token: 0x04002D11 RID: 11537
		private const int CATEGORYLENGTH = 2;

		// Token: 0x04002D12 RID: 11538
		private const int SETSTART = 3;

		// Token: 0x04002D13 RID: 11539
		private const char Nullchar = '\0';

		// Token: 0x04002D14 RID: 11540
		private const char Lastchar = '￿';

		// Token: 0x04002D15 RID: 11541
		private const char GroupChar = '\0';

		// Token: 0x04002D16 RID: 11542
		private const short SpaceConst = 100;

		// Token: 0x04002D17 RID: 11543
		private const short NotSpaceConst = -100;

		// Token: 0x04002D18 RID: 11544
		private const char ZeroWidthJoiner = '‍';

		// Token: 0x04002D19 RID: 11545
		private const char ZeroWidthNonJoiner = '‌';

		// Token: 0x04002D1A RID: 11546
		private static readonly string InternalRegexIgnoreCase = "__InternalRegexIgnoreCase__";

		// Token: 0x04002D1B RID: 11547
		private static readonly string Space = "d";

		// Token: 0x04002D1C RID: 11548
		private static readonly string NotSpace = RegexCharClass.NegateCategory(RegexCharClass.Space);

		// Token: 0x04002D1D RID: 11549
		private static readonly string Word;

		// Token: 0x04002D1E RID: 11550
		private static readonly string NotWord;

		// Token: 0x04002D1F RID: 11551
		internal static readonly string SpaceClass;

		// Token: 0x04002D20 RID: 11552
		internal static readonly string NotSpaceClass;

		// Token: 0x04002D21 RID: 11553
		internal static readonly string WordClass;

		// Token: 0x04002D22 RID: 11554
		internal static readonly string NotWordClass;

		// Token: 0x04002D23 RID: 11555
		internal static readonly string DigitClass;

		// Token: 0x04002D24 RID: 11556
		internal static readonly string NotDigitClass;

		// Token: 0x04002D25 RID: 11557
		private const string ECMASpaceSet = "\t\u000e !";

		// Token: 0x04002D26 RID: 11558
		private const string NotECMASpaceSet = "\0\t\u000e !";

		// Token: 0x04002D27 RID: 11559
		private const string ECMAWordSet = "0:A[_`a{İı";

		// Token: 0x04002D28 RID: 11560
		private const string NotECMAWordSet = "\00:A[_`a{İı";

		// Token: 0x04002D29 RID: 11561
		private const string ECMADigitSet = "0:";

		// Token: 0x04002D2A RID: 11562
		private const string NotECMADigitSet = "\00:";

		// Token: 0x04002D2B RID: 11563
		internal const string ECMASpaceClass = "\0\u0004\0\t\u000e !";

		// Token: 0x04002D2C RID: 11564
		internal const string NotECMASpaceClass = "\u0001\u0004\0\t\u000e !";

		// Token: 0x04002D2D RID: 11565
		internal const string ECMAWordClass = "\0\n\00:A[_`a{İı";

		// Token: 0x04002D2E RID: 11566
		internal const string NotECMAWordClass = "\u0001\n\00:A[_`a{İı";

		// Token: 0x04002D2F RID: 11567
		internal const string ECMADigitClass = "\0\u0002\00:";

		// Token: 0x04002D30 RID: 11568
		internal const string NotECMADigitClass = "\u0001\u0002\00:";

		// Token: 0x04002D31 RID: 11569
		internal const string AnyClass = "\0\u0001\0\0";

		// Token: 0x04002D32 RID: 11570
		internal const string EmptyClass = "\0\0\0";

		// Token: 0x04002D33 RID: 11571
		private static Dictionary<string, string> _definedCategories;

		// Token: 0x04002D34 RID: 11572
		private static readonly string[,] _propTable;

		// Token: 0x04002D35 RID: 11573
		private const int LowercaseSet = 0;

		// Token: 0x04002D36 RID: 11574
		private const int LowercaseAdd = 1;

		// Token: 0x04002D37 RID: 11575
		private const int LowercaseBor = 2;

		// Token: 0x04002D38 RID: 11576
		private const int LowercaseBad = 3;

		// Token: 0x04002D39 RID: 11577
		private static readonly RegexCharClass.LowerCaseMapping[] _lcTable;

		// Token: 0x020008B5 RID: 2229
		private struct LowerCaseMapping
		{
			// Token: 0x06004635 RID: 17973 RVA: 0x00125470 File Offset: 0x00123670
			internal LowerCaseMapping(char chMin, char chMax, int lcOp, int data)
			{
				this._chMin = chMin;
				this._chMax = chMax;
				this._lcOp = lcOp;
				this._data = data;
			}

			// Token: 0x04003B63 RID: 15203
			internal char _chMin;

			// Token: 0x04003B64 RID: 15204
			internal char _chMax;

			// Token: 0x04003B65 RID: 15205
			internal int _lcOp;

			// Token: 0x04003B66 RID: 15206
			internal int _data;
		}

		// Token: 0x020008B6 RID: 2230
		private sealed class SingleRangeComparer : IComparer<RegexCharClass.SingleRange>
		{
			// Token: 0x06004636 RID: 17974 RVA: 0x0012548F File Offset: 0x0012368F
			public int Compare(RegexCharClass.SingleRange x, RegexCharClass.SingleRange y)
			{
				if (x._first < y._first)
				{
					return -1;
				}
				if (x._first <= y._first)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x020008B7 RID: 2231
		private sealed class SingleRange
		{
			// Token: 0x06004638 RID: 17976 RVA: 0x001254BA File Offset: 0x001236BA
			internal SingleRange(char first, char last)
			{
				this._first = first;
				this._last = last;
			}

			// Token: 0x04003B67 RID: 15207
			internal char _first;

			// Token: 0x04003B68 RID: 15208
			internal char _last;
		}
	}
}
