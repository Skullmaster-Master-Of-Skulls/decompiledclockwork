namespace System.Web.UI.Design.Util
{
	// Token: 0x02000160 RID: 352
	internal abstract partial class DesignerForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000C62 RID: 3170 RVA: 0x0005111E File Offset: 0x0004F31E
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._serviceProvider = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040007A3 RID: 1955
		private global::System.IServiceProvider _serviceProvider;
	}
}
