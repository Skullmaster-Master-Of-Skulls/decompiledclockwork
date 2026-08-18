using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003F5 RID: 1013
	[ControlValueProperty("FileBytes")]
	[ValidationProperty("FileName")]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class FileUpload : WebControl
	{
		// Token: 0x060030D9 RID: 12505 RVA: 0x00087CE0 File Offset: 0x00085EE0
		public FileUpload() : base(HtmlTextWriterTag.Input)
		{
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x0009EEAC File Offset: 0x0009D0AC
		// (set) Token: 0x060030DB RID: 12507 RVA: 0x0009EED5 File Offset: 0x0009D0D5
		[Browsable(true)]
		[DefaultValue(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("FileUpload_AllowMultiple")]
		public virtual bool AllowMultiple
		{
			get
			{
				object obj = this.ViewState["AllowMultiple"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowMultiple"] = value;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x0009EEF0 File Offset: 0x0009D0F0
		[Bindable(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public byte[] FileBytes
		{
			get
			{
				Stream fileContent = this.FileContent;
				if (fileContent == null || fileContent == Stream.Null)
				{
					return new byte[0];
				}
				long length = fileContent.Length;
				BinaryReader binaryReader = new BinaryReader(fileContent);
				byte[] array = null;
				if (length > 2147483647L)
				{
					throw new HttpException(SR.GetString("FileUpload_StreamTooLong"));
				}
				if (!fileContent.CanSeek)
				{
					throw new HttpException(SR.GetString("FileUpload_StreamNotSeekable"));
				}
				int num = (int)fileContent.Position;
				int num2 = (int)length;
				try
				{
					fileContent.Seek(0L, SeekOrigin.Begin);
					array = binaryReader.ReadBytes(num2);
				}
				finally
				{
					fileContent.Seek((long)num, SeekOrigin.Begin);
				}
				if (array.Length != num2)
				{
					throw new HttpException(SR.GetString("FileUpload_StreamLengthNotReached"));
				}
				return array;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x060030DD RID: 12509 RVA: 0x0009EFB4 File Offset: 0x0009D1B4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Stream FileContent
		{
			get
			{
				HttpPostedFile postedFile = this.PostedFile;
				if (postedFile != null)
				{
					return this.PostedFile.InputStream;
				}
				return Stream.Null;
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x060030DE RID: 12510 RVA: 0x0009EFDC File Offset: 0x0009D1DC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string FileName
		{
			get
			{
				HttpPostedFile postedFile = this.PostedFile;
				string result = string.Empty;
				if (postedFile != null)
				{
					string fileName = postedFile.FileName;
					try
					{
						result = Path.GetFileName(fileName);
					}
					catch
					{
						result = fileName;
					}
				}
				return result;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x060030DF RID: 12511 RVA: 0x0009F020 File Offset: 0x0009D220
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasFile
		{
			get
			{
				HttpPostedFile postedFile = this.PostedFile;
				return postedFile != null && postedFile.ContentLength > 0;
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x060030E0 RID: 12512 RVA: 0x0009F042 File Offset: 0x0009D242
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasFiles
		{
			get
			{
				return this.PostedFiles.Any((HttpPostedFile f) => f.ContentLength > 0);
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x060030E1 RID: 12513 RVA: 0x0009F06E File Offset: 0x0009D26E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpPostedFile PostedFile
		{
			get
			{
				if (this.Page != null && this.Page.IsPostBack)
				{
					return this.Context.Request.Files[this.UniqueID];
				}
				return null;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x060030E2 RID: 12514 RVA: 0x0009F0A4 File Offset: 0x0009D2A4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IList<HttpPostedFile> PostedFiles
		{
			get
			{
				if (this._postedFiles == null)
				{
					IList<HttpPostedFile> postedFiles = FileUpload._emptyFileCollection;
					if (this.Page != null && this.Page.IsPostBack)
					{
						postedFiles = this.Context.Request.Files.GetMultiple(this.UniqueID);
					}
					this._postedFiles = postedFiles;
				}
				return this._postedFiles;
			}
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x0009F100 File Offset: 0x0009D300
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "file");
			if (this.AllowMultiple)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Multiple, "multiple");
			}
			string uniqueID = this.UniqueID;
			if (uniqueID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x0009F14C File Offset: 0x0009D34C
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			HtmlForm form = this.Page.Form;
			if (form != null && form.Enctype.Length == 0)
			{
				form.Enctype = "multipart/form-data";
			}
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x0009F187 File Offset: 0x0009D387
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			base.Render(writer);
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x0009F1A4 File Offset: 0x0009D3A4
		public void SaveAs(string filename)
		{
			HttpPostedFile postedFile = this.PostedFile;
			if (postedFile != null)
			{
				postedFile.SaveAs(filename);
			}
		}

		// Token: 0x04002098 RID: 8344
		private static readonly IList<HttpPostedFile> _emptyFileCollection = new HttpPostedFile[0];

		// Token: 0x04002099 RID: 8345
		private IList<HttpPostedFile> _postedFiles;
	}
}
