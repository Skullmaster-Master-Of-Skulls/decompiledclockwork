using System;

namespace OracleInternal.BinXml
{
	// Token: 0x02000023 RID: 35
	internal class ObxmlOpcode
	{
		// Token: 0x0600020E RID: 526 RVA: 0x0000BF10 File Offset: 0x0000A110
		static ObxmlOpcode()
		{
			for (int i = 0; i < 243; i++)
			{
				ObxmlOpcode.dataType[i] = 1;
			}
			ObxmlOpcode.dataType[96] = 7;
			ObxmlOpcode.dataType[97] = 7;
			ObxmlOpcode.dataType[98] = 7;
			ObxmlOpcode.dataType[99] = 7;
			ObxmlOpcode.dataType[100] = 7;
			ObxmlOpcode.dataType[101] = 7;
			ObxmlOpcode.dataType[102] = 7;
			ObxmlOpcode.dataType[103] = 7;
			ObxmlOpcode.dataType[104] = 7;
			ObxmlOpcode.dataType[105] = 7;
			ObxmlOpcode.dataType[106] = 7;
			ObxmlOpcode.dataType[107] = 7;
			ObxmlOpcode.dataType[108] = 7;
			ObxmlOpcode.dataType[109] = 7;
			ObxmlOpcode.dataType[110] = 7;
			ObxmlOpcode.dataType[111] = 7;
			ObxmlOpcode.dataType[112] = 7;
			ObxmlOpcode.dataType[113] = 7;
			ObxmlOpcode.dataType[114] = 7;
			ObxmlOpcode.dataType[115] = 7;
			ObxmlOpcode.dataType[116] = 7;
			ObxmlOpcode.dataType[64] = 2;
			ObxmlOpcode.dataType[65] = 2;
			ObxmlOpcode.dataType[66] = 2;
			ObxmlOpcode.dataType[67] = 2;
			ObxmlOpcode.dataType[68] = 2;
			ObxmlOpcode.dataType[69] = 2;
			ObxmlOpcode.dataType[70] = 2;
			ObxmlOpcode.dataType[71] = 2;
			ObxmlOpcode.dataType[72] = 2;
			ObxmlOpcode.dataType[73] = 2;
			ObxmlOpcode.dataType[74] = 2;
			ObxmlOpcode.dataType[75] = 2;
			ObxmlOpcode.dataType[76] = 2;
			ObxmlOpcode.dataType[77] = 2;
			ObxmlOpcode.dataType[78] = 2;
			ObxmlOpcode.dataType[79] = 2;
			ObxmlOpcode.dataType[80] = 2;
			ObxmlOpcode.dataType[81] = 2;
			ObxmlOpcode.dataType[82] = 2;
			ObxmlOpcode.dataType[83] = 2;
			ObxmlOpcode.dataType[84] = 2;
			ObxmlOpcode.dataType[85] = 2;
			ObxmlOpcode.dataType[86] = 2;
			ObxmlOpcode.dataType[87] = 2;
			ObxmlOpcode.dataType[88] = 2;
			ObxmlOpcode.dataType[89] = 2;
			ObxmlOpcode.dataType[90] = 2;
			ObxmlOpcode.dataType[91] = 2;
			ObxmlOpcode.dataType[92] = 2;
			ObxmlOpcode.dataType[93] = 2;
			ObxmlOpcode.dataType[94] = 2;
			ObxmlOpcode.dataType[95] = 2;
			ObxmlOpcode.dataType[117] = 4;
			ObxmlOpcode.dataType[118] = 4;
			ObxmlOpcode.dataType[119] = 4;
			ObxmlOpcode.dataType[120] = 4;
			ObxmlOpcode.dataType[121] = 5;
			ObxmlOpcode.dataType[122] = 5;
			ObxmlOpcode.dataType[123] = 5;
			ObxmlOpcode.dataType[124] = 5;
			ObxmlOpcode.dataType[125] = 6;
			ObxmlOpcode.dataType[126] = 6;
			ObxmlOpcode.dataType[136] = 12;
			ObxmlOpcode.dataType[137] = 12;
			ObxmlOpcode.dataType[132] = 9;
			ObxmlOpcode.dataType[133] = 9;
			ObxmlOpcode.dataType[131] = 8;
			ObxmlOpcode.dataType[127] = 10;
			ObxmlOpcode.dataType[128] = 10;
			ObxmlOpcode.dataType[129] = 11;
			ObxmlOpcode.dataType[130] = 11;
			ObxmlOpcode.dataType[135] = 13;
			ObxmlOpcode.dataType[134] = 3;
		}

