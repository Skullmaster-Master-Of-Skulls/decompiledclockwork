using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Telerik.Pdf;
using Telerik.Pdf.Filter;
using Telerik.Pdf.Security;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Image;
using Telerik.Web.Apoc.Layout;
using Telerik.Web.Apoc.Render.Pdf;

namespace Telerik.Web.Apoc.Pdf
{
	// Token: 0x0200164F RID: 5711
	internal sealed class PdfCreator
	{
		// Token: 0x170043C4 RID: 17348
		// (get) Token: 0x0600DD6F RID: 56687 RVA: 0x00306090 File Offset: 0x00304290
		// (set) Token: 0x0600DD70 RID: 56688 RVA: 0x00306098 File Offset: 0x00304298
		internal PdfRendererOptions RendererOptions { get; set; }

		// Token: 0x0600DD71 RID: 56689 RVA: 0x003060A4 File Offset: 0x003042A4
		public PdfCreator(Stream stream)
		{
			this.doc = new PdfDocument(stream);
			this.doc.Version = PdfVersion.V13;
			this.resources = new PdfResources(this.doc.NextObjectId());
			this.addTrailerObject(this.resources);
			this.xrefTable = new XRefTable();
		}

		// Token: 0x0600DD72 RID: 56690 RVA: 0x00306121 File Offset: 0x00304321
		public void setIDReferences(IDReferences idReferences)
		{
			this.idReferences = idReferences;
		}

		// Token: 0x170043C5 RID: 17349
		// (get) Token: 0x0600DD73 RID: 56691 RVA: 0x0030612A File Offset: 0x0030432A
		public PdfDocument Doc
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x0600DD74 RID: 56692 RVA: 0x00306132 File Offset: 0x00304332
		public void AddObject(PdfObject obj)
		{
			this.objects.Add(obj);
		}

		// Token: 0x0600DD75 RID: 56693 RVA: 0x00306144 File Offset: 0x00304344
		public PdfXObject AddImage(ApocImage img)
		{
			string uri = img.Uri;
			PdfXObject pdfXObject = (PdfXObject)this.xObjectsMap[uri];
			if (pdfXObject == null)
			{
				PdfICCStream pdfICCStream = null;
				ColorSpace colorSpace = img.ColorSpace;
				if (colorSpace.HasICCProfile())
				{
					pdfICCStream = new PdfICCStream(this.doc.NextObjectId(), colorSpace.GetICCProfile());
					pdfICCStream.NumComponents = new PdfNumeric(colorSpace.GetNumComponents());
					IFilter activeFilter = this.RendererOptions.GetActiveFilter();
					if (activeFilter != null)
					{
						pdfICCStream.AddFilter(activeFilter);
					}
					this.objects.Add(pdfICCStream);
				}
				PdfName name = new PdfName("XO" + this.xObjectsMap.Count);
				pdfXObject = new PdfXObject(img.Bitmaps, name, this.doc.NextObjectId());
				pdfXObject.SubType = PdfName.Names.Image;
				pdfXObject.Dictionary[PdfName.Names.Width] = new PdfNumeric(img.Width);
				pdfXObject.Dictionary[PdfName.Names.Height] = new PdfNumeric(img.Height);
				pdfXObject.Dictionary[PdfName.Names.BitsPerComponent] = new PdfNumeric(img.BitsPerPixel);
				if (pdfICCStream != null)
				{
					PdfArray pdfArray = new PdfArray();
					pdfArray.Add(PdfName.Names.ICCBased);
					pdfArray.Add(pdfICCStream.GetReference());
					pdfXObject.Dictionary[PdfName.Names.ColorSpace] = pdfArray;
				}
				else
				{
					pdfXObject.Dictionary[PdfName.Names.ColorSpace] = new PdfName(img.ColorSpace.GetColorSpacePDFString());
				}
				string value = Path.GetExtension(img.Uri).ToLower();
				string[] array = new string[]
				{
					".jpg",
					".jpe",
					".jpeg",
					".jfif",
					".jif",
					".jfi"
				};
				string pattern = "url\\(['|\\\"]data:image/(\\w{3,4});.+['|\\\"]\\)";
				Match match = Regex.Match(img.Uri, pattern);
				if (match.Success)
				{
					value = match.Groups[1].Value;
				}
				DctFilter dctFilter = new DctFilter();
				if (Array.IndexOf<string>(array, value) >= 0 || (img.Filter != null && img.Filter.Name == dctFilter.Name))
				{
					pdfXObject.AddFilter(dctFilter);
				}
				else
				{
					IFilter activeFilter2 = this.RendererOptions.GetActiveFilter();
					if (activeFilter2 != null)
					{
						pdfXObject.AddFilter(activeFilter2);
					}
				}
				this.objects.Add(pdfXObject);
				this.xObjectsMap.Add(uri, pdfXObject);
			}
			return pdfXObject;
		}

