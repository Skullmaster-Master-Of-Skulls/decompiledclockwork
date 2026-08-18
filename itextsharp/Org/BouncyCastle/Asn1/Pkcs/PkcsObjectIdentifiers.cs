using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x02000405 RID: 1029
	public abstract class PkcsObjectIdentifiers
	{
		// Token: 0x040017E4 RID: 6116
		public const string Pkcs1 = "1.2.840.113549.1.1";

		// Token: 0x040017E5 RID: 6117
		public const string Pkcs3 = "1.2.840.113549.1.3";

		// Token: 0x040017E6 RID: 6118
		public const string Pkcs5 = "1.2.840.113549.1.5";

		// Token: 0x040017E7 RID: 6119
		public const string EncryptionAlgorithm = "1.2.840.113549.3";

		// Token: 0x040017E8 RID: 6120
		public const string DigestAlgorithm = "1.2.840.113549.2";

		// Token: 0x040017E9 RID: 6121
		public const string Pkcs7 = "1.2.840.113549.1.7";

		// Token: 0x040017EA RID: 6122
		public const string Pkcs9 = "1.2.840.113549.1.9";

		// Token: 0x040017EB RID: 6123
		public const string CertTypes = "1.2.840.113549.1.9.22";

		// Token: 0x040017EC RID: 6124
		public const string CrlTypes = "1.2.840.113549.1.9.23";

		// Token: 0x040017ED RID: 6125
		public const string IdCT = "1.2.840.113549.1.9.16.1";

		// Token: 0x040017EE RID: 6126
		public const string IdCti = "1.2.840.113549.1.9.16.6";

		// Token: 0x040017EF RID: 6127
		public const string IdAA = "1.2.840.113549.1.9.16.2";

		// Token: 0x040017F0 RID: 6128
		public const string IdSpq = "1.2.840.113549.1.9.16.5";

		// Token: 0x040017F1 RID: 6129
		public const string Pkcs12 = "1.2.840.113549.1.12";

		// Token: 0x040017F2 RID: 6130
		public const string BagTypes = "1.2.840.113549.1.12.10.1";

		// Token: 0x040017F3 RID: 6131
		public const string Pkcs12PbeIds = "1.2.840.113549.1.12.1";

		// Token: 0x040017F4 RID: 6132
		public static readonly DerObjectIdentifier RsaEncryption = new DerObjectIdentifier("1.2.840.113549.1.1.1");

		// Token: 0x040017F5 RID: 6133
		public static readonly DerObjectIdentifier MD2WithRsaEncryption = new DerObjectIdentifier("1.2.840.113549.1.1.2");

		// Token: 0x040017F6 RID: 6134
		public static readonly DerObjectIdentifier MD4WithRsaEncryption = new DerObjectIdentifier("1.2.840.113549.1.1.3");

		// Token: 0x040017F7 RID: 6135
		public static readonly DerObjectIdentifier MD5WithRsaEncryption = new DerObjectIdentifier("1.2.840.113549.1.1.4");

		// Token: 0x040017F8 RID: 6136
		public static readonly DerObjectIdentifier Sha1WithRsaEncryption = new DerObjectIdentifier("1.2.840.113549.1.1.5");

		// Token: 0x040017F9 RID: 6137
		public static readonly DerObjectIdentifier SrsaOaepEncryptionSet = new DerObjectIdentifier("1.2.840.113549.1.1.6");

		// Token: 0x040017FA RID: 6138
		public static readonly DerObjectIdentifier IdRsaesOaep = new DerObjectIdentifier("1.2.840.113549.1.1.7");

		// Token: 0x040017FB RID: 6139
		public static readonly DerObjectIdentifier IdMgf1 = new DerObjectIdentifier("1.2.840.113549.1.1.8");

		// Token: 0x040017FC RID: 6140
		public static readonly DerObjectIdentifier IdPSpecified = new DerObjectIdentifier("1.2.840.113549.1.1.9");

		// Token: 0x040017FD RID: 6141
		public static readonly DerObjectIdentifier IdRsassaPss = new DerObjectIdentifier("1.2.840.113549.1.1.10");

		// Token: 0x040017FE RID: 6142
		public static readonly DerObjectIdentifier Sha256WithRsaEncryption = new DerObjectIdentifier("1.2.840.113549.1.1.11");

		// Token: 0x040017FF RID: 6143
		public static readonly DerObjectIdentifier Sha384WithRsaEncryption = new DerObjectIdentifier("1.2.840.113549.1.1.12");

		// Token: 0x04001800 RID: 6144
		public static readonly DerObjectIdentifier Sha512WithRsaEncryption = new DerObjectIdentifier("1.2.840.113549.1.1.13");

		// Token: 0x04001801 RID: 6145
		public static readonly DerObjectIdentifier Sha224WithRsaEncryption = new DerObjectIdentifier("1.2.840.113549.1.1.14");

		// Token: 0x04001802 RID: 6146
		public static readonly DerObjectIdentifier DhKeyAgreement = new DerObjectIdentifier("1.2.840.113549.1.3.1");

		// Token: 0x04001803 RID: 6147
		public static readonly DerObjectIdentifier PbeWithMD2AndDesCbc = new DerObjectIdentifier("1.2.840.113549.1.5.1");

		// Token: 0x04001804 RID: 6148
		public static readonly DerObjectIdentifier PbeWithMD2AndRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.5.4");

		// Token: 0x04001805 RID: 6149
		public static readonly DerObjectIdentifier PbeWithMD5AndDesCbc = new DerObjectIdentifier("1.2.840.113549.1.5.3");

		// Token: 0x04001806 RID: 6150
		public static readonly DerObjectIdentifier PbeWithMD5AndRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.5.6");

		// Token: 0x04001807 RID: 6151
		public static readonly DerObjectIdentifier PbeWithSha1AndDesCbc = new DerObjectIdentifier("1.2.840.113549.1.5.10");

		// Token: 0x04001808 RID: 6152
		public static readonly DerObjectIdentifier PbeWithSha1AndRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.5.11");

		// Token: 0x04001809 RID: 6153
		public static readonly DerObjectIdentifier IdPbeS2 = new DerObjectIdentifier("1.2.840.113549.1.5.13");

		// Token: 0x0400180A RID: 6154
		public static readonly DerObjectIdentifier IdPbkdf2 = new DerObjectIdentifier("1.2.840.113549.1.5.12");

		// Token: 0x0400180B RID: 6155
		public static readonly DerObjectIdentifier DesEde3Cbc = new DerObjectIdentifier("1.2.840.113549.3.7");

		// Token: 0x0400180C RID: 6156
		public static readonly DerObjectIdentifier RC2Cbc = new DerObjectIdentifier("1.2.840.113549.3.2");

		// Token: 0x0400180D RID: 6157
		public static readonly DerObjectIdentifier MD2 = new DerObjectIdentifier("1.2.840.113549.2.2");

		// Token: 0x0400180E RID: 6158
		public static readonly DerObjectIdentifier MD4 = new DerObjectIdentifier("1.2.840.113549.2.4");

		// Token: 0x0400180F RID: 6159
		public static readonly DerObjectIdentifier MD5 = new DerObjectIdentifier("1.2.840.113549.2.5");

		// Token: 0x04001810 RID: 6160
		public static readonly DerObjectIdentifier IdHmacWithSha1 = new DerObjectIdentifier("1.2.840.113549.2.7");

		// Token: 0x04001811 RID: 6161
		public static readonly DerObjectIdentifier IdHmacWithSha224 = new DerObjectIdentifier("1.2.840.113549.2.8");

		// Token: 0x04001812 RID: 6162
		public static readonly DerObjectIdentifier IdHmacWithSha256 = new DerObjectIdentifier("1.2.840.113549.2.9");

		// Token: 0x04001813 RID: 6163
		public static readonly DerObjectIdentifier IdHmacWithSha384 = new DerObjectIdentifier("1.2.840.113549.2.10");

		// Token: 0x04001814 RID: 6164
		public static readonly DerObjectIdentifier IdHmacWithSha512 = new DerObjectIdentifier("1.2.840.113549.2.11");

		// Token: 0x04001815 RID: 6165
		public static readonly DerObjectIdentifier Data = new DerObjectIdentifier("1.2.840.113549.1.7.1");

		// Token: 0x04001816 RID: 6166
		public static readonly DerObjectIdentifier SignedData = new DerObjectIdentifier("1.2.840.113549.1.7.2");

		// Token: 0x04001817 RID: 6167
		public static readonly DerObjectIdentifier EnvelopedData = new DerObjectIdentifier("1.2.840.113549.1.7.3");

		// Token: 0x04001818 RID: 6168
		public static readonly DerObjectIdentifier SignedAndEnvelopedData = new DerObjectIdentifier("1.2.840.113549.1.7.4");

		// Token: 0x04001819 RID: 6169
		public static readonly DerObjectIdentifier DigestedData = new DerObjectIdentifier("1.2.840.113549.1.7.5");

		// Token: 0x0400181A RID: 6170
		public static readonly DerObjectIdentifier EncryptedData = new DerObjectIdentifier("1.2.840.113549.1.7.6");

		// Token: 0x0400181B RID: 6171
		public static readonly DerObjectIdentifier Pkcs9AtEmailAddress = new DerObjectIdentifier("1.2.840.113549.1.9.1");

		// Token: 0x0400181C RID: 6172
		public static readonly DerObjectIdentifier Pkcs9AtUnstructuredName = new DerObjectIdentifier("1.2.840.113549.1.9.2");

		// Token: 0x0400181D RID: 6173
		public static readonly DerObjectIdentifier Pkcs9AtContentType = new DerObjectIdentifier("1.2.840.113549.1.9.3");

		// Token: 0x0400181E RID: 6174
		public static readonly DerObjectIdentifier Pkcs9AtMessageDigest = new DerObjectIdentifier("1.2.840.113549.1.9.4");

		// Token: 0x0400181F RID: 6175
		public static readonly DerObjectIdentifier Pkcs9AtSigningTime = new DerObjectIdentifier("1.2.840.113549.1.9.5");

		// Token: 0x04001820 RID: 6176
		public static readonly DerObjectIdentifier Pkcs9AtCounterSignature = new DerObjectIdentifier("1.2.840.113549.1.9.6");

		// Token: 0x04001821 RID: 6177
		public static readonly DerObjectIdentifier Pkcs9AtChallengePassword = new DerObjectIdentifier("1.2.840.113549.1.9.7");

		// Token: 0x04001822 RID: 6178
		public static readonly DerObjectIdentifier Pkcs9AtUnstructuredAddress = new DerObjectIdentifier("1.2.840.113549.1.9.8");

		// Token: 0x04001823 RID: 6179
		public static readonly DerObjectIdentifier Pkcs9AtExtendedCertificateAttributes = new DerObjectIdentifier("1.2.840.113549.1.9.9");

		// Token: 0x04001824 RID: 6180
		public static readonly DerObjectIdentifier Pkcs9AtSigningDescription = new DerObjectIdentifier("1.2.840.113549.1.9.13");

		// Token: 0x04001825 RID: 6181
		public static readonly DerObjectIdentifier Pkcs9AtExtensionRequest = new DerObjectIdentifier("1.2.840.113549.1.9.14");

		// Token: 0x04001826 RID: 6182
		public static readonly DerObjectIdentifier Pkcs9AtSmimeCapabilities = new DerObjectIdentifier("1.2.840.113549.1.9.15");

		// Token: 0x04001827 RID: 6183
		public static readonly DerObjectIdentifier Pkcs9AtFriendlyName = new DerObjectIdentifier("1.2.840.113549.1.9.20");

		// Token: 0x04001828 RID: 6184
		public static readonly DerObjectIdentifier Pkcs9AtLocalKeyID = new DerObjectIdentifier("1.2.840.113549.1.9.21");

		// Token: 0x04001829 RID: 6185
		[Obsolete("Use X509Certificate instead")]
		public static readonly DerObjectIdentifier X509CertType = new DerObjectIdentifier("1.2.840.113549.1.9.22.1");

		// Token: 0x0400182A RID: 6186
		public static readonly DerObjectIdentifier X509Certificate = new DerObjectIdentifier("1.2.840.113549.1.9.22.1");

		// Token: 0x0400182B RID: 6187
		public static readonly DerObjectIdentifier SdsiCertificate = new DerObjectIdentifier("1.2.840.113549.1.9.22.2");

		// Token: 0x0400182C RID: 6188
		public static readonly DerObjectIdentifier X509Crl = new DerObjectIdentifier("1.2.840.113549.1.9.23.1");

		// Token: 0x0400182D RID: 6189
		public static readonly DerObjectIdentifier IdAlgPwriKek = new DerObjectIdentifier("1.2.840.113549.1.9.16.3.9");

		// Token: 0x0400182E RID: 6190
		public static readonly DerObjectIdentifier PreferSignedData = new DerObjectIdentifier("1.2.840.113549.1.9.15.1");

		// Token: 0x0400182F RID: 6191
		public static readonly DerObjectIdentifier CannotDecryptAny = new DerObjectIdentifier("1.2.840.113549.1.9.15.2");

		// Token: 0x04001830 RID: 6192
		public static readonly DerObjectIdentifier SmimeCapabilitiesVersions = new DerObjectIdentifier("1.2.840.113549.1.9.15.3");

		// Token: 0x04001831 RID: 6193
		public static readonly DerObjectIdentifier IdAAReceiptRequest = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.1");

		// Token: 0x04001832 RID: 6194
		public static readonly DerObjectIdentifier IdCTAuthData = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.2");

		// Token: 0x04001833 RID: 6195
		public static readonly DerObjectIdentifier IdCTTstInfo = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.4");

		// Token: 0x04001834 RID: 6196
		public static readonly DerObjectIdentifier IdCTCompressedData = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.9");

		// Token: 0x04001835 RID: 6197
		public static readonly DerObjectIdentifier IdCTAuthEnvelopedData = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.23");

		// Token: 0x04001836 RID: 6198
		public static readonly DerObjectIdentifier IdCtiEtsProofOfOrigin = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.1");

		// Token: 0x04001837 RID: 6199
		public static readonly DerObjectIdentifier IdCtiEtsProofOfReceipt = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.2");

		// Token: 0x04001838 RID: 6200
		public static readonly DerObjectIdentifier IdCtiEtsProofOfDelivery = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.3");

		// Token: 0x04001839 RID: 6201
		public static readonly DerObjectIdentifier IdCtiEtsProofOfSender = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.4");

		// Token: 0x0400183A RID: 6202
		public static readonly DerObjectIdentifier IdCtiEtsProofOfApproval = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.5");

		// Token: 0x0400183B RID: 6203
		public static readonly DerObjectIdentifier IdCtiEtsProofOfCreation = new DerObjectIdentifier("1.2.840.113549.1.9.16.6.6");

		// Token: 0x0400183C RID: 6204
		public static readonly DerObjectIdentifier IdAAContentHint = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.4");

		// Token: 0x0400183D RID: 6205
		public static readonly DerObjectIdentifier IdAAMsgSigDigest = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.5");

		// Token: 0x0400183E RID: 6206
		public static readonly DerObjectIdentifier IdAAContentReference = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.10");

		// Token: 0x0400183F RID: 6207
		public static readonly DerObjectIdentifier IdAAEncrypKeyPref = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.11");

		// Token: 0x04001840 RID: 6208
		public static readonly DerObjectIdentifier IdAASigningCertificate = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.12");

		// Token: 0x04001841 RID: 6209
		public static readonly DerObjectIdentifier IdAASigningCertificateV2 = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.47");

		// Token: 0x04001842 RID: 6210
		public static readonly DerObjectIdentifier IdAAContentIdentifier = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.7");

		// Token: 0x04001843 RID: 6211
		public static readonly DerObjectIdentifier IdAASignatureTimeStampToken = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.14");

		// Token: 0x04001844 RID: 6212
		public static readonly DerObjectIdentifier IdAAEtsSigPolicyID = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.15");

		// Token: 0x04001845 RID: 6213
		public static readonly DerObjectIdentifier IdAAEtsCommitmentType = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.16");

		// Token: 0x04001846 RID: 6214
		public static readonly DerObjectIdentifier IdAAEtsSignerLocation = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.17");

		// Token: 0x04001847 RID: 6215
		public static readonly DerObjectIdentifier IdAAEtsSignerAttr = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.18");

		// Token: 0x04001848 RID: 6216
		public static readonly DerObjectIdentifier IdAAEtsOtherSigCert = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.19");

		// Token: 0x04001849 RID: 6217
		public static readonly DerObjectIdentifier IdAAEtsContentTimestamp = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.20");

		// Token: 0x0400184A RID: 6218
		public static readonly DerObjectIdentifier IdAAEtsCertificateRefs = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.21");

		// Token: 0x0400184B RID: 6219
		public static readonly DerObjectIdentifier IdAAEtsRevocationRefs = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.22");

		// Token: 0x0400184C RID: 6220
		public static readonly DerObjectIdentifier IdAAEtsCertValues = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.23");

		// Token: 0x0400184D RID: 6221
		public static readonly DerObjectIdentifier IdAAEtsRevocationValues = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.24");

		// Token: 0x0400184E RID: 6222
		public static readonly DerObjectIdentifier IdAAEtsEscTimeStamp = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.25");

		// Token: 0x0400184F RID: 6223
		public static readonly DerObjectIdentifier IdAAEtsCertCrlTimestamp = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.26");

		// Token: 0x04001850 RID: 6224
		public static readonly DerObjectIdentifier IdAAEtsArchiveTimestamp = new DerObjectIdentifier("1.2.840.113549.1.9.16.2.27");

		// Token: 0x04001851 RID: 6225
		[Obsolete("Use 'IdAAEtsSigPolicyID' instead")]
		public static readonly DerObjectIdentifier IdAASigPolicyID = PkcsObjectIdentifiers.IdAAEtsSigPolicyID;

		// Token: 0x04001852 RID: 6226
		[Obsolete("Use 'IdAAEtsCommitmentType' instead")]
		public static readonly DerObjectIdentifier IdAACommitmentType = PkcsObjectIdentifiers.IdAAEtsCommitmentType;

		// Token: 0x04001853 RID: 6227
		[Obsolete("Use 'IdAAEtsSignerLocation' instead")]
		public static readonly DerObjectIdentifier IdAASignerLocation = PkcsObjectIdentifiers.IdAAEtsSignerLocation;

		// Token: 0x04001854 RID: 6228
		[Obsolete("Use 'IdAAEtsOtherSigCert' instead")]
		public static readonly DerObjectIdentifier IdAAOtherSigCert = PkcsObjectIdentifiers.IdAAEtsOtherSigCert;

		// Token: 0x04001855 RID: 6229
		public static readonly DerObjectIdentifier IdSpqEtsUri = new DerObjectIdentifier("1.2.840.113549.1.9.16.5.1");

		// Token: 0x04001856 RID: 6230
		public static readonly DerObjectIdentifier IdSpqEtsUNotice = new DerObjectIdentifier("1.2.840.113549.1.9.16.5.2");

		// Token: 0x04001857 RID: 6231
		public static readonly DerObjectIdentifier KeyBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.1");

		// Token: 0x04001858 RID: 6232
		public static readonly DerObjectIdentifier Pkcs8ShroudedKeyBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.2");

		// Token: 0x04001859 RID: 6233
		public static readonly DerObjectIdentifier CertBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.3");

		// Token: 0x0400185A RID: 6234
		public static readonly DerObjectIdentifier CrlBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.4");

		// Token: 0x0400185B RID: 6235
		public static readonly DerObjectIdentifier SecretBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.5");

		// Token: 0x0400185C RID: 6236
		public static readonly DerObjectIdentifier SafeContentsBag = new DerObjectIdentifier("1.2.840.113549.1.12.10.1.6");

		// Token: 0x0400185D RID: 6237
		public static readonly DerObjectIdentifier PbeWithShaAnd128BitRC4 = new DerObjectIdentifier("1.2.840.113549.1.12.1.1");

		// Token: 0x0400185E RID: 6238
		public static readonly DerObjectIdentifier PbeWithShaAnd40BitRC4 = new DerObjectIdentifier("1.2.840.113549.1.12.1.2");

		// Token: 0x0400185F RID: 6239
		public static readonly DerObjectIdentifier PbeWithShaAnd3KeyTripleDesCbc = new DerObjectIdentifier("1.2.840.113549.1.12.1.3");

		// Token: 0x04001860 RID: 6240
		public static readonly DerObjectIdentifier PbeWithShaAnd2KeyTripleDesCbc = new DerObjectIdentifier("1.2.840.113549.1.12.1.4");

		// Token: 0x04001861 RID: 6241
		public static readonly DerObjectIdentifier PbeWithShaAnd128BitRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.12.1.5");

		// Token: 0x04001862 RID: 6242
		public static readonly DerObjectIdentifier PbewithShaAnd40BitRC2Cbc = new DerObjectIdentifier("1.2.840.113549.1.12.1.6");

		// Token: 0x04001863 RID: 6243
		public static readonly DerObjectIdentifier IdAlgCms3DesWrap = new DerObjectIdentifier("1.2.840.113549.1.9.16.3.6");

		// Token: 0x04001864 RID: 6244
		public static readonly DerObjectIdentifier IdAlgCmsRC2Wrap = new DerObjectIdentifier("1.2.840.113549.1.9.16.3.7");
	}
}
