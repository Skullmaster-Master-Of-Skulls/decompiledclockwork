using System;
using System.Text;

namespace a.h
{
	// Token: 0x020001FB RID: 507
	internal class g
	{
		// Token: 0x06001050 RID: 4176 RVA: 0x000452BA File Offset: 0x000442BA
		public int f()
		{
			return this.ab;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000452C2 File Offset: 0x000442C2
		public void b(int A_0)
		{
			this.ab = A_0;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x000452CB File Offset: 0x000442CB
		public int b()
		{
			if (this.ad != null)
			{
				return this.ad.Length;
			}
			return 0;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x000452DF File Offset: 0x000442DF
		public a[] e()
		{
			return this.ad;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x000452E7 File Offset: 0x000442E7
		public void a(a[] A_0)
		{
			this.ad = A_0;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x000452F0 File Offset: 0x000442F0
		public object g()
		{
			if (this.b() <= 0 || this.ad[0] == null)
			{
				return null;
			}
			return this.ad[0].f();
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00045314 File Offset: 0x00044314
		public l d()
		{
			return this.ae;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0004531C File Offset: 0x0004431C
		public void a(l A_0)
		{
			this.ae = A_0;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00045325 File Offset: 0x00044325
		public g()
		{
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0004532D File Offset: 0x0004432D
		public g(int A_0, int A_1, a[] A_2)
		{
			this.ab = A_0;
			this.ac = A_1;
			this.ad = A_2;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0004534A File Offset: 0x0004434A
		public int c()
		{
			return this.ac;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00045352 File Offset: 0x00044352
		public void a(int A_0)
		{
			this.ac = A_0;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0004535C File Offset: 0x0004435C
		public void a()
		{
			if (this.ad != null)
			{
				for (int i = 0; i < this.ad.Length; i++)
				{
					if (this.ad[i] != null)
					{
						this.ad[i].c();
					}
				}
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0004539C File Offset: 0x0004439C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MapiProp:").Append(" type=").Append(global::a.h.f.a((long)this.f()));
			if (this.d() != null)
			{
				stringBuilder.Append(" name=").Append(this.d());
			}
			stringBuilder.Append(" ID=").Append(global::a.h.f.a((long)this.c()));
			if (this.b() == 0)
			{
				stringBuilder.Append(" value=").Append(((object)null).ToString());
			}
			else if (this.b() == 1)
			{
				stringBuilder.Append(" value=").Append(this.e()[0]);
			}
			else
			{
				stringBuilder.Append(" values=").Append(this.e());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00045474 File Offset: 0x00044474
		public static g a(g[] A_0, int A_1)
		{
			int num = 0;
			while (A_0 != null && num < A_0.Length)
			{
				if (A_0[num].c() == A_1)
				{
					return A_0[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x000454A4 File Offset: 0x000444A4
		public static g a(g[] A_0, l A_1)
		{
			int num = 0;
			while (A_0 != null && num < A_0.Length)
			{
				if (A_1.Equals(A_0[num].d()))
				{
					return A_0[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x04000C22 RID: 3106
		public const int a = 4096;

		// Token: 0x04000C23 RID: 3107
		public const int b = 0;

		// Token: 0x04000C24 RID: 3108
		public const int c = 1;

		// Token: 0x04000C25 RID: 3109
		public const int d = 2;

		// Token: 0x04000C26 RID: 3110
		public const int e = 3;

		// Token: 0x04000C27 RID: 3111
		public const int f = 4;

		// Token: 0x04000C28 RID: 3112
		public const int g = 5;

		// Token: 0x04000C29 RID: 3113
		public const int h = 6;

		// Token: 0x04000C2A RID: 3114
		public const int i = 7;

		// Token: 0x04000C2B RID: 3115
		public const int j = 10;

		// Token: 0x04000C2C RID: 3116
		public const int k = 11;

		// Token: 0x04000C2D RID: 3117
		public const int l = 13;

		// Token: 0x04000C2E RID: 3118
		public const int m = 20;

		// Token: 0x04000C2F RID: 3119
		public const int n = 30;

		// Token: 0x04000C30 RID: 3120
		public const int o = 31;

		// Token: 0x04000C31 RID: 3121
		public const int p = 64;

		// Token: 0x04000C32 RID: 3122
		public const int q = 72;

		// Token: 0x04000C33 RID: 3123
		public const int r = 258;

		// Token: 0x04000C34 RID: 3124
		public const int s = 0;

		// Token: 0x04000C35 RID: 3125
		public const int t = 1;

		// Token: 0x04000C36 RID: 3126
		public const int u = 2;

		// Token: 0x04000C37 RID: 3127
		public const int v = 3;

		// Token: 0x04000C38 RID: 3128
		public const int w = 268435456;

		// Token: 0x04000C39 RID: 3129
		public const uint x = 2147483648U;

		// Token: 0x04000C3A RID: 3130
		public static readonly Guid y = new Guid("0000000b-0000-0000-c000-000000000046");

		// Token: 0x04000C3B RID: 3131
		public static readonly Guid z = new Guid("00020307-0000-0000-c000-000000000046");

		// Token: 0x04000C3C RID: 3132
		public static readonly Guid aa = new Guid("0000000c-0000-0000-c000-000000000046");

		// Token: 0x04000C3D RID: 3133
		private int ab;

		// Token: 0x04000C3E RID: 3134
		private int ac;

		// Token: 0x04000C3F RID: 3135
		private a[] ad;

		// Token: 0x04000C40 RID: 3136
		private l ae;

		// Token: 0x04000C41 RID: 3137
		public const int af = 1;

		// Token: 0x04000C42 RID: 3138
		public const int ag = 2;

		// Token: 0x04000C43 RID: 3139
		public const int ah = 3;

		// Token: 0x04000C44 RID: 3140
		public const int ai = 4;

		// Token: 0x04000C45 RID: 3141
		public const int aj = 5;

		// Token: 0x04000C46 RID: 3142
		public const int ak = 6;

		// Token: 0x04000C47 RID: 3143
		public const int al = 7;

		// Token: 0x04000C48 RID: 3144
		public const int am = 8;

		// Token: 0x04000C49 RID: 3145
		public const int an = 9;

		// Token: 0x04000C4A RID: 3146
		public const int ao = 10;

		// Token: 0x04000C4B RID: 3147
		public const int ap = 11;

		// Token: 0x04000C4C RID: 3148
		public const int aq = 12;

		// Token: 0x04000C4D RID: 3149
		public const int ar = 13;

		// Token: 0x04000C4E RID: 3150
		public const int @as = 14;

		// Token: 0x04000C4F RID: 3151
		public const int at = 15;

		// Token: 0x04000C50 RID: 3152
		public const int au = 16;

		// Token: 0x04000C51 RID: 3153
		public const int av = 17;

		// Token: 0x04000C52 RID: 3154
		public const int aw = 18;

		// Token: 0x04000C53 RID: 3155
		public const int ax = 19;

		// Token: 0x04000C54 RID: 3156
		public const int ay = 20;

		// Token: 0x04000C55 RID: 3157
		public const int az = 21;

		// Token: 0x04000C56 RID: 3158
		public const int a0 = 22;

		// Token: 0x04000C57 RID: 3159
		public const int a1 = 23;

		// Token: 0x04000C58 RID: 3160
		public const int a2 = 24;

		// Token: 0x04000C59 RID: 3161
		public const int a3 = 25;

		// Token: 0x04000C5A RID: 3162
		public const int a4 = 26;

		// Token: 0x04000C5B RID: 3163
		public const int a5 = 27;

		// Token: 0x04000C5C RID: 3164
		public const int a6 = 30;

		// Token: 0x04000C5D RID: 3165
		public const int a7 = 31;

		// Token: 0x04000C5E RID: 3166
		public const int a8 = 32;

		// Token: 0x04000C5F RID: 3167
		public const int a9 = 33;

		// Token: 0x04000C60 RID: 3168
		public const int ba = 34;

		// Token: 0x04000C61 RID: 3169
		public const int bb = 35;

		// Token: 0x04000C62 RID: 3170
		public const int bc = 36;

		// Token: 0x04000C63 RID: 3171
		public const int bd = 37;

		// Token: 0x04000C64 RID: 3172
		public const int be = 38;

		// Token: 0x04000C65 RID: 3173
		public const int bf = 39;

		// Token: 0x04000C66 RID: 3174
		public const int bg = 40;

		// Token: 0x04000C67 RID: 3175
		public const int bh = 41;

		// Token: 0x04000C68 RID: 3176
		public const int bi = 42;

		// Token: 0x04000C69 RID: 3177
		public const int bj = 43;

		// Token: 0x04000C6A RID: 3178
		public const int bk = 44;

		// Token: 0x04000C6B RID: 3179
		public const int bl = 45;

		// Token: 0x04000C6C RID: 3180
		public const int bm = 46;

		// Token: 0x04000C6D RID: 3181
		public const int bn = 47;

		// Token: 0x04000C6E RID: 3182
		public const int bo = 48;

		// Token: 0x04000C6F RID: 3183
		public const int bp = 49;

		// Token: 0x04000C70 RID: 3184
		public const int bq = 50;

		// Token: 0x04000C71 RID: 3185
		public const int br = 51;

		// Token: 0x04000C72 RID: 3186
		public const int bs = 52;

		// Token: 0x04000C73 RID: 3187
		public const int bt = 53;

		// Token: 0x04000C74 RID: 3188
		public const int bu = 54;

		// Token: 0x04000C75 RID: 3189
		public const int bv = 55;

		// Token: 0x04000C76 RID: 3190
		public const int bw = 56;

		// Token: 0x04000C77 RID: 3191
		public const int bx = 57;

		// Token: 0x04000C78 RID: 3192
		public const int by = 58;

		// Token: 0x04000C79 RID: 3193
		public const int bz = 59;

		// Token: 0x04000C7A RID: 3194
		public const int b0 = 60;

		// Token: 0x04000C7B RID: 3195
		public const int b1 = 61;

		// Token: 0x04000C7C RID: 3196
		public const int b2 = 62;

		// Token: 0x04000C7D RID: 3197
		public const int b3 = 63;

		// Token: 0x04000C7E RID: 3198
		public const int b4 = 64;

		// Token: 0x04000C7F RID: 3199
		public const int b5 = 65;

		// Token: 0x04000C80 RID: 3200
		public const int b6 = 66;

		// Token: 0x04000C81 RID: 3201
		public const int b7 = 67;

		// Token: 0x04000C82 RID: 3202
		public const int b8 = 68;

		// Token: 0x04000C83 RID: 3203
		public const int b9 = 69;

		// Token: 0x04000C84 RID: 3204
		public const int ca = 70;

		// Token: 0x04000C85 RID: 3205
		public const int cb = 71;

		// Token: 0x04000C86 RID: 3206
		public const int cc = 72;

		// Token: 0x04000C87 RID: 3207
		public const int cd = 73;

		// Token: 0x04000C88 RID: 3208
		public const int ce = 74;

		// Token: 0x04000C89 RID: 3209
		public const int cf = 75;

		// Token: 0x04000C8A RID: 3210
		public const int cg = 76;

		// Token: 0x04000C8B RID: 3211
		public const int ch = 77;

		// Token: 0x04000C8C RID: 3212
		public const int ci = 78;

		// Token: 0x04000C8D RID: 3213
		public const int cj = 79;

		// Token: 0x04000C8E RID: 3214
		public const int ck = 80;

		// Token: 0x04000C8F RID: 3215
		public const int cl = 81;

		// Token: 0x04000C90 RID: 3216
		public const int cm = 82;

		// Token: 0x04000C91 RID: 3217
		public const int cn = 83;

		// Token: 0x04000C92 RID: 3218
		public const int co = 84;

		// Token: 0x04000C93 RID: 3219
		public const int cp = 85;

		// Token: 0x04000C94 RID: 3220
		public const int cq = 86;

		// Token: 0x04000C95 RID: 3221
		public const int cr = 87;

		// Token: 0x04000C96 RID: 3222
		public const int cs = 88;

		// Token: 0x04000C97 RID: 3223
		public const int ct = 89;

		// Token: 0x04000C98 RID: 3224
		public const int cu = 90;

		// Token: 0x04000C99 RID: 3225
		public const int cv = 91;

		// Token: 0x04000C9A RID: 3226
		public const int cw = 92;

		// Token: 0x04000C9B RID: 3227
		public const int cx = 93;

		// Token: 0x04000C9C RID: 3228
		public const int cy = 94;

		// Token: 0x04000C9D RID: 3229
		public const int cz = 95;

		// Token: 0x04000C9E RID: 3230
		public const int c0 = 96;

		// Token: 0x04000C9F RID: 3231
		public const int c1 = 97;

		// Token: 0x04000CA0 RID: 3232
		public const int c2 = 98;

		// Token: 0x04000CA1 RID: 3233
		public const int c3 = 99;

		// Token: 0x04000CA2 RID: 3234
		public const int c4 = 100;

		// Token: 0x04000CA3 RID: 3235
		public const int c5 = 101;

		// Token: 0x04000CA4 RID: 3236
		public const int c6 = 102;

		// Token: 0x04000CA5 RID: 3237
		public const int c7 = 103;

		// Token: 0x04000CA6 RID: 3238
		public const int c8 = 104;

		// Token: 0x04000CA7 RID: 3239
		public const int c9 = 105;

		// Token: 0x04000CA8 RID: 3240
		public const int da = 112;

		// Token: 0x04000CA9 RID: 3241
		public const int db = 113;

		// Token: 0x04000CAA RID: 3242
		public const int dc = 114;

		// Token: 0x04000CAB RID: 3243
		public const int dd = 115;

		// Token: 0x04000CAC RID: 3244
		public const int de = 116;

		// Token: 0x04000CAD RID: 3245
		public const int df = 117;

		// Token: 0x04000CAE RID: 3246
		public const int dg = 118;

		// Token: 0x04000CAF RID: 3247
		public const int dh = 119;

		// Token: 0x04000CB0 RID: 3248
		public const int di = 120;

		// Token: 0x04000CB1 RID: 3249
		public const int dj = 121;

		// Token: 0x04000CB2 RID: 3250
		public const int dk = 122;

		// Token: 0x04000CB3 RID: 3251
		public const int dl = 123;

		// Token: 0x04000CB4 RID: 3252
		public const int dm = 124;

		// Token: 0x04000CB5 RID: 3253
		public const int dn = 125;

		// Token: 0x04000CB6 RID: 3254
		public const int @do = 126;

		// Token: 0x04000CB7 RID: 3255
		public const int dp = 127;

		// Token: 0x04000CB8 RID: 3256
		public const int dq = 4096;

		// Token: 0x04000CB9 RID: 3257
		public const int dr = 4097;

		// Token: 0x04000CBA RID: 3258
		public const int ds = 4098;

		// Token: 0x04000CBB RID: 3259
		public const int dt = 4099;

		// Token: 0x04000CBC RID: 3260
		public const int du = 4100;

		// Token: 0x04000CBD RID: 3261
		public const int dv = 4102;

		// Token: 0x04000CBE RID: 3262
		public const int dw = 4103;

		// Token: 0x04000CBF RID: 3263
		public const int dx = 4104;

		// Token: 0x04000CC0 RID: 3264
		public const int dy = 4105;

		// Token: 0x04000CC1 RID: 3265
		public const int dz = 4112;

		// Token: 0x04000CC2 RID: 3266
		public const int d0 = 4113;

		// Token: 0x04000CC3 RID: 3267
		public const int d1 = 4114;

		// Token: 0x04000CC4 RID: 3268
		public const int d2 = 4115;

		// Token: 0x04000CC5 RID: 3269
		public const int d3 = 4115;

		// Token: 0x04000CC6 RID: 3270
		public const int d4 = 3072;

		// Token: 0x04000CC7 RID: 3271
		public const int d5 = 3073;

		// Token: 0x04000CC8 RID: 3272
		public const int d6 = 3074;

		// Token: 0x04000CC9 RID: 3273
		public const int d7 = 3075;

		// Token: 0x04000CCA RID: 3274
		public const int d8 = 3076;

		// Token: 0x04000CCB RID: 3275
		public const int d9 = 3077;

		// Token: 0x04000CCC RID: 3276
		public const int ea = 3078;

		// Token: 0x04000CCD RID: 3277
		public const int eb = 3079;

		// Token: 0x04000CCE RID: 3278
		public const int ec = 3080;

		// Token: 0x04000CCF RID: 3279
		public const int ed = 3081;

		// Token: 0x04000CD0 RID: 3280
		public const int ee = 3082;

		// Token: 0x04000CD1 RID: 3281
		public const int ef = 3083;

		// Token: 0x04000CD2 RID: 3282
		public const int eg = 3084;

		// Token: 0x04000CD3 RID: 3283
		public const int eh = 3085;

		// Token: 0x04000CD4 RID: 3284
		public const int ei = 3086;

		// Token: 0x04000CD5 RID: 3285
		public const int ej = 3087;

		// Token: 0x04000CD6 RID: 3286
		public const int ek = 3088;

		// Token: 0x04000CD7 RID: 3287
		public const int el = 3089;

		// Token: 0x04000CD8 RID: 3288
		public const int em = 3090;

		// Token: 0x04000CD9 RID: 3289
		public const int en = 3091;

		// Token: 0x04000CDA RID: 3290
		public const int eo = 3092;

		// Token: 0x04000CDB RID: 3291
		public const int ep = 3093;

		// Token: 0x04000CDC RID: 3292
		public const int eq = 3094;

		// Token: 0x04000CDD RID: 3293
		public const int er = 3095;

		// Token: 0x04000CDE RID: 3294
		public const int es = 3096;

		// Token: 0x04000CDF RID: 3295
		public const int et = 3097;

		// Token: 0x04000CE0 RID: 3296
		public const int eu = 3098;

		// Token: 0x04000CE1 RID: 3297
		public const int ev = 3099;

		// Token: 0x04000CE2 RID: 3298
		public const int ew = 3100;

		// Token: 0x04000CE3 RID: 3299
		public const int ex = 3101;

		// Token: 0x04000CE4 RID: 3300
		public const int ey = 3102;

		// Token: 0x04000CE5 RID: 3301
		public const int ez = 3103;

		// Token: 0x04000CE6 RID: 3302
		public const int e0 = 3584;

		// Token: 0x04000CE7 RID: 3303
		public const int e1 = 3585;

		// Token: 0x04000CE8 RID: 3304
		public const int e2 = 3586;

		// Token: 0x04000CE9 RID: 3305
		public const int e3 = 3587;

		// Token: 0x04000CEA RID: 3306
		public const int e4 = 3588;

		// Token: 0x04000CEB RID: 3307
		public const int e5 = 3589;

		// Token: 0x04000CEC RID: 3308
		public const int e6 = 3590;

		// Token: 0x04000CED RID: 3309
		public const int e7 = 3591;

		// Token: 0x04000CEE RID: 3310
		public const int e8 = 3592;

		// Token: 0x04000CEF RID: 3311
		public const int e9 = 3593;

		// Token: 0x04000CF0 RID: 3312
		public const int fa = 3594;

		// Token: 0x04000CF1 RID: 3313
		public const int fb = 3596;

		// Token: 0x04000CF2 RID: 3314
		public const int fc = 3597;

		// Token: 0x04000CF3 RID: 3315
		public const int fd = 3598;

		// Token: 0x04000CF4 RID: 3316
		public const int fe = 3599;

		// Token: 0x04000CF5 RID: 3317
		public const int ff = 3600;

		// Token: 0x04000CF6 RID: 3318
		public const int fg = 3601;

		// Token: 0x04000CF7 RID: 3319
		public const int fh = 3602;

		// Token: 0x04000CF8 RID: 3320
		public const int fi = 3603;

		// Token: 0x04000CF9 RID: 3321
		public const int fj = 3604;

		// Token: 0x04000CFA RID: 3322
		public const int fk = 3605;

		// Token: 0x04000CFB RID: 3323
		public const int fl = 3606;

		// Token: 0x04000CFC RID: 3324
		public const int fm = 3607;

		// Token: 0x04000CFD RID: 3325
		public const int fn = 3608;

		// Token: 0x04000CFE RID: 3326
		public const int fo = 3609;

		// Token: 0x04000CFF RID: 3327
		public const int fp = 3610;

		// Token: 0x04000D00 RID: 3328
		public const int fq = 3611;

		// Token: 0x04000D01 RID: 3329
		public const int fr = 3612;

		// Token: 0x04000D02 RID: 3330
		public const int fs = 3613;

		// Token: 0x04000D03 RID: 3331
		public const int ft = 3615;

		// Token: 0x04000D04 RID: 3332
		public const int fu = 3616;

		// Token: 0x04000D05 RID: 3333
		public const int fv = 3617;

		// Token: 0x04000D06 RID: 3334
		public const int fw = 3618;

		// Token: 0x04000D07 RID: 3335
		public const int fx = 3621;

		// Token: 0x04000D08 RID: 3336
		public const int fy = 3622;

		// Token: 0x04000D09 RID: 3337
		public const int fz = 4095;

		// Token: 0x04000D0A RID: 3338
		public const int f0 = 4094;

		// Token: 0x04000D0B RID: 3339
		public const int f1 = 4093;

		// Token: 0x04000D0C RID: 3340
		public const int f2 = 4092;

		// Token: 0x04000D0D RID: 3341
		public const int f3 = 4091;

		// Token: 0x04000D0E RID: 3342
		public const int f4 = 4090;

		// Token: 0x04000D0F RID: 3343
		public const int f5 = 4089;

		// Token: 0x04000D10 RID: 3344
		public const int f6 = 4088;

		// Token: 0x04000D11 RID: 3345
		public const int f7 = 4087;

		// Token: 0x04000D12 RID: 3346
		public const int f8 = 4086;

		// Token: 0x04000D13 RID: 3347
		public const int f9 = 4085;

		// Token: 0x04000D14 RID: 3348
		public const int ga = 4084;

		// Token: 0x04000D15 RID: 3349
		public const int gb = 12288;

		// Token: 0x04000D16 RID: 3350
		public const int gc = 12289;

		// Token: 0x04000D17 RID: 3351
		public const int gd = 12290;

		// Token: 0x04000D18 RID: 3352
		public const int ge = 12291;

		// Token: 0x04000D19 RID: 3353
		public const int gf = 12292;

		// Token: 0x04000D1A RID: 3354
		public const int gg = 12293;

		// Token: 0x04000D1B RID: 3355
		public const int gh = 12294;

		// Token: 0x04000D1C RID: 3356
		public const int gi = 12295;

		// Token: 0x04000D1D RID: 3357
		public const int gj = 12296;

		// Token: 0x04000D1E RID: 3358
		public const int gk = 12297;

		// Token: 0x04000D1F RID: 3359
		public const int gl = 12298;

		// Token: 0x04000D20 RID: 3360
		public const int gm = 12299;

		// Token: 0x04000D21 RID: 3361
		public const int gn = 12300;

		// Token: 0x04000D22 RID: 3362
		public const int go = 12301;

		// Token: 0x04000D23 RID: 3363
		public const int gp = 13057;

		// Token: 0x04000D24 RID: 3364
		public const int gq = 13058;

		// Token: 0x04000D25 RID: 3365
		public const int gr = 13059;

		// Token: 0x04000D26 RID: 3366
		public const int gs = 13060;

		// Token: 0x04000D27 RID: 3367
		public const int gt = 13061;

		// Token: 0x04000D28 RID: 3368
		public const int gu = 13062;

		// Token: 0x04000D29 RID: 3369
		public const int gv = 13063;

		// Token: 0x04000D2A RID: 3370
		public const int gw = 13064;

		// Token: 0x04000D2B RID: 3371
		public const int gx = 13065;

		// Token: 0x04000D2C RID: 3372
		public const int gy = 13066;

		// Token: 0x04000D2D RID: 3373
		public const int gz = 13312;

		// Token: 0x04000D2E RID: 3374
		public const int g0 = 13325;

		// Token: 0x04000D2F RID: 3375
		public const int g1 = 13326;

		// Token: 0x04000D30 RID: 3376
		public const int g2 = 13328;

		// Token: 0x04000D31 RID: 3377
		public const int g3 = 13329;

		// Token: 0x04000D32 RID: 3378
		public const int g4 = 13330;

		// Token: 0x04000D33 RID: 3379
		public const int g5 = 13331;

		// Token: 0x04000D34 RID: 3380
		public const int g6 = 13332;

		// Token: 0x04000D35 RID: 3381
		public const int g7 = 13333;

		// Token: 0x04000D36 RID: 3382
		public const int g8 = 13791;

		// Token: 0x04000D37 RID: 3383
		public const int g9 = 13792;

		// Token: 0x04000D38 RID: 3384
		public const int ha = 13794;

		// Token: 0x04000D39 RID: 3385
		public const int hb = 13795;

		// Token: 0x04000D3A RID: 3386
		public const int hc = 13796;

		// Token: 0x04000D3B RID: 3387
		public const int hd = 13797;

		// Token: 0x04000D3C RID: 3388
		public const int he = 13798;

		// Token: 0x04000D3D RID: 3389
		public const int hf = 13799;

		// Token: 0x04000D3E RID: 3390
		public const int hg = 13824;

		// Token: 0x04000D3F RID: 3391
		public const int hh = 13825;

		// Token: 0x04000D40 RID: 3392
		public const int hi = 13826;

		// Token: 0x04000D41 RID: 3393
		public const int hj = 13827;

		// Token: 0x04000D42 RID: 3394
		public const int hk = 13828;

		// Token: 0x04000D43 RID: 3395
		public const int hl = 13829;

		// Token: 0x04000D44 RID: 3396
		public const int hm = 13831;

		// Token: 0x04000D45 RID: 3397
		public const int hn = 13833;

		// Token: 0x04000D46 RID: 3398
		public const int ho = 13834;

		// Token: 0x04000D47 RID: 3399
		public const int hp = 13835;

		// Token: 0x04000D48 RID: 3400
		public const int hq = 13836;

		// Token: 0x04000D49 RID: 3401
		public const int hr = 13837;

		// Token: 0x04000D4A RID: 3402
		public const int hs = 13838;

		// Token: 0x04000D4B RID: 3403
		public const int ht = 13839;

		// Token: 0x04000D4C RID: 3404
		public const int hu = 13840;

		// Token: 0x04000D4D RID: 3405
		public const int hv = 13841;

		// Token: 0x04000D4E RID: 3406
		public const int hw = 13842;

		// Token: 0x04000D4F RID: 3407
		public const int hx = 13843;

		// Token: 0x04000D50 RID: 3408
		public const int hy = 13844;

		// Token: 0x04000D51 RID: 3409
		public const int hz = 13845;

		// Token: 0x04000D52 RID: 3410
		public const int h0 = 13846;

		// Token: 0x04000D53 RID: 3411
		public const int h1 = 13847;

		// Token: 0x04000D54 RID: 3412
		public const int h2 = 14080;

		// Token: 0x04000D55 RID: 3413
		public const int h3 = 14081;

		// Token: 0x04000D56 RID: 3414
		public const int h4 = 14081;

		// Token: 0x04000D57 RID: 3415
		public const int h5 = 14082;

		// Token: 0x04000D58 RID: 3416
		public const int h6 = 14083;

		// Token: 0x04000D59 RID: 3417
		public const int h7 = 14084;

		// Token: 0x04000D5A RID: 3418
		public const int h8 = 14085;

		// Token: 0x04000D5B RID: 3419
		public const int h9 = 14087;

		// Token: 0x04000D5C RID: 3420
		public const int ia = 14088;

		// Token: 0x04000D5D RID: 3421
		public const int ib = 14089;

		// Token: 0x04000D5E RID: 3422
		public const int ic = 14090;

		// Token: 0x04000D5F RID: 3423
		public const int id = 14091;

		// Token: 0x04000D60 RID: 3424
		public const int ie = 14092;

		// Token: 0x04000D61 RID: 3425
		public const int @if = 14093;

		// Token: 0x04000D62 RID: 3426
		public const int ig = 14094;

		// Token: 0x04000D63 RID: 3427
		public const int ih = 14095;

		// Token: 0x04000D64 RID: 3428
		public const int ii = 14098;

		// Token: 0x04000D65 RID: 3429
		public const int ij = 14592;

		// Token: 0x04000D66 RID: 3430
		public const int ik = 14594;

		// Token: 0x04000D67 RID: 3431
		public const int il = 14596;

		// Token: 0x04000D68 RID: 3432
		public const int im = 14847;

		// Token: 0x04000D69 RID: 3433
		public const int @in = 14848;

		// Token: 0x04000D6A RID: 3434
		public const int io = 14849;

		// Token: 0x04000D6B RID: 3435
		public const int ip = 14850;

		// Token: 0x04000D6C RID: 3436
		public const int iq = 14851;

		// Token: 0x04000D6D RID: 3437
		public const int ir = 14852;

		// Token: 0x04000D6E RID: 3438
		public const int @is = 14853;

		// Token: 0x04000D6F RID: 3439
		public const int it = 14854;

		// Token: 0x04000D70 RID: 3440
		public const int iu = 14855;

		// Token: 0x04000D71 RID: 3441
		public const int iv = 14856;

		// Token: 0x04000D72 RID: 3442
		public const int iw = 14857;

		// Token: 0x04000D73 RID: 3443
		public const int ix = 14858;

		// Token: 0x04000D74 RID: 3444
		public const int iy = 14859;

		// Token: 0x04000D75 RID: 3445
		public const int iz = 14860;

		// Token: 0x04000D76 RID: 3446
		public const int i0 = 14861;

		// Token: 0x04000D77 RID: 3447
		public const int i1 = 14862;

		// Token: 0x04000D78 RID: 3448
		public const int i2 = 14863;

		// Token: 0x04000D79 RID: 3449
		public const int i3 = 14864;

		// Token: 0x04000D7A RID: 3450
		public const int i4 = 14865;

		// Token: 0x04000D7B RID: 3451
		public const int i5 = 14866;

		// Token: 0x04000D7C RID: 3452
		public const int i6 = 14867;

		// Token: 0x04000D7D RID: 3453
		public const int i7 = 14868;

		// Token: 0x04000D7E RID: 3454
		public const int i8 = 14869;

		// Token: 0x04000D7F RID: 3455
		public const int i9 = 14870;

		// Token: 0x04000D80 RID: 3456
		public const int ja = 14871;

		// Token: 0x04000D81 RID: 3457
		public const int jb = 14872;

		// Token: 0x04000D82 RID: 3458
		public const int jc = 14873;

		// Token: 0x04000D83 RID: 3459
		public const int jd = 14874;

		// Token: 0x04000D84 RID: 3460
		public const int je = 14875;

		// Token: 0x04000D85 RID: 3461
		public const int jf = 14876;

		// Token: 0x04000D86 RID: 3462
		public const int jg = 14877;

		// Token: 0x04000D87 RID: 3463
		public const int jh = 14878;

		// Token: 0x04000D88 RID: 3464
		public const int ji = 14879;

		// Token: 0x04000D89 RID: 3465
		public const int jj = 14880;

		// Token: 0x04000D8A RID: 3466
		public const int jk = 14881;

		// Token: 0x04000D8B RID: 3467
		public const int jl = 14882;

		// Token: 0x04000D8C RID: 3468
		public const int jm = 14883;

		// Token: 0x04000D8D RID: 3469
		public const int jn = 14884;

		// Token: 0x04000D8E RID: 3470
		public const int jo = 14885;

		// Token: 0x04000D8F RID: 3471
		public const int jp = 14886;

		// Token: 0x04000D90 RID: 3472
		public const int jq = 14887;

		// Token: 0x04000D91 RID: 3473
		public const int jr = 14888;

		// Token: 0x04000D92 RID: 3474
		public const int js = 14889;

		// Token: 0x04000D93 RID: 3475
		public const int jt = 14890;

		// Token: 0x04000D94 RID: 3476
		public const int ju = 14891;

		// Token: 0x04000D95 RID: 3477
		public const int jv = 14892;

		// Token: 0x04000D96 RID: 3478
		public const int jw = 14893;

		// Token: 0x04000D97 RID: 3479
		public const int jx = 14894;

		// Token: 0x04000D98 RID: 3480
		public const int jy = 14895;

		// Token: 0x04000D99 RID: 3481
		public const int jz = 14896;

		// Token: 0x04000D9A RID: 3482
		public const int j0 = 14912;

		// Token: 0x04000D9B RID: 3483
		public const int j1 = 14913;

		// Token: 0x04000D9C RID: 3484
		public const int j2 = 14914;

		// Token: 0x04000D9D RID: 3485
		public const int j3 = 14915;

		// Token: 0x04000D9E RID: 3486
		public const int j4 = 14916;

		// Token: 0x04000D9F RID: 3487
		public const int j5 = 14917;

		// Token: 0x04000DA0 RID: 3488
		public const int j6 = 14918;

		// Token: 0x04000DA1 RID: 3489
		public const int j7 = 14919;

		// Token: 0x04000DA2 RID: 3490
		public const int j8 = 14920;

		// Token: 0x04000DA3 RID: 3491
		public const int j9 = 14921;

		// Token: 0x04000DA4 RID: 3492
		public const int ka = 14922;

		// Token: 0x04000DA5 RID: 3493
		public const int kb = 14923;

		// Token: 0x04000DA6 RID: 3494
		public const int kc = 14924;

		// Token: 0x04000DA7 RID: 3495
		public const int kd = 14925;

		// Token: 0x04000DA8 RID: 3496
		public const int ke = 14926;

		// Token: 0x04000DA9 RID: 3497
		public const int kf = 14927;

		// Token: 0x04000DAA RID: 3498
		public const int kg = 14928;

		// Token: 0x04000DAB RID: 3499
		public const int kh = 14929;

		// Token: 0x04000DAC RID: 3500
		public const int ki = 14930;

		// Token: 0x04000DAD RID: 3501
		public const int kj = 14931;

		// Token: 0x04000DAE RID: 3502
		public const int kk = 14932;

		// Token: 0x04000DAF RID: 3503
		public const int kl = 14933;

		// Token: 0x04000DB0 RID: 3504
		public const int km = 14934;

		// Token: 0x04000DB1 RID: 3505
		public const int kn = 14935;

		// Token: 0x04000DB2 RID: 3506
		public const int ko = 14936;

		// Token: 0x04000DB3 RID: 3507
		public const int kp = 14937;

		// Token: 0x04000DB4 RID: 3508
		public const int kq = 14938;

		// Token: 0x04000DB5 RID: 3509
		public const int kr = 14939;

		// Token: 0x04000DB6 RID: 3510
		public const int ks = 14940;

		// Token: 0x04000DB7 RID: 3511
		public const int kt = 14941;

		// Token: 0x04000DB8 RID: 3512
		public const int ku = 14942;

		// Token: 0x04000DB9 RID: 3513
		public const int kv = 14943;

		// Token: 0x04000DBA RID: 3514
		public const int kw = 14944;

		// Token: 0x04000DBB RID: 3515
		public const int kx = 14945;

		// Token: 0x04000DBC RID: 3516
		public const int ky = 14946;

		// Token: 0x04000DBD RID: 3517
		public const int kz = 14947;

		// Token: 0x04000DBE RID: 3518
		public const int k0 = 14948;

		// Token: 0x04000DBF RID: 3519
		public const int k1 = 15616;

		// Token: 0x04000DC0 RID: 3520
		public const int k2 = 15617;

		// Token: 0x04000DC1 RID: 3521
		public const int k3 = 15618;

		// Token: 0x04000DC2 RID: 3522
		public const int k4 = 15620;

		// Token: 0x04000DC3 RID: 3523
		public const int k5 = 15621;

		// Token: 0x04000DC4 RID: 3524
		public const int k6 = 15622;

		// Token: 0x04000DC5 RID: 3525
		public const int k7 = 15623;

		// Token: 0x04000DC6 RID: 3526
		public const int k8 = 15624;

		// Token: 0x04000DC7 RID: 3527
		public const int k9 = 15625;

		// Token: 0x04000DC8 RID: 3528
		public const int la = 15626;

		// Token: 0x04000DC9 RID: 3529
		public const int lb = 15627;

		// Token: 0x04000DCA RID: 3530
		public const int lc = 15628;

		// Token: 0x04000DCB RID: 3531
		public const int ld = 15629;

		// Token: 0x04000DCC RID: 3532
		public const int le = 15630;

		// Token: 0x04000DCD RID: 3533
		public const int lf = 15631;

		// Token: 0x04000DCE RID: 3534
		public const int lg = 15632;

		// Token: 0x04000DCF RID: 3535
		public const int lh = 15633;

		// Token: 0x04000DD0 RID: 3536
		public const int li = 15634;

		// Token: 0x04000DD1 RID: 3537
		public const int lj = 15872;

		// Token: 0x04000DD2 RID: 3538
		public const int lk = 15873;

		// Token: 0x04000DD3 RID: 3539
		public const int ll = 15874;

		// Token: 0x04000DD4 RID: 3540
		public const int lm = 15875;

		// Token: 0x04000DD5 RID: 3541
		public const int ln = 15876;

		// Token: 0x04000DD6 RID: 3542
		public const int lo = 15877;

		// Token: 0x04000DD7 RID: 3543
		public const int lp = 15878;

		// Token: 0x04000DD8 RID: 3544
		public const int lq = 15879;

		// Token: 0x04000DD9 RID: 3545
		public const int lr = 15880;

		// Token: 0x04000DDA RID: 3546
		public const int ls = 15881;

		// Token: 0x04000DDB RID: 3547
		public const int lt = 15882;

		// Token: 0x04000DDC RID: 3548
		public const int lu = 15883;

		// Token: 0x04000DDD RID: 3549
		public const int lv = 15884;

		// Token: 0x04000DDE RID: 3550
		public const int lw = 15885;

		// Token: 0x04000DDF RID: 3551
		public const int lx = 16128;

		// Token: 0x04000DE0 RID: 3552
		public const int ly = 16129;

		// Token: 0x04000DE1 RID: 3553
		public const int lz = 16130;

		// Token: 0x04000DE2 RID: 3554
		public const int l0 = 16131;

		// Token: 0x04000DE3 RID: 3555
		public const int l1 = 16132;

		// Token: 0x04000DE4 RID: 3556
		public const int l2 = 16133;

		// Token: 0x04000DE5 RID: 3557
		public const int l3 = 16134;

		// Token: 0x04000DE6 RID: 3558
		public const int l4 = 16135;

		// Token: 0x04000DE7 RID: 3559
		public const int l5 = 16136;

		// Token: 0x04000DE8 RID: 3560
		public const int l6 = 26608;

		// Token: 0x04000DE9 RID: 3561
		public const int l7 = 26623;

		// Token: 0x04000DEA RID: 3562
		public const int l8 = 14960;

		// Token: 0x04000DEB RID: 3563
		public const int l9 = 14856;

		// Token: 0x04000DEC RID: 3564
		public const int ma = 14875;

		// Token: 0x04000DED RID: 3565
		public const int mb = 14876;

		// Token: 0x04000DEE RID: 3566
		public const int mc = 14881;

		// Token: 0x04000DEF RID: 3567
		public const int md = 14886;

		// Token: 0x04000DF0 RID: 3568
		public const int me = 14887;

		// Token: 0x04000DF1 RID: 3569
		public const int mf = 14888;

		// Token: 0x04000DF2 RID: 3570
		public const int mg = 14889;

		// Token: 0x04000DF3 RID: 3571
		public const int mh = 14890;

		// Token: 0x04000DF4 RID: 3572
		public const int mi = 14891;

		// Token: 0x04000DF5 RID: 3573
		public const int mj = 71;

		// Token: 0x04000DF6 RID: 3574
		public const int mk = 71;

		// Token: 0x04000DF7 RID: 3575
		public const int ml = 3592;

		// Token: 0x04000DF8 RID: 3576
		public const int mm = 4095;

		// Token: 0x04000DF9 RID: 3577
		public const int mn = 4337;

		// Token: 0x04000DFA RID: 3578
		public const int mo = 4338;

		// Token: 0x04000DFB RID: 3579
		public const int mp = 4352;

		// Token: 0x04000DFC RID: 3580
		public const int mq = 4353;

		// Token: 0x04000DFD RID: 3581
		public const int mr = 14847;

		// Token: 0x04000DFE RID: 3582
		public const int ms = 16344;

		// Token: 0x04000DFF RID: 3583
		public const int mt = 16345;

		// Token: 0x04000E00 RID: 3584
		public const int mu = 16346;

		// Token: 0x04000E01 RID: 3585
		public const int mv = 16347;

		// Token: 0x04000E02 RID: 3586
		public const int mw = 16348;

		// Token: 0x04000E03 RID: 3587
		public const int mx = 16349;

		// Token: 0x04000E04 RID: 3588
		public const int my = 16350;

		// Token: 0x04000E05 RID: 3589
		public const int mz = 16351;

		// Token: 0x04000E06 RID: 3590
		public const int m0 = 16352;

		// Token: 0x04000E07 RID: 3591
		public const int m1 = 16352;

		// Token: 0x04000E08 RID: 3592
		public const int m2 = 16353;

		// Token: 0x04000E09 RID: 3593
		public const int m3 = 16353;

		// Token: 0x04000E0A RID: 3594
		public const int m4 = 16354;

		// Token: 0x04000E0B RID: 3595
		public const int m5 = 16355;

		// Token: 0x04000E0C RID: 3596
		public const int m6 = 16356;

		// Token: 0x04000E0D RID: 3597
		public const int m7 = 16357;

		// Token: 0x04000E0E RID: 3598
		public const int m8 = 16358;

		// Token: 0x04000E0F RID: 3599
		public const int m9 = 16359;

		// Token: 0x04000E10 RID: 3600
		public const int na = 16360;

		// Token: 0x04000E11 RID: 3601
		public const int nb = 16361;

		// Token: 0x04000E12 RID: 3602
		public const int nc = 16362;

		// Token: 0x04000E13 RID: 3603
		public const int nd = 16363;

		// Token: 0x04000E14 RID: 3604
		public const int ne = 16364;

		// Token: 0x04000E15 RID: 3605
		public const int nf = 16365;

		// Token: 0x04000E16 RID: 3606
		public const int ng = 16366;

		// Token: 0x04000E17 RID: 3607
		public const int nh = 16367;

		// Token: 0x04000E18 RID: 3608
		public const int ni = 16368;

		// Token: 0x04000E19 RID: 3609
		public const int nj = 16369;

		// Token: 0x04000E1A RID: 3610
		public const int nk = 16370;

		// Token: 0x04000E1B RID: 3611
		public const int nl = 16371;

		// Token: 0x04000E1C RID: 3612
		public const int nm = 16372;

		// Token: 0x04000E1D RID: 3613
		public const int nn = 16373;

		// Token: 0x04000E1E RID: 3614
		public const int no = 16374;

		// Token: 0x04000E1F RID: 3615
		public const int np = 16375;

		// Token: 0x04000E20 RID: 3616
		public const int nq = 16376;

		// Token: 0x04000E21 RID: 3617
		public const int nr = 16377;

		// Token: 0x04000E22 RID: 3618
		public const int ns = 16378;

		// Token: 0x04000E23 RID: 3619
		public const int nt = 16379;

		// Token: 0x04000E24 RID: 3620
		public const int nu = 16380;

		// Token: 0x04000E25 RID: 3621
		public const int nv = 16381;

		// Token: 0x04000E26 RID: 3622
		public const int nw = 16382;

		// Token: 0x04000E27 RID: 3623
		public static readonly Guid nx = new Guid("02200600-0000-0000-c000-000000000046");

		// Token: 0x04000E28 RID: 3624
		public static readonly Guid ny = new Guid("03200600-0000-0000-c000-000000000046");

		// Token: 0x04000E29 RID: 3625
		public static readonly Guid nz = new Guid("04200600-0000-0000-c000-000000000046");

		// Token: 0x04000E2A RID: 3626
		public static readonly Guid n0 = new Guid("08200600-0000-0000-c000-000000000046");

		// Token: 0x04000E2B RID: 3627
		public static readonly Guid n1 = new Guid("29030200-0000-0000-c000-000000000046");

		// Token: 0x04000E2C RID: 3628
		public static readonly Guid n2 = new Guid("0e200600-0000-0000-c000-000000000046");

		// Token: 0x04000E2D RID: 3629
		public static readonly Guid n3 = new Guid("0a200600-0000-0000-c000-000000000046");
	}
}