		// Token: 0x0600DD76 RID: 56694 RVA: 0x003063C8 File Offset: 0x003045C8
		public PdfPage makePage(PdfResources resources, PdfContentStream contents, int pagewidth, int pageheight, Page currentPage)
		{
			PdfPage pdfPage = new PdfPage(resources, contents, pagewidth, pageheight, this.doc.NextObjectId());
			if (currentPage != null)
			{
				foreach (object obj in currentPage.getIDList())
				{
					string id = (string)obj;
					this.idReferences.setInternalGoToPageReference(id, pdfPage.GetReference());
				}
			}
			this.objects.Add(pdfPage);
			pdfPage.SetParent(this.doc.Pages);
			this.doc.Pages.Kids.Add(pdfPage.GetReference());
			return pdfPage;
		}

		// Token: 0x0600DD77 RID: 56695 RVA: 0x00306484 File Offset: 0x00304684
		public PdfLink makeLink(Rectangle rect, string destination, int linkType)
		{
			PdfLink pdfLink = new PdfLink(this.doc.NextObjectId(), rect);
			this.objects.Add(pdfLink);
			if (linkType == 1)
			{
				PdfUri action = new PdfUri(destination);
				pdfLink.SetAction(action);
			}
			else
			{
				PdfObjectReference goToReference = this.getGoToReference(destination);
				PdfInternalLink action2 = new PdfInternalLink(goToReference);
				pdfLink.SetAction(action2);
			}
			return pdfLink;
		}

		// Token: 0x0600DD78 RID: 56696 RVA: 0x003064DC File Offset: 0x003046DC
		private PdfObjectReference getGoToReference(string destination)
		{
			PdfGoTo pdfGoTo;
			if (this.idReferences.doesIDExist(destination))
			{
				if (this.idReferences.doesGoToReferenceExist(destination))
				{
					pdfGoTo = this.idReferences.getInternalLinkGoTo(destination);
				}
				else
				{
					pdfGoTo = this.idReferences.createInternalLinkGoTo(destination, this.doc.NextObjectId());
					this.addTrailerObject(pdfGoTo);
				}
			}
			else
			{
				this.idReferences.CreateUnvalidatedID(destination);
				this.idReferences.AddToIdValidationList(destination);
				pdfGoTo = this.idReferences.createInternalLinkGoTo(destination, this.doc.NextObjectId());
				this.addTrailerObject(pdfGoTo);
			}
			return pdfGoTo.GetReference();
		}

		// Token: 0x0600DD79 RID: 56697 RVA: 0x00306572 File Offset: 0x00304772
		private void addTrailerObject(PdfObject obj)
		{
			this.trailerObjects.Add(obj);
		}

		// Token: 0x0600DD7A RID: 56698 RVA: 0x00306584 File Offset: 0x00304784
		public PdfContentStream makeContentStream()
		{
			PdfContentStream pdfContentStream = new PdfContentStream(this.doc.NextObjectId());
			IFilter activeFilter = this.RendererOptions.GetActiveFilter();
			if (activeFilter != null)
			{
				pdfContentStream.AddFilter(activeFilter);
			}
			this.objects.Add(pdfContentStream);
			return pdfContentStream;
		}

		// Token: 0x0600DD7B RID: 56699 RVA: 0x003065C8 File Offset: 0x003047C8
		public PdfAnnotList makeAnnotList()
		{
			PdfAnnotList pdfAnnotList = new PdfAnnotList(this.doc.NextObjectId());
			this.objects.Add(pdfAnnotList);
			return pdfAnnotList;
		}

		// Token: 0x0600DD7C RID: 56700 RVA: 0x003065F4 File Offset: 0x003047F4
		public void SetOptions(PdfRendererOptions options)
		{
			this.RendererOptions = options;
			this.info = new PdfInfo(this.doc.NextObjectId());
			if (options.Title != null)
			{
				this.info.Title = new PdfString(options.Title);
			}
			if (options.Author != null)
			{
				this.info.Author = new PdfString(options.Author);
			}
			if (options.Subject != null)
			{
				this.info.Subject = new PdfString(options.Subject);
			}
			if (!string.IsNullOrEmpty(options.Keywords))
			{
				this.info.Keywords = new PdfString(options.Keywords);
			}
			if (options.Creator != null)
			{
				this.info.Creator = new PdfString(options.Creator);
			}
			if (options.Producer != null)
			{
				this.info.Producer = new PdfString(options.Producer);
			}
			this.info.CreationDate = new PdfString(PdfDate.Format(DateTime.Now));
			this.objects.Add(this.info);
			if ((options.UserPassword != null || options.OwnerPassword != null || options.HasPermissions) && !PdfCreator.FipsEnabled)
			{
				SecurityOptions securityOptions = new SecurityOptions();
				securityOptions.UserPassword = options.UserPassword;
				securityOptions.OwnerPassword = options.OwnerPassword;
				securityOptions.EnableAdding(options.EnableAdd);
				securityOptions.EnableChanging(options.EnableModify);
				securityOptions.EnableCopying(options.EnableCopy);
				securityOptions.EnablePrinting(options.EnablePrinting);
				this.doc.SecurityOptions = securityOptions;
				this.encrypt = this.doc.Writer.SecurityManager.GetEncrypt(this.doc.NextObjectId());
				this.objects.Add(this.encrypt);
			}
		}

