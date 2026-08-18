using System;
using System.Runtime.Serialization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200015C RID: 348
	[DataContract]
	public class SamlNameIdentifierClaimResource
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x000303B1 File Offset: 0x0002E5B1
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (string.IsNullOrEmpty(this.name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("name");
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000303D0 File Offset: 0x0002E5D0
		public SamlNameIdentifierClaimResource(string name, string nameQualifier, string format)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("name");
			}
			this.name = name;
			this.nameQualifier = nameQualifier;
			this.format = format;
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00030405 File Offset: 0x0002E605
		public string NameQualifier
		{
			get
			{
				return this.nameQualifier;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0003040D File Offset: 0x0002E60D
		public string Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00030415 File Offset: 0x0002E615
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00030420 File Offset: 0x0002E620
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			SamlNameIdentifierClaimResource samlNameIdentifierClaimResource = obj as SamlNameIdentifierClaimResource;
			return samlNameIdentifierClaimResource != null && (this.nameQualifier == samlNameIdentifierClaimResource.nameQualifier && this.format == samlNameIdentifierClaimResource.format) && this.name == samlNameIdentifierClaimResource.name;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0003047D File Offset: 0x0002E67D
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x04000BCD RID: 3021
		[DataMember]
		private string nameQualifier;

		// Token: 0x04000BCE RID: 3022
		[DataMember]
		private string format;

		// Token: 0x04000BCF RID: 3023
		[DataMember]
		private string name;
	}
}
