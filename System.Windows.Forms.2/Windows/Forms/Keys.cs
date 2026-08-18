using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002B7 RID: 695
	[Flags]
	[TypeConverter(typeof(KeysConverter))]
	[Editor("System.Windows.Forms.Design.ShortcutKeysEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[ComVisible(true)]
	public enum Keys
	{
		// Token: 0x04001143 RID: 4419
		KeyCode = 65535,
		// Token: 0x04001144 RID: 4420
		Modifiers = -65536,
		// Token: 0x04001145 RID: 4421
		None = 0,
		// Token: 0x04001146 RID: 4422
		LButton = 1,
		// Token: 0x04001147 RID: 4423
		RButton = 2,
		// Token: 0x04001148 RID: 4424
		Cancel = 3,
		// Token: 0x04001149 RID: 4425
		MButton = 4,
		// Token: 0x0400114A RID: 4426
		XButton1 = 5,
		// Token: 0x0400114B RID: 4427
		XButton2 = 6,
		// Token: 0x0400114C RID: 4428
		Back = 8,
		// Token: 0x0400114D RID: 4429
		Tab = 9,
		// Token: 0x0400114E RID: 4430
		LineFeed = 10,
		// Token: 0x0400114F RID: 4431
		Clear = 12,
		// Token: 0x04001150 RID: 4432
		Return = 13,
		// Token: 0x04001151 RID: 4433
		Enter = 13,
		// Token: 0x04001152 RID: 4434
		ShiftKey = 16,
		// Token: 0x04001153 RID: 4435
		ControlKey = 17,
		// Token: 0x04001154 RID: 4436
		Menu = 18,
		// Token: 0x04001155 RID: 4437
		Pause = 19,
		// Token: 0x04001156 RID: 4438
		Capital = 20,
		// Token: 0x04001157 RID: 4439
		CapsLock = 20,
		// Token: 0x04001158 RID: 4440
		KanaMode = 21,
		// Token: 0x04001159 RID: 4441
		HanguelMode = 21,
		// Token: 0x0400115A RID: 4442
		HangulMode = 21,
		// Token: 0x0400115B RID: 4443
		JunjaMode = 23,
		// Token: 0x0400115C RID: 4444
		FinalMode = 24,
		// Token: 0x0400115D RID: 4445
		HanjaMode = 25,
		// Token: 0x0400115E RID: 4446
		KanjiMode = 25,
		// Token: 0x0400115F RID: 4447
		Escape = 27,
		// Token: 0x04001160 RID: 4448
		IMEConvert = 28,
		// Token: 0x04001161 RID: 4449
		IMENonconvert = 29,
		// Token: 0x04001162 RID: 4450
		IMEAccept = 30,
		// Token: 0x04001163 RID: 4451
		IMEAceept = 30,
		// Token: 0x04001164 RID: 4452
		IMEModeChange = 31,
		// Token: 0x04001165 RID: 4453
		Space = 32,
		// Token: 0x04001166 RID: 4454
		Prior = 33,
		// Token: 0x04001167 RID: 4455
		PageUp = 33,
		// Token: 0x04001168 RID: 4456
		Next = 34,
		// Token: 0x04001169 RID: 4457
		PageDown = 34,
		// Token: 0x0400116A RID: 4458
		End = 35,
		// Token: 0x0400116B RID: 4459
		Home = 36,
		// Token: 0x0400116C RID: 4460
		Left = 37,
		// Token: 0x0400116D RID: 4461
		Up = 38,
		// Token: 0x0400116E RID: 4462
		Right = 39,
		// Token: 0x0400116F RID: 4463
		Down = 40,
		// Token: 0x04001170 RID: 4464
		Select = 41,
		// Token: 0x04001171 RID: 4465
		Print = 42,
		// Token: 0x04001172 RID: 4466
		Execute = 43,
		// Token: 0x04001173 RID: 4467
		Snapshot = 44,
		// Token: 0x04001174 RID: 4468
		PrintScreen = 44,
		// Token: 0x04001175 RID: 4469
		Insert = 45,
		// Token: 0x04001176 RID: 4470
		Delete = 46,
		// Token: 0x04001177 RID: 4471
		Help = 47,
		// Token: 0x04001178 RID: 4472
		D0 = 48,
		// Token: 0x04001179 RID: 4473
		D1 = 49,
		// Token: 0x0400117A RID: 4474
		D2 = 50,
		// Token: 0x0400117B RID: 4475
		D3 = 51,
		// Token: 0x0400117C RID: 4476
		D4 = 52,
		// Token: 0x0400117D RID: 4477
		D5 = 53,
		// Token: 0x0400117E RID: 4478
		D6 = 54,
		// Token: 0x0400117F RID: 4479
		D7 = 55,
		// Token: 0x04001180 RID: 4480
		D8 = 56,
		// Token: 0x04001181 RID: 4481
		D9 = 57,
		// Token: 0x04001182 RID: 4482
		A = 65,
		// Token: 0x04001183 RID: 4483
		B = 66,
		// Token: 0x04001184 RID: 4484
		C = 67,
		// Token: 0x04001185 RID: 4485
		D = 68,
		// Token: 0x04001186 RID: 4486
		E = 69,
		// Token: 0x04001187 RID: 4487
		F = 70,
		// Token: 0x04001188 RID: 4488
		G = 71,
		// Token: 0x04001189 RID: 4489
		H = 72,
		// Token: 0x0400118A RID: 4490
		I = 73,
		// Token: 0x0400118B RID: 4491
		J = 74,
		// Token: 0x0400118C RID: 4492
		K = 75,
		// Token: 0x0400118D RID: 4493
		L = 76,
		// Token: 0x0400118E RID: 4494
		M = 77,
		// Token: 0x0400118F RID: 4495
		N = 78,
		// Token: 0x04001190 RID: 4496
		O = 79,
		// Token: 0x04001191 RID: 4497
		P = 80,
		// Token: 0x04001192 RID: 4498
		Q = 81,
		// Token: 0x04001193 RID: 4499
		R = 82,
		// Token: 0x04001194 RID: 4500
		S = 83,
		// Token: 0x04001195 RID: 4501
		T = 84,
		// Token: 0x04001196 RID: 4502
		U = 85,
		// Token: 0x04001197 RID: 4503
		V = 86,
		// Token: 0x04001198 RID: 4504
		W = 87,
		// Token: 0x04001199 RID: 4505
		X = 88,
		// Token: 0x0400119A RID: 4506
		Y = 89,
		// Token: 0x0400119B RID: 4507
		Z = 90,
		// Token: 0x0400119C RID: 4508
		LWin = 91,
		// Token: 0x0400119D RID: 4509
		RWin = 92,
		// Token: 0x0400119E RID: 4510
		Apps = 93,
		// Token: 0x0400119F RID: 4511
		Sleep = 95,
		// Token: 0x040011A0 RID: 4512
		NumPad0 = 96,
		// Token: 0x040011A1 RID: 4513
		NumPad1 = 97,
		// Token: 0x040011A2 RID: 4514
		NumPad2 = 98,
		// Token: 0x040011A3 RID: 4515
		NumPad3 = 99,
		// Token: 0x040011A4 RID: 4516
		NumPad4 = 100,
		// Token: 0x040011A5 RID: 4517
		NumPad5 = 101,
		// Token: 0x040011A6 RID: 4518
		NumPad6 = 102,
		// Token: 0x040011A7 RID: 4519
		NumPad7 = 103,
		// Token: 0x040011A8 RID: 4520
		NumPad8 = 104,
		// Token: 0x040011A9 RID: 4521
		NumPad9 = 105,
		// Token: 0x040011AA RID: 4522
		Multiply = 106,
		// Token: 0x040011AB RID: 4523
		Add = 107,
		// Token: 0x040011AC RID: 4524
		Separator = 108,
		// Token: 0x040011AD RID: 4525
		Subtract = 109,
		// Token: 0x040011AE RID: 4526
		Decimal = 110,
		// Token: 0x040011AF RID: 4527
		Divide = 111,
		// Token: 0x040011B0 RID: 4528
		F1 = 112,
		// Token: 0x040011B1 RID: 4529
		F2 = 113,
		// Token: 0x040011B2 RID: 4530
		F3 = 114,
		// Token: 0x040011B3 RID: 4531
		F4 = 115,
		// Token: 0x040011B4 RID: 4532
		F5 = 116,
		// Token: 0x040011B5 RID: 4533
		F6 = 117,
		// Token: 0x040011B6 RID: 4534
		F7 = 118,
		// Token: 0x040011B7 RID: 4535
		F8 = 119,
		// Token: 0x040011B8 RID: 4536
		F9 = 120,
		// Token: 0x040011B9 RID: 4537
		F10 = 121,
		// Token: 0x040011BA RID: 4538
		F11 = 122,
		// Token: 0x040011BB RID: 4539
		F12 = 123,
		// Token: 0x040011BC RID: 4540
		F13 = 124,
		// Token: 0x040011BD RID: 4541
		F14 = 125,
		// Token: 0x040011BE RID: 4542
		F15 = 126,
		// Token: 0x040011BF RID: 4543
		F16 = 127,
		// Token: 0x040011C0 RID: 4544
		F17 = 128,
		// Token: 0x040011C1 RID: 4545
		F18 = 129,
		// Token: 0x040011C2 RID: 4546
		F19 = 130,
		// Token: 0x040011C3 RID: 4547
		F20 = 131,
		// Token: 0x040011C4 RID: 4548
		F21 = 132,
		// Token: 0x040011C5 RID: 4549
		F22 = 133,
		// Token: 0x040011C6 RID: 4550
		F23 = 134,
		// Token: 0x040011C7 RID: 4551
		F24 = 135,
		// Token: 0x040011C8 RID: 4552
		NumLock = 144,
		// Token: 0x040011C9 RID: 4553
		Scroll = 145,
		// Token: 0x040011CA RID: 4554
		LShiftKey = 160,
		// Token: 0x040011CB RID: 4555
		RShiftKey = 161,
		// Token: 0x040011CC RID: 4556
		LControlKey = 162,
		// Token: 0x040011CD RID: 4557
		RControlKey = 163,
		// Token: 0x040011CE RID: 4558
		LMenu = 164,
		// Token: 0x040011CF RID: 4559
		RMenu = 165,
		// Token: 0x040011D0 RID: 4560
		BrowserBack = 166,
		// Token: 0x040011D1 RID: 4561
		BrowserForward = 167,
		// Token: 0x040011D2 RID: 4562
		BrowserRefresh = 168,
		// Token: 0x040011D3 RID: 4563
		BrowserStop = 169,
		// Token: 0x040011D4 RID: 4564
		BrowserSearch = 170,
		// Token: 0x040011D5 RID: 4565
		BrowserFavorites = 171,
		// Token: 0x040011D6 RID: 4566
		BrowserHome = 172,
		// Token: 0x040011D7 RID: 4567
		VolumeMute = 173,
		// Token: 0x040011D8 RID: 4568
		VolumeDown = 174,
		// Token: 0x040011D9 RID: 4569
		VolumeUp = 175,
		// Token: 0x040011DA RID: 4570
		MediaNextTrack = 176,
		// Token: 0x040011DB RID: 4571
		MediaPreviousTrack = 177,
		// Token: 0x040011DC RID: 4572
		MediaStop = 178,
		// Token: 0x040011DD RID: 4573
		MediaPlayPause = 179,
		// Token: 0x040011DE RID: 4574
		LaunchMail = 180,
		// Token: 0x040011DF RID: 4575
		SelectMedia = 181,
		// Token: 0x040011E0 RID: 4576
		LaunchApplication1 = 182,
		// Token: 0x040011E1 RID: 4577
		LaunchApplication2 = 183,
		// Token: 0x040011E2 RID: 4578
		OemSemicolon = 186,
		// Token: 0x040011E3 RID: 4579
		Oem1 = 186,
		// Token: 0x040011E4 RID: 4580
		Oemplus = 187,
		// Token: 0x040011E5 RID: 4581
		Oemcomma = 188,
		// Token: 0x040011E6 RID: 4582
		OemMinus = 189,
		// Token: 0x040011E7 RID: 4583
		OemPeriod = 190,
		// Token: 0x040011E8 RID: 4584
		OemQuestion = 191,
		// Token: 0x040011E9 RID: 4585
		Oem2 = 191,
		// Token: 0x040011EA RID: 4586
		Oemtilde = 192,
		// Token: 0x040011EB RID: 4587
		Oem3 = 192,
		// Token: 0x040011EC RID: 4588
		OemOpenBrackets = 219,
		// Token: 0x040011ED RID: 4589
		Oem4 = 219,
		// Token: 0x040011EE RID: 4590
		OemPipe = 220,
		// Token: 0x040011EF RID: 4591
		Oem5 = 220,
		// Token: 0x040011F0 RID: 4592
		OemCloseBrackets = 221,
		// Token: 0x040011F1 RID: 4593
		Oem6 = 221,
		// Token: 0x040011F2 RID: 4594
		OemQuotes = 222,
		// Token: 0x040011F3 RID: 4595
		Oem7 = 222,
		// Token: 0x040011F4 RID: 4596
		Oem8 = 223,
		// Token: 0x040011F5 RID: 4597
		OemBackslash = 226,
		// Token: 0x040011F6 RID: 4598
		Oem102 = 226,
		// Token: 0x040011F7 RID: 4599
		ProcessKey = 229,
		// Token: 0x040011F8 RID: 4600
		Packet = 231,
		// Token: 0x040011F9 RID: 4601
		Attn = 246,
		// Token: 0x040011FA RID: 4602
		Crsel = 247,
		// Token: 0x040011FB RID: 4603
		Exsel = 248,
		// Token: 0x040011FC RID: 4604
		EraseEof = 249,
		// Token: 0x040011FD RID: 4605
		Play = 250,
		// Token: 0x040011FE RID: 4606
		Zoom = 251,
		// Token: 0x040011FF RID: 4607
		NoName = 252,
		// Token: 0x04001200 RID: 4608
		Pa1 = 253,
		// Token: 0x04001201 RID: 4609
		OemClear = 254,
		// Token: 0x04001202 RID: 4610
		Shift = 65536,
		// Token: 0x04001203 RID: 4611
		Control = 131072,
		// Token: 0x04001204 RID: 4612
		Alt = 262144
	}
}
