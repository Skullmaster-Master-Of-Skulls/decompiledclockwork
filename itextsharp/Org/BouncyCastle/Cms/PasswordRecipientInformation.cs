using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002FF RID: 767
	public class PasswordRecipientInformation : RecipientInformation
	{
		// Token: 0x06001C1C RID: 7196 RVA: 0x000A878F File Offset: 0x000A778F
		[Obsolete]
		public PasswordRecipientInformation(PasswordRecipientInfo info, AlgorithmIdentifier encAlg, Stream data) : this(info, encAlg, null, null, data)
		{
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000A879C File Offset: 0x000A779C
		[Obsolete]
		public PasswordRecipientInformation(PasswordRecipientInfo info, AlgorithmIdentifier encAlg, AlgorithmIdentifier macAlg, Stream data) : this(info, encAlg, macAlg, null, data)
		{
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x000A87AA File Offset: 0x000A77AA
		public PasswordRecipientInformation(PasswordRecipientInfo info, AlgorithmIdentifier encAlg, AlgorithmIdentifier macAlg, AlgorithmIdentifier authEncAlg, Stream data) : base(encAlg, macAlg, authEncAlg, info.KeyEncryptionAlgorithm, data)
		{
			this.info = info;
			this.rid = new RecipientID();
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x000A87D0 File Offset: 0x000A77D0
		public virtual AlgorithmIdentifier KeyDerivationAlgorithm
		{
			get
			{
				return this.info.KeyDerivationAlgorithm;
			}
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x000A87E0 File Offset: 0x000A77E0
		public override CmsTypedStream GetContentStream(ICipherParameters key)
		{
			CmsTypedStream contentFromSessionKey;
			try
			{
				AlgorithmIdentifier instance = AlgorithmIdentifier.GetInstance(this.info.KeyEncryptionAlgorithm);
				Asn1Sequence asn1Sequence = (Asn1Sequence)instance.Parameters;
				byte[] octets = this.info.EncryptedKey.GetOctets();
				string id = DerObjectIdentifier.GetInstance(asn1Sequence[0]).Id;
				string rfc3211WrapperName = CmsEnvelopedHelper.Instance.GetRfc3211WrapperName(id);
				IWrapper wrapper = WrapperUtilities.GetWrapper(rfc3211WrapperName);
				byte[] octets2 = Asn1OctetString.GetInstance(asn1Sequence[1]).GetOctets();
				ICipherParameters parameters = ((CmsPbeKey)key).GetEncoded(id);
				parameters = new ParametersWithIV(parameters, octets2);
				wrapper.Init(false, parameters);
				AlgorithmIdentifier activeAlgID = base.GetActiveAlgID();
				string id2 = activeAlgID.ObjectID.Id;
				KeyParameter sKey = ParameterUtilities.CreateKeyParameter(id2, wrapper.Unwrap(octets, 0, octets.Length));
				contentFromSessionKey = base.GetContentFromSessionKey(sKey);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("couldn't create cipher.", e);
			}
			catch (InvalidKeyException e2)
			{
				throw new CmsException("key invalid in message.", e2);
			}
			return contentFromSessionKey;
		}

		// Token: 0x04001353 RID: 4947
		private readonly PasswordRecipientInfo info;
	}
}
