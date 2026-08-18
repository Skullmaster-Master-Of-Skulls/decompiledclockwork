using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000981 RID: 2433
	internal static class RedirectionConstants
	{
		// Token: 0x040037DD RID: 14301
		public const string AddressElementName = "Address";

		// Token: 0x040037DE RID: 14302
		public const string LocationElementName = "Location";

		// Token: 0x040037DF RID: 14303
		public const string Namespace = "http://schemas.microsoft.com/ws/2008/06/redirect";

		// Token: 0x040037E0 RID: 14304
		public const string Prefix = "r";

		// Token: 0x040037E1 RID: 14305
		public const string RedirectionElementName = "Redirection";

		// Token: 0x02000DF3 RID: 3571
		internal static class Duration
		{
			// Token: 0x0400498A RID: 18826
			public const string Permanent = "Permanent";

			// Token: 0x0400498B RID: 18827
			public const string Temporary = "Temporary";

			// Token: 0x0400498C RID: 18828
			public const string XmlName = "duration";
		}

		// Token: 0x02000DF4 RID: 3572
		internal static class Scope
		{
			// Token: 0x0400498D RID: 18829
			public const string Endpoint = "Endpoint";

			// Token: 0x0400498E RID: 18830
			public const string Message = "Message";

			// Token: 0x0400498F RID: 18831
			public const string Session = "Session";

			// Token: 0x04004990 RID: 18832
			public const string XmlName = "scope";
		}

		// Token: 0x02000DF5 RID: 3573
		internal static class Type
		{
			// Token: 0x04004991 RID: 18833
			public const string Cache = "Cache";

			// Token: 0x04004992 RID: 18834
			public const string Resource = "Resource";

			// Token: 0x04004993 RID: 18835
			public const string UseIntermediary = "UseIntermediary";

			// Token: 0x04004994 RID: 18836
			public const string XmlName = "type";
		}
	}
}
