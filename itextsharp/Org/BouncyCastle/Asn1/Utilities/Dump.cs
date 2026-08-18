using System;
using System.IO;

namespace Org.BouncyCastle.Asn1.Utilities
{
	// Token: 0x02000259 RID: 601
	public sealed class Dump
	{
		// Token: 0x060016D6 RID: 5846 RVA: 0x00083B66 File Offset: 0x00082B66
		private Dump()
		{
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00083B70 File Offset: 0x00082B70
		public static void Main(string[] args)
		{
			FileStream inputStream = File.OpenRead(args[0]);
			Asn1InputStream asn1InputStream = new Asn1InputStream(inputStream);
			Asn1Object obj;
			while ((obj = asn1InputStream.ReadObject()) != null)
			{
				Console.WriteLine(Asn1Dump.DumpAsString(obj));
			}
			asn1InputStream.Close();
		}
	}
}
