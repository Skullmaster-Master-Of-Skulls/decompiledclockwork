using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008ED RID: 2285
	internal static class SR
	{
		// Token: 0x04002AB8 RID: 10936
		internal const string Argument_InvalidValue = "Value was invalid.";

		// Token: 0x04002AB9 RID: 10937
		internal const string Argument_SourceOverlapsDestination = "The destination buffer overlaps the source buffer.";

		// Token: 0x04002ABA RID: 10938
		internal const string Argument_UniversalValueIsFixed = "Tags with TagClass Universal must have the appropriate TagValue value for the data type being read or written.";

		// Token: 0x04002ABB RID: 10939
		internal const string BCryptAlgorithmHandle_ProviderNotFound = "A provider could not be found for algorithm '{0}'.";

		// Token: 0x04002ABC RID: 10940
		internal const string BCryptDeriveKeyPBKDF2_Failed = "A call to BCryptDeriveKeyPBKDF2 failed with code '{0}'.";

		// Token: 0x04002ABD RID: 10941
		internal const string ContentException_CerRequiresIndefiniteLength = "A constructed tag used a definite length encoding, which is invalid for CER data. The input may be encoded with BER or DER.";

		// Token: 0x04002ABE RID: 10942
		internal const string ContentException_ConstructedEncodingRequired = "The encoded value uses a primitive encoding, which is invalid for '{0}' values.";

		// Token: 0x04002ABF RID: 10943
		internal const string ContentException_DefaultMessage = "The ASN.1 value is invalid.";

		// Token: 0x04002AC0 RID: 10944
		internal const string ContentException_InvalidTag = "The provided data does not represent a valid tag.";

		// Token: 0x04002AC1 RID: 10945
		internal const string ContentException_InvalidUnderCerOrDer_TryBer = "The encoded value is not valid under the selected encoding, but it may be valid under the BER encoding.";

		// Token: 0x04002AC2 RID: 10946
		internal const string ContentException_InvalidUnderCer_TryBerOrDer = "The encoded value is not valid under the selected encoding, but it may be valid under the BER or DER encoding.";

		// Token: 0x04002AC3 RID: 10947
		internal const string ContentException_InvalidUnderDer_TryBerOrCer = "The encoded value is not valid under the selected encoding, but it may be valid under the BER or CER encoding.";

		// Token: 0x04002AC4 RID: 10948
		internal const string ContentException_LengthExceedsPayload = "The encoded length exceeds the number of bytes remaining in the input buffer.";

		// Token: 0x04002AC5 RID: 10949
		internal const string ContentException_LengthRuleSetConstraint = "The encoded length is not valid under the requested encoding rules, the value may be valid under the BER encoding.";

		// Token: 0x04002AC6 RID: 10950
		internal const string ContentException_LengthTooBig = "The encoded length exceeds the maximum supported by this library (Int32.MaxValue).";

		// Token: 0x04002AC7 RID: 10951
		internal const string ContentException_PrimitiveEncodingRequired = "The encoded value uses a constructed encoding, which is invalid for '{0}' values.";

		// Token: 0x04002AC8 RID: 10952
		internal const string ContentException_SetOfNotSorted = "The encoded set is not sorted as required by the current encoding rules. The value may be valid under the BER encoding, or you can ignore the sort validation by specifying skipSortValidation=true.";

		// Token: 0x04002AC9 RID: 10953
		internal const string ContentException_TooMuchData = "The last expected value has been read, but the reader still has pending data. This value may be from a newer schema, or is corrupt.";

		// Token: 0x04002ACA RID: 10954
		internal const string ContentException_WrongTag = "The provided data is tagged with '{0}' class value '{1}', but it should have been '{2}' class value '{3}'.";

		// Token: 0x04002ACB RID: 10955
		internal const string Cryptography_AlgKdfRequiresChars = "The KDF requires a char-based password input.";

		// Token: 0x04002ACC RID: 10956
		internal const string Cryptography_Der_Invalid_Encoding = "ASN1 corrupted data.";

		// Token: 0x04002ACD RID: 10957
		internal const string Cryptography_UnknownAlgorithmIdentifier = "The algorithm is unknown, not valid for the requested usage, or was not handled.";

		// Token: 0x04002ACE RID: 10958
		internal const string Cryptography_UnknownHashAlgorithm = "'{0}' is not a known hash algorithm.";
	}
}
