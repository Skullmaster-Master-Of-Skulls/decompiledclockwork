using System;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x02000480 RID: 1152
	public struct TextMetrics
	{
		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06004DB6 RID: 19894 RVA: 0x0014190F File Offset: 0x0013FB0F
		// (set) Token: 0x06004DB7 RID: 19895 RVA: 0x00141917 File Offset: 0x0013FB17
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06004DB8 RID: 19896 RVA: 0x00141920 File Offset: 0x0013FB20
		// (set) Token: 0x06004DB9 RID: 19897 RVA: 0x00141928 File Offset: 0x0013FB28
		public int Ascent
		{
			get
			{
				return this.ascent;
			}
			set
			{
				this.ascent = value;
			}
		}

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06004DBA RID: 19898 RVA: 0x00141931 File Offset: 0x0013FB31
		// (set) Token: 0x06004DBB RID: 19899 RVA: 0x00141939 File Offset: 0x0013FB39
		public int Descent
		{
			get
			{
				return this.descent;
			}
			set
			{
				this.descent = value;
			}
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06004DBC RID: 19900 RVA: 0x00141942 File Offset: 0x0013FB42
		// (set) Token: 0x06004DBD RID: 19901 RVA: 0x0014194A File Offset: 0x0013FB4A
		public int InternalLeading
		{
			get
			{
				return this.internalLeading;
			}
			set
			{
				this.internalLeading = value;
			}
		}

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06004DBE RID: 19902 RVA: 0x00141953 File Offset: 0x0013FB53
		// (set) Token: 0x06004DBF RID: 19903 RVA: 0x0014195B File Offset: 0x0013FB5B
		public int ExternalLeading
		{
			get
			{
				return this.externalLeading;
			}
			set
			{
				this.externalLeading = value;
			}
		}

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06004DC0 RID: 19904 RVA: 0x00141964 File Offset: 0x0013FB64
		// (set) Token: 0x06004DC1 RID: 19905 RVA: 0x0014196C File Offset: 0x0013FB6C
		public int AverageCharWidth
		{
			get
			{
				return this.aveCharWidth;
			}
			set
			{
				this.aveCharWidth = value;
			}
		}

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06004DC2 RID: 19906 RVA: 0x00141975 File Offset: 0x0013FB75
		// (set) Token: 0x06004DC3 RID: 19907 RVA: 0x0014197D File Offset: 0x0013FB7D
		public int MaxCharWidth
		{
			get
			{
				return this.maxCharWidth;
			}
			set
			{
				this.maxCharWidth = value;
			}
		}

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06004DC4 RID: 19908 RVA: 0x00141986 File Offset: 0x0013FB86
		// (set) Token: 0x06004DC5 RID: 19909 RVA: 0x0014198E File Offset: 0x0013FB8E
		public int Weight
		{
			get
			{
				return this.weight;
			}
			set
			{
				this.weight = value;
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06004DC6 RID: 19910 RVA: 0x00141997 File Offset: 0x0013FB97
		// (set) Token: 0x06004DC7 RID: 19911 RVA: 0x0014199F File Offset: 0x0013FB9F
		public int Overhang
		{
			get
			{
				return this.overhang;
			}
			set
			{
				this.overhang = value;
			}
		}

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06004DC8 RID: 19912 RVA: 0x001419A8 File Offset: 0x0013FBA8
		// (set) Token: 0x06004DC9 RID: 19913 RVA: 0x001419B0 File Offset: 0x0013FBB0
		public int DigitizedAspectX
		{
			get
			{
				return this.digitizedAspectX;
			}
			set
			{
				this.digitizedAspectX = value;
			}
		}

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06004DCA RID: 19914 RVA: 0x001419B9 File Offset: 0x0013FBB9
		// (set) Token: 0x06004DCB RID: 19915 RVA: 0x001419C1 File Offset: 0x0013FBC1
		public int DigitizedAspectY
		{
			get
			{
				return this.digitizedAspectY;
			}
			set
			{
				this.digitizedAspectY = value;
			}
		}

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06004DCC RID: 19916 RVA: 0x001419CA File Offset: 0x0013FBCA
		// (set) Token: 0x06004DCD RID: 19917 RVA: 0x001419D2 File Offset: 0x0013FBD2
		public char FirstChar
		{
			get
			{
				return this.firstChar;
			}
			set
			{
				this.firstChar = value;
			}
		}

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06004DCE RID: 19918 RVA: 0x001419DB File Offset: 0x0013FBDB
		// (set) Token: 0x06004DCF RID: 19919 RVA: 0x001419E3 File Offset: 0x0013FBE3
		public char LastChar
		{
			get
			{
				return this.lastChar;
			}
			set
			{
				this.lastChar = value;
			}
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06004DD0 RID: 19920 RVA: 0x001419EC File Offset: 0x0013FBEC
		// (set) Token: 0x06004DD1 RID: 19921 RVA: 0x001419F4 File Offset: 0x0013FBF4
		public char DefaultChar
		{
			get
			{
				return this.defaultChar;
			}
			set
			{
				this.defaultChar = value;
			}
		}

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06004DD2 RID: 19922 RVA: 0x001419FD File Offset: 0x0013FBFD
		// (set) Token: 0x06004DD3 RID: 19923 RVA: 0x00141A05 File Offset: 0x0013FC05
		public char BreakChar
		{
			get
			{
				return this.breakChar;
			}
			set
			{
				this.breakChar = value;
			}
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06004DD4 RID: 19924 RVA: 0x00141A0E File Offset: 0x0013FC0E
		// (set) Token: 0x06004DD5 RID: 19925 RVA: 0x00141A16 File Offset: 0x0013FC16
		public bool Italic
		{
			get
			{
				return this.italic;
			}
			set
			{
				this.italic = value;
			}
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06004DD6 RID: 19926 RVA: 0x00141A1F File Offset: 0x0013FC1F
		// (set) Token: 0x06004DD7 RID: 19927 RVA: 0x00141A27 File Offset: 0x0013FC27
		public bool Underlined
		{
			get
			{
				return this.underlined;
			}
			set
			{
				this.underlined = value;
			}
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06004DD8 RID: 19928 RVA: 0x00141A30 File Offset: 0x0013FC30
		// (set) Token: 0x06004DD9 RID: 19929 RVA: 0x00141A38 File Offset: 0x0013FC38
		public bool StruckOut
		{
			get
			{
				return this.struckOut;
			}
			set
			{
				this.struckOut = value;
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06004DDA RID: 19930 RVA: 0x00141A41 File Offset: 0x0013FC41
		// (set) Token: 0x06004DDB RID: 19931 RVA: 0x00141A49 File Offset: 0x0013FC49
		public TextMetricsPitchAndFamilyValues PitchAndFamily
		{
			get
			{
				return this.pitchAndFamily;
			}
			set
			{
				this.pitchAndFamily = value;
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06004DDC RID: 19932 RVA: 0x00141A52 File Offset: 0x0013FC52
		// (set) Token: 0x06004DDD RID: 19933 RVA: 0x00141A5A File Offset: 0x0013FC5A
		public TextMetricsCharacterSet CharSet
		{
			get
			{
				return this.charSet;
			}
			set
			{
				this.charSet = value;
			}
		}

		// Token: 0x0400338B RID: 13195
		private int height;

		// Token: 0x0400338C RID: 13196
		private int ascent;

		// Token: 0x0400338D RID: 13197
		private int descent;

		// Token: 0x0400338E RID: 13198
		private int internalLeading;

		// Token: 0x0400338F RID: 13199
		private int externalLeading;

		// Token: 0x04003390 RID: 13200
		private int aveCharWidth;

		// Token: 0x04003391 RID: 13201
		private int maxCharWidth;

		// Token: 0x04003392 RID: 13202
		private int weight;

		// Token: 0x04003393 RID: 13203
		private int overhang;

		// Token: 0x04003394 RID: 13204
		private int digitizedAspectX;

		// Token: 0x04003395 RID: 13205
		private int digitizedAspectY;

		// Token: 0x04003396 RID: 13206
		private char firstChar;

		// Token: 0x04003397 RID: 13207
		private char lastChar;

		// Token: 0x04003398 RID: 13208
		private char defaultChar;

		// Token: 0x04003399 RID: 13209
		private char breakChar;

		// Token: 0x0400339A RID: 13210
		private bool italic;

		// Token: 0x0400339B RID: 13211
		private bool underlined;

		// Token: 0x0400339C RID: 13212
		private bool struckOut;

		// Token: 0x0400339D RID: 13213
		private TextMetricsPitchAndFamilyValues pitchAndFamily;

		// Token: 0x0400339E RID: 13214
		private TextMetricsCharacterSet charSet;
	}
}
