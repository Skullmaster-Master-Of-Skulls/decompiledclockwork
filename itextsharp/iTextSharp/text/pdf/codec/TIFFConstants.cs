using System;

namespace iTextSharp.text.pdf.codec
{
	// Token: 0x020004A8 RID: 1192
	public class TIFFConstants
	{
		// Token: 0x04001BC0 RID: 7104
		public const int TIFFTAG_SUBFILETYPE = 254;

		// Token: 0x04001BC1 RID: 7105
		public const int FILETYPE_REDUCEDIMAGE = 1;

		// Token: 0x04001BC2 RID: 7106
		public const int FILETYPE_PAGE = 2;

		// Token: 0x04001BC3 RID: 7107
		public const int FILETYPE_MASK = 4;

		// Token: 0x04001BC4 RID: 7108
		public const int TIFFTAG_OSUBFILETYPE = 255;

		// Token: 0x04001BC5 RID: 7109
		public const int OFILETYPE_IMAGE = 1;

		// Token: 0x04001BC6 RID: 7110
		public const int OFILETYPE_REDUCEDIMAGE = 2;

		// Token: 0x04001BC7 RID: 7111
		public const int OFILETYPE_PAGE = 3;

		// Token: 0x04001BC8 RID: 7112
		public const int TIFFTAG_IMAGEWIDTH = 256;

		// Token: 0x04001BC9 RID: 7113
		public const int TIFFTAG_IMAGELENGTH = 257;

		// Token: 0x04001BCA RID: 7114
		public const int TIFFTAG_BITSPERSAMPLE = 258;

		// Token: 0x04001BCB RID: 7115
		public const int TIFFTAG_COMPRESSION = 259;

		// Token: 0x04001BCC RID: 7116
		public const int COMPRESSION_NONE = 1;

		// Token: 0x04001BCD RID: 7117
		public const int COMPRESSION_CCITTRLE = 2;

		// Token: 0x04001BCE RID: 7118
		public const int COMPRESSION_CCITTFAX3 = 3;

		// Token: 0x04001BCF RID: 7119
		public const int COMPRESSION_CCITTFAX4 = 4;

		// Token: 0x04001BD0 RID: 7120
		public const int COMPRESSION_LZW = 5;

		// Token: 0x04001BD1 RID: 7121
		public const int COMPRESSION_OJPEG = 6;

		// Token: 0x04001BD2 RID: 7122
		public const int COMPRESSION_JPEG = 7;

		// Token: 0x04001BD3 RID: 7123
		public const int COMPRESSION_NEXT = 32766;

		// Token: 0x04001BD4 RID: 7124
		public const int COMPRESSION_CCITTRLEW = 32771;

		// Token: 0x04001BD5 RID: 7125
		public const int COMPRESSION_PACKBITS = 32773;

		// Token: 0x04001BD6 RID: 7126
		public const int COMPRESSION_THUNDERSCAN = 32809;

		// Token: 0x04001BD7 RID: 7127
		public const int COMPRESSION_IT8CTPAD = 32895;

		// Token: 0x04001BD8 RID: 7128
		public const int COMPRESSION_IT8LW = 32896;

		// Token: 0x04001BD9 RID: 7129
		public const int COMPRESSION_IT8MP = 32897;

		// Token: 0x04001BDA RID: 7130
		public const int COMPRESSION_IT8BL = 32898;

		// Token: 0x04001BDB RID: 7131
		public const int COMPRESSION_PIXARFILM = 32908;

		// Token: 0x04001BDC RID: 7132
		public const int COMPRESSION_PIXARLOG = 32909;

		// Token: 0x04001BDD RID: 7133
		public const int COMPRESSION_DEFLATE = 32946;

		// Token: 0x04001BDE RID: 7134
		public const int COMPRESSION_ADOBE_DEFLATE = 8;

		// Token: 0x04001BDF RID: 7135
		public const int COMPRESSION_DCS = 32947;

		// Token: 0x04001BE0 RID: 7136
		public const int COMPRESSION_JBIG = 34661;

		// Token: 0x04001BE1 RID: 7137
		public const int COMPRESSION_SGILOG = 34676;

