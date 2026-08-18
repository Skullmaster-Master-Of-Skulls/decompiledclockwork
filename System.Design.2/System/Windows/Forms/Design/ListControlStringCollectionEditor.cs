using System;
using System.ComponentModel;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000309 RID: 777
	internal class ListControlStringCollectionEditor : StringCollectionEditor
	{
		// Token: 0x06001EC4 RID: 7876 RVA: 0x0001EFCE File Offset: 0x0001D1CE
		public ListControlStringCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000B83AC File Offset: 0x000B65AC
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			ListControl listControl = context.Instance as ListControl;
			if (listControl != null && listControl.DataSource != null)
			{
				throw new ArgumentException(SR.GetString("DataSourceLocksItems"));
			}
			return base.EditValue(context, provider, value);
		}
	}
}
