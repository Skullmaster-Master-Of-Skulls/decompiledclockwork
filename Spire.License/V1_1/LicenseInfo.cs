using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Spire.License.V1_0;

namespace Spire.License.V1_1
{
	// Token: 0x02000004 RID: 4
	[XmlRoot("License")]
	[Serializable]
	public class LicenseInfo : LicenseInfoAdapter
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002908 File Offset: 0x00000B08
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000294C File Offset: 0x00000B4C
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002990 File Offset: 0x00000B90
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000029D4 File Offset: 0x00000BD4
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002A18 File Offset: 0x00000C18
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002A5C File Offset: 0x00000C5C
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002AA0 File Offset: 0x00000CA0
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002AE4 File Offset: 0x00000CE4
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002B28 File Offset: 0x00000D28
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002B6C File Offset: 0x00000D6C
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002BB0 File Offset: 0x00000DB0
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002BF4 File Offset: 0x00000DF4
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002C38 File Offset: 0x00000E38
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002C7C File Offset: 0x00000E7C
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002CC0 File Offset: 0x00000EC0
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002D04 File Offset: 0x00000F04
		public Issuer Issuer
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002D48 File Offset: 0x00000F48
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002D8C File Offset: 0x00000F8C
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

		// Token: 0x0600001F RID: 31 RVA: 0x00002DD0 File Offset: 0x00000FD0
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
			return global::a.a(this);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002E14 File Offset: 0x00001014
		public override BaseLicenseInfo ConvertFromCurrentVersion(LicenseInfo license)
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
			global::a.a(license, this);
			return this;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E58 File Offset: 0x00001058
		public LicenseInfo()
		{
			int a_ = 14;
			this.a = Product.b("龭麯莱", a_);
			base..ctor();
		}

		// Token: 0x04000005 RID: 5
		private string \u2609\u00A2\u0086\u00A5;

		// Token: 0x04000006 RID: 6
		private new string a;

		// Token: 0x04000007 RID: 7
		[CompilerGenerated]
		private LicenseType b;

		// Token: 0x04000008 RID: 8
		[CompilerGenerated]
		private string c;

		// Token: 0x04000009 RID: 9
		[CompilerGenerated]
		private string d;

		// Token: 0x0400000A RID: 10
		[CompilerGenerated]
		private string e;

		// Token: 0x0400000B RID: 11
		[CompilerGenerated]
		private DateTime f;

		// Token: 0x0400000C RID: 12
		[CompilerGenerated]
		private Product[] g;

		// Token: 0x0400000D RID: 13
		[CompilerGenerated]
		private Issuer h;

		// Token: 0x0400000E RID: 14
		[CompilerGenerated]
		private bool i;
	}
}