		// Token: 0x04000168 RID: 360
		internal const short SPACE_FLAG = 0;

		// Token: 0x04000169 RID: 361
		internal const short TAB_FLAG = 32;

		// Token: 0x0400016A RID: 362
		internal const short LNFEED_FLAG = 64;

		// Token: 0x0400016B RID: 363
		internal const short CR_FLAG = 96;

		// Token: 0x0400016C RID: 364
		internal const short CR_SPACE_FLAG = 128;

		// Token: 0x0400016D RID: 365
		internal static byte HDR_CSX_VERSION = 1;

		// Token: 0x0400016E RID: 366
		internal static readonly string[] ENCODING_TYPES = new string[]
		{
			"string",
			"binary",
			"boolean",
			"int",
			"unsigned-int",
			"float",
			"oranum",
			"orats",
			"orats",
			"orats",
			"orats",
			"int(for enum)",
			"string(for qname)",
			"float",
			"int",
			"int",
			"int",
			"unsigned-int",
			"unsigned-int",
			"unsigned-int",
			"unsigned-int",
			"binary",
			"binary"
		};

		// Token: 0x0400016F RID: 367
		internal static readonly int STARTELEM_FLAG_SSEQ = 1;

		// Token: 0x04000170 RID: 368
		internal static readonly int STARTELEM_FLAG_NOTDECTYP = 2;

		// Token: 0x04000171 RID: 369
		internal static readonly int STARTELEM_FLAG_IMPTYP = 4;

		// Token: 0x04000172 RID: 370
		internal static readonly int STARTELEM_FLAG_PFXID = 8;

		// Token: 0x04000173 RID: 371
		internal static readonly int DATL2MAXLEN = 16383;

		// Token: 0x04000174 RID: 372
		internal static readonly long DATL8MAXLEN = 4611686018427387903L;

		// Token: 0x04000175 RID: 373
		internal static readonly int DATL2STRMSK = 0;

		// Token: 0x04000176 RID: 374
		internal static readonly int DATL2BINMSK = 16384;

		// Token: 0x04000177 RID: 375
		internal static readonly long DATL8STRMSK = 0L;

		// Token: 0x04000178 RID: 376
		internal static readonly long DATL8BINMSK = 4611686018427387904L;

		// Token: 0x04000179 RID: 377
		internal static int ENCODER_IGN_WHITESPACE = 1;

		// Token: 0x0400017A RID: 378
		internal static int ENCODER_NO_OPTPRPOPC = 2;

		// Token: 0x0400017B RID: 379
		internal static int ENCODER_FORCE_NONSCHEMABASED = 4;

		// Token: 0x0400017C RID: 380
		internal static int ENCODER_NO_SCHEMASEQ = 8;

		// Token: 0x0400017D RID: 381
		internal static int ENCODER_NO_ARRAYMODE = 16;

		// Token: 0x0400017E RID: 382
		internal static readonly int DTD_SYSTEM_EMPTY = 1;

		// Token: 0x0400017F RID: 383
		internal static readonly int DTD_internal_EMPTY = 2;

		// Token: 0x04000180 RID: 384
		internal static readonly int DTD_internal_SYSTEM_EMPTY = ObxmlOpcode.DTD_SYSTEM_EMPTY | ObxmlOpcode.DTD_internal_EMPTY;

		// Token: 0x04000181 RID: 385
		internal static readonly int DTD_VALUE_EMPTY = 4;

		// Token: 0x04000182 RID: 386
		internal static readonly int NAMESPACEID_XML = 1;

		// Token: 0x04000183 RID: 387
		internal static readonly int NAMESPACEID_XMLNS = 2;

		// Token: 0x04000184 RID: 388
		internal static readonly int NAMESPACEID_NONAMESPACE = 7;

		// Token: 0x04000185 RID: 389
		internal static readonly short SPACE1_IDMASK = 31;

		// Token: 0x04000186 RID: 390
		internal static readonly short SPACE2_IDMASK = 8191;

