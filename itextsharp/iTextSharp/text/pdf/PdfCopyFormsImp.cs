using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000DF RID: 223
	internal class PdfCopyFormsImp : PdfCopyFieldsImp
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x0002AF30 File Offset: 0x00029F30
		internal PdfCopyFormsImp(Stream os) : base(os)
		{
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0002AF3C File Offset: 0x00029F3C
		public void CopyDocumentFields(PdfReader reader)
		{
			if (!reader.IsOpenedWithFullPermissions)
			{
				throw new BadPasswordException(MessageLocalization.GetComposedMessage("pdfreader.not.opened.with.owner.password"));
			}
			if (this.readers2intrefs.ContainsKey(reader))
			{
				reader = new PdfReader(reader);
			}
			else
			{
				if (reader.Tampered)
				{
					throw new DocumentException(MessageLocalization.GetComposedMessage("the.document.was.reused"));
				}
				reader.ConsolidateNamedDestinations();
				reader.Tampered = true;
			}
			reader.ShuffleSubsetNames();
			this.readers2intrefs[reader] = new IntHashtable();
			this.fields.Add(reader.AcroFields);
			base.UpdateCalculationOrder(reader);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0002AFD0 File Offset: 0x00029FD0
		internal override void MergeFields()
		{
			for (int i = 0; i < this.fields.Count; i++)
			{
				Dictionary<string, AcroFields.Item> fields = this.fields[i].Fields;
				base.MergeWithMaster(fields);
			}
		}
	}
}
