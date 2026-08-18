using System;

namespace TechnoPro.Common.Core.DataSync
{
	// Token: 0x0200010B RID: 267
	internal enum EDayOfWeek
	{
		// Token: 0x040001E3 RID: 483
		[DayOfWeek(DayOfWeek.Sunday, new string[]
		{
			"sun",
			"u",
			"sunday",
			"su",
			"sund",
			"dimanche",
			"dim"
		})]
		Sunday,
		// Token: 0x040001E4 RID: 484
		[DayOfWeek(DayOfWeek.Monday, new string[]
		{
			"mon",
			"m",
			"mo",
			"monday",
			"mond",
			"lundi",
			"l",
			"lun"
		})]
		Monday,
		// Token: 0x040001E5 RID: 485
		[DayOfWeek(DayOfWeek.Tuesday, new string[]
		{
			"tue",
			"t",
			"tu",
			"tuesday",
			"mardi",
			"mar"
		})]
		Tuesday,
		// Token: 0x040001E6 RID: 486
		[DayOfWeek(DayOfWeek.Wednesday, new string[]
		{
			"wed",
			"w",
			"we",
			"wedn",
			"wednesday",
			"mecredi"
		})]
		Wednesday,
		// Token: 0x040001E7 RID: 487
		[DayOfWeek(DayOfWeek.Thursday, new string[]
		{
			"thu",
			"u",
			"th",
			"thur",
			"thursday",
			"jeudi",
			"j",
			"jeu"
		})]
		Thursday,
		// Token: 0x040001E8 RID: 488
		[DayOfWeek(DayOfWeek.Friday, new string[]
		{
			"fri",
			"f",
			"fr",
			"friday",
			"vendredi",
			"v",
			"ven"
		})]
		Friday,
		// Token: 0x040001E9 RID: 489
		[DayOfWeek(DayOfWeek.Saturday, new string[]
		{
			"sat",
			"s",
			"sa",
			"saturday",
			"samedi",
			"sam"
		})]
		Saturday
	}
}
