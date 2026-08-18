using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Telerik.Pdf.Filter;
using Telerik.Pdf.Security;

namespace Telerik.Pdf
{
	// Token: 0x0200164C RID: 5708
	public class PdfStream : PdfObject
	{
		// Token: 0x170043BB RID: 17339
		// (get) Token: 0x0600DD41 RID: 56641 RVA: 0x00305733 File Offset: 0x00303933
		// (set) Token: 0x0600DD42 RID: 56642 RVA: 0x0030573B File Offset: 0x0030393B
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		protected byte[] data { get; set; }

		// Token: 0x0600DD43 RID: 56643 RVA: 0x00305744 File Offset: 0x00303944
		public PdfStream()
		{
		}

		// Token: 0x0600DD44 RID: 56644 RVA: 0x00305757 File Offset: 0x00303957
		public PdfStream(PdfObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x0600DD45 RID: 56645 RVA: 0x0030576B File Offset: 0x0030396B
		public PdfStream(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x0600DD46 RID: 56646 RVA: 0x00305785 File Offset: 0x00303985
		public PdfStream(byte[] data, PdfObjectId objectId) : base(objectId)
		{
			this.data = data;
		}

		// Token: 0x170043BC RID: 17340
		// (get) Token: 0x0600DD47 RID: 56647 RVA: 0x003057A0 File Offset: 0x003039A0
		// (set) Token: 0x0600DD48 RID: 56648 RVA: 0x003057A8 File Offset: 0x003039A8
		protected PdfDictionary m_dictionary
		{
			get
			{
				return this._m_dictionary;
			}
			set
			{
				this._m_dictionary = value;
			}
		}

		// Token: 0x0600DD49 RID: 56649 RVA: 0x003057B1 File Offset: 0x003039B1
		public void AddFilter(IFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (this.filters == null)
			{
				this.filters = new ArrayList();
			}
			this.filters.Add(filter);
		}

		// Token: 0x170043BD RID: 17341
		// (get) Token: 0x0600DD4A RID: 56650 RVA: 0x003057E4 File Offset: 0x003039E4
		private PdfObject FilterName
		{
			get
			{
				if (!this.HasFilters)
				{
					return PdfNull.Null;
				}
				if (this.filters.Count == 1)
				{
					IFilter filter = (IFilter)this.filters[0];
					return filter.Name;
				}
				PdfArray pdfArray = new PdfArray();
				foreach (object obj in this.filters)
				{
					IFilter filter2 = (IFilter)obj;
					pdfArray.Add(filter2.Name);
				}
				return pdfArray;
			}
		}

		// Token: 0x170043BE RID: 17342
		// (get) Token: 0x0600DD4B RID: 56651 RVA: 0x00305884 File Offset: 0x00303A84
		private PdfObject FilterDecodeParms
		{
			get
			{
				if (!this.HasFilters)
				{
					return PdfNull.Null;
				}
				if (this.filters.Count == 1)
				{
					IFilter filter = (IFilter)this.filters[0];
					return filter.DecodeParms;
				}
				PdfArray pdfArray = new PdfArray();
				foreach (object obj in this.filters)
				{
					IFilter filter2 = (IFilter)obj;
					pdfArray.Add(filter2.DecodeParms);
				}
				return pdfArray;
			}
		}

		// Token: 0x170043BF RID: 17343
		// (get) Token: 0x0600DD4C RID: 56652 RVA: 0x00305924 File Offset: 0x00303B24
		private bool HasFilters
		{
			get
			{
				return this.filters != null && this.filters.Count > 0;
			}
		}

		// Token: 0x170043C0 RID: 17344
		// (get) Token: 0x0600DD4D RID: 56653 RVA: 0x00305940 File Offset: 0x00303B40
		private bool HasDecodeParams
		{
			get
			{
				if (this.filters == null)
				{
					return false;
				}
				foreach (object obj in this.filters)
				{
					IFilter filter = (IFilter)obj;
					if (filter.HasDecodeParams)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x0600DD4E RID: 56654 RVA: 0x003059AC File Offset: 0x00303BAC
		private byte[] ApplyFilters(byte[] data)
		{
			if (this.filters == null)
			{
				return data;
			}
			byte[] array = data;
			for (int i = this.filters.Count - 1; i >= 0; i--)
			{
				IFilter filter = (IFilter)this.filters[i];
				array = filter.Encode(array);
			}
			return array;
		}

		// Token: 0x0600DD4F RID: 56655 RVA: 0x003059F8 File Offset: 0x00303BF8
		protected internal override void Write(PdfWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (this.data == null)
			{
				throw new InvalidOperationException("No data for stream.");
			}
			byte[] array = (byte[])this.data.Clone();
			if (this.HasFilters)
			{
				array = this.ApplyFilters(this.data);
			}
			SecurityManager securityManager = writer.SecurityManager;
			if (securityManager != null)
			{
				array = securityManager.Encrypt(array, writer.EnclosingIndirect.ObjectId);
			}
			this.m_dictionary[PdfName.Names.Length] = new PdfNumeric(array.Length);
			if (this.HasFilters)
			{
				this.m_dictionary[PdfName.Names.Filter] = this.FilterName;
				if (this.HasDecodeParams)
				{
					this.m_dictionary[PdfName.Names.DecodeParams] = this.FilterDecodeParms;
				}
			}
			writer.WriteLine(this.m_dictionary);
			writer.WriteKeywordLine(Keyword.Stream);
			writer.WriteLine(array);
			writer.WriteKeyword(Keyword.EndStream);
		}

		// Token: 0x04003EF2 RID: 16114
		private PdfDictionary _m_dictionary = new PdfDictionary();

		// Token: 0x04003EF3 RID: 16115
		private IList filters;
	}
}
