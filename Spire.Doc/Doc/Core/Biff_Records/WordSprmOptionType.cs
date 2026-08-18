using System;

namespace Spire.Doc.Core.Biff_Records
{
	// Token: 0x020004F9 RID: 1273
	internal enum WordSprmOptionType
	{
		// Token: 0x040033CE RID: 13262
		sprmPIstd = 17920,
		// Token: 0x040033CF RID: 13263
		sprmPIstdPermute = 50689,
		// Token: 0x040033D0 RID: 13264
		sprmPIncLvl = 9730,
		// Token: 0x040033D1 RID: 13265
		sprmPJc = 9219,
		// Token: 0x040033D2 RID: 13266
		sprmPFSideBySide,
		// Token: 0x040033D3 RID: 13267
		sprmPFKeep,
		// Token: 0x040033D4 RID: 13268
		sprmPFKeepFollow,
		// Token: 0x040033D5 RID: 13269
		sprmPFPageBreakBefore,
		// Token: 0x040033D6 RID: 13270
		sprmPBrcl,
		// Token: 0x040033D7 RID: 13271
		sprmPBrcp,
		// Token: 0x040033D8 RID: 13272
		sprmPIlvl = 9738,
		// Token: 0x040033D9 RID: 13273
		sprmPIlfo = 17931,
		// Token: 0x040033DA RID: 13274
		sprmPFNoLineNumb = 9228,
		// Token: 0x040033DB RID: 13275
		sprmPChgTabsPapx = 50701,
		// Token: 0x040033DC RID: 13276
		sprmPDxaRight = 33806,
		// Token: 0x040033DD RID: 13277
		sprmPDxaLeft,
		// Token: 0x040033DE RID: 13278
		sprmPNest = 17936,
		// Token: 0x040033DF RID: 13279
		sprmPDxaLeft1 = 33809,
		// Token: 0x040033E0 RID: 13280
		sprmPDyaLine = 25618,
		// Token: 0x040033E1 RID: 13281
		sprmPDyaBefore = 42003,
		// Token: 0x040033E2 RID: 13282
		sprmPDyaAfter,
		// Token: 0x040033E3 RID: 13283
		sprmPChgTabs = 50709,
		// Token: 0x040033E4 RID: 13284
		sprmPFInTable = 9238,
		// Token: 0x040033E5 RID: 13285
		sprmPFTtp,
		// Token: 0x040033E6 RID: 13286
		sprmPDxaAbs = 33816,
		// Token: 0x040033E7 RID: 13287
		sprmPDyaAbs,
		// Token: 0x040033E8 RID: 13288
		sprmPDxaWidth,
		// Token: 0x040033E9 RID: 13289
		sprmPPc = 9755,
		// Token: 0x040033EA RID: 13290
		sprmPBrcTop10 = 17948,
		// Token: 0x040033EB RID: 13291
		sprmPBrcLeft10,
		// Token: 0x040033EC RID: 13292
		sprmPBrcBottom10,
		// Token: 0x040033ED RID: 13293
		sprmPBrcRight10,
		// Token: 0x040033EE RID: 13294
		sprmPBrcBetween10,
		// Token: 0x040033EF RID: 13295
		sprmPBrcBar10,
		// Token: 0x040033F0 RID: 13296
		sprmPDxaFromText10,
		// Token: 0x040033F1 RID: 13297
		sprmPWr = 9251,
		// Token: 0x040033F2 RID: 13298
		sprmPBrcTop = 25636,
		// Token: 0x040033F3 RID: 13299
		sprmPBrcLeft,
		// Token: 0x040033F4 RID: 13300
		sprmPBrcBottom,
		// Token: 0x040033F5 RID: 13301
		sprmPBrcRight,
		// Token: 0x040033F6 RID: 13302
		sprmPBrcBetween,
		// Token: 0x040033F7 RID: 13303
		sprmPBrcBar = 26153,
		// Token: 0x040033F8 RID: 13304
		sprmPBrcTopNew = 50766,
		// Token: 0x040033F9 RID: 13305
		sprmPBrcLeftNew,
		// Token: 0x040033FA RID: 13306
		sprmPBrcBottomNew,
		// Token: 0x040033FB RID: 13307
		sprmPBrcRightNew,
		// Token: 0x040033FC RID: 13308
		sprmPFNoAutoHyph = 9258,
		// Token: 0x040033FD RID: 13309
		sprmPWHeightAbs = 17451,
		// Token: 0x040033FE RID: 13310
		sprmPDcs,
		// Token: 0x040033FF RID: 13311
		sprmPShd,
		// Token: 0x04003400 RID: 13312
		sprmPDyaFromText = 33838,
		// Token: 0x04003401 RID: 13313
		sprmPDxaFromText,
		// Token: 0x04003402 RID: 13314
		sprmPFLocked = 9264,
		// Token: 0x04003403 RID: 13315
		sprmPFWidowControl,
		// Token: 0x04003404 RID: 13316
		sprmPRuler = 50738,
		// Token: 0x04003405 RID: 13317
		sprmPFKinsoku = 9267,
		// Token: 0x04003406 RID: 13318
		sprmPFWordWrap,
		// Token: 0x04003407 RID: 13319
		sprmPFOverflowPunct,
		// Token: 0x04003408 RID: 13320
		sprmPFTopLinePunct,
		// Token: 0x04003409 RID: 13321
		sprmPFAutoSpaceDE,
		// Token: 0x0400340A RID: 13322
		sprmPFAutoSpaceDN,
		// Token: 0x0400340B RID: 13323
		sprmPWAlignFont = 17465,
		// Token: 0x0400340C RID: 13324
		sprmPFrameTextFlow,
		// Token: 0x0400340D RID: 13325
		sprmPISnapBaseLine = 9275,
		// Token: 0x0400340E RID: 13326
		sprmPAnld = 50750,
		// Token: 0x0400340F RID: 13327
		sprmPPropRMark,
		// Token: 0x04003410 RID: 13328
		sprmPOutLvl = 9792,
		// Token: 0x04003411 RID: 13329
		sprmPFBiDi = 9281,
		// Token: 0x04003412 RID: 13330
		sprmPFNumRMIns = 9283,
		// Token: 0x04003413 RID: 13331
		sprmPCrLf,
		// Token: 0x04003414 RID: 13332
		sprmPNumRM = 50757,
		// Token: 0x04003415 RID: 13333
		sprmPHugePapx = 26181,
		// Token: 0x04003416 RID: 13334
		sprmPHugePapx2,
		// Token: 0x04003417 RID: 13335
		sprmPFUsePgsuSettings = 9287,
		// Token: 0x04003418 RID: 13336
		sprmPFAdjustRight,
		// Token: 0x04003419 RID: 13337
		sprmCFRMarkDel = 2048,
		// Token: 0x0400341A RID: 13338
		sprmCFRMark,
		// Token: 0x0400341B RID: 13339
		sprmCFFldVanish,
		// Token: 0x0400341C RID: 13340
		sprmCPicLocation = 27139,
		// Token: 0x0400341D RID: 13341
		sprmCIbstRMark = 18436,
		// Token: 0x0400341E RID: 13342
		sprmCDttmRMark = 26629,
		// Token: 0x0400341F RID: 13343
		sprmCFData = 2054,
		// Token: 0x04003420 RID: 13344
		sprmCIdslRMark = 18439,
		// Token: 0x04003421 RID: 13345
		sprmCChs = 59912,
		// Token: 0x04003422 RID: 13346
		sprmCSymbol = 27145,
		// Token: 0x04003423 RID: 13347
		sprmCFOle2 = 2058,
		// Token: 0x04003424 RID: 13348
		sprmCIdCharType = 18443,
		// Token: 0x04003425 RID: 13349
		sprmCHighlight = 10764,
		// Token: 0x04003426 RID: 13350
		sprmCObjLocation = 26638,
		// Token: 0x04003427 RID: 13351
		sprmCFFtcAsciSymb = 10768,
		// Token: 0x04003428 RID: 13352
		sprmCIstd = 18992,
		// Token: 0x04003429 RID: 13353
		sprmCIstdPermute = 51761,
		// Token: 0x0400342A RID: 13354
		sprmCDefault = 10802,
		// Token: 0x0400342B RID: 13355
		sprmCPlain,
		// Token: 0x0400342C RID: 13356
		sprmCKcd,
		// Token: 0x0400342D RID: 13357
		sprmCFBold = 2101,
		// Token: 0x0400342E RID: 13358
		sprmCFItalic,
		// Token: 0x0400342F RID: 13359
		sprmCFStrike,
		// Token: 0x04003430 RID: 13360
		sprmCFOutline,
		// Token: 0x04003431 RID: 13361
		sprmCFShadow,
		// Token: 0x04003432 RID: 13362
		sprmCFSmallCaps,
		// Token: 0x04003433 RID: 13363
		sprmCFCaps,
		// Token: 0x04003434 RID: 13364
		sprmCFVanish,
		// Token: 0x04003435 RID: 13365
		sprmCFtcDefault = 19005,
		// Token: 0x04003436 RID: 13366
		sprmCKul = 10814,
		// Token: 0x04003437 RID: 13367
		sprmCSizePos = 59967,
		// Token: 0x04003438 RID: 13368
		sprmCDxaSpace = 34880,
		// Token: 0x04003439 RID: 13369
		sprmCLid = 19009,
		// Token: 0x0400343A RID: 13370
		sprmCIco = 10818,
		// Token: 0x0400343B RID: 13371
		sprmCIcoe = 26736,
		// Token: 0x0400343C RID: 13372
		sprmCHps = 19011,
		// Token: 0x0400343D RID: 13373
		sprmCHpsInc = 10820,
		// Token: 0x0400343E RID: 13374
		sprmCHpsPos = 18501,
		// Token: 0x0400343F RID: 13375
		sprmCHpsPosAdj = 10822,
		// Token: 0x04003440 RID: 13376
		sprmCMajority = 51783,
		// Token: 0x04003441 RID: 13377
		sprmCIss = 10824,
		// Token: 0x04003442 RID: 13378
		sprmCHpsNew50 = 51785,
		// Token: 0x04003443 RID: 13379
		sprmCHpsInc1,
		// Token: 0x04003444 RID: 13380
		sprmCHpsKern = 18507,
		// Token: 0x04003445 RID: 13381
		sprmCMajority50 = 51788,
		// Token: 0x04003446 RID: 13382
		sprmCHpsMul = 19021,
		// Token: 0x04003447 RID: 13383
		sprmCYsri = 18510,
		// Token: 0x04003448 RID: 13384
		sprmCRgFtc0 = 19023,
		// Token: 0x04003449 RID: 13385
		sprmCRgFtc1,
		// Token: 0x0400344A RID: 13386
		sprmCRgFtc2,
		// Token: 0x0400344B RID: 13387
		sprmCCharScale = 18514,
		// Token: 0x0400344C RID: 13388
		sprmCFDStrike = 10835,
		// Token: 0x0400344D RID: 13389
		sprmCFImprint = 2132,
		// Token: 0x0400344E RID: 13390
		sprmCFSpec,
		// Token: 0x0400344F RID: 13391
		sprmCFObj,
		// Token: 0x04003450 RID: 13392
		sprmCPropRMark = 51799,
		// Token: 0x04003451 RID: 13393
		sprmCFEmboss = 2136,
		// Token: 0x04003452 RID: 13394
		sprmCSfxText = 10329,
		// Token: 0x04003453 RID: 13395
		sprmCFBiDi = 2138,
		// Token: 0x04003454 RID: 13396
		sprmCFDiacColor,
		// Token: 0x04003455 RID: 13397
		sprmCFBoldBi,
		// Token: 0x04003456 RID: 13398
		sprmCFItalicBi,
		// Token: 0x04003457 RID: 13399
		sprmCFtcBi = 19038,
		// Token: 0x04003458 RID: 13400
		sprmCLidBi = 18527,
		// Token: 0x04003459 RID: 13401
		sprmCIcoBi = 19040,
		// Token: 0x0400345A RID: 13402
		sprmCHpsBi,
		// Token: 0x0400345B RID: 13403
		sprmCDispFldRMark = 51810,
		// Token: 0x0400345C RID: 13404
		sprmCIbstRMarkDel = 18531,
		// Token: 0x0400345D RID: 13405
		sprmCDttmRMarkDel = 26724,
		// Token: 0x0400345E RID: 13406
		sprmCBrc,
		// Token: 0x0400345F RID: 13407
		sprmCShd = 18534,
		// Token: 0x04003460 RID: 13408
		sprmCIdslRMarkDel,
		// Token: 0x04003461 RID: 13409
		sprmCFUsePgsuSettings = 2152,
		// Token: 0x04003462 RID: 13410
		sprmCCpg = 18539,
		// Token: 0x04003463 RID: 13411
		sprmCRgLid0 = 18541,
		// Token: 0x04003464 RID: 13412
		sprmCRgLid1,
		// Token: 0x04003465 RID: 13413
		sprmCIdctHint = 10351,
		// Token: 0x04003466 RID: 13414
		sprmPicBrcl = 11776,
		// Token: 0x04003467 RID: 13415
		sprmPicScale = 52737,
		// Token: 0x04003468 RID: 13416
		sprmPicBrcTop = 27650,
		// Token: 0x04003469 RID: 13417
		sprmPicBrcLeft,
		// Token: 0x0400346A RID: 13418
		sprmPicBrcBottom,
		// Token: 0x0400346B RID: 13419
		sprmPicBrcRight,
		// Token: 0x0400346C RID: 13420
		sprmScnsPgn = 12288,
		// Token: 0x0400346D RID: 13421
		sprmSiHeadingPgn,
		// Token: 0x0400346E RID: 13422
		sprmSOlstAnm = 53762,
		// Token: 0x0400346F RID: 13423
		sprmSDxaColWidth = 61955,
		// Token: 0x04003470 RID: 13424
		sprmSDxaColSpacing,
		// Token: 0x04003471 RID: 13425
		sprmSFEvenlySpaced = 12293,
		// Token: 0x04003472 RID: 13426
		sprmSFProtected,
		// Token: 0x04003473 RID: 13427
		sprmSDmBinFirst = 20487,
		// Token: 0x04003474 RID: 13428
		sprmSDmBinOther,
		// Token: 0x04003475 RID: 13429
		sprmSBkc = 12297,
		// Token: 0x04003476 RID: 13430
		sprmSFTitlePage,
		// Token: 0x04003477 RID: 13431
		sprmSCcolumns = 20491,
		// Token: 0x04003478 RID: 13432
		sprmSDxaColumns = 36876,
		// Token: 0x04003479 RID: 13433
		sprmSFAutoPgn = 12301,
		// Token: 0x0400347A RID: 13434
		sprmSNfcPgn,
		// Token: 0x0400347B RID: 13435
		sprmSDyaPgn = 45071,
		// Token: 0x0400347C RID: 13436
		sprmSDxaPgn,
		// Token: 0x0400347D RID: 13437
		sprmSFPgnRestart = 12305,
		// Token: 0x0400347E RID: 13438
		sprmSFEndnote,
		// Token: 0x0400347F RID: 13439
		sprmSLnc,
		// Token: 0x04003480 RID: 13440
		sprmSGprfIhdt,
		// Token: 0x04003481 RID: 13441
		sprmSNLnnMod = 20501,
		// Token: 0x04003482 RID: 13442
		sprmSDxaLnn = 36886,
		// Token: 0x04003483 RID: 13443
		sprmSDyaHdrTop = 45079,
		// Token: 0x04003484 RID: 13444
		sprmSDyaHdrBottom,
		// Token: 0x04003485 RID: 13445
		sprmSLBetween = 12313,
		// Token: 0x04003486 RID: 13446
		sprmSVjc,
		// Token: 0x04003487 RID: 13447
		sprmSLnnMin = 20507,
		// Token: 0x04003488 RID: 13448
		sprmSPgnStart,
		// Token: 0x04003489 RID: 13449
		sprmSBOrientation = 12317,
		// Token: 0x0400348A RID: 13450
		sprmSBCustomize,
		// Token: 0x0400348B RID: 13451
		sprmSXaPage = 45087,
		// Token: 0x0400348C RID: 13452
		sprmSYaPage,
		// Token: 0x0400348D RID: 13453
		sprmSDxaLeft,
		// Token: 0x0400348E RID: 13454
		sprmSDxaRight,
		// Token: 0x0400348F RID: 13455
		sprmSDyaTop = 36899,
		// Token: 0x04003490 RID: 13456
		sprmSDyaBottom,
		// Token: 0x04003491 RID: 13457
		sprmSDzaGutter = 45093,
		// Token: 0x04003492 RID: 13458
		sprmSDmPaperReq = 20518,
		// Token: 0x04003493 RID: 13459
		sprmSPropRMark = 53799,
		// Token: 0x04003494 RID: 13460
		sprmSFBiDi = 12840,
		// Token: 0x04003495 RID: 13461
		sprmSFFacingCol,
		// Token: 0x04003496 RID: 13462
		sprmSFRTLGutter,
		// Token: 0x04003497 RID: 13463
		sprmSBrcTop = 28715,
		// Token: 0x04003498 RID: 13464
		sprmSBrcLeft,
		// Token: 0x04003499 RID: 13465
		sprmSBrcBottom,
		// Token: 0x0400349A RID: 13466
		sprmSBrcRight,
		// Token: 0x0400349B RID: 13467
		sprmSPgbProp = 21039,
		// Token: 0x0400349C RID: 13468
		sprmSDxtCharSpace = 28720,
		// Token: 0x0400349D RID: 13469
		sprmSDyaLinePitch = 36913,
		// Token: 0x0400349E RID: 13470
		sprmSClm = 20530,
		// Token: 0x0400349F RID: 13471
		sprmSTextFlow,
		// Token: 0x040034A0 RID: 13472
		sprmTJc = 21504,
		// Token: 0x040034A1 RID: 13473
		sprmTDxaLeft = 38401,
		// Token: 0x040034A2 RID: 13474
		sprmTDxaGapHalf,
		// Token: 0x040034A3 RID: 13475
		sprmTFCantSplit = 13315,
		// Token: 0x040034A4 RID: 13476
		sprmTFCantSplit90 = 13414,
		// Token: 0x040034A5 RID: 13477
		sprmTTableHeader = 13316,
		// Token: 0x040034A6 RID: 13478
		sprmTTableBorders = 54789,
		// Token: 0x040034A7 RID: 13479
		sprmTDefTable10,
		// Token: 0x040034A8 RID: 13480
		sprmTDyaRowHeight = 37895,
		// Token: 0x040034A9 RID: 13481
		sprmTDefTable = 54792,
		// Token: 0x040034AA RID: 13482
		sprmTDefTableShd,
		// Token: 0x040034AB RID: 13483
		sprmTTlp = 29706,
		// Token: 0x040034AC RID: 13484
		sprmTFBiDi = 22027,
		// Token: 0x040034AD RID: 13485
		sprmTHTMLProps = 29708,
		// Token: 0x040034AE RID: 13486
		sprmTSetBrc = 54816,
		// Token: 0x040034AF RID: 13487
		sprmTInsert = 30241,
		// Token: 0x040034B0 RID: 13488
		sprmTDelete = 22050,
		// Token: 0x040034B1 RID: 13489
		sprmTDxaCol = 30243,
		// Token: 0x040034B2 RID: 13490
		sprmTMerge = 22052,
		// Token: 0x040034B3 RID: 13491
		sprmTSplit,
		// Token: 0x040034B4 RID: 13492
		sprmTSetBrc10 = 54822,
		// Token: 0x040034B5 RID: 13493
		sprmTSetShd = 30247,
		// Token: 0x040034B6 RID: 13494
		sprmTSetShdOdd,
		// Token: 0x040034B7 RID: 13495
		sprmTTextFlow,
		// Token: 0x040034B8 RID: 13496
		sprmTDiagLine = 54826,
		// Token: 0x040034B9 RID: 13497
		sprmTVertMerge,
		// Token: 0x040034BA RID: 13498
		sprmTVertAlign,
		// Token: 0x040034BB RID: 13499
		sprmPTimeStamp = 25703,
		// Token: 0x040034BC RID: 13500
		sprmCShdNew = 51825,
		// Token: 0x040034BD RID: 13501
		sprmPShdNew = 50765,
		// Token: 0x040034BE RID: 13502
		sprmTTableBordersNew = 54803,
		// Token: 0x040034BF RID: 13503
		sprmTCellMargins = 54834,
		// Token: 0x040034C0 RID: 13504
		sprmTTableCellMargins = 54836,
		// Token: 0x040034C1 RID: 13505
		sprmNone = 0,
		// Token: 0x040034C2 RID: 13506
		sprmUnknown1 = 26645,
		// Token: 0x040034C3 RID: 13507
		sprmUnknown2,
		// Token: 0x040034C4 RID: 13508
		sprmCRgLid3 = 18547,
		// Token: 0x040034C5 RID: 13509
		sprmCRgLid3_2,
		// Token: 0x040034C6 RID: 13510
		sprmPSubTableCellEnd = 9291,
		// Token: 0x040034C7 RID: 13511
		sprmPSubTableRowEnd,
		// Token: 0x040034C8 RID: 13512
		sprmTTopBorderColor = 54810,
		// Token: 0x040034C9 RID: 13513
		sprmTLeftBorderColor,
		// Token: 0x040034CA RID: 13514
		sprmTBottomBorderColor,
		// Token: 0x040034CB RID: 13515
		sprmTRightBorderColor,
		// Token: 0x040034CC RID: 13516
		sprmTCellSpacing = 54835,
		// Token: 0x040034CD RID: 13517
		sprmTAutoResizeCells = 13845,
		// Token: 0x040034CE RID: 13518
		sprmSBrcBottomNew = 53814,
		// Token: 0x040034CF RID: 13519
		sprmSBrcLeftNew = 53813,
		// Token: 0x040034D0 RID: 13520
		sprmSBrcRightNew = 53815,
		// Token: 0x040034D1 RID: 13521
		sprmSBrcTopNew = 53812,
		// Token: 0x040034D2 RID: 13522
		sprmPDxaLeft1Bi = 33888,
		// Token: 0x040034D3 RID: 13523
		sprmPHugePapx3 = 25707,
		// Token: 0x040034D4 RID: 13524
		sprmTNestingLevel = 26185,
		// Token: 0x040034D5 RID: 13525
		sprmTableUnknown1 = 61955,
		// Token: 0x040034D6 RID: 13526
		sprmTableUnknown2,
		// Token: 0x040034D7 RID: 13527
		sprmTPreferredWidth = 62996,
		// Token: 0x040034D8 RID: 13528
		sprmTableUnknown4 = 62999,
		// Token: 0x040034D9 RID: 13529
		sprmTCellFitText,
		// Token: 0x040034DA RID: 13530
		sprmTWidthIndent = 63073,
		// Token: 0x040034DB RID: 13531
		sprmCUnderlineColor = 26743,
		// Token: 0x040034DC RID: 13532
		sprmCFNoProof = 2165,
		// Token: 0x040034DD RID: 13533
		sprmPJcBi = 9313,
		// Token: 0x040034DE RID: 13534
		sprmPDxaLeftBi = 33886,
		// Token: 0x040034DF RID: 13535
		sprmPDxaRightBi = 33885,
		// Token: 0x040034E0 RID: 13536
		sprmPFBeforeAuto = 9307,
		// Token: 0x040034E1 RID: 13537
		sprmPFAfterAuto,
		// Token: 0x040034E2 RID: 13538
		sprmTPropRMark = 54887,
		// Token: 0x040034E3 RID: 13539
		sprmTPositionCode = 13837,
		// Token: 0x040034E4 RID: 13540
		sprmTFrameLeft = 37902,
		// Token: 0x040034E5 RID: 13541
		sprmTFrameTop,
		// Token: 0x040034E6 RID: 13542
		sprmTFromTextBottom = 37919,
		// Token: 0x040034E7 RID: 13543
		sprmTFromTextLeft = 37904,
		// Token: 0x040034E8 RID: 13544
		sprmTFromTextRight = 37918,
		// Token: 0x040034E9 RID: 13545
		sprmTFromTextTop = 37905,
		// Token: 0x040034EA RID: 13546
		sprmTCellShdNew = 54802,
		// Token: 0x040034EB RID: 13547
		sprmTCellShdNew2 = 54806,
		// Token: 0x040034EC RID: 13548
		SprmTCellShdNew2Dup = 54897,
		// Token: 0x040034ED RID: 13549
		sprmTCellShdNew3 = 54796,
		// Token: 0x040034EE RID: 13550
		SprmTCellShdNew3Dup = 54899,
		// Token: 0x040034EF RID: 13551
		sprmTCellShdNewDup = 54896,
		// Token: 0x040034F0 RID: 13552
		sprmTTableShd = 54880,
		// Token: 0x040034F1 RID: 13553
		sprmPUndocumented2462 = 9314,
		// Token: 0x040034F2 RID: 13554
		sprmPUndocumented4458 = 17496,
		// Token: 0x040034F3 RID: 13555
		sprmPUndocumented4459,
		// Token: 0x040034F4 RID: 13556
		sprmPUndocumented6465 = 25701,
		// Token: 0x040034F5 RID: 13557
		sprmPUndocumented6654 = 26196,
		// Token: 0x040034F6 RID: 13558
		sprmPUndocumentedC653 = 50771,
		// Token: 0x040034F7 RID: 13559
		sprmPUndocumentedC66C = 50796,
		// Token: 0x040034F8 RID: 13560
		sprmCUndocumented2879 = 10361,
		// Token: 0x040034F9 RID: 13561
		sprmCUndocumented2A86 = 10886,
		// Token: 0x040034FA RID: 13562
		sprmCPbiHasImage = 18568,
		// Token: 0x040034FB RID: 13563
		sprmCUndocumented6815 = 26645,
		// Token: 0x040034FC RID: 13564
		sprmCUndocumented6816,
		// Token: 0x040034FD RID: 13565
		sprmCUndocumented6817,
		// Token: 0x040034FE RID: 13566
		sprmCPbiImageIndex = 26759,
		// Token: 0x040034FF RID: 13567
		sprmCUndocumented811 = 2065,
		// Token: 0x04003500 RID: 13568
		sprmCUndocumentedSpacing = 26624,
		// Token: 0x04003501 RID: 13569
		sprmCUndocumentedRevisionProblem = 2560,
		// Token: 0x04003502 RID: 13570
		sprmCUndocumented1 = 10752,
		// Token: 0x04003503 RID: 13571
		sprmCUndocumented2 = 26880,
		// Token: 0x04003504 RID: 13572
		sprmCUndocumented3 = 51200,
		// Token: 0x04003505 RID: 13573
		sprmCUndocumented4 = 27136,
		// Token: 0x04003506 RID: 13574
		sprmCUndocumented5 = 18944,
		// Token: 0x04003507 RID: 13575
		sprmCUndocumented6 = 43264,
		// Token: 0x04003508 RID: 13576
		sprmCUndocumented7 = 43520,
		// Token: 0x04003509 RID: 13577
		sprmCUndocumented8 = 43776,
		// Token: 0x0400350A RID: 13578
		sprmTCellBrcType = 54882,
		// Token: 0x0400350B RID: 13579
		sprmCPropRMark1 = 51849,
		// Token: 0x0400350C RID: 13580
		sprmCWall = 10883,
		// Token: 0x0400350D RID: 13581
		sprmPWall = 9828,
		// Token: 0x0400350E RID: 13582
		sprmPPropRMark90 = 50799,
		// Token: 0x0400350F RID: 13583
		sprmTWall = 13928,
		// Token: 0x04003510 RID: 13584
		sprmPFContSpacing = 9325,
		// Token: 0x04003511 RID: 13585
		sprmSWall = 12857,
		// Token: 0x04003512 RID: 13586
		sprmPDxcLeft = 17494,
		// Token: 0x04003513 RID: 13587
		sprmPDxcLeft1,
		// Token: 0x04003514 RID: 13588
		sprmPDxcRight = 17493
	}
}
