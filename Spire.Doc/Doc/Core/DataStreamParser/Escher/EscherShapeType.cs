using System;

namespace Spire.Doc.Core.DataStreamParser.Escher
{
	// Token: 0x0200012A RID: 298
	internal enum EscherShapeType
	{
		// Token: 0x04001031 RID: 4145
		msosptAccentBorderCallout1 = 50,
		// Token: 0x04001032 RID: 4146
		msosptAccentBorderCallout2,
		// Token: 0x04001033 RID: 4147
		msosptAccentBorderCallout3,
		// Token: 0x04001034 RID: 4148
		msosptAccentBorderCallout90 = 181,
		// Token: 0x04001035 RID: 4149
		msosptAccenrCallout1 = 44,
		// Token: 0x04001036 RID: 4150
		msosptAccentCallout2,
		// Token: 0x04001037 RID: 4151
		msosptAccentCallout3,
		// Token: 0x04001038 RID: 4152
		msosptAccentCallout90 = 179,
		// Token: 0x04001039 RID: 4153
		msosptActionButtonBackPrevious = 194,
		// Token: 0x0400103A RID: 4154
		msosptActionButtonBeginning = 196,
		// Token: 0x0400103B RID: 4155
		msosptActionButtonBlank = 189,
		// Token: 0x0400103C RID: 4156
		msosptActionButtonDocument = 198,
		// Token: 0x0400103D RID: 4157
		msosptActionButtonEnd = 195,
		// Token: 0x0400103E RID: 4158
		msosptActionButtonForwardNext = 193,
		// Token: 0x0400103F RID: 4159
		msosptActionButtonHelp = 191,
		// Token: 0x04001040 RID: 4160
		msosptActionButtonHome = 190,
		// Token: 0x04001041 RID: 4161
		msosptActionButtonInformation = 192,
		// Token: 0x04001042 RID: 4162
		msosptActionButtonMovie = 200,
		// Token: 0x04001043 RID: 4163
		msosptActionButtonReturn = 197,
		// Token: 0x04001044 RID: 4164
		msosptActionButtonSound = 199,
		// Token: 0x04001045 RID: 4165
		msosptArc = 19,
		// Token: 0x04001046 RID: 4166
		msosptArrow = 13,
		// Token: 0x04001047 RID: 4167
		msosptBalloon = 17,
		// Token: 0x04001048 RID: 4168
		msosptBentArrow = 91,
		// Token: 0x04001049 RID: 4169
		msosptBentConnector2 = 33,
		// Token: 0x0400104A RID: 4170
		msosptBentConnector3,
		// Token: 0x0400104B RID: 4171
		msosptBentConnector4,
		// Token: 0x0400104C RID: 4172
		msosptBentConnector5,
		// Token: 0x0400104D RID: 4173
		msosptBentUpArrow = 90,
		// Token: 0x0400104E RID: 4174
		msosptBevel = 84,
		// Token: 0x0400104F RID: 4175
		msosptBlockArc = 95,
		// Token: 0x04001050 RID: 4176
		msosptBorderCallout1 = 47,
		// Token: 0x04001051 RID: 4177
		msosptBorderCallout2,
		// Token: 0x04001052 RID: 4178
		msosptBorderCallout3,
		// Token: 0x04001053 RID: 4179
		msosptBorderCallout90 = 180,
		// Token: 0x04001054 RID: 4180
		msosptBracePair = 186,
		// Token: 0x04001055 RID: 4181
		msosptBracketPair = 185,
		// Token: 0x04001056 RID: 4182
		msosptCallout1 = 41,
		// Token: 0x04001057 RID: 4183
		msosptCallout2,
		// Token: 0x04001058 RID: 4184
		msosptCallout3,
		// Token: 0x04001059 RID: 4185
		msosptCallout90 = 178,
		// Token: 0x0400105A RID: 4186
		msosptCan = 22,
		// Token: 0x0400105B RID: 4187
		msosptChevron = 55,
		// Token: 0x0400105C RID: 4188
		msosptCircularArrow = 99,
		// Token: 0x0400105D RID: 4189
		msosptCloudCallout = 106,
		// Token: 0x0400105E RID: 4190
		msosptCube = 16,
		// Token: 0x0400105F RID: 4191
		msosptCurvedConnector2 = 37,
		// Token: 0x04001060 RID: 4192
		msosptCurvedConnector3,
		// Token: 0x04001061 RID: 4193
		msosptCurvedConnector4,
		// Token: 0x04001062 RID: 4194
		msosptCurvedConnector5,
		// Token: 0x04001063 RID: 4195
		msosptCurvedDownArrow = 105,
		// Token: 0x04001064 RID: 4196
		msosptCurvedLeftArrow = 103,
		// Token: 0x04001065 RID: 4197
		msosptCurvedRightArrow = 102,
		// Token: 0x04001066 RID: 4198
		msosptCurvedUpArrow = 104,
		// Token: 0x04001067 RID: 4199
		msosptCustomShape = 100,
		// Token: 0x04001068 RID: 4200
		msosptDiamond = 4,
		// Token: 0x04001069 RID: 4201
		msosptDonut = 23,
		// Token: 0x0400106A RID: 4202
		msosptDoubleWave = 188,
		// Token: 0x0400106B RID: 4203
		msosptDownArrow = 67,
		// Token: 0x0400106C RID: 4204
		msosptDownArrowCallout = 80,
		// Token: 0x0400106D RID: 4205
		msosptEllipse = 3,
		// Token: 0x0400106E RID: 4206
		msosptEllipseRibbon = 107,
		// Token: 0x0400106F RID: 4207
		msosptEllipseRibbon2,
		// Token: 0x04001070 RID: 4208
		msosptFlowChartAlternateProcess = 176,
		// Token: 0x04001071 RID: 4209
		msosptFlowChartCollate = 125,
		// Token: 0x04001072 RID: 4210
		msosptFlowChartConnector = 120,
		// Token: 0x04001073 RID: 4211
		msosptFlowChartDecision = 110,
		// Token: 0x04001074 RID: 4212
		msosptFlowChartDelay = 135,
		// Token: 0x04001075 RID: 4213
		msosptFlowChartDisplay = 134,
		// Token: 0x04001076 RID: 4214
		msosptFlowChartDocument = 114,
		// Token: 0x04001077 RID: 4215
		msosptFlowChartExtract = 127,
		// Token: 0x04001078 RID: 4216
		msosptFlowChartInputOutput = 111,
		// Token: 0x04001079 RID: 4217
		msosptFlowChartInternalStorage = 113,
		// Token: 0x0400107A RID: 4218
		msosptFlowChartMagneticDisk = 132,
		// Token: 0x0400107B RID: 4219
		msosptFlowChartMagneticDrum,
		// Token: 0x0400107C RID: 4220
		msosptFlowChartMagneticTape = 131,
		// Token: 0x0400107D RID: 4221
		msosptFlowChartManualInput = 118,
		// Token: 0x0400107E RID: 4222
		msosptFlowChartManualOperation,
		// Token: 0x0400107F RID: 4223
		msosptFlowChartMerge = 128,
		// Token: 0x04001080 RID: 4224
		msosptFlowChartMultidocument = 115,
		// Token: 0x04001081 RID: 4225
		msosptFlowChartOfflineStorage = 129,
		// Token: 0x04001082 RID: 4226
		msosptFlowChartOffpageConnector = 177,
		// Token: 0x04001083 RID: 4227
		msosptFlowChartOnlineStorage = 130,
		// Token: 0x04001084 RID: 4228
		msosptFlowChartOr = 124,
		// Token: 0x04001085 RID: 4229
		msosptFlowChartPredefinedProcess = 112,
		// Token: 0x04001086 RID: 4230
		msosptFlowChartPreparation = 117,
		// Token: 0x04001087 RID: 4231
		msosptFlowChartProcess = 109,
		// Token: 0x04001088 RID: 4232
		msosptFlowChartPunchedCard = 121,
		// Token: 0x04001089 RID: 4233
		msosptFlowChartPunchedTape,
		// Token: 0x0400108A RID: 4234
		msosptFlowChartSort = 126,
		// Token: 0x0400108B RID: 4235
		msosptFlowChartSummingJunction = 123,
		// Token: 0x0400108C RID: 4236
		msosptFlowChartTerminator = 116,
		// Token: 0x0400108D RID: 4237
		msosptFoldedCorner = 65,
		// Token: 0x0400108E RID: 4238
		msosptGroup = -1,
		// Token: 0x0400108F RID: 4239
		msosptHeart = 74,
		// Token: 0x04001090 RID: 4240
		msosptHexagon = 9,
		// Token: 0x04001091 RID: 4241
		msosptHomePlate = 15,
		// Token: 0x04001092 RID: 4242
		msosptHorizontalScroll = 98,
		// Token: 0x04001093 RID: 4243
		msosptHostControl = 201,
		// Token: 0x04001094 RID: 4244
		msosptPictureFrame = 75,
		// Token: 0x04001095 RID: 4245
		msosptIrregularSeal1 = 71,
		// Token: 0x04001096 RID: 4246
		msosptIrregularSeal2,
		// Token: 0x04001097 RID: 4247
		msosptLeftArrow = 66,
		// Token: 0x04001098 RID: 4248
		msosptLeftArrowCallout = 77,
		// Token: 0x04001099 RID: 4249
		msosptLeftBrace = 87,
		// Token: 0x0400109A RID: 4250
		msosptLeftBracket = 85,
		// Token: 0x0400109B RID: 4251
		msosptLeftRightArrow = 69,
		// Token: 0x0400109C RID: 4252
		msosptLeftRightArrowCallout = 81,
		// Token: 0x0400109D RID: 4253
		msosptLeftRightUpArrow = 182,
		// Token: 0x0400109E RID: 4254
		msosptLeftUpArrow = 89,
		// Token: 0x0400109F RID: 4255
		msosptLightningBolt = 73,
		// Token: 0x040010A0 RID: 4256
		msosptLine = 20,
		// Token: 0x040010A1 RID: 4257
		msosptMoon = 184,
		// Token: 0x040010A2 RID: 4258
		msosptMin = 0,
		// Token: 0x040010A3 RID: 4259
		msosptNoSmoking = 57,
		// Token: 0x040010A4 RID: 4260
		msosptNotchedRightArrow = 94,
		// Token: 0x040010A5 RID: 4261
		msosptOctagon = 10,
		// Token: 0x040010A6 RID: 4262
		msosptOleControl = -3,
		// Token: 0x040010A7 RID: 4263
		msosptOleObject,
		// Token: 0x040010A8 RID: 4264
		msosptParallelogram = 7,
		// Token: 0x040010A9 RID: 4265
		msosptPentagon = 56,
		// Token: 0x040010AA RID: 4266
		msosptPlaque = 21,
		// Token: 0x040010AB RID: 4267
		msosptPlus = 11,
		// Token: 0x040010AC RID: 4268
		msosptQuadArrow = 76,
		// Token: 0x040010AD RID: 4269
		msosptQuadArrowCallout = 83,
		// Token: 0x040010AE RID: 4270
		msosptRectangle = 1,
		// Token: 0x040010AF RID: 4271
		msosptRibbon = 53,
		// Token: 0x040010B0 RID: 4272
		msosptRibbon2,
		// Token: 0x040010B1 RID: 4273
		msosptRightArrowCallout = 78,
		// Token: 0x040010B2 RID: 4274
		msosptRightBrace = 88,
		// Token: 0x040010B3 RID: 4275
		msosptRightBracket = 86,
		// Token: 0x040010B4 RID: 4276
		msosptRightTriangle = 6,
		// Token: 0x040010B5 RID: 4277
		msosptRoundRectangle = 2,
		// Token: 0x040010B6 RID: 4278
		msosptSeal = 18,
		// Token: 0x040010B7 RID: 4279
		msosptSeal16 = 59,
		// Token: 0x040010B8 RID: 4280
		msosptSeal24 = 92,
		// Token: 0x040010B9 RID: 4281
		msosptSeal32 = 60,
		// Token: 0x040010BA RID: 4282
		msosptSeal4 = 187,
		// Token: 0x040010BB RID: 4283
		msosptSeal8 = 58,
		// Token: 0x040010BC RID: 4284
		msosptSmileyFace = 96,
		// Token: 0x040010BD RID: 4285
		msosptStar = 12,
		// Token: 0x040010BE RID: 4286
		msosptStraightConnector1 = 32,
		// Token: 0x040010BF RID: 4287
		msosptStripedRightArrow = 93,
		// Token: 0x040010C0 RID: 4288
		msosptSun = 183,
		// Token: 0x040010C1 RID: 4289
		msosptTextBox = 202,
		// Token: 0x040010C2 RID: 4290
		msosptTextArchDownCurve = 145,
		// Token: 0x040010C3 RID: 4291
		msosptTextArchDownPour = 149,
		// Token: 0x040010C4 RID: 4292
		msosptTextArchUpCurve = 144,
		// Token: 0x040010C5 RID: 4293
		msosptTextArchUpPour = 148,
		// Token: 0x040010C6 RID: 4294
		msosptTextButtonCurve = 147,
		// Token: 0x040010C7 RID: 4295
		msosptTextButtonPour = 151,
		// Token: 0x040010C8 RID: 4296
		msosptTextCanDown = 175,
		// Token: 0x040010C9 RID: 4297
		msosptTextCanUp = 174,
		// Token: 0x040010CA RID: 4298
		msosptTextCascadeDown = 155,
		// Token: 0x040010CB RID: 4299
		msosptTextCascadeUp = 154,
		// Token: 0x040010CC RID: 4300
		msosptTextChevron = 140,
		// Token: 0x040010CD RID: 4301
		msosptTextChevronInverted,
		// Token: 0x040010CE RID: 4302
		msosptTextCircleCurve = 146,
		// Token: 0x040010CF RID: 4303
		msosptTextCirclePour = 150,
		// Token: 0x040010D0 RID: 4304
		msosptTextCurve = 27,
		// Token: 0x040010D1 RID: 4305
		msosptTextCurveDown = 153,
		// Token: 0x040010D2 RID: 4306
		msosptTextCurveUp = 152,
		// Token: 0x040010D3 RID: 4307
		msosptTextDeflate = 161,
		// Token: 0x040010D4 RID: 4308
		msosptTextDeflateBottom = 163,
		// Token: 0x040010D5 RID: 4309
		msosptTextDeflateInflate = 166,
		// Token: 0x040010D6 RID: 4310
		msosptTextDeflateInflateDeflate,
		// Token: 0x040010D7 RID: 4311
		msosptTextDeflateTop = 165,
		// Token: 0x040010D8 RID: 4312
		msosptTextFadeDown = 171,
		// Token: 0x040010D9 RID: 4313
		msosptTextFadeLeft = 169,
		// Token: 0x040010DA RID: 4314
		msosptTextFadeRight = 168,
		// Token: 0x040010DB RID: 4315
		msosptTextFadeUp = 170,
		// Token: 0x040010DC RID: 4316
		msosptTextHexagon = 26,
		// Token: 0x040010DD RID: 4317
		msosptTextInflate = 160,
		// Token: 0x040010DE RID: 4318
		msosptTextInflateBottom = 162,
		// Token: 0x040010DF RID: 4319
		msosptTextInflateTop = 164,
		// Token: 0x040010E0 RID: 4320
		msosptTextOctagon = 25,
		// Token: 0x040010E1 RID: 4321
		msosptTextOnCurve = 30,
		// Token: 0x040010E2 RID: 4322
		msosptTextOnRing,
		// Token: 0x040010E3 RID: 4323
		msosptTextPlainText = 136,
		// Token: 0x040010E4 RID: 4324
		msosptTextRing = 29,
		// Token: 0x040010E5 RID: 4325
		msosptTextRingInside = 142,
		// Token: 0x040010E6 RID: 4326
		msosptTextRingOutside,
		// Token: 0x040010E7 RID: 4327
		msosptTextSimple = 24,
		// Token: 0x040010E8 RID: 4328
		msosptTextSlantDown = 173,
		// Token: 0x040010E9 RID: 4329
		msosptTextSlantUp = 172,
		// Token: 0x040010EA RID: 4330
		msosptTextStop = 137,
		// Token: 0x040010EB RID: 4331
		msosptTextTriangle,
		// Token: 0x040010EC RID: 4332
		msosptTextTriangleInverted,
		// Token: 0x040010ED RID: 4333
		msosptTextWave = 28,
		// Token: 0x040010EE RID: 4334
		msosptTextWave1 = 156,
		// Token: 0x040010EF RID: 4335
		msosptTextWave2,
		// Token: 0x040010F0 RID: 4336
		msosptTextWave3,
		// Token: 0x040010F1 RID: 4337
		msosptTextWave4,
		// Token: 0x040010F2 RID: 4338
		msosptThickArrow = 14,
		// Token: 0x040010F3 RID: 4339
		msosptTrapezoid = 8,
		// Token: 0x040010F4 RID: 4340
		msosptTriangle = 5,
		// Token: 0x040010F5 RID: 4341
		msosptUpArrow = 68,
		// Token: 0x040010F6 RID: 4342
		msosptUpArrowCallout = 79,
		// Token: 0x040010F7 RID: 4343
		msosptUpDownArrow = 70,
		// Token: 0x040010F8 RID: 4344
		msosptUpDownArrowCallout = 82,
		// Token: 0x040010F9 RID: 4345
		msosptUturnArrow = 101,
		// Token: 0x040010FA RID: 4346
		msosptVerticalScroll = 97,
		// Token: 0x040010FB RID: 4347
		msosptWave = 64,
		// Token: 0x040010FC RID: 4348
		msosptWedgeEllipseCallout = 63,
		// Token: 0x040010FD RID: 4349
		msosptWedgeRectCallout = 61,
		// Token: 0x040010FE RID: 4350
		msosptWedgeRRectCallout
	}
}
