using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200065C RID: 1628
	public static class ModelMetadataProviders
	{
		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x06005004 RID: 20484 RVA: 0x00114EEF File Offset: 0x001130EF
		// (set) Token: 0x06005005 RID: 20485 RVA: 0x00114EF6 File Offset: 0x001130F6
		public static ModelMetadataProvider Current
		{
			get
			{
				return ModelMetadataProviders._current;
			}
			set
			{
				ModelMetadataProviders._current = (value ?? new EmptyModelMetadataProvider());
			}
		}

		// Token: 0x04002AB0 RID: 10928
		private static ModelMetadataProvider _current = new DataAnnotationsModelMetadataProvider();
	}
}
