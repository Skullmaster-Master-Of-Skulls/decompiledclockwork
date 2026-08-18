using System;

namespace OracleInternal.BinXml
{
	// Token: 0x0200001A RID: 26
	internal class ObxmlInstructionFormat
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00008F14 File Offset: 0x00007114
		private ObxmlInstructionFormat(string name, bool startframe, bool endframe, int flags, bool hasfixeddata, bool hasvardata, int opnum, int op1len, int op2len, int op3len, int op4len, int fixeddatalen)
		{
			this.name = name;
			this.startframe = startframe;
			this.endframe = endframe;
			this.flags = flags;
			this.hasfixeddata = hasfixeddata;
			this.hasvardata = hasvardata;
			this.opnum = opnum;
			this.oplen = new int[4];
			this.oplen[0] = op1len;
			this.oplen[1] = op2len;
			this.oplen[2] = op3len;
			this.oplen[3] = op4len;
			this.skiplen = (hasvardata ? (op2len + op3len) : (op1len + op2len + op3len));
			this.fixeddatalen = fixeddatalen;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008FB4 File Offset: 0x000071B4
		internal static ObxmlInstructionFormat CreateFormatFixedData(string name, int fixeddatalen)
		{
			return new ObxmlInstructionFormat(name, false, false, 0, true, false, 0, 0, 0, 0, 0, fixeddatalen);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00008FD4 File Offset: 0x000071D4
		internal static ObxmlInstructionFormat CreateFormatEntry(string name, bool hasvardata, int numops, int op1len, int op2len, int op3len)
		{
			return new ObxmlInstructionFormat(name, false, false, 0, false, hasvardata, numops, op1len, op2len, op3len, 0, 0);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008FF4 File Offset: 0x000071F4
		internal static ObxmlInstructionFormat CreateFormatEntry4(string name, bool hasvardata, int op1len, int op2len, int op3len, int op4len)
		{
			return new ObxmlInstructionFormat(name, false, false, 0, false, hasvardata, 4, op1len, op2len, op3len, op4len, 0);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009014 File Offset: 0x00007214
		internal static ObxmlInstructionFormat CreateFormatEntryF(string name, int flags, bool hasvardata, int numops, int op1len, int op2len, int op3len)
		{
			return new ObxmlInstructionFormat(name, false, false, flags, false, hasvardata, numops, op1len, op2len, op3len, 0, 0);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00009038 File Offset: 0x00007238
		internal static ObxmlInstructionFormat CreateFormatEntrySF(string name, int flags, bool hasvardata, int numops, int op1len, int op2len, int op3len)
		{
			return new ObxmlInstructionFormat(name, true, false, flags, false, hasvardata, numops, op1len, op2len, op3len, 0, 0);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000905C File Offset: 0x0000725C
		internal static ObxmlInstructionFormat CreateFormatEntryEF(string name, bool hasvardata, int numops, int op1len, int op2len, int op3len)
		{
			return new ObxmlInstructionFormat(name, false, true, 0, false, hasvardata, numops, op1len, op2len, op3len, 0, 0);
		}

		// Token: 0x040000E3 RID: 227
		internal static readonly short NOOP = 255;

		// Token: 0x040000E4 RID: 228
		internal static readonly int HDR_RGUID_LEN = 16;

		// Token: 0x040000E5 RID: 229
		internal static readonly byte CSX_MAX_SCHID_LEN = 16;

		// Token: 0x040000E6 RID: 230
		internal static readonly byte CSX_MAX_PFX_LEN = 100;

		// Token: 0x040000E7 RID: 231
		internal static readonly byte CSX_DEFQNF_ATTR = 1;

		// Token: 0x040000E8 RID: 232
		internal static readonly int KIDNUM = 1;

		// Token: 0x040000E9 RID: 233
		internal static readonly int TOKENID = 2;

		// Token: 0x040000EA RID: 234
		internal static readonly int TYPDATA = 4;

		// Token: 0x040000EB RID: 235
		internal static readonly int DTDLEN = 8;

		// Token: 0x040000EC RID: 236
		internal static readonly int STANDALONE_SPECIFIED = 1;

		// Token: 0x040000ED RID: 237
		internal static readonly int PROLOG_SPECIFIED = 2;

		// Token: 0x040000EE RID: 238
		internal static readonly int ENCODING_SPECIFIED = 4;

		// Token: 0x040000EF RID: 239
		internal static readonly int VERSION_SPECIFIED = 8;

		// Token: 0x040000F0 RID: 240
		internal static readonly int STANDALONE_TRUE = 16;

		// Token: 0x040000F1 RID: 241
		internal static readonly int VERSION_MASK = 65280;

		// Token: 0x040000F2 RID: 242
		internal static readonly int VERSION_ELEVEN = 4352;

		// Token: 0x040000F3 RID: 243
		internal static readonly int ELSTF_SSEQ = 1;

		// Token: 0x040000F4 RID: 244
		internal static readonly int ELSTF_NOTDECTYP = 2;

		// Token: 0x040000F5 RID: 245
		internal static readonly int ELSTF_IMPTYP = 4;

		// Token: 0x040000F6 RID: 246
		internal static readonly int ELSTF_PFXID = 8;

		// Token: 0x040000F7 RID: 247
		internal string name;

		// Token: 0x040000F8 RID: 248
		internal bool startframe;

		// Token: 0x040000F9 RID: 249
		internal bool endframe;

		// Token: 0x040000FA RID: 250
		internal int flags;

		// Token: 0x040000FB RID: 251
		internal bool hasfixeddata;

		// Token: 0x040000FC RID: 252
		internal bool hasvardata;

		// Token: 0x040000FD RID: 253
		internal int opnum;

		// Token: 0x040000FE RID: 254
		internal int[] oplen;

		// Token: 0x040000FF RID: 255
		internal int skiplen;

		// Token: 0x04000100 RID: 256
		internal int fixeddatalen;

		// Token: 0x04000101 RID: 257
		internal static ObxmlInstructionFormat[] InstructionFormats = new ObxmlInstructionFormat[]
		{
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR1", 1),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR2", 2),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR3", 3),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR4", 4),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR5", 5),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR6", 6),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR7", 7),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR8", 8),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR9", 9),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR10", 10),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR11", 11),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR12", 12),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR13", 13),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR14", 14),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR15", 15),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR16", 16),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR17", 17),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR18", 18),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR19", 19),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR20", 20),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR21", 21),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR22", 22),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR23", 23),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR24", 24),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR25", 25),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR26", 26),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR27", 27),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR28", 28),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR29", 29),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR30", 30),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR31", 31),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR32", 32),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR33", 33),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR34", 34),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR35", 35),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR36", 36),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR37", 37),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR38", 38),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR39", 39),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR40", 40),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR41", 41),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR42", 42),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR43", 43),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR44", 44),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR45", 45),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR46", 46),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR47", 47),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR48", 48),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR49", 49),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR50", 50),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR51", 51),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR52", 52),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR53", 53),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR54", 54),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR55", 55),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR56", 56),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR57", 57),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR58", 58),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR59", 59),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR60", 60),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR61", 61),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR62", 62),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR63", 63),
			ObxmlInstructionFormat.CreateFormatFixedData("DATSTR64", 64),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN1", 1),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN2", 2),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN3", 3),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN4", 4),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN5", 5),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN6", 6),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN7", 7),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN8", 8),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN9", 9),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN10", 10),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN11", 11),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN12", 12),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN13", 13),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN14", 14),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN15", 15),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN16", 16),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN17", 17),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN18", 18),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN19", 19),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN20", 20),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN21", 21),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN22", 22),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN23", 23),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN24", 24),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN25", 25),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN26", 26),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN27", 27),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN28", 28),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN29", 29),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN30", 30),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN31", 31),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBIN32", 32),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM1", 1),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM2", 2),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM3", 3),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM4", 4),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM5", 5),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM6", 6),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM7", 7),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM8", 8),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM9", 9),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM10", 10),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM11", 11),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM12", 12),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM13", 13),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM14", 14),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM15", 15),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM16", 16),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM17", 17),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM18", 18),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM19", 19),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM20", 20),
			ObxmlInstructionFormat.CreateFormatFixedData("DATNM21", 21),
			ObxmlInstructionFormat.CreateFormatFixedData("DATINT1", 1),
			ObxmlInstructionFormat.CreateFormatFixedData("DATINT2", 2),
			ObxmlInstructionFormat.CreateFormatFixedData("DATINT4", 4),
			ObxmlInstructionFormat.CreateFormatFixedData("DATINT8", 8),
			ObxmlInstructionFormat.CreateFormatFixedData("DATUINT1", 1),
			ObxmlInstructionFormat.CreateFormatFixedData("DATUINT2", 2),
			ObxmlInstructionFormat.CreateFormatFixedData("DATUINT4", 4),
			ObxmlInstructionFormat.CreateFormatFixedData("DATUINT8", 8),
			ObxmlInstructionFormat.CreateFormatFixedData("DATFLT4", 4),
			ObxmlInstructionFormat.CreateFormatFixedData("DATFLT8", 8),
			ObxmlInstructionFormat.CreateFormatFixedData("DATEPH4", 4),
			ObxmlInstructionFormat.CreateFormatFixedData("DATEPH8", 8),
			ObxmlInstructionFormat.CreateFormatFixedData("DATEPZ6", 4),
			ObxmlInstructionFormat.CreateFormatFixedData("DATEPZ10", 8),
			ObxmlInstructionFormat.CreateFormatFixedData("DATODT", 7),
			ObxmlInstructionFormat.CreateFormatFixedData("DATOTS", 11),
			ObxmlInstructionFormat.CreateFormatFixedData("DATOTSZ", 13),
			ObxmlInstructionFormat.CreateFormatFixedData("DATBOL", 1),
			ObxmlInstructionFormat.CreateFormatFixedData("DATQNM", 7),
			ObxmlInstructionFormat.CreateFormatFixedData("DATENM1", 1),
			ObxmlInstructionFormat.CreateFormatFixedData("DATENM2", 2),
			ObxmlInstructionFormat.CreateFormatEntryF("DATAL2", ObxmlInstructionFormat.TYPDATA, true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DATAL8", ObxmlInstructionFormat.TYPDATA, true, 1, 8, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DATATL1", ObxmlInstructionFormat.TYPDATA, true, 2, 1, 4, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DATATL2", ObxmlInstructionFormat.TYPDATA, true, 2, 2, 4, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DATATL8", ObxmlInstructionFormat.TYPDATA, true, 2, 8, 4, 0),
			ObxmlInstructionFormat.CreateFormatFixedData("DATEMPT", 0),
			ObxmlInstructionFormat.CreateFormatEntry("DATNULL", false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("SCHSST1", 0, true, 2, 1, 1, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("SCHSST4", 0, true, 2, 1, 4, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("SCHSST4V", 0, true, 2, 1, 4, 0),
			ObxmlInstructionFormat.CreateFormatEntryEF("SCHSEND", false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DTDSTR", ObxmlInstructionFormat.DTDLEN, true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DTDELEM", ObxmlInstructionFormat.DTDLEN, true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DTDALIST", ObxmlInstructionFormat.DTDLEN, true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DTDENT", ObxmlInstructionFormat.DTDLEN, true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DTDPENT", ObxmlInstructionFormat.DTDLEN, true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DTDNOT", ObxmlInstructionFormat.DTDLEN, true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("DTDEND", false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("ENTREF", false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("CHARREF", false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("DOC", true, 2, 1, 2, 0),
			ObxmlInstructionFormat.CreateFormatEntry("STRTSEC", false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("ENDSEC", false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("CHUNK", false, 2, 1, 4, 0),
			ObxmlInstructionFormat.CreateFormatEntry("REF", true, 1, 1, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("TEXT1", true, 1, 1, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("TEXT2", true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("TEXT8", true, 1, 8, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("CDATA1", true, 1, 1, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("CDATA2", true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("CDATA8", true, 1, 8, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("PI1L1", true, 2, 1, 1, 0),
			ObxmlInstructionFormat.CreateFormatEntry("PI2L4", true, 2, 4, 2, 0),
			ObxmlInstructionFormat.CreateFormatEntry("CMT1", true, 1, 1, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("CMT2", true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("CMT8", true, 1, 8, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("DEFNM4L1", true, 2, 1, 4, 0),
			ObxmlInstructionFormat.CreateFormatEntry("DEFNM4L2", true, 2, 2, 4, 0),
			ObxmlInstructionFormat.CreateFormatEntry("DEFNM8L1", true, 2, 1, 8, 0),
			ObxmlInstructionFormat.CreateFormatEntry("DEFNM8L2", true, 2, 2, 8, 0),
			ObxmlInstructionFormat.CreateFormatEntry("DEFPFX4", true, 3, 1, 4, 2),
			ObxmlInstructionFormat.CreateFormatEntry("DEFPFX8", true, 3, 1, 8, 2),
			ObxmlInstructionFormat.CreateFormatEntry4("DEFQ4N4L1", true, 1, 1, 4, 4),
			ObxmlInstructionFormat.CreateFormatEntry4("DEFQ4N4L2", true, 2, 1, 4, 4),
			ObxmlInstructionFormat.CreateFormatEntry4("DEFQ4N8L1", true, 1, 1, 4, 8),
			ObxmlInstructionFormat.CreateFormatEntry4("DEFQ4N8L2", true, 2, 1, 4, 8),
			ObxmlInstructionFormat.CreateFormatEntry4("DEFQ8N4L1", true, 1, 1, 8, 4),
			ObxmlInstructionFormat.CreateFormatEntry4("DEFQ8N4L2", true, 2, 1, 8, 4),
			ObxmlInstructionFormat.CreateFormatEntry4("DEFQ8N8L1", true, 1, 1, 8, 8),
			ObxmlInstructionFormat.CreateFormatEntry4("DEFQ8N8L2", true, 2, 1, 8, 8),
			ObxmlInstructionFormat.CreateFormatEntryF("PRPK1L1", ObxmlInstructionFormat.KIDNUM | ObxmlInstructionFormat.TYPDATA, true, 2, 1, 1, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("PRPK1L2", ObxmlInstructionFormat.KIDNUM | ObxmlInstructionFormat.TYPDATA, true, 2, 2, 1, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("PRPK2L1", ObxmlInstructionFormat.KIDNUM | ObxmlInstructionFormat.TYPDATA, true, 2, 1, 2, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("PRPK2L2", ObxmlInstructionFormat.KIDNUM | ObxmlInstructionFormat.TYPDATA, true, 2, 2, 2, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("PRPT2L1", ObxmlInstructionFormat.TOKENID | ObxmlInstructionFormat.TYPDATA, true, 2, 1, 2, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("PRPT2L2", ObxmlInstructionFormat.TOKENID | ObxmlInstructionFormat.TYPDATA, true, 2, 2, 2, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("PRPT4L1", ObxmlInstructionFormat.TOKENID | ObxmlInstructionFormat.TYPDATA, true, 2, 1, 4, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("PRPT4L2", ObxmlInstructionFormat.TOKENID | ObxmlInstructionFormat.TYPDATA, true, 2, 2, 4, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("PRPT8L1", ObxmlInstructionFormat.TOKENID | ObxmlInstructionFormat.TYPDATA, true, 2, 1, 8, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("PRPT8L2", ObxmlInstructionFormat.TOKENID | ObxmlInstructionFormat.TYPDATA, true, 2, 2, 8, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTK1", ObxmlInstructionFormat.KIDNUM, false, 1, 1, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTK2", ObxmlInstructionFormat.KIDNUM, false, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTT2", ObxmlInstructionFormat.TOKENID, false, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTT4", ObxmlInstructionFormat.TOKENID, false, 1, 4, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTT8", ObxmlInstructionFormat.TOKENID, false, 1, 8, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTK1F", ObxmlInstructionFormat.KIDNUM, false, 2, 1, 1, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTK2F", ObxmlInstructionFormat.KIDNUM, false, 2, 1, 1, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTT2F", ObxmlInstructionFormat.TOKENID, false, 2, 2, 1, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTT4F", ObxmlInstructionFormat.TOKENID, false, 2, 4, 1, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTT8F", ObxmlInstructionFormat.TOKENID, false, 2, 8, 1, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTK1V", ObxmlInstructionFormat.KIDNUM, true, 3, 1, 1, 1),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTK2V", ObxmlInstructionFormat.KIDNUM, true, 3, 1, 2, 1),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTT2V", ObxmlInstructionFormat.TOKENID, true, 3, 1, 2, 1),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTT4V", ObxmlInstructionFormat.TOKENID, true, 3, 1, 4, 1),
			ObxmlInstructionFormat.CreateFormatEntrySF("PRPSTT8V", ObxmlInstructionFormat.TOKENID, true, 3, 1, 8, 1),
			ObxmlInstructionFormat.CreateFormatEntrySF("ELMSTART", 0, false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("ELMSTSSEQ", 0, false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("ARRBEG", false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("ARREND", false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryEF("ENDPRP", false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("NOSEQ", false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("NOP", false, 0, 0, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("NOPARR", true, 1, 4, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("NMSPC", false, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("NSP4", true, 2, 1, 4, 0),
			ObxmlInstructionFormat.CreateFormatEntry("NSP8", true, 2, 1, 8, 0),
			ObxmlInstructionFormat.CreateFormatEntrySF("ARRSTK1V", ObxmlInstructionFormat.KIDNUM, true, 3, 1, 1, 1),
			ObxmlInstructionFormat.CreateFormatEntrySF("ARRSTK2V", ObxmlInstructionFormat.KIDNUM, true, 3, 1, 2, 1),
			ObxmlInstructionFormat.CreateFormatEntrySF("ARRSTK4V", ObxmlInstructionFormat.TOKENID, true, 3, 1, 4, 1),
			ObxmlInstructionFormat.CreateFormatEntrySF("ARRSTK8V", ObxmlInstructionFormat.TOKENID, true, 3, 1, 8, 1),
			ObxmlInstructionFormat.CreateFormatEntryF("PRTDATA", ObxmlInstructionFormat.TYPDATA, true, 1, 4, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("PRTDATAT", ObxmlInstructionFormat.TYPDATA, true, 2, 4, 4, 0),
			ObxmlInstructionFormat.CreateFormatEntry("PRTTEXT", true, 1, 4, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("PRTCDATA", true, 1, 4, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("PRTPI", true, 2, 4, 2, 0),
			ObxmlInstructionFormat.CreateFormatEntry("PRTCMT", true, 1, 4, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("SPACE1", false, 1, 1, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("SPACE2", false, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("SPACE8", false, 1, 8, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("XMLDECL", true, 1, 1, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("SPACEQN", true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("SPACEQN8", true, 1, 8, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("ENDPRPSP", true, 1, 1, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("ENDPRPSP8", true, 1, 8, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("DTDDECL", true, 1, 1, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntry("FORMATEXTENSION", false, 0, 0, 0, 0)
		};

		// Token: 0x04000102 RID: 258
		internal static ObxmlInstructionFormat[] ExtendedInstructionFormats = new ObxmlInstructionFormat[]
		{
			ObxmlInstructionFormat.CreateFormatEntryF("DTDSTRE", ObxmlInstructionFormat.DTDLEN, true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DTDNOTE", ObxmlInstructionFormat.DTDLEN, true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DTDENTE", ObxmlInstructionFormat.DTDLEN, true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DTDPENTE", ObxmlInstructionFormat.DTDLEN, true, 1, 2, 0, 0),
			ObxmlInstructionFormat.CreateFormatEntryF("DTDALISTE", ObxmlInstructionFormat.DTDLEN, true, 1, 2, 0, 0)
		};
	}
}
