using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Spire.License.V1_0;

namespace Spire.License.V1_2
{
	// Token: 0x02000016 RID: 22
	[XmlRoot("License")]
	[Serializable]
	public class LicenseInfo : LicenseInfoAdapter
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00007424 File Offset: 0x00005624
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00007468 File Offset: 0x00005668
		[XmlAttribute]
		public override string Version
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.a;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.a = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000074AC File Offset: 0x000056AC
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000074F0 File Offset: 0x000056F0
		public LicenseType Type
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.b;
			}
			[CompilerGenerated]
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.b = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00007534 File Offset: 0x00005734
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00007578 File Offset: 0x00005778
		public string Username
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.c;
			}
			[CompilerGenerated]
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.c = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000075BC File Offset: 0x000057BC
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00007600 File Offset: 0x00005800
		public string Email
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.d;
			}
			[CompilerGenerated]
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.d = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00007644 File Offset: 0x00005844
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00007688 File Offset: 0x00005888
		public string Organization
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.e;
			}
			[CompilerGenerated]
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.e = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000076CC File Offset: 0x000058CC
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00007710 File Offset: 0x00005910
		public DateTime LicensedDate
		{
			[CompilerGenerated]
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.f;
			}
			[CompilerGenerated]
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.f = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00007754 File Offset: 0x00005954
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00007798 File Offset: 0x00005998
		public DateTime ExpiredDate
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.g;
			}
			[CompilerGenerated]
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.g = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000077DC File Offset: 0x000059DC
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00007820 File Offset: 0x00005A20
		public Product[] Products
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.h;
			}
			[CompilerGenerated]
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.h = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00007864 File Offset: 0x00005A64
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000078A8 File Offset: 0x00005AA8
		public Issuer Issuer
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.i;
			}
			[CompilerGenerated]
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.i = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000078EC File Offset: 0x00005AEC
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00007930 File Offset: 0x00005B30
		[XmlIgnore]
		public bool IsUpdateRightExpired
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.j;
			}
			[CompilerGenerated]
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.j = value;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00007974 File Offset: 0x00005B74
		public override LicenseInfo ConvertToCurrentVersion()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return global::a.a(this);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000079B8 File Offset: 0x00005BB8
		public override BaseLicenseInfo ConvertFromCurrentVersion(LicenseInfo license)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			global::a.a(license, this);
			return this;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000079FC File Offset: 0x00005BFC
		public LicenseInfo()
		{
			int a_ = 6;
			this.a = Product.b("鞥蚧颩", a_);
			base..ctor();
		}

		// Token: 0x04000069 RID: 105
		private int \u2593\u009E\u00A1\u0097;

		// Token: 0x0400006A RID: 106
		private new string a;

		// Token: 0x0400006B RID: 107
		[CompilerGenerated]
		private LicenseType b;

		// Token: 0x0400006C RID: 108
		[CompilerGenerated]
		private string c;

		// Token: 0x0400006D RID: 109
		[CompilerGenerated]
		private string d;

		// Token: 0x0400006E RID: 110
		[CompilerGenerated]
		private string e;

		// Token: 0x0400006F RID: 111
		[CompilerGenerated]
		private DateTime f;

		// Token: 0x04000070 RID: 112
		[CompilerGenerated]
		private DateTime g;

		// Token: 0x04000071 RID: 113
		[CompilerGenerated]
		private Product[] h;

		// Token: 0x04000072 RID: 114
		[CompilerGenerated]
		private Issuer i;

		// Token: 0x04000073 RID: 115
		[CompilerGenerated]
		private bool j;
	}
}
