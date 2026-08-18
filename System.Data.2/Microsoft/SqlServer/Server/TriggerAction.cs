using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000075 RID: 117
	public enum TriggerAction
	{
		// Token: 0x04000205 RID: 517
		Invalid,
		// Token: 0x04000206 RID: 518
		Insert,
		// Token: 0x04000207 RID: 519
		Update,
		// Token: 0x04000208 RID: 520
		Delete,
		// Token: 0x04000209 RID: 521
		CreateTable = 21,
		// Token: 0x0400020A RID: 522
		AlterTable,
		// Token: 0x0400020B RID: 523
		DropTable,
		// Token: 0x0400020C RID: 524
		CreateIndex,
		// Token: 0x0400020D RID: 525
		AlterIndex,
		// Token: 0x0400020E RID: 526
		DropIndex,
		// Token: 0x0400020F RID: 527
		CreateSynonym = 34,
		// Token: 0x04000210 RID: 528
		DropSynonym = 36,
		// Token: 0x04000211 RID: 529
		CreateSecurityExpression = 31,
		// Token: 0x04000212 RID: 530
		DropSecurityExpression = 33,
		// Token: 0x04000213 RID: 531
		CreateView = 41,
		// Token: 0x04000214 RID: 532
		AlterView,
		// Token: 0x04000215 RID: 533
		DropView,
		// Token: 0x04000216 RID: 534
		CreateProcedure = 51,
		// Token: 0x04000217 RID: 535
		AlterProcedure,
		// Token: 0x04000218 RID: 536
		DropProcedure,
		// Token: 0x04000219 RID: 537
		CreateFunction = 61,
		// Token: 0x0400021A RID: 538
		AlterFunction,
		// Token: 0x0400021B RID: 539
		DropFunction,
		// Token: 0x0400021C RID: 540
		CreateTrigger = 71,
		// Token: 0x0400021D RID: 541
		AlterTrigger,
		// Token: 0x0400021E RID: 542
		DropTrigger,
		// Token: 0x0400021F RID: 543
		CreateEventNotification,
		// Token: 0x04000220 RID: 544
		DropEventNotification = 76,
		// Token: 0x04000221 RID: 545
		CreateType = 91,
		// Token: 0x04000222 RID: 546
		DropType = 93,
		// Token: 0x04000223 RID: 547
		CreateAssembly = 101,
		// Token: 0x04000224 RID: 548
		AlterAssembly,
		// Token: 0x04000225 RID: 549
		DropAssembly,
		// Token: 0x04000226 RID: 550
		CreateUser = 131,
		// Token: 0x04000227 RID: 551
		AlterUser,
		// Token: 0x04000228 RID: 552
		DropUser,
		// Token: 0x04000229 RID: 553
		CreateRole,
		// Token: 0x0400022A RID: 554
		AlterRole,
		// Token: 0x0400022B RID: 555
		DropRole,
		// Token: 0x0400022C RID: 556
		CreateAppRole,
		// Token: 0x0400022D RID: 557
		AlterAppRole,
		// Token: 0x0400022E RID: 558
		DropAppRole,
		// Token: 0x0400022F RID: 559
		CreateSchema = 141,
		// Token: 0x04000230 RID: 560
		AlterSchema,
		// Token: 0x04000231 RID: 561
		DropSchema,
		// Token: 0x04000232 RID: 562
		CreateLogin,
		// Token: 0x04000233 RID: 563
		AlterLogin,
		// Token: 0x04000234 RID: 564
		DropLogin,
		// Token: 0x04000235 RID: 565
		CreateMsgType = 151,
		// Token: 0x04000236 RID: 566
		DropMsgType = 153,
		// Token: 0x04000237 RID: 567
		CreateContract,
		// Token: 0x04000238 RID: 568
		DropContract = 156,
		// Token: 0x04000239 RID: 569
		CreateQueue,
		// Token: 0x0400023A RID: 570
		AlterQueue,
		// Token: 0x0400023B RID: 571
		DropQueue,
		// Token: 0x0400023C RID: 572
		CreateService = 161,
		// Token: 0x0400023D RID: 573
		AlterService,
		// Token: 0x0400023E RID: 574
		DropService,
		// Token: 0x0400023F RID: 575
		CreateRoute,
		// Token: 0x04000240 RID: 576
		AlterRoute,
		// Token: 0x04000241 RID: 577
		DropRoute,
		// Token: 0x04000242 RID: 578
		GrantStatement,
		// Token: 0x04000243 RID: 579
		DenyStatement,
		// Token: 0x04000244 RID: 580
		RevokeStatement,
		// Token: 0x04000245 RID: 581
		GrantObject,
		// Token: 0x04000246 RID: 582
		DenyObject,
		// Token: 0x04000247 RID: 583
		RevokeObject,
		// Token: 0x04000248 RID: 584
		CreateBinding = 174,
		// Token: 0x04000249 RID: 585
		AlterBinding,
		// Token: 0x0400024A RID: 586
		DropBinding,
		// Token: 0x0400024B RID: 587
		CreatePartitionFunction = 191,
		// Token: 0x0400024C RID: 588
		AlterPartitionFunction,
		// Token: 0x0400024D RID: 589
		DropPartitionFunction,
		// Token: 0x0400024E RID: 590
		CreatePartitionScheme,
		// Token: 0x0400024F RID: 591
		AlterPartitionScheme,
		// Token: 0x04000250 RID: 592
		DropPartitionScheme
	}
}
