using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000443 RID: 1091
	public class ArmoredOutputStream : BaseOutputStream
	{
		// Token: 0x060024F5 RID: 9461 RVA: 0x000E080C File Offset: 0x000DF80C
		private static void Encode(Stream outStream, int[] data, int len)
		{
			byte[] array = new byte[4];
			int num = data[0];
			array[0] = ArmoredOutputStream.encodingTable[num >> 2 & 63];
			switch (len)
			{
			case 1:
				array[1] = ArmoredOutputStream.encodingTable[num << 4 & 63];
				array[2] = 61;
				array[3] = 61;
				break;
			case 2:
			{
				int num2 = data[1];
				array[1] = ArmoredOutputStream.encodingTable[(num << 4 | num2 >> 4) & 63];
				array[2] = ArmoredOutputStream.encodingTable[num2 << 2 & 63];
				array[3] = 61;
				break;
			}
			case 3:
			{
				int num3 = data[1];
				int num4 = data[2];
				array[1] = ArmoredOutputStream.encodingTable[(num << 4 | num3 >> 4) & 63];
				array[2] = ArmoredOutputStream.encodingTable[(num3 << 2 | num4 >> 6) & 63];
				array[3] = ArmoredOutputStream.encodingTable[num4 & 63];
				break;
			}
			}
			outStream.Write(array, 0, array.Length);
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x000E08E4 File Offset: 0x000DF8E4
		public ArmoredOutputStream(Stream outStream)
		{
			this.outStream = outStream;
			this.headers = new Hashtable();
			this.headers["Version"] = ArmoredOutputStream.version;
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000E093C File Offset: 0x000DF93C
		public ArmoredOutputStream(Stream outStream, IDictionary headers)
		{
			this.outStream = outStream;
			this.headers = new Hashtable(headers);
			this.headers["Version"] = ArmoredOutputStream.version;
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000E0995 File Offset: 0x000DF995
		public void SetHeader(string name, string v)
		{
			this.headers[name] = v;
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000E09A4 File Offset: 0x000DF9A4
		public void ResetHeaders()
		{
			this.headers.Clear();
			this.headers["Version"] = ArmoredOutputStream.version;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000E09C8 File Offset: 0x000DF9C8
		public void BeginClearText(HashAlgorithmTag hashAlgorithm)
		{
			string str;
			switch (hashAlgorithm)
			{
			case HashAlgorithmTag.MD5:
				str = "MD5";
				goto IL_82;
			case HashAlgorithmTag.Sha1:
				str = "SHA1";
				goto IL_82;
			case HashAlgorithmTag.RipeMD160:
				str = "RIPEMD160";
				goto IL_82;
			case HashAlgorithmTag.MD2:
				str = "MD2";
				goto IL_82;
			case HashAlgorithmTag.Sha256:
				str = "SHA256";
				goto IL_82;
			case HashAlgorithmTag.Sha384:
				str = "SHA384";
				goto IL_82;
			case HashAlgorithmTag.Sha512:
				str = "SHA512";
				goto IL_82;
			}
			throw new IOException("unknown hash algorithm tag in beginClearText: " + hashAlgorithm);
			IL_82:
			this.DoWrite("-----BEGIN PGP SIGNED MESSAGE-----" + ArmoredOutputStream.nl);
			this.DoWrite("Hash: " + str + ArmoredOutputStream.nl + ArmoredOutputStream.nl);
			this.clearText = true;
			this.newLine = true;
			this.lastb = 0;
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000E0A9C File Offset: 0x000DFA9C
		public void EndClearText()
		{
			this.clearText = false;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000E0AA8 File Offset: 0x000DFAA8
		public override void WriteByte(byte b)
		{
			if (this.clearText)
			{
				this.outStream.WriteByte(b);
				if (this.newLine)
				{
					if (b != 10 || this.lastb != 13)
					{
						this.newLine = false;
					}
					if (b == 45)
					{
						this.outStream.WriteByte(32);
						this.outStream.WriteByte(45);
					}
				}
				if (b == 13 || (b == 10 && this.lastb != 13))
				{
					this.newLine = true;
				}
				this.lastb = (int)b;
				return;
			}
			if (this.start)
			{
				bool flag = (b & 64) != 0;
				int num;
				if (flag)
				{
					num = (int)(b & 63);
				}
				else
				{
					num = (b & 63) >> 2;
				}
				switch (num)
				{
				case 2:
					this.type = "SIGNATURE";
					goto IL_EF;
				case 5:
					this.type = "PRIVATE KEY BLOCK";
					goto IL_EF;
				case 6:
					this.type = "PUBLIC KEY BLOCK";
					goto IL_EF;
				}
				this.type = "MESSAGE";
				IL_EF:
				this.DoWrite(ArmoredOutputStream.headerStart + this.type + ArmoredOutputStream.headerTail + ArmoredOutputStream.nl);
				this.WriteHeaderEntry("Version", (string)this.headers["Version"]);
				foreach (object obj in this.headers)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = (string)dictionaryEntry.Key;
					if (text != "Version")
					{
						string v = (string)dictionaryEntry.Value;
						this.WriteHeaderEntry(text, v);
					}
				}
				this.DoWrite(ArmoredOutputStream.nl);
				this.start = false;
			}
			if (this.bufPtr == 3)
			{
				ArmoredOutputStream.Encode(this.outStream, this.buf, this.bufPtr);
				this.bufPtr = 0;
				if ((++this.chunkCount & 15) == 0)
				{
					this.DoWrite(ArmoredOutputStream.nl);
				}
			}
			this.crc.Update((int)b);
			this.buf[this.bufPtr++] = (int)(b & byte.MaxValue);
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000E0CEC File Offset: 0x000DFCEC
		public override void Close()
		{
			if (this.type != null)
			{
				if (this.bufPtr > 0)
				{
					ArmoredOutputStream.Encode(this.outStream, this.buf, this.bufPtr);
				}
				this.DoWrite(ArmoredOutputStream.nl + '=');
				int value = this.crc.Value;
				this.buf[0] = (value >> 16 & 255);
				this.buf[1] = (value >> 8 & 255);
				this.buf[2] = (value & 255);
				ArmoredOutputStream.Encode(this.outStream, this.buf, 3);
				this.DoWrite(ArmoredOutputStream.nl);
				this.DoWrite(ArmoredOutputStream.footerStart);
				this.DoWrite(this.type);
				this.DoWrite(ArmoredOutputStream.footerTail);
				this.DoWrite(ArmoredOutputStream.nl);
				this.outStream.Flush();
				this.type = null;
				this.start = true;
				base.Close();
			}
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000E0DE2 File Offset: 0x000DFDE2
		private void WriteHeaderEntry(string name, string v)
		{
			this.DoWrite(name + ": " + v + ArmoredOutputStream.nl);
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000E0DFC File Offset: 0x000DFDFC
		private void DoWrite(string s)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(s);
			this.outStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x040019BF RID: 6591
		private static readonly byte[] encodingTable = new byte[]
		{
			65,
			66,
			67,
			68,
			69,
			70,
			71,
			72,
			73,
			74,
			75,
			76,
			77,
			78,
			79,
			80,
			81,
			82,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			97,
			98,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108,
			109,
			110,
			111,
			112,
			113,
			114,
			115,
			116,
			117,
			118,
			119,
			120,
			121,
			122,
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
			43,
			47
		};

		// Token: 0x040019C0 RID: 6592
		private readonly Stream outStream;

		// Token: 0x040019C1 RID: 6593
		private int[] buf = new int[3];

		// Token: 0x040019C2 RID: 6594
		private int bufPtr;

		// Token: 0x040019C3 RID: 6595
		private Crc24 crc = new Crc24();

		// Token: 0x040019C4 RID: 6596
		private int chunkCount;

		// Token: 0x040019C5 RID: 6597
		private int lastb;

		// Token: 0x040019C6 RID: 6598
		private bool start = true;

		// Token: 0x040019C7 RID: 6599
		private bool clearText;

		// Token: 0x040019C8 RID: 6600
		private bool newLine;

		// Token: 0x040019C9 RID: 6601
		private string type;

		// Token: 0x040019CA RID: 6602
		private static readonly string nl = Platform.NewLine;

		// Token: 0x040019CB RID: 6603
		private static readonly string headerStart = "-----BEGIN PGP ";

		// Token: 0x040019CC RID: 6604
		private static readonly string headerTail = "-----";

		// Token: 0x040019CD RID: 6605
		private static readonly string footerStart = "-----END PGP ";

		// Token: 0x040019CE RID: 6606
		private static readonly string footerTail = "-----";

		// Token: 0x040019CF RID: 6607
		private static readonly string version = "BCPG C# v" + Assembly.GetExecutingAssembly().GetName().Version;

		// Token: 0x040019D0 RID: 6608
		private readonly IDictionary headers;
	}
}