		// Token: 0x04001BE2 RID: 7138
		public const int COMPRESSION_SGILOG24 = 34677;

		// Token: 0x04001BE3 RID: 7139
		public const int TIFFTAG_PHOTOMETRIC = 262;

		// Token: 0x04001BE4 RID: 7140
		public const int PHOTOMETRIC_MINISWHITE = 0;

		// Token: 0x04001BE5 RID: 7141
		public const int PHOTOMETRIC_MINISBLACK = 1;

		// Token: 0x04001BE6 RID: 7142
		public const int PHOTOMETRIC_RGB = 2;

		// Token: 0x04001BE7 RID: 7143
		public const int PHOTOMETRIC_PALETTE = 3;

		// Token: 0x04001BE8 RID: 7144
		public const int PHOTOMETRIC_MASK = 4;

		// Token: 0x04001BE9 RID: 7145
		public const int PHOTOMETRIC_SEPARATED = 5;

		// Token: 0x04001BEA RID: 7146
		public const int PHOTOMETRIC_YCBCR = 6;

		// Token: 0x04001BEB RID: 7147
		public const int PHOTOMETRIC_CIELAB = 8;

		// Token: 0x04001BEC RID: 7148
		public const int PHOTOMETRIC_LOGL = 32844;

		// Token: 0x04001BED RID: 7149
		public const int PHOTOMETRIC_LOGLUV = 32845;

		// Token: 0x04001BEE RID: 7150
		public const int TIFFTAG_THRESHHOLDING = 263;

		// Token: 0x04001BEF RID: 7151
		public const int THRESHHOLD_BILEVEL = 1;

		// Token: 0x04001BF0 RID: 7152
		public const int THRESHHOLD_HALFTONE = 2;

		// Token: 0x04001BF1 RID: 7153
		public const int THRESHHOLD_ERRORDIFFUSE = 3;

		// Token: 0x04001BF2 RID: 7154
		public const int TIFFTAG_CELLWIDTH = 264;

		// Token: 0x04001BF3 RID: 7155
		public const int TIFFTAG_CELLLENGTH = 265;

		// Token: 0x04001BF4 RID: 7156
		public const int TIFFTAG_FILLORDER = 266;

		// Token: 0x04001BF5 RID: 7157
		public const int FILLORDER_MSB2LSB = 1;

		// Token: 0x04001BF6 RID: 7158
		public const int FILLORDER_LSB2MSB = 2;

		// Token: 0x04001BF7 RID: 7159
		public const int TIFFTAG_DOCUMENTNAME = 269;

		// Token: 0x04001BF8 RID: 7160
		public const int TIFFTAG_IMAGEDESCRIPTION = 270;

		// Token: 0x04001BF9 RID: 7161
		public const int TIFFTAG_MAKE = 271;

		// Token: 0x04001BFA RID: 7162
		public const int TIFFTAG_MODEL = 272;

		// Token: 0x04001BFB RID: 7163
		public const int TIFFTAG_STRIPOFFSETS = 273;

		// Token: 0x04001BFC RID: 7164
		public const int TIFFTAG_ORIENTATION = 274;

		// Token: 0x04001BFD RID: 7165
		public const int ORIENTATION_TOPLEFT = 1;

		// Token: 0x04001BFE RID: 7166
		public const int ORIENTATION_TOPRIGHT = 2;

		// Token: 0x04001BFF RID: 7167
		public const int ORIENTATION_BOTRIGHT = 3;

		// Token: 0x04001C00 RID: 7168
		public const int ORIENTATION_BOTLEFT = 4;

		// Token: 0x04001C01 RID: 7169
		public const int ORIENTATION_LEFTTOP = 5;

		// Token: 0x04001C02 RID: 7170
		public const int ORIENTATION_RIGHTTOP = 6;

		// Token: 0x04001C03 RID: 7171
		public const int ORIENTATION_RIGHTBOT = 7;

		// Token: 0x04001C04 RID: 7172
		public const int ORIENTATION_LEFTBOT = 8;

		// Token: 0x04001C05 RID: 7173
		public const int TIFFTAG_SAMPLESPERPIXEL = 277;

