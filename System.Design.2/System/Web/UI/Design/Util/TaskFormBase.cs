using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x02000166 RID: 358
	internal abstract partial class TaskFormBase : DesignerForm
	{
		// Token: 0x06000CB0 RID: 3248 RVA: 0x00051AAB File Offset: 0x0004FCAB
		public TaskFormBase(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			this.InitializeComponent();
			this.InitializeUI();
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00051AC0 File Offset: 0x0004FCC0
		protected Label CaptionLabel
		{
			get
			{
				return this._captionLabel;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x00051AC8 File Offset: 0x0004FCC8
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x00051AD5 File Offset: 0x0004FCD5
		public Image Glyph
		{
			get
			{
				return this._glyphPictureBox.Image;
			}
			set
			{
				this._glyphPictureBox.Image = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00051AE3 File Offset: 0x0004FCE3
		protected Panel TaskPanel
		{
			get
			{
				return this._taskPanel;
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00051DD0 File Offset: 0x0004FFD0
		private void InitializeUI()
		{
			this.UpdateFonts();
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00051DD8 File Offset: 0x0004FFD8
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateFonts();
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00051DE7 File Offset: 0x0004FFE7
		private void UpdateFonts()
		{
			this._captionLabel.Font = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, this.Font.Unit);
		}
	}
}
