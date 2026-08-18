using System;
using System.Configuration;
using System.Configuration.Internal;

namespace System.Web.Configuration
{
	// Token: 0x020006D4 RID: 1748
	internal class ErrorRuntimeConfig : RuntimeConfig
	{
		// Token: 0x06005417 RID: 21527 RVA: 0x001270A2 File Offset: 0x001252A2
		internal ErrorRuntimeConfig() : base(new ErrorRuntimeConfig.ErrorConfigRecord(), false)
		{
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x001270B0 File Offset: 0x001252B0
		protected override object GetSectionObject(string sectionName)
		{
			throw new ConfigurationErrorsException();
		}

		// Token: 0x02000A40 RID: 2624
		private class ErrorConfigRecord : IInternalConfigRecord
		{
			// Token: 0x06006E87 RID: 28295 RVA: 0x000030B5 File Offset: 0x000012B5
			internal ErrorConfigRecord()
			{
			}

			// Token: 0x17001E3C RID: 7740
			// (get) Token: 0x06006E88 RID: 28296 RVA: 0x001270B0 File Offset: 0x001252B0
			string IInternalConfigRecord.ConfigPath
			{
				get
				{
					throw new ConfigurationErrorsException();
				}
			}

			// Token: 0x17001E3D RID: 7741
			// (get) Token: 0x06006E89 RID: 28297 RVA: 0x001270B0 File Offset: 0x001252B0
			string IInternalConfigRecord.StreamName
			{
				get
				{
					throw new ConfigurationErrorsException();
				}
			}

			// Token: 0x17001E3E RID: 7742
			// (get) Token: 0x06006E8A RID: 28298 RVA: 0x000097B7 File Offset: 0x000079B7
			bool IInternalConfigRecord.HasInitErrors
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006E8B RID: 28299 RVA: 0x001270B0 File Offset: 0x001252B0
			void IInternalConfigRecord.ThrowIfInitErrors()
			{
				throw new ConfigurationErrorsException();
			}

			// Token: 0x06006E8C RID: 28300 RVA: 0x001270B0 File Offset: 0x001252B0
			object IInternalConfigRecord.GetSection(string configKey)
			{
				throw new ConfigurationErrorsException();
			}

			// Token: 0x06006E8D RID: 28301 RVA: 0x001270B0 File Offset: 0x001252B0
			object IInternalConfigRecord.GetLkgSection(string configKey)
			{
				throw new ConfigurationErrorsException();
			}

			// Token: 0x06006E8E RID: 28302 RVA: 0x001270B0 File Offset: 0x001252B0
			void IInternalConfigRecord.RefreshSection(string configKey)
			{
				throw new ConfigurationErrorsException();
			}

			// Token: 0x06006E8F RID: 28303 RVA: 0x001270B0 File Offset: 0x001252B0
			void IInternalConfigRecord.Remove()
			{
				throw new ConfigurationErrorsException();
			}
		}
	}
}