		// Token: 0x04000187 RID: 391
		internal static readonly short SPACE_FLAGMASK = 224;

		// Token: 0x04000188 RID: 392
		internal static readonly int CSX_MAX_HASH_TRIES = 1000;

		// Token: 0x04000189 RID: 393
		internal static readonly int CSX_IDGEN_SKIP_LEN = 13;

		// Token: 0x0400018A RID: 394
		internal static readonly long UB4MAXVAL = (long)((ulong)-1);

		// Token: 0x0400018B RID: 395
		internal static readonly long SB4MAXVAL = 2147483647L;

		// Token: 0x0400018C RID: 396
		internal static readonly int UB2MAXVAL = 65535;

		// Token: 0x0400018D RID: 397
		internal static readonly int SB2MAXVAL = 32767;

		// Token: 0x0400018E RID: 398
		internal static readonly short UB1MAXVAL = 255;

		// Token: 0x0400018F RID: 399
		internal static readonly int CSX_TOKENTABLE_INITIAL_CAPACITY = 100;

		// Token: 0x04000190 RID: 400
		internal static readonly int CSX_NAMESPACE_LIST_INITIAL_CAPACITY = 5;

		// Token: 0x04000191 RID: 401
		internal static string CSX_DEFAULT_ENCODING = "UTF-8";

		// Token: 0x04000192 RID: 402
		internal static readonly int CSX_INTERNAL_ERR = 35000;

		// Token: 0x04000193 RID: 403
		internal static readonly int CSX_UNEXPECTED_EOF_ERR = 35001;

		// Token: 0x04000194 RID: 404
		internal static readonly int CSX_NOT_FOUND_ERR = 35002;

		// Token: 0x04000195 RID: 405
		internal static readonly int CSX_MAX_PEFIX_LEN_ERR = 35003;

		// Token: 0x04000196 RID: 406
		internal static readonly int CSX_INVALID_BINXML_ERR = 35004;

		// Token: 0x04000197 RID: 407
		internal static readonly int CSX_INVALID_ENCTYPE_ERR = 35005;

		// Token: 0x04000198 RID: 408
		internal static readonly int CSX_MAX_NSURL_ERR = 35006;

		// Token: 0x04000199 RID: 409
		internal static readonly int CSX_TYPECONV_ERR = 35007;

		// Token: 0x0400019A RID: 410
		internal static readonly int CSX_INVALID_DTDEVT_ERR = 35008;

		// Token: 0x0400019B RID: 411
		internal static readonly int CSX_MISMATCH_TGTNS_ERR = 35009;

		// Token: 0x0400019C RID: 412
		internal static readonly int CSX_INVALID_SCHEMALINF_ERR = 35010;

		// Token: 0x0400019D RID: 413
		internal static readonly int CSX_CANNOT_CREATEURL_ERR = 35011;

		// Token: 0x0400019E RID: 414
		internal static readonly int CSX_NSID_ERR = 35012;

		// Token: 0x0400019F RID: 415
		internal static readonly int CSX_TOKEN_NOTFOUND_ERR = 35013;

		// Token: 0x040001A0 RID: 416
		internal static readonly int CSX_VERSION_MISMATCH = 35014;

		// Token: 0x040001A1 RID: 417
		internal static readonly int CSX_INVALID_OPCODE_ERR = 35015;

		// Token: 0x040001A2 RID: 418
		internal static readonly int CSX_NO_STRING_TERMINATOR_ERR = 35016;

		// Token: 0x040001A3 RID: 419
		internal static readonly string CSX_NS_DEFINITION = "http://xmlns.oracle.com/2004/CSX";

		// Token: 0x040001A4 RID: 420
		internal static readonly string CSX_ENCODING_TYPE = "csx:encodingType";

		// Token: 0x040001A5 RID: 421
		internal static readonly string CSX_KIDLIST = "csx:kidList";

		// Token: 0x040001A6 RID: 422
		internal static readonly string CSX_KID = "csx:kid";

		// Token: 0x040001A7 RID: 423
		internal static readonly string CSX_PROPERTY_ID = "csx:propertyID";

		// Token: 0x040001A8 RID: 424
		internal static readonly string CSX_TYPE_ID = "csx:typeID";

		// Token: 0x040001A9 RID: 425
		internal static readonly string STR_ENCODING_TYPE = "encodingType";

