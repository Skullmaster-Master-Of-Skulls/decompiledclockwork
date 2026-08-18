using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000504 RID: 1284
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibVersionAttribute : Attribute
	{
		// Token: 0x0600319F RID: 12703 RVA: 0x000A9907 File Offset: 0x000A8907
		public TypeLibVersionAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x000A991D File Offset: 0x000A891D
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x000A9925 File Offset: 0x000A8925
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x040019A8 RID: 6568
		internal int _major;

		// Token: 0x040019A9 RID: 6569
		internal int _minor;
	}
}
