using System;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x020003F0 RID: 1008
	[XmlRoot(ElementName = "Location", Namespace = "http://schemas.xmlsoap.org/ws/2004/09/mex")]
	public class MetadataLocation
	{
		// Token: 0x060025F5 RID: 9717 RVA: 0x000895C1 File Offset: 0x000877C1
		public MetadataLocation()
		{
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x000895C9 File Offset: 0x000877C9
		public MetadataLocation(string location)
		{
			this.Location = location;
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060025F7 RID: 9719 RVA: 0x000895D8 File Offset: 0x000877D8
		// (set) Token: 0x060025F8 RID: 9720 RVA: 0x000895E0 File Offset: 0x000877E0
		[XmlText]
		public string Location
		{
			get
			{
				return this.location;
			}
			set
			{
				Uri uri;
				if (value != null && !Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out uri))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxMetadataReferenceInvalidLocation", new object[]
					{
						value
					}));
				}
				this.location = value;
			}
		}

		// Token: 0x04002170 RID: 8560
		private string location;
	}
}
