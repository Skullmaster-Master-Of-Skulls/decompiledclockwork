using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000052 RID: 82
	public sealed class PdfEncryptor
	{
		// Token: 0x06000244 RID: 580 RVA: 0x0000B55C File Offset: 0x0000A55C
		private PdfEncryptor()
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B564 File Offset: 0x0000A564
		public static void Encrypt(PdfReader reader, Stream os, byte[] userPassword, byte[] ownerPassword, int permissions, bool strength128Bits)
		{
			PdfStamper pdfStamper = new PdfStamper(reader, os);
			pdfStamper.SetEncryption(userPassword, ownerPassword, permissions, strength128Bits);
			pdfStamper.Close();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000B58C File Offset: 0x0000A58C
		public static void Encrypt(PdfReader reader, Stream os, byte[] userPassword, byte[] ownerPassword, int permissions, bool strength128Bits, Dictionary<string, string> newInfo)
		{
			PdfStamper pdfStamper = new PdfStamper(reader, os);
			pdfStamper.SetEncryption(userPassword, ownerPassword, permissions, strength128Bits);
			pdfStamper.MoreInfo = newInfo;
			pdfStamper.Close();
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000B5BC File Offset: 0x0000A5BC
		public static void Encrypt(PdfReader reader, Stream os, bool strength, string userPassword, string ownerPassword, int permissions)
		{
			PdfStamper pdfStamper = new PdfStamper(reader, os);
			pdfStamper.SetEncryption(strength, userPassword, ownerPassword, permissions);
			pdfStamper.Close();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000B5E4 File Offset: 0x0000A5E4
		public static void Encrypt(PdfReader reader, Stream os, bool strength, string userPassword, string ownerPassword, int permissions, Dictionary<string, string> newInfo)
		{
			PdfStamper pdfStamper = new PdfStamper(reader, os);
			pdfStamper.SetEncryption(strength, userPassword, ownerPassword, permissions);
			pdfStamper.MoreInfo = newInfo;
			pdfStamper.Close();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000B614 File Offset: 0x0000A614
		public static void Encrypt(PdfReader reader, Stream os, int type, string userPassword, string ownerPassword, int permissions, Dictionary<string, string> newInfo)
		{
			PdfStamper pdfStamper = new PdfStamper(reader, os);
			pdfStamper.SetEncryption(type, userPassword, ownerPassword, permissions);
			pdfStamper.MoreInfo = newInfo;
			pdfStamper.Close();
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000B644 File Offset: 0x0000A644
		public static void Encrypt(PdfReader reader, Stream os, int type, string userPassword, string ownerPassword, int permissions)
		{
			PdfStamper pdfStamper = new PdfStamper(reader, os);
			pdfStamper.SetEncryption(type, userPassword, ownerPassword, permissions);
			pdfStamper.Close();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000B66C File Offset: 0x0000A66C
		public static string GetPermissionsVerbose(int permissions)
		{
			StringBuilder stringBuilder = new StringBuilder("Allowed:");
			if ((2052 & permissions) == 2052)
			{
				stringBuilder.Append(" Printing");
			}
			if ((8 & permissions) == 8)
			{
				stringBuilder.Append(" Modify contents");
			}
			if ((16 & permissions) == 16)
			{
				stringBuilder.Append(" Copy");
			}
			if ((32 & permissions) == 32)
			{
				stringBuilder.Append(" Modify annotations");
			}
			if ((256 & permissions) == 256)
			{
				stringBuilder.Append(" Fill in");
			}
			if ((512 & permissions) == 512)
			{
				stringBuilder.Append(" Screen readers");
			}
			if ((1024 & permissions) == 1024)
			{
				stringBuilder.Append(" Assembly");
			}
			if ((4 & permissions) == 4)
			{
				stringBuilder.Append(" Degraded printing");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000B73E File Offset: 0x0000A73E
		public static bool IsPrintingAllowed(int permissions)
		{
			return (2052 & permissions) == 2052;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000B74E File Offset: 0x0000A74E
		public static bool IsModifyContentsAllowed(int permissions)
		{
			return (8 & permissions) == 8;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000B756 File Offset: 0x0000A756
		public static bool IsCopyAllowed(int permissions)
		{
			return (16 & permissions) == 16;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B760 File Offset: 0x0000A760
		public static bool IsModifyAnnotationsAllowed(int permissions)
		{
			return (32 & permissions) == 32;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B76A File Offset: 0x0000A76A
		public static bool IsFillInAllowed(int permissions)
		{
			return (256 & permissions) == 256;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000B77A File Offset: 0x0000A77A
		public static bool IsScreenReadersAllowed(int permissions)
		{
			return (512 & permissions) == 512;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B78A File Offset: 0x0000A78A
		public static bool IsAssemblyAllowed(int permissions)
		{
			return (1024 & permissions) == 1024;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B79A File Offset: 0x0000A79A
		public static bool IsDegradedPrintingAllowed(int permissions)
		{
			return (4 & permissions) == 4;
		}
	}
}
