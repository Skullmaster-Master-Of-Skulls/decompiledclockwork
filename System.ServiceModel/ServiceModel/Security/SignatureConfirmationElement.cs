using System;
using System.IdentityModel;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000289 RID: 649
	internal class SignatureConfirmationElement : ISignatureValueSecurityElement, ISecurityElement
	{
		// Token: 0x060012EC RID: 4844 RVA: 0x00044294 File Offset: 0x00042494
		public SignatureConfirmationElement(string id, byte[] signatureValue, SecurityVersion version)
		{
			if (id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("id");
			}
			if (signatureValue == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("signatureValue");
			}
			this.id = id;
			this.signatureValue = signatureValue;
			this.version = version;
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x000442E2 File Offset: 0x000424E2
		public bool HasId
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x000442E5 File Offset: 0x000424E5
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000442ED File Offset: 0x000424ED
		public byte[] GetSignatureValue()
		{
			return this.signatureValue;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x000442F5 File Offset: 0x000424F5
		public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			this.version.WriteSignatureConfirmation(writer, this.id, this.signatureValue);
		}

		// Token: 0x04001A08 RID: 6664
		private SecurityVersion version;

		// Token: 0x04001A09 RID: 6665
		private string id;

		// Token: 0x04001A0A RID: 6666
		private byte[] signatureValue;
	}
}