		// Token: 0x04001C06 RID: 7174
		public const int TIFFTAG_ROWSPERSTRIP = 278;

		// Token: 0x04001C07 RID: 7175
		public const int TIFFTAG_STRIPBYTECOUNTS = 279;

		// Token: 0x04001C08 RID: 7176
		public const int TIFFTAG_MINSAMPLEVALUE = 280;

		// Token: 0x04001C09 RID: 7177
		public const int TIFFTAG_MAXSAMPLEVALUE = 281;

		// Token: 0x04001C0A RID: 7178
		public const int TIFFTAG_XRESOLUTION = 282;

		// Token: 0x04001C0B RID: 7179
		public const int TIFFTAG_YRESOLUTION = 283;

		// Token: 0x04001C0C RID: 7180
		public const int TIFFTAG_PLANARCONFIG = 284;

		// Token: 0x04001C0D RID: 7181
		public const int PLANARCONFIG_CONTIG = 1;

		// Token: 0x04001C0E RID: 7182
		public const int PLANARCONFIG_SEPARATE = 2;

		// Token: 0x04001C0F RID: 7183
		public const int TIFFTAG_PAGENAME = 285;

		// Token: 0x04001C10 RID: 7184
		public const int TIFFTAG_XPOSITION = 286;

		// Token: 0x04001C11 RID: 7185
		public const int TIFFTAG_YPOSITION = 287;

		// Token: 0x04001C12 RID: 7186
		public const int TIFFTAG_FREEOFFSETS = 288;

		// Token: 0x04001C13 RID: 7187
		public const int TIFFTAG_FREEBYTECOUNTS = 289;

		// Token: 0x04001C14 RID: 7188
		public const int TIFFTAG_GRAYRESPONSEUNIT = 290;

		// Token: 0x04001C15 RID: 7189
		public const int GRAYRESPONSEUNIT_10S = 1;

		// Token: 0x04001C16 RID: 7190
		public const int GRAYRESPONSEUNIT_100S = 2;

		// Token: 0x04001C17 RID: 7191
		public const int GRAYRESPONSEUNIT_1000S = 3;

		// Token: 0x04001C18 RID: 7192
		public const int GRAYRESPONSEUNIT_10000S = 4;

		// Token: 0x04001C19 RID: 7193
		public const int GRAYRESPONSEUNIT_100000S = 5;

		// Token: 0x04001C1A RID: 7194
		public const int TIFFTAG_GRAYRESPONSECURVE = 291;

		// Token: 0x04001C1B RID: 7195
		public const int TIFFTAG_GROUP3OPTIONS = 292;

		// Token: 0x04001C1C RID: 7196
		public const int GROUP3OPT_2DENCODING = 1;

		// Token: 0x04001C1D RID: 7197
		public const int GROUP3OPT_UNCOMPRESSED = 2;

		// Token: 0x04001C1E RID: 7198
		public const int GROUP3OPT_FILLBITS = 4;

		// Token: 0x04001C1F RID: 7199
		public const int TIFFTAG_GROUP4OPTIONS = 293;

		// Token: 0x04001C20 RID: 7200
		public const int GROUP4OPT_UNCOMPRESSED = 2;

		// Token: 0x04001C21 RID: 7201
		public const int TIFFTAG_RESOLUTIONUNIT = 296;

		// Token: 0x04001C22 RID: 7202
		public const int RESUNIT_NONE = 1;

		// Token: 0x04001C23 RID: 7203
		public const int RESUNIT_INCH = 2;

		// Token: 0x04001C24 RID: 7204
		public const int RESUNIT_CENTIMETER = 3;

		// Token: 0x04001C25 RID: 7205
		public const int TIFFTAG_PAGENUMBER = 297;

		// Token: 0x04001C26 RID: 7206
		public const int TIFFTAG_COLORRESPONSEUNIT = 300;

		// Token: 0x04001C27 RID: 7207
		public const int COLORRESPONSEUNIT_10S = 1;

		// Token: 0x04001C28 RID: 7208
		public const int COLORRESPONSEUNIT_100S = 2;

		// Token: 0x04001C29 RID: 7209
		public const int COLORRESPONSEUNIT_1000S = 3;

