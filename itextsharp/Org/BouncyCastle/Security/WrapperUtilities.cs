using System;
using System.Collections;
using System.Globalization;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Kisa;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;

namespace Org.BouncyCastle.Security
{
	// Token: 0x0200022F RID: 559
	public sealed class WrapperUtilities
	{
		// Token: 0x060015C3 RID: 5571 RVA: 0x0007DDBE File Offset: 0x0007CDBE
		private WrapperUtilities()
		{
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0007DDC8 File Offset: 0x0007CDC8
		static WrapperUtilities()
		{
			WrapperUtilities.algorithms[NistObjectIdentifiers.IdAes128Wrap.Id] = "AESWRAP";
			WrapperUtilities.algorithms[NistObjectIdentifiers.IdAes192Wrap.Id] = "AESWRAP";
			WrapperUtilities.algorithms[NistObjectIdentifiers.IdAes256Wrap.Id] = "AESWRAP";
			WrapperUtilities.algorithms[NttObjectIdentifiers.IdCamellia128Wrap.Id] = "CAMELLIAWRAP";
			WrapperUtilities.algorithms[NttObjectIdentifiers.IdCamellia192Wrap.Id] = "CAMELLIAWRAP";
			WrapperUtilities.algorithms[NttObjectIdentifiers.IdCamellia256Wrap.Id] = "CAMELLIAWRAP";
			WrapperUtilities.algorithms[PkcsObjectIdentifiers.IdAlgCms3DesWrap.Id] = "DESEDEWRAP";
			WrapperUtilities.algorithms[PkcsObjectIdentifiers.IdAlgCmsRC2Wrap.Id] = "RC2WRAP";
			WrapperUtilities.algorithms[KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap.Id] = "SEEDWRAP";
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0007DEC0 File Offset: 0x0007CEC0
		public static IWrapper GetWrapper(DerObjectIdentifier oid)
		{
			return WrapperUtilities.GetWrapper(oid.Id);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0007DED0 File Offset: 0x0007CED0
		public static IWrapper GetWrapper(string algorithm)
		{
			string text = algorithm.ToUpper(CultureInfo.InvariantCulture);
			string text2 = (string)WrapperUtilities.algorithms[text];
			if (text2 == null)
			{
				text2 = text;
			}
			string key;
			switch (key = text2)
			{
			case "AESWRAP":
				return new AesWrapEngine();
			case "CAMELLIAWRAP":
				return new CamelliaWrapEngine();
			case "DESEDEWRAP":
				return new DesEdeWrapEngine();
			case "RC2WRAP":
				return new RC2WrapEngine();
			case "SEEDWRAP":
				return new SeedWrapEngine();
			case "DESEDERFC3211WRAP":
				return new Rfc3211WrapEngine(new DesEdeEngine());
			case "AESRFC3211WRAP":
				return new Rfc3211WrapEngine(new AesFastEngine());
			case "CAMELLIARFC3211WRAP":
				return new Rfc3211WrapEngine(new CamelliaEngine());
			}
			IBufferedCipher cipher = CipherUtilities.GetCipher(algorithm);
			if (cipher != null)
			{
				return new WrapperUtilities.BufferedCipherWrapper(cipher);
			}
			throw new SecurityUtilityException("Wrapper " + algorithm + " not recognised.");
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0007E01C File Offset: 0x0007D01C
		public static string GetAlgorithmName(DerObjectIdentifier oid)
		{
			return (string)WrapperUtilities.algorithms[oid.Id];
		}

		// Token: 0x04000F33 RID: 3891
		private static readonly Hashtable algorithms = new Hashtable();

		// Token: 0x02000230 RID: 560
		private class BufferedCipherWrapper : IWrapper
		{
			// Token: 0x060015C8 RID: 5576 RVA: 0x0007E033 File Offset: 0x0007D033
			public BufferedCipherWrapper(IBufferedCipher cipher)
			{
				this.cipher = cipher;
			}

			// Token: 0x170003F6 RID: 1014
			// (get) Token: 0x060015C9 RID: 5577 RVA: 0x0007E042 File Offset: 0x0007D042
			public string AlgorithmName
			{
				get
				{
					return this.cipher.AlgorithmName;
				}
			}

			// Token: 0x060015CA RID: 5578 RVA: 0x0007E04F File Offset: 0x0007D04F
			public void Init(bool forWrapping, ICipherParameters parameters)
			{
				this.forWrapping = forWrapping;
				this.cipher.Init(forWrapping, parameters);
			}

			// Token: 0x060015CB RID: 5579 RVA: 0x0007E065 File Offset: 0x0007D065
			public byte[] Wrap(byte[] input, int inOff, int length)
			{
				if (!this.forWrapping)
				{
					throw new InvalidOperationException("Not initialised for wrapping");
				}
				return this.cipher.DoFinal(input, inOff, length);
			}

			// Token: 0x060015CC RID: 5580 RVA: 0x0007E088 File Offset: 0x0007D088
			public byte[] Unwrap(byte[] input, int inOff, int length)
			{
				if (this.forWrapping)
				{
					throw new InvalidOperationException("Not initialised for Unwrapping");
				}
				return this.cipher.DoFinal(input, inOff, length);
			}

			// Token: 0x04000F34 RID: 3892
			private readonly IBufferedCipher cipher;

			// Token: 0x04000F35 RID: 3893
			private bool forWrapping;
		}
	}
}
