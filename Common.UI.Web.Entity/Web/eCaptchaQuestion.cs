using System;

namespace TechnoPro.Common.UI.Web.Entity.Web
{
	// Token: 0x02000010 RID: 16
	public enum eCaptchaQuestion
	{
		// Token: 0x04000062 RID: 98
		[CaptchaQuestion("What is fifty five thousand six hundred and eighty three as a number?", new string[]
		{
			"55683",
			"55,683",
			"55, 683"
		})]
		Question1,
		// Token: 0x04000063 RID: 99
		[CaptchaQuestion("Leg, nose, chest and bank: how many body parts in the list?", new string[]
		{
			"3",
			"three"
		})]
		Question2,
		// Token: 0x04000064 RID: 100
		[CaptchaQuestion("Head, house, cake, chips, mosquito or milk: the body part is?", new string[]
		{
			"head"
		})]
		Question3,
		// Token: 0x04000065 RID: 101
		[CaptchaQuestion("Which digit is sixth in the number 6995453?", new string[]
		{
			"5",
			"five"
		})]
		Question4,
		// Token: 0x04000066 RID: 102
		[CaptchaQuestion("Thirteen minus seven equals ?", new string[]
		{
			"6",
			"six"
		})]
		Question5,
		// Token: 0x04000067 RID: 103
		[CaptchaQuestion("Spattering, bowdlerise or dukes : which is the last item in this list?", new string[]
		{
			"dukes"
		})]
		Question6,
		// Token: 0x04000068 RID: 104
		[CaptchaQuestion("Butane, pluperfect, laths, quotients, foxy: the word starting with 'q' is?", new string[]
		{
			"quotients"
		})]
		Question7,
		// Token: 0x04000069 RID: 105
		[CaptchaQuestion("The word 'restoration' has which letter in 9th position?", new string[]
		{
			"i"
		})]
		Question8,
		// Token: 0x0400006A RID: 106
		[CaptchaQuestion("Which day from Wednesday, Saturday, Thursday, Friday or Monday is part of the weekend?", new string[]
		{
			"saturday",
			"sat"
		})]
		Question9,
		// Token: 0x0400006B RID: 107
		[CaptchaQuestion("Which of 3, twenty-nine, 70, 46 or 65 is the lowest?", new string[]
		{
			"3",
			"three"
		})]
		Question10,
		// Token: 0x0400006C RID: 108
		[CaptchaQuestion("Type a letter that is a vowel", new string[]
		{
			"a",
			"e",
			"i",
			"o",
			"u"
		})]
		Question11,
		// Token: 0x0400006D RID: 109
		[CaptchaQuestion("What planet are we on?", new string[]
		{
			"earth"
		})]
		Question12,
		// Token: 0x0400006E RID: 110
		QuestionCount
	}
}
