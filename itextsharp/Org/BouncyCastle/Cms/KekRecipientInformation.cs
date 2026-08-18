using System;
using System.IO;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020003DB RID: 987
	public class KekRecipientInformation : RecipientInformation
	{
		// Token: 0x06002269 RID: 8809 RVA: 0x000D5F36 File Offset: 0x000D4F36
		[Obsolete]
		public KekRecipientInformation(KekRecipientInfo info, AlgorithmIdentifier encAlg, Stream data) : this(info, encAlg, null, null, data)
		{
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x000D5F43 File Offset: 0x000D4F43
		[Obsolete]
		public KekRecipientInformation(KekRecipientInfo info, AlgorithmIdentifier encAlg, AlgorithmIdentifier macAlg, Stream data) : this(info, encAlg, macAlg, null, data)
		{
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000D5F54 File Offset: 0x000D4F54
		public KekRecipientInformation(KekRecipientInfo info, AlgorithmIdentifier encAlg, AlgorithmIdentifier macAlg, AlgorithmIdentifier authEncAlg, Stream data) : base(encAlg, macAlg, authEncAlg, info.KeyEncryptionAlgorithm, data)
		{
			this.info = info;
			this.rid = new RecipientID();
			KekIdentifier kekID = info.KekID;
			this.rid.KeyIdentifier = kekID.KeyIdentifier.GetOctets();
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x000D5FA4 File Offset: 0x000D4FA4
		public override CmsTypedStream GetContentStream(ICipherParameters key)
		{
			CmsTypedStream contentFromSessionKey;
			try
			{
				byte[] octets = this.info.EncryptedKey.GetOctets();
				IWrapper wrapper = WrapperUtilities.GetWrapper(this.keyEncAlg.ObjectID.Id);
				wrapper.Init(false, key);
				AlgorithmIdentifier activeAlgID = base.GetActiveAlgID();
				KeyParameter sKey = ParameterUtilities.CreateKeyParameter(activeAlgID.ObjectID, wrapper.Unwrap(octets, 0, octets.Length));
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

		// Token: 0x040017A1 RID: 6049
		private KekRecipientInfo info;
	}
}
