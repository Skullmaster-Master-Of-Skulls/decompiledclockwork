using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000505 RID: 1285
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComCompatibleVersionAttribute : Attribute
	{
		// Token: 0x060031A2 RID: 12706 RVA: 0x000A992D File Offset: 0x000A892D
		public ComCompatibleVersionAttribute(int major, int minor, int build, int revision)
		{
			this._major = major;
			this._minor = minor;
			this._build = build;
			this._revision = revision;
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x000A9952 File Offset: 0x000A8952
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060031A4 RID: 12708 RVA: 0x000A995A File Offset: 0x000A895A
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060031A5 RID: 12709 RVA: 0x000A9962 File Offset: 0x000A8962
		public int BuildNumber
		{
			get
			{
				return this._build;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060031A6 RID: 12710 RVA: 0x000A996A File Offset: 0x000A896A
		public int RevisionNumber
		{
			get
			{
				return this._revision;
			}
		}

		// Token: 0x040019AA RID: 6570
		internal int _major;

		// Token: 0x040019AB RID: 6571
		internal int _minor;

		// Token: 0x040019AC RID: 6572
		internal int _build;

		// Token: 0x040019AD RID: 6573
		internal int _revision;
	}
}
