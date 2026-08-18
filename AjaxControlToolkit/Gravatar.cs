using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200009F RID: 159
	[RequiredScript(typeof(ScriptControlBase), 1)]
	[ToolboxBitmap(typeof(Accessor), "Gravatar.bmp")]
	[RequiredScript(typeof(CommonToolkitScripts), 2)]
	[Designer(typeof(GravatarDesigner))]
	[ToolboxData("<{0}:Gravatar runat=\"server\"></{0}:Gravatar>")]
	public class Gravatar : WebControl
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x0000D46D File Offset: 0x0000B66D
		public Gravatar() : base(HtmlTextWriterTag.Img)
		{
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000D477 File Offset: 0x0000B677
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x0000D47F File Offset: 0x0000B67F
		[ClientPropertyName("email")]
		[ExtenderControlProperty]
		[Description("Account email.")]
		[Category("Behavior")]
		public string Email { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000D488 File Offset: 0x0000B688
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x0000D490 File Offset: 0x0000B690
		[Description("Image size.")]
		[Category("Behavior")]
		[ClientPropertyName("size")]
		[ExtenderControlProperty]
		public int? Size { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000D499 File Offset: 0x0000B699
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0000D4A1 File Offset: 0x0000B6A1
		[ClientPropertyName("defaultImage")]
		[Description("Image, that will be shown by default.")]
		[ExtenderControlProperty]
		[Category("Behavior")]
		public string DefaultImage { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000D4AA File Offset: 0x0000B6AA
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x0000D4B2 File Offset: 0x0000B6B2
		[ExtenderControlProperty]
		[Category("Behavior")]
		[Description("Behavior, that will be by default.")]
		[ClientPropertyName("defaultImage")]
		public GravatarDefaultImageBehavior DefaultImageBehavior { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0000D4BB File Offset: 0x0000B6BB
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x0000D4C3 File Offset: 0x0000B6C3
		[Category("Behavior")]
		[Description("Image rating.")]
		[ExtenderControlProperty]
		[ClientPropertyName("rating")]
		public GravatarRating Rating { get; set; }

		// Token: 0x060004DE RID: 1246 RVA: 0x0000D4CC File Offset: 0x0000B6CC
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Src, this.GetUrl(this.Email, this.Size, this.DefaultImage, this.Rating));
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		private string GetUrl(string email, int? size, string defaultImage, GravatarRating rating)
		{
			StringBuilder stringBuilder = new StringBuilder("http://www.gravatar.com/avatar/");
			stringBuilder.Append(this.GetHash(this.Email));
			if (size == null)
			{
				size = new int?(80);
			}
			stringBuilder.Append("?s=");
			stringBuilder.Append(size);
			if (!string.IsNullOrEmpty(defaultImage))
			{
				stringBuilder.Append("&d=");
				stringBuilder.Append(defaultImage);
			}
			else if (this.DefaultImageBehavior != GravatarDefaultImageBehavior.Default)
			{
				string str = this.DefaultImageBehavior.ToString().ToLower();
				GravatarDefaultImageBehavior defaultImageBehavior = this.DefaultImageBehavior;
				if (defaultImageBehavior == GravatarDefaultImageBehavior.MysteryMan)
				{
					str = "mm";
				}
				stringBuilder.Append("&d=" + str);
			}
			if (rating != GravatarRating.Default)
			{
				stringBuilder.Append("&r=");
				stringBuilder.Append(rating.ToString().ToLowerInvariant());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		private string GetHash(string Email)
		{
			Email = Email.ToLower();
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] array = Encoding.ASCII.GetBytes(Email);
			array = md5CryptoServiceProvider.ComputeHash(array);
			string text = string.Empty;
			foreach (byte b in array)
			{
				text += b.ToString("x2");
			}
			return text;
		}
	}
}
