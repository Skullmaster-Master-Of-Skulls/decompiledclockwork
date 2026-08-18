using System;

namespace System.Data.Spatial
{
	// Token: 0x020002DE RID: 734
	public abstract class DbSpatialDataReader
	{
		// Token: 0x06002C43 RID: 11331
		public abstract DbGeography GetGeography(int ordinal);

		// Token: 0x06002C44 RID: 11332
		public abstract DbGeometry GetGeometry(int ordinal);
	}
}
