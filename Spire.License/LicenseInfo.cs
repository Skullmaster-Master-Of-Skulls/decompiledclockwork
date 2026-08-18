using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Spire.License.V1_0;

namespace Spire.License
{
	// Token: 0x0200000E RID: 14
	[XmlRoot("License")]
	[Serializable]
	public class LicenseInfo : LicenseInfoAdapter
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000061CC File Offset: 0x000043CC
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00006210 File Offset: 0x00004410
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
				this.a = global::f.a(this.Version);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00006264 File Offset: 0x00004464
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000062A8 File Offset: 0x000044A8
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
				return this.d;
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
				this.d = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000062EC File Offset: 0x000044EC
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00006330 File Offset: 0x00004530
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
				return this.e;
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
				this.e = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00006374 File Offset: 0x00004574
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000063B8 File Offset: 0x000045B8
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000063FC File Offset: 0x000045FC
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00006440 File Offset: 0x00004640
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
				return this.g;
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
				this.g = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00006484 File Offset: 0x00004684
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000064C8 File Offset: 0x000046C8
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
				return this.h;
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
				this.h = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000650C File Offset: 0x0000470C
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00006550 File Offset: 0x00004750
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00006594 File Offset: 0x00004794
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000065D8 File Offset: 0x000047D8
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000661C File Offset: 0x0000481C
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00006660 File Offset: 0x00004860
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
				return this.k;
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
				this.k = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000066A4 File Offset: 0x000048A4
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000066E8 File Offset: 0x000048E8
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
				return this.l;
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
				this.l = value;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000672C File Offset: 0x0000492C
		internal void c()
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
			this.b = new LicenseType?(this.Type);
			this.c = new bool?(this.IsUpdateRightExpired);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000678C File Offset: 0x0000498C
		internal void b()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_94;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_64;
					}
					break;
				case 1:
					if (this.c != null)
					{
						num = 4;
						continue;
					}
					return;
				case 3:
					return;
				case 4:
					this.IsUpdateRightExpired = this.c.Value;
					num = 3;
					continue;
				case 5:
					this.Type = this.b.Value;
					goto IL_94;
				}
				if (this.b != null)
				{
					num = 5;
					continue;
				}
				IL_64:
				num = 1;
				continue;
				IL_94:
				num = 0;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00006860 File Offset: 0x00004A60
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000068A4 File Offset: 0x00004AA4
		[XmlIgnore]
		internal f OriginalVersion
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
				return this.m;
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
				this.m = value;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000068E8 File Offset: 0x00004AE8
		public override LicenseInfo ConvertToCurrentVersion()
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
			return this;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00006924 File Offset: 0x00004B24
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
			return this;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00006960 File Offset: 0x00004B60
		public LicenseInfo()
		{
			int a_ = 13;
			this.a = Product.b("鲬膮芰", a_);
			base..ctor();
		}

		// Token: 0x0400002B RID: 43
		private int \u2460\u00A3\u0084\u00A1;

		// Token: 0x0400002C RID: 44
		public const string NO_LICENSE_MESSAGE = "A valid license file couldn't be found, you can more help from http://www.e-iceblue.com/fqa/license.html.";

		// Token: 0x0400002D RID: 45
		private float \u2593\u00AD\u0087\u008D;

		// Token: 0x0400002E RID: 46
		private new string a;

		// Token: 0x0400002F RID: 47
		private int[] \u2460\u00A4\u0081\u0081;

		// Token: 0x04000030 RID: 48
		private LicenseType? b;

		// Token: 0x04000031 RID: 49
		private long \u25D8\u00A0\u0094\u0085;

		// Token: 0x04000032 RID: 50
		private bool? c;

		// Token: 0x04000033 RID: 51
		private int[] \u2609\u0095\u0080\u00AF;

		// Token: 0x04000034 RID: 52
		[CompilerGenerated]
		private LicenseType d;

		// Token: 0x04000035 RID: 53
		[CompilerGenerated]
		private string e;

		// Token: 0x04000036 RID: 54
		[CompilerGenerated]
		private string f;

		// Token: 0x04000037 RID: 55
		[CompilerGenerated]
		private string g;

		// Token: 0x04000038 RID: 56
		[CompilerGenerated]
		private DateTime h;

		// Token: 0x04000039 RID: 57
		[CompilerGenerated]
		private DateTime i;

		// Token: 0x0400003A RID: 58
		[CompilerGenerated]
		private Product[] j;

		// Token: 0x0400003B RID: 59
		[CompilerGenerated]
		private Issuer k;

		// Token: 0x0400003C RID: 60
		[CompilerGenerated]
		private bool l;

		// Token: 0x0400003D RID: 61
		[CompilerGenerated]
		private f m;
	}
}