		// Token: 0x170043C6 RID: 17350
		// (get) Token: 0x0600DD7D RID: 56701 RVA: 0x003067BC File Offset: 0x003049BC
		private static bool FipsEnabled
		{
			get
			{
				try
				{
					MD5.Create();
				}
				catch (Exception ex)
				{
					if (ex is TargetInvocationException)
					{
						return true;
					}
					throw;
				}
				return false;
			}
		}

		// Token: 0x0600DD7E RID: 56702 RVA: 0x003067F4 File Offset: 0x003049F4
		public PdfOutline getOutlineRoot()
		{
			if (this.outlineRoot != null)
			{
				return this.outlineRoot;
			}
			this.outlineRoot = new PdfOutline(this.doc.NextObjectId(), null, null);
			this.addTrailerObject(this.outlineRoot);
			this.doc.Catalog.Outlines = this.outlineRoot;
			return this.outlineRoot;
		}

		// Token: 0x0600DD7F RID: 56703 RVA: 0x00306850 File Offset: 0x00304A50
		public PdfOutline makeOutline(PdfOutline parent, string label, string destination)
		{
			PdfObjectReference goToReference = this.getGoToReference(destination);
			PdfOutline pdfOutline = new PdfOutline(this.doc.NextObjectId(), label, goToReference);
			if (parent != null)
			{
				parent.AddOutline(pdfOutline);
			}
			this.objects.Add(pdfOutline);
			return pdfOutline;
		}

		// Token: 0x0600DD80 RID: 56704 RVA: 0x00306890 File Offset: 0x00304A90
		public PdfResources getResources()
		{
			return this.resources;
		}

		// Token: 0x0600DD81 RID: 56705 RVA: 0x00306898 File Offset: 0x00304A98
		private void WritePdfObject(PdfObject obj)
		{
			this.xrefTable.Add(obj.ObjectId, this.doc.Writer.Position);
			this.doc.Writer.WriteLine(obj);
		}

		// Token: 0x0600DD82 RID: 56706 RVA: 0x003068CC File Offset: 0x00304ACC
		public void output()
		{
			foreach (object obj in this.objects)
			{
				PdfObject obj2 = (PdfObject)obj;
				this.WritePdfObject(obj2);
			}
			this.objects.Clear();
		}

		// Token: 0x0600DD83 RID: 56707 RVA: 0x00306930 File Offset: 0x00304B30
		public void outputHeader()
		{
			this.doc.WriteHeader();
		}

		// Token: 0x0600DD84 RID: 56708 RVA: 0x00306940 File Offset: 0x00304B40
		public void outputTrailer()
		{
			this.output();
			foreach (object obj in this.xObjectsMap.Values)
			{
				PdfXObject xObject = (PdfXObject)obj;
				this.resources.AddXObject(xObject);
			}
			this.xrefTable.Add(this.doc.Catalog.ObjectId, this.doc.Writer.Position);
			this.doc.Writer.WriteLine(this.doc.Catalog);
			this.xrefTable.Add(this.doc.Pages.ObjectId, this.doc.Writer.Position);
			this.doc.Writer.WriteLine(this.doc.Pages);
			foreach (object obj2 in this.trailerObjects)
			{
				PdfObject obj3 = (PdfObject)obj2;
				this.WritePdfObject(obj3);
			}
			long position = this.doc.Writer.Position;
			this.xrefTable.Write(this.doc.Writer);
			PdfFileTrailer pdfFileTrailer = new PdfFileTrailer();
			pdfFileTrailer.Size = new PdfNumeric(this.doc.ObjectCount + 1);
			pdfFileTrailer.Root = this.doc.Catalog.GetReference();
			pdfFileTrailer.Id = this.doc.FileIdentifier;
			if (this.info != null)
			{
				pdfFileTrailer.Info = this.info.GetReference();
			}
			if (this.info != null && this.encrypt != null)
			{
				pdfFileTrailer.Encrypt = this.encrypt.GetReference();
			}
			pdfFileTrailer.XRefOffset = position;
			this.doc.Writer.Write(pdfFileTrailer);
		}

		// Token: 0x04003EFA RID: 16122
		private PdfDocument doc;

		// Token: 0x04003EFB RID: 16123
		private ArrayList trailerObjects = new ArrayList();

		// Token: 0x04003EFC RID: 16124
		private ArrayList objects = new ArrayList();

		// Token: 0x04003EFD RID: 16125
		private PdfOutline outlineRoot;

		// Token: 0x04003EFE RID: 16126
		private PdfResources resources;

		// Token: 0x04003EFF RID: 16127
		private IDReferences idReferences;

		// Token: 0x04003F00 RID: 16128
		private Hashtable xObjectsMap = new Hashtable();

		// Token: 0x04003F01 RID: 16129
		private XRefTable xrefTable;

		// Token: 0x04003F02 RID: 16130
		private PdfInfo info;

		// Token: 0x04003F03 RID: 16131
		private PdfDictionary encrypt;
	}
}
