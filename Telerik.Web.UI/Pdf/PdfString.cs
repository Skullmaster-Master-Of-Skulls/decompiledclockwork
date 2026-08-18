using System;
using System.IO;
using System.Text;
using Telerik.Pdf.Security;

namespace Telerik.Pdf
{
	// Token: 0x0200166D RID: 5741
	public sealed class PdfString : PdfObject
	{
		// Token: 0x0600DE1D RID: 56861 RVA: 0x00308BD2 File Offset: 0x00306DD2
		public PdfString(string val)
		{
			this.data = this.encoding.GetBytes(val);
		}

		// Token: 0x0600DE1E RID: 56862 RVA: 0x00308BF7 File Offset: 0x00306DF7
		public PdfString(string val, PdfObjectId objectId) : base(objectId)
		{
			this.data = this.encoding.GetBytes(val);
		}

		// Token: 0x0600DE1F RID: 56863 RVA: 0x00308C1D File Offset: 0x00306E1D
		public PdfString(string val, Encoding encoding)
		{
			this.encoding = encoding;
			this.data = encoding.GetBytes(val);
		}

		// Token: 0x0600DE20 RID: 56864 RVA: 0x00308C44 File Offset: 0x00306E44
		public PdfString(string val, Encoding encoding, PdfObjectId objectId) : base(objectId)
		{
			this.encoding = encoding;
			this.data = encoding.GetBytes(val);
		}

		// Token: 0x0600DE21 RID: 56865 RVA: 0x00308C6C File Offset: 0x00306E6C
		public PdfString(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x0600DE22 RID: 56866 RVA: 0x00308C86 File Offset: 0x00306E86
		public PdfString(byte[] data, PdfObjectId objectId) : base(objectId)
		{
			this.data = data;
		}

		// Token: 0x170043F2 RID: 17394
		// (get) Token: 0x0600DE23 RID: 56867 RVA: 0x00308CA1 File Offset: 0x00306EA1
		// (set) Token: 0x0600DE24 RID: 56868 RVA: 0x00308CA9 File Offset: 0x00306EA9
		public PdfStringFormat Format
		{
			get
			{
				return this.format;
			}
			set
			{
				this.format = value;
			}
		}

		// Token: 0x170043F3 RID: 17395
		// (get) Token: 0x0600DE25 RID: 56869 RVA: 0x00308CB2 File Offset: 0x00306EB2
		// (set) Token: 0x0600DE26 RID: 56870 RVA: 0x00308CBA File Offset: 0x00306EBA
		internal bool NeverEncrypt
		{
			get
			{
				return this.neverEncrypt;
			}
			set
			{
				this.neverEncrypt = value;
			}
		}

		// Token: 0x0600DE27 RID: 56871 RVA: 0x00308CC4 File Offset: 0x00306EC4
		protected internal override void Write(PdfWriter writer)
		{
			byte[] array = (byte[])this.data.Clone();
			if (!this.neverEncrypt)
			{
				SecurityManager securityManager = writer.SecurityManager;
				if (securityManager != null)
				{
					array = securityManager.Encrypt(array, writer.EnclosingIndirect.ObjectId);
				}
			}
			if (this.format == PdfStringFormat.Literal)
			{
				array = PdfString.ToPdfLiteral(this.encoding.GetPreamble(), array);
			}
			else
			{
				array = PdfString.ToPdfHexadecimal(this.encoding.GetPreamble(), array);
			}
			writer.Write(array);
		}

		// Token: 0x0600DE28 RID: 56872 RVA: 0x00308D3C File Offset: 0x00306F3C
		internal static byte[] ToPdfLiteral(byte[] preamble, byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream(data.Length + 10);
			memoryStream.WriteByte(40);
			memoryStream.Write(preamble, 0, preamble.Length);
			foreach (byte b in data)
			{
				if (b == 40 || b == 41 || b == 92)
				{
					memoryStream.WriteByte(92);
					memoryStream.WriteByte(b);
				}
				else if (b == 13)
				{
					memoryStream.WriteByte(92);
					memoryStream.WriteByte(114);
				}
				else if (b == 10)
				{
					memoryStream.WriteByte(92);
					memoryStream.WriteByte(110);
				}
				else
				{
					memoryStream.WriteByte(b);
				}
			}
			memoryStream.WriteByte(41);
			return memoryStream.ToArray();
		}

		// Token: 0x0600DE29 RID: 56873 RVA: 0x00308DE0 File Offset: 0x00306FE0
		internal static byte[] ToPdfHexadecimal(byte[] preamble, byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream(data.Length * 2 + 2);
			memoryStream.WriteByte(60);
			memoryStream.Write(preamble, 0, preamble.Length);
			foreach (byte b in data)
			{
				memoryStream.WriteByte(PdfString.HexDigits[b >> 4]);
				memoryStream.WriteByte(PdfString.HexDigits[(int)(b & 15)]);
			}
			memoryStream.WriteByte(62);
			return memoryStream.ToArray();
		}

		// Token: 0x04003FD8 RID: 16344
		private byte[] data;

		// Token: 0x04003FD9 RID: 16345
		private Encoding encoding = Encoding.Default;

		// Token: 0x04003FDA RID: 16346
		private PdfStringFormat format;

		// Token: 0x04003FDB RID: 16347
		private bool neverEncrypt;

		// Token: 0x04003FDC RID: 16348
		private static readonly byte[] HexDigits = new byte[]
		{
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			97,
			98,
			99,
			100,
			101,
			102
		};
	}
}
