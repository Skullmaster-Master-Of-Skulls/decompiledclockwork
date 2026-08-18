using System;

namespace OracleInternal.Network
{
	// Token: 0x02000171 RID: 369
	internal class TNSPacketOffsets
	{
		// Token: 0x04001078 RID: 4216
		internal static byte NSPHDLEN = 0;

		// Token: 0x04001079 RID: 4217
		internal static byte NSPHDPSM = 2;

		// Token: 0x0400107A RID: 4218
		internal static byte NSPHDTYP = 4;

		// Token: 0x0400107B RID: 4219
		internal static byte NSPHDFLGS = 5;

		// Token: 0x0400107C RID: 4220
		internal static byte NSPHDHSM = 6;

		// Token: 0x0400107D RID: 4221
		internal static byte NSPSIZHD = 8;

		// Token: 0x0400107E RID: 4222
		internal static byte NSPCNVSN = 8;

		// Token: 0x0400107F RID: 4223
		internal static byte NSPCNLOV = 10;

		// Token: 0x04001080 RID: 4224
		internal static byte NSPCNOPT = 12;

		// Token: 0x04001081 RID: 4225
		internal static byte NSPCNSDU = 14;

		// Token: 0x04001082 RID: 4226
		internal static byte NSPCNTDU = 16;

		// Token: 0x04001083 RID: 4227
		internal static byte NSPCNNTC = 18;

		// Token: 0x04001084 RID: 4228
		internal static byte NSPCNTNA = 20;

		// Token: 0x04001085 RID: 4229
		internal static byte NSPCNONE = 22;

		// Token: 0x04001086 RID: 4230
		internal static byte NSPCNLEN = 24;

		// Token: 0x04001087 RID: 4231
		internal static byte NSPCNOFF = 26;

		// Token: 0x04001088 RID: 4232
		internal static byte NSPCNMXC = 28;

		// Token: 0x04001089 RID: 4233
		internal static byte NSPCNFL0 = 32;

		// Token: 0x0400108A RID: 4234
		internal static byte NSPCNFL1 = 33;

		// Token: 0x0400108B RID: 4235
		internal static byte NSPCNNUL = 34;

		// Token: 0x0400108C RID: 4236
		internal static byte NSPCNTMO = 50;

		// Token: 0x0400108D RID: 4237
		internal static byte NSPCNTCK = 52;

		// Token: 0x0400108E RID: 4238
		internal static byte NSPCNADL = 54;

		// Token: 0x0400108F RID: 4239
		internal static byte NSPCNADF = 56;

		// Token: 0x04001090 RID: 4240
		internal static byte NSPCNDAT = 58;

		// Token: 0x04001091 RID: 4241
		internal static byte NSPACVSN = 8;

		// Token: 0x04001092 RID: 4242
		internal static byte NSPACOPT = 10;

		// Token: 0x04001093 RID: 4243
		internal static byte NSPACSDU = 12;

		// Token: 0x04001094 RID: 4244
		internal static byte NSPACTDU = 14;

		// Token: 0x04001095 RID: 4245
		internal static byte NSPACONE = 16;

		// Token: 0x04001096 RID: 4246
		internal static byte NSPACLEN = 18;

		// Token: 0x04001097 RID: 4247
		internal static byte NSPACOFF = 20;

		// Token: 0x04001098 RID: 4248
		internal static byte NSPACFL0 = 22;

		// Token: 0x04001099 RID: 4249
		internal static byte NSPACFL1 = 23;

		// Token: 0x0400109A RID: 4250
		internal static byte NSPACTMO = 24;

		// Token: 0x0400109B RID: 4251
		internal static byte NSPACTCK = 26;

		// Token: 0x0400109C RID: 4252
		internal static byte NSPACADL = 28;

		// Token: 0x0400109D RID: 4253
		internal static byte NSPACAOF = 30;

		// Token: 0x0400109E RID: 4254
		internal static byte NSPACDAT = 32;

		// Token: 0x0400109F RID: 4255
		internal static byte NSPRFURS = 8;

		// Token: 0x040010A0 RID: 4256
		internal static byte NSPRFSRS = 9;

		// Token: 0x040010A1 RID: 4257
		internal static byte NSPRFLEN = 10;

		// Token: 0x040010A2 RID: 4258
		internal static byte NSPRFDAT = 12;

		// Token: 0x040010A3 RID: 4259
		internal static byte NSPRDLEN = 8;

		// Token: 0x040010A4 RID: 4260
		internal static byte NSPRDDAT = 10;

		// Token: 0x040010A5 RID: 4261
		internal static short NSPDAFLG = 8;

		// Token: 0x040010A6 RID: 4262
		internal static short NSPDADAT = 10;

		// Token: 0x040010A7 RID: 4263
		internal static short NSPMKTYP = 8;

		// Token: 0x040010A8 RID: 4264
		internal static short NSPMKODT = 9;

		// Token: 0x040010A9 RID: 4265
		internal static short NSPMKDAT = 10;

		// Token: 0x040010AA RID: 4266
		internal static ushort NSGDONTCARE = 1;

		// Token: 0x040010AB RID: 4267
		internal static ushort NSGHDX = 2;

		// Token: 0x040010AC RID: 4268
		internal static ushort NSGFDX = 4;

		// Token: 0x040010AD RID: 4269
		internal static ushort NSGHDRCHKSUM = 8;

		// Token: 0x040010AE RID: 4270
		internal static ushort NSGPAKCHKSUM = 16;

		// Token: 0x040010AF RID: 4271
		internal static ushort NSGCHKSUM = TNSPacketOffsets.NSGHDRCHKSUM | TNSPacketOffsets.NSGPAKCHKSUM;

		// Token: 0x040010B0 RID: 4272
		internal static ushort NSGBROKEN = 32;

		// Token: 0x040010B1 RID: 4273
		internal static ushort NSGUSEVIO = 64;

		// Token: 0x040010B2 RID: 4274
		internal static ushort NSGOSAUTHOK = 128;

		// Token: 0x040010B3 RID: 4275
		internal static ushort NSGSENDATTN = 512;

		// Token: 0x040010B4 RID: 4276
		internal static ushort NSGRECVATTN = 1024;

		// Token: 0x040010B5 RID: 4277
		internal static ushort NSGNOATTNPR = 2048;

		// Token: 0x040010B6 RID: 4278
		internal static ushort NSGRAW = 4096;

		// Token: 0x040010B7 RID: 4279
		internal static ushort NSGMLTPLX = 8192;

		// Token: 0x040010B8 RID: 4280
		internal static ushort NSGDHANDOFF = 16384;

		// Token: 0x040010B9 RID: 4281
		internal static ushort NSGNEVER = 32768;

		// Token: 0x040010BA RID: 4282
		internal static readonly ushort NSPMXCDATA = 230;

		// Token: 0x040010BB RID: 4283
		internal static readonly byte NSINADISABLEFORCONNECTION = 4;
	}
}
