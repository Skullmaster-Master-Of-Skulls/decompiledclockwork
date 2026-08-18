using System;
using System.Collections.Generic;
using System.IO;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200027D RID: 637
	public class PdfSmartCopy : PdfCopy
	{
		// Token: 0x06001830 RID: 6192 RVA: 0x0008C267 File Offset: 0x0008B267
		public PdfSmartCopy(Document document, Stream os) : base(document, os)
		{
			this.streamMap = new Dictionary<PdfSmartCopy.ByteStore, PdfIndirectReference>();
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0008C27C File Offset: 0x0008B27C
		protected override PdfIndirectReference CopyIndirect(PRIndirectReference inp)
		{
			PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(inp);
			PdfSmartCopy.ByteStore key = null;
			bool flag = false;
			if (pdfObjectRelease.IsStream())
			{
				key = new PdfSmartCopy.ByteStore((PRStream)pdfObjectRelease);
				flag = true;
				PdfIndirectReference result;
				if (this.streamMap.TryGetValue(key, out result))
				{
					return result;
				}
			}
			PdfCopy.RefKey key2 = new PdfCopy.RefKey(inp);
			PdfCopy.IndirectReferences indirectReferences;
			this.indirects.TryGetValue(key2, out indirectReferences);
			PdfIndirectReference pdfIndirectReference;
			if (indirectReferences != null)
			{
				pdfIndirectReference = indirectReferences.Ref;
				if (indirectReferences.Copied)
				{
					return pdfIndirectReference;
				}
			}
			else
			{
				pdfIndirectReference = this.body.PdfIndirectReference;
				indirectReferences = new PdfCopy.IndirectReferences(pdfIndirectReference);
				this.indirects[key2] = indirectReferences;
			}
			if (pdfObjectRelease != null && pdfObjectRelease.IsDictionary())
			{
				PdfObject pdfObjectRelease2 = PdfReader.GetPdfObjectRelease(((PdfDictionary)pdfObjectRelease).Get(PdfName.TYPE));
				if (pdfObjectRelease2 != null && PdfName.PAGE.Equals(pdfObjectRelease2))
				{
					return pdfIndirectReference;
				}
			}
			indirectReferences.SetCopied();
			if (flag)
			{
				this.streamMap[key] = pdfIndirectReference;
			}
			PdfObject objecta = base.CopyObject(pdfObjectRelease);
			base.AddToBody(objecta, pdfIndirectReference);
			return pdfIndirectReference;
		}

		// Token: 0x04001051 RID: 4177
		private Dictionary<PdfSmartCopy.ByteStore, PdfIndirectReference> streamMap;

		// Token: 0x0200027E RID: 638
		internal class ByteStore
		{
			// Token: 0x06001832 RID: 6194 RVA: 0x0008C378 File Offset: 0x0008B378
			private void SerObject(PdfObject obj, int level, ByteBuffer bb)
			{
				if (level <= 0)
				{
					return;
				}
				if (obj == null)
				{
					bb.Append("$Lnull");
					return;
				}
				obj = PdfReader.GetPdfObject(obj);
				if (obj.IsStream())
				{
					bb.Append("$B");
					this.SerDic((PdfDictionary)obj, level - 1, bb);
					if (level > 0)
					{
						bb.Append(PdfEncryption.DigestComputeHash("MD5", PdfReader.GetStreamBytesRaw((PRStream)obj)));
						return;
					}
				}
				else
				{
					if (obj.IsDictionary())
					{
						this.SerDic((PdfDictionary)obj, level - 1, bb);
						return;
					}
					if (obj.IsArray())
					{
						this.SerArray((PdfArray)obj, level - 1, bb);
						return;
					}
					if (obj.IsString())
					{
						bb.Append("$S").Append(obj.ToString());
						return;
					}
					if (obj.IsName())
					{
						bb.Append("$N").Append(obj.ToString());
						return;
					}
					bb.Append("$L").Append(obj.ToString());
				}
			}

			// Token: 0x06001833 RID: 6195 RVA: 0x0008C474 File Offset: 0x0008B474
			private void SerDic(PdfDictionary dic, int level, ByteBuffer bb)
			{
				bb.Append("$D");
				if (level <= 0)
				{
					return;
				}
				PdfName[] array = new PdfName[dic.Size];
				dic.Keys.CopyTo(array, 0);
				Array.Sort<PdfName>(array);
				for (int i = 0; i < array.Length; i++)
				{
					this.SerObject(array[i], level, bb);
					this.SerObject(dic.Get(array[i]), level, bb);
				}
			}

			// Token: 0x06001834 RID: 6196 RVA: 0x0008C4DC File Offset: 0x0008B4DC
			private void SerArray(PdfArray array, int level, ByteBuffer bb)
			{
				bb.Append("$A");
				if (level <= 0)
				{
					return;
				}
				for (int i = 0; i < array.Size; i++)
				{
					this.SerObject(array[i], level, bb);
				}
			}

			// Token: 0x06001835 RID: 6197 RVA: 0x0008C51C File Offset: 0x0008B51C
			internal ByteStore(PRStream str)
			{
				ByteBuffer byteBuffer = new ByteBuffer();
				int level = 100;
				this.SerObject(str, level, byteBuffer);
				this.b = byteBuffer.ToByteArray();
			}

			// Token: 0x06001836 RID: 6198 RVA: 0x0008C550 File Offset: 0x0008B550
			public override bool Equals(object obj)
			{
				if (obj == null || !(obj is PdfSmartCopy.ByteStore))
				{
					return false;
				}
				if (this.GetHashCode() != obj.GetHashCode())
				{
					return false;
				}
				byte[] array = ((PdfSmartCopy.ByteStore)obj).b;
				if (array.Length != this.b.Length)
				{
					return false;
				}
				int num = this.b.Length;
				for (int i = 0; i < num; i++)
				{
					if (this.b[i] != array[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06001837 RID: 6199 RVA: 0x0008C5BC File Offset: 0x0008B5BC
			public override int GetHashCode()
			{
				if (this.hash == 0)
				{
					int num = this.b.Length;
					for (int i = 0; i < num; i++)
					{
						this.hash = this.hash * 31 + (int)this.b[i];
					}
				}
				return this.hash;
			}

			// Token: 0x04001052 RID: 4178
			private byte[] b;

			// Token: 0x04001053 RID: 4179
			private int hash;
		}
	}
}
