using System;

namespace System.Web.Mvc
{
	// Token: 0x020000A1 RID: 161
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class AllowHtmlAttribute : Attribute, IMetadataAware
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000D09A File Offset: 0x0000B29A
		public void OnMetadataCreated(ModelMetadata metadata)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			metadata.RequestValidationEnabled = false;
		}
	}
}
