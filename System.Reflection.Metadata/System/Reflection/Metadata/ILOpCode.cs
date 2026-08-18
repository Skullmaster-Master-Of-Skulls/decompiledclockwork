using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200002D RID: 45
	internal enum ILOpCode : ushort
	{
		// Token: 0x04000189 RID: 393
		Nop,
		// Token: 0x0400018A RID: 394
		Break,
		// Token: 0x0400018B RID: 395
		Ldarg_0,
		// Token: 0x0400018C RID: 396
		Ldarg_1,
		// Token: 0x0400018D RID: 397
		Ldarg_2,
		// Token: 0x0400018E RID: 398
		Ldarg_3,
		// Token: 0x0400018F RID: 399
		Ldloc_0,
		// Token: 0x04000190 RID: 400
		Ldloc_1,
		// Token: 0x04000191 RID: 401
		Ldloc_2,
		// Token: 0x04000192 RID: 402
		Ldloc_3,
		// Token: 0x04000193 RID: 403
		Stloc_0,
		// Token: 0x04000194 RID: 404
		Stloc_1,
		// Token: 0x04000195 RID: 405
		Stloc_2,
		// Token: 0x04000196 RID: 406
		Stloc_3,
		// Token: 0x04000197 RID: 407
		Ldarg_s,
		// Token: 0x04000198 RID: 408
		Ldarga_s,
		// Token: 0x04000199 RID: 409
		Starg_s,
		// Token: 0x0400019A RID: 410
		Ldloc_s,
		// Token: 0x0400019B RID: 411
		Ldloca_s,
		// Token: 0x0400019C RID: 412
		Stloc_s,
		// Token: 0x0400019D RID: 413
		Ldnull,
		// Token: 0x0400019E RID: 414
		Ldc_i4_m1,
		// Token: 0x0400019F RID: 415
		Ldc_i4_0,
		// Token: 0x040001A0 RID: 416
		Ldc_i4_1,
		// Token: 0x040001A1 RID: 417
		Ldc_i4_2,
		// Token: 0x040001A2 RID: 418
		Ldc_i4_3,
		// Token: 0x040001A3 RID: 419
		Ldc_i4_4,
		// Token: 0x040001A4 RID: 420
		Ldc_i4_5,
		// Token: 0x040001A5 RID: 421
		Ldc_i4_6,
		// Token: 0x040001A6 RID: 422
		Ldc_i4_7,
		// Token: 0x040001A7 RID: 423
		Ldc_i4_8,
		// Token: 0x040001A8 RID: 424
		Ldc_i4_s,
		// Token: 0x040001A9 RID: 425
		Ldc_i4,
		// Token: 0x040001AA RID: 426
		Ldc_i8,
		// Token: 0x040001AB RID: 427
		Ldc_r4,
		// Token: 0x040001AC RID: 428
		Ldc_r8,
		// Token: 0x040001AD RID: 429
		Dup = 37,
		// Token: 0x040001AE RID: 430
		Pop,
		// Token: 0x040001AF RID: 431
		Jmp,
		// Token: 0x040001B0 RID: 432
		Call,
		// Token: 0x040001B1 RID: 433
		Calli,
		// Token: 0x040001B2 RID: 434
		Ret,
		// Token: 0x040001B3 RID: 435
		Br_s,
		// Token: 0x040001B4 RID: 436
		Brfalse_s,
		// Token: 0x040001B5 RID: 437
		Brtrue_s,
		// Token: 0x040001B6 RID: 438
		Beq_s,
		// Token: 0x040001B7 RID: 439
		Bge_s,
		// Token: 0x040001B8 RID: 440
		Bgt_s,
		// Token: 0x040001B9 RID: 441
		Ble_s,
		// Token: 0x040001BA RID: 442
		Blt_s,
		// Token: 0x040001BB RID: 443
		Bne_un_s,
		// Token: 0x040001BC RID: 444
		Bge_un_s,
		// Token: 0x040001BD RID: 445
		Bgt_un_s,
		// Token: 0x040001BE RID: 446
		Ble_un_s,
		// Token: 0x040001BF RID: 447
		Blt_un_s,
		// Token: 0x040001C0 RID: 448
		Br,
		// Token: 0x040001C1 RID: 449
		Brfalse,
		// Token: 0x040001C2 RID: 450
		Brtrue,
		// Token: 0x040001C3 RID: 451
		Beq,
		// Token: 0x040001C4 RID: 452
		Bge,
		// Token: 0x040001C5 RID: 453
		Bgt,
		// Token: 0x040001C6 RID: 454
		Ble,
		// Token: 0x040001C7 RID: 455
		Blt,
		// Token: 0x040001C8 RID: 456
		Bne_un,
		// Token: 0x040001C9 RID: 457
		Bge_un,
		// Token: 0x040001CA RID: 458
		Bgt_un,
		// Token: 0x040001CB RID: 459
		Ble_un,
		// Token: 0x040001CC RID: 460
		Blt_un,
		// Token: 0x040001CD RID: 461
		Switch,
		// Token: 0x040001CE RID: 462
		Ldind_i1,
		// Token: 0x040001CF RID: 463
		Ldind_u1,
		// Token: 0x040001D0 RID: 464
		Ldind_i2,
		// Token: 0x040001D1 RID: 465
		Ldind_u2,
		// Token: 0x040001D2 RID: 466
		Ldind_i4,
		// Token: 0x040001D3 RID: 467
		Ldind_u4,
		// Token: 0x040001D4 RID: 468
		Ldind_i8,
		// Token: 0x040001D5 RID: 469
		Ldind_i,
		// Token: 0x040001D6 RID: 470
		Ldind_r4,
		// Token: 0x040001D7 RID: 471
		Ldind_r8,
		// Token: 0x040001D8 RID: 472
		Ldind_ref,
		// Token: 0x040001D9 RID: 473
		Stind_ref,
		// Token: 0x040001DA RID: 474
		Stind_i1,
		// Token: 0x040001DB RID: 475
		Stind_i2,
		// Token: 0x040001DC RID: 476
		Stind_i4,
		// Token: 0x040001DD RID: 477
		Stind_i8,
		// Token: 0x040001DE RID: 478
		Stind_r4,
		// Token: 0x040001DF RID: 479
		Stind_r8,
		// Token: 0x040001E0 RID: 480
		Add,
		// Token: 0x040001E1 RID: 481
		Sub,
		// Token: 0x040001E2 RID: 482
		Mul,
		// Token: 0x040001E3 RID: 483
		Div,
		// Token: 0x040001E4 RID: 484
		Div_un,
		// Token: 0x040001E5 RID: 485
		Rem,
		// Token: 0x040001E6 RID: 486
		Rem_un,
		// Token: 0x040001E7 RID: 487
		And,
		// Token: 0x040001E8 RID: 488
		Or,
		// Token: 0x040001E9 RID: 489
		Xor,
		// Token: 0x040001EA RID: 490
		Shl,
		// Token: 0x040001EB RID: 491
		Shr,
		// Token: 0x040001EC RID: 492
		Shr_un,
		// Token: 0x040001ED RID: 493
		Neg,
		// Token: 0x040001EE RID: 494
		Not,
		// Token: 0x040001EF RID: 495
		Conv_i1,
		// Token: 0x040001F0 RID: 496
		Conv_i2,
		// Token: 0x040001F1 RID: 497
		Conv_i4,
		// Token: 0x040001F2 RID: 498
		Conv_i8,
		// Token: 0x040001F3 RID: 499
		Conv_r4,
		// Token: 0x040001F4 RID: 500
		Conv_r8,
		// Token: 0x040001F5 RID: 501
		Conv_u4,
		// Token: 0x040001F6 RID: 502
		Conv_u8,
		// Token: 0x040001F7 RID: 503
		Callvirt,
		// Token: 0x040001F8 RID: 504
		Cpobj,
		// Token: 0x040001F9 RID: 505
		Ldobj,
		// Token: 0x040001FA RID: 506
		Ldstr,
		// Token: 0x040001FB RID: 507
		Newobj,
		// Token: 0x040001FC RID: 508
		Castclass,
		// Token: 0x040001FD RID: 509
		Isinst,
		// Token: 0x040001FE RID: 510
		Conv_r_un,
		// Token: 0x040001FF RID: 511
		Unbox = 121,
		// Token: 0x04000200 RID: 512
		Throw,
		// Token: 0x04000201 RID: 513
		Ldfld,
		// Token: 0x04000202 RID: 514
		Ldflda,
		// Token: 0x04000203 RID: 515
		Stfld,
		// Token: 0x04000204 RID: 516
		Ldsfld,
		// Token: 0x04000205 RID: 517
		Ldsflda,
		// Token: 0x04000206 RID: 518
		Stsfld,
		// Token: 0x04000207 RID: 519
		Stobj,
		// Token: 0x04000208 RID: 520
		Conv_ovf_i1_un,
		// Token: 0x04000209 RID: 521
		Conv_ovf_i2_un,
		// Token: 0x0400020A RID: 522
		Conv_ovf_i4_un,
		// Token: 0x0400020B RID: 523
		Conv_ovf_i8_un,
		// Token: 0x0400020C RID: 524
		Conv_ovf_u1_un,
		// Token: 0x0400020D RID: 525
		Conv_ovf_u2_un,
		// Token: 0x0400020E RID: 526
		Conv_ovf_u4_un,
		// Token: 0x0400020F RID: 527
		Conv_ovf_u8_un,
		// Token: 0x04000210 RID: 528
		Conv_ovf_i_un,
		// Token: 0x04000211 RID: 529
		Conv_ovf_u_un,
		// Token: 0x04000212 RID: 530
		Box,
		// Token: 0x04000213 RID: 531
		Newarr,
		// Token: 0x04000214 RID: 532
		Ldlen,
		// Token: 0x04000215 RID: 533
		Ldelema,
		// Token: 0x04000216 RID: 534
		Ldelem_i1,
		// Token: 0x04000217 RID: 535
		Ldelem_u1,
		// Token: 0x04000218 RID: 536
		Ldelem_i2,
		// Token: 0x04000219 RID: 537
		Ldelem_u2,
		// Token: 0x0400021A RID: 538
		Ldelem_i4,
		// Token: 0x0400021B RID: 539
		Ldelem_u4,
		// Token: 0x0400021C RID: 540
		Ldelem_i8,
		// Token: 0x0400021D RID: 541
		Ldelem_i,
		// Token: 0x0400021E RID: 542
		Ldelem_r4,
		// Token: 0x0400021F RID: 543
		Ldelem_r8,
		// Token: 0x04000220 RID: 544
		Ldelem_ref,
		// Token: 0x04000221 RID: 545
		Stelem_i,
		// Token: 0x04000222 RID: 546
		Stelem_i1,
		// Token: 0x04000223 RID: 547
		Stelem_i2,
		// Token: 0x04000224 RID: 548
		Stelem_i4,
		// Token: 0x04000225 RID: 549
		Stelem_i8,
		// Token: 0x04000226 RID: 550
		Stelem_r4,
		// Token: 0x04000227 RID: 551
		Stelem_r8,
		// Token: 0x04000228 RID: 552
		Stelem_ref,
		// Token: 0x04000229 RID: 553
		Ldelem,
		// Token: 0x0400022A RID: 554
		Stelem,
		// Token: 0x0400022B RID: 555
		Unbox_any,
		// Token: 0x0400022C RID: 556
		Conv_ovf_i1 = 179,
		// Token: 0x0400022D RID: 557
		Conv_ovf_u1,
		// Token: 0x0400022E RID: 558
		Conv_ovf_i2,
		// Token: 0x0400022F RID: 559
		Conv_ovf_u2,
		// Token: 0x04000230 RID: 560
		Conv_ovf_i4,
		// Token: 0x04000231 RID: 561
		Conv_ovf_u4,
		// Token: 0x04000232 RID: 562
		Conv_ovf_i8,
		// Token: 0x04000233 RID: 563
		Conv_ovf_u8,
		// Token: 0x04000234 RID: 564
		Refanyval = 194,
		// Token: 0x04000235 RID: 565
		Ckfinite,
		// Token: 0x04000236 RID: 566
		Mkrefany = 198,
		// Token: 0x04000237 RID: 567
		Ldtoken = 208,
		// Token: 0x04000238 RID: 568
		Conv_u2,
		// Token: 0x04000239 RID: 569
		Conv_u1,
		// Token: 0x0400023A RID: 570
		Conv_i,
		// Token: 0x0400023B RID: 571
		Conv_ovf_i,
		// Token: 0x0400023C RID: 572
		Conv_ovf_u,
		// Token: 0x0400023D RID: 573
		Add_ovf,
		// Token: 0x0400023E RID: 574
		Add_ovf_un,
		// Token: 0x0400023F RID: 575
		Mul_ovf,
		// Token: 0x04000240 RID: 576
		Mul_ovf_un,
		// Token: 0x04000241 RID: 577
		Sub_ovf,
		// Token: 0x04000242 RID: 578
		Sub_ovf_un,
		// Token: 0x04000243 RID: 579
		Endfinally,
		// Token: 0x04000244 RID: 580
		Leave,
		// Token: 0x04000245 RID: 581
		Leave_s,
		// Token: 0x04000246 RID: 582
		Stind_i,
		// Token: 0x04000247 RID: 583
		Conv_u,
		// Token: 0x04000248 RID: 584
		Arglist = 65024,
		// Token: 0x04000249 RID: 585
		Ceq,
		// Token: 0x0400024A RID: 586
		Cgt,
		// Token: 0x0400024B RID: 587
		Cgt_un,
		// Token: 0x0400024C RID: 588
		Clt,
		// Token: 0x0400024D RID: 589
		Clt_un,
		// Token: 0x0400024E RID: 590
		Ldftn,
		// Token: 0x0400024F RID: 591
		Ldvirtftn,
		// Token: 0x04000250 RID: 592
		Ldarg = 65033,
		// Token: 0x04000251 RID: 593
		Ldarga,
		// Token: 0x04000252 RID: 594
		Starg,
		// Token: 0x04000253 RID: 595
		Ldloc,
		// Token: 0x04000254 RID: 596
		Ldloca,
		// Token: 0x04000255 RID: 597
		Stloc,
		// Token: 0x04000256 RID: 598
		Localloc,
		// Token: 0x04000257 RID: 599
		Endfilter = 65041,
		// Token: 0x04000258 RID: 600
		Unaligned,
		// Token: 0x04000259 RID: 601
		Volatile,
		// Token: 0x0400025A RID: 602
		Tail,
		// Token: 0x0400025B RID: 603
		Initobj,
		// Token: 0x0400025C RID: 604
		Constrained,
		// Token: 0x0400025D RID: 605
		Cpblk,
		// Token: 0x0400025E RID: 606
		Initblk,
		// Token: 0x0400025F RID: 607
		Rethrow = 65050,
		// Token: 0x04000260 RID: 608
		Sizeof = 65052,
		// Token: 0x04000261 RID: 609
		Refanytype,
		// Token: 0x04000262 RID: 610
		Readonly
	}
}
