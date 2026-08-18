using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000501 RID: 1281
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public sealed class PrimaryInteropAssemblyAttribute : Attribute
	{
		// Token: 0x06003197 RID: 12695 RVA: 0x000A98A4 File Offset: 0x000A88A4
		public PrimaryInteropAssemblyAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x000A98BA File Offset: 0x000A88BA
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x000A98C2 File Offset: 0x000A88C2
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x040019A3 RID: 6563
		internal int _major;

		// Token: 0x040019A4 RID: 6564
		internal int _minor;
	}
}