		// Token: 0x040001AA RID: 426
		internal static readonly string STR_PROPERTY_ID = "propertyID";

		// Token: 0x040001AB RID: 427
		internal static readonly string STR_TYPE_ID = "typeID";

		// Token: 0x040001AC RID: 428
		internal static readonly string STR_SEQUENTIAL = "sequential";

		// Token: 0x040001AD RID: 429
		internal static readonly string STR_KIDNUM = "kidNum";

		// Token: 0x040001AE RID: 430
		internal static readonly string STR_KIDLIST = "kidList";

		// Token: 0x040001AF RID: 431
		internal static readonly string POSITIVE_SIGN = "+";

		// Token: 0x040001B0 RID: 432
		internal static readonly int MAX_RESERVED_TYPE_IDS = 100;

		// Token: 0x040001B1 RID: 433
		internal static readonly long SIMPLIFIED_INLINE_ENCODING_START_TOKENID = 1000L;

		// Token: 0x040001B2 RID: 434
		internal static byte[] dataType = new byte[243];

		// Token: 0x02000024 RID: 36
		internal enum OpcodeIds
		{
			// Token: 0x040001B4 RID: 436
			None = -1,
			// Token: 0x040001B5 RID: 437
			DATSTR1,
			// Token: 0x040001B6 RID: 438
			DATSTR2,
			// Token: 0x040001B7 RID: 439
			DATSTR3,
			// Token: 0x040001B8 RID: 440
			DATSTR4,
			// Token: 0x040001B9 RID: 441
			DATSTR5,
			// Token: 0x040001BA RID: 442
			DATSTR6,
			// Token: 0x040001BB RID: 443
			DATSTR7,
			// Token: 0x040001BC RID: 444
			DATSTR8,
			// Token: 0x040001BD RID: 445
			DATSTR9,
			// Token: 0x040001BE RID: 446
			DATSTR10,
			// Token: 0x040001BF RID: 447
			DATSTR11,
			// Token: 0x040001C0 RID: 448
			DATSTR12,
			// Token: 0x040001C1 RID: 449
			DATSTR13,
			// Token: 0x040001C2 RID: 450
			DATSTR14,
			// Token: 0x040001C3 RID: 451
			DATSTR15,
			// Token: 0x040001C4 RID: 452
			DATSTR16,
			// Token: 0x040001C5 RID: 453
			DATSTR17,
			// Token: 0x040001C6 RID: 454
			DATSTR18,
			// Token: 0x040001C7 RID: 455
			DATSTR19,
			// Token: 0x040001C8 RID: 456
			DATSTR20,
			// Token: 0x040001C9 RID: 457
			DATSTR21,
			// Token: 0x040001CA RID: 458
			DATSTR22,
			// Token: 0x040001CB RID: 459
			DATSTR23,
			// Token: 0x040001CC RID: 460
			DATSTR24,
			// Token: 0x040001CD RID: 461
			DATSTR25,
			// Token: 0x040001CE RID: 462
			DATSTR26,
			// Token: 0x040001CF RID: 463
			DATSTR27,
			// Token: 0x040001D0 RID: 464
			DATSTR28,
			// Token: 0x040001D1 RID: 465
			DATSTR29,
			// Token: 0x040001D2 RID: 466
			DATSTR30,
			// Token: 0x040001D3 RID: 467
			DATSTR31,
			// Token: 0x040001D4 RID: 468
			DATSTR32,
			// Token: 0x040001D5 RID: 469
			DATSTR33,
			// Token: 0x040001D6 RID: 470
			DATSTR34,
			// Token: 0x040001D7 RID: 471
			DATSTR35,
			// Token: 0x040001D8 RID: 472
			DATSTR36,
			// Token: 0x040001D9 RID: 473
			DATSTR37,
			// Token: 0x040001DA RID: 474
			DATSTR38,
			// Token: 0x040001DB RID: 475
			DATSTR39,
			// Token: 0x040001DC RID: 476
			DATSTR40,
			// Token: 0x040001DD RID: 477
			DATSTR41,
			// Token: 0x040001DE RID: 478
			DATSTR42,
			// Token: 0x040001DF RID: 479
			DATSTR43,
			// Token: 0x040001E0 RID: 480
			DATSTR44,
			// Token: 0x040001E1 RID: 481
			DATSTR45,
			// Token: 0x040001E2 RID: 482
			DATSTR46,
			// Token: 0x040001E3 RID: 483
			DATSTR47,
			// Token: 0x040001E4 RID: 484
			DATSTR48,
			// Token: 0x040001E5 RID: 485
			DATSTR49,
			// Token: 0x040001E6 RID: 486
			DATSTR50,
			// Token: 0x040001E7 RID: 487
			DATSTR51,
			// Token: 0x040001E8 RID: 488
			DATSTR52,
			// Token: 0x040001E9 RID: 489
			DATSTR53,
			// Token: 0x040001EA RID: 490
			DATSTR54,
			// Token: 0x040001EB RID: 491
			DATSTR55,
			// Token: 0x040001EC RID: 492
			DATSTR56,
			// Token: 0x040001ED RID: 493
			DATSTR57,
			// Token: 0x040001EE RID: 494
			DATSTR58,
			// Token: 0x040001EF RID: 495
			DATSTR59,
			// Token: 0x040001F0 RID: 496
			DATSTR60,
			// Token: 0x040001F1 RID: 497
			DATSTR61,
			// Token: 0x040001F2 RID: 498
			DATSTR62,
			// Token: 0x040001F3 RID: 499
			DATSTR63,
			// Token: 0x040001F4 RID: 500
			DATSTR64,
			// Token: 0x040001F5 RID: 501
			DATBIN1,
			// Token: 0x040001F6 RID: 502
			DATBIN2,
			// Token: 0x040001F7 RID: 503
			DATBIN3,
			// Token: 0x040001F8 RID: 504
			DATBIN4,
			// Token: 0x040001F9 RID: 505
			DATBIN5,
			// Token: 0x040001FA RID: 506
			DATBIN6,
			// Token: 0x040001FB RID: 507
			DATBIN7,
			// Token: 0x040001FC RID: 508
			DATBIN8,
			// Token: 0x040001FD RID: 509
			DATBIN9,
			// Token: 0x040001FE RID: 510
			DATBIN10,
			// Token: 0x040001FF RID: 511
			DATBIN11,
			// Token: 0x04000200 RID: 512
			DATBIN12,
			// Token: 0x04000201 RID: 513
			DATBIN13,
			// Token: 0x04000202 RID: 514
			DATBIN14,
			// Token: 0x04000203 RID: 515
			DATBIN15,
			// Token: 0x04000204 RID: 516
			DATBIN16,
			// Token: 0x04000205 RID: 517
			DATBIN17,
			// Token: 0x04000206 RID: 518
			DATBIN18,
			// Token: 0x04000207 RID: 519
			DATBIN19,
			// Token: 0x04000208 RID: 520
			DATBIN20,
			// Token: 0x04000209 RID: 521
			DATBIN21,
			// Token: 0x0400020A RID: 522
			DATBIN22,
			// Token: 0x0400020B RID: 523
			DATBIN23,
			// Token: 0x0400020C RID: 524
			DATBIN24,
			// Token: 0x0400020D RID: 525
			DATBIN25,
			// Token: 0x0400020E RID: 526
			DATBIN26,
			// Token: 0x0400020F RID: 527
			DATBIN27,
			// Token: 0x04000210 RID: 528
			DATBIN28,
			// Token: 0x04000211 RID: 529
			DATBIN29,
			// Token: 0x04000212 RID: 530
			DATBIN30,
			// Token: 0x04000213 RID: 531
			DATBIN31,
			// Token: 0x04000214 RID: 532
			DATBIN32,
			// Token: 0x04000215 RID: 533
			DATNM1,
			// Token: 0x04000216 RID: 534
			DATNM2,
			// Token: 0x04000217 RID: 535
			DATNM3,
			// Token: 0x04000218 RID: 536
			DATNM4,
			// Token: 0x04000219 RID: 537
			DATNM5,
			// Token: 0x0400021A RID: 538
			DATNM6,
			// Token: 0x0400021B RID: 539
			DATNM7,
			// Token: 0x0400021C RID: 540
			DATNM8,
			// Token: 0x0400021D RID: 541
			DATNM9,
			// Token: 0x0400021E RID: 542
			DATNM10,
			// Token: 0x0400021F RID: 543
			DATNM11,
			// Token: 0x04000220 RID: 544
			DATNM12,
			// Token: 0x04000221 RID: 545
			DATNM13,
			// Token: 0x04000222 RID: 546
			DATNM14,
			// Token: 0x04000223 RID: 547
			DATNM15,
			// Token: 0x04000224 RID: 548
			DATNM16,
			// Token: 0x04000225 RID: 549
			DATNM17,
			// Token: 0x04000226 RID: 550
			DATNM18,
			// Token: 0x04000227 RID: 551
			DATNM19,
			// Token: 0x04000228 RID: 552
			DATNM20,
			// Token: 0x04000229 RID: 553
			DATNM21,
			// Token: 0x0400022A RID: 554
			DATINT1,
			// Token: 0x0400022B RID: 555
			DATINT2,
			// Token: 0x0400022C RID: 556
			DATINT4,
			// Token: 0x0400022D RID: 557
			DATINT8,
			// Token: 0x0400022E RID: 558
			DATUINT1,
			// Token: 0x0400022F RID: 559
			DATUINT2,
			// Token: 0x04000230 RID: 560
			DATUINT4,
			// Token: 0x04000231 RID: 561
			DATUINT8,
			// Token: 0x04000232 RID: 562
			DATFLT4,
			// Token: 0x04000233 RID: 563
			DATFLT8,
			// Token: 0x04000234 RID: 564
			DATEPH4,
			// Token: 0x04000235 RID: 565
			DATEPH8,
			// Token: 0x04000236 RID: 566
			DATEPZ6,
			// Token: 0x04000237 RID: 567
			DATEPZ10,
			// Token: 0x04000238 RID: 568
			DATODT,
			// Token: 0x04000239 RID: 569
			DATOTS,
			// Token: 0x0400023A RID: 570
			DATOTSZ,
			// Token: 0x0400023B RID: 571
			DATBOL,
			// Token: 0x0400023C RID: 572
			DATQNM,
			// Token: 0x0400023D RID: 573
			DATENM1,
			// Token: 0x0400023E RID: 574
			DATENM2,
			// Token: 0x0400023F RID: 575
			DATAL2,
			// Token: 0x04000240 RID: 576
			DATAL8,
			// Token: 0x04000241 RID: 577
			DATATL1,
			// Token: 0x04000242 RID: 578
			DATATL2,
			// Token: 0x04000243 RID: 579
			DATATL8,
			// Token: 0x04000244 RID: 580
			DATEMPT,
			// Token: 0x04000245 RID: 581
			DATNULL,
			// Token: 0x04000246 RID: 582
			SCHSST1,
			// Token: 0x04000247 RID: 583
			SCHSST4,
			// Token: 0x04000248 RID: 584
			SCHSST4V,
			// Token: 0x04000249 RID: 585
			SCHSEND,
			// Token: 0x0400024A RID: 586
			DTDSTR,
			// Token: 0x0400024B RID: 587
			DTDELEM,
			// Token: 0x0400024C RID: 588
			DTDALIST,
			// Token: 0x0400024D RID: 589
			DTDENT,
			// Token: 0x0400024E RID: 590
			DTDPENT,
			// Token: 0x0400024F RID: 591
			DTDNOT,
			// Token: 0x04000250 RID: 592
			DTDEND,
			// Token: 0x04000251 RID: 593
			ENTREF,
			// Token: 0x04000252 RID: 594
			CHARREF,
			// Token: 0x04000253 RID: 595
			DOC,
			// Token: 0x04000254 RID: 596
			STRTSEC,
			// Token: 0x04000255 RID: 597
			ENDSEC,
			// Token: 0x04000256 RID: 598
			CHUNK,
			// Token: 0x04000257 RID: 599
			REF,
			// Token: 0x04000258 RID: 600
			TEXT1,
			// Token: 0x04000259 RID: 601
			TEXT2,
			// Token: 0x0400025A RID: 602
			TEXT8,
			// Token: 0x0400025B RID: 603
			CDATA1,
			// Token: 0x0400025C RID: 604
			CDATA2,
			// Token: 0x0400025D RID: 605
			CDATA8,
			// Token: 0x0400025E RID: 606
			PI1L1,
			// Token: 0x0400025F RID: 607
			PI2L4,
			// Token: 0x04000260 RID: 608
			CMT1,
			// Token: 0x04000261 RID: 609
			CMT2,
			// Token: 0x04000262 RID: 610
			CMT8,
			// Token: 0x04000263 RID: 611
			DEFNM4L1,
			// Token: 0x04000264 RID: 612
			DEFNM4L2,
			// Token: 0x04000265 RID: 613
			DEFNM8L1,
			// Token: 0x04000266 RID: 614
			DEFNM8L2,
			// Token: 0x04000267 RID: 615
			DEFPFX4,
			// Token: 0x04000268 RID: 616
			DEFPFX8,
			// Token: 0x04000269 RID: 617
			DEFQ4N4L1,
			// Token: 0x0400026A RID: 618
			DEFQ4N4L2,
			// Token: 0x0400026B RID: 619
			DEFQ4N8L1,
			// Token: 0x0400026C RID: 620
			DEFQ4N8L2,
			// Token: 0x0400026D RID: 621
			DEFQ8N4L1,
			// Token: 0x0400026E RID: 622
			DEFQ8N4L2,
			// Token: 0x0400026F RID: 623
			DEFQ8N8L1,
			// Token: 0x04000270 RID: 624
			DEFQ8N8L2,
			// Token: 0x04000271 RID: 625
			PRPK1L1,
			// Token: 0x04000272 RID: 626
			PRPK1L2,
			// Token: 0x04000273 RID: 627
			PRPK2L1,
			// Token: 0x04000274 RID: 628
			PRPK2L2,
			// Token: 0x04000275 RID: 629
			PRPT2L1,
			// Token: 0x04000276 RID: 630
			PRPT2L2,
			// Token: 0x04000277 RID: 631
			PRPT4L1,
			// Token: 0x04000278 RID: 632
			PRPT4L2,
			// Token: 0x04000279 RID: 633
			PRPT8L1,
			// Token: 0x0400027A RID: 634
			PRPT8L2,
			// Token: 0x0400027B RID: 635
			PRPSTK1,
			// Token: 0x0400027C RID: 636
			PRPSTK2,
			// Token: 0x0400027D RID: 637
			PRPSTT2,
			// Token: 0x0400027E RID: 638
			PRPSTT4,
			// Token: 0x0400027F RID: 639
			PRPSTT8,
			// Token: 0x04000280 RID: 640
			PRPSTK1F,
			// Token: 0x04000281 RID: 641
			PRPSTK2F,
			// Token: 0x04000282 RID: 642
			PRPSTT2F,
			// Token: 0x04000283 RID: 643
			PRPSTT4F,
			// Token: 0x04000284 RID: 644
			PRPSTT8F,
			// Token: 0x04000285 RID: 645
			PRPSTK1V,
			// Token: 0x04000286 RID: 646
			PRPSTK2V,
			// Token: 0x04000287 RID: 647
			PRPSTT2V,
			// Token: 0x04000288 RID: 648
			PRPSTT4V,
			// Token: 0x04000289 RID: 649
			PRPSTT8V,
			// Token: 0x0400028A RID: 650
			ELMSTART,
			// Token: 0x0400028B RID: 651
			ELMSTSSEQ,
			// Token: 0x0400028C RID: 652
			ARRBEG,
			// Token: 0x0400028D RID: 653
			ARREND,
			// Token: 0x0400028E RID: 654
			ENDPRP,
			// Token: 0x0400028F RID: 655
			NOSEQ,
			// Token: 0x04000290 RID: 656
			NOP,
			// Token: 0x04000291 RID: 657
			NOPARR,
			// Token: 0x04000292 RID: 658
			NMSPC,
			// Token: 0x04000293 RID: 659
			NSP4,
			// Token: 0x04000294 RID: 660
			NSP8,
			// Token: 0x04000295 RID: 661
			ARRSTK1V,
			// Token: 0x04000296 RID: 662
			ARRSTK2V,
			// Token: 0x04000297 RID: 663
			ARRSTT4V,
			// Token: 0x04000298 RID: 664
			ARRSTT8V,
			// Token: 0x04000299 RID: 665
			PRTDATA,
			// Token: 0x0400029A RID: 666
			PRTDATAT,
			// Token: 0x0400029B RID: 667
			PRTTEXT,
			// Token: 0x0400029C RID: 668
			PRTCDATA,
			// Token: 0x0400029D RID: 669
			PRTPI,
			// Token: 0x0400029E RID: 670
			PRTCMT,
			// Token: 0x0400029F RID: 671
			SPACE1,
			// Token: 0x040002A0 RID: 672
			SPACE2,
			// Token: 0x040002A1 RID: 673
			SPACE8,
			// Token: 0x040002A2 RID: 674
			XMLDECL,
			// Token: 0x040002A3 RID: 675
			SPACEQN,
			// Token: 0x040002A4 RID: 676
			SPACEQN8,
			// Token: 0x040002A5 RID: 677
			ENDPRPSP,
			// Token: 0x040002A6 RID: 678
			ENDPRPSP8,
			// Token: 0x040002A7 RID: 679
			DTDDECL,
			// Token: 0x040002A8 RID: 680
			FORMATEXTENSION = 254,
			// Token: 0x040002A9 RID: 681
			OPCODE_NUMBER = 243,
			// Token: 0x040002AA RID: 682
			DTDSTRE = 0,
			// Token: 0x040002AB RID: 683
			DTDNOTE,
			// Token: 0x040002AC RID: 684
			DTDENTE,
			// Token: 0x040002AD RID: 685
			DTDPENTE,
			// Token: 0x040002AE RID: 686
			DTDALISTE,
			// Token: 0x040002AF RID: 687
			EXTENDED_OPCODE_NUMBER
		}