		// Token: 0x04001C2A RID: 7210
		public const int COLORRESPONSEUNIT_10000S = 4;

		// Token: 0x04001C2B RID: 7211
		public const int COLORRESPONSEUNIT_100000S = 5;

		// Token: 0x04001C2C RID: 7212
		public const int TIFFTAG_TRANSFERFUNCTION = 301;

		// Token: 0x04001C2D RID: 7213
		public const int TIFFTAG_SOFTWARE = 305;

		// Token: 0x04001C2E RID: 7214
		public const int TIFFTAG_DATETIME = 306;

		// Token: 0x04001C2F RID: 7215
		public const int TIFFTAG_ARTIST = 315;

		// Token: 0x04001C30 RID: 7216
		public const int TIFFTAG_HOSTCOMPUTER = 316;

		// Token: 0x04001C31 RID: 7217
		public const int TIFFTAG_PREDICTOR = 317;

		// Token: 0x04001C32 RID: 7218
		public const int TIFFTAG_WHITEPOINT = 318;

		// Token: 0x04001C33 RID: 7219
		public const int TIFFTAG_PRIMARYCHROMATICITIES = 319;

		// Token: 0x04001C34 RID: 7220
		public const int TIFFTAG_COLORMAP = 320;

		// Token: 0x04001C35 RID: 7221
		public const int TIFFTAG_HALFTONEHINTS = 321;

		// Token: 0x04001C36 RID: 7222
		public const int TIFFTAG_TILEWIDTH = 322;

		// Token: 0x04001C37 RID: 7223
		public const int TIFFTAG_TILELENGTH = 323;

		// Token: 0x04001C38 RID: 7224
		public const int TIFFTAG_TILEOFFSETS = 324;

		// Token: 0x04001C39 RID: 7225
		public const int TIFFTAG_TILEBYTECOUNTS = 325;

		// Token: 0x04001C3A RID: 7226
		public const int TIFFTAG_BADFAXLINES = 326;

		// Token: 0x04001C3B RID: 7227
		public const int TIFFTAG_CLEANFAXDATA = 327;

		// Token: 0x04001C3C RID: 7228
		public const int CLEANFAXDATA_CLEAN = 0;

		// Token: 0x04001C3D RID: 7229
		public const int CLEANFAXDATA_REGENERATED = 1;

		// Token: 0x04001C3E RID: 7230
		public const int CLEANFAXDATA_UNCLEAN = 2;

		// Token: 0x04001C3F RID: 7231
		public const int TIFFTAG_CONSECUTIVEBADFAXLINES = 328;

		// Token: 0x04001C40 RID: 7232
		public const int TIFFTAG_SUBIFD = 330;

		// Token: 0x04001C41 RID: 7233
		public const int TIFFTAG_INKSET = 332;

		// Token: 0x04001C42 RID: 7234
		public const int INKSET_CMYK = 1;

		// Token: 0x04001C43 RID: 7235
		public const int TIFFTAG_INKNAMES = 333;

		// Token: 0x04001C44 RID: 7236
		public const int TIFFTAG_NUMBEROFINKS = 334;

		// Token: 0x04001C45 RID: 7237
		public const int TIFFTAG_DOTRANGE = 336;

		// Token: 0x04001C46 RID: 7238
		public const int TIFFTAG_TARGETPRINTER = 337;

		// Token: 0x04001C47 RID: 7239
		public const int TIFFTAG_EXTRASAMPLES = 338;

		// Token: 0x04001C48 RID: 7240
		public const int EXTRASAMPLE_UNSPECIFIED = 0;

		// Token: 0x04001C49 RID: 7241
		public const int EXTRASAMPLE_ASSOCALPHA = 1;

		// Token: 0x04001C4A RID: 7242
		public const int EXTRASAMPLE_UNASSALPHA = 2;

		// Token: 0x04001C4B RID: 7243
		public const int TIFFTAG_SAMPLEFORMAT = 339;

		// Token: 0x04001C4C RID: 7244
		public const int SAMPLEFORMAT_UINT = 1;

		// Token: 0x04001C4D RID: 7245
		public const int SAMPLEFORMAT_INT = 2;

