using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x02000444 RID: 1092
internal class spr\u2295
{
	// Token: 0x06003CDA RID: 15578 RVA: 0x0038B22C File Offset: 0x0038A22C
	private spr\u2295()
	{
	}

	// Token: 0x06003CDB RID: 15579 RVA: 0x0038B240 File Offset: 0x0038A240
	internal static DigitalSignature ᜀ(BinaryReader A_0, byte[] A_1)
	{
		int a_ = 16;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			switch (0)
			{
			}
			break;
		}
		DigitalSignature digitalSignature = new DigitalSignature(DigitalSignatureType.CryptoApi);
		DateTime minValue = DateTime.MinValue;
		bool a_4;
		try
		{
			for (;;)
			{
				int num = A_0.ReadInt32();
				int num2 = A_0.ReadInt32();
				spr\u2295.ᜀ(A_0);
				uint num3 = A_0.ReadUInt32();
				uint num4 = A_0.ReadUInt32();
				long num5 = (long)((ulong)num3);
				num5 <<= 32;
				num5 |= (long)((ulong)num4);
				digitalSignature.ᜀ(DateTime.FromFileTimeUtc(num5));
				A_0.ReadInt32();
				int count = A_0.ReadInt32();
				int num6 = A_0.ReadInt32();
				A_0.ReadInt32();
				int count2 = A_0.ReadInt32();
				int count3 = A_0.ReadInt32();
				A_0.ReadInt32();
				sprឱ.ᜃ(A_0, (num + 1) * 2);
				sprឱ.ᜃ(A_0, (num2 + 1) * 2);
				byte[] array = A_0.ReadBytes(count);
				byte[] rawData = A_0.ReadBytes(num6);
				A_0.ReadBytes(count2);
				A_0.ReadBytes(count3);
				int num7 = 1;
				for (;;)
				{
					switch (num7)
					{
					case 0:
						goto IL_237;
					case 1:
					{
						if (num6 == 0)
						{
							num7 = 2;
							continue;
						}
						X509Certificate2 x509Certificate = new X509Certificate2(rawData);
						digitalSignature.ᜀ(x509Certificate);
						RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)x509Certificate.PublicKey.Key;
						RSAParameters rsaparameters = rsacryptoServiceProvider.ExportParameters(false);
						Array.Reverse(array);
						byte[] a_2 = sprὔ.ᜀ(array);
						Rsa rsa = new Rsa(rsaparameters.Modulus, rsaparameters.Exponent);
						byte[] a_3 = sprὔ.ᜀ(rsa, a_2);
						byte[] array2 = sprὔ.ᜀ(a_3, rsa.Modulus.ᜆ() >> 3);
						byte[] array3 = new byte[16];
						Array.Copy(array2, array2.Length - 16, array3, 0, 16);
						Spire.Doc.Fields.Shape.MD5 md = new Spire.Doc.Fields.Shape.MD5();
						md.Update(A_1, A_1.Length);
						md.Update(BitConverter.GetBytes(num4), 4);
						md.Update(BitConverter.GetBytes(num3), 4);
						md.FinalUpdate();
						a_4 = sprὊ.ᜂ(array3, md.Digest);
						num7 = 0;
						continue;
					}
					case 2:
						goto IL_13E;
					}
					break;
				}
			}
			IL_13E:
			throw new InvalidOperationException(ClipboardData.b("≵ၷό๻᭽ꁿꚅ겋ﾕﺗﾛﾝ풟잡蒣쎥얧좩즫쪭풯ힱ킳隵\udeb7햹캻麽ꆿꃃ꿅꿇ꏉ룋꿍볏ꟓ뿕뿗듙뷛ꫝ闟郡臣죥", a_));
			IL_237:;
		}
		catch (Exception)
		{
			a_4 = false;
		}
		digitalSignature.ᜂ(a_4);
		return digitalSignature;
	}

	// Token: 0x06003CDC RID: 15580 RVA: 0x0038B4B0 File Offset: 0x0038A4B0
	private static DateTime ᜀ(BinaryReader A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		long num = (long)((ulong)A_0.ReadUInt32());
		num <<= 32;
		num |= (long)((ulong)A_0.ReadUInt32());
		return DateTime.FromFileTimeUtc(num);
	}

	// Token: 0x04002C0A RID: 11274
	private const int ᜀ = 16;
}