		// Token: 0x02000025 RID: 37
		internal enum OpcodeDataTypes : byte
		{
			// Token: 0x040002B1 RID: 689
			String = 1,
			// Token: 0x040002B2 RID: 690
			Bin,
			// Token: 0x040002B3 RID: 691
			Boolean,
			// Token: 0x040002B4 RID: 692
			Int,
			// Token: 0x040002B5 RID: 693
			Uint,
			// Token: 0x040002B6 RID: 694
			Float,
			// Token: 0x040002B7 RID: 695
			Oranum,
			// Token: 0x040002B8 RID: 696
			Oradate,
			// Token: 0x040002B9 RID: 697
			Orats,
			// Token: 0x040002BA RID: 698
			Epoch,
			// Token: 0x040002BB RID: 699
			Epochtz,
			// Token: 0x040002BC RID: 700
			Enum,
			// Token: 0x040002BD RID: 701
			Qname,
			// Token: 0x040002BE RID: 702
			Double,
			// Token: 0x040002BF RID: 703
			Byte,
			// Token: 0x040002C0 RID: 704
			Short,
			// Token: 0x040002C1 RID: 705
			Long,
			// Token: 0x040002C2 RID: 706
			Unsignedbyte,
			// Token: 0x040002C3 RID: 707
			Unsignedshort,
			// Token: 0x040002C4 RID: 708
			Unsignedint,
			// Token: 0x040002C5 RID: 709
			Unsignedlong,
			// Token: 0x040002C6 RID: 710
			Hexbinary,
			// Token: 0x040002C7 RID: 711
			Base64binary
		}

		// Token: 0x02000026 RID: 38
		internal enum CSXHeaderFlags
		{
			// Token: 0x040002C9 RID: 713
			HDR_FLAG_NOINTOK = 1,
			// Token: 0x040002CA RID: 714
			HDR_FLAG_NOSCHREF,
			// Token: 0x040002CB RID: 715
			HDR_FLAG_RGUID = 4,
			// Token: 0x040002CC RID: 716
			HDR_FLAG_DOCID = 8,
			// Token: 0x040002CD RID: 717
			HDR_FLAG_PATHID = 16,
			// Token: 0x040002CE RID: 718
			HDR_FLAG_SEQID = 32,
			// Token: 0x040002CF RID: 719
			HDR_FLAG_BIGEFLT = 64,
			// Token: 0x040002D0 RID: 720
			HDR_FLAG_MASK = 127,
			// Token: 0x040002D1 RID: 721
			HDR_FLAG_STANDALONE_SPECIFIED = 64,
			// Token: 0x040002D2 RID: 722
			HDR_FLAG_STANDALONE_TRUE = 128,
			// Token: 0x040002D3 RID: 723
			HDR_FLAG_ENCODING_SPECIFIED = 256
		}
	}
}