		// Token: 0x04001C4E RID: 7246
		public const int SAMPLEFORMAT_IEEEFP = 3;

		// Token: 0x04001C4F RID: 7247
		public const int SAMPLEFORMAT_VOID = 4;

		// Token: 0x04001C50 RID: 7248
		public const int SAMPLEFORMAT_COMPLEXINT = 5;

		// Token: 0x04001C51 RID: 7249
		public const int SAMPLEFORMAT_COMPLEXIEEEFP = 6;

		// Token: 0x04001C52 RID: 7250
		public const int TIFFTAG_SMINSAMPLEVALUE = 340;

		// Token: 0x04001C53 RID: 7251
		public const int TIFFTAG_SMAXSAMPLEVALUE = 341;

		// Token: 0x04001C54 RID: 7252
		public const int TIFFTAG_JPEGTABLES = 347;

		// Token: 0x04001C55 RID: 7253
		public const int TIFFTAG_JPEGPROC = 512;

		// Token: 0x04001C56 RID: 7254
		public const int JPEGPROC_BASELINE = 1;

		// Token: 0x04001C57 RID: 7255
		public const int JPEGPROC_LOSSLESS = 14;

		// Token: 0x04001C58 RID: 7256
		public const int TIFFTAG_JPEGIFOFFSET = 513;

		// Token: 0x04001C59 RID: 7257
		public const int TIFFTAG_JPEGIFBYTECOUNT = 514;

		// Token: 0x04001C5A RID: 7258
		public const int TIFFTAG_JPEGRESTARTINTERVAL = 515;

		// Token: 0x04001C5B RID: 7259
		public const int TIFFTAG_JPEGLOSSLESSPREDICTORS = 517;

		// Token: 0x04001C5C RID: 7260
		public const int TIFFTAG_JPEGPOINTTRANSFORM = 518;

		// Token: 0x04001C5D RID: 7261
		public const int TIFFTAG_JPEGQTABLES = 519;

		// Token: 0x04001C5E RID: 7262
		public const int TIFFTAG_JPEGDCTABLES = 520;

		// Token: 0x04001C5F RID: 7263
		public const int TIFFTAG_JPEGACTABLES = 521;

		// Token: 0x04001C60 RID: 7264
		public const int TIFFTAG_YCBCRCOEFFICIENTS = 529;

		// Token: 0x04001C61 RID: 7265
		public const int TIFFTAG_YCBCRSUBSAMPLING = 530;

		// Token: 0x04001C62 RID: 7266
		public const int TIFFTAG_YCBCRPOSITIONING = 531;

		// Token: 0x04001C63 RID: 7267
		public const int YCBCRPOSITION_CENTERED = 1;

		// Token: 0x04001C64 RID: 7268
		public const int YCBCRPOSITION_COSITED = 2;

		// Token: 0x04001C65 RID: 7269
		public const int TIFFTAG_REFERENCEBLACKWHITE = 532;

		// Token: 0x04001C66 RID: 7270
		public const int TIFFTAG_REFPTS = 32953;

		// Token: 0x04001C67 RID: 7271
		public const int TIFFTAG_REGIONTACKPOINT = 32954;

		// Token: 0x04001C68 RID: 7272
		public const int TIFFTAG_REGIONWARPCORNERS = 32955;

		// Token: 0x04001C69 RID: 7273
		public const int TIFFTAG_REGIONAFFINE = 32956;

		// Token: 0x04001C6A RID: 7274
		public const int TIFFTAG_MATTEING = 32995;

		// Token: 0x04001C6B RID: 7275
		public const int TIFFTAG_DATATYPE = 32996;

		// Token: 0x04001C6C RID: 7276
		public const int TIFFTAG_IMAGEDEPTH = 32997;

		// Token: 0x04001C6D RID: 7277
		public const int TIFFTAG_TILEDEPTH = 32998;

		// Token: 0x04001C6E RID: 7278
		public const int TIFFTAG_PIXAR_IMAGEFULLWIDTH = 33300;

		// Token: 0x04001C6F RID: 7279
		public const int TIFFTAG_PIXAR_IMAGEFULLLENGTH = 33301;

