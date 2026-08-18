using System;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Web.UI.HtmlControls;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000578 RID: 1400
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ControlValueProperty("FileBytes")]
	[ValidationProperty("FileName")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class FileUpload : WebControl
	{
		// Token: 0x060044B4 RID: 17588 RVA: 0x0011A3E8 File Offset: 0x001193E8
		public FileUpload() : base(HtmlTextWriterTag.Input)
		{
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x060044B5 RID: 17589 RVA: 0x0011A3F4 File Offset: 0x001193F4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(true)]
		[Browsable(false)]
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

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x060044B6 RID: 17590 RVA: 0x0011A4B8 File Offset: 0x001194B8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x060044B7 RID: 17591 RVA: 0x0011A4E0 File Offset: 0x001194E0
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

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x060044B8 RID: 17592 RVA: 0x0011A524 File Offset: 0x00119524
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

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x060044B9 RID: 17593 RVA: 0x0011A546 File Offset: 0x00119546
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x060044BA RID: 17594 RVA: 0x0011A57C File Offset: 0x0011957C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "file");
			string uniqueID = this.UniqueID;
			if (uniqueID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x0011A5B0 File Offset: 0x001195B0
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			HtmlForm form = this.Page.Form;
			if (form != null && form.Enctype.Length == 0)
			{
				form.Enctype = "multipart/form-data";
			}
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x0011A5EB File Offset: 0x001195EB
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			base.Render(writer);
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x0011A608 File Offset: 0x00119608
		public void SaveAs(string filename)
		{
			HttpPostedFile postedFile = this.PostedFile;
			if (postedFile != null)
			{
				postedFile.SaveAs(filename);
			}
		}
	}
}