		// Token: 0x04001C70 RID: 7280
		public const int TIFFTAG_PIXAR_TEXTUREFORMAT = 33302;

		// Token: 0x04001C71 RID: 7281
		public const int TIFFTAG_PIXAR_WRAPMODES = 33303;

		// Token: 0x04001C72 RID: 7282
		public const int TIFFTAG_PIXAR_FOVCOT = 33304;

		// Token: 0x04001C73 RID: 7283
		public const int TIFFTAG_PIXAR_MATRIX_WORLDTOSCREEN = 33305;

		// Token: 0x04001C74 RID: 7284
		public const int TIFFTAG_PIXAR_MATRIX_WORLDTOCAMERA = 33306;

		// Token: 0x04001C75 RID: 7285
		public const int TIFFTAG_WRITERSERIALNUMBER = 33405;

		// Token: 0x04001C76 RID: 7286
		public const int TIFFTAG_COPYRIGHT = 33432;

		// Token: 0x04001C77 RID: 7287
		public const int TIFFTAG_RICHTIFFIPTC = 33723;

		// Token: 0x04001C78 RID: 7288
		public const int TIFFTAG_IT8SITE = 34016;

		// Token: 0x04001C79 RID: 7289
		public const int TIFFTAG_IT8COLORSEQUENCE = 34017;

		// Token: 0x04001C7A RID: 7290
		public const int TIFFTAG_IT8HEADER = 34018;

		// Token: 0x04001C7B RID: 7291
		public const int TIFFTAG_IT8RASTERPADDING = 34019;

		// Token: 0x04001C7C RID: 7292
		public const int TIFFTAG_IT8BITSPERRUNLENGTH = 34020;

		// Token: 0x04001C7D RID: 7293
		public const int TIFFTAG_IT8BITSPEREXTENDEDRUNLENGTH = 34021;

		// Token: 0x04001C7E RID: 7294
		public const int TIFFTAG_IT8COLORTABLE = 34022;

		// Token: 0x04001C7F RID: 7295
		public const int TIFFTAG_IT8IMAGECOLORINDICATOR = 34023;

		// Token: 0x04001C80 RID: 7296
		public const int TIFFTAG_IT8BKGCOLORINDICATOR = 34024;

		// Token: 0x04001C81 RID: 7297
		public const int TIFFTAG_IT8IMAGECOLORVALUE = 34025;

		// Token: 0x04001C82 RID: 7298
		public const int TIFFTAG_IT8BKGCOLORVALUE = 34026;

		// Token: 0x04001C83 RID: 7299
		public const int TIFFTAG_IT8PIXELINTENSITYRANGE = 34027;

		// Token: 0x04001C84 RID: 7300
		public const int TIFFTAG_IT8TRANSPARENCYINDICATOR = 34028;

		// Token: 0x04001C85 RID: 7301
		public const int TIFFTAG_IT8COLORCHARACTERIZATION = 34029;

		// Token: 0x04001C86 RID: 7302
		public const int TIFFTAG_FRAMECOUNT = 34232;

		// Token: 0x04001C87 RID: 7303
		public const int TIFFTAG_ICCPROFILE = 34675;

		// Token: 0x04001C88 RID: 7304
		public const int TIFFTAG_PHOTOSHOP = 34377;

		// Token: 0x04001C89 RID: 7305
		public const int TIFFTAG_JBIGOPTIONS = 34750;

		// Token: 0x04001C8A RID: 7306
		public const int TIFFTAG_FAXRECVPARAMS = 34908;

		// Token: 0x04001C8B RID: 7307
		public const int TIFFTAG_FAXSUBADDRESS = 34909;

		// Token: 0x04001C8C RID: 7308
		public const int TIFFTAG_FAXRECVTIME = 34910;

		// Token: 0x04001C8D RID: 7309
		public const int TIFFTAG_STONITS = 37439;

		// Token: 0x04001C8E RID: 7310
		public const int TIFFTAG_FEDEX_EDR = 34929;

		// Token: 0x04001C8F RID: 7311
		public const int TIFFTAG_DCSHUESHIFTVALUES = 65535;
	}
}
